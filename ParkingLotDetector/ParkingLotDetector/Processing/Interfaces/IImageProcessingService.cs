using System.Drawing;
using ParkingLotDetector.Model;

namespace ParkingLotDetector.Processing.Interfaces
{
    public interface IImageProcessingService
    {
        ProcessedImage Process(Bitmap bitmap);
    }
}