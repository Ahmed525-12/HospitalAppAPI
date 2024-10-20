using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CommandLine.Text;
using HospitalAPP.Email.Intrefaces;
using HospitalAPP.ErrorHandler;
using HospitalAPP.JWTToken.Interace;
using HospitalAPP.Wrapper.WorkWrapper;
using HospitalAppAPI.Cahceing;
using HospitalDomain.DTOS;
using HospitalDomain.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using System.Security.Claims;

namespace HospitalAppAPI.Controllers
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class AccountController : BaseController
    {
        private readonly IEmailSettings _emailSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRedisCahe _redisCahe;
        private readonly UserManager<Guest> _guestManager;
        private readonly SignInManager<Guest> _signInManagerGuest;
        private readonly UserManager<Employee> _employeeManager;
        private readonly SignInManager<Employee> _signInManagerEmployee;
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _signInManagerAccount;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<Guest> guestManager,
        SignInManager<Guest> signInManagerGuest,
        UserManager<Employee> employeeManager,
        SignInManager<Employee> signInManagerEmployee,
        UserManager<Account> accountManager,
        SignInManager<Account> signInManagerAccount,
        ITokenService tokenService,
       ILogger<AccountController> logger,
            IEmailSettings emailSettings,
            RoleManager<IdentityRole> roleManager,
            IRedisCahe redisCahe
            ) : base(logger)
        {
            _guestManager = guestManager;
            _signInManagerGuest = signInManagerGuest;
            _employeeManager = employeeManager;
            _signInManagerEmployee = signInManagerEmployee;
            _accountManager = accountManager;
            _signInManagerAccount = signInManagerAccount;
            _tokenService = tokenService;

            _emailSettings = emailSettings;
            _roleManager = roleManager;
            _redisCahe = redisCahe;
        }

        [HttpPost("GuestRegister")]
        public async Task<ActionResult<GuestDto>> Register()
        {
            try
            {
                // Check if the guest already exists
                var guestExists = await CheckIfGuestExist("mohab5250067@gmail.com");
                if (guestExists.Value)
                {
                    _logger.LogError("This Email Is Already Exist");
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }
                {
                }
                // Create a new Guest user
                var user = new Guest
                {
                    DisplayName = "mohab66666",
                    Email = "mohab5250067@gmail.com",
                    IdentityCardNumber = 2437647205,
                    UserName = "mohab666",
                    EmailConfirmed = true
                };

                // Create the user with the specified password
                var createResult = await _guestManager.CreateAsync(user, "@Hmed5250047");
                if (!createResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Register failed"));
                }

                // Add the user to the "Guest" role
                var roleResult = await _guestManager.AddToRoleAsync(user, "Guest");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Return the created user with the token
                var returnedUser = new GuestDto
                {
                    DisplayName = "mohab66666",
                    Email = "mohab5250067@gmail.com",
                    IdentityCardNumber = 2437647205,

                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok(Result<GuestDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new ApiResponse(500, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpPost("EmployeeRegister")]
        public async Task<ActionResult<EmployeeDTO>> EmployeeRegister(EmployeeDTORegister registerDto)
        {
            try
            {
                if (CheckIfEmployeeExist(registerDto.Email).Result.Value)
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));

                var user = new Employee()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email.Split('@')[0],
                    Salary = registerDto.Salary,
                    ShiftOfWork = registerDto.ShiftOfWork,
                    EmailConfirmed = true
                };
                var Result = await _employeeManager.CreateAsync(user, registerDto.Password);

                if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "register fail"));
                var roleResult = await _employeeManager.AddToRoleAsync(user, "Employee");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                var ReturnedUser = new EmployeeDTO()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,

                    Token = _tokenService.CreateTokenAsync(user)
                };

                if (user.RefreshToken.Any(t => t.IsAvtive))
                {
                    var activeRefreshToken = user.RefreshToken.FirstOrDefault(t => t.IsAvtive);
                    ReturnedUser.RefreshToken = activeRefreshToken.Token;
                    ReturnedUser.RefreshTokenExpiration = activeRefreshToken.Expireson;
                    setRefreshTokenCookie(activeRefreshToken.Token, activeRefreshToken.Expireson);
                }
                else
                {
                    var refreshToken = _tokenService.CreateRefreshTokenAsync();
                    ReturnedUser.RefreshToken = refreshToken.Token;
                    ReturnedUser.RefreshTokenExpiration = refreshToken.Expireson;
                    user.RefreshToken.Add(refreshToken);
                    await _employeeManager.UpdateAsync(user);
                    setRefreshTokenCookie(refreshToken.Token, refreshToken.Expireson);
                }

                return Ok((Result<EmployeeDTO>.Success(ReturnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<EmployeeDTO>.Fail(ex.Message));
            }
        }

        [HttpPost("EmployeeLogin")]
        public async Task<ActionResult<EmployeeDTO>> EmployeeLogin(EmployeeDTOLogin logInDto)
        {
            try
            {
                var user = await _employeeManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                if (await _employeeManager.IsLockedOutAsync(user))
                {
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                var resultcode = await _signInManagerEmployee.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    return BadRequest(new ApiResponse(400, "Invalid login attempt"));
                }

                var returnedUser = new EmployeeDTO
                {
                    Email = logInDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)
                };
                if (user.RefreshToken.Any(t => t.IsAvtive))
                {
                    var activeRefreshToken = user.RefreshToken.FirstOrDefault(t => t.IsAvtive);
                    returnedUser.RefreshToken = activeRefreshToken.Token;
                    returnedUser.RefreshTokenExpiration = activeRefreshToken.Expireson;
                    setRefreshTokenCookie(activeRefreshToken.Token, activeRefreshToken.Expireson);
                }
                else
                {
                    var refreshToken = _tokenService.CreateRefreshTokenAsync();
                    returnedUser.RefreshToken = refreshToken.Token;
                    returnedUser.RefreshTokenExpiration = refreshToken.Expireson;
                    user.RefreshToken.Add(refreshToken);
                    await _employeeManager.UpdateAsync(user);
                    setRefreshTokenCookie(refreshToken.Token, refreshToken.Expireson);
                }

                return Ok((Result<EmployeeDTO>.Success(returnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<EmployeeDTO>.Fail(ex.Message));
            }
        }

        [HttpPost("GuestLogin")]
        public async Task<ActionResult<GuestDto>> GuestLogin(GuestDTOLogin logInDto)
        {
            try
            {
                var user = await _guestManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                if (await _guestManager.IsLockedOutAsync(user))
                {
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }
                if (user.IdentityCardNumber != logInDto.IdentityCardNumber)
                {
                    return BadRequest(new ApiResponse(400, "IdentityCardNumber Is Wrong "));
                }
                var resultcode = await _signInManagerGuest.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    return BadRequest(new ApiResponse(400, "Invalid login attempt"));
                }

                var returnedUser = new GuestDto
                {
                    Email = logInDto.Email,
                    IdentityCardNumber = logInDto.IdentityCardNumber,

                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<GuestDto>.Success(returnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<GuestDto>.Fail(ex.Message));
            }
        }

        [HttpPost("SendEmailGuest")]
        public async Task<IActionResult> SendEmailGuest(ForgetPasswordDto emailinput)
        {
            try
            {
                if (string.IsNullOrEmpty(emailinput.Email))
                {
                    return BadRequest("Email input cannot be null or empty.");
                }

                var user = await _guestManager.FindByEmailAsync(emailinput.Email);
                if (user != null)
                {
                    var token = await _guestManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { email = user.Email, Token = token }, Request.Scheme);
                    var email = new EmailDTO()
                    {
                        Subject = "Reset Password",
                        To = emailinput.Email,
                        Body = resetPasswordLink,
                    };

                    _emailSettings.SendEmail(email);
                    var emailsucess = new RecieveEmail()
                    {
                        token = token,
                        email = emailinput.Email,
                    }
                        ;
                    return Ok(Result<RecieveEmail>.Success(emailsucess, "Success"));
                }
                else
                {
                    return Ok(Result.Fail("Email does not exist."));
                }
            }
            catch (Exception ex)
            {
                return Ok(Result.Fail(ex.Message));
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            try
            {
                var user = await _guestManager.FindByEmailAsync(model.email);
                var result = await _guestManager.ResetPasswordAsync(user, model.token, model.Password);

                if (result.Succeeded)
                {
                    return Ok(Result.Success("Success"));
                }
                else
                {
                    return Ok(Result.Fail("fail to reset password"));
                }
            }
            catch (Exception ex)
            {
                return Ok(Result.Fail(ex.Message));
            }
        }

        [HttpGet("IsGuestExist")]
        public async Task<ActionResult<bool>> CheckIfGuestExist(string Email)
        {
            return await _guestManager.FindByEmailAsync(Email) is not null;
        }

        [HttpGet("IsEmployeeExist")]
        public async Task<ActionResult<bool>> CheckIfEmployeeExist(string Email)
        {
            return await _employeeManager.FindByEmailAsync(Email) is not null;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
        {
            var users = _redisCahe.GetData<IEnumerable<IdentityUser>>("Users");
            if (users is not null)
            {
                return Ok(users);
            }
            users = _accountManager.Users; // This fetches all users from the database
            _redisCahe.SetData("Users", users);
            return Ok(users);
        }

        private void setRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = expires.ToLocalTime(),
                HttpOnly = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}