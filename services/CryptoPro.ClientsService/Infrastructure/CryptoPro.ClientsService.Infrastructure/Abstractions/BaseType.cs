﻿namespace CryptoPro.ClientsService.Infrastructure.Abstractions;

public abstract class BaseType(string value)
{
    public string Value { get; init; } = value;
}