﻿namespace TastyBeans.Profile.Api.Shared;

public class AggregateNotFoundException: Exception
{
    public AggregateNotFoundException(string? message) : base(message)
    {
    }
}