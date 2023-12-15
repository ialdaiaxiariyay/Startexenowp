using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WebOpener
{
    public class WebOp
    {
        public void OpenWebPage(string url)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Windowsϵͳʹ��cmd����
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linuxϵͳʹ��xdg-open����
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // MacOSϵͳʹ��open����
                    Process.Start("open", url);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported operating system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("�޷�����ҳ��" + ex.Message);
            }
        }
    }
}