using Models;
using Models.DTO;

namespace Services;

public interface IPasswordResetTokenService
{

    public Task<ResponsePageDto<IPasswordResetToken>> ReadPasswordResetTokensAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IPasswordResetToken>> ReadPasswordResetTokenAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IPasswordResetToken>> DeletePasswordResetTokenAsync(Guid id);
    public Task<ResponseItemDto<IPasswordResetToken>> UpdatePasswordResetTokenAsync(PasswordResetTokenDto item);
    public Task<ResponseItemDto<IPasswordResetToken>> CreatePasswordResetTokenAsync(PasswordResetTokenDto item);
    public Task<ResponseItemDto<IStaff>> ResetPasswordAsync(PasswordResetTokenDto item);
    public Task<ResponseItemDto<IPasswordResetToken>> ChangePasswordAsync(string email, string currentPassword, string newPassword);

}