using System.Drawing;
using Accord.Imaging;
using Accord.Math;
using ParkingLotDetector.Model;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    public class LocalBinaryPatternService : IImageProcessingService
    {
        public ProcessedImage Process(Bitmap bitmap)
        {
            LocalBinaryPattern localBinaryPattern = new LocalBinaryPattern(cellSize: 0);

            localBinaryPattern.ProcessImage(bitmap);

            double[] result = localBinaryPattern.Histograms[0, 0].ToDouble();

            ProcessedImage processedImage = new ProcessedImage() {Data = result};
            return processedImage;
        }
    }
}