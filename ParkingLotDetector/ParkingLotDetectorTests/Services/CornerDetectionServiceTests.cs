using System.Configuration;
using Accord.Imaging;
using Accord.Imaging.Filters;
using NUnit.Framework;
using ParkingLotDetector.Processing;
using ParkingLotDetector.Services;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetectorTests.Services
{
    [TestFixture]
    public class CornerDetectionServiceTests
    {
        private CornerDetectionService _cornerDetectionService;
        private IImageReaderService _imageReaderService;

        [SetUp]
        public void SetUp()
        {
            HarrisCornersDetector harrisCornersDetector = new HarrisCornersDetector();
            FastCornersDetector fastCornersDetector = new FastCornersDetector();
            _imageReaderService = new ImageReaderService();

            _cornerDetectionService = new CornerDetectionService();
        }

        [Test]
        public void Test()
        {
            var image = _imageReaderService.GetBitmap(@"K:\TestSets\UFPR04\Sunny\2012-12-07\Occupied\2012-12-07_17_12_25#001.jpg");
            var processedImage =  _cornerDetectionService.Process(image);

            CannyEdgeDetector x = new CannyEdgeDetector();

            x.Apply(image);
            
            Assert.Fail();
        }

        
    }
}