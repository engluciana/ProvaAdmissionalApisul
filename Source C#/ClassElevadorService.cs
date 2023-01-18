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

    public IEnumerable<JsonInput> turnoM;
    public IEnumerable<JsonInput> turnoV;
    public IEnumerable<JsonInput> turnoN;

    public Dictionary<char, int> elevadores;
    public Dictionary<char, int> turnos;

    List<JsonInput> entrada;

    public float sizeInput;

    public ClassElevadorService()
    {
      elevadores = new Dictionary<char, int>();
      turnos = new Dictionary<char, int>();
    }

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

      turnoM = from input in inputs where input.turno.Equals("M") select input;
      turnoV = from input in inputs where input.turno.Equals("V") select input;
      turnoN = from input in inputs where input.turno.Equals("N") select input;

      turnos.Add('M', turnoM.Count());
      turnos.Add('V', turnoV.Count());
      turnos.Add('N', turnoN.Count());

      entrada = inputs;
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
      List<char> ret = new();
      int elevadorCount = 0;
      foreach (var item in elevadores.OrderByDescending(count => count.Value))
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          elevadorCount = item.Value;
        }
        else if (elevadorCount == item.Value)
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
    public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
    {
      List<char> elevadores = elevadorMaisFrequentado();
      List<char> ret = new();

      foreach (var item in elevadores)
      {
        List<char> aux = new();
        switch (item)
        {
          case 'A':
            aux = GetPeriodoMenorMaior(elevadorA, 1);
            break;
          case 'B':
            aux = GetPeriodoMenorMaior(elevadorB, 1);
            break;
          case 'C':
            aux = GetPeriodoMenorMaior(elevadorC, 1);
            break;
          case 'D':
            aux = GetPeriodoMenorMaior(elevadorD, 1);
            break;
          case 'E':
            aux = GetPeriodoMenorMaior(elevadorE, 1);
            break;
        }
        foreach (var i in aux)
        {
          ret.Add(i);
        }
      }
      return ret;
    }
    public List<char> elevadorMenosFrequentado()
    {
      List<char> ret = new();
      int elevadorCount = 0;
      foreach (var item in elevadores.OrderBy(count => count.Value))
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          elevadorCount = item.Value;
        }
        else if (elevadorCount == item.Value)
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
    public List<char> periodoMenorFluxoElevadorMenosFrequentado()
    {
      List<char> menosUsado = elevadorMenosFrequentado();
      List<char> ret = new();

      foreach (var item in menosUsado)
      {
        List<char> aux = new();
        switch (item)
        {
          case 'A':
            aux = GetPeriodoMenorMaior(elevadorA, 0);
            break;
          case 'B':
            aux = GetPeriodoMenorMaior(elevadorB, 0);
            break;
          case 'C':
            aux = GetPeriodoMenorMaior(elevadorC, 0);
            break;
          case 'D':
            aux = GetPeriodoMenorMaior(elevadorD, 0);
            break;
          case 'E':
            aux = GetPeriodoMenorMaior(elevadorE, 0);
            break;
        }
        foreach (var i in aux)
        {
          ret.Add(i);
        }
      }
      return ret;
    }

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

      int countTurno = 0;
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
      
      foreach (var item in periodoOrdenado)
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          countTurno = item.Value;
        }
        else if (countTurno == item.Value)
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

    public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
    {
      List<char> ret = new();

      int countTurno = 0;
      foreach (var item in turnos.OrderByDescending(count => count.Value))
      {
        if (ret.Count == 0)
        {
          ret.Add(item.Key);
          countTurno = item.Value;
        }
        else if (countTurno == item.Value)
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

    public float PercentualUso(IEnumerable<JsonInput> elevadorX)
    {          
      return (float)Math.Round(Convert.ToDouble((elevadorX.Count()/sizeInput)) * 100,2);
    }

  }
}
