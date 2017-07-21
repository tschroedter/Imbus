using System;
using Autofac.Extras.NLog;
using Imbus.Core.Example.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Example
{
    [UsedImplicitly]
    public class ImbusLogger
        : IImbusLogger
    {
        public ImbusLogger([NotNull] ILogger logger)
        {
            m_Logger = logger;
        }

        private readonly ILogger m_Logger;

        public void Debug(string format,
                          params object[] args)
        {
            m_Logger.Debug(format,
                           args);
        }

        public void Info(string format,
                         params object[] args)
        {
            m_Logger.Info(format,
                          args);
        }

        public void Error(string format,
                          params object[] args)
        {
            m_Logger.Error(format,
                           args);
        }

        public void Error(Exception exception)
        {
            m_Logger.Error(exception);
        }

        public void Error(Exception exception,
                          string format,
                          params object[] args)
        {
            m_Logger.Error(format,
                           args,
                           exception);
        }
    }
}