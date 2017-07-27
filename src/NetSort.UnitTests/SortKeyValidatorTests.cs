using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetSort.UnitTests.Models;
using NetSort.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetSort.UnitTests
{
    [TestClass]
    public class SortKeyValidatorTests
    {
        [TestMethod]
        public void SortKeyValidator_IsKeyValid_WithKeyFound_ShouldReturnTrue()
        {
            var sut = new SortKeyValidator();
            var isValid = sut.IsKeyValid<Person>("name");

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SortKeyValidator_IsKeyValid_WithKeyNotFound_ShouldReturnFalse()
        {
            var sut = new SortKeyValidator();
            var isValid = sut.IsKeyValid<Person>("someKeyThatDoesntExist");

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void SortKeyValidator_IsKeyValid_WithKeyForIComparableComplexObject_ShouldReturnTrue()
        {
            var sut = new SortKeyValidator();
            var isValid = sut.IsKeyValid<Person>("complexAddress");

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SortKeyValidator_IsKeyValid_WithKeyForNonIComparableComplexObject_ShouldReturnFalse()
        {
            var sut = new SortKeyValidator();
            var isValid = sut.IsKeyValid<Person>("address");

            Assert.IsFalse(isValid);
        }
    }
}
