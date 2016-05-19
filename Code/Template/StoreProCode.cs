using StkGenCode.Code.Type;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class StoreProCode : CodeBase
    {
        //อันเก่าก่อนแก้เป็น Store
        //private string GenSp_GetPageWise()
        //{
        //    string code = "";
        //    string stroeName = "Sp_Get" + _TableName + "PageWise";
        //    code += "DROP PROCEDURE [dbo].[" + stroeName + "];" + _NewLine;
        //    code += "go" + _NewLine;
        //    code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + _NewLine;
        //    code += "    @PageIndex INT = 1  " + _NewLine;
        //    code += ", @PageSize INT = 10 ,@CommandFilter varchar(max)" + _NewLine;

        //    code += "AS  " + _NewLine;
        //    code += "BEGIN  " + _NewLine;
        //    code += " SET NOCOUNT ON;  " + _NewLine;
        //    code += " " + _NewLine;
        //    code += "  " + _NewLine;
        //    code += " create table #Results " + _NewLine;
        //    code += "( " + _NewLine;
        //    code += "[RowNumber] [int],";
        //    foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
        //    {
        //        code += "[" + dataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "," + _NewLine;
        //    }

        //    code += ") " + _NewLine;
        //    code += " " + _NewLine;
        //    code += " " + _NewLine;
        //    code += "Declare @Command NVARCHAR(MAX)  " + _NewLine;
        //    code += " " + _NewLine;
        //    //code += "--Set @Command = 'SELECT ROW_NUMBER() OVER (ORDER BY [ID1] ASC )AS RowNumber ,* INTO #Results  FROM ["+ _TableName + "] '+ @CommandFilter; " + _NewLine;
        //    //code += "--Set @Command = 'SELECT ROW_NUMBER() OVER (ORDER BY [AutoID] ASC )AS RowNumber ,*  FROM ["+ _TableName + "] '+ @CommandFilter; " + _NewLine;
        //    code += " Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [" + _ds.Tables[0].PrimaryKey[0].ColumnName + "] ASC )AS RowNumber ,*  FROM [" + _TableName + "]'+ @CommandFilter; " + _NewLine;

        //    code += " Set @Command =  @CommandFilter; " + _NewLine;

        //    code += " " + _NewLine;
        //    code += " " + _NewLine;
        //    code += " " + _NewLine;
        //    code += " " + _NewLine;
        //    code += "EXEC sp_executesql @Command " + _NewLine;
        //    code += " " + _NewLine;
        //    code += " declare @RecordCount as int;  " + _NewLine;
        //    code += " SELECT @RecordCount = COUNT(*)  " + _NewLine;
        //    code += " FROM #Results  " + _NewLine;
        //    code += "  " + _NewLine;
        //    code += " SELECT *,@RecordCount AS RecordCount FROM #Results  " + _NewLine;
        //    code += " WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1  " + _NewLine;
        //    code += "  " + _NewLine;
        //    code += " DROP TABLE #Results  " + _NewLine;
        //    code += "END  " + _NewLine;
        //    code += " " + _NewLine;
        //    code += "  " + _NewLine;

        //    //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
        //    //{
        //    //    code += "," + _DataColumn.ColumnName + _NewLine;
        //    //}

        //    code += "" + _NewLine;
        //    code += "" + _NewLine;
        //    code += "" + _NewLine;
        //    return code;
        //}

        private string GenSp_GetPageWise()
        {
            string code = "";
            string stroeName = "Sp_Get" + _TableName + "PageWise";
            code += "DROP PROCEDURE [dbo].[" + stroeName + "];" + _NewLine;
            code += "go" + _NewLine;
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + _NewLine;

            code += "/* Optional Filters for Dynamic Search*/" + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                code += "@" + dataColumn.ColumnName + " " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + _NewLine;
            }
            code += "/*– Pagination Parameters */" + _NewLine;
            code += "@PageIndex INT = 1 ," + _NewLine;
            code += "@PageSize INT = 10 ," + _NewLine;
            code += "/*– Sorting Parameters */" + _NewLine;
            code += $"@SortColumn NVARCHAR(20) = '{_ds.Tables[0].PrimaryKey[0].ColumnName}'," + _NewLine;
            code += "@SortOrder NVARCHAR(4) ='ASC'" + _NewLine;
            code += "AS  " + _NewLine;
            code += "BEGIN  " + _NewLine;
            code += " SET NOCOUNT ON;  " + _NewLine;
            code += " " + _NewLine;
            code += "  " + _NewLine;
            //code += " create table #Results " + _NewLine;
            //code += "( " + _NewLine;
            //code += "[RowNumber] [int],";
            ////foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            ////{
            ////    code += "[" + dataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "," + _NewLine;
            ////}

            //code += ") " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            //code += "Declare @Command NVARCHAR(MAX)  " + _NewLine;
            code += "/*–Declaring Local Variables corresponding to parameters for modification */" + _NewLine;
            code += "DECLARE ";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                code += "@l" + dataColumn.ColumnName + " " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + _NewLine;
            }

            //Stattic Vriable
            code += "@lPageNbr INT," + _NewLine;
            code += "@lPageSize INT," + _NewLine;
            code += "@lSortCol NVARCHAR(20)," + _NewLine;
            code += "@lFirstRec INT," + _NewLine;
            code += "@lLastRec INT," + _NewLine;
            code += "@lTotalRows INT" + _NewLine;

            code += "/*Setting Local Variables*/" + _NewLine;
            /*Setting Local Variables*/

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += "SET @l" + dataColumn.ColumnName + " =@" + dataColumn.ColumnName + _NewLine;
                }
                else
                {
                    code += $"SET @l{dataColumn.ColumnName} = LTRIM(RTRIM(@{dataColumn.ColumnName}))" + _NewLine;
                }
            }

            //Set stattic vaiable
            code += "SET @lPageNbr = @PageIndex" + _NewLine;
            code += "    SET @lPageSize = @PageSize" + _NewLine;
            code += "    SET @lSortCol = LTRIM(RTRIM(@SortColumn))" + _NewLine;
            code += " " + _NewLine;
            code += "    SET @lFirstRec = ( @lPageNbr - 1 ) * @lPageSize" + _NewLine;
            code += "    SET @lLastRec = ( @lPageNbr * @lPageSize + 1 )" + _NewLine;
            code += "    SET @lTotalRows = @lFirstRec - @lLastRec + 1" + _NewLine;

            //================================================================================

            code += $"; WITH {_TableName}Result" + _NewLine;
            code += "AS(" + _NewLine;
            code += "SELECT ROW_NUMBER() OVER(ORDER BY " + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //code += $"CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder ='ASC')" + _NewLine;
                //code += $"        THEN ContactID" + _NewLine;
                //code += "END ASC," + _NewLine;
                code += _NewLine + $"CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder='ASC')" + _NewLine;
                code += $"                   THEN {dataColumn.ColumnName}" + _NewLine;
                code += "       END ASC," + _NewLine;
                code += $"       CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder='DESC')" + _NewLine;
                code += $"                  THEN {dataColumn.ColumnName}" + _NewLine;
                code += "       END DESC,";
            }
            code = code.TrimEnd(',');
            code += "  ) AS ROWNUM," + _NewLine;
            code += "Count(*) over() AS RecordCount," + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                code += _NewLine + $" {dataColumn.ColumnName},";
            }
            code = code.TrimEnd(',') + _NewLine;
            code += $" FROM {_TableName}" + _NewLine;

            #region Where

            code += $"WHERE" + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if ((dataColumn.DataType.ToString() == "System.Int32") || (dataColumn.DataType.ToString() == "System.DateTime") || (dataColumn.DataType.ToString() == "System.Decimal"))
                {
                    code += $"(@l{dataColumn.ColumnName} IS NULL OR {dataColumn.ColumnName} = @l{dataColumn.ColumnName}) AND" + _NewLine;
                }
                else
                {
                    code += $"(@l{dataColumn.ColumnName} IS NULL OR {dataColumn.ColumnName} LIKE '%' +@l{dataColumn.ColumnName} + '%') AND " + _NewLine;
                }
            }
            code = code.Remove(code.Length - ("AND".Length + _NewLine.Length + 1)) + _NewLine;
            code += ")" + _NewLine;

            #endregion Where

            code += "SELECT   RecordCount," + _NewLine;
            code += " ROWNUM," + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                code += _NewLine + $"{dataColumn.ColumnName},";
            }
            code = code.TrimEnd(',');

            code += $" FROM {_TableName}Result" + _NewLine;
            code += " WHERE" + _NewLine;
            code += "         ROWNUM > @lFirstRec" + _NewLine;
            code += "               AND ROWNUM < @lLastRec" + _NewLine;
            code += " ORDER BY ROWNUM ASC" + _NewLine;

            code += "END" + _NewLine;
            code += "GO" + _NewLine;
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

        private string GenSp_GetAutocompleteByColumn()
        {
            string code = "";

            string stroeName = "Sp_Get" + _TableName + "_Autocomplete";
            code += "DROP PROCEDURE [dbo].[" + stroeName + "];" + _NewLine;
            code += "go" + _NewLine;
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + _NewLine;
            //code += "create PROCEDURE [dbo].[Sp_Getfxrates_family_Autocomplete]  " + _NewLine;
            //code += "     @Key_word    nvarchar(50) " + _NewLine;

            code += "@Column  nvarchar(50),";
            code += "@keyword nvarchar(50)";

            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "AS    " + _NewLine;
            code += "BEGIN    " + _NewLine;
            code += "SET NOCOUNT ON;    " + _NewLine;
            code += "   " + _NewLine;
            code += " select top 20 KetText,count(*) as NumberOfkey from  " + _NewLine;
            code += "( " + _NewLine;

            string sqlWhenCase = "CASE";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                // Do Something
                sqlWhenCase += $"  WHEN (@Column = '{dataColumn.ColumnName}') THEN CONVERT(varchar, {dataColumn.ColumnName} )";

                //  code += "[" + _DataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDB(_DataColumn.DataType.ToString()) + "," + _NewLine;

                //Remove union all
            }
            sqlWhenCase += "END";
            code += "SELECT  " + _NewLine;
            code += "      " + sqlWhenCase + " As KetText " + _NewLine;
            code += "        " + _NewLine;
            code += "  FROM [" + _TableName + "] where " + sqlWhenCase + " like ''+@keyword+'%' " + _NewLine;
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

        private string GenSp_GetTradeFromCategory_UpdateColumn()
        {
            string code = "";
            code += $"DROP PROCEDURE [dbo].[Sp_Get{_TableName}_UpdateColumn];" + _NewLine;
            code += GenGoSplitBatch();
            code += "  " + _NewLine;
            code += "       " + _NewLine;
            code += $"      create PROCEDURE [dbo].[Sp_Get{_TableName}_UpdateColumn] " + _NewLine;
            code += $"        @{_ds.Tables[0].PrimaryKey[0]} {DbTypeConversion.CTypeNetToTypeDb(_ds.Tables[0].PrimaryKey[0].DataType.ToString()) },@Column  nvarchar(max),@Data nvarchar(max)   " + _NewLine;

            code += "   AS     " + _NewLine;
            code += "       BEGIN     " + _NewLine;
            code += "       --SET NOCOUNT ON;     " + _NewLine;
            code += "         " + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ColumnName)
                {
                    continue;
                }
                //code += "@" + dataColumn.ColumnName + " " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + _NewLine;

                code += $"         if  @Column = '{dataColumn.ColumnName}'" + _NewLine;
                code += "           BEGIN " + _NewLine;
                code += $"           UPDATE   {_TableName} SET {dataColumn.ColumnName}=@Data where {_ds.Tables[0].PrimaryKey[0]} = @{_ds.Tables[0].PrimaryKey[0]};  " + _NewLine;
                code += "         END " + _NewLine;
            }

            //code += "         if  @Column = 'Colin'" + _NewLine;
            //code += "           BEGIN " + _NewLine;
            //code += "           UPDATE   TradeFromCategory SET CategoryName=@keyword where CategoryID = @CategoryID;  " + _NewLine;
            //code += "         END " + _NewLine;

            code += "       END     " + _NewLine;
            code += GenGoSplitBatch();
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
            code += GenSp_GetAutocompleteByColumn();
            code += GenSp_GetTradeFromCategory_UpdateColumn();

            InnitProperties();
            _FileCode.WriteFile(_FileName.StoreProCodeName(), code);
            //_FileCode.writeFile("Sp_" + _TableName + "Store", _code, _fileType);
        }
    }
}