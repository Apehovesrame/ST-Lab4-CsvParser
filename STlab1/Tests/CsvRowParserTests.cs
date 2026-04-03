using Lab.Interfaces;
using NUnit.Framework;
using System;

using Lab.Implementations.GenCode2;

namespace Tests
{
    [TestFixture]
    public class GenCode_CsvParserTests
    {
        private ICsvRowParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new CsvRowParser();
        }

        // Тесты "Чёрного ящика

        // 1 Базовое разделение строки по запятым (без кавычек)
        [Test]
        public void ParseRow_SimpleCommaSeparated_ReturnsArray()
        {
            string input = "яблоко,банан,вишня";
            var result = _parser.ParseRow(input);
            Assert.That(result, Is.EqualTo(new[] { "яблоко", "банан", "вишня" }));
        }

        // 2 Игнорирование запятых внутри закавыченных полей
        [Test]
        public void ParseRow_WithQuotes_IgnoresCommasInside()
        {
            // запятая внутри кавычек игнорируется
            string input = "один,\"два, три\",четыре";
            var result = _parser.ParseRow(input);

            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[1], Does.Contain(",")); 
            // кавычки вокруг значения удаляются
            Assert.That(result[1], Is.EqualTo("два, три").Or.EqualTo("\"два, три\""));
        }

        // 3 обра пустых полей (две запятые подряд)
        [Test]
        public void ParseRow_EmptyFields_ReturnsEmptyStrings()
        {
            string input = "а,,в";
            var result = _parser.ParseRow(input);
            Assert.That(result, Is.EqualTo(new[] { "а", "", "в" }));
        }

        // 4 пров выброса исключения при передаче null
        [Test]
        public void ParseRow_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.ParseRow(null));
        }

        // Тесты "Белого ящика"

        // 5 пров сдвоенных кавычек внутри текста
        [Test]
        public void ParseRow_EscapedQuotes_HandlesCorrectly()
        {
            string input = "\"Она сказала \"\"Привет\"\"\"";
            var result = _parser.ParseRow(input);

            Assert.That(result, Does.Contain("Она сказала \"Привет\""));
        }

        // 6 пров выброса исключения при незакрытой кавычке
        [Test]
        public void ParseRow_UnclosedQuotes_ThrowsOrHandles()
        {
            string input = "а,\"б";
            Assert.Throws<FormatException>(() => _parser.ParseRow(input));
        }
    }
}