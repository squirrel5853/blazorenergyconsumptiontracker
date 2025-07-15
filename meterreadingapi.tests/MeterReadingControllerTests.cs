using energyconsumptiontracker.Application.DataImport;
using energyconsumptiontracker.Application.Models;
using energyconsumptiontracker.Domain;
using meterreadingapi.Controllers;
using Moq;

namespace meterreadingapi.tests
{
    public class MeterReadingControllerTests
    {
        private Mock<ICsvFileProcessor> _csvFileProcessorMock;
        private Mock<IMeterReadingPersistence> _persistenceMock;
        private MeterReadingController _controller;

        [SetUp]
        public void SetUp()
        {
            _csvFileProcessorMock = new Mock<ICsvFileProcessor>();
            _persistenceMock = new Mock<IMeterReadingPersistence>();
            _controller = new MeterReadingController(_csvFileProcessorMock.Object, _persistenceMock.Object);
        }

        [Test]
        public async Task PostMeterReadings_CallsPersistenceWithData()
        {
            // Arrange
            var meterReadingDTOs = new[]
            {
                new MeterReadingDto { AccountId = 123, ReadingDate = DateTime.UtcNow, ReadingValue = 100 }
            };

            // Act
            await _controller.PostMeterReadings(meterReadingDTOs);

            // Assert
            _persistenceMock.Verify(p => p.StoreMeterReadings(It.IsAny<MeterReading[]>()), Times.Once);
        }
    }
}
