using DataAccessLayer.ViewModels;
using DataAccessLayer.ViewModels.HD_ViewModels;

namespace HomeDelivey.HomeDelivery_BussinessLgic
{
    public interface IHDBussinessLogic
    {
        Task<CustomerDetails> CustomerDetails(string phonenumber);
        Task<IEnumerable<Catalog>> GetCatalog();
        Task<ResponseMessage> PostOrder(CustomerOrderViewModel co);
        Task<IEnumerable<PreviousOrdersViewModel>> PreviousOrders(string phonenumber);
        Task<ResponseMessage> RecheckOrderAmount(string materialcode, int quantity, double price);
    }
}