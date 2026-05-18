using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PainelAnaliseDisney.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("mensagem")]
        public string Mensagem { get; set; } = string.Empty;

        // Alterado de "data" para "dados" para bater com o servidor real:
        [JsonPropertyName("dados")]
        public List<Filme> Data { get; set; } = new List<Filme>();
    }
}
