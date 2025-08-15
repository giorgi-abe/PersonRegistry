using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Tests.Unit.Persons
{
    public class PersonValueObjectsTests
    {
        [Fact]
        public void PersonName_Valid_Georgian_And_Latin()
        {
            _ = new PersonName("გიორგი");
            _ = new PersonName("Giorgi");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void PersonName_Empty_Fails(string v)
        {
            Assert.Throws<ArgumentException>(() => new PersonName(v));
        }

        [Theory]
        [InlineData(1)]   
        [InlineData(51)]  
        public void PersonName_Length_Range_Enforced(int len)
        {
            var v = new string('a', len);         
            Assert.Throws<DomainException>(() => new PersonName(v));
        }

        [Fact]
        public void PersonName_Disallows_Mixed_Alphabets()
        {
            Assert.Throws<DomainException>(() => new PersonName("Giorgiგ"));
        }

        [Fact]
        public void PersonSurname_Valid()
        {
            _ = new PersonSurname("Brown");
            _ = new PersonSurname("ბრაუნ");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void PersonSurname_Empty_Fails(string v)
        {
            Assert.Throws<DomainException>(() => new PersonSurname(v));
        }

        [Theory]
        [InlineData(1)]  
        [InlineData(51)]  
        public void PersonSurname_Length_Range_Enforced(int len)
        {
            var v = new string('a', len);
            Assert.Throws<DomainException>(() => new PersonSurname(v));
        }


        [Fact]
        public void PersonSurname_Disallows_Mixed_Alphabets()
        {
            Assert.Throws<DomainException>(() => new PersonSurname("Brownბ"));
        }

        [Theory]
        [InlineData("12345678901")]
        [InlineData("00000000000")]
        public void PersonalNumber_Exactly_11_Digits_Valid(string pn)
        {
            _ = new PersonPersonalNumber(pn);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("1234567890a")]
        [InlineData(" 12345678901 ")]
        public void PersonalNumber_Invalid_Formats_Fail(string pn)
        {
            Assert.Throws<DomainException>(() => new PersonPersonalNumber(pn));
        }

        [Fact]
        public void PhoneNumberNumber_Valid_Shapes()
        {
            _ = new PhoneNumberNumber("+995 599 12-34-56");
            _ = new PhoneNumberNumber("(032) 2 22 22 22");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void PhoneNumberNumber_Empty_Fails(string v)
        {
            Assert.Throws<DomainException>(() => new PhoneNumberNumber(v));
        }

        [Theory]
        [InlineData(3)]   
        [InlineData(51)]  
        public void PhoneNumberNumber_Length_Range_Enforced(int len)
        {
            var v = new string('1', len);
            Assert.Throws<DomainException>(() => new PhoneNumberNumber(v));
        }

        [Theory]
        [InlineData("ABC!@#")]
        [InlineData("123_456")]
        public void PhoneNumberNumber_Invalid_Characters_Fail(string v)
        {
            Assert.Throws<DomainException>(() => new PhoneNumberNumber(v));
        }

        [Fact]
        public void BirthDate_AtLeast_18_Years()
        {
            var today = new DateOnly(2025, 8, 1);
            var ok = new DateOnly(2000, 7, 31);
            _ = new PersonBirthDate(ok, today);
        }

        [Fact]
        public void BirthDate_Cannot_Be_In_Future()
        {
            var today = new DateOnly(2025, 8, 1);
            var future = new DateOnly(2025, 8, 2);
            Assert.Throws<DomainException>(() => new PersonBirthDate(future, today));
        }

        [Fact]
        public void BirthDate_Younger_Than_18_Fails()
        {
            var today = new DateOnly(2025, 8, 1);
            var tooYoung = new DateOnly(2010, 8, 1);
            Assert.Throws<DomainException>(() => new PersonBirthDate(tooYoung, today));
        }

        [Fact]
        public void BirthDate_ToString_YyyyMMdd()
        {
            var d = new PersonBirthDate(new DateOnly(1990, 1, 2));
            Assert.Equal("1990-01-02", d.ToString());
        }
    }
}
