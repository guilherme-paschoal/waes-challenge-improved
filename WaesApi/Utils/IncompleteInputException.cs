using System;
namespace WaesApi.Utils
{
    public class IncompleteInputException : ApplicationException
    {
        public IncompleteInputException() 
            : base("Both sides of Diff need to be inserted before getting a result") 
        {
        }
    }
}
