using StkGenCode.Code.Type;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class StoreProCode : CodeBase
    {
        //public FileCode _FileCode;
        //public DataSet _ds;
        //public string _TableName;

        //private string _fileType = ".sql";
        //private string _NewLine = " \r\n";

        //private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string GenSp_GetPageWise()
        {
            string code = "";
            string stroeName = "Sp_Get" + _TableName + "PageWise";
            code += "DROP PROCEDURE [dbo].[" + stroeName + "];" + _NewLine;
            code += "go" + _NewLine;
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + _NewLine;
            code += "    @PageIndex INT = 1  " + _NewLine;
            code += ", @PageSize INT = 10 ,@CommandFilter varchar(max)" + _NewLine;

            code += "AS  " + _NewLine;
            code += "BEGIN  " + _NewLine;
            code += " SET NOCOUNT ON;  " + _NewLine;
            code += " " + _NewLine;
            code += "  " + _NewLine;
            code += " create table #Results " + _NewLine;
            code += "( " + _NewLine;
            code += "[RowNumber] [int],";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                code += "[" + dataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDB(dataColumn.DataType.ToString()) + "," + _NewLine;
            }

            code += ") " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "Declare @Command NVARCHAR(MAX)  " + _NewLine;
            code += " " + _NewLine;
            //code += "--Set @Command = 'SELECT ROW_NUMBER() OVER (ORDER BY [ID1] ASC )AS RowNumber ,* INTO #Results  FROM ["+ _TableName + "] '+ @CommandFilter; " + _NewLine;
            //code += "--Set @Command = 'SELECT ROW_NUMBER() OVER (ORDER BY [AutoID] ASC )AS RowNumber ,*  FROM ["+ _TableName + "] '+ @CommandFilter; " + _NewLine;
            code += " Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [" + _ds.Tables[0].PrimaryKey[0].ColumnName + "] ASC )AS RowNumber ,*  FROM [" + _TableName + "]'+ @CommandFilter; " + _NewLine;

            code += " Set @Command =  @CommandFilter; " + _NewLine;

            code += " " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "EXEC sp_executesql @Command " + _NewLine;
            code += " " + _NewLine;
            code += " declare @RecordCount as int;  " + _NewLine;
            code += " SELECT @RecordCount = COUNT(*)  " + _NewLine;
            code += " FROM #Results  " + _NewLine;
            code += "  " + _NewLine;
            code += " SELECT *,@RecordCount AS RecordCount FROM #Results  " + _NewLine;
            code += " WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1  " + _NewLine;
            code += "  " + _NewLine;
            code += " DROP TABLE #Results  " + _NewLine;
            code += "END  " + _NewLine;
            code += " " + _NewLine;
            code += "  " + _NewLine;

            //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            //{
            //    code += "," + _DataColumn.ColumnName + _NewLine;
            //}

            code += "" + _NewLine;
            code += "" + _NewLine;
            code += "" + _NewLine;
            return code;
        }

        private string GenSp_GetAutocomplete()
        {
            string code = "";

            string stroeName = "Sp_Get" + _TableName + "_Autocomplete";
            code += "DROP PROCEDURE [dbo].[" + stroeName + "];" + _NewLine;
            code += "go" + _NewLine;
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + _NewLine;
            //code += "create PROCEDURE [dbo].[Sp_Getfxrates_family_Autocomplete]  " + _NewLine;
            code += "     @Key_word    nvarchar(50) " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "AS    " + _NewLine;
            code += "BEGIN    " + _NewLine;
            code += " SET NOCOUNT ON;    " + _NewLine;
            code += "   " + _NewLine;
            code += " select top 20 KetText,count(*) as NumberOfkey from  " + _NewLine;
            code += "( " + _NewLine;

            string UnionAll = "union all";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                // Do Something

                //  code += "[" + _DataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDB(_DataColumn.DataType.ToString()) + "," + _NewLine;
                if (dataColumn.DataType.ToString() == "System.String")
                {
                    code += "SELECT  " + _NewLine;
                    code += "      [" + dataColumn.ColumnName + "] As KetText " + _NewLine;
                    code += "        " + _NewLine;
                    code += "  FROM [" + _TableName + "] where [" + dataColumn.ColumnName + "] like ''+@Key_word+'%' " + _NewLine;

                    code += UnionAll + _NewLine;
                }

                //Remove union all
            }

            code = code.Remove(code.Length - (UnionAll.Length + _NewLine.Length)) + _NewLine;
            //code += "SELECT  " + _NewLine;
            //code += "      [Family] As KetText " + _NewLine;
            //code += "        " + _NewLine;
            //code += "  FROM [WEBAPP].[dbo].[fxrates_family] where [Family] like ''+@Key_word+'%' " + _NewLine;
            //code += "union all" + _NewLine;

            code += "  )KeyTable  " + _NewLine;
            code += "  group by KetText " + _NewLine;
            code += "  order by count(*) desc  " + _NewLine;
            code += " " + _NewLine;
            code += "END    " + _NewLine;
            code += "   " + _NewLine;

            code += "" + _NewLine;
            code += "" + _NewLine;
            code += "" + _NewLine;
            return code;
        }

        private string GenGoSplitBatch()
        {
            string code = "";
            code += "" + _NewLine;
            code += "go" + _NewLine;
            code += "" + _NewLine;
            return code;
        }

        public override void Gen()
        {
            string code = "";
            code += GenSp_GetPageWise();
            code += GenGoSplitBatch();
            code += GenSp_GetAutocomplete();

            InnitProperties();
            _FileCode.writeFile(_FileName.StoreProCodeName(), code);
            //_FileCode.writeFile("Sp_" + _TableName + "Store", _code, _fileType);
        }
    }
}