using System.Collections.Generic;
using System.Threading.Tasks;
using PainelAnaliseDisney.Models;
using PainelAnaliseDisney.Services;

namespace PainelAnaliseDisney.Respositories
{
    internal class FilmeRepository
    {
        private readonly FilmeApiService _apiService;
        private readonly LogFilme _logFilme;

        public FilmeRepository()
        {
            _apiService = new FilmeApiService();
            _logFilme = new LogFilme();
        }

        public async Task<List<Filme>> ObterEGravarFilmesAsync()
        {
            // 1. Busca os dados na API externa
            var filmes = await _apiService.BuscarFilmesDaApiAsync();

            // 2. Grava de forma assíncrona o log de cada registro capturado no Firebase
            foreach (var filme in filmes)
            {
                await _logFilme.SalvarNoFirebaseAsync(filme);
            }

            return filmes;
        }
    }
}