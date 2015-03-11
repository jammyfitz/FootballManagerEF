using FootballManagerEF.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Tests.Services
{
    [TestFixture]
    public class StringCipherServiceTest
    {
        [Test]
        public void StringCipherService_EncryptReturnsEncodedValue()
        {
            //Arrange 
            string expectedResult = "s+ujX2T9QSr1pVDNYLFU8A==";

            //Act
            var result = StringCipherService.Encrypt("test", "something");

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void StringCipherService_DecryptReturnsDecodedValue()
        {
            //Arrange 
            string expectedResult = "test";

            //Act
            var result = StringCipherService.Decrypt("s+ujX2T9QSr1pVDNYLFU8A==", "something");

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
