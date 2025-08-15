using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Tests.Unit.Persons
{
    public class PersonRelationTests
    {
        [Fact]
        public void Create_Sets_Fields()
        {
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            var rel = PersonRelation.Create(a, b, RelationType.Colleague);

            Assert.Equal(a, rel.PersonId);
            Assert.Equal(b, rel.RelatedPersonId);
            Assert.Equal(RelationType.Colleague, rel.Type);
        }

        [Fact]
        public void Create_Fails_On_Invalid_Ids()
        {
            var good = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(() => PersonRelation.Create(Guid.Empty, good, RelationType.Colleague));
            Assert.Throws<ArgumentOutOfRangeException>(() => PersonRelation.Create(good, Guid.Empty, RelationType.Colleague));
        }

        [Fact]
        public void Create_Fails_On_Self_Relation()
        {
            var id = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => PersonRelation.Create(id, id, RelationType.Colleague));
        }
    }
}
