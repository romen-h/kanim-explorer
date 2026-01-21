using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KanimExplorer.Logging;

namespace KanimExplorer.Forms
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		private void LogForm_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible)
			{
				MemoryLogger.LogEntriesChanged += MemoryLogger_LogEntriesChanged;
				string text = MemoryLogger.LogText;
				richTextBox.Text = text;
				richTextBox.Select(text.Length, 0);
				richTextBox.ScrollToCaret();
			}
			else
			{
				MemoryLogger.LogEntriesChanged -= MemoryLogger_LogEntriesChanged;
			}
		}

		private void MemoryLogger_LogEntriesChanged(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(() => MemoryLogger_LogEntriesChanged(sender, e));
				return;
			}

			if (!Visible) return;

			string text = MemoryLogger.LogText;
			richTextBox.Text = text;
			richTextBox.Select(text.Length, 0);
			richTextBox.ScrollToCaret();
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}
	}
}
