using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using System.Text;

namespace energyconsumptiontracker.Application.tests
{
    public class CsvFileProcessorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ProcessCsvFile_ValidCsv_ReturnsMeterReadings()
        {
            // Arrange
            var csvContent = "AccountId,MeterReadingDateTime,MeterReadValue\n123,01/07/2025 10:05,100\n456,28/07/2025 12:34,200";
            var byteArray = Encoding.UTF8.GetBytes(csvContent);
            using var stream = new MemoryStream(byteArray);

            var processor = new CsvFileProcessor();

            // Act
            MeterReading[] result = await processor.ProcessCsvFile(stream);

            // Assert
            Assert.That(result != null);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].AccountId, Is.EqualTo(123));
            Assert.That(result[0].ReadingDate, Is.EqualTo(new DateTime(2025, 7, 1, 10, 05, 0)));
            Assert.That(result[0].ReadingValue, Is.EqualTo(100));
        }
    }
}
