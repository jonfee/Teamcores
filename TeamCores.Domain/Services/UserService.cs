using System.Collections.Generic;
using TeamCores.Data.Caching;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.User;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
    /// <summary>
    /// 用户相关服务
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 新增用户，同时初始化用户的学习情况数据
        /// </summary>
        /// <param name="newUser"></param>
        public bool AddUser(NewUserRequest request)
        {
            NewUser newUser = new NewUser(request);

            return newUser.Save();
        }

        /// <summary>
        /// 初始化超级用户
        /// </summary>
        /// <returns></returns>
        public bool InitSuperUser(NewUserRequest request)
        {
            bool success = false;

            var super = UsersAccessor.GetSuperUser();

            string[] codes = PermissionCache.Instance.GetCodeArray();

            if (super != null)
            {
                UserManage manage = new UserManage(super);
                success = manage.ModifyTo(new UserModifyState
                {
                    Email = request.Email,
                    Mobile = request.Mobile,
                    Name = request.Name,
                    Title = request.Title,
                    UserName = request.Username,
                    Password = request.Password,
                    Permissions = codes
                });
            }
            else
            {
                request.Permissions = codes;
                NewUser newUser = new NewUser(request, true);
                success = newUser.Save();
            }

            return success;
        }

        /// <summary>
        /// 尝试登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users TrySignin(string account, string password)
        {
            UserSign sign = new UserSign(account, password);

            bool success = sign.TrySign();

            if (success)
            {
                return sign.User;
            }

            return null;
        }

        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldWord"></param>
        /// <param name="newWord"></param>
        /// <returns></returns>
        public bool ModifyPassword(long userId, string oldWord, string newWord)
        {
            UserManage user = new UserManage(userId);

            return user.ModifyPassword(oldWord, newWord);
        }

        /// <summary>
        /// 重置用户登录密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newWord"></param>
        /// <returns></returns>
        public bool ResetPassword(long userId, string newWord)
        {
            UserManage user = new UserManage(userId);

            return user.ResetPassword(newWord);
        }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <param name="title">头衔</param>
        /// <param name="name">姓名</param>
        /// <param name="permissions">权限集合</param>
        /// <returns></returns>
        public bool ModifyFor(long userId, string userName, string email, string mobile, string title, string name, string[] permissions)
        {
            UserManage user = new UserManage(userId);

            UserModifyState state = new UserModifyState
            {
                Email = email,
                Mobile = mobile,
                Name = name,
                Title = title,
                UserName = userName,
                Permissions = permissions,
                IsModifyBySelf = false
            };

            return user.ModifyTo(state);
        }

        /// <summary>
        /// 修改本人资料
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <param name="title">头衔</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public bool ModifySelfFor(long userId, string userName, string email, string mobile, string title, string name)
        {
            UserManage user = new UserManage(userId);

            UserModifyState state = new UserModifyState
            {
                Email = email,
                Mobile = mobile,
                Name = name,
                Title = title,
                UserName = userName,
                IsModifyBySelf = true
            };

            return user.ModifyTo(state);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetUserAccount(long userId)
        {
            return new UserManage(userId).UserInfo;
        }

        /// <summary>
        /// 设置用户状态为启用
        /// </summary>
        /// <param name="userId"></param>
        public bool SetEnabled(long userId)
        {
            UserManage user = new UserManage(userId);

            return user.SetEnabled();
        }

        /// <summary>
        /// 设置用户状态为禁用
        /// </summary>
        /// <param name="userId"></param>
        public bool SetDisabled(long userId)
        {
            UserManage user = new UserManage(userId);

            return user.SetDisabled();
        }

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="keyword"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public PagerModel<Users> Search(int pageSize, int pageIndex, string keyword, int? status)
        {
            UserSearch search = new UserSearch(pageIndex, pageSize, keyword, status);

            return search.Search();
        }

        /// <summary>
        /// 获取指定用户的简要数据集合
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<UserSimpleInfo> GetSimpleUsers(int? status = null)
        {
            return UsersAccessor.GetSimpleUsers(status);
        }

        /// <summary>
        /// 统计用户相关信息数据报表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserStatisticalReports GetUserStatisticalReports(long userId)
        {
            var user = new UserManage(userId);

            var reports = new UserStatisticalReports();
            reports.Exam = user.StatisticsForExams();
            reports.Message = user.StatisticsForMessage();
            reports.StudyPlan = user.StatisticsForStudyPlan();

            return reports;
        }
    }
}
