using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using AWGL.States;

namespace AWGL.Managers.Tests
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
