using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dialog
{
	public partial class Prompt : Form
	{
		public string Value { get { return textBox1.Text; } }

		public Prompt()
		{
			InitializeComponent();
			this.Text = "Enter Input.";
		}
		public Prompt(string prompt)
		{
			InitializeComponent();
			this.Text = prompt;
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{			
			if (e.KeyCode == Keys.Enter)
			{
				button1.PerformClick();
			}
		}

		private void ok_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	
		private void cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}