using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class PasswordResetTokenServiceDb : IPasswordResetTokenService
{
    private readonly PasswordResetTokenRepos _passresetRepo;

    private readonly ILogger<PasswordResetTokenServiceDb> _logger;



    public PasswordResetTokenServiceDb(PasswordResetTokenRepos passresetRepo, ILogger<PasswordResetTokenServiceDb> logger)
    {
        _passresetRepo = passresetRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IPasswordResetToken>> ReadPasswordResetTokensAsync(bool flat, string filter, int pageNumber, int pageSize) => _passresetRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IPasswordResetToken>> ReadPasswordResetTokenAsync(Guid id, bool flat) => _passresetRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IPasswordResetToken>> DeletePasswordResetTokenAsync(Guid id) => _passresetRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IPasswordResetToken>> UpdatePasswordResetTokenAsync(PasswordResetTokenDto item) => _passresetRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IPasswordResetToken>> CreatePasswordResetTokenAsync(PasswordResetTokenDto item) => _passresetRepo.CreateItemAsync(item);
    public Task<ResponseItemDto<IStaff>> ResetPasswordAsync(PasswordResetTokenDto item) => _passresetRepo.ResetPasswordAsync(item);
    public Task<bool> ChangePasswordAsync(string email, string newPassword, string currentPassword)
    {
        throw new NotImplementedException();
    }
}