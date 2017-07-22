using System;
using Imbus.Core.Autofac.NLog.Interfaces;
using NLog;

namespace Imbus.Core.Autofac.NLog
{
    //INFO: ADAPTED FROM NLOG:INTERFACE Project https://github.com/uhaciogullari/NLog.Interface
    public class ImbusLoggerAdapter
        : IImbusLogger
    {
        public ImbusLoggerAdapter(ILogger logger)
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
            m_Logger.Error(exception,
                           format,
                           args);
        }
    }
}