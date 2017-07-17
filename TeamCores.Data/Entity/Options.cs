using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    [Table("Options")]
    public class Options
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long OptionId { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// 配置内容
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Value { get; set; }
    }
}
