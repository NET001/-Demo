using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Configuration.Startup;
using Abp.Dependency.Installers;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.UI;
using Abp.Web.Configuration;
using Abp.Web.Models;
using NSubstitute;
using Xunit;

namespace Magicodes.Admin.Tests.ErrorInfoConverter
{
    public class ErrorInfoBuilder_Tests : AppTestBase
    {
        private readonly IErrorInfoBuilder _errorInfoBuilder;

        public ErrorInfoBuilder_Tests()
        {
            _errorInfoBuilder = Resolve<IErrorInfoBuilder>();
        }

       
        [Fact]
        public void Should_Convert_UserFriendlyException()
        {
            var errorInfo = _errorInfoBuilder.BuildForException(new UserFriendlyException("Test message"));
            Assert.Equal(0, errorInfo.Code);
            Assert.Equal("Test message", errorInfo.Message);
        }

        [Fact]
        public async Task AdminErrorInfoConverter_Should_Work_For_EntityNotFoundException_Overload_Methods()
        {
            var errorInfo = _errorInfoBuilder.BuildForException(new EntityNotFoundException());

            Assert.Equal(errorInfo.Message, "数据不存在，请检查!");

            var exceptionWithoutMessage = new EntityNotFoundException()
            {
                EntityType = typeof(AuditLog),
                Id = 100
            };
            errorInfo = _errorInfoBuilder.BuildForException(exceptionWithoutMessage);
            Assert.Equal(errorInfo.Message, $"Id为 {exceptionWithoutMessage.Id} 的数据并不存在，请检查!");
        }

    }
}
