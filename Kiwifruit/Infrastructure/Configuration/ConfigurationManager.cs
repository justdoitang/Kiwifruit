using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Kiwifruit.Infrastructure.Configuration
{
    /// <summary>
    /// 获取配置信息
    /// </summary>
    public class ConfigurationManager
    {
        private static readonly object _locker = new object();

        private static ConfigurationManager _instance = null;

        private IConfigurationRoot Config { get; }

        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        private static ConfigurationManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                }
            }

            return _instance;
        }

        private static void Testc()
        {
            Console.WriteLine(1);
        }

        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }
    }
}
