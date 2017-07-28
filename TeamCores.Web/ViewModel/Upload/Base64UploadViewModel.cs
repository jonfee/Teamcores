namespace TeamCores.Web.ViewModel.Upload
{
	public class Base64UploadViewModel : UploadViewModel
	{
		/// <summary>
		/// Base64文件数据
		/// </summary>
		public string Data { get; set; }

		/// <summary>
		/// 文件标志字段名称（一般用于单文件上传时，由客户端提供，上传后原值返回，以达到标识当前上传的文件）
		/// </summary>
		public string FieldName { get; set; }
	}
}
