namespace Kiwifruit.Models.Enums
{
    /// <summary>
    /// 日志等级枚举
    /// </summary>
    public enum LogLevelEnum
    {
        /// <summary>
        ///     调试级别
        /// </summary>
        Debug = 1,

        /// <summary>
        ///     日常信息级别
        /// </summary>
        /// 
        Info = 2,

        /// <summary>
        ///     警告级别
        /// </summary>
        /// 
        Warn = 3,

        /// <summary>
        ///     出错级别
        /// </summary>
        Error = 4,

        /// <summary>
        ///     不记录
        /// </summary>
        Off = 5
    }
}
