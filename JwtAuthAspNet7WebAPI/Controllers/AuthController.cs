using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Route For Seeding my roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _authService.SeedRolesAsync();

            return Ok(seerRoles);
        }


        // Route -> Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }


        // Route -> Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized(loginResult);
        }


        // Route -> make guest -> admin
        [HttpPost]
        [Route("make-admin")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]

        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> owner
        [HttpPost]
        [Route("make-owner")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> user
        [HttpPost]
        [Route("make-user")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeUser([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeUserAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> superadmin
        [HttpPost]
        [Route("make-superadmin")]
        [Authorize(Roles = StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeSuperAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeSuperAdminAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> engineer
        [HttpPost]
        [Route("make-engineer")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeEngineer([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeEngineerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> carrier
        [HttpPost]
        [Route("make-carrier")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeCarrier([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeCarrierAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> production worker
        [HttpPost]
        [Route("make-production-worker")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeProductionWorker([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeProductionWorkerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make guest -> procurement
        [HttpPost]
        [Route("make-procurement")]
        [Authorize(Roles = StaticUserRoles.OWNER + "," + StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> MakeProcurement([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeProcurementAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetAllUserNames")]
        public async Task<IActionResult> GetAllUsersNames()
        {
            var users = await _authService.GetAllUserNamesAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("remove-admin")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> RemoveAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveAdminAsync(updatePermissionDto);
            return Ok(users);
        }

        [HttpPost]
        [Route("remove-user")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveUser([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveUserAsync(updatePermissionDto);
            return Ok(users);
        }

        [HttpPost]
        [Route("remove-owner")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveOwnerAsync(updatePermissionDto);
            return Ok(users);
        }

        [HttpPost]
        [Route("remove-superadmin")]
        [Authorize(Roles = StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> RemoveSuperAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveSuperAdminAsync(updatePermissionDto);
            return Ok(users);
        }

        // Route -> remove engineer
        [HttpPost]
        [Route("remove-engineer")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveEngineer([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveEngineerAsync(updatePermissionDto);
            return Ok(users);
        }

        // Route -> remove carrier
        [HttpPost]
        [Route("remove-carrier")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveCarrier([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveCarrierAsync(updatePermissionDto);
            return Ok(users);
        }

        // Route -> remove production worker
        [HttpPost]
        [Route("remove-production-worker")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveProductionWorker([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveProductionWorkerAsync(updatePermissionDto);
            return Ok(users);
        }

        // Route -> remove procurement
        [HttpPost]
        [Route("remove-procurement")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.OWNER)]
        public async Task<IActionResult> RemoveProcurement([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var users = await _authService.RemoveProcurementAsync(updatePermissionDto);
            return Ok(users);
        }
    }
}
