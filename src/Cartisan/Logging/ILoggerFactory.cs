using System;

namespace Cartisan.Logging {
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILoggerFactory {
        ILogger CreateLogger(string name);
        ILogger CreateLogger(Type type);
    }
}