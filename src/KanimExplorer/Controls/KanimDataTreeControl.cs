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

		private KanimPackage _data;

		public object SelectedObject
		{ get; private set; }

		public event EventHandler SelectedObjectChanged;

		public KanimDataTreeControl()
		{
			InitializeComponent();

			buttonAdd.Visible = false;
			buttonRemove.Visible = false;
			buttonRename.Visible = false;
			buttonDuplicate.Visible = false;
			buttonReplaceSprite.Visible = false;
		}

		public void SetKanim(KanimPackage data)
		{
			if (_data != data)
			{
				_data = data;
			}
			RebuildTree();
		}

		public void RebuildTree()
		{
			treeView.BeginUpdate();
			treeView.Nodes.Clear();

			if (_data == null)
			{
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

				foreach (KAnimBank bank in _data.Anim.Banks)
				{
					TreeNode bankNode = new TreeNode(bank.Name);
					bankNode.ImageIndex = 4;
					bankNode.SelectedImageIndex = 4;
					bankNode.Tag = bank;

					for (int i = 0; i < bank.Frames.Count; i++)
					{
						KAnimFrame frame = bank.Frames[i];
						TreeNode frameNode = new TreeNode($"Frame {i}");
						frameNode.ImageIndex = 5;
						frameNode.SelectedImageIndex = 5;
						frameNode.Tag = frame;

						for (int j = 0; j < frame.Elements.Count; j++)
						{
							KAnimElement element = frame.Elements[j];
							TreeNode elementNode = new TreeNode($"Element {j}");
							elementNode.ImageIndex = 6;
							elementNode.SelectedImageIndex = 6;
							elementNode.Tag = element;

							frameNode.Nodes.Add(elementNode);
						}

						bankNode.Nodes.Add(frameNode);
					}

					animNode.Nodes.Add(bankNode);
				}

				treeView.Nodes.Add(animNode);
			}

			treeView.EndUpdate();
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

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			buttonAdd.Visible = false;
			buttonRemove.Visible = false;
			buttonRename.Visible = false;
			buttonDuplicate.Visible = false;
			buttonReplaceSprite.Visible = false;

			// Is this needed?
			TreeView tree = sender as TreeView;
			if (tree == null) return;

			SelectedObject = e.Node?.Tag;

			switch (SelectedObject)
			{
				case KBuild:
					//buttonAdd.Visible = true;
					break;

				case KSymbol:
					//buttonAdd.Visible = true;
					//buttonRemove.Visible = true;
					buttonRename.Visible = true;
					buttonDuplicate.Visible = true;
					break;

				case KFrame:
					//buttonRemove.Visible = true;
					buttonReplaceSprite.Visible = true;
					break;

				case KAnim:
					//buttonRemove.Visible = true;
					//buttonRename.Visible = true;
					break;
			}

			SelectedObjectChanged?.Invoke(this, EventArgs.Empty);
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
