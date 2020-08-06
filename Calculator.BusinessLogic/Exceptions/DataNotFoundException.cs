using System;

namespace Calculator.BusinessLogic.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message):base(message)
        {

        }
    }
}
