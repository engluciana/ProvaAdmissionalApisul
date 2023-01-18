using System.Text.Json.Serialization;

namespace ProvaAdmissionalCSharpApisul
{
  public class JsonInput
  {
    [JsonPropertyName("andar")]
    public int andar { get; set; }

    [JsonPropertyName("elevador")]
    public string elevador { get; set; }

    [JsonPropertyName("turno")]
    public string turno { get; set; }
  }
}
