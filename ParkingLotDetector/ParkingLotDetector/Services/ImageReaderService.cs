using System.Drawing;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.Services
{
    class ImageReaderService : IImageReaderService
    {
        public Bitmap GetBitmap(string filename)
        {
            Image loaded = Image.FromFile(filename);
            Bitmap bitmap = new Bitmap(loaded);

            return bitmap;
        }
    }
}