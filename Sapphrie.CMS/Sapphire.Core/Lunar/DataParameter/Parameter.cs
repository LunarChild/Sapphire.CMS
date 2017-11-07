using System.Data;

namespace Sapphire.Core.Power
{
    /// <summary>
    /// Parameter。
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public Parameter()
        {
        }

        /// <summary>
        /// 参数对象构造函数（输入参数）。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <param name="type">参数类型。</param>
        /// <param name="value">参数值。</param>
        public Parameter(string name, DbType type, object value)
            : this(ParameterDirection.Input, name, type, value, 0)
        {
        }

        /// <summary>
        /// 参数对象构造函数。
        /// </summary>
        /// <param name="direction">输入参数还是输出参数。</param>
        /// <param name="name">参数名。</param>
        /// <param name="type">参数类型。</param>
        /// <param name="value">参数值。</param>
        /// <param name="size">输出参数大小。</param>
        public Parameter(ParameterDirection direction, string name, DbType type, object value, int size)
        {
            this.Direction = direction;
            this.Name = name;
            this.DbType = type;
            this.Value = value;
            this.Size = size;
        }

        /// <summary>
        /// 表示输入参数或者输出参数。
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 参数名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型。
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 参数值。
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 输出参数的大小。
        /// </summary>
        public int Size { get; set; }
    }
}