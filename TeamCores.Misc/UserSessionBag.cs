using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TeamCores.Common;
using TeamCores.Data.DataAccess;

namespace TeamCores.Misc
{
    sealed class UserSessionBag
    {
        UserContext user;
        HttpContext context;
        UserSessionStorage storage;

        UserSessionBag(UserContext userContext, HttpContext httpContext)
        {
            user = userContext;
            context = httpContext;
        }

        internal static UserSessionBag Setup(UserContext userContext, HttpContext httpContext)
        {
            return new UserSessionBag(userContext, httpContext);
        }

        internal long SessionId
        {
            get
            {
                if (storage == null)
                {
                    return 0;
                }
                return storage.SessionId;
            }
            set
            {
                if (storage != null)
                {
                    storage.SessionId = value;
                }
            }
        }


        UserSessionStorage CreateStorage()
        {
            if (user.IsGuest)
            {
                var storage = new UserSessionStorage();

                return storage;
            }
            var data = UsersAccessor.Get(user.UserId);
            if (data != null)
            {
                try
                {
                    var storage = new UserSessionStorage();
                    storage.SessionId = data.UserId;
                    storage.Username = data.Username;
                    storage.Mobile = data.Mobile;
                    storage.Email = data.Email;
                    storage.Title = data.Title;
                    storage.Name = data.Name;
                    storage.Password = data.Password;
                    return storage;
                }
                catch
                {
                    user.Logout();
                }
            }
            user.Logout();
            return new UserSessionStorage();
        }

        void EnsureStorage()
        {
            if (storage == null)
            {
                if (user.IsGuest)
                {
                    storage = CreateStorage();
                }
                else
                {
                    var data = context.Session.GetString(Keys.UserSession);
                    if (!string.IsNullOrEmpty(data))
                    {
                        storage = JsonConvert.DeserializeObject<UserSessionStorage>(data);
                    }
                    if (storage == null || string.IsNullOrEmpty(storage.Username))
                    {
                        storage = CreateStorage();
                        data = JsonConvert.SerializeObject(storage);
                        context.Session.SetString(Keys.UserSession, data);
                    }
                }
            }
        }


        internal void CopyValue()
        {
            EnsureStorage();
            if (storage.SessionId > 0)
            {
                user.SessionId = storage.SessionId;
            }
            user.Email = storage.Email;
            user.Mobile = storage.Mobile;
            user.Username = storage.Username;
            user.Title = storage.Title;
            user.Name = storage.Name;
            user.Password = storage.Password;
        }

        internal void Refresh()
        {
            if (context.Session.Get(Keys.UserSession) != null)
            {
                context.Session.Remove(Keys.UserSession);
            }
            storage = null;
        }


    }

    public class UserSessionStorage
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头衔
        /// </summary>
        public string Title { get; set; }
    }
}
