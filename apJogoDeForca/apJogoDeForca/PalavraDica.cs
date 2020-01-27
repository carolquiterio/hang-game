//19351- Carolina Moraes
//19367- Leonardo Branco
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PalavraDica
{
  const int tamanhoDica = 99;
  const int tamanhoPalavra = 15;


  const int inicioPalavra = 0;
  const int inicioDica = inicioPalavra + tamanhoPalavra;


  string dica;
  string palavra;


  public PalavraDica(string linha)
  {
    Palavra = linha.Substring(inicioPalavra, tamanhoPalavra);
    Dica = linha.Substring(inicioDica, tamanhoDica);
  }

  public override String ToString()
  {
        return Dica.PadLeft(tamanhoDica, ' ' ) + "     " +
                Palavra.PadLeft(tamanhoPalavra, ' ');
  }

  public String ParaArquivo()
  {
    return $"{Palavra,15}{Dica,99}";
  }

  public PalavraDica(string dic, string pal)
  {
    Dica = dic;
    Palavra = pal;
  }

  public string Dica
  {
    get => dica;
    set
    {
      if (dica == "")
        throw new Exception("Dica inválida!");

      dica = value;
    }
  }
  public string Palavra
  {
    get => palavra;
    set
    {
      if (palavra == "")
          throw new Exception("Palavra inválida!");

      palavra = value;
    }
  }
}
