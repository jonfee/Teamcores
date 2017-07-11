using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 消息
    /// </summary>
    [Table("Messages")]
    public class Messages
    {
        public long MessageId { get; set; }
    }
}
