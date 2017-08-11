namespace TeamCores.Models
{
	/// <summary>
	/// 考试用户信息
	/// </summary>
	public class UserSimpleInfo
    {
		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 用户姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 用户手机号
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// 用户电子邮箱
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// 用户头衔
		/// </summary>
		public string Title { get; set; }
	}
}
