using System.Collections.Generic;

namespace StkGenCode.Code.Column
{
    public class MappingColumn
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        /// <summary>
        ///     Columnname : TableName;Columnname : TableName;Columnname : TableName;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<MappingColumn> ExtractMappingColumn(string str)
        {
            var mappingColumnCollection = new List<MappingColumn>();
            var columnTables = str.Split(';'); // Columnname : TableName;
            foreach (var columnTable in columnTables)
            {
                var split = columnTable.Split(':');
                var columnName = split[0];
                var tableName = split[1];
                var mappingColumn = new MappingColumn
                {
                    ColumnName = columnName,
                    TableName = tableName
                };
                mappingColumnCollection.Add(mappingColumn);
            }

            return mappingColumnCollection;
        }
    }
}