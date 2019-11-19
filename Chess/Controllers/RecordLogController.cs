using Chess.Domain;
using Chess.DtoModel;
using Chess.DtoModel.ViewModel;
using Chess.Model;
using Chess.Service;
using Chess.Utility;
using Chess.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chess.Controllers
{
    /// <summary>
    /// 战绩记录控制器
    /// </summary>
    public class RecordLogController : BaseController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public ISystemUserService SystemUserService { get; set; }

        /// <summary>
        /// 俱乐部表服务
        /// </summary>
        public IClubService ClubServervice { get; set; }

        /// <summary>
        /// 战绩服务
        /// </summary>
        public IRecordLogService RecordLogService { get; set; }

        /// <summary>
        /// web上下文服务
        /// </summary>
        public IWebContext WebContext { get; set; }

        /// <summary>
        /// 战绩记录跳转页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult InputRecord()
        {
            return View();
        }

        /// <summary>
        ///   战绩查询页面
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult GetRecordLog(string clubId, string roomId, string message = null)
        {
            //记录入参日志

            LogHelper.Info("RecordLogController>>>>>>>,入参：clubId:" + clubId + ";roomId:" + roomId + ";message:" + message + "");
            ViewBag.Web = ConfigHelper.WebServerDamain;
            LogHelper.Info("RecordLogController>>>>>>>,ViewBag.Web=" + ViewBag.Web);

            if (!string.IsNullOrEmpty(clubId) && !string.IsNullOrEmpty(roomId))
            {
                clubId = clubId.Trim();
                roomId = roomId.Trim();
                //string[] recordLogArry = recordLogStr.Split(':');
                //验证此群是否已经注册（通过当前clubid 查询是否有此群的信息，若无，提示错误）
                //验证此人是否为本群成员

                //验证此玩家是否有付款码，若无
                //则页面打开时给出弹窗，上传成功才能继续操作

                //从粘贴板中获取json
                //int clubidSpace = recordLogArry[1].IndexOf(" ");
                //int gameidSpace = recordLogArry[2].IndexOf(" ");
                //int roomidSpace = recordLogArry[3].IndexOf(" ");
                //string clubid = recordLogArry[1].Substring(0, clubidSpace + 1).Trim();
                //string roomid = recordLogArry[2].Substring(0, gameidSpace).Trim();
                //string gameid = recordLogArry[3].Substring(0, roomidSpace).Trim();


                //string[] recordLogArry = recordLogStr.Split(',');
                //string clubid = recordLogArry[0];
                //string roomid = recordLogArry[1];
                //string gameid = recordLogArry[2];

                //玩家列表                                                
                List<PlayScoreViewModel> PlayerList = new List<PlayScoreViewModel>();
                //房间信息表
                RoomViewModel RoomMessage = new RoomViewModel();
                var cid = int.Parse(clubId);
                var club = this.ClubServervice.GetByWhere(x => x.ClubId == cid);
                if (club.Count() > 0)
                {
                    RoomMessage.PayMoney = club.First().PayMoney;
                    RoomMessage.Rate = club.First().Rate;
                    RoomMessage.PlatformId = club.First().PlatformId.Value;
                    int ownerId = club.First().SystemUserId.Value;
                    //RoomMessage.PayCodeUrl = this.SystemUserService.GetByWhere(x => x.Id == ownerId).Select(x => x.PayCodeUrl).FirstOrDefault();  //群主收款码
                    RoomMessage.PayCodeUrl = club.FirstOrDefault().PayImage;  //群收款码
                }
                ViewBag.RoomMessage = RoomMessage;
                //接口查询
                var pairs = new KeyValuePair<string, string>[] {
               new KeyValuePair<string, string>("action","GetPlayScore"),
               new KeyValuePair<string, string>("clubid",clubId),
               new KeyValuePair<string, string>("roomid",roomId),
               new KeyValuePair<string, string>("token",ConfigHelper.Token),
              };
                var resultJson = NetWorkHelper.HttpGetDown<ResultJson<PayScoreResult>>(ConfigHelper.GetAppSetting(ConfigConst.APIURL), pairs);
                //记录入参日志
                if (resultJson != null && resultJson.code == (int)EnumSuccess.Success)
                {
                    LogHelper.Info("RecordLogController>>>>>>>,接口返回战绩数据:" + resultJson.data.scores + "；当前登陆人信息：" + WebContext.CurrentUser + "");
                    var paySocres = resultJson.data.scores.Replace(@"\", "").DeserializeObject<ResultJson<dynamic>>();
                    if (paySocres != null)
                    {
                        string[] sArray = paySocres.data.ToString().Split(']');

                        //查询这条信息是否已在数据库 已在就直接读取数据库，未在就重新获取并存入
                        int b = sArray[0].IndexOf("[");
                        string thisPlayer = sArray[0].Substring(b + 1);
                        string[] pl = thisPlayer.Split(',');
                        int RecId = int.Parse(pl[0]);
                        var recordList = this.RecordLogService.GetByWhere(x => x.RecId == RecId).ToList();
                        int RecCount = recordList.Count();
                        if (RecCount == 0)
                        {
                            for (int i = 0; i < sArray.Length - 1; i++)
                            {
                                PlayScoreViewModel Player = new PlayScoreViewModel();
                                int a = sArray[i].IndexOf("[");
                                string player = sArray[i].Substring(a + 1);
                                string[] play = player.Split(',');
                                Player.RecId = int.Parse(play[0]);
                                Player.UserID = int.Parse(play[1]);
                                Player.RoomID = int.Parse(play[2]);
                                Player.ClubID = int.Parse(play[3]);
                                Player.Score = int.Parse(play[4]);

                                var user = this.SystemUserService.GetByWhere((x => x.UserId == Player.UserID));
                                if (user.Count() > 0)
                                {
                                    Player.NickName = string.IsNullOrEmpty(user.First().NickName)?"未上传名片": user.First().NickName;
                                    Player.HeadImgUrl = user.First().HeadImgUrl;
                                    Player.GameId = user.First().GameId.Value;
                                    //Player.PayCodeImg = user.First().PayCodeImg;
                                    Player.PayCodeUrl = user.First().PayCodeUrl;
                                    Player.PlatformId = user.First().PlatformId;
                                    Player.SystemUserId = user.First().Id;
                                }
                                Player.WriteTime = Convert.ToDateTime(play[5]).ToString("yyyy-MM-dd hh:mm:ss");
                                PlayerList.Add(Player);

                                //将此战绩存入战绩表
                                RecordLog recLog = new RecordLog();
                                recLog.RecId = int.Parse(play[0]);
                                recLog.ReplayId = play[6];
                                recLog.ClubId = Player.ClubID;
                                recLog.CreateTime = DateTime.Now;
                                recLog.PayMoney = Convert.ToDecimal(Player.Score * RoomMessage.Rate);
                                recLog.Score = Player.Score;
                                recLog.PlatformId = Player.PlatformId;
                                recLog.SystemUserId = Player.SystemUserId;

                                recLog.PayState = EnumPayStatus.UnPaid;
                                recLog.RoomId = Player.RoomID;
                                recLog.UserId = Player.UserID;

                                ///待修改
                                //recLog.MemberOpenid = "123";
                                this.RecordLogService.Insert(recLog);
                            }
                        }
                        else
                        {
                            foreach (var item in recordList)
                            {
                                PlayScoreViewModel Player2 = new PlayScoreViewModel();
                                Player2.RecId = item.RecId;
                                Player2.UserID = item.UserId;
                                Player2.RoomID = item.RoomId;
                                Player2.ClubID = item.ClubId;
                                Player2.Score = item.Score;
                                Player2.PayState = item.PayState;

                                var user = this.SystemUserService.GetByWhere((x => x.UserId == Player2.UserID));
                                if (user.Count() > 0)
                                {
                                    Player2.NickName = user.First().NickName;
                                    Player2.HeadImgUrl = user.First().HeadImgUrl;
                                    Player2.GameId = user.First().GameId.Value;
                                    //Player2.PayCodeImg = user.First().PayCodeImg;
                                    Player2.PayCodeUrl = user.First().PayCodeUrl;
                                }
                                Player2.WriteTime = item.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                                PlayerList.Add(Player2);
                            }
                        }

                        if (PlayerList.Count > 0)
                        {
                            ViewBag.PlayTime = PlayerList.First().WriteTime;
                            ViewBag.PlayerList = PlayerList;
                            ViewBag.RoomID = PlayerList.First().RoomID;
                            ViewBag.RecId = PlayerList.First().RecId;
                            int maxSorce = PlayerList.Max(x => x.Score);
                            foreach (var item in PlayerList)
                            {
                                if (item.Score == maxSorce)
                                {
                                    item.Winner = true;
                                }
                            }
                            LogHelper.Info("RecordLogController>>>>>>>,界面展示信息：" + PlayerList + "");
                        }
                    }
                }
            }
            if (WebContext.CurrentUser != null)
            {
                ViewBag.UserPayCode = WebContext.CurrentUser.PayCodeUrl;
                ViewBag.GameId = WebContext.CurrentUser.GameId.Value;
            }
            
            ViewBag.ClubId = clubId;
            ViewBag.RoomId = roomId;
            ViewBag.Message = message;
            
            return View();
        }

        /// <summary>
        /// 更新支付状态
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int ChangePayState(int recId)
        {
            int state = 0;
            try
            {
                int userId = this.WebContext.CurrentUser.UserId.Value;
                RecordLog rl = new RecordLog();
                rl = this.RecordLogService.GetByWhere(x => x.RecId == recId && x.UserId == userId).First();
                rl.PayState = EnumPayStatus.Paid;
                this.RecordLogService.Update(rl);
                state = 1;
            }
            catch
            {

            }
            return state;
        }

    }
}