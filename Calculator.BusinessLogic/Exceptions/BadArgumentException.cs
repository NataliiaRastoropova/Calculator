using System;

namespace Calculator.BusinessLogic.Exceptions
{
    public class BadArgumentException:Exception
    {
        public BadArgumentException(string message):base(message)
        {

        }
    }
}
