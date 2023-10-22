using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces;

public interface ICustomerService
{
    void Register(string userId);
    
    Customer? GetCustomerOrDefault();

    Customer GetCustomer();
}