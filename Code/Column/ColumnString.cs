using StkGenCode.Code.Template;
using System.Data;

namespace StkGenCode.Code.Column
{
    public class ColumnString
    {
        /// <summary>
        /// $('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService
        /// ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="format">".chekBox{0},"</param>
        /// <returns></returns>
        public static string GenLineString(DataSet ds, string format)
        {
            var code = "";

            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                if (CodeBase.ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }
                code += string.Format(format, dataColumn);
            }

            return code;
        }
    }
}