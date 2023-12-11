using System;
using System.Diagnostics;

public class WebOpener
{
    public void OpenWebPage(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine("无法打开网页：" + ex.Message);
        }
    }
}