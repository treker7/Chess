using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(chessBoard1.BoardLength, chessBoard1.BoardLength + menuStrip1.Height);

            chessBoard1.PutPiece(new Piece(false, "k"), new Square(5, 5));
            chessBoard1.DoMove(new Square(5, 5), new Square(1, 1));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
