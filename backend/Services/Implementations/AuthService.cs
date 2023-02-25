using System;
using backend.Account;
using backend.Wrappers;

namespace backend.Services.Implementations
{
    public class AuthService : IAccountService
    {
        public Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ConfirmPhoneAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
