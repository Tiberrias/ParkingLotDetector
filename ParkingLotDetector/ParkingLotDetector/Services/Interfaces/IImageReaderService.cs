using System.Drawing;

namespace ParkingLotDetector.Services.Interfaces
{
    public interface IImageReaderService
    {
        Bitmap GetBitmap(string filename);
    }
}