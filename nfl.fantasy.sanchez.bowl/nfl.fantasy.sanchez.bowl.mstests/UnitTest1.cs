using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nfl.fantasy.sanchez.bowl.app;
using nfl.fantasy.sanchez.bowl.domain;
using NSubstitute;

namespace nfl.fantasy.sanchez.bowl.mstests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var htmlWebWrapper = Substitute.For<IHtmlWebWrapper>();
            var accessorMock = Substitute.For<IOptions<TeamConfig>>();
            var domHelper = new DomHelper(htmlWebWrapper, accessorMock);

            domHelper.LoadPlayerDetails(PlayerDetailsHelper.LeagueUrl).GetAwaiter().GetResult();

            domHelper.Should().NotBeNull();
        }
    }
}
