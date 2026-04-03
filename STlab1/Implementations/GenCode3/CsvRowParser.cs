using System;
using System.Collections.Generic;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode3
{
    public class CsvRowParser : ICsvRowParser //GigaChat
    {
        public string[] ParseRow(string row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            var result = new System.Text.StringBuilder();
            bool inQuotes = false;
            int startIndex = 0;
            var fields = new List<string>();

            // Внимание: этот код скорее всего упадет, так как result пустой
            for (int i = 0; i <= row.Length; i++)
            {
                char currentChar = i < row.Length ? row[i] : '\n';

                switch (currentChar)
                {
                    case ',' when !inQuotes:
                        // Здесь будет ошибка ArgumentOutOfRangeException
                        AddField(fields, result.ToString().Substring(startIndex, i - startIndex).Trim('"'));
                        startIndex = i + 1;
                        break;
                    case '"':
                        inQuotes = !inQuotes;
                        break;
                    default:
                        continue;
                }
            }

            return fields.ToArray();
        }

        private void AddField(List<string> fields, string fieldValue)
        {
            fields.Add(fieldValue ?? "");
        }
    }
}