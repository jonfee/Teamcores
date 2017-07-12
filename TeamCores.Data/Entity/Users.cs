using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    [Table("Users")]
    public class Users
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Column(TypeName = "nvarchar(250)")]
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(TypeName = "varchar(32)")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头衔
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime LastTime { get; set; }

    }
}
