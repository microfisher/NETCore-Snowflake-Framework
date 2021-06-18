using System;
using System.Threading.Tasks;
using Snowflake.Services.Dtos;
using Snowflake.Services.Modules.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Snowflake.Identity.Controllers
{
    /// <summary>
    /// 身份认证服务
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public IdentityController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 创建账户
        /// </summary>
        [HttpPost("account")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] SignUpDto dto)
        {
            var data = await _userManager.SignUp(dto);
            return Ok(data);
        }

        /// <summary>
        /// 登陆账号（密码错误10次锁定5分钟后才能再登陆）
        /// </summary>
        [HttpPost("authorize")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] SignInDto dto)
        {
            var data = await _userManager.SignIn(dto);
            return Ok(data);
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        [HttpGet("authorize")]
        public async Task<IActionResult> Get()
        {
            var data = await _userManager.GetJwtToken();
            return Ok(data);
        }

        /// <summary>
        /// 登出账号
        /// </summary>     
        [HttpDelete("authorize")]
        public async Task<IActionResult> Delete()
        {
            var data = await _userManager.SignOut();
            return Ok(data);
        }


    }
}



