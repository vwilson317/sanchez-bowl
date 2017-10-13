using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nfl.fantasy.sanchez.bowl.app;

namespace nfl.fantasy.sanchez.bowl.mstests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var httpWrapper = new HttpWrapper();
            var domHelper = new DomHelper(httpWrapper);

            domHelper.LoadPageAsync(ProgramLogic._leagueUrl).GetAwaiter().GetResult();

            domHelper.Should().NotBeNull();
        }
    }
}
