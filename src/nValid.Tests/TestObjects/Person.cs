using System.Collections.Generic;

namespace nValid.Tests.TestObjects
{
    public class Person : ISentient
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IList<string> Notes { get; set; }
        public IList<Item> Possessions { get; set; }
    }
}