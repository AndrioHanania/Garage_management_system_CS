namespace Ex03.GarageLogic
{
    public class ExceptionMessage
    {
        public static string FormatOutOfRangeExceptionMessage(
            string i_InvalidParameter, float i_MinRange, float i_MaxRange)
        {
            return string.Format("The range of {0} must be {1} - {2}",
                i_InvalidParameter, i_MinRange, i_MaxRange);
        }

        public static string FormatBadString(string i_InvalidParameter)
        {
            return string.Format("The string {0} is empty or not contains just letters",
                i_InvalidParameter);
        }

        public static string FormatBadPhone(string i_InvalidParameter)
        {
            return string.Format("The phone {0} is empty or not contains just digits",
                i_InvalidParameter);
        }

        public static string FormatUndefinedEnumValueExceptionMessage(string i_InvalidParameter)
        {
            return string.Format("{0} argument value is not defined in enum", i_InvalidParameter);
        }
    }
}