using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces;

public interface ICustomerService
{
    void Register(string userId, string address);
    
    Customer? GetCustomerOrDefault();

    Customer GetCustomer();
}