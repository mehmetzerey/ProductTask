using App.Application.ModelView;
using App.Domain;

namespace App.Application.Authentication;

public interface IAuthService
{
    Task<ServiceResponse<TokenView>> SignInAsync(string email, string password, bool isPersistent, bool lockOutFailure);
    Task<ServiceResponse> UpdatePassword(UpdatePasswordRequestModel updatePasswordRequestModel);

    Task<ServiceResponse> IForgotMyPassword(ResetPasswordRequestModel resetPasswordRequest);
}
