using System.Drawing;
using Accord;
using Accord.Imaging;
using Accord.Math;
using ParkingLotDetector.Model;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    public class CornerDetectionService : IImageProcessingService
    {
        private readonly HarrisCornersDetector _harrisCornersDetector;
        private readonly FastCornersDetector _fastCornersDetector;

        public CornerDetectionService(HarrisCornersDetector harrisCornersDetector, FastCornersDetector fastCornersDetector)
        {
            _harrisCornersDetector = harrisCornersDetector;
            _fastCornersDetector = fastCornersDetector;
        }
        
        public ProcessedImage Process(Bitmap bitmap)
        {
            var firstResult = _harrisCornersDetector.ProcessImage(bitmap);

            var secondResult = _fastCornersDetector.ProcessImage(bitmap);
            
            return new ProcessedImage()
            {
                Data = new[] {firstResult.Count, secondResult.Count}.ToDouble()
            };

        }
    }
}