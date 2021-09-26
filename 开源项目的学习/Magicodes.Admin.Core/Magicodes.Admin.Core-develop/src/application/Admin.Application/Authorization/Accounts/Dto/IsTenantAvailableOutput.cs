// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IsTenantAvailableOutput.cs
//           description :
// 
//           created by 雪雁 at  2019-06-14 11:22
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

namespace Magicodes.Admin.Application.Authorization.Accounts.Dto
{
    public class IsTenantAvailableOutput
    {
        public IsTenantAvailableOutput()
        {
        }

        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
        {
            State = state;
            TenantId = tenantId;
        }

        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId, string serverRootAddress)
        {
            State = state;
            TenantId = tenantId;
            ServerRootAddress = serverRootAddress;
        }

        public TenantAvailabilityState State { get; set; }

        public int? TenantId { get; set; }

        public string ServerRootAddress { get; set; }
    }
}