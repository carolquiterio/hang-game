//19351- Carolina Moraes
//19367- Leonardo Branco
using System;
using System.Windows.Forms;

namespace apJogoDeForca
{
  public partial class FrmCadastro : Form
  {
    public FrmCadastro()
    {
      InitializeComponent();
    }
    
    // declaração global; asPld fica acessível em todo o programa
    VetorPalavraDica asPld;

    int ondeIncluir;

        

        private void FrmFunc_Load(object sender, EventArgs e)
        {
      // prepara o toolstrip para exibir os ícones dos botões:
      tsBotoes.ImageList = imlBotoes;
      int indice = 0;
      foreach (ToolStripItem item in tsBotoes.Items)
        if (item is ToolStripButton)
          (item as ToolStripButton).ImageIndex = indice++;

      if (dlgAbrir.ShowDialog() == DialogResult.OK)
      {
        asPld = new VetorPalavraDica(30);
        asPld.LerDados(dlgAbrir.FileName);
        asPld.PosicionarNoInicio();
        AtualizarTela();
      }
        }
    void LimparTela()
    {
      txtDica.Clear();
      txtPalavra.Clear();
    }
    void AtualizarTela()
    {
      if (asPld.EstaVazio)
        LimparTela();
      else
      {
        PalavraDica qualPalDica = asPld[asPld.PosicaoAtual];
        txtDica.Text = qualPalDica.Dica + "";
        txtPalavra.Text = qualPalDica.Palavra + "";
      }
      stlbMensagem.Text = "Registro " + (asPld.PosicaoAtual+1) + 
                                  " de " + asPld.Tamanho;
    }

    private void btnProximo_Click(object sender, EventArgs e)
    {
      asPld.AvancarPosicao();
      AtualizarTela();
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
      asPld.PosicionarNoInicio();
      AtualizarTela();
    }

    private void btnAnterior_Click(object sender, EventArgs e)
    {
      asPld.RetrocederPosicao();
      AtualizarTela();
    }

    private void btnUltimo_Click(object sender, EventArgs e)
    {
      asPld.PosicionarNoUltimo();
      AtualizarTela();
    }

    private void btnSair_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void FrmFunc_FormClosing(object sender, FormClosingEventArgs e)
    {
      asPld.GravacaoEmDisco(dlgAbrir.FileName);
    }

    private void btnNovo_Click(object sender, EventArgs e)
    {
      asPld.SituacaoAtual = Situacao.incluindo;
      LimparTela();
      stlbMensagem.Text = "Digite a nova palavra!";
      txtPalavra.Focus();
    }

    private void txtPalavra_Leave(object sender, EventArgs e)
    {
      if (txtPalavra.Text == "")
        MessageBox.Show("Digite uma palavra válida!");
      else
      if (asPld.SituacaoAtual == Situacao.incluindo)
      {
                var palDica = new PalavraDica(txtDica.Text, txtPalavra.Text);
        ondeIncluir = -1;
        if (asPld.Existe(palDica, ref ondeIncluir))
        {
          MessageBox.Show("Matrícula repetida, não pode ser incluída");
          asPld.SituacaoAtual = Situacao.navegando;
          AtualizarTela();
        }
        else
        {
          txtDica.Focus();
          stlbMensagem.Text = "Digite os demais campos e pressione [Salvar]";
        }
      }
      else
        if (asPld.SituacaoAtual == Situacao.procurando)
        {
          // criamos objeto Funcionario apenas com a matrícula como
          // campo preenchido, para fazermos a pesquisa binária
          // dessa matrícula no vetor interno dados de ofFunc
          var palDicaProc = new PalavraDica(txtDica.Text, txtPalavra.Text);
          int ondeEsta = -1;
         if (asPld.ExisteSequencial(palDicaProc, ref ondeEsta))
           asPld.PosicaoAtual = ondeEsta;  // reposiciona para exibir
         else
            MessageBox.Show("Palavra não encontrada!");

          asPld.SituacaoAtual = Situacao.navegando;
          AtualizarTela();
        }
    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
      if (asPld.SituacaoAtual == Situacao.incluindo)
      {
        var novoFunc = new PalavraDica(txtDica.Text, txtPalavra.Text);
        asPld.Incluir(novoFunc, ondeIncluir);
        asPld.PosicaoAtual = ondeIncluir;
        AtualizarTela();
      }
      else
        if (asPld.SituacaoAtual == Situacao.editando)
        {
          var funcAlterado = new PalavraDica(txtDica.Text, txtPalavra.Text);
          asPld[asPld.PosicaoAtual] = funcAlterado;
          asPld.SituacaoAtual = Situacao.navegando;
          AtualizarTela();
        }
    }

    private void btnExcluir_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Deseja realmente excluir?",
                          "Atenção para exclusão!",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) == DialogResult.Yes)
      {
        asPld.Excluir(asPld.PosicaoAtual);

        if (asPld.PosicaoAtual >= asPld.Tamanho)
           asPld.PosicionarNoUltimo();

        AtualizarTela();
      }
    }

    private void btnProcurar_Click(object sender, EventArgs e)
    {
      asPld.SituacaoAtual = Situacao.procurando;
      LimparTela();
      stlbMensagem.Text = "Digite a Palavra desejada: ";
      txtPalavra.Focus();
    }

    private void tpLista_Enter(object sender, EventArgs e)
    {
            asPld.Listar(txtLista,
             "Palavra                                                                                                 Dica" + Environment.NewLine); 
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      asPld.SituacaoAtual = Situacao.navegando;
      AtualizarTela();
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
      txtPalavra.ReadOnly = true; // para não alterar matrícula
      asPld.SituacaoAtual = Situacao.editando;
      txtDica.Focus();
      stlbMensagem.Text="Digite o novo valor e pressione [Salvar]";
    }

    private void txtLista_TextChanged(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)  //// BTN ORDENAR  // tinhamnos excluido ele achando q n dava pra usar em string
    {                                                           // mas agora esta funcionando 
       asPld.Ordenar();
       asPld.PosicionarNoInicio();
       AtualizarTela(); 
    }     
  }
}
