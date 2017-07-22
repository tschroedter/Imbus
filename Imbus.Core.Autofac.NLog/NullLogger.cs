using System;
using System.Diagnostics.CodeAnalysis;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Imbus.Core.Autofac.NLog
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class NullLogger
        : IImbusLogger
    {
        public void Debug(string format,
                          params object[] args)
        {
        }

        public void Info(string format,
                         params object[] args)
        {
        }

        public void Error(string format,
                          params object[] args)
        {
        }

        public void Error(Exception exception)
        {
        }

        public void Error(Exception exception,
                          string format,
                          params object[] args)
        {
        }
    }
}