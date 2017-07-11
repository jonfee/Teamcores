using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        /// 用户名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(TypeName = "varchar(32)")]
        [Required]
        public string Password { get; set; }

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
