using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
