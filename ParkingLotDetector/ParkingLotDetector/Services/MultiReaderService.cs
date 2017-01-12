using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.Services
{
    public class MultiReaderService : IMultiReaderService
    {
        private readonly IImageReaderService _imageReaderService;

        public MultiReaderService(IImageReaderService imageReaderService)
        {
            _imageReaderService = imageReaderService;
        }

        public List<Bitmap> ReadAllImagesInFolder(string folderPath, string extension)
        {
            List<Bitmap> images = new List<Bitmap>();
            
            foreach (string file in Directory.EnumerateFiles(folderPath, $"*{extension}"))
            {
                images.Add(_imageReaderService.GetBitmap(file));
            }
            return images;
        }
    }
}