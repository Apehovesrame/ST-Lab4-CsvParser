using System;
using System.Collections.Generic;
using System.Text;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode1
{
    public class CsvRowParser : ICsvRowParser //DeepSeek
    {
        public string[] ParseRow(string row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row), "Входная строка не может быть null.");

            var fields = new List<string>();
            var currentField = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < row.Length; i++)
            {
                char currentChar = row[i];

                if (currentChar == '"')
                {
                    if (insideQuotes)
                    {
                        if (i + 1 < row.Length && row[i + 1] == '"')
                        {
                            currentField.Append('"');
                            i++;
                        }
                        else
                        {
                            insideQuotes = false;
                        }
                    }
                    else
                    {
                        insideQuotes = true;
                    }
                }
                else if (currentChar == ',' && !insideQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(currentChar);
                }
            }

            if (insideQuotes)
                throw new FormatException("Строка содержит незакрытую кавычку.");

            fields.Add(currentField.ToString());
            return fields.ToArray();
        }
    }
}