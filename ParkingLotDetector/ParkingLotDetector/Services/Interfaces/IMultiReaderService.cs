using System.Collections.Generic;
using System.Drawing;

namespace ParkingLotDetector.Services.Interfaces
{
    public interface IMultiReaderService
    {
        List<Bitmap> ReadAllImagesInFolder(string folderPath, string extension);
    }
}