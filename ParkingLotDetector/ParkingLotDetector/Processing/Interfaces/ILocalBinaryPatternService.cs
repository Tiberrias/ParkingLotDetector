using System.Collections.Generic;
using System.Drawing;

namespace ParkingLotDetector.Processing.Interfaces
{
    public interface ILocalBinaryPatternService
    {
        List<double[]> Process(Bitmap bitmap);
    }
}