using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Model.Models.RootTKey
{
    /// <summary>
    /// Tibug 博文
    /// </summary>
    public class TopicDetailRoot<Tkey> : RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
        public Tkey TopicId { get; set; }
    }
}
