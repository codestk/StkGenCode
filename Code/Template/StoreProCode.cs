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
         
            var code = "";
            var stroeName = "Sp_Get" + TableName + "PageWise";
            code += "   DROP PROCEDURE   [dbo].[" + stroeName + "];" + NewLine;
            code += "go" + NewLine;
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + NewLine;

            code += "/* Optional Filters for Dynamic Search*/" + NewLine;
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                code += "@" + dataColumn.ColumnName + " " +
                        DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + NewLine;
            }
            code += "/*– Pagination Parameters */" + NewLine;
            code += "@PageIndex INT = 1 ," + NewLine;
            code += "@PageSize INT = 10 ," + NewLine;
            code += "/*– Sorting Parameters */" + NewLine;
            code += $"@SortColumn NVARCHAR(20) = '{Ds.Tables[0].PrimaryKey[0].ColumnName}'," + NewLine;
            code += "@SortOrder NVARCHAR(4) ='ASC'" + NewLine;
            code += "AS  " + NewLine;
            code += "BEGIN  " + NewLine;
            code += " SET NOCOUNT ON;  " + NewLine;
            code += " " + NewLine;
            code += "  " + NewLine;
            //code += " create table #Results " + _NewLine;
            //code += "( " + _NewLine;
            //code += "[RowNumber] [int],";
            ////foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            ////{
            ////    code += "[" + dataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "," + _NewLine;
            ////}

            //code += ") " + _NewLine;
            code += " " + NewLine;
            code += " " + NewLine;
            //code += "Declare @Command NVARCHAR(MAX)  " + _NewLine;
            code += "/*–Declaring Local Variables corresponding to parameters for modification */" + NewLine;
            code += "DECLARE ";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                code += "@l" + dataColumn.ColumnName + " " +
                        DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + NewLine;
            }

            //Stattic Vriable
            code += "@lPageNbr INT," + NewLine;
            code += "@lPageSize INT," + NewLine;
            code += "@lSortCol NVARCHAR(20)," + NewLine;
            code += "@lFirstRec INT," + NewLine;
            code += "@lLastRec INT," + NewLine;
            code += "@lTotalRows INT" + NewLine;

            code += "/*Setting Local Variables*/" + NewLine;
            /*Setting Local Variables*/

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += "SET @l" + dataColumn.ColumnName + " =@" + dataColumn.ColumnName + NewLine;
                }
                else
                {
                    code += $"SET @l{dataColumn.ColumnName} = LTRIM(RTRIM(@{dataColumn.ColumnName}))" + NewLine;
                }
            }

            //Set stattic vaiable
            code += "SET @lPageNbr = @PageIndex" + NewLine;
            code += "    SET @lPageSize = @PageSize" + NewLine;
            code += "    SET @lSortCol = LTRIM(RTRIM(@SortColumn))" + NewLine;
            code += " " + NewLine;
            code += "    SET @lFirstRec = ( @lPageNbr - 1 ) * @lPageSize" + NewLine;
            code += "    SET @lLastRec = ( @lPageNbr * @lPageSize + 1 )" + NewLine;
            code += "    SET @lTotalRows = @lFirstRec - @lLastRec + 1" + NewLine;

            //================================================================================

            code += $"; WITH {TableName}Result" + NewLine;
            code += "AS(" + NewLine;
            code += "SELECT ROW_NUMBER() OVER(ORDER BY " + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                //code += $"CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder ='ASC')" + _NewLine;
                //code += $"        THEN ContactID" + _NewLine;
                //code += "END ASC," + _NewLine;
                code += NewLine + $"CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder='ASC')" + NewLine;
                code += $"                   THEN {dataColumn.ColumnName}" + NewLine;
                code += "       END ASC," + NewLine;
                code += $"       CASE WHEN (@lSortCol = '{dataColumn.ColumnName}' AND @SortOrder='DESC')" + NewLine;
                code += $"                  THEN {dataColumn.ColumnName}" + NewLine;
                code += "       END DESC,";
            }
            code = code.TrimEnd(',');
            code += "  ) AS ROWNUM," + NewLine;
            code += "Count(*) over() AS RecordCount," + NewLine;
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                code += NewLine + $" {dataColumn.ColumnName},";
            }
            code = code.TrimEnd(',') + NewLine;
            code += $" FROM {TableName}" + NewLine;

            #region Where

            code += "WHERE" + NewLine;
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                if ((dataColumn.DataType.ToString() == "System.Int32") ||
                    (dataColumn.DataType.ToString() == "System.DateTime") ||
                    (dataColumn.DataType.ToString() == "System.Decimal")||

                    (dataColumn.DataType.ToString() == "System.Boolean")
                    
                    )
                {
                    code +=
                        $"(@l{dataColumn.ColumnName} IS NULL OR {dataColumn.ColumnName} = @l{dataColumn.ColumnName}) AND" +
                        NewLine;
                }
                else
                {
                    code +=
                        $"(@l{dataColumn.ColumnName} IS NULL OR {dataColumn.ColumnName} LIKE '%' +@l{dataColumn.ColumnName} + '%') AND " +
                        NewLine;
                }
            }
            code = code.Remove(code.Length - ("AND".Length + NewLine.Length + 1)) + NewLine;
            code += ")" + NewLine;

            #endregion Where

            code += "SELECT   RecordCount," + NewLine;
            code += " ROWNUM," + NewLine;
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                code += NewLine + $"{dataColumn.ColumnName},";
            }
            code = code.TrimEnd(',');

            code += $" FROM {TableName}Result" + NewLine;
            code += " WHERE" + NewLine;
            code += "         ROWNUM > @lFirstRec" + NewLine;
            code += "               AND ROWNUM < @lLastRec" + NewLine;
            code += " ORDER BY ROWNUM ASC" + NewLine;

            code += "END" + NewLine;
            code += "GO" + NewLine;
            code += "" + NewLine;
            return code;
        }

        private string GenSp_GetAutocomplete()
        {
            var code = "";

            var stroeName = "Sp_Get" + TableName + "_Autocomplete";
            code += "DROP PROCEDURE   [dbo].[" + stroeName + "];" + NewLine;
            code += GenGoSplitBatch();
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + NewLine;
            //code += "create PROCEDURE [dbo].[Sp_Getfxrates_family_Autocomplete]  " + _NewLine;
            code += "     @Key_word    nvarchar(50) " + NewLine;
            code += "  " + NewLine;
            code += "  " + NewLine;
            code += "AS    " + NewLine;
            code += "BEGIN    " + NewLine;
            code += " SET NOCOUNT ON;    " + NewLine;
            code += "   " + NewLine;
            code += " select top 20 KetText,count(*) as NumberOfkey from  " + NewLine;
            code += "( " + NewLine;

            var UnionAll = "union all";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                // Do Something
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }
                //  code += "[" + _DataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDB(_DataColumn.DataType.ToString()) + "," + _NewLine;
                if (dataColumn.DataType.ToString() == "System.String")
                {
                    code += "SELECT  " + NewLine;
                    code += "      [" + dataColumn.ColumnName + "] As KetText " + NewLine;
                    code += "        " + NewLine;
                    code += "  FROM [" + TableName + "] where [" + dataColumn.ColumnName + "] like ''+@Key_word+'%' " +
                            NewLine;

                    code += UnionAll + NewLine;
                }

                //Remove union all
            }

            code = code.Remove(code.Length - (UnionAll.Length + NewLine.Length)) + NewLine;
            //code += "SELECT  " + _NewLine;
            //code += "      [Family] As KetText " + _NewLine;
            //code += "        " + _NewLine;
            //code += "  FROM [WEBAPP].[dbo].[fxrates_family] where [Family] like ''+@Key_word+'%' " + _NewLine;
            //code += "union all" + _NewLine;

            code += "  )KeyTable  " + NewLine;
            code += "  group by KetText " + NewLine;
            code += "  order by count(*) desc  " + NewLine;
            code += " " + NewLine;
            code += "END    " + NewLine;
            code += "   " + NewLine;

            code += GenGoSplitBatch();
            return code;
        }

        private string GenSp_GetAutocompleteByColumn()
        {
            var code = "";

            var stroeName = "Sp_Get" + TableName + "_Autocomplete";
            code += "DROP PROCEDURE   [dbo].[" + stroeName + "];" + NewLine;
            code += GenGoSplitBatch();
            code += "CREATE PROCEDURE [dbo].[" + stroeName + "]" + NewLine;
            //code += "create PROCEDURE [dbo].[Sp_Getfxrates_family_Autocomplete]  " + _NewLine;
            //code += "     @Key_word    nvarchar(50) " + _NewLine;

            code += "@Column  nvarchar(50),";
            code += "@keyword nvarchar(50)";

            code += "  " + NewLine;
            code += "  " + NewLine;
            code += "AS    " + NewLine;
            code += "BEGIN    " + NewLine;
            code += "SET NOCOUNT ON;    " + NewLine;
            code += "   " + NewLine;
            code += " select top 20 KetText,count(*) as NumberOfkey from  " + NewLine;
            code += "( " + NewLine;

            var sqlWhenCase = "CASE";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }
                // Do Something
                sqlWhenCase +=
                    $"  WHEN (@Column = '{dataColumn.ColumnName}') THEN CONVERT(varchar, {dataColumn.ColumnName} )";

                //  code += "[" + _DataColumn.ColumnName + "] " + DbTypeConversion.CTypeNetToTypeDB(_DataColumn.DataType.ToString()) + "," + _NewLine;

                //Remove union all
            }
            sqlWhenCase += "END";
            code += "SELECT  " + NewLine;
            code += "      " + sqlWhenCase + " As KetText " + NewLine;
            code += "        " + NewLine;
            code += "  FROM [" + TableName + "] where " + sqlWhenCase + " like ''+@keyword+'%' " + NewLine;
            //code += "SELECT  " + _NewLine;
            //code += "      [Family] As KetText " + _NewLine;
            //code += "        " + _NewLine;
            //code += "  FROM [WEBAPP].[dbo].[fxrates_family] where [Family] like ''+@Key_word+'%' " + _NewLine;
            //code += "union all" + _NewLine;

            code += "  )KeyTable  " + NewLine;
            code += "  group by KetText " + NewLine;
            code += "  order by count(*) desc  " + NewLine;
            code += " " + NewLine;
            code += "END    " + NewLine;
            code += "   " + NewLine;

            code += GenGoSplitBatch();

            return code;
        }

        private string GenSp_GetTradeFromCategory_UpdateColumn()
        {
            var code = "";
            code += $"DROP PROCEDURE   [dbo].[Sp_Get{TableName}_UpdateColumn];" + NewLine;
            code += GenGoSplitBatch();
            code += "  " + NewLine;
            code += "       " + NewLine;
            code += $"      create PROCEDURE [dbo].[Sp_Get{TableName}_UpdateColumn] " + NewLine;
            code +=
                $"        @{Ds.Tables[0].PrimaryKey[0]} {DbTypeConversion.CTypeNetToTypeDb(Ds.Tables[0].PrimaryKey[0].DataType.ToString())},@Column  nvarchar(max),@Data nvarchar(max)   " +
                NewLine;

            code += "   AS     " + NewLine;
            code += "       BEGIN     " + NewLine;
            code += "       --SET NOCOUNT ON;     " + NewLine;
            code += "         " + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                if (dataColumn.ColumnName == Ds.Tables[0].PrimaryKey[0].ColumnName)
                {
                    continue;
                }
                //code += "@" + dataColumn.ColumnName + " " + DbTypeConversion.CTypeNetToTypeDb(dataColumn.DataType.ToString()) + "=null," + _NewLine;

                code += $"         if  @Column = '{dataColumn.ColumnName}'" + NewLine;
                code += "           BEGIN " + NewLine;
                code +=
                    $"           UPDATE   {TableName} SET {dataColumn.ColumnName}=@Data where {Ds.Tables[0].PrimaryKey[0]} = @{Ds.Tables[0].PrimaryKey[0]};  " +
                    NewLine;
                code += "         END " + NewLine;
            }

            //code += "         if  @Column = 'Colin'" + _NewLine;
            //code += "           BEGIN " + _NewLine;
            //code += "           UPDATE   TradeFromCategory SET CategoryName=@keyword where CategoryID = @CategoryID;  " + _NewLine;
            //code += "         END " + _NewLine;

            code += "       END     " + NewLine;
        
       
            code += GenGoSplitBatch();
            return code;
        }

        private string GenGoSplitBatch()
        {
            var code = "";
            code += "" + NewLine;
            code += "go" + NewLine;
            code += "" + NewLine;
            return code;
        }

        public override void Gen()
        {
            var code = "";
            code += GenSp_GetPageWise();
            code += GenGoSplitBatch();
            code += GenSp_GetAutocomplete();
            code += GenSp_GetAutocompleteByColumn();
            code += GenSp_GetTradeFromCategory_UpdateColumn();

            InnitProperties();
            FileCode.WriteFile(FileName.StoreProCodeName(), code);
            //_FileCode.writeFile("Sp_" + _TableName + "Store", _code, _fileType);
        }
    }
}