using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Model.Models.RootTKey
{
    /// <summary>
    /// 所有实体的基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class RootEntityTkey<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 指定主键不为空，而且为主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public TKey Id { get; set; }
    }
}
