﻿using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            try
            {
                var logPath = ConfigHelper.GetPhysicsPath(ConfigHelper.GetAppSetting(ConfigConst.LOG));
                var fileInfo = new FileInfo(logPath);
                if (fileInfo.Exists)
                {
                    log4net.Config.XmlConfigurator.Configure(fileInfo);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
        }

        #region 调试

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Debug(Exception e, string msg = "")
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (string.IsNullOrWhiteSpace(msg))
            {
                logger.Debug(e.Message, e);
            }
            else
            {
                logger.Debug(msg, e);
            }
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Debug(object msg)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(msg);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Debug(object msg, Exception e)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(msg, e);
        }

        #endregion

        #region 应用错误
        /// <summary>
        /// 应用错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Error(Exception e, string msg = "")
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (string.IsNullOrWhiteSpace(msg))
            {
                logger.Error(e.Message, e);
            }
            else
            {
                logger.Error(msg, e);
            }
        }

        /// <summary>
        /// 应用错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Error(object msg)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error(msg);
        }

        /// <summary>
        /// 应用错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Error(object msg, Exception e)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error(msg, e);
        }

        #endregion

        #region 不可修复错误

        /// <summary>
        /// 不可修复错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Fatal(Exception e, string msg = "")
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (string.IsNullOrWhiteSpace(msg))
            {
                logger.Fatal(e.Message, e);
            }
            else
            {
                logger.Fatal(msg, e);
            }
        }

        /// <summary>
        /// 不可修复错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Fatal(object msg)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Fatal(msg);
        }

        /// <summary>
        /// 不可修复错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Fatal(object msg, Exception e)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Fatal(msg, e);
        }

        #endregion

        #region 信息

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Info(Exception e, string msg = "")
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (string.IsNullOrWhiteSpace(msg))
            {
                logger.Info(e.Message, e);
            }
            else
            {
                logger.Info(msg, e);
            }
        }

        /// <summary>
        /// 应用错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Info(object msg)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(msg);
        }

        /// <summary>
        /// 应用错误
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Info(object msg, Exception e)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(msg, e);
        }

        #endregion

        #region 警告

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Warn(Exception e, string msg = "")
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (string.IsNullOrWhiteSpace(msg))
            {
                logger.Warn(e.Message, e);
            }
            else
            {
                logger.Warn(msg, e);
            }
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Warn(object msg)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Warn(msg);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        public static void Warn(object msg, Exception e)
        {
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Warn(msg, e);
        }

        #endregion

    }
}
