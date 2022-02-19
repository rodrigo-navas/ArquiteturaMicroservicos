using Provedor_Email.Messages;
using System.Threading.Tasks;

namespace Provedor_Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
