using DataAccessLayer.ViewModels;

namespace HomeDelivey.HomeDelivery_BussinessLgic
{
    public interface ISetups
    {
        Task<CodeGenResponse> CodeGenerators(string TypeName);
    }
}