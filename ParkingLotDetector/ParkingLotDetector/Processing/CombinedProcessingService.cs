using System.Collections.Generic;
using System.Drawing;
using ParkingLotDetector.Model;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    public class CombinedProcessingService : IImageProcessingService
    {
        private readonly CornerDetectionService _cornerDetectionService;
        private readonly LocalBinaryPatternService _localBinaryPatternService;

        public ProcessedImage Process(Bitmap bitmap)
        {
            List<double> features = new List<double>();
            features.AddRange(_cornerDetectionService.Process(bitmap).Data);
            features.AddRange(_localBinaryPatternService.Process(bitmap).Data);

            return new ProcessedImage() {Data = features.ToArray()};
        }

        public CombinedProcessingService()
        {
            _cornerDetectionService = new CornerDetectionService();
            _localBinaryPatternService = new LocalBinaryPatternService();
        }
    }
}