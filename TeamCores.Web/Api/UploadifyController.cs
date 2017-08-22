using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Utilities;
using TeamCores.Misc.Filters;
using TeamCores.Uploader;
using TeamCores.Web.ViewModel.Upload;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Api
{
	[Route("api/uploadify")]
	[UserAuthorization]
    public class UploadifyController : Controller
    {
        /// <summary>
        /// Base64文件内容检测正则
        /// </summary>
        private static Regex _regexBase64 = new Regex(@"^(?:data:[^;]+;base64,)(?<content>.+)$", RegexOptions.IgnoreCase);

		/// <summary>
		/// Form表单上传文件，支持多文件多方式（如：批量上传且file+base64混合上传）
		/// </summary>
		/// <param name="model">文件上传视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("form")]
        public IActionResult Upload(UploadViewModel model)
        {
            List<UploadResult> result = new List<UploadResult>();

            #region Form.Files 文件

            IFormFileCollection files = HttpContext.Request.Form.Files;

            if (null != files && files.Count > 0)
            {
				FormFileUploadViewModel fileModel = new FormFileUploadViewModel();

                foreach (IFormFile file in files)
                {
					model.CopyTo(fileModel);

					fileModel.File = file;

					var itemResult = UploadItemByFormFile(fileModel);

                    result.Add(itemResult);
                }
            }

            #endregion

            #region Form.Base64 文件

            Dictionary<string, string> base64Dictionary = new Dictionary<string, string>();

            var formCollection = Request.Form;

            foreach (var fc in formCollection)
            {
                if (_regexBase64.IsMatch(fc.Value))
                {
                    base64Dictionary.Add(fc.Key, fc.Value);
                }
            }

			if (null != base64Dictionary && base64Dictionary.Count > 0)
			{
				Base64UploadViewModel base64Model = null;

				foreach (var file in base64Dictionary)
				{
					model.CopyTo(base64Model);
					base64Model.Data = file.Value;
					base64Model.FieldName = file.Key;

					var itemResult = UploadItemByBase64(base64Model);

					result.Add(itemResult);
				}
			}

            #endregion

            return Json(result);
        }

		/// <summary>
		/// 上传文件
		/// </summary>
		/// <param name="model">Base64文件上传视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("base64")]
		public IActionResult UploadBase64(Base64UploadViewModel model)
        {
            var result = UploadItemByBase64(model);

            return Json(result);
        }

		/// <summary>
		/// 上传Base64图片文件
		/// </summary>
		/// <param name="model">Base64文件上传视图模型</param>
		/// <returns></returns>
		private UploadResult UploadItemByBase64(Base64UploadViewModel model)
        {
            UploadResult result = new UploadResult();
            
            Match m = _regexBase64.Match(model.Data ?? string.Empty);

            if (m.Success)
            {
                string content = m.Groups["content"].Value;

                //文件内容长度
                long fileLength = 0;

                try
                {
                    byte[] arr = Convert.FromBase64String(content);

                    fileLength = arr.Length;

                    Image image;

                    using (MemoryStream ms = new MemoryStream(arr))
                    {
                        image = new Bitmap(ms);
                    }

                    result = image.Save(
						fileLength, 
						model.SavePath, 
						model.FixedPath, 
						model.Name, 
						model.Extension.GetImageFormat(), 
						model.BeOverride, 
						model.CompressIfGreaterSize, 
						model.MaxWidth, 
						model.MaxHeight, 
						model.CutIfOut);
                }
                catch
                {
                    result.Message = "文件上传失败";
                }
            }
            else
            {
                result.Message = "不是有效的Base64图片内容";
            }
            //标志字段名
            result.FieldName = model.FieldName ?? string.Empty;

            return result;
        }

		/// <summary>
		/// 上传文件
		/// </summary>
		/// <param name="model">FormFile文件上传视图模型</param>
		/// <returns></returns>
		private UploadResult UploadItemByFormFile(FormFileUploadViewModel model)
        {
            UploadResult result = new UploadResult();

            try
            {
                if (model.File == null)
                {
                    result.Message = "文件对象为空";
                }
                else
                {
                    //是否为图片文件
                    bool isImage = model.File.IsImage();

                    if (isImage)
                    {
                        Image image = Image.FromStream(model.File.OpenReadStream(), true, true);

                        result = image.Save(
							model.File.Length, 
							model.SavePath, 
							model.FixedPath, 
							model.Name, 
							model.Extension.GetImageFormat(), 
							model.BeOverride, 
							model.CompressIfGreaterSize, 
							model.MaxWidth, 
							model.MaxHeight, 
							model.CutIfOut);
                    }
                    else
                    {
                        result = model.File.Save(model.SavePath, model.FixedPath, model.Name, model.BeOverride) ?? new UploadResult(); ;
                    }

                    if (result == null) result = new UploadResult { Message = "上传失败" };

                    //标识字段名
                    result.FieldName = model.File.GetFieldName();
                }
            }
            catch
            {
                result.Message = "文件上传失败";
            }

            return result;
        }
    }
}
