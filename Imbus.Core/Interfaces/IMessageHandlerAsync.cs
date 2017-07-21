using System.Threading.Tasks;
using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace Imbus.Core.Interfaces
{
    public interface IMessageHandlerAsync <in T>
    {
        Task Handle([NotNull] T message);
    }
}