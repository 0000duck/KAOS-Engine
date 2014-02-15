
using KAOS.States;
using NUnit.Framework;

namespace KAOS.Managers.Tests
{
    [TestFixture]
    public class StateManagerTest
    {
        [Test]
        public void TestAddStateExists()
        {
            StateManager stateManager = new StateManager();
            stateManager.AddState("test-state", new SplashScreenState(stateManager));

            Assert.IsTrue(stateManager.Exists("test-state"));
        }
    }
}
