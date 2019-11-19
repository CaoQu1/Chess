using Chess.Domain;
using Chess.DtoModel;
using Chess.Service;
using Chess.Utility;
using Chess.Web.Framework.Attibute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chess.Controllers
{
    /// <summary>
    /// 公共控制器
    /// </summary>
    public class CommonController : Controller
    {
        #region Property

        /// <summary>
        /// web上下文服务
        /// </summary>
        public IWebContext WebContext { get; set; }

        /// <summary>
        /// 用户服务
        /// </summary>
        public ISystemUserService SystemUserService { get; set; }

        #endregion

        #region View

        /// <summary>
        /// 异常页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HttpException(CustomException error)
        {
            return View(error);
        }

        /// <summary>
        /// 上传文件页面
        /// </summary>
        /// <returns></returns>
        //[ChildActionOnly]
        [HttpGet]
        public PartialViewResult _UploadAttachment(string ClubId = null, string RoomId = null)
        {
            TempData["ClubId"] = ClubId;
            TempData["RoomId"] = RoomId;
            return PartialView();
        }

        #endregion

        #region Ajax
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //#if !DEBUG
        //        [UserAuthorize]
        //#endif
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "上传成功!"
            };
            try
            {
                //#if !DEBUG
                var current_user = this.WebContext.CurrentUser;
                if (current_user == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请先登录!";
                    return Json(extResult);
                }

                var user = this.SystemUserService.GetById(current_user.Id);
                //#else
                //                var user = this.SystemUserService.GetAllList().FirstOrDefault();
                //#endif
                var path = "";
                extResult = file.SavePath(out path, (user.UserId ?? user.Id).ToString(), user.UserIdentity.ToString());
                if (extResult.IsSuccess)
                {
                    if (user != null)
                    {
                        //user.PayCodeImg = (byte[])extResult.Data;
                        user.PayCodeUrl = path;
                        this.SystemUserService.Update(user);
                    }
                }
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return RedirectToAction("GetRecordLog", "RecordLog", new { message = extResult.Message, ClubId = TempData["ClubId"], RoomId = TempData["RoomId"] });
        }
        #endregion
    }
}