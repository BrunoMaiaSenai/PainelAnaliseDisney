using System;

namespace PainelAnaliseDisney.Models
{
    public class Filme
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Films { get; set; }
        public string? ShortFilms { get; set; }
        public string? TvShows { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }

        // Data/hora em que o registro foi salvo no Firebase
        public string Timestamp { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }
}
