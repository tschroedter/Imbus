using System;
using Imbus.Core.Autofac.NLog.Interfaces;
using NLog;

// ReSharper disable UnusedMember.Global

namespace Imbus.Core.Autofac.NLog
{
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

        public void Error(Exception exception,
                          string format,
                          params object[] args)
        {
            m_Logger.Error(exception,
                           format,
                           args);
        }

        public void Error(Exception exception)
        {
            m_Logger.Error(exception);
        }

        public void Debug(string message)
        {
            m_Logger.Debug(message);
        }

        public void Debug(Exception exception,
                          string message,
                          params object[] args)
        {
            m_Logger.Debug(exception,
                           message,
                           args);
        }

        public void Error(string message)
        {
            m_Logger.Error(message);
        }

        public void Fatal(string message)
        {
            m_Logger.Fatal(message);
        }

        public void Fatal(Exception exception,
                          string message,
                          params object[] args)
        {
            m_Logger.Fatal(exception,
                           message,
                           args);
        }

        public void Fatal(string format,
                          params object[] args)
        {
            m_Logger.Fatal(format,
                           args);
        }

        public void Info(Exception exception,
                         string message,
                         params object[] args)
        {
            m_Logger.Info(exception,
                          message,
                          args);
        }

        public void Info(string message)
        {
            m_Logger.Info(message);
        }

        public void Warn(string message)
        {
            m_Logger.Warn(message);
        }

        public void Warn(Exception exception,
                         string message,
                         params object[] args)
        {
            m_Logger.Warn(exception,
                          message,
                          args);
        }

        public void Warn(string format,
                         params object[] args)
        {
            m_Logger.Warn(format,
                          args);
        }
    }
}