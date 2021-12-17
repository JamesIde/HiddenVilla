using Models;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IStripePaymentService
    {

        public Task<SuccessModel> CheckOutCompleted(StripePaymentDTO model);

    }
}
