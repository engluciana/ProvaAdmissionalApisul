using System.Text.Json.Serialization;
using System.Text.Json;

namespace ProvaAdmissionalCSharpApisul
{
  public class Program
  {
    static void Main(string[] args)
    {
      ReadInputFile readInput = new ReadInputFile();
      readInput.ReadFile();
      if (readInput.file)
      {
        List<JsonInput> jsonsInput = JsonSerializer.Deserialize<List<JsonInput>>(readInput.text);
                
        ClassElevadorService elevadorService = new ClassElevadorService();
        elevadorService.ElevadorStatistic(jsonsInput);

        Console.WriteLine("Elevador mais usado:");
        foreach (var item in elevadorService.elevadorMaisFrequentado())
        {
          Console.WriteLine(item);
        }            
        Console.WriteLine("Elevador menos usado: ");
        foreach (var item in elevadorService.elevadorMenosFrequentado())
        {
          Console.WriteLine(item);
        }
        Console.WriteLine("Período de Menor Fluxo do Elevador Menos Frequentado: ");        
        foreach (var i in elevadorService.periodoMenorFluxoElevadorMenosFrequentado())
        {
          Console.WriteLine(i);
        }
        Console.WriteLine("Período de Maior Fluxo do Elevador mais Frequentado: ");       
        foreach (var i in elevadorService.periodoMaiorFluxoElevadorMaisFrequentado())
        {
          Console.WriteLine(i);
        }
        Console.WriteLine("Período de Maior Utilização do Conjunto de Elevadores: ");
        foreach (var i in elevadorService.periodoMaiorUtilizacaoConjuntoElevadores())
        {
          Console.WriteLine(i);
        }

        Console.WriteLine("Percentual de uso do Elevador A: " + elevadorService.percentualDeUsoElevadorA() +"%");
        Console.WriteLine("Percentual de uso do Elevador B: " + elevadorService.percentualDeUsoElevadorB() +"%");
        Console.WriteLine("Percentual de uso do Elevador C: " + elevadorService.percentualDeUsoElevadorC() +"%");
        Console.WriteLine("Percentual de uso do Elevador D: " + elevadorService.percentualDeUsoElevadorD() +"%");
        Console.WriteLine("Percentual de uso do Elevador E: " + elevadorService.percentualDeUsoElevadorE() +"%");

        Console.WriteLine("Andar mesnos utilizado pelos usuários: ");
        foreach(var i in elevadorService.andarMenosUtilizado())
        {
          Console.WriteLine(i);
        }
      }     
    }
  }
}