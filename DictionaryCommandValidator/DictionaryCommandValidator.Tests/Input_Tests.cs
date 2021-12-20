using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DictionaryCommandValidator.Tests
{
    [TestFixture]
    class Input_Tests
    {
        [Test]
        public void Validator_ValidInput()
        {
            Assert.IsTrue(Validator.Do(new object(), new ValidationProperty[0], out string message));
        }

        [Test]
        public void Validator_InvalidInput_Command()
        {
            Assert.Throws<ArgumentNullException>(() => Validator.Do(null, new ValidationProperty[0], out string message));
        }

        [Test]
        public void Validator_InvalidInput_Properties()
        {
            Assert.Throws<ArgumentNullException>(() => Validator.Do(new Dictionary<string, object>(), null, out string message));
        }
    }
}
