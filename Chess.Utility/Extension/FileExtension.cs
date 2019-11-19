using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chess.Utility
{
    /// <summary>
    /// 文件扩展类
    /// </summary>
    public static class FileExtension
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="catelog">相对目录</param>
        /// <returns></returns>
        public static ExtResult SavePath(this HttpPostedFileBase file, out string fullpath, string filename = null, string catelog = "catelog")
        {
            fullpath = "";
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "保存成功!"
            };
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "文件为空!";
                    return extResult;
                }
                List<string> filter_exts = null;
                var file_exts = ConfigHelper.GetAppSetting(ConfigConst.FILEEXT);
                if (!string.IsNullOrEmpty(file_exts))
                {
                    filter_exts = file_exts.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToList();
                }
                var file_split = file.FileName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (file_split.Count() > 1)
                {
                    if (filter_exts != null && filter_exts.Any() && !filter_exts.Contains(file_split[file_split.Length - 1].ToLower()))
                    {
                        extResult.IsSuccess = false;
                        extResult.Message = "不支持的文件格式!";
                        return extResult;
                    }
                    catelog = catelog ?? "catelog";
                    var path = ConfigHelper.GetAppSetting(ConfigConst.SAVEPATH);
                    var save_path = string.IsNullOrEmpty(path) ? $"//upload//{catelog}" : string.Format(path, catelog);
                    var physics_path = HttpContext.Current.Server.MapPath(save_path);
                    DirectoryInfo directory = new DirectoryInfo(physics_path);
                    if (!directory.Exists)
                        Directory.CreateDirectory(physics_path);
                    string new_file_name = $"{filename ?? DateTime.Now.ToString("yyyyMMddHHmmss")}_.{file_split[file_split.Length - 1]}";
                    string new_full_name = string.Format("{0}//{1}", physics_path, new_file_name);
                    file.SaveAs(new_full_name);
                    //byte[] pay_bytes = new byte[file.ContentLength];
                    //file.InputStream.Read(pay_bytes, 0, pay_bytes.Length);
                    fullpath = string.Format("{0}//{1}", save_path, new_file_name);
                    //extResult.Data = pay_bytes;
                }
                else
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "未知格式的文件!";
                }
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return extResult;
        }
    }
}
