namespace RWAN10T.Api.Utils
{
    public static class NumberHelper
    {
        public static decimal ConvertToDecimal(string number)
        {
            decimal decimalValue;

            if (Decimal.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimalValue))
                return decimalValue;

            return 0;
        }

        public static bool IsNumeric(string number)
        {
            decimal decimalValue;

            bool isNumber = Decimal.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimalValue);

            return isNumber;
        }
    }
}
