using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamCores.Web.ViewModel
{
    public class LoginViewModel : IValidatableObject
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Username))
            {
                errors.Add(new ValidationResult("登入账号必填", new[] { "Error", "Username" }));
                return errors;
            }
            if (string.IsNullOrEmpty(Password))
            {
                errors.Add(new ValidationResult("密码不能为空", new[] { "Error", "Password" }));
                return errors;
            }
            if (Password.Length < 6)
            {
                errors.Add(new ValidationResult("密码长度最小6位", new[] { "Error", "Password" }));

                return errors;
            }
            return errors;
        }
    }
}
