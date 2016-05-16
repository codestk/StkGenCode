namespace StkGenCode.Code.Type
{
    public class DbTypeConversion
    {
        /// <summary>
        ///     Convert To Store Type
        /// </summary>
        /// <param name="dotNetType"></param>
        /// <returns></returns>
        public static string CTypeNetToTypeDb(string dotNetType)
        {
            string type;
            if (dotNetType == "System.Guid")
            {
                type = " [nvarchar](255)";
            }
            else if
                (dotNetType == "System.Int32")
            {
                type = " [int] ";
            }
            else if (dotNetType == "System.Int16")
            {
                type = " [int] ";
            }
            else if (dotNetType == "System.Decimal")
            {
                type = " [float] ";
            }
            else if (dotNetType == "System.DateTime")
            {
                type = " [datetime] ";
            }
            else if (dotNetType == "System.Boolean")
            {
                type = " [bit] ";
            }
            else
            {
                type = " [nvarchar](255) ";
            }

            return type;
        }
    }
}