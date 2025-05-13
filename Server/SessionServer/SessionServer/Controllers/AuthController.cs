using Microsoft.AspNetCore.Mvc;
using SessionServer.DBContexts;
using SessionServer.Models;
using SessionServer.Requests;

namespace SessionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(UserDBContext context, ILogger<AuthController> logger>)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequest request)
        {
            _logger.LogInformation($"회원가입 요청: 유저 이름: {request.UserName}");
            
            if (_context.Users.Any(u => u.UserName == request.UserName))
                return Conflict("이미 존재하는 유저 이름입니다.");
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                UserName = request.UserName,
                Password = hashedPassword
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();
            
            return Ok("회원 가입 성공!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == request.UserName);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    _logger.LogWarning($"로그인 실패: 유저 이름: {request.UserName}");
                    return Unauthorized("알맞지 않은 유저 정보");
                }
                
                _logger.LogInformation($"로그인 성공: 유저 이름: {request.UserName}");
                
                return Ok(new { username = user.UserName, id = user.Id });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "서버 오류...");
            }
        }
    }
}
