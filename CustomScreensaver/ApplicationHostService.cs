using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CustomScreensaver
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private MainWindow? _navigationWindow;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await HandleActivationAsync();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Creates main window during activation.
        /// </summary>
        private async Task HandleActivationAsync()
        {
            try
            {
                await Task.CompletedTask;

                if (!App.Current.Windows.OfType<Container>().Any())
                {
                    _navigationWindow = (_serviceProvider.GetService(typeof(MainWindow)) as MainWindow)!;
                    _navigationWindow!.Show();
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
            }
        }

        private void CheckApp()
        {
            //var authentication = _serviceProvider.GetService(typeof(AuthenticationViewModel)) as AuthenticationViewModel;
            //var settings = _serviceProvider.GetService(typeof(SettingsViewModel)) as SettingsViewModel;

            //if (authentication.IsAuthenticated == false)
            //{
            //    _navigationWindow.Navigate(typeof(Views.Pages.AuthenticationPage));
            //}
            //else
            //{
            //    if (settings.IsSettedUp != true)
            //    {
            //        _navigationWindow.Navigate(typeof(Views.Pages.SettingsPage));
            //    }
            //    else
            //    {
            //        _navigationWindow.Navigate(typeof(Views.Pages.ConversationOpenedPage));
            //    }
            //}
        }
    }
}
