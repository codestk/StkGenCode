﻿using System.Collections.Generic;

namespace StkGenCode.Code.Label
{
    public class TextTemplate
    {
        // TextID:บ้านเลขที่;Number:ตัวเลข;
        //string MappingLabel = "Col1:Text1;Col2:Text1;Col3:Text1;";
        public string ColumnAndLabelSet;

        public string GetLabel(string columnName)
        {
            if (ColumnAndLabelSet == "")
            {
                return columnName;
            }

            var columnAndTextSet = ColumnAndLabelSet.Trim(';').Split(';');

            Dictionary<string, string> dictionary =
                new Dictionary<string, string>();

            foreach (var columnAndText in columnAndTextSet)
            {
                var split = columnAndText.Split(':');
                var extractcolumnName = split[0];
                var text = split[1];
                dictionary.Add(extractcolumnName, text);
                
            }

            string textResult = "";

            if (dictionary.TryGetValue(columnName, out textResult))
            {
                return textResult;
            }

            //หาไม่เจอ return ชื่อ Column
            return columnName;
        }
    }
}