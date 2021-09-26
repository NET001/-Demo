// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : MemberActivity.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

namespace Magicodes.Admin.Application.Tenants.Dashboard.Dto
{
    public class MemberActivity
    {
        public MemberActivity(string name, string earnings, int cases, int closed, string rate)
        {
            Name = name;
            Earnings = earnings;
            Cases = cases;
            Closed = closed;
            Rate = rate;
        }

        public string Name { get; set; }
        public string Earnings { get; set; }
        public int Cases { get; set; }
        public int Closed { get; set; }
        public string Rate { get; set; }
    }
}