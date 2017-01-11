using System.Collections.Generic;
using System.Drawing;
using Accord.Imaging;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    class LocalBinaryPatternService : ILocalBinaryPatternService
    {
        public List<double[]> Process(Bitmap bitmap)
        {
            LocalBinaryPattern localBinaryPattern = new LocalBinaryPattern();
            return localBinaryPattern.ProcessImage(bitmap);
        }
    }
}