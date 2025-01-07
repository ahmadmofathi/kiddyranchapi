using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserManager _userManager1;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(
            AppDbContext context,
            UserManager<User> userManager,
            IConfiguration configuration,
            IUserManager userManager1)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _userManager1 = userManager1;

        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            try
            {
                var existingUsername = await _userManager.FindByNameAsync(registerDTO.Username);
                if (existingUsername != null)
                {
                    return BadRequest("Username is already taken.");
                }

                // Check if the phone number is already used
            
                if (!registerDTO.Username.Any(char.IsUpper))
                {
                    return BadRequest("Username should has a captial and small letter at least");
                }
                var newUser = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registerDTO.Username,
                        Name = registerDTO.Name,
                    };

                
                var CreationResult = await _userManager.CreateAsync(newUser,registerDTO.Password);

                if (!CreationResult.Succeeded)
                {
                    return BadRequest(CreationResult.Errors);
                }
                // Fetch the user from the database
                var createdUser = await _userManager.FindByNameAsync(newUser.UserName);
                if (createdUser == null)
                {
                    return BadRequest("User was not found after creation");
                }

                var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, newUser.UserName),
                            new Claim(ClaimTypes.Country, "EGY"),
                        };


                var claimsResult = await _userManager.AddClaimsAsync(newUser, userClaims);
                if (!claimsResult.Succeeded)
                {
                    // If adding the claims fails, delete the user to avoid orphaned users
                    await _userManager.DeleteAsync(newUser);
                    return BadRequest(claimsResult.Errors);
                }

              
                return Ok(newUser);
            }
            catch (DbUpdateException ex)
            {
                var exceptionDetails = ex.InnerException?.Message;
                Console.WriteLine($"DbUpdateException: {exceptionDetails}");

                return BadRequest(exceptionDetails);
            }
            catch (Exception ex)
            {
                // Log the exception or return it
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credintials)
        {
            var user = await _userManager.FindByNameAsync(credintials.Username);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Your account is locked... Try again later");
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            bool isAuthenticated = await _userManager.CheckPasswordAsync(user, credintials.Password);
            if (!isAuthenticated)
            {
                await _userManager.AccessFailedAsync(user);
                return Unauthorized("Wrong Username or Password");
            }
            var exp = DateTime.Now.AddMinutes(750);
            var secretKey = _configuration.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("The secret key cannot be null or empty.");
            }
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            var Key = new SymmetricSecurityKey(secretKeyBytes);
            var methodGeneratingToken = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: exp,
                signingCredentials: methodGeneratingToken
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            return new TokenDTO
            {
                Token = tokenString,
                ExpiryDate = exp,
                Username = user.UserName,
                User_id = user.Id,
            };
        }



        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var Users = _userManager1.GetUsers();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var User = _userManager1.GetUser(id);
            if (User == null)
            {
                return BadRequest("User not found");
            }
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            UserDTO? User = _userManager1.GetUser(id);
            if (User == null)
            {
                return NotFound("User not found");
            }
            _userManager1.Delete(id);
            return Ok("User " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(string id, UserDTO User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }
            _userManager1.Update(User);
            return NoContent();
        }
    }
}
