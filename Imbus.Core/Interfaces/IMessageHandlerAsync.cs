using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace Imbus.Core.Interfaces
{
    public interface IMessageHandlerAsync <in T>
    {
        void Handle([NotNull] T message);
    }
}