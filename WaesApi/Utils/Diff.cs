using System;
using System.Collections.Generic;
using System.Text;

namespace WaesApi.Utils
{
    public class CustomDiffResultItem
    {
        public CustomDiffResultItem(int startPosition, int length)
        {
            StartPosition = startPosition;
            Length = length;
        }
        public int StartPosition { get; set; }
        public int Length { get; set; }
    }

    public struct CustomDiffResult
    {
        public string Result { get; set; }
        public List<CustomDiffResultItem> Differences { get; set; }

        public bool HasDifferences
        {
            get
            {
                return Differences != null && Differences.Count > 0;
            }
        }
    }

    public class CustomDiffResultList : List<CustomDiffResultItem>
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ForEach((item) =>
            {
                builder.Append(String.Format("Difference found at position {0} with Length of {1}. ", item.StartPosition.ToString(), item.Length.ToString()));
            });
            return builder.ToString().Trim();
        }
    }

    public class CustomDiff
    {
        readonly string left;
        readonly string right;

        /// <summary>
        /// Initialize CustomDiff class
        /// </summary>
        /// <param name="left">Firt string to execute diff on</param>
        /// <param name="right">Other string to execute diff on</param>
        public CustomDiff(string left, string right)
        {
            // The Diff method can handle when one or both are empty 
            this.left = left ?? throw new ArgumentNullException(nameof(left), "Please provide a valid string on 'left' argument");
            this.right = right ?? throw new ArgumentNullException(nameof(right), "Please provide a valid string on 'right' argument");
        }

        /// <summary>
        /// Calculate Difference between strings initialized in the constructor of this class
        /// </summary>
        /// <returns> Custom Struct containing the results of the comparison </returns>
        public CustomDiffResult Diff()
        {
            CustomDiffResult result = new CustomDiffResult();

            if (left.Equals(right))
            {
                result.Result = "The compared strings are equal";
                return result;
            }

            if (!left.Length.Equals(right.Length))
                result.Result = "The compared strings have different sizes";
            else
            {
                result.Result = "The compared strings have the same size but the content is different";
                result.Differences = calculateSameSizeDifferences();
            }

            return result;

        }

        List<CustomDiffResultItem> calculateSameSizeDifferences()
        {
            List<CustomDiffResultItem> differences = new List<CustomDiffResultItem>();

            // Initialize control variables 
            int currentDifferenceStartPosition = 0, currentDifferenceLength = 0;

            // For each character in left string, compare it with same position on the right string
            for (int x = 0; x < left.Length; x++)
            {
                if (left[x] != right[x])
                {
                    /* If current characters are different then we store where the difference starts and keep adding to the length of the difference 
                        * while strings are different */

                    if (currentDifferenceStartPosition == 0)
                        currentDifferenceStartPosition = x;

                    currentDifferenceLength++;
                }
                else
                {
                    /* If characters are equal we need to check if it means that a difference has ended and if so, we store a new difference item
                        * and reset the control variables so the code can "start over" when there's a new difference */
                    if (currentDifferenceStartPosition > 0)
                    {
                        differences.Add(new CustomDiffResultItem(currentDifferenceStartPosition, currentDifferenceLength));
                        currentDifferenceStartPosition = 0;
                        currentDifferenceLength = 0;
                    }
                }
            }
            return differences;
        }
    }
}
