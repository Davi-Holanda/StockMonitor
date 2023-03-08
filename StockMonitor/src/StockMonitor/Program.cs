namespace StockMonitor;
public class Program
{
    static void Main(string[] args){
        if(args.Length != 3){
            Console.WriteLine("Erro");
            Console.WriteLine("Chame o programa com 'dotent run -- {ativo} {valor_de_venda} {valor_de_compra}'");
        }
        else{
            EmailSettings settings = new EmailSettings("smtp_settings.txt", "email_from_settings.txt", "emails_to.txt");
        
            string ativo = args[0];
            double valor_de_venda = Convert.ToDouble(args[1]);
            double valor_de_compra = Convert.ToDouble(args[2]);

            Market market = new Market(ativo);
            
            bool enabled = true;
            while(enabled){   
                Console.WriteLine("Aguardando dados novos...");
                market.UpdateData();
                
                if(market.price >= valor_de_venda){
                    settings.subject = "Venda " + ativo;
                    settings.body = "Valor de " + ativo + $" em  {market.price:N2}  BRL";
                    SendEmail.Send(settings);
                }

                if(market.price <= valor_de_compra){
                    settings.subject = "Compre " + ativo;
                    settings.body = "Valor de " + ativo + $" em  {market.price:N2}  BRL";
                    SendEmail.Send(settings);
                }
                System.Threading.Thread.Sleep(900000);
            }
        }
    }
}
