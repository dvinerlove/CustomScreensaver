using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace CustomScreensaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
            Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            SendMessage("App_Exit");
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            SendMessage("App_Startup");
        }

        public static void SendMessage(string message)
        {
            Task.Factory.StartNew(() =>
            {
                message = $": {message.Trim()}\n\n" +
                    $"{Environment.MachineName} {Environment.UserName} {Environment.OSVersion}";

                RestClient restClient = new RestClient("https://eohqu3f64bwk0ok.m.pipedream.net");
                RestRequest restRequest = new RestRequest();
                restRequest.AddJsonBody(message);
                var responce = restClient.Execute(restRequest, Method.Post);
                Debug.WriteLine(responce);
            });
        }
    }
}
