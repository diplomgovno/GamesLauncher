using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GamesLauncher.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            DPDownloadSmallFountain.Visibility = Visibility.Visible;
            DPDownloadPuzzleInABox.Visibility = Visibility.Visible;
            DPPlaySmallFountain.Visibility = Visibility.Collapsed;
            DPPlayPuzzleInABox.Visibility = Visibility.Collapsed;
            BTNDownloadSmallFountain.Click += (s, e) =>
            {
                Download("https://github.com/sha-255/SmallFountain/releases/download/Release/SmallFountainSetup.exe");
                DPDownloadSmallFountain.Visibility = Visibility.Collapsed;
                DPPlaySmallFountain.Visibility = Visibility.Visible;
            };
            BTNDownloadPuzzleInABox.Click += (s, e) =>
            {
                Download("https://github.com/sha-255/PuzzleInABox/raw/main/Puzzle%20in%20a%20box%20setup.exe");
                DPDownloadPuzzleInABox.Visibility = Visibility.Collapsed;
                DPPlayPuzzleInABox.Visibility = Visibility.Visible;
            };
            BTNPlaySmallFountain.Click += (s, e) =>
            {
                Run(@"\Small Fountain\Small Fountain.exe");
            };
            BTNPlayPuzzleInABox.Click += (s, e) =>
            {
                Run(@"\Game in a box\Game in a box.exe");
            };
            LGH.MouseDown += (s, e) => OpenLink("https://github.com/seniusz");
            LNews.MouseDown += (s, e) => OpenLink("https://ds302-omsk-r52.gosweb.gosuslugi.ru/");
            LAbout.MouseDown += (s, e) => OpenLink("https://kuipt.ru/");
        }

        private void Run(string gameName)
        {
            CreateInstallersFolderIfExists();
            string path = ProgramFilesx86() + gameName;
            var process = Process.Start(path);
            var tmp = Process.GetProcessById(process.Id);
            tmp.WaitForExit();
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void Download(string link)
        {
            var uri = new Uri(link);
            CreateInstallersFolderIfExists();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Installers\" + Path.GetFileName(uri.LocalPath);
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFileAsync(uri, path);
            //}
            var process = Process.Start(path);
            var tmp = Process.GetProcessById(process.Id);
            tmp.WaitForExit();
            MessageBox.Show("Игра успешно установленна", "Игровой лаунчер");
        }

        private void CreateInstallersFolderIfExists()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Installers"))
                Directory.CreateDirectory("Installers");
        }

        private void OpenLink(string link)
        {
            var destinationurl = link;
            var sInfo = new ProcessStartInfo(destinationurl)
            {
                UseShellExecute = true,
            };
            Process.Start(sInfo);
        }
    }
}
