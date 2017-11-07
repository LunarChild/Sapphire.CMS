using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Web.Security;
using Sapphire.Core.CommonHelper;
using Sapphire.Core.Web;

namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 定义管理员主体的基本功能。
    /// </summary>
    [Serializable]
    public class AdminPrincipal : IPrincipal
    {
        /// <summary>
        /// 管理员信息。
        /// </summary>
        [NonSerialized]
        private IAdministrator administratorInfo;

        /// <summary>
        /// 身份标识。
        /// </summary>
        private IIdentity identity;

        /// <summary>
        /// 构造函数。
        /// </summary>
        public AdminPrincipal()
        {
        }

        /// <summary>
        /// 从 Identity 和角色名称数组（System.Security.Principal.GenericIdentity。
        /// 表示的用户属于该数组）初始化 System.Security.Principal.GenericPrincipal 类的新实例。
        /// </summary>
        /// <param name="identity">Identity 。</param>
        /// <param name="roles">角色名称数组。</param>
        public AdminPrincipal(IIdentity identity, int[] roles)
        {
            Check.NotNull(identity, "identity");

            this.identity = identity;
            this.Roles = roles;
        }

        /// <summary>
        /// 管理员名。
        /// </summary>
        public string AdministratorName { get; set; }

        /// <summary>
        /// 是否超级管理员。
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                return this.HasRole(new[] { SystemConstant.SuperAdminRoleId });
            }
        }

        /// <summary>
        /// 关联前台用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 管理员加密后密码。
        /// </summary>
        public string AdministratorPassword { get; set; }

        /// <summary>
        /// 判断多人同时登录随机验证码。
        /// </summary>
        public string RandomPassword { get; set; }

        /// <summary>
        /// 角色名称数组。
        /// </summary>
        public int[] Roles { get; set; }

        /// <summary>
        /// 站点权限集名称数组。
        /// </summary>
        public int[] SitePermissionSets { get; set; }

        /// <summary>
        /// 当前管理员信息。
        /// </summary>
        public IAdministrator AdministratorInfo
        {
            get
            {
                return this.administratorInfo;
            }

            set
            {
                this.administratorInfo = value;
            }
        }

        #region IPrincipal Members

        /// <summary>
        /// 获取当前AdminPrincipal表示的用户的IIdentity。
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }

            set
            {
                this.identity = value;
            }
        }

        /// <summary>
        /// 从FormsAuthenticationTicket身份认证信息中创建一个AdminPrincipal。
        /// </summary>
        /// <param name="ticket">FormsAuthenticationTicket身份认证信息。</param>
        /// <returns>AdminPrincipal。</returns>
        public static AdminPrincipal CreatePrincipal(FormsAuthenticationTicket ticket)
        {
            Check.NotNull(ticket, "ticket");

            try
            {
                var binaryFormatter = new BinaryFormatter();
                var memoryStream = new MemoryStream(Convert.FromBase64String(ticket.UserData));
                var adminPrincipal = (AdminPrincipal)binaryFormatter.Deserialize(memoryStream);
                memoryStream.Dispose();
                adminPrincipal.Identity = new FormsIdentity(ticket);
                return adminPrincipal;
            }
            catch (ArgumentNullException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
            catch (FormatException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
            catch (SerializationException)
            {
                return new AdminPrincipal(new NoAuthenticateIdentity(), null);
            }
        }

        /// <summary>
        /// 确定当前 AdminPrincipal 是否属于指定的角色Id列表字符串。
        /// </summary>
        /// <param name="role">角色Id列表字符串。</param>
        /// <returns>如果当前 AdminPrincipal 属于指定角色的成员，则为 true；否则为 false。</returns>
        public bool IsInRole(string role)
        {
            var roleIdStringArray = role.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var roleIdArray = new int[roleIdStringArray.Length];
            for (var i = 0; i < roleIdStringArray.Length; i++)
            {
                int result;
                if (int.TryParse(roleIdStringArray[i], out result))
                {
                    roleIdArray[i] = result;
                }
                else
                {
                    return false;
                }
            }

            return this.HasRole(roleIdArray);
        }

        /// <summary>
        /// 确定当前 AdminPrincipal 是否属于指定的角色Id数组。
        /// </summary>
        /// <param name="roleIdArray">角色Id数组。</param>
        /// <returns>如果当前 AdminPrincipal 属于指定角色的成员，则为 true；否则为 false。</returns>
        public bool HasRole(int[] roleIdArray)
        {
            return this.Roles.Any(roleIdArray.Contains);
        }

        /// <summary>
        /// 确定当前 AdminPrincipal 是否属于指定的角色Id数组。
        /// </summary>
        /// <param name="roleIdArray">角色Id数组。</param>
        /// <returns>如果当前 AdminPrincipal 属于指定角色的成员，则为 true；否则为 false。</returns>
        public bool HasSitePermissionSet(int[] roleIdArray)
        {
            return this.SitePermissionSets.Any(roleIdArray.Contains);
        }

        #endregion

        /// <summary>
        /// 序列化成字符串。
        /// </summary>
        /// <returns>返回序列化字符串。</returns>
        public string SerializeToString()
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, this);
            var serializeString = Convert.ToBase64String(memoryStream.ToArray());
            memoryStream.Dispose();
            return serializeString;
        }
    }
}