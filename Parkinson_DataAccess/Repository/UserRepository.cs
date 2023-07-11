using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Parkinson_DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private string secretkey;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _RoleManager;


        public UserRepository(ApplicationDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IMapper mapper, RoleManager<IdentityRole> RoleManager)
        {
            _context = context;
            _userManager = userManager;
            secretkey = configuration.GetValue<string>("APISetting:secret");
            _mapper = mapper;
            _RoleManager = RoleManager;
        }
        public bool IsuniqueUser(string username)
        {
            var user = _context.IdentityUsers.FirstOrDefault(N => N.UserName == username);
            if (user is null)
                return true;
            else
                return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.IdentityUsers
                .FirstOrDefaultAsync(l => l.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user is null || isValid is false)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }
            //if found generate jwt token
            var role = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,role.FirstOrDefault()),

                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var id = GetUserIdByUserName(user.UserName);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user),
                Id = id



            };
            return loginResponseDto;
        }


        public int GetUserIdByUserName(string? userName)
        {
            var userDoctor = (_context.Doctors.FirstOrDefault(doctor => doctor.User.UserName == userName));
            var userPatient = _context.Patients.FirstOrDefault(doctor => doctor.User.UserName == userName);
            if (userPatient is not null)
            {
                return userPatient.Id;
            }

            if (userDoctor is not null)
            {
                return userDoctor.Id;
            }
            else
            {
                return 0;
            }
        }



        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            IdentityUser user = new()
            {

                Email = registrationRequestDto.UserName,
                NormalizedEmail = registrationRequestDto.UserName.ToUpper(),
                UserName = registrationRequestDto.UserName,

            };
            try
            {
                var isValidRole = await _RoleManager.FindByNameAsync(registrationRequestDto.Role);
                if (isValidRole is null)
                {
                    user = null;
                    return null;
                }
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);
                    var userToReturn = await _context.IdentityUsers.FirstOrDefaultAsync(
                            u => u.UserName == registrationRequestDto.UserName);
                    return _mapper.Map<UserDto>(userToReturn);
                }
                else
                {
                    user = null;
                    return null;
                }
            }
            catch (Exception e)
            {

            }

            return new UserDto();
        }
    }
}
