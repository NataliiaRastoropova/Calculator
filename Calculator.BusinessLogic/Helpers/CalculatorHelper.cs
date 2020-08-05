using System;

namespace Calculator.BusinessLogic.Helpers
{
    internal static class CalculatorHelper
    {
        internal static decimal GetResult(char operation, decimal firstVal, decimal secondVal)
        {
            return operation switch
            {
                '+' => firstVal + secondVal,
                '-' => firstVal - secondVal,
                '/' => firstVal / secondVal,
                '*' => firstVal * secondVal,
                _ => throw new NotImplementedException($"Operation {operation} not implemented"),
            };
        }
    }
}
