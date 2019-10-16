using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MilleniumEye
{
    public class Doro
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public void CaptureProcessWindow(Process process)
        {
            try
            {
                //var p = Process.GetProcessesByName("dlpc")[0];
                IntPtr h = process.MainWindowHandle;
                SetForegroundWindow(h);
                ShowWindow(h, 9);
                Rect rect = new Rect();
                IntPtr error = GetWindowRect(process.MainWindowHandle, ref rect);
                while (error == (IntPtr)0)
                {
                    error = GetWindowRect(process.MainWindowHandle, ref rect);
                }
                Thread.Sleep(200);
                var width = rect.right - rect.left;
                var height = rect.bottom - rect.top;
                var date = DateTime.UtcNow.ToBinary();
                System.IO.FileStream fs = System.IO.File.Create(@"D:\yugi-"+ date+".jpg");
                var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics.FromImage(bitmap).CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height),
                    CopyPixelOperation.SourceCopy);
                bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}