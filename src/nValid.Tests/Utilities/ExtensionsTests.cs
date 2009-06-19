using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Utilities;

namespace nValid.Tests.Utilities
{
    public class ExtensionsTests : TestBase
    {
        [Test]
        public void Can_get_name_of_property()
        {
            Assert.That(Extensions.GetMemberName<Person, string>(p => p.FullName), Is.EqualTo("Full Name"));
        }

        [Test]
        public void Can_get_key_from_property()
        {
            Assert.That(Extensions.GetMemberKey<Person, string>(p => p.FullName), Is.EqualTo("FullName"));
        }

        [Test]
        public void Member_name_should_default_to_expression_body_when_not_a_member_expression()
        {
            Assert.That(Extensions.GetMemberName<Person, string>(p => p.FullName.ToUpper()), Is.EqualTo("p.FullName.ToUpper()"));
        }

        [Test]
        public void Member_key_should_default_to_expression_body_when_not_a_member_expression()
        {
            Assert.That(Extensions.GetMemberKey<Person, string>(p => p.FullName.ToUpper()), Is.EqualTo("p.FullName.ToUpper()"));
        }

        [Test]
        public void Can_format_pascal_cased_string_into_separate_words()
        {
            Assert.That("AAaa".PascalSplit(), Is.EqualTo("A Aaa"));
            Assert.That("Aaa".PascalSplit(), Is.EqualTo("Aaa"));
            Assert.That("AaaAaa".PascalSplit(), Is.EqualTo("Aaa Aaa"));
            Assert.That("AAA".PascalSplit(), Is.EqualTo("AAA"));
            Assert.That("AaaAAAaa123Aaa".PascalSplit(), Is.EqualTo("Aaa AA Aaa 123 Aaa"));
        }
    }
}
