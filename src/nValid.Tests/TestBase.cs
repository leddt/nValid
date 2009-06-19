using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace nValid.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        private List<object> mocks;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            FixtureSetUp();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            FixtureTearDown();
        }

        [SetUp]
        public void SetUp()
        {
            mocks = new List<object>();
            PerTestSetUp();
        }

        [TearDown]
        public void TearDown()
        {
            PerTestTearDown();
        }

        protected T CreateMock<T>(params object[] args) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(args);
            mocks.Add(mock);

            return mock;
        }

        protected static T CreateStub<T>(params object[] args) where T : class
        {
            return MockRepository.GenerateStub<T>(args);
        }

        protected void VerifyAll()
        {
            foreach (var mock in mocks)
                mock.VerifyAllExpectations();
        }

        protected virtual void PerTestSetUp()
        {
        }

        protected virtual void PerTestTearDown()
        {
        }

        protected virtual void FixtureSetUp()
        {
        }

        protected virtual void FixtureTearDown()
        {
        }
    }
}
