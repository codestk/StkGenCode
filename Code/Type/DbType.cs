namespace StkGenCode.Code.Type
{
    public class DbTypeConversion
    {
        /// <summary>
        /// Convert To Store Type
        /// </summary>
        /// <param name="DotNetType"></param>
        /// <returns></returns>
        public static string CTypeNetToTypeDB(string DotNetType)
        {
            string Type = "";
            if ((DotNetType == "System.Guid"))
            {
                Type = " [nvarchar](255)";
            }
            else if
            ((DotNetType == "System.Int32"))
            {
                Type = " [int] ";
            }
            else if (DotNetType == "System.Int16")
            {
                Type = " [int] ";
            }
            else if (DotNetType == "System.Decimal")
            {
                Type = " [float] ";
            }
            else if (DotNetType == "System.DateTime")
            {
                Type = " [datetime] ";
            }
            else if (DotNetType == "System.Boolean")
            {
                Type = " [bit] ";
            }
            else
            {
                Type = " [nvarchar](255) ";
            }

            return Type;
        }
    }
}