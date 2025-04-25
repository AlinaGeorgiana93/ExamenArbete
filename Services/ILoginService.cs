using System;
using Models.DTO;

namespace Services;

public interface ILoginService
{
    public Task<ResponseItemDto<LoginUserSessionDto>> LoginUserAsync(LoginCredentialsDto usrCreds);
    public Task<ResponseItemDto<LoginStaffSessionDto>> LoginStaffAsync(LoginCredentialsDto usrCreds);
}

