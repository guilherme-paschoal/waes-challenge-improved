using System;
namespace WaesApi.Utils
{
    public static class DecodingHelper
    {
        /// <summary>
        /// Decodes a Base 64 encoded string back to string and removes "\", "\r" and "\n" characters
        /// </summary>
        /// <param name="input">Base 64 encoded string</param>
        /// <returns>Decoded string</returns>
        public static string Decode(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) { throw new ArgumentNullException(nameof(input), "Please provide a valid input"); }

            // Convert.FromBase64String already throws FormatException if input is invalid. Should I wrap this in a try/catch and thrown a new exception with 
            // a friendlier message but keeping the same FormatException type?
            var byteArray = Convert.FromBase64String(input);
            var stringified = System.Text.Encoding.Default.GetString(byteArray);

            return stringified.Replace("\n", "").Replace("\r", "").Replace(@"\", "");
        }
    }
}
