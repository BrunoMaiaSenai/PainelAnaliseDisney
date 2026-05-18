using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PainelAnaliseDisney.Models;
using PainelAnaliseDisney.Data;

namespace PainelAnaliseDisney.Services
{
    internal class LogFilme
    {
        private readonly HttpClient _httpClient;

        public LogFilme()
        {
            _httpClient = new HttpClient();
        }

        public async Task SalvarNoFirebaseAsync(Filme filme)
        {
            try
            {
                // Envia uma requisição POST salvando o filme no histórico do Firebase Realtime DB
                await _httpClient.PostAsJsonAsync(Database.FirebaseUrl, filme);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar Log no Firebase: {ex.Message}");
            }
        }
    }
}
