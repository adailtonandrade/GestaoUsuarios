using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Presentation.Models;
using System.Net.Http.Json;

namespace GestaoUsuarios.Presentation.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly HttpClient _httpClient;

        public ClienteRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<IEnumerable<ClienteResponse>> ObterClientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ClienteResponse[]>("api/clientes") ?? Array.Empty<ClienteResponse>();
        }

        public async Task<bool> DesativarClienteByIdAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"api/clientes/{id}");
            return resposta.IsSuccessStatusCode;
        }

        public async Task<ClienteModel> ObterClientePorIdAsync(int id)
        {
            var cliente = await _httpClient.GetFromJsonAsync<ClienteModel>($"api/clientes/{id}");
            if (cliente != null)
                return cliente;
            return null;
        }

        public async Task<bool> CadastrarClienteAsync(ClienteModel cliente)
        {
            var resposta = await _httpClient.PostAsJsonAsync("api/clientes", cliente);
            if (resposta.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> AtualizarClienteAsync(ClienteModel cliente)
        {
            var resposta = await _httpClient.PutAsJsonAsync("api/clientes", cliente);
            if (resposta.IsSuccessStatusCode)
                return true;
            return false;
        }


        //public async Task<IEnumerable<ClienteResponse>> GetReviewSummariesAsync()
        //{
        //    return await _httpClient.GetFromJsonAsync<BookReview[]>("api/BookReviews/Summary") ?? Array.Empty<BookReview>();
        //}
    }
}
