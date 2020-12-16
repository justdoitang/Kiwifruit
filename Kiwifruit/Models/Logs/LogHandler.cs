using Kiwifruit.Models.Enums;
using Kiwifruit.Tools;
using log4net;
using log4net.Repository;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Kiwifruit.Models.Logs
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHandler : ILoggers
    {
        public static ILoggerRepository Repository { get; set; }
        private readonly ConcurrentDictionary<Type, ILog> _loggers = new ConcurrentDictionary<Type, ILog>();

        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="source">soruce</param>
        /// <returns></returns>
        private ILog GetLogger(Type source)
        {
            if (_loggers.ContainsKey(source))
            {
                return _loggers[source];
            }
            else
            {
                //log4net的仓储由程序启动时创建，并赋值给公共库的Repository
                ILog logger = LogManager.GetLogger(Repository.Name, source);
                _loggers.TryAdd(source, logger);
                return logger;
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Debug(object source, string message)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }

        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Info(object source, object message)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Warn(object source, object message)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }

        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Error(object source, object message)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }

        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Fatal(object source, object message)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }

        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Apiops(object source, object message)
        {
            Info(source.GetType(), message);
        }

        public void Error(object source, object message, string title)
        {
            ILog logger = GetLogger(source.GetType());
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        ///     格式化打印信息
        ///     时间 日志级别 进程id [进程名] : /*日志内容*/
        ///     日志级别 INFO｜WARN ｜ERROR ｜DEBUG
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLine(LogLevelEnum logLevel, string format, params object[] args)
        {
            var info = string.Format("{0} {1} {2} [{3}]:/*{4}*/", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                logLevel.GetDescription().ToUpper(),
                Process.GetCurrentProcess().Id,
                Process.GetCurrentProcess().ProcessName,
               args.Length > 0 ? string.Format(format, args) : format);
            Console.WriteLine(info);
        }
    }
}
