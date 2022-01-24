using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using API_GerenciamentoProdutos.Entidades;

namespace API_GerenciamentoProdutos.Servicos
{
    public class PagamentoService
    {
        public static async Task<Pagamento> ValidarPagamento(Pagamento pagamento)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7070/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                                                        "api/pagamentos", pagamento);
            response.EnsureSuccessStatusCode();

            var formatters = new List<MediaTypeFormatter>() {
                                                        new JsonMediaTypeFormatter(),
                                                        new XmlMediaTypeFormatter() };

            return await response.Content.ReadAsAsync<Pagamento>(formatters);
        }
    }
}
