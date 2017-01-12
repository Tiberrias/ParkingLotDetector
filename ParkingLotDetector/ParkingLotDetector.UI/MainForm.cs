using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using ParkingLotDetector.Processing;
using ParkingLotDetector.Processing.Interfaces;
using ParkingLotDetector.Services;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.UI
{
    public partial class MainForm : Form
    {
        private IImageReaderService _imageReaderService;
        private ILocalBinaryPatternService _localBinaryPatternService;

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _imageReaderService = new ImageReaderService();
            _localBinaryPatternService = new LocalBinaryPatternService();
        }

        private void OnLoadMeClick(object sender, System.EventArgs e)
        {
            string filename = @"K:\PKLot.tar\SunnySeg\Sunny\2012-12-07\Occupied\2012-12-07_17_12_25#001.jpg";
            Bitmap bitmap = _imageReaderService.GetBitmap(filename);
            var result = _localBinaryPatternService.Process(bitmap);

        }
    }
}
