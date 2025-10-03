﻿using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Services.Authentication
{
    public interface IAuthenticationCoordinator
    {
        IAuthenticationProvider SelectProvider(AuthenticationProviderType type);
    }
}