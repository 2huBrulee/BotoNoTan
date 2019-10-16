using System;
using System.Diagnostics;
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
            var doro = new Doro();
            doro.CaptureProcessWindow(Process.GetProcessesByName("dlpc")[0]);
            InitializeComponent();
        }
    }
}