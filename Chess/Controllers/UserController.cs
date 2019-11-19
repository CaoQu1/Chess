using AutoMapper;
using Chess.Domain;
using Chess.DtoModel;
using Chess.Model;
using Chess.Service;
using Chess.Utility;
using Chess.Web.Framework;
using Chess.Web.Framework.Attibute;
using Chess.Web.Framework.Controllers;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Chess.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : BaseController
    {
        #region Obsolute Interface
        /// http://teahouse.mxiong.com.cn/Handler.ashx?action=routegetauth2&state=score

        //        http://www.mxiong.com.cn/WS/MobileInterface.ashx?action=CheckTeahouseLogon&agentaccount=30002&agentpwd=123456&token=sdfdfj
        //int tid = GameRequest.GetQueryInt("agentaccount", -1);         // 代理账号（目前对应的就是茶馆编号）
        //string pwd = GameRequest.GetQueryString("agentpwd");       // 代理密码
        //string token = GameRequest.GetQueryString("token");               // token（暂时可以为任意值）

        //http://www.mxiong.com.cn/WS/MobileInterface.ashx?action=CheckTeahouseClub&clubid=8021&agentaccount=30002&agentpwd=123456&token=sdfdfj
        //int clubid = GameRequest.GetQueryInt("clubid", -1);        // 俱乐部编号（目前对应的就是茶馆编号）
        //int tid = GameRequest.GetQueryInt("agentaccount", -1);     // 代理账号（目前对应的就是茶馆编号）
        //string pwd = GameRequest.GetQueryString("agentpwd");       // 代理密码
        //string token = GameRequest.GetQueryString("token");        // token（暂时可以为任意值）

        //http://www.mxiong.com.cn/WS/MobileInterface.ashx?action=GetClubMember&clubid=8021&agentaccount=30002&agentpwd=123456&token=sdfdfj
        //int clubid = GameRequest.GetQueryInt("clubid", -1);        // 俱乐部编号（目前对应的就是茶馆编号）
        //int tid = GameRequest.GetQueryInt("agentaccount", -1);     // 代理账号（目前对应的就是茶馆编号）
        //string pwd = GameRequest.GetQueryString("agentpwd");       // 代理密码
        //string token = GameRequest.GetQueryString("token");        // token（暂时可以为任意值）

        //http://www.mxiong.com.cn/WS/MobileInterface.ashx?action=GetPlayScore&clubid=8021&roomid=0408057&gameid=103248&token=sdfdfj
        //int clubid = GameRequest.GetQueryInt("clubid", -1);        // 俱乐部编号（目前对应的就是茶馆编号）
        //int roomid = GameRequest.GetQueryInt("roomid", -1);           // 房间编号
        //int userid = GameRequest.GetQueryInt("gameid", 0);         // 用户游戏id
        //string token = GameRequest.GetQueryString("token");        // token（暂时可以为任意值）
        #endregion

        #region Property
        /// <summary>
        /// 用户服务
        /// </summary>
        public ISystemUserService SystemUserService { get; set; }

        /// <summary>
        /// 缓存服务
        /// </summary>
        public IMemoryCacheManager CacheManager { get; set; }

        /// <summary>
        /// 俱乐部服务
        /// </summary>
        public IClubService ClubService { get; set; }

        /// <summary>
        /// 平台服务
        /// </summary>
        public IPlatformService PlatformService { get; set; }

        /// <summary>
        /// web上下文服务
        /// </summary>
        public IWebContext WebContext { get; set; }

        /// <summary>
        /// 加密服务
        /// </summary>
        public IEncryptionService EncryptionService { get; set; }
        #endregion

        #region View

        /// <summary>
        /// 微信注册
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register(string returnUrl = null)
        {
            returnUrl = HttpUtility.UrlEncode(returnUrl);
            LogHelper.Info($"UserController=>Register>>>>>>>,UrlEncode url=>{returnUrl}");
            string State = Guid.NewGuid().ToString();
            Session["State"] = State;
            var return_url = string.Format(Config.SenparcWeixinSetting.AgentUrl, "GroupRegister");
            #region obsolute
            //var url = OAuthApi.GetAuthorizeUrl(Config.SenparcWeixinSetting.WeixinAppId, string.Format(Config.SenparcWeixinSetting.AgentUrl, returnUrl), State, Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);
            //var pairs = new KeyValuePair<string, string>[] {
            //   new KeyValuePair<string, string>("action","routegetauth2"),u
            //   new KeyValuePair<string, string>("state",ConfigHelper.WebServerDamain)
            //  };
            //var resultJson = NetWorkHelper.HttpGetDown<ResultJson<Auth2Result>>(ConfigHelper.GetAppSetting(ConfigConst.AUTH2), pairs);
            //if (resultJson.data.valid)
            //{
            //    return RedirectToAction("callback", "user", new { code = resultJson.data.code, returnUrl = return_url, state = State });
            //}
            //return Redirect(url);
            //return Content("授权失败!");
            Session["url"] = returnUrl;
            LogHelper.Info($"UserController=>Register>>>>>>>,return_url=>{return_url}, session_url=>{Session["url"]}");

            #endregion
            LogHelper.Info($"UserController=>Register>>>>>>>,Redirect to {ConfigHelper.GetAppSetting(ConfigConst.AUTH2)}/Handler.ashx?action=routegetauth2&state={HttpUtility.UrlEncode(return_url)}");
            return Redirect($"{ConfigHelper.GetAppSetting(ConfigConst.AUTH2)}/Handler.ashx?action=routegetauth2&state={HttpUtility.UrlEncode(return_url)}");

            //return RedirectToAction("ThridCallback", new { userid = "702347", gameid = "702557", returnUrl = "GroupRegister" });

        }

        /// <summary>
        /// 三方回调
        /// </summary>
        /// <param name="resultJson"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult ThridCallback(string userid, string gameid, string returnUrl = null)
        {
            try
            {
                returnUrl = Session["url"] != null ? Session["url"].ToString() : returnUrl;
                LogHelper.Info($"UserController=>ThridCallback>>>>>>>,Session[Url]=>{Session["url"]}");
                LogHelper.Info($"UserController=>ThridCallback>>>>>>>,returnUrl=>{returnUrl}");
                if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(gameid))
                {
                    int user_id = 0, game_id = 0;
                    if (int.TryParse(userid, out user_id) && int.TryParse(gameid, out game_id))
                    {
                        LogHelper.Info($"UserController=>ThridCallback>>>>>>>,userid=>{userid},gameid=>{gameid}");
                        var systemUser = this.SystemUserService.GetByWhere(x => x.UserId == user_id && x.GameId == game_id).FirstOrDefault() ?? new SystemUser();
                        systemUser.GameId = game_id;
                        systemUser.UserId = user_id;
                        systemUser.Sex = EnumSex.UnKown;
                        LogHelper.Info($"UserController=>ThridCallback>>>>>>>systemUser.Id={systemUser.Id}");
                        if (systemUser.Id > 0)
                        {
                            systemUser.UpdateTime = DateTime.Now;
                            this.SystemUserService.Update(systemUser);
                            LogHelper.Info($"UserController=>ThridCallback>>>>>>>systemUser.UserIdentity={systemUser.UserIdentity}");
                            switch (systemUser.UserIdentity)
                            {
                                case EnumRole.GroupOwner:
                                    var clubs = this.ClubService.GetByWhere(x => x.UserId == systemUser.UserId && x.SystemUserId == systemUser.Id);
                                    //returnUrl = string.IsNullOrEmpty(returnUrl) ? (clubs != null && clubs.Any() ? "GetClubs" : "AdminMange") : returnUrl;
                                    //只要是馆主就直接到列表或者添加俱乐部页面
                                    returnUrl = (clubs != null && clubs.Any() ? "GetClubs" : "AdminMange") ;
                                    break;
                                case EnumRole.Member:
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            systemUser.CreateTime = DateTime.Now;
                            this.SystemUserService.Insert(systemUser);
                        }
                        LogHelper.Info($"UserController=>ThridCallback>>>>>>>,systemUser.UserId=>{systemUser.UserId},returnUrl=>{returnUrl}");
                        this.WebContext.CurrentUser = systemUser;   //认证通过，置当前用户Session
                        return Redirect(HttpUtility.UrlDecode(returnUrl));
                    }
                    else
                    {
                        LogHelper.Error("UserController=>ThridCallback>>>>>>>,回调参数有误!");
                        return Content("回调参数有误!");
                    }
                }
                LogHelper.Error("UserController=>ThridCallback>>>>>>>,授权失败!");
                return Content("授权失败!");
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController=>ThridCallback>>>>>>>,解析失败!" + ex.Message);
                LogHelper.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 微信回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Callback(string code, string state = null, string returnUrl = null)
        {
            LogHelper.Info("Callback入参:" + returnUrl + "");
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return Content("取消授权");
                }
                if (state != null && !Session["State"].ToString().Equals(state))
                {
                    return Content("请从正规入口进入");
                }
                var accessTokenResult = OAuthApi.GetAccessToken(Config.SenparcWeixinSetting.WeixinAppId, Config.SenparcWeixinSetting.WeixinAppSecret, code);
                if (accessTokenResult.errcode != ReturnCode.请求成功)
                {
                    return Content(accessTokenResult.errmsg);
                }
                LogHelper.Info($"access_token=>{accessTokenResult.access_token},openid=>{accessTokenResult.openid}");
                var userinfo = OAuthApi.GetUserInfo(accessTokenResult.access_token, accessTokenResult.openid);
                if (userinfo == null)
                {
                    return Content("未获取到用户信息");
                }
                LogHelper.Info($"userinfo.unionid=>{userinfo.unionid},userinfo.NickName=>{userinfo.nickname}");
                var systemUser = new SystemUser
                {
                    Openid = userinfo.openid,
                    Unionid = userinfo.unionid,
                    HeadImgUrl = userinfo.headimgurl,
                    NickName = userinfo.nickname,
                    Sex = (EnumSex)userinfo.sex,
                    UserIdentity = EnumRole.GroupOwner,
                    CreateTime = DateTime.Now
                };
                WebContext.CurrentUser = systemUser;
                this.SystemUserService.Insert(systemUser);
                return Redirect(returnUrl ?? "/user/groupregister");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 群主注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GroupRegister()
        {
            LogHelper.Info($"UserController=>GroupRegister>>>>>>>,RUN TO GroupRegister");
            var current_user = this.WebContext.CurrentUser;
            if (current_user != null && !string.IsNullOrEmpty(current_user.AgentId))
            {
                var clubs = this.ClubService.GetByWhere(x => x.UserId == current_user.UserId && x.SystemUserId == current_user.Id && x.PlatformId == current_user.PlatformId).ToList();
                if (clubs == null || clubs.Count == 0)
                {
                    LogHelper.Info($"UserController=>GroupRegister>>>>>>>,AdminMange");
                    return Redirect("AdminMange");
                }
                else
                {
                    LogHelper.Info($"UserController=>GroupRegister>>>>>>>,Redirect to GetClubs");
                    return Redirect("GetClubs");
                }
            }
            LogHelper.Info($"UserController=>GroupRegister>>>>>>>,platforms list");
            var platforms = this.PlatformService.GetAllList().ToList();
            ViewBag.Platforms = platforms;
            return View(new RegisterParameter());
        }

        /// <summary>
        /// 管理主界面
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public ActionResult AdminMange(int? clubid = null)
        {
            LogHelper.Info($"UserController=>AdminMange>>>>>>>,RUN TO AdminMange");
            try
            {
                var current_user = this.WebContext.CurrentUser;
                //var tinyUrl = Provider.Tinyurl(current_user.Id);
                //ViewBag.Tiny = tinyUrl;
                ViewBag.ClubId = clubid;
                ViewBag.User = current_user;
                ViewBag.Web = ConfigHelper.WebServerDamain;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return View();
        }

        /// <summary>
        /// 俱乐部列表  
        /// </summary>
        /// <returns></returns>
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public ActionResult GetClubs()
        {
            LogHelper.Info($"UserController=>GetClubs>>>>>>>,RUN TO GetClubs");
            var current_user = this.WebContext.CurrentUser;
            var clubs = this.ClubService.GetByWhere(x => x.UserId == current_user.UserId && x.SystemUserId == current_user.Id && x.PlatformId == current_user.PlatformId).ToList();
            if (clubs == null || clubs.Count == 0)
            {
                return Redirect("AdminMange");
            }
            ViewBag.Clubs = Mapper.Map<List<Club>, List<ClubViewModel>>(clubs);
            return View();
        }

        #endregion

        #region Ajax

        /// <summary>
        /// 群主注册
        /// </summary>
        /// <param name="registerParameter"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GroupRegister(RegisterParameter registerParameter)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "注册成功!"
            };
            try
            {
                string AgentPassWordMd5 = this.EncryptionService.MD5Hash(registerParameter.AgentPassWord);
                var curent_user = this.WebContext.CurrentUser;
                if (curent_user == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请先登录!";
                    return Json(extResult);
                }

                LogHelper.Info($"UserController=>GroupRegister>>>>>>>,current_user.Id=>{curent_user.Id}");

                var platfrom = this.PlatformService.GetById(registerParameter.PlatformId);
                if (platfrom == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "平台不存在!";
                    return Json(extResult);
                }
                var pairs = new KeyValuePair<string, string>[] {
               new KeyValuePair<string, string>("action","CheckTeahouseLogon"),
               new KeyValuePair<string, string>("agentaccount",registerParameter.AgentId),
               new KeyValuePair<string, string>("agentpwd",AgentPassWordMd5),
               new KeyValuePair<string, string>("token",ConfigHelper.Token),
              };
                var resultJson = NetWorkHelper.HttpGetDown<ResultJson<BaseResult>>(ConfigHelper.GetAppSetting(ConfigConst.APIURL), pairs);
                if (resultJson.data != null && resultJson.data.result == (int)EnumSuccess.Success)
                {
                    var user = this.SystemUserService.GetById(curent_user.Id);
                    var agents = this.SystemUserService.GetAllList().Where(x => x.AgentId == registerParameter.AgentId && x.AgentPassWord == AgentPassWordMd5);
                    if (agents != null && agents.Any())
                    {
                        extResult.IsSuccess = false;
                        extResult.Message = "该账号已绑定其他微信!";
                        return Json(extResult);
                    }
                    if (user != null)
                    {
                        user.AgentId = registerParameter.AgentId;
                        user.AgentPassWord = AgentPassWordMd5;
                        user.CheckKey = "";
                        user.UpdateTime = DateTime.Now;
                        user.UserIdentity = EnumRole.GroupOwner;
                        user.PlatformId = platfrom.Id;
                        this.SystemUserService.Update(user);
                        this.WebContext.CurrentUser = user;
                    }
                }
                else
                {
                    extResult.IsSuccess = false;
                    extResult.Message = string.IsNullOrEmpty(resultJson.msg) ? "注册失败（代理账号或密码错误）" : resultJson.msg;
                }
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
                LogHelper.Error(ex);
            }
            return Json(extResult);
        }

        /// <summary>
        /// 查询俱乐部信息
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public JsonResult QueryClub(string clubId)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "获取俱乐部信息成功!"
            };
            LogHelper.Info($"UserController=>QueryClub>>>>>>> Start query club");
            try
            {
                int club_Id = 0;
                if (string.IsNullOrEmpty(clubId) || !int.TryParse(clubId, out club_Id))
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请输入俱乐部编号!";
                    return Json(extResult, JsonRequestBehavior.AllowGet);
                }
                var current_user = this.WebContext.CurrentUser;
                if (current_user == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请先登录!";
                    return Json(extResult, JsonRequestBehavior.AllowGet);
                }
                //TODO 这里可能还要加上平台参数，这样才能多平台使用

                var clubBind = this.ClubService.GetByWhere(x => x.ClubId == club_Id);
                if (clubBind.Where(x => x.SystemUserId.Value != current_user.Id).FirstOrDefault() != null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "该俱乐部已被绑定!";
                    return Json(extResult, JsonRequestBehavior.AllowGet);
                }
                var clubInfo = clubBind.Where(x => x.SystemUserId.HasValue && x.SystemUserId.Value == current_user.Id).FirstOrDefault();
                //if (clubInfo == null)
                //{
                LogHelper.Info($"UserController=>QueryClub,>>>>>>> 本地不存在的俱乐部，向远端请求!");
                var pairs = new KeyValuePair<string, string>[]
                    {
                          new KeyValuePair<string, string>("action","GetClubMember"),
                          new KeyValuePair<string, string>("clubid",clubId),
                          new KeyValuePair<string, string>("agentaccount",current_user.AgentId),
                          new KeyValuePair<string, string>("agentpwd",current_user.AgentPassWord),
                          new KeyValuePair<string, string>("token",ConfigHelper.Token)
                    };
                var result = NetWorkHelper.HttpGetDown<ResultJson<ClubMemberResult>>(ConfigHelper.GetAppSetting(ConfigConst.APIURL), pairs);
                //TODO 这里要考虑result==null或result.data == null的情况

                if (result.data != null && result.data.result == (int)EnumSuccess.VeFail)
                {
                    LogHelper.Info($"UserController=>QueryClub,>>>>>>> 从远端获取俱乐部信息失败!");
                    extResult.IsSuccess = false;
                    extResult.Message = result.data.msg;
                    return Json(extResult, JsonRequestBehavior.AllowGet);
                }
                if (!string.IsNullOrEmpty(result.data.members))
                {
                    var members = result.data.members.Replace(@"\", "").DeserializeObject<ResultJson<dynamic>>();
                    LogHelper.Info($"UserController=>QueryClub,>>>>>>>members=" + members.data.ToString());
                    if (members.data != null)
                    {
                        string[] sArray = members.data.ToString().Split(']');
                        LogHelper.Info($"UserController=>QueryClub,>>>>>>>sArray.Length=" + sArray.Length);
                        Club club = null;
                        using (var trans = new TransactionScope())
                        {
                            try
                            {
                                bool club_success = false, user_success = false;
                                for (int i = 0; i < sArray.Length - 1; i++)
                                {
                                    int a = sArray[i].IndexOf("[");
                                    LogHelper.Info($"UserController=>QueryClub,>>>>>>>sArray[i]=" + sArray[i]);
                                    LogHelper.Info($"UserController=>QueryClub,>>>>>>>a=" + a);
                                    string player = sArray[i].Substring(a + 1);
                                    string[] play = player.Split(',');
                                    LogHelper.Info($"UserController=>QueryClub,>>>>>>>player=" + player);
                                    LogHelper.Info($"UserController=>QueryClub,>>>>>>>play.Length=" + play.Length);
                                    user_success = false;
                                    //member_success = false;
                                    if (play.Length > 8)
                                    {
                                        #region 更新俱乐部信息
                                        int clubid = int.Parse(play[2] ?? "0");
                                        if (i == 0)
                                        {
                                            var club_pairs = new KeyValuePair<string, string>[] {
                                                      new KeyValuePair<string, string>("action","CheckTeahouseClub"),
                                                      new KeyValuePair<string, string>("clubid",clubId),
                                                      new KeyValuePair<string, string>("agentaccount",current_user.AgentId),
                                                      new KeyValuePair<string, string>("agentpwd",current_user.AgentPassWord),
                                                      new KeyValuePair<string, string>("token",ConfigHelper.Token),
                                                      };
                                            var resultJson = NetWorkHelper.HttpGetDown<ResultJson<BaseResult>>(ConfigHelper.GetAppSetting(ConfigConst.APIURL), club_pairs);
                                            if (resultJson.data.result != (int)EnumSuccess.Success)
                                            {
                                                extResult.IsSuccess = false;
                                                extResult.Message = resultJson.data.msg;
                                                return Json(extResult, JsonRequestBehavior.AllowGet);
                                            }
                                            this.ClubService.Delete(clubid);
                                            club_success = false;
                                            club = new Club();
                                            club.ClubId = clubid;
                                            club.ClubName = resultJson.data.name;
                                            club.CreateTime = DateTime.Now;
                                            club.UserId = current_user.UserId.Value;
                                            club.SystemUserId = current_user.Id;
                                            club.PlatformId = current_user.PlatformId.Value;
                                            club.PlatformName = current_user.Platform.PlatformName;
                                            club_success = this.ClubService.Insert(club) > 0;
                                        }
                                        #endregion

                                        LogHelper.Info($"UserController=>QueryClub,>>>>>>>play[1]=" + play[1]);
                                        LogHelper.Info($"UserController=>QueryClub,>>>>>>>play[0]=" + play[0]);
                                        int gameid = 0;
                                        int userid = 0;
                                        if (int.TryParse(play[1], out gameid) && int.TryParse(play[0], out userid))
                                        {
                                            #region 更新俱乐部用户信息
                                            var user = this.SystemUserService.GetByWhere(x => x.GameId == gameid && x.UserId == userid).FirstOrDefault() ?? new SystemUser();
                                            user.GameId = gameid;
                                            user.NickName = play[5] ?? "";
                                            user.HeadImgUrl = play[6] ?? "";
                                            user.Sex = (EnumSex)int.Parse(play[8] ?? "0");
                                            user.UserId = userid;
                                            LogHelper.Info($"UserController=>QueryClub,新增俱乐部>>>>>>>user.NickName=" + user.NickName);
                                            LogHelper.Info($"UserController=>QueryClub,新增俱乐部>>>>>>>user.Id=" + user.Id);
                                            if (user.Id > 0)
                                            {
                                                user.UpdateTime = DateTime.Now;
                                            }
                                            else
                                            {
                                                user.CreateTime = DateTime.Now;
                                            }
                                            user_success = user.Id > 0 ? this.SystemUserService.Update(user) > 0 : this.SystemUserService.Insert(user) > 0;
                                            #endregion

                                            #region 更新俱乐部成员信息
                                            ClubMember member = new ClubMember();
                                            member.UserId = userid;
                                            member.ClubId = clubid;
                                            member.IsClubManger = play[3] == "1";
                                            member.IsOwner = play[4] == "1";
                                            member.SystemUserId = user.Id;
                                            member.CreateTime = DateTime.Now;
                                            //LogHelper.Info($"UserController=>QueryClub,>>>>>>>member.UserId=" + member.UserId);
                                            if (club != null)
                                            {
                                                club.ClubMembers.Add(member);
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        LogHelper.Info($"原字符串=>{player},新字符串=>{string.Join(",", play)}");
                                    }
                                }//END-FOR
                                this.ClubService.Update(club);
                                LogHelper.Info($"UserController=>QueryClub,>>>>>>>Update club.Id=" + club.Id);
                                club = this.ClubService.GetByWhere(x => x.Id == club.Id).FirstOrDefault();
                                if (club_success && user_success)
                                {
                                    trans.Complete();
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error($"UserController=>QueryClub,>>>>>>>,ex=>" + ex.Message);

                                throw ex;
                            }
                        }
                        clubInfo = club;
                    }
                }
                else
                {
                    extResult.IsSuccess = false;
                    extResult.Message = result.data.msg;
                    return Json(extResult, JsonRequestBehavior.AllowGet);
                }
                //显示控制
                if (clubInfo != null)
                {
                    var clubviewModel = Mapper.Map<Club, ClubViewModel>(clubInfo);
                    var clubmembers = clubInfo.ClubMembers.ToList();
                    if (clubmembers != null && clubmembers.Any())
                    {
                        if (!clubmembers.Where(x => x.SystemUser != null).Any())
                        {
                            clubmembers.ForEach(x =>
                            {
                                x.SystemUser = this.SystemUserService.GetById(x.SystemUserId.Value);
                                //LogHelper.Info($"UserController=>QueryClub,>>>>>>>x.UserId=" + x.UserId);
                            });
                        }
                        clubviewModel.ClubMemberViews = Mapper.Map<List<ClubMember>, List<ClubMemberViewModel>>(clubmembers);
                        //LogHelper.Info($"UserController=>QueryClub,>>>>>>>clubviewModel.ClubMemberViews[0].UserId=" + clubviewModel.ClubMemberViews[0].UserId);
                        //LogHelper.Info($"UserController=>QueryClub,>>>>>>>clubviewModel.ClubMemberViews[0].GameId=" + clubviewModel.ClubMemberViews[0].GameId);
                    }
                    if (clubInfo.SystemUserId.HasValue)
                    {
                        var groupOwner = this.SystemUserService.GetById(clubInfo.SystemUserId.Value);
                        clubviewModel.PayImage = this.ClubService.GetById(int.Parse(clubId)).PayImage;
                    }
                    var shortUrl = ShortUrl(int.Parse(clubId));
                    clubviewModel.ShortUrl = shortUrl != null && shortUrl.Data != null ? shortUrl.Data.ToString() : "";
                    extResult.Data = clubviewModel;
                }
                else
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "未获取到数据!";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return new JsonResult
            {
                Data = extResult,
                MaxJsonLength = int.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 编辑俱乐部
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public JsonResult EditClub(ClubParameter clubParameter)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "编辑成功!"
            };
            try
            {
                var current_user = this.WebContext.CurrentUser;
                if (current_user == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请先登录!";
                    return Json(extResult);
                }
                var club = this.ClubService.GetByWhere(x => x.ClubId == clubParameter.clubId && x.UserId == current_user.UserId).FirstOrDefault();
                if (club == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "俱乐部不存在!";
                    return Json(extResult);
                }

                using (var trans = new TransactionScope())
                {
                    try
                    {
                        if (Request.Files != null && Request.Files.Count > 0 && Request.Files[0].ContentLength != 0)
                        {
                            string path = "";
                            extResult = Request.Files[0].SavePath(out path, (current_user.UserId ?? current_user.Id).ToString(), current_user.UserIdentity.ToString());
                            if (extResult.IsSuccess)
                            {
                                var groupUser = this.SystemUserService.GetById(current_user.Id);
                                if (groupUser != null)
                                {
                                    //groupUser.PayCodeImg = (byte[])extResult.Data;
                                    groupUser.PayCodeUrl = path;
                                    this.SystemUserService.Update(groupUser);
                                }
                            }
                        }
                        club.PayMoney = clubParameter.paymoney;
                        club.Rate = clubParameter.rate;
                        this.ClubService.Update(club);
                        if (club != null)
                        {
                            var clubviewModel = Mapper.Map<Club, ClubViewModel>(club);
                            var clubmembers = club.ClubMembers.ToList();
                            if (clubmembers != null && clubmembers.Any())
                            {
                                clubviewModel.ClubMemberViews = Mapper.Map<List<ClubMember>, List<ClubMemberViewModel>>(clubmembers);
                            }
                            if (club.SystemUserId.HasValue)
                            {
                                var groupOwner = this.SystemUserService.GetById(club.SystemUserId.Value);
                                //if (groupOwner.PayCodeImg != null && groupOwner.PayCodeImg.Count() > 0)
                                //{
                                //clubviewModel.PayImage = Convert.ToBase64String(groupOwner.PayCodeImg);
                                clubviewModel.PayImage = groupOwner.PayCodeUrl;
                                //}
                            }
                            extResult.Data = clubviewModel;
                        }
                        trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
                LogHelper.Error(ex);
            }
            stopwatch.Stop();
            LogHelper.Info($"更新耗时=>{stopwatch.ElapsedMilliseconds / 1000}s");
            return Json(extResult);
        }


        /// <summary>
        /// 上传二维码
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public JsonResult UploadQrCode(int userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "上传成功!"
            };
            LogHelper.Info($"UserController=>UploadQrCode,开始上传二维码!");
            try
            {
                var current_user = this.WebContext.CurrentUser;
                LogHelper.Info($"UserController=>UploadQrCode,current_user is null?");
                if (current_user == null)
                {
                    LogHelper.Error($"UserController=>UploadQrCode,请先登录!");
                    extResult.IsSuccess = false;
                    extResult.Message = "请先登录!";
                    return Json(extResult);
                }

                var user = this.SystemUserService.GetByWhere(x => x.UserId == userId).FirstOrDefault() ?? new SystemUser();
                LogHelper.Info($"UserController=>UploadQrCode,user.id="+ user.Id);
                if (user.Id > 0)
                {
                    user.UpdateTime = DateTime.Now;
                    this.SystemUserService.Update(user);
                }
                else
                {
                    //肯定是有问题的
                    LogHelper.Error($"UserController=>UploadQrCode,查询用户失败!");
                    extResult.IsSuccess = false;
                    extResult.Message = "查询用户失败!";
                    return Json(extResult);
                }
               
                using (var trans = new TransactionScope())
                {
                    try
                    {
                        if (Request.Files != null && Request.Files.Count > 0 && Request.Files[0].ContentLength != 0)
                        {
                            string path = "";
                            extResult = Request.Files[0].SavePath(out path, userId.ToString(),"0");
                            if (extResult.IsSuccess)
                            {
                                user.PayCodeUrl = path;
                                this.SystemUserService.Update(user);
                            }
                        }
                        else
                        {
                            LogHelper.Error($"UserController=>UploadQrCode,文件没上传!Request.Files is null?"+ Request.Files==null?"true":"false");
                            if(Request.Files != null)
                            {
                                LogHelper.Error($"UserController=>UploadQrCode,文件没上传!Request.Files.Count=" + Request.Files.Count);
                                if (Request.Files.Count > 0)
                                {
                                    LogHelper.Error($"UserController=>UploadQrCode,文件没上传!Request.Files[0].ContentLength=" + Request.Files[0].ContentLength);
                                }
                            }
                        }
                        trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error($"UserController=>UploadQrCode,写数据库异常!"+ ex.Message);
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
                LogHelper.Error($"UserController=>UploadQrCode,异常!" + ex.Message);
            }
            stopwatch.Stop();
            LogHelper.Info($"上传更新耗时=>{stopwatch.ElapsedMilliseconds / 1000}s");
            return Json(extResult);
        }

        /// <summary>
        /// 删除俱乐部
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public JsonResult DeleteClub(int clubId)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "编辑成功!"
            };
            try
            {
                var club = this.ClubService.GetByWhere(x => x.ClubId == clubId && x.SystemUserId == this.WebContext.CurrentUser.Id).FirstOrDefault();
                if (club == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "俱乐部不存在!";
                    return Json(extResult);
                }
                var flag = this.ClubService.Delete(clubId) > 0;
                extResult.IsSuccess = flag;
                extResult.Message = flag ? "删除成功!" : "删除失败!";
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return Json(extResult);
        }

        /// <summary>
        /// 编辑俱乐部成员
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public JsonResult EditClubMember(int clubId)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "编辑成功!"
            };
            try
            {
                var club = this.ClubService.GetById(clubId);
                if (club == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "俱乐部不存在!";
                    return Json(extResult);
                }
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return Json(extResult);
        }

        /// <summary>
        /// 生成短连接
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        [RoleAuthorize(Role = EnumRole.GroupOwner)]
        public JsonResult GenerateShortUrl(int clubId)
        {
            return Json(ShortUrl(clubId), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Util

        /// <summary>
        /// 生成短链接
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        private ExtResult ShortUrl(int clubId)
        {
            ExtResult extResult = new ExtResult
            {
                IsSuccess = true,
                Message = "生成成功!"
            };
            try
            {
                var club = this.ClubService.GetByWhere(x => x.ClubId == clubId && x.UserId == this.WebContext.CurrentUser.UserId).FirstOrDefault();
                if (club == null)
                {
                    extResult.IsSuccess = false;
                    extResult.Message = "请先查询俱乐部信息!";
                    return extResult;
                }
                //if (string.IsNullOrEmpty(club.ShortLink))
                //{
                var tinyUrl = Provider.Tinyurl(clubId);
                club.ShortLink = tinyUrl.url_short;
                this.ClubService.Update(club);
                //}
                extResult.Data = club.ShortLink;
            }
            catch (Exception ex)
            {
                extResult.IsSuccess = false;
                extResult.Message = ex.Message;
            }
            return extResult;
        }

        #endregion
    }
}