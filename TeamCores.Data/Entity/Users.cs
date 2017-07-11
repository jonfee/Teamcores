using System;
using System.Collections.Generic;
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
    }
}
