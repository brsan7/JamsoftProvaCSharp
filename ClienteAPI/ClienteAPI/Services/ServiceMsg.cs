using System.Net.Http;


namespace ClienteAPI.Services
{
    public class ServiceMsg
    {
        public static string FormatarResposta(HttpResponseMessage resposta)
        {
            int status_code = ((int)resposta.StatusCode);
            string mensagem;
            switch (status_code)
            {
                case 200:
                    mensagem = "Operação Realizada com sucesso";
                    break;
                case 400:
                    mensagem = "Ocorreu um erro desconhecido";
                    break;
                case 412:
                    mensagem = "Os valores informados não são válidos";
                    break;
                default:
                    mensagem = "Ocorreu um erro desconhecido";
                    break;
            }
            return mensagem;
        }
    }
}
