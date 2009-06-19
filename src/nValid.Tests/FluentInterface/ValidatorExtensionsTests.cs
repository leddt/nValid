using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using nValid.FluentInterface;
using nValid.Tests.TestObjects;
using nValid.Validators;
using Iz = Rhino.Mocks.Constraints.Is;

namespace nValid.Tests.FluentInterface
{
    public class ValidatorExtensionsTests : TestBase
    {
        private IValidationNode<Person, string> stringNode;
        private IValidationNode<Person, IList<Item>> itemsNode;

        protected override void PerTestSetUp()
        {
            stringNode = CreateMock<IValidationNode<Person, string>>();
            itemsNode = CreateMock<IValidationNode<Person, IList<Item>>>();
        }

        protected override void PerTestTearDown()
        {
            stringNode.VerifyAllExpectations();
            itemsNode.VerifyAllExpectations();
        }

        private static void ExpectValidatorType<TValidator, TValidated>(IValidationNode<Person, TValidated> node)
        {
            node.Expect(n => n.AddValidator(null)).Constraints(Iz.TypeOf<TValidator>()).Return(null);
        }
        
        [Test]
        public void Can_use_fluent_syntax_to_get_Null_validator()
        {
            ExpectValidatorType<NullValidator<Person, string>, string>(stringNode);
            stringNode.Null();
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_StringEmpty_validator()
        {
            ExpectValidatorType<StringEmptyValidator<Person>, string>(stringNode); 
            stringNode.Empty();
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_ListEmpty_validator()
        {
            ExpectValidatorType<ListEmptyValidator<Person, Item>, IList<Item>>(itemsNode);
            itemsNode.Empty();
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Length_validator()
        {
            ExpectValidatorType<LengthValidator<Person>, string>(stringNode);
            stringNode.Length(1, 10);
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Length_validator_with_instance_properties()
        {
            ExpectValidatorType<LengthValidator<Person>, string>(stringNode);
            stringNode.Length(p => p.Age, p => p.Age);
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Equal_validator()
        {
            ExpectValidatorType<EqualValidator<Person, string>, string>(stringNode);
            stringNode.EqualTo("test");
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Equal_validator_with_instance_property()
        {
            ExpectValidatorType<EqualValidator<Person, string>, string>(stringNode);
            stringNode.EqualTo(p => p.Name.ToLower());
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_GreaterThan_validator()
        {
            ExpectValidatorType<GreaterThanValidator<Person, string>, string>(stringNode);
            stringNode.GreaterThan("test");
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_GreaterThan_validator_with_instance_property()
        {
            ExpectValidatorType<GreaterThanValidator<Person, string>, string>(stringNode);
            stringNode.GreaterThan(p => p.Name.ToUpper());
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_GreaterOrEqualTo_validator()
        {
            ExpectValidatorType<GreaterOrEqualToValidator<Person, string>, string>(stringNode);
            stringNode.GreaterOrEqualTo("test");
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_GreaterOrEqualTo_validator_with_instance_property()
        {
            ExpectValidatorType<GreaterOrEqualToValidator<Person, string>, string>(stringNode);
            stringNode.GreaterOrEqualTo(p => p.Name.ToUpper());
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_LowerThan_validator()
        {
            ExpectValidatorType<LowerThanValidator<Person, string>, string>(stringNode);
            stringNode.LowerThan("test");
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_LowerThan_validator_with_instance_property()
        {
            ExpectValidatorType<LowerThanValidator<Person, string>, string>(stringNode);
            stringNode.LowerThan(p => p.Name.ToUpper());
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_LowerOrEqualTo_validator()
        {
            ExpectValidatorType<LowerOrEqualToValidator<Person, string>, string>(stringNode);
            stringNode.LowerOrEqualTo("test");
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_LowerOrEqualTo_validator_with_instance_property()
        {
            ExpectValidatorType<LowerOrEqualToValidator<Person, string>, string>(stringNode);
            stringNode.LowerOrEqualTo(p => p.Name.ToUpper());
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Custom_validator()
        {
            ExpectValidatorType<CustomValidator<Person, string>, string>(stringNode);
            stringNode.Custom(s => s.StartsWith("test"));
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_Custom_validator_with_instance_reference()
        {
            ExpectValidatorType<CustomValidator<Person, string>, string>(stringNode);
            stringNode.Custom((p, s) => s.StartsWith(p.Name));
        }

        [Test]
        public void Can_use_fluent_syntax_to_get_RegEx_validator()
        {
            ExpectValidatorType<RegExValidator<Person>, string>(stringNode);
            stringNode.Matches("pattern");
        }
    }
}
