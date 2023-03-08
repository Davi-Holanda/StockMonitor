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
            bool bolsa_fechada = false;

            while(hora == ultimo_update){
                if(bolsa_fechada){
                    Console.WriteLine("Esperando abertura da bolsa...");
                    Console.WriteLine("Caso esteja aberta, ocorreu um erro.\n");
                    System.Threading.Thread.Sleep(30 * 60 * 1000);
                }
                else if(tentativas_sem_update >= 20){
                    Console.WriteLine("1 hora sem atualização.");
                    Console.WriteLine("Caso a bolsa tenha fechado, o funcionamento volta ao normal em 15 horas.");
                    Console.WriteLine("Caso contrário, ocorreu um erro.\n");
                    bolsa_fechada = true;
                    System.Threading.Thread.Sleep(14 * 60 * 60 * 1000);
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
            Console.WriteLine($"Valor:  {price:N2}\n");
        }
    }
}