using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using Sapphire.Core.Config;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Web
{
    // UNDONE [2013-11-15]  此类需要完全重写，身份验证后面考虑使用ASP.NET Identity，此类暂不需要代码走查

    /// <summary>
    /// 统一管理网站的Cookie的写入和移除。
    /// </summary>
    public static class ManageCookies
    {
        private static readonly string MainDomain = SiteConfig.Instance.Domain;

        /// <summary>
        /// 获取用于存储前台用户 Forms 身份验证票证的 Cookie 名称。
        /// </summary>
        public static string UserCookieName
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var applicationPath = HttpContext.Current.Request.ApplicationPath == null ? string.Empty : HttpContext.Current.Request.ApplicationPath.Replace("/", string.Empty);
                    if (!string.IsNullOrEmpty(MainDomain))
                    {
                        return applicationPath + FormsAuthentication.FormsCookieName;
                    }

                    return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Url.Port + applicationPath + FormsAuthentication.FormsCookieName;
                }

                return FormsAuthentication.FormsCookieName;
            }
        }

        /// <summary>
        /// 获取用于存储后台管理员 Forms 身份验证票证的 Cookie 名称。
        /// </summary>
        public static string AdminCookieName
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request.ApplicationPath != null)
                {
                    var applicationPath = string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath) ? string.Empty : HttpContext.Current.Request.ApplicationPath.Replace("/", string.Empty);
                    if (!string.IsNullOrEmpty(MainDomain))
                    {
                        return applicationPath + FormsAuthentication.FormsCookieName + "AdminCookie";
                    }

                    return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Url.Port + applicationPath + FormsAuthentication.FormsCookieName + "AdminCookie";
                }

                return FormsAuthentication.FormsCookieName + "AdminCookie";
            }
        }

        /// <summary>
        /// 创建用户Cookie。
        /// </summary>
        /// <param name="authenticationTicket">身份验证票。</param>
        /// <param name="isPersistent">是否指定过期时间。</param>
        /// <param name="expirationTime">过期时间。</param>
        public static void CreateUserCookie(FormsAuthenticationTicket authenticationTicket, bool isPersistent, DateTime expirationTime)
        {
            CreateCookie(UserCookieName, authenticationTicket, isPersistent, expirationTime);
        }

        /// <summary>
        /// 创建管理员Cookie。
        /// </summary>
        /// <param name="authenticationTicket">身份验证票。</param>
        /// <param name="isPersistent">是否指定过期时间。</param>
        /// <param name="expirationTime">过期时间。</param>
        public static void CreateAdminCookie(FormsAuthenticationTicket authenticationTicket, bool isPersistent, DateTime expirationTime)
        {
            CreateCookie(AdminCookieName, authenticationTicket, isPersistent, expirationTime);
        }

        /// <summary>
        /// 创建Cookie。
        /// </summary>
        /// <param name="cookieName">Cookie名。</param>
        /// <param name="cookieValue">Cookie值。</param>
        public static void CreateCookie(string cookieName, string cookieValue)
        {
            var authenticationCookie = new HttpCookie(cookieName, cookieValue);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(authenticationCookie);
            }
        }

        /// <summary>
        /// 创建Cookie。
        /// </summary>
        /// <param name="cookieName">Cookie名。</param>
        /// <param name="authenticationTicket">要写入cookie的内容。</param>
        /// <param name="isPersistent">是否指定过期时间。</param>
        /// <param name="expirationTime">过期时间。</param>
        public static void CreateCookie(string cookieName, FormsAuthenticationTicket authenticationTicket, bool isPersistent, DateTime expirationTime)
        {
            var cookieValue = FormsAuthentication.Encrypt(authenticationTicket);
            var authenticationCookie = new HttpCookie(cookieName, cookieValue);
            if (isPersistent)
            {
                authenticationCookie.Expires = expirationTime;
            }

            authenticationCookie.HttpOnly = true;
            authenticationCookie.Path = FormsAuthentication.FormsCookiePath;
            authenticationCookie.Secure = FormsAuthentication.RequireSSL;
            if (!string.IsNullOrEmpty(MainDomain))
            {
                if (HttpContext.Current != null)
                {
                    var host = "." + HttpContext.Current.Request.Url.Host;
                    if (host.EndsWith(MainDomain, StringComparison.OrdinalIgnoreCase))
                    {
                        authenticationCookie.Domain = MainDomain;
                    }
                }
                else
                {
                    authenticationCookie.Domain = MainDomain;
                }
            }

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(authenticationCookie);
            }
        }

        /// <summary>
        /// 移除管理员的Cookie。
        /// </summary>
        public static void RemoveAdminCookie()
        {
            var cookieName = AdminCookieName;
            RemoveCookie(cookieName);

            // UNDONE [2013-09-18]  功能未实现完全
            if (true)
            {
                cookieName = UserCookieName;
                RemoveCookie(cookieName);
            }
        }

        /// <summary>
        /// 移除前台用户的cookie。
        /// </summary>
        public static void RemoveUserCookie()
        {
            var cookieName = UserCookieName;
            RemoveCookie(cookieName);
        }

        /// <summary>
        /// 从Cookie获取FormsAuthenticationTicket。
        /// </summary>
        /// <param name="context">HttpContext。</param>
        /// <param name="name">Cookie名称。</param>
        /// <returns>FormsAuthenticationTicket。</returns>
        public static FormsAuthenticationTicket ExtractTicketFromCookie(HttpContext context, string name)
        {
            Check.NotNull(context, "context");

            FormsAuthenticationTicket ticket = null;
            string encryptedTicket = null;

            var cookie = context.Request.Cookies[name];
            if (cookie != null)
            {
                encryptedTicket = cookie.Value;
            }

            if ((encryptedTicket != null) && (encryptedTicket.Length > 1))
            {
                try
                {
                    ticket = FormsAuthentication.Decrypt(encryptedTicket);
                }
                catch (ArgumentException)
                {
                    return null;
                }
                catch (CryptographicException)
                {
                    context.Request.Cookies.Remove(name);
                }

                if (ticket != null)
                {
                    if (SecurityConfig.Instance.TicketTime == 0)
                    {
                        return ticket;
                    }

                    if (!ticket.Expired)
                    {
                        return ticket;
                    }
                }

                return null;
            }

            return null;
        }

        /// <summary>
        /// 设置Cookie的可调过期。
        /// </summary>
        /// <param name="context">HttpContext。</param>
        /// <param name="ticket">FormsAuthenticationTicket。</param>
        /// <param name="cookieName">Cookie名称。</param>
        public static void SlidingExpiration(HttpContext context, FormsAuthenticationTicket ticket, string cookieName)
        {
            Check.NotNull(context, "context");

            Check.NotNull(ticket, "ticket");

            var newAuthenticationTicket = FormsAuthentication.SlidingExpiration ? FormsAuthentication.RenewTicketIfOld(ticket) : ticket;

            if (newAuthenticationTicket != null)
            {
                var cookieValue = FormsAuthentication.Encrypt(newAuthenticationTicket);

                var authenticationCookie = context.Request.Cookies[cookieName] ?? new HttpCookie(cookieName, cookieValue) { Path = newAuthenticationTicket.CookiePath };

                if (ticket.IsPersistent)
                {
                    authenticationCookie.Expires = newAuthenticationTicket.Expiration;
                }

                authenticationCookie.Value = cookieValue;
                authenticationCookie.Secure = FormsAuthentication.RequireSSL;
                authenticationCookie.HttpOnly = true;

                if (!string.IsNullOrEmpty(MainDomain))
                {
                    if (HttpContext.Current != null)
                    {
                        var host = "." + HttpContext.Current.Request.Url.Host;
                        if (host.EndsWith(MainDomain, StringComparison.OrdinalIgnoreCase))
                        {
                            authenticationCookie.Domain = MainDomain;
                        }
                    }
                    else
                    {
                        authenticationCookie.Domain = MainDomain;
                    }
                }

                context.Response.Cookies.Remove(authenticationCookie.Name);
                context.Response.Cookies.Add(authenticationCookie);
            }
        }

        /// <summary>
        /// 移除Cookie。
        /// </summary>
        /// <param name="cookieName">Cookie名。</param>
        private static void RemoveCookie(string cookieName)
        {
            var cookieValue = string.Empty;
            if (HttpContext.Current.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
            {
                cookieValue = "NoCookie";
            }

            var cookie = new HttpCookie(cookieName, cookieValue)
            {
                HttpOnly = true,
                Path = FormsAuthentication.FormsCookiePath,
                Expires = new DateTime(1999, 10, 12),
                Secure = FormsAuthentication.RequireSSL
            };
            if (!string.IsNullOrEmpty(MainDomain))
            {
                if (HttpContext.Current != null)
                {
                    var host = "." + HttpContext.Current.Request.Url.Host;
                    if (host.EndsWith(MainDomain, StringComparison.OrdinalIgnoreCase))
                    {
                        cookie.Domain = MainDomain;
                    }
                }
                else
                {
                    cookie.Domain = MainDomain;
                }
            }

            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}