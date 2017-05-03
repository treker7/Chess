using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace GUI
{
    public partial class Form1 : Form
    {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private string fenFilter = "FEN Files|*.fen";

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(chessBoard1.BoardLength, chessBoard1.BoardLength + menuStrip1.Height);

            saveFileDialog1.Filter = fenFilter;
            saveFileDialog1.Title = "Save Game";
            saveFileDialog1.FileName = "Game";
            saveFileDialog1.RestoreDirectory = true;

            openFileDialog1.Filter = fenFilter;
            openFileDialog1.Title = "Open Game";
            openFileDialog1.RestoreDirectory = true;
        }        

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessBoard1.SetBoard(ChessBoard.START_FEN);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)openFileDialog1.OpenFile();
                string fenStr = (new StreamReader(fs)).ReadToEnd();

                try
                {
                    chessBoard1.SetBoard(fenStr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    fs.Close();
                }
            }                       
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                byte[] fen = new UTF8Encoding(true).GetBytes(chessBoard1.ToString());
                fs.Write(fen, 0, fen.Length);
                fs.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
