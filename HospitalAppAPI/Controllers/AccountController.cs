using HospitalAPP.ErrorHandler;
using HospitalAPP.JWTToken.Interace;
using HospitalAPP.Wrapper.WorkWrapper;
using HospitalDomain.DTOS;
using HospitalDomain.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<Guest> _GuestManager;
        private readonly SignInManager<Guest> _signInManager;
        private readonly UserManager<Employee> _employeeManager;
        private readonly SignInManager<Employee> _signInManagerEmployee;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<Guest> GuestManager, SignInManager<Guest> signInManager, UserManager<Employee> EmployeeManager, SignInManager<Employee> signInManagerEmployee, ITokenService tokenService)
        {
            _GuestManager = GuestManager;
            _signInManager = signInManager;
            _employeeManager = EmployeeManager;
            _signInManagerEmployee = signInManagerEmployee;
            _tokenService = tokenService;
        }

        [HttpPost("GuestRegister")]
        public async Task<ActionResult<GuestDto>> Register(GuestReigsterDTO registerDto)
        {
            try
            {
                if (CheckIfGuestExist(registerDto.Email).Result.Value)
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));

                var user = new Guest()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    IdentityCardNumber = registerDto.IdentityCardNumber,
                    UserName = registerDto.Email.Split('@')[0]
                };
                var Result = await _GuestManager.CreateAsync(user, registerDto.Password);

                if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "register fail"));

                var ReturnedUser = new GuestDto()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    IdentityCardNumber = registerDto.IdentityCardNumber,

                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<GuestDto>.Success(ReturnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<Guest>.Fail(ex.Message));
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
                };
                var Result = await _employeeManager.CreateAsync(user, registerDto.Password);

                if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "register fail"));

                var ReturnedUser = new EmployeeDTO()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,

                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<EmployeeDTO>.Success(ReturnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<Guest>.Fail(ex.Message));
            }
        }

        [HttpGet("IsGuestExist")]
        public async Task<ActionResult<bool>> CheckIfGuestExist(string Email)
        {
            return await _GuestManager.FindByEmailAsync(Email) is not null;
        }

        [HttpGet("IsEmployeeExist")]
        public async Task<ActionResult<bool>> CheckIfEmployeeExist(string Email)
        {
            return await _employeeManager.FindByEmailAsync(Email) is not null;
        }
    }
}