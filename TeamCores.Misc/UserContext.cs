using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using TeamCores.Models;
using TeamCores.Models.Enum;
using TeamCores.Data.Entity;
using TeamCores.Data.DataAccess;
using TeamCores.Common;
using TeamCores.Common.Utilities;

namespace TeamCores.Misc
{
    public class UserContext: UserSessionModel
    {
        HttpContext context;
        UserSessionBag sessionBag;
        string token;
        long sessionId;

        private HttpContext Context
        {
            get
            {
                return context;
            }
        }

        private UserSessionBag SessionBag
        {
            get
            {
                if (sessionBag == null)
                {
                    sessionBag = UserSessionBag.Setup(this, Context);
                }
                return sessionBag;
            }
        }

        public bool IsGuest
        {
            get { return UserId == 0; }
        }


        private string Token
        {
            get
            {
                return token;
            }
        }

        public long SessionId
        {
            get { return sessionId; }
            set
            {
                sessionId = value;
                this.SessionBag.SessionId = sessionId;
            }
        }

        public UserContext(HttpContext context)
        {
            this.context = context;
            Initialize();
        }

        internal static UserContext Standby(HttpContext context)
        {
            var me = context.Items[Keys.UserContext] as UserContext;
            if (me == null)
            {
                me = new UserContext(context);
                context.Items.Add(Keys.UserContext, me);
            }
            return me;
        }

        /// <summary>
        /// 初始化cookie读取
        /// </summary>
        private void Initialize()
        {
            var userCookie = Cookies.Get(Context, Keys.UserCookie);
            if (userCookie.Count == 0)
            {
                UserId = 0;
                token = string.Empty;
                Username = "Guest";
            }
            else
            {
                try
                {
                    UserId = long.Parse(userCookie["uid"]);
                    token = userCookie["token"];
                }
                catch { Logout(); }
                SessionBag.CopyValue();
                ValidateCookie();
            }
        }

        /// <summary>
        /// 验证token
        /// </summary>
        void ValidateCookie()
        {
            if (!IsGuest && CreateCookieToken(UserId, Username, Password) != Token)
            {
                Logout();
            }
        }

        /// <summary>
        /// 创建cookie 令牌
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="md5Password"></param>
        /// <returns></returns>
        string CreateCookieToken(long userid, string name, string md5Password)
        {
            return Strings.CreateToken(Keys.TokenPrefix + userid + name + md5Password);
        }

        /// <summary>
        /// 退出清除cookie 和 session
        /// </summary>
        public void Logout()
        {
            SessionBag.Refresh();
            Cookies.Remove(Context, Keys.UserCookie);
        }

        /// <summary>
        /// 保存登陆信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="effectiveTime"></param>
        public void SaveLogin(UserSessionModel user, int effectiveTime)
        {
            var userCookie = Cookies.Get(Context, Keys.UserCookie);
            userCookie["uid"] = user.UserId.ToString();
            userCookie["token"] = CreateCookieToken(user.UserId, user.Username, user.Password);
            if (effectiveTime == 0)
            {
                effectiveTime = 2;
            }
            else
            {
                effectiveTime = effectiveTime * 24;
            }
            CookieOptions option = new CookieOptions()
            {
                Expires = DateTime.Now.AddHours(effectiveTime)
            };
            Cookies.Save(Context, Keys.UserCookie, userCookie, option);
        }

        /// <summary>
        /// 申请登陆
        /// </summary>
        /// <param name="name">账户名</param>
        /// <param name="password">登陆密码</param>
        /// <param name="effectiveTime">保存时间</param>
        /// <param name="isAdmin">是否管理员登陆</param>
        /// <returns></returns>
        private EnumLoginState ApplyLogin(string name, string password, int effectiveTime, bool isAdmin)
        {
            if (string.IsNullOrEmpty(name))
            {
                return EnumLoginState.AccountError;
            }
            Users user = new Users();

            if (name.Contains("@"))
            {
                user = UsersAccessor.GetByEmail(name);
            }
            else
            {
                user = UsersAccessor.GetByMobile(name);
            }
            if (user == null)
            {
                return EnumLoginState.AccountError;
            }
            if (!string.Equals(user.Password, Strings.PasswordEncrypt(password)))
            {
                return EnumLoginState.PasswordError;
            }


            UserId = user.UserId;
            Password = user.Password;
            Username = user.Username;
            Mobile = user.Mobile;
            Email = user.Email;
			CurrentLoginTime = DateTime.Now;

            SaveLogin(this, effectiveTime);
            SessionBag.Refresh();
            SessionBag.CopyValue();
            //更新用户登陆次数
            //更新在线数据
            return EnumLoginState.Succeed;
        }

        public EnumLoginState UserLogin(string name, string password, int effectiveTime)
        {
            return ApplyLogin(name, password, effectiveTime, false);
        }


		/// <summary>
		/// 更新本次学习的时间
		/// </summary>
		public void UpdateStudingTime()
		{
			LastReportStudyTime = DateTime.Now;
		}
	}
}
