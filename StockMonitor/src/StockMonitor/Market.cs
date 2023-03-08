using System.Net;
using System.Text.Json;

namespace StockMonitor;
public class Market
{
    string ativo;
    Uri queryUri;
    string ultimo_update = "";
    public double price = 0;
    public Market(string _ativo){
        ativo = _ativo;

        string QUERY_URL = "https://brapi.dev/api/quote/" + ativo + "?range=1d&interval=1d&fundamental=true&dividends=true";
        queryUri = new Uri(QUERY_URL);
    }

    public void UpdateData(){
        using (WebClient client = new WebClient()){
            string hora = ultimo_update;
            int tentativas_sem_update = 0;
            string data = "";

            while(hora == ultimo_update){
                if(Date.BovespaIsClosed()){
                    Console.WriteLine("A bolsa esta fechada");
                    Console.WriteLine("Aguardando abertura...\n");
                    tentativas_sem_update = 0;
                    System.Threading.Thread.Sleep(Date.NextBovespaOpening());
                }
                else if(tentativas_sem_update >= 50){
                    Console.WriteLine("Erro");
                    Console.WriteLine("2 hora sem atualização.");
                    Console.WriteLine("Desativando programa ate a proxima abertura da bolsa.\n");
                    tentativas_sem_update = 0;
                    System.Threading.Thread.Sleep(Date.NextBovespaOpening());
                }
                else if(tentativas_sem_update > 0){
                    System.Threading.Thread.Sleep(120000);
                }

                Dictionary<string, dynamic> bovespa_json = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                Dictionary<string, dynamic> market_infos = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(bovespa_json["results"][0]);
                
                string update_time = bovespa_json["requestedAt"].GetString();
                price = market_infos["regularMarketPrice"].GetDouble();
                data = update_time.Substring(8, 2) + "/" + update_time.Substring(5, 2);
                hora = update_time.Substring(11, 5);
                
                tentativas_sem_update++;
            }
            ultimo_update = hora;

            Console.WriteLine("Update mais recente: " + data + " " + hora + " UTC");
            Console.WriteLine($"Valor:  {price:N2}  BRL\n");
        }
    }
}
