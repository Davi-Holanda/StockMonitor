using System.Net;
using System.Text.Json;

namespace StockMonitor;
public class Program
{
    static void Main(string[] args){
        if(args.Length != 3){
            Console.WriteLine("Erro");
            Console.WriteLine("Chame o programa com 'dotent run -- ação valor_de_venda valor_de_compra'");
        }
        else{
            EmailSettings settings = new EmailSettings("smtp_settings.txt", "email_from_settings.txt", "emails_to.txt");
        
            string inc = args[0];
            double val_venda = Convert.ToDouble(args[1]);
            double val_compra = Convert.ToDouble(args[2]);
            string QUERY_URL = "https://brapi.dev/api/quote/" + inc + "?range=1d&interval=1d&fundamental=true&dividends=true";
            Uri queryUri = new Uri(QUERY_URL);
            bool enabled = true;

            while(enabled){
                using (WebClient client = new WebClient()){
                    Dictionary<string, dynamic> json = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                    Dictionary<string, dynamic> market_infos = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(json["results"][0]);
                    string update_time = json["requestedAt"].GetString();
                    double market_price = market_infos["regularMarketPrice"].GetDouble();

                    Console.WriteLine($"{market_price:N2}");
                    Console.WriteLine(update_time);

                    if(market_price >= val_venda){
                        settings.subject = "Venda" + inc;
                        settings.body = "Atualização " + update_time + "\n" 
                                        + "Valor de " + inc + $" em  {market_price:N2} BRL";
                        SendEmail.Send(settings);
                    }

                    if(market_price <= val_compra){
                        settings.subject = "Compre" + inc;
                        settings.body = "Atualização " + update_time + "\n" 
                                        + "Valor de " + inc + $" em  {market_price:N2} BRL";
                        SendEmail.Send(settings);
                    }
                    System.Threading.Thread.Sleep(1200000);
                }
            }
        }
    }
}