using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.FluentInterface;
using nValid.Tests.TestObjects;

namespace nValid.Tests.FluentInterface
{
    [TestFixture]
    public class Usage
    {
        /// <summary>
        /// This sample uses most of the fluent interface's functionality.
        /// </summary>
        [Test]
        public void Can_configure_and_validate_using_fluent_syntax()
        {
            SetupValidation.For<Person>(
                rules => {

                    rules.Not.Null();

                    rules.Property(p => p.Name)
                        .WithKey("_name")
                        .WithName("The person's name")
                        .Not.Empty().WithMessage("Name is required.")
                        .Length(2, 50).WithMessage("Name should be between 2 and 50 characters.")
                        .Matches("^([A-Z][a-z]* ?)+$");

                    rules.Property(p => p.Age)
                        .GreaterOrEqualTo(13).WithMessage("Age must be greater than 13");

                    rules.Property(p => p.Notes)
                        .Custom(notes => notes.Count <= 10).When(p => p.Age <= 6);

                    rules.ForEach(p => p.Notes)
                        .Not.Empty()
                        .Length(1, 255);

                    rules.Property(p => p.Possessions)
                        .Custom(possessions => possessions.Sum(i => i.Weight) <= 50).WithMessage("Total weights of possessions must be under 50 pounds.");

                    rules.ForEach(p => p.Possessions)
                        .Property(i => i.Weight)
                        .LowerThan(10).WithMessage("Can't carry a single item that weights more than 10 pounds.");

                });
            
            var p1 = new Person
                         {
                             Age = 21,
                             Name = "John Doe",
                             Notes = new List<string> { "Note 1" },
                             Possessions = new List<Item> { new Item { Weight = 5 } }
                         };

            var p2 = new Person
                         {
                             Age = 21,
                             Name = "John doe", // <-- should be invalid
                             Notes = new List<string> { "Note 1" },
                             Possessions = new List<Item> { new Item { Weight = 12 } } // <-- should be invalid
                         };

            var r1 = Validate.Instance(p1);
            var r2 = p2.Validate();
            
            Assert.That(r1.IsValid, Is.True);
            Assert.That(r2.IsValid, Is.False);
            Assert.That(r2.BrokenRules.Count, Is.EqualTo(2));

            Assert.That(r2.BrokenRules[0].PropertyKey, Is.EqualTo("_name"));
            Assert.That(r2.BrokenRules[0].Message, Is.EqualTo("The person's name does not have the correct format."));
        }

    }
}