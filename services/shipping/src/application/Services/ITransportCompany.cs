﻿namespace RecommendCoffee.Shipping.Application.Services;

public interface ITransportCompany
{
    Task ShipAsync(Guid shippingOrderId);
}