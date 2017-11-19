using StkGenCode.Code.Template;
using System.Collections.Generic;
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
        public static string GenLineString(DataSet ds, string format, List<string> expColumnList = null)
        {
            var code = "";
            if (expColumnList == null)
            {
                expColumnList = new List<string>();
                expColumnList.Add("");
            }

            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                //if (CodeBase.ExceptionType.Contains(dataColumn.DataType.ToString()))
           
                if (CodeBase.ExceptionType.Contains(dataColumn.DataType.ToString()) || expColumnList.Contains(dataColumn.ColumnName))
                {
                    continue;
                }

                code += string.Format(format, dataColumn);
            }

            return code;
        }
    }
}