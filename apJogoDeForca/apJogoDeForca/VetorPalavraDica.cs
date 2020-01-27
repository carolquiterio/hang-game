//19351- Carolina Moraes
//19367- Leonardo Branco
using System;
using System.IO;
using System.Windows.Forms;

public enum Situacao
         { navegando, incluindo, editando, procurando, excluindo }
class VetorPalavraDica
{
  PalavraDica[] dados = null; // dados é um vetor vazio no momento
  int qtosDados;              // controla a quantidade de posições em uso
  int posicaoAtual;           // indica a posição do registro visto na tela
  Situacao situacaoAtual;     // informa o que o programa está fazendo

  public PalavraDica this[int qualPosicao]
  {
    get
    {
      if (qualPosicao >= 0 && qualPosicao < qtosDados)
         return dados[qualPosicao];
      throw new Exception("Acesso a posição inválida: "+
                          qualPosicao+"!");
    }
    set
    {
      if (qualPosicao >= 0 && qualPosicao < qtosDados)
         dados[qualPosicao] = value;
      else
        throw new Exception("Acesso a posição inválida: " +
                            qualPosicao + "!");
    }
  }
  public int Tamanho // permite à aplicação consultar o número de registros armazenados
  {
    get => qtosDados;
  }
  public bool EstaVazio // permite à aplicação saber se o vetor dados está vazio
  {
    get => qtosDados <= 0; // se qtosDados <= 0, retorna true
  }
  public VetorPalavraDica(int tamanhoInicial)
  {
    dados = new PalavraDica[tamanhoInicial];
    qtosDados = 0;
    posicaoAtual = -1;
    situacaoAtual = Situacao.navegando;
  }
  public int PosicaoAtual
  {
    get => posicaoAtual;
    set => posicaoAtual = value;
  }
  public Situacao SituacaoAtual
  {
    get => situacaoAtual;
    set => situacaoAtual = value;
  }

  public void PosicionarNoInicio()
  {
    posicaoAtual = 0;
  }

  public void AvancarPosicao()
  {
    if (posicaoAtual < qtosDados-1)
       posicaoAtual++;
  }

  public void RetrocederPosicao()
  {
    if (posicaoAtual > 0)
      posicaoAtual--;
  }

  public void PosicionarNoUltimo()
  {
    posicaoAtual = qtosDados - 1;
  }
  public void LerDados(string nomeArquivo)
  {
    var arquivo = new StreamReader(nomeArquivo);
    while (!arquivo.EndOfStream)
    {
      string linhaLida = arquivo.ReadLine();
      var novoDicaPal = new PalavraDica(linhaLida);
      IncluirAposFim(novoDicaPal);
    }
    arquivo.Close();
  }
  public void IncluirAposFim(PalavraDica novoValor)
  {
    if (qtosDados >= dados.Length)
      Expandir();

    dados[qtosDados] = novoValor;
    qtosDados++;
  }

  public void Incluir(PalavraDica novoValor, int posicaoDeInclusao)
  {
    if (qtosDados >= dados.Length)
       Expandir();

    for (int indice = qtosDados - 1; indice >= posicaoDeInclusao;
         indice--)
        dados[indice + 1] = dados[indice];
    dados[posicaoDeInclusao] = novoValor;
    qtosDados++;
  }
  void Expandir()
  {
    PalavraDica[] vetorMaior = new PalavraDica[dados.Length + 10];
    for (int indice = 0; indice < dados.Length; indice++)
      vetorMaior[indice] = dados[indice];
    dados = vetorMaior;
  }

  public void Excluir(int posicaoASerExcluida)
  {
    qtosDados--;
    for (int ind = posicaoASerExcluida; ind < qtosDados; ind++)
      dados[ind] = dados[ind + 1];
  }

  public bool Existe(PalavraDica palavraProc, ref int meio)  // pesquisa binária
  {                                                          //Não possível usar pesquisa binária, pois o vetor nao esta ordenado
      int inicio = 0;
      int fim = qtosDados - 1;
      bool achou = false;
      while (!achou && inicio <= fim)
      {
          meio = (inicio + fim) / 2;
          if (dados[meio].Palavra == palavraProc.Palavra)  // achou procurado
              achou = true;
          else
            if (palavraProc.Palavra.CompareTo(dados[meio].Palavra) < 0)
              fim = meio - 1;
          else
              inicio = meio + 1;
      }
      if (!achou)       // não existe a palavara procurada
          meio = inicio; // posição onde deveria ser incluido o valor
   
      return achou;
  }
  public bool ExisteSequencial(PalavraDica palDicaProc, ref int indice)
  {
    bool achouIgual = false;
    indice = 0; // para começar a percorrer o vetor dados
    while (!achouIgual && indice < qtosDados)
      if (dados[indice].Palavra == palDicaProc.Palavra)
        achouIgual = true;
      else
        indice++;

    return achouIgual;
  }
  public void Listar(ListBox lista)
  {
    lista.Items.Clear();
    for (int indice = 0; indice < qtosDados; indice++)
      lista.Items.Add($"[{indice,2}] - {dados[indice]}");
  }

  public void Listar(TextBox lista, string cabecalho)
  {
    lista.Clear();
    lista.AppendText(cabecalho + Environment.NewLine);
    for (int indice = 0; indice < qtosDados; indice++)
      lista.AppendText($"{dados[indice]}"+Environment.NewLine);
  }
  public void GravacaoEmDisco(string nomeArquivo)
  {
    var arqPalavraDica = new StreamWriter(nomeArquivo);
    for (int i = 0; i < qtosDados; i++)
        arqPalavraDica.WriteLine(dados[i].ParaArquivo());
    arqPalavraDica.Close();
  }
  public void Ordenar()
  {
      for (int lento = 0; lento < qtosDados; lento++)
          for (int rapido = lento + 1; rapido < qtosDados; rapido++)
              if (dados[rapido].Palavra.CompareTo(dados[lento].Palavra) < 0)
              {
                  PalavraDica aux = dados[rapido];
                  dados[rapido] = dados[lento];
                  dados[lento] = aux;
              }
  }
}
