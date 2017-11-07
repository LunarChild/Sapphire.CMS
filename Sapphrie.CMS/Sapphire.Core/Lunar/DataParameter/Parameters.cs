using System.Collections.Generic;
using System.Data;

namespace Sapphire.Core.Power
{
    /// <summary>
    /// 表示DbParameter的对象集合类。
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// 参数项的集合。
        /// </summary>
        private IList<Parameter> entries = new List<Parameter>();

        /// <summary>
        /// 默认的构造函数。
        /// </summary>
        public Parameters()
        {
        }

        /// <summary>
        /// 参数对象构造函数。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <param name="type">参数类型。</param>
        /// <param name="value">参数值。</param>
        public Parameters(string name, DbType type, object value)
        {
            var param = new Parameter(name, type, value);
            this.entries.Add(param);
        }

        /// <summary>
        /// 参数项的集合。
        /// </summary>
        public IList<Parameter> Entries
        {
            get
            {
                return this.entries;
            }

            set
            {
                this.entries = value;
            }
        }

        /// <summary>
        /// 增加一个输入参数。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <param name="type">参数类型。</param>
        /// <param name="value">参数值。</param>
        public void Add(string name, DbType type, object value)
        {
            var param = new Parameter(name, type, value);
            this.entries.Add(param);
        }
    }
}