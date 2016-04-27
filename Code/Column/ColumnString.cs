using System.Data;

namespace StkGenCode.Code.Column
{
    public class ColumnString
    {
        /// <summary>
        ///  $('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="format">  ".chekBox{0},"</param>
        /// <returns></returns>
        public static string GenLineString(DataSet _ds, string _TableName, string format)
        {
            string code = "";

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                //{
                //    primary = true;
                //}
                if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                {
                    continue;
                }

                code += string.Format(format, _DataColumn);
            }

            return code;
        }
    }
}