namespace StockMonitor;
public class EmailSettings
{
    public string host = "";
    public int port = 0;
    public string email_from_address = "";
    public string email_from_user = "";
    public string email_from_password = "";
    public string[] emails_to;
    public string subject = "";
    public string body = "";
    
    public EmailSettings(string smtp_settings_file, string email_from_settings_file, string emails_to_file){
        string[] smtp_lines = File.ReadAllLines(smtp_settings_file);
        string[] email_from_lines = File.ReadAllLines(email_from_settings_file);
        emails_to = File.ReadAllLines(emails_to_file);

        for(int i = 0; i < smtp_lines[0].Length; i++)
            if(smtp_lines[0][i] == '"'){
                for(int j = i + 1; j < smtp_lines[0].Length && smtp_lines[0][j] != '"'; j++)
                    host += smtp_lines[0][j];
                break;
            }
        
        for(int i = 0; i < smtp_lines[1].Length; i++)
            if(smtp_lines[1][i] == '"'){
                for(int j = i + 1; j < smtp_lines[1].Length && smtp_lines[1][j] != '"'; j++)
                    port = 10 * port + (int)(smtp_lines[1][j] - '0');
                break;
            }

        for(int i = 0; i < email_from_lines[0].Length; i++)
            if(email_from_lines[0][i] == '"'){
                for(int j = i + 1; j < email_from_lines[0].Length && email_from_lines[0][j] != '"'; j++)
                    email_from_address += email_from_lines[0][j];
                break;
            }
        
        for(int i = 0; i < email_from_lines[1].Length; i++)
            if(email_from_lines[1][i] == '"'){
                for(int j = i + 1; j < email_from_lines[1].Length && email_from_lines[1][j] != '"'; j++)
                    email_from_user += email_from_lines[1][j];
                break;
            }

        for(int i = 0; i < email_from_lines[2].Length; i++)
            if(email_from_lines[2][i] == '"'){
                for(int j = i + 1; j < email_from_lines[2].Length && email_from_lines[2][j] != '"'; j++)
                    email_from_password += email_from_lines[2][j];
                break;
            }
    }
}