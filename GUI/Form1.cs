using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dialog;

namespace GUI
{
    public partial class Form1 : Form
    {
        private Prompt fenPrompt = new Prompt("Enter FEN string.");

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(chessBoard1.BoardLength, chessBoard1.BoardLength + menuStrip1.Height);            
        }        

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessBoard1.SetBoard(ChessBoard.START_FEN);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fenPrompt.ShowDialog();
            if (fenPrompt.DialogResult == DialogResult.OK)
            {
                try
                {
                    chessBoard1.SetBoard(fenPrompt.Value);
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }               
    }
}
