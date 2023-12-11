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
                    // Windows系统使用cmd命令
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linux系统使用xdg-open命令
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // MacOS系统使用open命令
                    Process.Start("open", url);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported operating system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法打开网页：" + ex.Message);
            }
        }
    }
}