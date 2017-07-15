using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Common;
using TeamCores.Common.Exceptions;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models;

namespace TeamCores.Domain.Services
{
    public class UserService
    {
        public void AddUser(NewUser newUser)
        {
            //对象为null时，抛出业务异常
            if (newUser == null) throw new UserNullException(nameof(newUser), "新增的用户对象不能为NULL。");

            //校验领域对象是否存在错误的规则
            newUser.ThrowExceptionIfValidateFailure();

            //新用户仓储对象
            Users user = new Users
            {
                UserId = newUser.ID,
                Username = newUser.Username,
                Name = newUser.Name,
                Password = newUser.EncryptPassword,
                Title = newUser.Title,
                Email = newUser.Email,
                Mobile = newUser.Mobile,
                CreateTime = DateTime.Now,
                LastTime = DateTime.Now,
                LoginCount = 0
            };

            //初始化新用户的学习情况
            UserStudy study = new UserStudy
            {
                UserId = user.UserId,
                Answers = 0,
                Average = 0,
                ReadCount = 0,
                StudyPlans = 0,
                StudyTimes = 0,
                TestExams = 0
            };

            UsersAccessor.Add(user, study);
        }
    }
}
