namespace TeamCores.Web.ViewModel.User
{
    public class UserAddViewModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 账户名
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile
        {
            get;
            set;
        }

        /// <summary>
        /// 明文密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 头衔
        /// </summary>
        public string Title
        {
            get;
            set;
        }
    }
}