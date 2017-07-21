using System;
using JetBrains.Annotations;

namespace Imbus.Core.Example.Interfaces
{
    public interface IImbusLogger
    {
        // ReSharper disable UnusedMember.Global
        void Debug([NotNull] string format,
                   [NotNull] params object[] args);

        void Info([NotNull] string format,
                  [NotNull] params object[] args);

        void Error([NotNull] string format,
                   [NotNull] params object[] args);

        void Error([NotNull] Exception exception);

        void Error(Exception exception,
                   string format,
                   params object[] args);
        // ReSharper restore UnusedMember.Global
    }
}