using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Column
{
   public  class MappingColumn
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        /// <summary>
        /// Columnname : TableName;Columnname : TableName;Columnname : TableName;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<MappingColumn> ExtractMappingColumn(string str)
        {
            List<MappingColumn> _MappingColumnCollection = new List<Column.MappingColumn>();
            string[] ColumnTables = str.Split(';');   // Columnname : TableName;
            foreach (string ColumnTable in ColumnTables)
            { string[] split = ColumnTable.Split(':');
                string columnName = split[0];
                string tableName = split[1];
                MappingColumn _MappingColumn = new MappingColumn();
                _MappingColumn.ColumnName = columnName;
                _MappingColumn.TableName = tableName;
                _MappingColumnCollection.Add(_MappingColumn);
            }

            return _MappingColumnCollection;
        }
    }
}
