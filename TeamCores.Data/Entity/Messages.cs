using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 消息
    /// </summary>
    [Table("Messages")]
    public class Messages
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MessageId { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public long Sender { get; set; }

        /// <summary>
        /// 接受人
        /// </summary>
        public long Receiver { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public bool Readed { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? ReadTime { get; set; }
    }
}
