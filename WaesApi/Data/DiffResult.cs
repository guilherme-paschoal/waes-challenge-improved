using System;
using System.Collections.Generic;
using System.Linq;

namespace WaesApi.Data
{
    public class DiffResult
    {

        readonly string left;
        readonly string right;

        public DiffResult(string left, string right)
        {
            this.left = left;
            this.right = right;
            Differences = new List<string>();
            Execute();
        }

        public string Result { get; set; }
        public List<string> Differences { get; set; }

        public string GetDifferencesFlat() {
            return Differences.Any() ? Differences.Aggregate((current, next) => current += " | " + next) : "";
        }

        private void Execute()
        {
            if (left.Equals(right)) 
            {
                Result = "The compared strings are equal";
                return;
            }

            if (!left.Length.Equals(right.Length))
                Result = "The compared strings have different sizes";
            else
            {
                Result = "The compared strings have the same size but the content is different";
                Calculate();
            }
        }

        // Since I made this a private calculation, it can be a void that accesses the class instant variables and the Unit tests can test Execute() and this
        // method as a single unit
        private void Calculate() {

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
                        Differences.Add(String.Format("Difference at {0} with length {1}", currentDifferenceStartPosition, currentDifferenceLength));
                        currentDifferenceStartPosition = 0;
                        currentDifferenceLength = 0;
                    }
                }
            }
        } 
    }
}
