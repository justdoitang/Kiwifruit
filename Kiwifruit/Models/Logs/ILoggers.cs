namespace Kiwifruit.Models.Logs
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggers
    {
        /// <summary>
        ///     调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Debug(object source, string message);

        /// <summary>
        ///     关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Info(object source, object message);

        /// <summary>
        ///     警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Warn(object source, object message);

        /// <summary>
        ///     错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Error(object source, object message);

        /// <summary>
        ///     错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="title">title</param>
        void Error(object source, object message, string title);

        /// <summary>
        ///     失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Fatal(object source, object message);

        /// <summary>
        ///     关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        void Apiops(object source, object message);
    }
}
