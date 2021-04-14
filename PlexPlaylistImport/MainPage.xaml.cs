using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly HttpClient client = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
            //image1.Source = new SvgImageSource(new Uri("https://www.plex.tv/wp-content/themes/plex/assets/img/plex-logo.svg", UriKind.Absolute));
            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1000, 1000));
            ApplicationView.PreferredLaunchViewSize = new Size(900, 650);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void SectionHelp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PlexTokenHelp_Click(object sender, RoutedEventArgs e)
        {
        }

        private bool FormIsValid()
        {
            return true;
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder path = new StringBuilder();
            if (Url.Text.Substring(Url.Text.Length - 1) == "/")
            {
                path.Append(Url.Text);
            }
            else
            {
                path.Append($"{Url.Text}/");
            }

            path.Append($"playlists/upload?sectionID={SectionId.Text}&path={PlaylistPath.Text}&X-Plex-Token={Token.Text}");

            System.Diagnostics.Debug.WriteLine(path.ToString(), "Path");

            try
            {
                // Send POST request
                var response = await client.PostAsync(path.ToString(), null);

                // Await response
                var responseString = await response.Content.ReadAsStringAsync();

                // Write to debugger
                System.Diagnostics.Debug.WriteLine(responseString, "Response");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString(), "Exception");
            }
            
        }
    }
}
