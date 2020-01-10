using System;
using System.Threading.Tasks;
using Microfisher.Snowflake.Services.Dtos;
using Microfisher.Snowflake.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microfisher.Snowflake.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("v1/identity")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 创建账户
        /// </summary>
        [HttpPost("account")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] SignUpDto dto)
        {
            var data = await _accountService.SignUp(dto);
            return Ok(data);
        }

        /// <summary>
        /// 登陆账号（密码错误10次锁定5分钟后才能再登陆）
        /// </summary>
        [HttpPost("authorize")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] SignInDto dto)
        {
            var data = await _accountService.SignIn(dto);
            return Ok(data);
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        [HttpGet("authorize")]
        public async Task<IActionResult> Get()
        {
            var data = await _accountService.GetJwtToken();
            return Ok(data);
        }

        /// <summary>
        /// 登出账号
        /// </summary>     
        [HttpDelete("authorize")]
        public async Task<IActionResult> Delete()
        {
            var data = await _accountService.SignOut();
            return Ok(data);
        }


    }
}



