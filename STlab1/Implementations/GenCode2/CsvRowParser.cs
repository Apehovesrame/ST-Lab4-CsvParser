using System;
using System.Collections.Generic;
using System.Text; 
using Lab.Interfaces;

namespace Lab.Implementations.GenCode2 
{
    public class CsvRowParser : ICsvRowParser //YandexGPT
    {
        public string[] ParseRow(string row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row), "Входная строка не может быть null.");
            }

            var result = new List<string>();
            var currentField = new StringBuilder();
            bool inQuotes = false;
            int length = row.Length;

            for (int i = 0; i < length; i++)
            {
                char currentChar = row[i];

                if (currentChar == '"')
                {
                    if (i + 1 < length && row[i + 1] == '"')
                    {
                        currentField.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (currentChar == ',' && !inQuotes)
                {
                    result.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(currentChar);
                }
            }

            result.Add(currentField.ToString());

            for (int j = 0; j < result.Count; j++)
            {
                string field = result[j];
                if (field.Length >= 2 && field.StartsWith("\"") && field.EndsWith("\""))
                {
                    result[j] = field.Substring(1, field.Length - 2);
                }
            }

            return result.ToArray();
        }
    }
}