using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ClienteAPI.Models;

namespace ClienteAPI.Services
{
    public class ComprasService
    {
        private static HttpClient setupClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://10.0.2.2:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<HttpResponseMessage> ComprarProduto(Compra compra)
        {
            HttpClient client = setupClient();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.PostAsJsonAsync("api/compras", compra);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha em ComprarProduto()");
            }
            return response;
        }
    }
}
