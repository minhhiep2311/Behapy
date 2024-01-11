﻿using Behapy.Presentation.Models;

namespace Behapy.Presentation.Services.Interfaces;

public interface ICustomerService
{
    void Register(string userId, string address, DateTime birthday);
    
    Customer? GetCustomerOrDefault(bool track = false);

    Customer GetCustomer(bool track = false);
}