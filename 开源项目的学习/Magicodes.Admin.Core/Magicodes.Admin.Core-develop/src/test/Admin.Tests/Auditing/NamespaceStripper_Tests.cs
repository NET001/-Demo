// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : NamespaceStripper_Tests.cs
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

using Magicodes.Admin.Application.Auditing;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Auditing
{
    public class NamespaceStripper_Tests : AppTestBase
    {
        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        private readonly INamespaceStripper _namespaceStripper;

        [Theory]
        [InlineData(
            "Magicodes.Admin.Auditing.GenericEntityService`1[[Magicodes.Admin.Storage.BinaryObject, Magicodes.Admin.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]",
            "GenericEntityService<BinaryObject>")]
        [InlineData(
            "CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...",
            "EntityService<Book, CreateInput>")]
        [InlineData(
            "Magicodes.Admin.Auditing.XEntityService`1[Magicodes.Admin.Auditing.AService`5[[Magicodes.Admin.Storage.BinaryObject, Magicodes.Admin.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Magicodes.Admin.Storage.TestObject, Magicodes.Admin.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]",
            "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Magicodes.Admin.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }
    }
}