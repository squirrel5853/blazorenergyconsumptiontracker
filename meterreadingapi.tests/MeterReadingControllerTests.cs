using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using meterreadingapi.Services;
using Moq;

namespace meterreadingapi.tests
{
    public class MeterReadingControllerTests
    {
        private Mock<ICsvFileProcessor> _csvFileProcessorMock;
        private Mock<IMeterReadingPersistence> _persistenceMock;
        private MeterReadingService _controller;

        [SetUp]
        public void SetUp()
        {
            _csvFileProcessorMock = new Mock<ICsvFileProcessor>();
            _persistenceMock = new Mock<IMeterReadingPersistence>();
            _controller = new MeterReadingService(_csvFileProcessorMock.Object, _persistenceMock.Object);
        }

        [Test]
        public async Task PostMeterReadings_CallsPersistenceWithData()
        {
            // Arrange
            var meterReadings = new[]
            {
                new MeterReading { AccountId = 123, ReadingDate = DateTime.UtcNow, ReadingValue = 100 }
            };

            // Act
            await _controller.PostMeterReadings(meterReadings);

            // Assert
            _persistenceMock.Verify(p => p.StoreMeterReadings(meterReadings), Times.Once);
        }
    }
}
