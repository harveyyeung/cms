﻿using System;
using System.Collections.Generic;
using SiteServer.CMS.Caches;
using SiteServer.CMS.Core;
using SiteServer.CMS.Database.Core;
using SiteServer.CMS.Database.Models;
using SiteServer.CMS.Fx;
using SiteServer.CMS.Plugin.Impl;
using SiteServer.Plugin;
using SiteServer.Utils;
using SiteServer.Utils.Auth;

namespace SiteServer.CMS.Apis
{
    public class UserApi : IUserApi
    {
        private UserApi() { }

        private static UserApi _instance;
        public static UserApi Instance => _instance ?? (_instance = new UserApi());

        public IUserInfo NewInstance()
        {
            return new UserInfo();
        }

        public IUserInfo GetUserInfoByUserId(int userId)
        {
            return UserManager.GetUserInfoByUserId(userId);
        }

        public IUserInfo GetUserInfoByUserName(string userName)
        {
            return UserManager.GetUserInfoByUserName(userName);
        }

        public IUserInfo GetUserInfoByEmail(string email)
        {
            return UserManager.GetUserInfoByEmail(email);
        }

        public IUserInfo GetUserInfoByMobile(string mobile)
        {
            return UserManager.GetUserInfoByMobile(mobile);
        }

        public IUserInfo GetUserInfoByAccount(string account)
        {
            return UserManager.GetUserInfoByAccount(account);
        }

        public bool IsUserNameExists(string userName)
        {
            return DataProvider.User.IsUserNameExists(userName);
        }

        public bool IsEmailExists(string email)
        {
            return DataProvider.User.IsEmailExists(email);
        }

        public bool IsMobileExists(string mobile)
        {
            return DataProvider.User.IsMobileExists(mobile);
        }

        public bool Insert(IUserInfo userInfo, string password, out string errorMessage)
        {
            var userId = DataProvider.User.Insert(userInfo as UserInfo, password, FxUtils.GetIpAddress(), out errorMessage);
            return userId > 0;
        }

        public bool Validate(string account, string password, out string userName, out string errorMessage)
        {
            var userInfo = DataProvider.User.Validate(account, password, false, out userName, out errorMessage);
            return userInfo != null;
        }

        public bool ChangePassword(string userName, string password, out string errorMessage)
        {
            return DataProvider.User.ChangePassword(userName, password, out errorMessage);
        }

        public void Update(IUserInfo userInfo)
        {
            DataProvider.User.Update(userInfo as UserInfo);
        }

        public bool IsPasswordCorrect(string password, out string errorMessage)
        {
            return DataProvider.User.IsPasswordCorrect(password, out errorMessage);
        }

        public void AddLog(string userName, string action, string summary)
        {
            LogUtils.AddUserLog(userName, action, summary);
        }

        public List<ILogInfo> GetLogs(string userName, int totalNum, string action = "")
        {
            return DataProvider.UserLog.List(userName, totalNum, action);
        }

        public string GetAccessToken(int userId, string userName, TimeSpan expiresAt)
        {
            if (userId <= 0 || string.IsNullOrEmpty(userName)) return null;

            var userToken = new AccessTokenImpl
            {
                UserId = userId,
                UserName = userName,
                ExpiresAt = DateUtils.GetExpiresAt(expiresAt)
            };

            return JsonWebToken.Encode(userToken, WebConfigUtils.SecretKey, JwtHashAlgorithm.HS256);
        }

        public IAccessToken ParseAccessToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) return new AccessTokenImpl();

            try
            {
                var tokenObj = JsonWebToken.DecodeToObject<AccessTokenImpl>(accessToken, WebConfigUtils.SecretKey);

                if (tokenObj?.ExpiresAt.AddDays(Constants.AccessTokenExpireDays) > DateTime.Now)
                {
                    return tokenObj;
                }
            }
            catch
            {
                // ignored
            }

            return new AccessTokenImpl();
        }
    }
}
