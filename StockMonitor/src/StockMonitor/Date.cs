namespace StockMonitor;

public class Date
{
    string week_day = "";
    int time = 0;
    int bovespa_open_hour = 10 * 60;
    int bovespa_close_hour = 18 * 60;    

    public Date(){
        string date = DateTime.Today.ToString("D");
        week_day = "";
        time = 0;

        int i;
        for(i = 0; date[i] != ','; i++)
            week_day += date[i];

        date = DateTime.Now.ToString();
        time = 60 * (10 * (int)(date[11] - '0') + (int)(date[12] - '0')) + 10 * (int)(date[14] - '0') + (int)(date[15] - '0');
    }
    public static bool BovespaIsClosed(){
        Date date = new Date();

        if(date.week_day == "sábado" || date.week_day == "domingo")
            return true;
        
        if(date.time < date.bovespa_open_hour || date.time > date.bovespa_close_hour)
            return true;
        
        return false;
    }
    public static int NextBovespaOpening(){
        Date date = new Date();

        if(date.time <= date.bovespa_open_hour && date.week_day != "sábado" && date.week_day != "domingo")
            return (date.bovespa_open_hour - date.time + 1) * 60000;

        else if(date.week_day == "sexta-feira")
            return (date.bovespa_open_hour + 3 * 24 * 60 - date.time + 1) * 60000;

        else if(date.week_day == "sábado")
            return (date.bovespa_open_hour + 2 * 24 * 60 - date.time + 1) * 60000;

        else
            return (date.bovespa_open_hour + 1 * 24 * 60 - date.time + 1) * 60000;
    }
}
