using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PainelAnaliseDisney.Models;
using PainelAnaliseDisney.Data;

namespace PainelAnaliseDisney.Services
{
    public class FilmeApiService
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
                // 1. Baixa o JSON bruto como string para evitar falhas de rede ocultas
                string jsonRaw = await _httpClient.GetStringAsync(Database.ApiUrl);

                // 2. Configura o conversor para tolerar maiúsculas/minúsculas
                var opcoes = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // 3. Converte o JSON bruto para a nossa estrutura ApiResponse
                var respostaRaiz = JsonSerializer.Deserialize<ApiResponse>(jsonRaw, opcoes);

                if (respostaRaiz != null && respostaRaiz.Data != null)
                {
                    return respostaRaiz.Data;
                }

                return new List<Filme>();
            }
            catch (Exception ex)
            {
                // Esse log vai imprimir no Console de Saída o motivo exato caso falte alguma propriedade
                Console.WriteLine($"Erro crítico na desserialização: {ex.Message}");
                return new List<Filme>();
            }
        }
    }
}