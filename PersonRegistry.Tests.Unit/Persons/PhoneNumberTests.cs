using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Tests.Unit.Persons
{
    public class PhoneNumberTests
    {
        [Fact]
        public void Create_Sets_Fields()
        {
            var pId = Guid.NewGuid();
            var phone = PhoneNumber.Create(pId, PhoneNumberType.Mobile, new PhoneNumberNumber("+995 599 00 00 00"));

            Assert.Equal(pId, phone.PersonId);
            Assert.Equal(PhoneNumberType.Mobile, phone.Type);
            Assert.Equal("+995 599 00 00 00", phone.Number.Value);
            Assert.NotEqual(Guid.Empty, phone.Id.Value);
        }

        [Fact]
        public void ChangeType_And_Number_Work()
        {
            var phone = PhoneNumber.Create(Guid.NewGuid(), PhoneNumberType.Home, new PhoneNumberNumber("1111"));
            phone.ChangeType(PhoneNumberType.Office);
            phone.ChangeNumber(new PhoneNumberNumber("2222"));

            Assert.Equal(PhoneNumberType.Office, phone.Type);
            Assert.Equal("2222", phone.Number.Value);
        }

        [Fact]
        public void Create_With_Empty_PersonId_Fails()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                PhoneNumber.Create(Guid.Empty, PhoneNumberType.Home, new PhoneNumberNumber("1234")));
        }
    }
}
