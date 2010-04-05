using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Framework;

namespace nValid.Tests.Framework
{
    public class ValidationContextSwitchTests : TestBase
    {
        protected override void PerTestTearDown()
        {
            ValidationContext.Reset();
        }

        [Test]
        public void Can_switch_to_named_context()
        {
            var defaultContext = ValidationContext.Current;

            using (new ValidationContextSwitch("TestContext"))
            {
                Assert.That(ValidationContext.Current, Is.Not.SameAs(defaultContext));
            }
        }

        [Test]
        public void Original_context_is_restored()
        {
            var defaultContext = ValidationContext.Current;

            using (new ValidationContextSwitch("TestContext"))
            {
            }

            Assert.That(ValidationContext.Current, Is.SameAs(defaultContext));
        }

        [Test]
        public void Can_reuse_named_context()
        {
            IValidationContext switched;
            using (new ValidationContextSwitch("TestContext"))
            {
                switched = ValidationContext.Current;
            }

            using (new ValidationContextSwitch("TestContext"))
            {
                Assert.That(switched, Is.SameAs(ValidationContext.Current));
            }
        }

        [Test]
        public void Different_context_names_result_in_different_contexts()
        {
            IValidationContext switched;
            using (new ValidationContextSwitch("TestContext"))
            {
                switched = ValidationContext.Current;
            }

            using (new ValidationContextSwitch("TestContext 2"))
            {
                Assert.That(switched, Is.Not.SameAs(ValidationContext.Current));
            }
        }

        [Test]
        public void Context_name_ignores_case()
        {
            IValidationContext switched;
            using (new ValidationContextSwitch("TestContext"))
            {
                switched = ValidationContext.Current;
            }

            using (new ValidationContextSwitch("testcontext"))
            {
                Assert.That(switched, Is.SameAs(ValidationContext.Current));
            }
        }

        [Test]
        public void Switching_context_does_not_influence_other_threads()
        {
            IValidationContext threadContext = null;
            var thread = new Thread(() =>
                                        {
                                            threadContext = ValidationContext.Current;
                                        });

            using (new ValidationContextSwitch("TestContext"))
            {
                thread.Start();
                thread.Join();
            }

            Assert.That(threadContext, Is.SameAs(ValidationContext.Current));
        }
    }
}
