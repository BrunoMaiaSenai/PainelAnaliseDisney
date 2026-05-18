using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PainelAnaliseDisney.Models
{
    public class Filme
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        // Mapeia o campo "name" do JSON para a sua propriedade C#
        [JsonPropertyName("name")]
        public string Nome { get; set; } = string.Empty;

        // Mapeia o campo "movie" do JSON
        [JsonPropertyName("movie")]
        public string NomeFilme { get; set; } = string.Empty;

        // Mapeia o campo "imageUrl" do JSON para a sua propriedade de Imagem
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("films")]
        public List<string> Films { get; set; } = new List<string>();

        [JsonPropertyName("shortFilms")]
        public List<string> ShortFilms { get; set; } = new List<string>();

        [JsonPropertyName("tvShows")]
        public List<string> TvShows { get; set; } = new List<string>();
    }
}
