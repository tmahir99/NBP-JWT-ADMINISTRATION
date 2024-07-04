using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Entities;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthAspNet7WebAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = token,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles.ToList()
            };
        }

        public async Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an ADMIN"
            };
        }

        public async Task<AuthServiceResponseDto> MakeSuperAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.SUPERADMIN);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an SuperAdmin"
            };
        }

        public async Task<AuthServiceResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.OWNER);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an OWNER"
            };
        }

        public async Task<AuthServiceResponseDto> MakeUserAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.USER);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Guest " + user + " is now an User"
            };
        }

        public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var isExistsUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (isExistsUser != null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "UserName Already Exists"
                };

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Beacause: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }

            // Add a Default GUEST Role to all users
            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.GUEST);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + newUser + " Created Successfully"
            };
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);
            bool isSuperAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.SUPERADMIN);
            bool isGuestRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.GUEST);
            bool isEngineerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ENGINEER);
            bool isCarrierRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.CARRIER);
            bool isProductionWorkerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.PRODUCTION_WORKER);
            bool isProcurementRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.PROCUREMENT);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists && isSuperAdminRoleExists && isGuestRoleExists
                && isEngineerRoleExists && isCarrierRoleExists && isProductionWorkerRoleExists && isProcurementRoleExists)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roles Seeding is Already Done"
                };
            }

            if (!isUserRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            }
            if (!isAdminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            }
            if (!isOwnerRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            }
            if (!isSuperAdminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.SUPERADMIN));
            }
            if (!isGuestRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.GUEST));
            }
            if (!isEngineerRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ENGINEER));
            }
            if (!isCarrierRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.CARRIER));
            }
            if (!isProductionWorkerRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.PRODUCTION_WORKER));
            }
            if (!isProcurementRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.PROCUREMENT));
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Role Seeding Done Successfully"
            };
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        public async Task<List<ApplicationUserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<ApplicationUserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new ApplicationUserDto
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return userDtos;
        }

        public async Task<List<string>> GetAllUserNamesAsync()
        {
            var userNames = _userManager.Users.Select(user => user.UserName).ToList();
            return userNames;
        }

        public async Task<AuthServiceResponseDto> RemoveAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, StaticUserRoles.ADMIN);

            if (!isAdmin)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not an admin"
                };
            }

            var removeAdminResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.ADMIN);

            if (!removeAdminResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove admin role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer an admin"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveOwnerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isOwner = await _userManager.IsInRoleAsync(user, StaticUserRoles.OWNER);

            if (!isOwner)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not an OWNER"
                };
            }

            var removeOwnerResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.OWNER);

            if (!removeOwnerResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove OWNER role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer an OWNER"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveUserAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isUser = await _userManager.IsInRoleAsync(user, StaticUserRoles.USER);

            if (!isUser)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a USER"
                };
            }

            var removeUserResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.USER);

            if (!removeUserResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove USER role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a USER"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveSuperAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isSuperAdmin = await _userManager.IsInRoleAsync(user, StaticUserRoles.SUPERADMIN);

            if (!isSuperAdmin)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a SUPERADMIN"
                };
            }

            var removeSuperAdminResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.SUPERADMIN);

            if (!removeSuperAdminResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove SUPERADMIN role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a SUPERADMIN"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveEngineerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isEngineer = await _userManager.IsInRoleAsync(user, StaticUserRoles.ENGINEER);

            if (!isEngineer)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not an ENGINEER"
                };
            }

            var removeEngineerResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.ENGINEER);

            if (!removeEngineerResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove ENGINEER role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer an ENGINEER"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveCarrierAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isCarrier = await _userManager.IsInRoleAsync(user, StaticUserRoles.CARRIER);

            if (!isCarrier)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a CARRIER"
                };
            }

            var removeCarrierResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.CARRIER);

            if (!removeCarrierResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove CARRIER role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a CARRIER"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveProductionWorkerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isProductionWorker = await _userManager.IsInRoleAsync(user, StaticUserRoles.PRODUCTION_WORKER);

            if (!isProductionWorker)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a PRODUCTION_WORKER"
                };
            }

            var removeProductionWorkerResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.PRODUCTION_WORKER);

            if (!removeProductionWorkerResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove PRODUCTION_WORKER role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a PRODUCTION_WORKER"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveProcurementAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isProcurement = await _userManager.IsInRoleAsync(user, StaticUserRoles.PROCUREMENT);

            if (!isProcurement)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a PROCUREMENT"
                };
            }

            var removeProcurementResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.PROCUREMENT);

            if (!removeProcurementResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove PROCUREMENT role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a PROCUREMENT"
            };
        }

        public async Task<AuthServiceResponseDto> MakeEngineerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.ENGINEER);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an ENGINEER"
            };
        }

        public async Task<AuthServiceResponseDto> MakeCarrierAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.CARRIER);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an CARRIER"
            };
        }

        public async Task<AuthServiceResponseDto> MakeProductionWorkerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.PRODUCTION_WORKER);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now an PRODUCTION_WORKER"
            };
        }

        public async Task<AuthServiceResponseDto> MakeProcurementAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRoles.PROCUREMENT);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + " is now a PROCUREMENT"
            };
        }

        public async Task<AuthServiceResponseDto> RemoveGuestAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name " + user
                };
            }

            var isGuest = await _userManager.IsInRoleAsync(user, StaticUserRoles.GUEST);

            if (!isGuest)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User " + user + " is not a GUEST"
                };
            }

            var removeGuestResult = await _userManager.RemoveFromRoleAsync(user, StaticUserRoles.GUEST);

            if (!removeGuestResult.Succeeded)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Failed to remove GUEST role for " + user
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User " + user + "  is no longer a GUEST"
            };
        }
    }
}
