//19351- Carolina Moraes
//19367- Leonardo Branco
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apJogoDeForca
{
  public partial class frmPrincipal : Form
  {
    FrmCadastro frmCadastro = null;
    frmForca frmForca = null;
    public frmPrincipal()
    {
      InitializeComponent();
    }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastro = new FrmCadastro();
            frmCadastro.Show();
        }

        private void jogarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmForca = new frmForca();
            frmForca.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // frmForca.Close();
            //FrmCadastro.Close();
            //FrmPrincipal.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
