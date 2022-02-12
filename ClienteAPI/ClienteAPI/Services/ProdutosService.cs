using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using ClienteAPI.Models;

namespace ClienteAPI.Services
{
    public class ProdutosService
    {
        private static List<MediaTypeFormatter> formatters = 
            new List<MediaTypeFormatter>(){ new JsonMediaTypeFormatter() };

        private static HttpClient setupClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://10.0.2.2:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<List<Produto>> ListarProdutos()
        {
            HttpClient client = setupClient();
            HttpResponseMessage response;// = await client.GetAsync("api/produtos");
            try
            {
                response = await client.GetAsync("api/produtos");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<Produto>>(formatters);
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha em ListarProdutos");
                return new List<Produto>();
            }
        }

        public static async Task<Produto> DetalharProduto(int Id_Produto)
        {
            HttpClient client = setupClient();
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"api/produtos/{Id_Produto}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Produto>(formatters);
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha em DetalharProduto");
                return new Produto();
            }
        }

        public static async Task RegistrarProduto(Produto produto)
        {
            HttpClient client = setupClient();
            HttpResponseMessage response;
            try
            {
                response = await client.PostAsJsonAsync("api/produtos", produto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha em RegistrarProduto");
            }
        }

        public static async Task ExcluirProduto(int Id_Produto)
        {
            HttpClient client = setupClient();
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync($"api/produtos/{Id_Produto}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha em ExcluirProduto");
            }
        }
    }
}
