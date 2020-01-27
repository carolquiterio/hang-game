//19351- Carolina Moraes
//19367- Leonardo Branco
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apJogoDeForca
{
    public partial class frmForca : Form
    {
        VetorPalavraDica vetPal ;
        int contagem = 120;
        string palavraComTrim;
        int letrasCorretas = 0;
        int errosCometidos = 0;
        int errosRestantes = 10;
        char[] letras;


        public frmForca()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void frmForca_Load(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                vetPal = new VetorPalavraDica(30);
                vetPal.LerDados(dlgAbrir.FileName);
                vetPal.PosicionarNoInicio();

            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void cbComDica_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            ReiniciarImagensEbotoes();
            letras = null;
            ResetarTimer();
            letrasCorretas = 0;
            errosCometidos = 0;
            errosRestantes = 10;
            tmrTempo.Enabled = true;
            

            for (int indice = 0; indice < 15; indice++) 
            {      
                if(dataGridView1.Columns[indice].Visible == true)
                dataGridView1.Columns[indice].Visible = false;  //Aqui ele limpa os espaçoes que se referem as palavras.
                dataGridView1.Rows[0].Cells[indice].Value = "";
            }

            if (cbComDica.Checked)
            {
                
                Random random = new Random();
                PalavraDica qualPal = vetPal[random.Next(vetPal.Tamanho)]; // esse objeto recebe um dado do vetor vetPal,
                string dicaRandomizada = qualPal.Dica;                     // pega a dica
                lbDica.Text = "Dica: " + dicaRandomizada;                  // tentamos codificar aqui um Enviroment.NewLine para a dica pular linha
                lbPontos.Text = "Pontos: " + letrasCorretas.ToString();    // e não ia para fora no forms, mas não conseguimos, então aumentamos o forms  
                lbErros.Text = "Erros(" + errosRestantes.ToString() + "): " + errosCometidos.ToString();

                palavraComTrim = qualPal.Palavra;          // variável para armazenar a palavra.
                palavraComTrim = palavraComTrim.Trim();    // dentro dessa variável a palavra com o Trim()
                letras = qualPal.Palavra.ToCharArray();    // vetor com cada letra dessa palavra

                for (int indice = 0; indice < palavraComTrim.Length; indice++)  // o for percorre esse vetor, 
                {                                                               // de acordo com o tamanho da palavra, sem espaços.
                    dataGridView1.Columns[indice].Visible = true;
                }
            }
            else
            {
                lbDica.Text = "Dica: ----------------------";
                lbPontos.Text = "Pontos: " + letrasCorretas.ToString();
                lbErros.Text = "Erros(" + errosRestantes.ToString() + "): " + errosCometidos.ToString();

                Random random = new Random();
                PalavraDica qualPal = vetPal[random.Next(vetPal.Tamanho)];
                string palavra = qualPal.Palavra.ToUpper();    
                palavraComTrim = palavra.Trim();
                letras = palavraComTrim.ToCharArray();

                for (int indice = 0; indice < palavraComTrim.Length; indice++)
                {
                    dataGridView1.Columns[indice].Visible = true;
                }
            }
        }

   
        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void tmrTempo_Tick(object sender, EventArgs e)
        {
            
            lbTempoRestante.Text = "Tempo Restante: " + contagem.ToString() +"s";
            if(contagem == 0)
            {
                MessageBox.Show("Acabou o tempo!");
                tmrTempo.Enabled = false;
                tmrTempo.Stop();
                
            }
            else
                contagem--;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool letraCorreta = false;
            ((Button)sender).Enabled = false;
            string letra = ((Button)sender).Text.ToLower();


            for (int i = 0; i < palavraComTrim.Length; i++)
            {
                if (letra == letras[i].ToString())
                {
                    letrasCorretas++;
                    lbPontos.Text = "Pontos: " + letrasCorretas.ToString();
                    dataGridView1.Rows[0].Cells[i].Value = $"{letra}";
                    letraCorreta = true;
                }
            }
            
            if (letrasCorretas == palavraComTrim.Length)
            {
                MessageBox.Show("Parabéns, você venceu! Aperte iniciar para jogar novamente!");
            }
            if (!letraCorreta)
            {
                errosCometidos += 1;
                errosRestantes -= 1;
                lbErros.Text = "Erros(" + errosRestantes.ToString() + "): " + errosCometidos.ToString();

                switch (errosCometidos)
                {
                    case 1: pbCabecaVivo.Visible = true; break;
                    case 2: pictureBox7.Visible = true; break;
                    case 3: pictureBox8.Visible = true; break;
                    case 4: pictureBox11.Visible = true; break;
                    case 5: pictureBox12.Visible = true; break;
                    case 6: pictureBox13.Visible = true; break;
                    case 7: pictureBox9.Visible = true; break;
                    case 8: pictureBox10.Visible = true; break;
                    case 9:
                        pictureBox10.Visible = false;
                        pictureBox14.Visible = true;
                        pictureBox16.Visible = true;
                        pictureBox15.Visible = true; break;
                    case 10:
                        pictureBox17.Visible = true;
                        pbCabecaVivo.Visible = false; pictureBox8.Visible = false; pictureBox7.Visible = false;
                        pictureBox11.Visible = false; pictureBox12.Visible = false; pictureBox13.Visible = false;
                        pictureBox9.Visible = false; pictureBox10.Visible = false; pictureBox14.Visible = false;
                        pictureBox15.Visible = false; pictureBox16.Visible = false;
                        MessageBox.Show("Voce perdeu! Aperte iniciar para jogar novamente!"
                            + Environment.NewLine + "                      A palavra era: " + palavraComTrim);
                        break;
                }

            }
           
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void ResetarTimer()
        {
            contagem = 120;
            tmrTempo.Stop();            
            tmrTempo.Start();
        }

        private void ReiniciarImagensEbotoes()
        {
            foreach (Control botao in panel1.Controls)
                ((Button)botao).Enabled = true;
            //foreach (Control img in frmForca.Controls)                           // tentamos o mesmo para imagens mas não deu certo
            //    ((Image)img).Visible = false;

            pictureBox17.Visible = false;
            pbCabecaVivo.Visible = false;
            pictureBox8.Visible = false;
            pictureBox7.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lbDica_Click(object sender, EventArgs e)
        {

        }
    }
}
