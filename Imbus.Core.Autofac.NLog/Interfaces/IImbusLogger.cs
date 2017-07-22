using System;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Imbus.Core.Autofac.NLog.Interfaces
{
    // INFO: ADAPTED FROM NLOG:INTERFACE Project https://github.com/uhaciogullari/NLog.Interface
    public interface IImbusLogger
    {
        void Debug([NotNull] string format,
                   [NotNull] params object[] args);

        void Error([NotNull] string format,
                   [NotNull] params object[] args);

        void Error([NotNull] Exception exception);

        void Error(Exception exception,
                   string format,
                   params object[] args);

        void Info([NotNull] string format,
                  [NotNull] params object[] args);
    }
}