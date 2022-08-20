﻿using System.Diagnostics;
using System.Windows.Forms;

using KanimExplorer.OpenGL;
using KanimLib;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.Forms
{
	public partial class AnimationForm : Form
	{
		KAnimPackage data;

		KAnimPackage dupeData;

		KAnimBank SelectedBank;

		KAnimBank SelectedDupeBank;

		GLControl display;

		AnimationRenderer renderer;

		AnimationRenderer dupeRenderer;

		int currentFrame = 0;

		bool playing = false;

		Timer playbackTimer;

		public AnimationForm()
		{
			InitializeComponent();

			playbackTimer = new Timer();
			playbackTimer.Interval = 33;
			playbackTimer.Tick += PlaybackTimer_Tick;
			
			renderer = new AnimationRenderer();
			dupeRenderer = new AnimationRenderer();
			InitGL();
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
			textBoxFrameNumber.Text = currentFrame.ToString();
			playbackTimer.Start();
		}

		private void InitGL()
		{
			GraphicsMode mode = new GraphicsMode(32, 24, 8, 8);

			display = new GLControl(mode, 3, 3, GraphicsContextFlags.Default);
			display.Dock = DockStyle.Fill;
			display.SizeChanged += Display_SizeChanged;
			display.Paint += Display_Paint;

			display.MakeCurrent();

			renderer.Initialize();
			dupeRenderer.Initialize();

			panelDisplayArea.Controls.Add(display);
		}

		private void Display_SizeChanged(object sender, System.EventArgs e)
		{
			display.MakeCurrent();
			GL.Viewport(0, 0, display.Width, display.Height);
			renderer.SetViewport(display.Width, display.Height);
			dupeRenderer.SetViewport(display.Width, display.Height);
			display.Invalidate();
		}

		private void Display_Paint(object sender, PaintEventArgs e)
		{
			display.MakeCurrent();

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			if (SelectedBank != null)
			{
				renderer.Render(data.Build, SelectedBank, currentFrame);
			}

			if (SelectedDupeBank != null)
			{
				dupeRenderer.Render(dupeData.Build, SelectedDupeBank, currentFrame);
			}

			display.SwapBuffers();
		}

		public void SetData(KAnimPackage pkg)
		{
			Debug.Assert(pkg.IsComplete);

			data = pkg;

			renderer.SetTexture(data.Texture);

			foreach (KAnimBank bank in data.Anim.Banks)
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
			dupeData = KanimLoader.BrowseForKanimFiles();

			listBoxDupeBanks.Items.Clear();

			if (dupeData != null && dupeData.HasTexture && dupeData.HasAnim)
			{
				listBoxDupeBanks.Enabled = true;
				foreach (KAnimBank bank in dupeData.Anim.Banks)
				{
					listBoxDupeBanks.Items.Add(bank);
				}
				dupeRenderer.SetTexture(dupeData.Texture);
			}
			else
			{
				listBoxDupeBanks.Enabled = false;
			}
		}

		
	}
}
