using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PainelAnaliseDisney.Models;
using PainelAnaliseDisney.Data;

namespace PainelAnaliseDisney.Services
{
    internal class FilmeApiService
    {
        private readonly HttpClient _httpClient;

        public FilmeApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Filme>> BuscarFilmesDaApiAsync()
        {
            try
            {
                // Consome a lista de filmes/personagens do endpoint configurado
                var resposta = await _httpClient.GetFromJsonAsync<List<Filme>>(Database.ApiUrl);
                return resposta ?? new List<Filme>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao consumir API: {ex.Message}");
                return new List<Filme>();
            }
        }
    }
}