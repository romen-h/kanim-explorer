using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using KanimExplorer.OpenGL;

using KanimLib;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.WinForms;

namespace KanimExplorer.Forms
{
	public partial class AnimationForm : Form
	{
		KanimPackage _data;

		KanimPackage _dupeData;

		KAnimBank SelectedBank;

		KAnimBank SelectedDupeBank;

		//GLControl display;

		AnimationRenderer renderer;

		AnimationRenderer dupeRenderer;

		int currentFrame = 0;

		bool playing = false;

		Timer playbackTimer;

		PointF _viewPos = new PointF(0,-100);
		float _viewScale = 1f;

		public AnimationForm(KanimPackage data)
		{
			InitializeComponent();

			playbackTimer = new Timer();
			playbackTimer.Interval = 33;
			playbackTimer.Tick += PlaybackTimer_Tick;
			
			renderer = new AnimationRenderer();
			dupeRenderer = new AnimationRenderer();

			_data = data;
		}

		private void PlaybackTimer_Tick(object sender, System.EventArgs e)
		{
			if (SelectedBank == null || !playing) return;

			currentFrame++;
			if (currentFrame >= SelectedBank.FrameCount)
			{
				currentFrame = 0;
			}
			textBoxFrameNumber.Text = currentFrame.ToString();

			display.Invalidate();
		}

		private void AnimationForm_Load(object sender, System.EventArgs e)
		{
			InitGL();
			textBoxFrameNumber.Text = currentFrame.ToString();
			playbackTimer.Start();
		}

		private void InitGL()
		{
#if false
			display = new GLControl(new GLControlSettings()
			{
				API = OpenTK.Windowing.Common.ContextAPI.OpenGL,
				APIVersion = new System.Version(3,3,0,0),
				Flags = OpenTK.Windowing.Common.ContextFlags.Default | OpenTK.Windowing.Common.ContextFlags.ForwardCompatible
			});

			//display.Dock = DockStyle.Fill;
			//display.SizeChanged += Display_SizeChanged;
			//display.Paint += Display_Paint;
#endif

			display.MakeCurrent();

			renderer.Initialize();
			dupeRenderer.Initialize();

			SetData(_data);
			UpdateView();
		}

		private void Display_Resize(object sender, System.EventArgs e)
		{
			display.MakeCurrent();
			GL.Viewport(0, 0, display.Width, display.Height);
			UpdateView();
		}

		private void Display_Paint(object sender, PaintEventArgs e)
		{
			display.MakeCurrent();

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			if (SelectedBank != null)
			{
				renderer.Render(_data.Build, SelectedBank, currentFrame);
			}

			if (SelectedDupeBank != null)
			{
				dupeRenderer.Render(_dupeData.Build, SelectedDupeBank, currentFrame);
			}

			display.SwapBuffers();
		}

		private void UpdateView()
		{
			display.MakeCurrent();
			
			renderer.SetViewport(_viewPos, _viewScale, display.Width, display.Height);
			dupeRenderer.SetViewport(_viewPos, _viewScale, display.Width, display.Height);

			display.Invalidate();
		}

		private void SetData(KanimPackage pkg)
		{
			Debug.Assert(pkg.IsComplete);

			_data = pkg;

			renderer.SetTexture(_data.Texture);

			foreach (KAnimBank bank in _data.Anim.Banks)
			{
				listBoxBanks.Items.Add(bank);
			}
		}

		private void listBoxBanks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (listBoxBanks.SelectedIndex < 0)
			{
				SelectedBank = null;
			}
			else
			{
				SelectedBank = listBoxBanks.SelectedItem as KAnimBank;
			}

			currentFrame = 0;
			textBoxFrameNumber.Text = currentFrame.ToString();

			display.Invalidate();
		}

		private void listBoxDupeBanks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (listBoxDupeBanks.SelectedIndex < 0)
			{
				SelectedDupeBank = null;
			}
			else
			{
				SelectedDupeBank = listBoxDupeBanks.SelectedItem as KAnimBank;
			}

			display.Invalidate();
		}

		private void AnimationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			playbackTimer.Stop();
			renderer.Release();
			dupeRenderer.Release();
		}

		private void buttonPrevFrame_Click(object sender, System.EventArgs e)
		{
			if (SelectedBank == null) return;

			currentFrame--;
			if (currentFrame < 0) currentFrame = SelectedBank.FrameCount - 1;

			textBoxFrameNumber.Text = currentFrame.ToString();

			display.Invalidate();
		}

		private void buttonNextFrame_Click(object sender, System.EventArgs e)
		{
			if (SelectedBank == null) return;

			currentFrame++;
			if (currentFrame >= SelectedBank.FrameCount) currentFrame = 0;

			textBoxFrameNumber.Text = currentFrame.ToString();

			display.Invalidate();
		}

		private void checkBoxPlay_CheckedChanged(object sender, System.EventArgs e)
		{
			playing = checkBoxPlay.Checked;
		}

		private void closeToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void openInteractKanimToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			_dupeData = KanimLoader.BrowseForKanimFiles();

			listBoxDupeBanks.Items.Clear();

			if (_dupeData != null && _dupeData.HasTexture && _dupeData.HasAnim)
			{
				listBoxDupeBanks.Enabled = true;
				foreach (KAnimBank bank in _dupeData.Anim.Banks)
				{
					listBoxDupeBanks.Items.Add(bank);
				}
				dupeRenderer.SetTexture(_dupeData.Texture);
			}
			else
			{
				listBoxDupeBanks.Enabled = false;
			}
		}

		private bool _leftDragging = false;
		private PointF _lastPos = PointF.Empty;

		private void Display_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_leftDragging = true;
				_lastPos = e.Location;
			}
		}

		private void Display_MouseMove(object sender, MouseEventArgs e)
		{
			if (_leftDragging)
			{
				float dx = ((float)e.Location.X - _lastPos.X) * (_viewScale);
				float dy = ((float)e.Location.Y - _lastPos.Y) * (_viewScale);
				_viewPos = new PointF(_viewPos.X - dx, _viewPos.Y + dy);
				_lastPos = e.Location;
				UpdateView();
			}
		}

		private void Display_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_leftDragging = false;
				_lastPos = PointF.Empty;
			}
		}

		private void Display_MouseLeave(object sender, System.EventArgs e)
		{
			_leftDragging = false;
			_lastPos = PointF.Empty;
		}

		private void Display_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
			{
				// Down
				_viewScale *= 1.1f;
				if (_viewScale > 10f) _viewScale = 10f;
				UpdateView();
			}
			else if (e.Delta > 0)
			{
				// Up
				_viewScale /= 1.1f;
				if (_viewScale < 0.1f) _viewScale = 0.1f;
				UpdateView();
			}
		}
	}
}
