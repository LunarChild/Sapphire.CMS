using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 动态模板参数。
    /// </summary>
    internal class DynamicParameterDictionary : DynamicObject
    {
        /// <summary>
        /// 参数字典。
        /// </summary>
        private readonly IDictionary<string, object> parameters;

        /// <summary>
        /// 动态参数构造函数。
        /// </summary>
        public DynamicParameterDictionary()
        {
            this.parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// 解析动态参数。
        /// </summary>
        /// <param name="parameters">匿名对象参数。</param>
        /// <returns>返回动态参数。</returns>
        public static dynamic ParseDynamicParameters(object parameters)
        {
            var dynamicParameter = new DynamicParameterDictionary();

            if (parameters != null)
            {
                foreach (var item in parameters.GetType().GetProperties())
                {
                    dynamicParameter.parameters.Add(item.Name.ToLower(), item.GetValue(parameters));
                }
            }

            return dynamicParameter;
        }

        /// <summary>
        /// 解析动态参数。
        /// </summary>
        /// <param name="parameters">参数集合。</param>
        /// <returns>动态参数对象。</returns>
        public static dynamic ParseDynamicParameters(NameValueCollection parameters)
        {
            var dynamicParameter = new DynamicParameterDictionary();

            if (parameters != null)
            {
                foreach (var key in parameters.AllKeys)
                {
                    if (key != null)
                    {
                        var stringValue = parameters[key];
                        int intValue;
                        bool boolValue;

                        if (int.TryParse(stringValue, out intValue))
                        {
                            dynamicParameter.parameters.Add(key.ToLower(), intValue);
                        }
                        else if (bool.TryParse(stringValue, out boolValue))
                        {
                            dynamicParameter.parameters.Add(key.ToLower(), boolValue);
                        }
                        else
                        {
                            dynamicParameter.parameters.Add(key.ToLower(), stringValue);
                        }
                    }
                }
            }

            return dynamicParameter;
        }

        /// <summary>
        /// 获取成员值。
        /// </summary>
        /// <param name="binder">动态获取成员绑定。</param>
        /// <param name="result">返回值。</param>
        /// <returns>返回是否获取成功。</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            this.parameters.TryGetValue(binder.Name.ToLower(), out result);
            return true;
        }

        /// <summary>
        /// 设置成员值。
        /// </summary>
        /// <param name="binder">动态设置成员绑定。</param>
        /// <param name="value">值。</param>
        /// <returns>返回是否设置成功。</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var binderName = binder.Name.ToLower();

            this.parameters[binderName] = value;

            return true;
        }
    }
}