using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;

namespace MilleniumEye
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            var ts = new ThreadStart(BackgroundScreenshot);
            var backgroundThread = new Thread(ts);
            backgroundThread.Start();
            InitializeComponent();
        }

        private static void BackgroundScreenshot()
        {
            var doro = new Doro();
            var process = Process.GetProcessesByName("devenv")[0];
            while (true)
            {
                doro.CaptureProcessWindow(process);
                Thread.Sleep(50);
            }
        }
    }
}