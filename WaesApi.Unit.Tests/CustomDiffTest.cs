using System;
using System.Collections.Generic;
using WaesApi.Utils;
using Xunit;

namespace WaesApi.Unit.Tests
{
    // Since CustomDiff classes are interdependent but very simple and they are stored in the same class file, I wanted to keep the tests under the same idea

    // Test class for CustomDiffResultList
    public class CustomDiffResultListTest
    {
        /// <summary>
        /// Checks if CustomDiffResultList.ToString method returns expected string output.
        /// </summary>
        [Fact]
        public void ToStringOutputsFormattedText()
        {
            var customDiffResultList = new CustomDiffResultList
            {
                new CustomDiffResultItem(10, 20),
                new CustomDiffResultItem(20, 10)
            };
            var expected = "Difference found at position 10 with Length of 20. Difference found at position 20 with Length of 10.";
            Assert.Equal(expected, customDiffResultList.ToString());
        }
    }

    // Test class for CustomDiffResult
    public class CustomDiffResultTest
    {
        /// <summary>
        /// Checks if HasDifferences returns true if list of Differences has at least one item
        /// </summary>
        [Fact]
        public void HasDifferencesReturnsTrue()
        {
            var customDiffResult = new CustomDiffResult
            {
                //Improve: Mock CustomDiffResultItem?
                Differences = new List<CustomDiffResultItem> { new CustomDiffResultItem(0, 1) }
            };

            Assert.True(customDiffResult.HasDifferences);
        }

        /// <summary>
        /// Checks if HasDifferences returns true if list of Differences is null or Empty
        /// </summary>
        [Fact]
        public void HasDifferencesReturnsFalse()
        {
            var customDiffResult = new CustomDiffResult();

            // First asserts when Differences list hasn't been initialized/is null
            Assert.False(customDiffResult.HasDifferences);

            //Improve: Mock CustomDiffResultItem?
            customDiffResult.Differences = new List<CustomDiffResultItem>();

            // Then when it has been initialized but is empty
            Assert.False(customDiffResult.HasDifferences);
        }
    }

    public class CustomDiffTest
    {
        /// <summary>
        /// Checks if when input strings are equal, diff returns expected Result string
        /// </summary>
        [Fact]
        public void DiffDetecsWhenInputStringsAreEqual()
        {
            var customDiffResult = new CustomDiff("this is an input string", "this is an input string").Diff();
            Assert.Equal("The compared strings are equal", customDiffResult.Result);
        }

        /// <summary>
        /// Checks if when input strings have different sizes, diff returns expected Result string
        /// </summary>
        [Fact]
        public void DiffDetecsWhenInputStringsHaveDifferentSizes()
        {
            var customDiffResult = new CustomDiff("this is an input string", "this is an input string 2").Diff();
            Assert.Equal("The compared strings have different sizes", customDiffResult.Result);
        }

        /// <summary>
        /// Checks if when one of the input strings is empty, diff returns expected Result string
        /// </summary>
        [Fact]
        public void DiffDetecsWhenOneInputStringIsEmpty()
        {
            var customDiffResult = new CustomDiff("this is an input string", "").Diff();
            Assert.Equal("The compared strings have different sizes", customDiffResult.Result);
        }

        [Fact]
        public void ConstructorThrowsExceptionWhenLeftArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomDiff(null,""));
        }

        [Fact]
        public void ConstructorThrowsExceptionWhenRightArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomDiff("", null));
        }

        /// <summary>
        /// Checks if when input strings have equal sizes but different contant, diff returns expected Result string and expected Differences insights
        /// </summary>
        [Fact]
        public void DiffDetecsWhenInputStringsHaveSameSizeButDifferentContent()
        {
            var customDiff = new CustomDiff("this iz on imtot string", "this is an input steeeg");
            var customDiffResult = customDiff.Diff();

            Assert.Equal("The compared strings have the same size but the content is different", customDiffResult.Result);
            Assert.True(customDiffResult.Differences.Count > 0);

            Assert.Equal(6, customDiffResult.Differences[0].StartPosition);
            Assert.Equal(1, customDiffResult.Differences[0].Length);

            Assert.Equal(8, customDiffResult.Differences[1].StartPosition);
            Assert.Equal(1, customDiffResult.Differences[1].Length);

            Assert.Equal(12, customDiffResult.Differences[2].StartPosition);
            Assert.Equal(3, customDiffResult.Differences[2].Length);

            Assert.Equal(19, customDiffResult.Differences[3].StartPosition);
            Assert.Equal(3, customDiffResult.Differences[3].Length);

        }
    }
}
