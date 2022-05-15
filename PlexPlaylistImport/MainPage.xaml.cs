using Newtonsoft.Json;
using PlexPlaylistImport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        private FormModel formModel;

        private readonly string PREVIOUS_INPUT_NAME = Path.Combine(ApplicationData.Current.LocalFolder.Path, "previousInput.json");
        private static readonly HttpClient client = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();
            //image1.Source = new SvgImageSource(new Uri("https://www.plex.tv/wp-content/themes/plex/assets/img/plex-logo.svg", UriKind.Absolute));
            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1000, 1000));
            ApplicationView.PreferredLaunchViewSize = new Size(900, 650);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            GetPreviousInputs();
        }

        private void FormIsValid(object sender, TextChangedEventArgs e)
        {
            if (Url.Text.Length > 0
                && Token.Text.Length > 0
                && SectionId.Text.Length > 0
                && PlaylistPath.Text.Length > 0)
            {
                Import.IsEnabled = true;
            }
            else
            {
                Import.IsEnabled = false;
            }
                
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            SetPreviousInputs();

            result.Text = "";
            StringBuilder path = new StringBuilder();

            // Add slash if not present in provided URL input
            if (Url.Text.Substring(Url.Text.Length - 1) == "/")
            {
                path.Append(Url.Text);
            }
            else
            {
                path.Append($"{Url.Text}/");
            }

            // Build path for playlist endpoint
            path.Append($"playlists/upload?sectionID={SectionId.Text}&path={PlaylistPath.Text}&X-Plex-Token={Token.Text}");

            System.Diagnostics.Debug.WriteLine(path.ToString(), "Path");

            try
            {
                // Send POST request
                var response = await client.PostAsync(path.ToString(), null);

                // Await response
                var responseString = await response.Content.ReadAsStringAsync();

                // Write to debugger
                System.Diagnostics.Debug.WriteLine(response, "Response");

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        result.Text = "Command successfully sent to PLEX";
                        break;
                    default:
                        result.Text = $"Unexpected result of request ({response.StatusCode})";
                        Logger.Error($"Errors occurred while processing request. {response.StatusCode}\n{response.RequestMessage.RequestUri.AbsoluteUri}");
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Text = $"Errors occurred while processing request. {ex.Message}";
                System.Diagnostics.Debug.WriteLine(ex.ToString(), "Exception");
                Logger.Error($"Errors occurred while processing request. {ex}");
            }
            
        }

        private void GetPreviousInputs()
        {
            if (File.Exists(PREVIOUS_INPUT_NAME))
            {
                try
                {
                    string fromFile = File.ReadAllText(PREVIOUS_INPUT_NAME);
                    formModel = JsonConvert.DeserializeObject<FormModel>(fromFile);

                    Url.Text = formModel.Url;
                    Token.Text = formModel.Token;
                    SectionId.Text = formModel.SectionId.ToString();
                    PlaylistPath.Text = formModel.Path;
                }
                catch(Exception ex)
                {
                    Logger.Error($"Errors occurred while getting previous inputs. {ex}");
                }
            }
            else
            {
                formModel = new FormModel();
            }
        }

        private void SetPreviousInputs()
        {
            if (Url.Text.Substring(Url.Text.Length - 1) == "/")
            {
                formModel.Url = Url.Text;
            }
            else
            {
                formModel.Url = $"{Url.Text}/";
            }
            formModel.Token = Token.Text;
            formModel.SectionId = Convert.ToByte(SectionId.Text);
            formModel.Path = PlaylistPath.Text; ;

            try
            {
                using (StreamWriter sw = new StreamWriter(PREVIOUS_INPUT_NAME,false))
                {
                    string content = JsonConvert.SerializeObject(formModel);
                    sw.Write(content);
                    sw.Close();
                }
            }
            catch(Exception ex)
            {
                Logger.Error($"Errors occurred while writing previous inputs. {ex}");
            }
        }

        private void SectionId_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Url_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
