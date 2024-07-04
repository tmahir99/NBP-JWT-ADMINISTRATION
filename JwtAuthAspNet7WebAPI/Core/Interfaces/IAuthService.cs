using JwtAuthAspNet7WebAPI.Core.Dtos;

namespace JwtAuthAspNet7WebAPI.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeUserAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeSuperAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeEngineerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeCarrierAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeProductionWorkerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeProcurementAsync(UpdatePermissionDto updatePermissionDto);
        Task<List<ApplicationUserDto>> GetAllUsersAsync();
        Task<List<string>> GetAllUserNamesAsync();
        Task<AuthServiceResponseDto> RemoveAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveOwnerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveUserAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveSuperAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveEngineerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveCarrierAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveProductionWorkerAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveProcurementAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> RemoveGuestAsync(UpdatePermissionDto updatePermissionDto);
    }
}
