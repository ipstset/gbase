using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace GamebaseApi.Auth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //private IAuthRepository _repository;
        private IUserStore _userStore;

        public ResourceOwnerPasswordValidator(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_userStore.ValidateCredentials(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(_userStore.FindByUsername(context.UserName).Id.ToString(), "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
