using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAdmissionalCSharpApisul
{
  class ClassElevadorService : IElevadorService
  {
    public IEnumerable<JsonInput> elevadorA;
    public IEnumerable<JsonInput> elevadorB;
    public IEnumerable<JsonInput> elevadorC;
    public IEnumerable<JsonInput> elevadorD;
    public IEnumerable<JsonInput> elevadorE;    

    public Dictionary<char, int> elevadores;
    public Dictionary<char, int> turnos;

    List<JsonInput> entrada;

    public float sizeInput;

    public ClassElevadorService(List<JsonInput> inputs)
    {
      elevadores = new Dictionary<char, int>();
      turnos = new Dictionary<char, int>();
      entrada = new();
      ElevadorStatistic(inputs);
    }
    //Separa a entrada por Elevador e por turno
    public void ElevadorStatistic(List<JsonInput> inputs)
    {
      sizeInput= inputs.Count;

      elevadorA = from input in inputs where input.elevador.Equals("A") select input;
      elevadorB = from input in inputs where input.elevador.Equals("B") select input;
      elevadorC = from input in inputs where input.elevador.Equals("C") select input;
      elevadorD = from input in inputs where input.elevador.Equals("D") select input;
      elevadorE = from input in inputs where input.elevador.Equals("E") select input;

      elevadores.Add('A', elevadorA.Count());
      elevadores.Add('B', elevadorB.Count());
      elevadores.Add('C', elevadorC.Count());
      elevadores.Add('D', elevadorD.Count());
      elevadores.Add('E', elevadorE.Count());

      var turnoM = from input in inputs where input.turno.Equals("M") select input;
      var turnoV = from input in inputs where input.turno.Equals("V") select input;
      var turnoN = from input in inputs where input.turno.Equals("N") select input;

      turnos.Add('M', turnoM.Count());
      turnos.Add('V', turnoV.Count());
      turnos.Add('N', turnoN.Count());

      entrada = inputs;
    }
    //De acordo com a lista recebida, seleciona o elevador para obter o turno de maior ou menor uso
    // de acordo com a opção informada
    public List<char> SelectElevadorForGetPeriodo(List<char>FreqElevador, int opcao)
    {
      List<char> ret = new();
      foreach (var item in FreqElevador)
      {
        List<char> aux = new();
        switch (item)
        {
          case 'A':
            aux = GetPeriodoMenorMaior(elevadorA, opcao);
            break;
          case 'B':
            aux = GetPeriodoMenorMaior(elevadorB, opcao);
            break;
          case 'C':
            aux = GetPeriodoMenorMaior(elevadorC, opcao);
            break;
          case 'D':
            aux = GetPeriodoMenorMaior(elevadorD, opcao);
            break;
          case 'E':
            aux = GetPeriodoMenorMaior(elevadorE, opcao);
            break;
        }
        foreach (var i in aux)
        {
          ret.Add(i);
        }
      }
      return ret;
    }    
    // Retorna o turno mais usado ou menos usado de acordo com as opções:
    // 0 -> turno menos usado
    // 1 -> turno mais usado
    public List<char> GetPeriodoMenorMaior(IEnumerable<JsonInput> elevador, int opcao)
    {
      List<char> ret = new();
      Dictionary<char, int> periodo = new();

      var m = from item in elevador where item.turno.Equals("M") select item;
      var v = from item in elevador where item.turno.Equals("V") select item;
      var n = from item in elevador where item.turno.Equals("N") select item;

      periodo.Add('M', m.Count());
      periodo.Add('V', v.Count());
      periodo.Add('N', n.Count());

      IOrderedEnumerable<KeyValuePair<char, int>> periodoOrdenado = null;
      switch (opcao)
      {
        case 0:
          periodoOrdenado = periodo.OrderBy(count => count.Value);
          break;
        case 1:
          periodoOrdenado = periodo.OrderByDescending(count => count.Value);
          break;
        default:
          break;
      }
      return GetUsoElevador(periodoOrdenado);
    }
    //Calcula o percentual de uso de um determinado elevador
    public float PercentualUso(IEnumerable<JsonInput> elevadorX)
    {
      return (float)Math.Round(Convert.ToDouble((elevadorX.Count() / sizeInput)) * 100, 2);
    }
    //Retorna o uso ou turno de um ou mais elevadores de acordo com o parâmentro informado
    public List<char> GetUsoElevador(IOrderedEnumerable<KeyValuePair<char, int>> usoElevador)
    {
      List<char> ret = new();
      int count = 0;
      foreach (var item in usoElevador)
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          count = item.Value;
        }
        else if (count == item.Value)
        {
          ret.Add(item.Key);
        }
        else
        {
          break;
        }
      }
      return ret;
    }
    public List<int> andarMenosUtilizado()
    {
      Dictionary<int, int> andares = new();
      List<int> ret = new();

      for (int i = 0; i < 16; i++)
      {
        var andar = from input in entrada where input.andar.Equals(i) select input;
        andares.Add(i, andar.Count());
      }
      int andarCount = 0; 
      foreach (var item in andares.OrderBy(count => count.Value))      
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          andarCount = item.Value;
        }
        else if (andarCount == item.Value)
        {
          ret.Add(item.Key);
        }
        else
        {
          break;
        }
      }
      return ret;
    }
    public List<char> elevadorMaisFrequentado()
    {           
      return GetUsoElevador(elevadores.OrderByDescending(count => count.Value));     
    } 
    public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
    {          
      return SelectElevadorForGetPeriodo(elevadorMaisFrequentado(),1);
    }
    public List<char> elevadorMenosFrequentado()
    {
      return GetUsoElevador(elevadores.OrderBy(count => count.Value));      
    }
    public List<char> periodoMenorFluxoElevadorMenosFrequentado()
    {      
      return SelectElevadorForGetPeriodo(elevadorMenosFrequentado(),0);
    }   
    public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
    {
      return GetUsoElevador(turnos.OrderByDescending(count => count.Value));      
    }
    public float percentualDeUsoElevadorA()
    {
       return PercentualUso(elevadorA);
    }
    public float percentualDeUsoElevadorB()
    {
      return PercentualUso(elevadorB);
    }
    public float percentualDeUsoElevadorC()
    {
      return PercentualUso(elevadorC);
    }
    public float percentualDeUsoElevadorD()
    {
      return PercentualUso(elevadorD);
    }
    public float percentualDeUsoElevadorE()
    {
      return PercentualUso(elevadorE);
    }
  }
}
