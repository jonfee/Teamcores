using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
	[Table("Permission")]
	public class Permission
	{
		/// <summary>
		/// 权限ID
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long Id { get; set; }

		/// <summary>
		/// 权限代号，如：A01
		/// </summary>
		[Column(TypeName = "char(3)")]
		public string Code { get; set; }

		/// <summary>
		/// 权限名称，如：查看用户
		/// </summary>
		[Column(TypeName = "nvarchar(20)")]
		public string Name { get; set; }

		/// <summary>
		/// 模块名称，如：用户管理
		/// </summary>
		[Column(TypeName = "nvarchar(20)")]
		public string Module { get; set; }
	}
}
