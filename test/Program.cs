using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SnakeGame;
using ialdLibrary;
using WebOpener;
class Program
{
    private static object assembly;

    static void Main()
    {
        // 设置st.json文件的路径为当前工作目录
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "st.json");

        // 检查st.json文件是否存在
        if (!File.Exists(jsonFilePath))
        {
            // 如果不存在，则创建一个新的st.json文件并写入默认内容
            JObject defaultConfig = new JObject
            {
                ["application"] = ""
            };

            File.WriteAllText(jsonFilePath, defaultConfig.ToString());
            Console.WriteLine("未找到st.json，已创建新文件。");
        }

        // 读取st.json文件
        string jsonString = File.ReadAllText(jsonFilePath);

        // 解析st.json文件
        dynamic config = JsonConvert.DeserializeObject(jsonString);
        string applicationPath = config.application;

        // 启动其他exe程序，前提是applicationPath不为空
        if (!string.IsNullOrEmpty(applicationPath))
        {
            Process process = new Process();
            process.StartInfo.FileName = applicationPath;
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"无法启动应用程序: {ex.Message}");
                return;
            }

            Console.WriteLine("请输入exit或stop来关闭执行窗口或向程序发送关闭信号。");

            // 监听用户输入
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "exit")
                {
                    Console.WriteLine("正在关闭程序...");
                    break;
                }
                else if (input == "stop")
                {
                    if (!process.HasExited)
                    {
                        process.CloseMainWindow();
                        Console.WriteLine("已向程序发送关闭信号。");
                    }
                    else
                    {
                        Console.WriteLine("程序已经关闭。");
                    }
                }
                else if (input == "tcs")
                {
                    Console.WriteLine("正在启动贪吃蛇");
                    Snake game = new Snake();
                    game.Start();
                }
                else if (input == "wy")
                {
                    WebOp webOpener = new WebOp();

                    // 使用实例调用OpenWebPage方法
                    webOpener.OpenWebPage("https://www.ialdaiaxiariyay.top");
                }
                else
                {
                    Console.WriteLine("未知指令。请输入exit或stop。");
                }
            }
        }
        else
        {
            Console.WriteLine("Application path is empty. Please set the path to an executable in st.json.");
        }
    }
}