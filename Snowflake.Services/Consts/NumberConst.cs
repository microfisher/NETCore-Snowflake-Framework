using System;
namespace Snowflake.Services.Consts
{
    public static class NumberConst
    {
        public const int IDENTITY_SIGN_IN_STATE_EXPIRED = 7 * 86400;// 登陆状态保存时间

        public const int IDENTITY_SIGN_IN_FAILED_COUNT = 10;// 登陆最大失败次数

        public const int IDENTITY_SIGN_IN_LOCKED_SECOND = 300;// 超过登陆最大失败次数后锁定秒数
    }
}
