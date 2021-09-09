using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Model.Models.RootTKey
{
    /// <summary>
    /// 接口API地址信息表 
    /// 父类
    /// </summary>
    public class ModulesRoot<Tkey> : RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Tkey ParentId { get; set; }
    }
}
