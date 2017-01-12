using System.Drawing;
using ParkingLotDetector.Model;

namespace ParkingLotDetector.Processing.Interfaces
{
    public interface ILocalBinaryPatternService
    {
        ProcessedImage Process(Bitmap bitmap);
    }
}