using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using KanimExplorer.Forms;
using KanimExplorer.Logging;

using KanimLib;
using KanimLib.KanimModel;

using Microsoft.Extensions.Logging;

namespace KanimExplorer.Controls
{
	public partial class KanimDataTreeControl : UserControl
	{
		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("DataTreeControl");

		private bool _rebuilding = false;

		private KanimPackage _data;
		
		private object SelectedObject => treeView.SelectedNode?.Tag;

		public KanimDataTreeControl()
		{
			InitializeComponent();

			buttonAdd.Visible = false;
			buttonRemove.Visible = false;
			buttonRename.Visible = false;
			buttonDuplicate.Visible = false;
			buttonReplaceSprite.Visible = false;

			DocumentManager.Instance.LoadedBuildChanged += DocumentManager_LoadedBuildChanged;
			DocumentManager.Instance.LoadedAnimChanged += DocumentManager_LoadedAnimChanged;
		}

		private void DocumentManager_LoadedBuildChanged(object sender, EventArgs e)
		{
			SetKanim(DocumentManager.Instance.Data);
		}

		private void DocumentManager_LoadedAnimChanged(object sender, EventArgs e)
		{
			SetKanim(DocumentManager.Instance.Data);
		}

		public void SetKanim(KanimPackage data)
		{
			_data = data;
			RebuildTree();
		}

		public void RebuildTree()
		{
			_rebuilding = true;

			treeView.BeginUpdate();
			treeView.Nodes.Clear();

			if (_data == null)
			{
				_rebuilding = false;
				treeView.EndUpdate();
				return;
			}

			if (_data.Build != null)
			{
				TreeNode buildNode = new TreeNode("Texture Atlas (Build)");
				buildNode.Name = "build";
				buildNode.ImageIndex = 0;
				buildNode.SelectedImageIndex = 0;
				buildNode.Tag = _data.Build;

				foreach (KSymbol symbol in _data.Build.Symbols)
				{
					TreeNode symbolNode = new TreeNode(symbol.Name);
					symbolNode.Name = symbol.Name;
					symbolNode.ImageIndex = 1;
					symbolNode.SelectedImageIndex = 1;
					symbolNode.Tag = symbol;

					foreach (KFrame frame in symbol.Frames)
					{
						TreeNode frameNode = new TreeNode(frame.Index.ToString());
						frameNode.Name = frame.Index.ToString();
						frameNode.ImageIndex = 2;
						frameNode.SelectedImageIndex = 2;
						frameNode.Tag = frame;

						symbolNode.Nodes.Add(frameNode);
					}

					buildNode.Nodes.Add(symbolNode);
				}

				treeView.Nodes.Add(buildNode);
			}

			if (_data.Anim != null)
			{
				TreeNode animNode = new TreeNode("Animations");
				animNode.ImageIndex = 3;
				animNode.SelectedImageIndex = 3;
				animNode.Tag = _data.Anim;
				animNode.Nodes.Add(new TreeNode("Placeholder Bank") { Name = "placeholder" });

				treeView.Nodes.Add(animNode);
			}

			_rebuilding = false;
			treeView.EndUpdate();
		}

		private void LazyLoad(TreeNode node)
		{
			if (node.Nodes.Count == 0) return;
			if (node.FirstNode.Name != "placeholder") return;

			treeView.BeginUpdate();
			node.Nodes.Clear();
			TreeNode[] children = null;
			switch (node.Tag)
			{
				case KAnim anim:
					children = MakeChildNodes(anim).ToArray();
					break;

				case KAnimBank bank:
					children = MakeChildNodes(bank).ToArray();
					break;

				case KAnimFrame frame:
					children = MakeChildNodes(frame).ToArray();
					break;
			}

			if (children != null)
			{
				node.Nodes.AddRange(children);
			}

			treeView.EndUpdate();
		}

		private IEnumerable<TreeNode> MakeChildNodes(KAnim anim)
		{
			foreach (KAnimBank bank in anim.Banks)
			{
				TreeNode node = new TreeNode(bank.Name)
				{
					ImageIndex = 4,
					SelectedImageIndex = 4,
					Tag = bank
				};
				node.Nodes.Add(new TreeNode("Placeholder Bank") { Name = "placeholder" });
				yield return node;
			}
		}

		private IEnumerable<TreeNode> MakeChildNodes(KAnimBank bank)
		{
			for (int i = 0; i < bank.Frames.Count; i++)
			{
				KAnimFrame frame = bank.Frames[i];
				TreeNode node = new TreeNode($"Frame {i}")
				{
					ImageIndex = 5,
					SelectedImageIndex = 5,
					Tag = frame
				};
				node.Nodes.Add(new TreeNode("Placeholder Frame") { Name = "placeholder" });
				yield return node;
			}
		}

		private IEnumerable<TreeNode> MakeChildNodes(KAnimFrame animFrame)
		{
			for (int j = 0; j < animFrame.Elements.Count; j++)
			{
				KAnimElement element = animFrame.Elements[j];
				TreeNode node = new TreeNode($"Element {j}")
				{
					ImageIndex = 6,
					SelectedImageIndex = 6,
					Tag = element
				};
				yield return node;
			}
		}

		private void SelectNode(string key)
		{
			TreeNode node = null;
			if (key == "build")
			{
				node = treeView.Nodes["build"];
			}
			else if (key.StartsWith("symbol:"))
			{
				var parts = key.Split(':');
				if (parts.Length == 2)
				{
					var buildNode = treeView.Nodes["build"];
					node = buildNode?.Nodes[parts[1]];
				}
			}
			else if (key.StartsWith("frame:"))
			{
				var parts = key.Split(':');
				if (parts.Length == 3)
				{
					var buildNode = treeView.Nodes["build"];
					var symbolNode = buildNode?.Nodes[parts[1]];
					node = symbolNode?.Nodes[parts[2]];
				}
			}

			if (node != null)
			{
				node.EnsureVisible();
				treeView.SelectedNode = node;
			}
		}

		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node == null) return;
			if (e.Node.Nodes.Count == 0 || e.Node.FirstNode == null) return;
			
			LazyLoad(e.Node);
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_rebuilding) return;

			var obj = e.Node?.Tag;
			
			buttonAdd.Visible = false;
			buttonRemove.Visible = false;
			buttonRename.Visible = false;
			buttonDuplicate.Visible = false;
			buttonReplaceSprite.Visible = false;

			DocumentManager.Instance.SelectObject(obj);

			// Is this needed?
			TreeView tree = sender as TreeView;
			if (tree == null) return;

			switch (obj)
			{
				case KBuild:
					//buttonAdd.Visible = true;
					break;

				case KSymbol:
					//buttonAdd.Visible = true;
					buttonRemove.Visible = true;
					buttonRename.Visible = true;
					buttonDuplicate.Visible = true;
					break;

				case KFrame:
					//buttonRemove.Visible = true;
					buttonReplaceSprite.Visible = true;
					break;
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			Func<bool> action = null;
			string selectionKey = null;

			switch (SelectedObject)
			{
				case KBuild build:
					// TODO: Add a symbol
					return;

				case KSymbol symbol:
					// TODO: Add a frame
					return;

				default:
					return;
			}

			Debug.Assert(action != null);

			if (!UIUtils.TryWithErrorMessage(action, $"Adding {SelectedObject.GetType().Name}", _log)) return;

			RebuildTree();

			if (selectionKey != null)
			{
				SelectNode(selectionKey);
			}
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			Func<bool> action = null;

			switch (SelectedObject)
			{
				case KSymbol symbol:
					// TODO: Remove a symbol
					action = () => ModalTasks.RemoveSymbol(_data, symbol.Name, _log);
					break;

				case KFrame frame:
					// TODO: Remove a frame
					break;

				case KAnim anim:
					// TODO: Remove an anim
					break;

				default:
					return;
			}

			Debug.Assert(action != null);

			if (!UIUtils.TryWithErrorMessage(action, $"Removing {SelectedObject.GetType().Name}", _log)) return;

			RebuildTree();
		}

		private void buttonRename_Click(object sender, EventArgs e)
		{
			Func<bool> action = null;
			string selectionKey = null;

			switch (SelectedObject)
			{
				case KSymbol symbol:
					action = () => ModalTasks.RenameSymbol(_data, symbol.Name, _log);
					selectionKey = $"symbol:{symbol.Name}";
					break;

				default:
					return;
			}

			Debug.Assert(action != null);

			if (!UIUtils.TryWithErrorMessage(action, $"Renaming {SelectedObject.GetType().Name}", _log)) return;

			RebuildTree();

			if (selectionKey != null)
			{
				SelectNode(selectionKey);
			}
		}

		private void buttonDuplicate_Click(object sender, EventArgs e)
		{
			Func<bool> action = null;
			string selectionKey = null;

			switch (SelectedObject)
			{
				case KSymbol symbol:
					action = () => ModalTasks.DuplicateSymbol(_data, symbol.Name, _log);
					selectionKey = $"symbol:{symbol.Name}";
					break;

				default:
					return;
			}

			Debug.Assert(action != null);

			if (!UIUtils.TryWithErrorMessage(action, $"Duplicating {SelectedObject.GetType().Name}", _log)) return;

			RebuildTree();

			if (selectionKey != null)
			{
				SelectNode(selectionKey);
			}
		}

		private void buttonReplaceSprite_Click(object sender, EventArgs e)
		{
			Func<bool> action = null;
			string selectionKey = null;

			switch (SelectedObject)
			{
				case KFrame frame:
					action = () => ModalTasks.ReplaceSprite(_data, frame, _log);
					selectionKey = $"frame:{frame.Parent.Name}:{frame.Index}";
					break;

				default:
					return;
			}

			Debug.Assert(action != null);

			UIUtils.TryWithErrorMessage(action, $"Editing Pivot", _log);

			if (selectionKey != null)
			{
				SelectNode(selectionKey);
			}
		}

		
	}
}
