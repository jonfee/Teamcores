namespace TeamCores.Web.ViewModel.Upload
{
	/// <summary>
	/// 文件上传视图模型
	/// </summary>
	public class UploadViewModel
    {
		/// <summary>
		/// 存储的相对目录路径
		/// </summary>
		public string SavePath { get; set; }

		/// <summary>
		/// 是否固定存储在指定路径的根目录下，为false时在savepath目录后添加日期目录
		/// </summary>
		public bool FixedPath { get; set; }

		/// <summary>
		/// 指定上传后的文件名，为空时使用随机名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 文件上传后的扩展名（如：jpg，为空时表示与源文件一致）
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// 文件存在时是否覆盖
		/// </summary>
		public bool BeOverride { get; set; }

		/// <summary>
		/// 超过该大小时进行质量压缩（单位：KB），该参数只对图片上传有效，图片超出限制宽高时失效
		/// </summary>
		public long CompressIfGreaterSize { get; set; }

		/// <summary>
		/// 最大宽，该参数只对图片上传有效
		/// </summary>
		public int MaxWidth { get; set; }

		/// <summary>
		/// 最大高，该参数只对图片上传有效
		/// </summary>
		public int MaxHeight { get; set; }

		/// <summary>
		/// 超过最大宽高时是否裁剪（为false时不足部分留白），该参数只对图片上传有效
		/// </summary>
		public bool CutIfOut { get; set; }
	}
}
