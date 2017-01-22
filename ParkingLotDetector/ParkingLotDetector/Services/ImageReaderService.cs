using System.Drawing;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.Services
{
    public class ImageReaderService : IImageReaderService
    {
        public Bitmap GetBitmap(string filename)
        {
            Image loaded = Image.FromFile(filename);
            Bitmap bitmap = new Bitmap(loaded);
            loaded.Dispose();
            return bitmap;
        }
    }
}