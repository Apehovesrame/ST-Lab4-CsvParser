namespace Lab.Interfaces
{    public interface ICsvRowParser
    {
        /// <summary>
        /// Разбор строки CSV на массив значений.
        /// </summary>
        /// <param name="row">Входная строка в формате CSV.</param>
        /// <returns>Массив строковых значений полей.</returns>
        string[] ParseRow(string row);
    }
}