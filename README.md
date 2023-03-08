# StockMonitor
Bot to automatically notify, via email, changes in the market price of certain stocks on Bovespa.

Currently it is able to check the prices every 20 minutes.

It is coded in C# and uses the Brapi API to get the Bovespa information.

You should have the latest version of .NET Core installed.

# Setting the email address
You should have 3 .txt files in the same folder as the main program:

1. smtp_settings.txt, which should contain the access settings for the SMTP server as follow:

--------------------

smtp host = "{host}"

smtp port = "{port}"

--------------------


  Change {host} and {port} to your settings, I'm going to use the default {host} = smtp.gmail.com and {port} = 587

2. email_from_settings.txt, which should contain the access settings for the email which will send the messages.

The file should follow the template:

-------------------------------------

mail address = "{email}"

mail user = "{user}"

mail app password = "{app password}"

--------------------------------------

Change {email}, {user} and {app password} to your settings. Usually {email} = {user}@gmail.com or something similar.

Attention for the {app password}. It is NOT the email password, it is a password specific for applications.

You can get your {app password} going to the email settings > security, problably after completing the 2-step verification.

3. emails_to.txt, which should contain all the emails that would like to be notified, 1 per line.

# Running the program
This project in C# is a console application, a.k.a. you run it in the Command Prompt (if you are using Windows).

Go to the project folder in the Command Prompt and run the line

----------------------------------

dotnet run -- {Stock} {Sell} {Buy}

----------------------------------

Where:

{Stock} -> Ticker symbol of the stock to be monitored. This is NOT the company name, this is an official abreviation to identify the stock.

{Sell} -> Stock price that, if hit, an email should be sent warning it (problably because you would want to sell).

{Buy} -> Stock price that, if hit, an email should be sent warning it (problably because you would want to buy, so, usually, {Sell} > {Buy}).

{Sell} and {Buy} should have the decimal places separated by comma, NOT dot.

# Warnings
You might want to change some values on the Date.cs file. The variables bovespa_open_hour and bovespa_close_hour are the times of opening and closure of bovespa, int minutes past the midnight (10:00 AM correspond to the value 10 * 60).

"sexta-feira", "sábado" and "domingo" correspond to friday, saturday and sunday. It should have the exact format as your computer gives the week days.
