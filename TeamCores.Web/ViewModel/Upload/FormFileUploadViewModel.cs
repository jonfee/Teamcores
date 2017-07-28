using Microsoft.AspNetCore.Http;

namespace TeamCores.Web.ViewModel.Upload
{
	public class FormFileUploadViewModel : UploadViewModel
	{
		/// <summary>
		/// 表单上待上传的File
		/// </summary>
		public IFormFile File { get; set; }
	}
}
