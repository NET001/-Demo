using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Web.Models;
using Magicodes.Admin.Core.ErrorInfoConverter;
using Xunit;
using Xunit.Abstractions;

namespace Magicodes.Admin.Tests.ErrorInfoConverter
{
    public class AdminErrorInfoConverter_Tests : AppTestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AdminErrorInfoConverter _errorInfoConverter;

        public AdminErrorInfoConverter_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _errorInfoConverter = Resolve<AdminErrorInfoConverter>();
        }

        [Fact]
        public async Task AdminErrorInfoConverter_Should_Work_For_EntityNotFoundException_Overload_Methods()
        {
            var errorInfo = _errorInfoConverter.Convert(new EntityNotFoundException());

            Assert.Equal(errorInfo.Message, "数据不存在，请检查!");

            var exceptionWithoutMessage = new EntityNotFoundException()
            {
                EntityType = typeof(AuditLog),
                Id = 100
            };
            errorInfo = _errorInfoConverter.Convert(exceptionWithoutMessage);
            _testOutputHelper.WriteLine(errorInfo.Message);
            _testOutputHelper.WriteLine(exceptionWithoutMessage.Message);
            Assert.Equal(errorInfo.Message, $"Id为 {exceptionWithoutMessage.Id} 的数据并不存在，请检查!");
        }
    }
}
