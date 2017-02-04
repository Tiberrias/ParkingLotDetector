using System.Drawing;
using Accord.Imaging;
using Accord.Math;
using ParkingLotDetector.Model;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    public class LocalBinaryPatternOptimizationService : IImageProcessingService
    {
        public ProcessedImage Process(Bitmap bitmap)
        {
            LocalBinaryPattern localBinaryPattern = new LocalBinaryPattern(cellSize: 0);

            localBinaryPattern.ProcessImage(bitmap);

            int reductionScale = 1;
            var initialHistogram = localBinaryPattern.Histograms[0, 0].ToDouble();
            var resultLength = initialHistogram.Length / reductionScale;
            
            double[] result = new double[resultLength];
            int resultIterator = 0;
            for (int i = 0; i < initialHistogram.Length; i++)
            {
                result[resultIterator] += initialHistogram[i];
                if (i % reductionScale == reductionScale - 1)
                    resultIterator++;
            }


            ProcessedImage processedImage = new ProcessedImage() {Data = result};
            return processedImage;
        }
    }
}