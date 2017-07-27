namespace TeamCores.Uploader
{
	/// <summary>
	/// 文件上传配置项
	/// </summary>
	public class UploadOptions
	{
		/// <summary>
		/// 文件存储的根目录
		/// </summary>
		public string SaveRoot { get; set; }

		/// <summary>
		/// 文件上传后的物理路径根
		/// </summary>
		public string PhysicalUploadRoot { get; set; }

		/// <summary>
		/// 限制最大允许上传的文件大小（单位：KB）
		/// </summary>
		public long LimitSize { get; set; }

		/// <summary>
		/// 默认压缩质量（L）
		/// </summary>
		public long EncoderQualityValue { get; set; }

		/// <summary>
		/// 默认压缩色彩（L）
		/// </summary>
		public long EncoderColorDepthValue { get; set; }

		/// <summary>
		/// 需要处理的图片文件扩展名（多个用“|”分隔）
		/// </summary>
		public string ImgExtensions { get; set; }
	}

	/// <summary>
	/// 配置类
	/// </summary>
	public class Configs
	{
		/// <summary>
		/// 文件上传配置项
		/// </summary>
		public static UploadOptions UploadOptions;
	}
}
