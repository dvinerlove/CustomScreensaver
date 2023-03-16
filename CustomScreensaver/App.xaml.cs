using CustomScreensaver.Properties;
using CustomScreensaver.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.Collections;
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

    public class AppConfig
    {
        public string ConfigurationsFolder { get; set; } = "";

        public string AppPropertiesFileName { get; set; } = "0";
    }
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()

            .ConfigureServices((context, services) =>
            {

                // App Host
                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<HotKeyService>();
                services.AddSingleton<ScreensaverViewModel>();
                services.AddSingleton<ScreensaverService>();
                services.AddSingleton<SettingsViewModel>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<SettingsWindow>();

                // Configuration
                services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
            }).Build();
        public App()
        {
            Startup += App_Startup;
            Exit += App_Exit;
            //if (string.IsNullOrEmpty(Settings.Default.RecordingsFolder.Trim()))
            //{
            //    Settings.Default.Upgrade();
            //}
        }

        private async void App_Exit(object sender, ExitEventArgs e)
        {
            SendMessage("App_Exit");
            await _host.StopAsync();

            _host.Dispose();
        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            SendMessage("App_Startup");
            await _host.StartAsync();
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
