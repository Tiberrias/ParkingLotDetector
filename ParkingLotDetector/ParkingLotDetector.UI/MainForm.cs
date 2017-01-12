using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ParkingLotDetector.Classification;
using ParkingLotDetector.Model;
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
        private IMultiReaderService _multiReaderService;
        private ILoggingService _loggingService;
        private SvmLearningSet _svmLearningSet;
        private SvmClassificationService _svmClassificationService;

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _loggingService = new LoggingService();
            _loggingService.MessageLogged += OnMessageLogged;

            _imageReaderService = new ImageReaderService();
            _localBinaryPatternService = new LocalBinaryPatternService();
            _multiReaderService = new MultiReaderService(_imageReaderService);

            _svmClassificationService = new SvmClassificationService(_loggingService);
            _svmLearningSet = new SvmLearningSet();
        }

        private void OnMessageLogged(string log)
        {
            logBox.Text += log + Environment.NewLine;
        }

        private void OnLearnClick(object sender, System.EventArgs e)
        {
            if(_svmLearningSet == null)
                return;

            _svmClassificationService.Learn(_svmLearningSet);
        }

        private void OnLoadEmptyClick(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var images = _multiReaderService.ReadAllImagesInFolder(folderBrowserDialog.SelectedPath, ".jpg");
                _loggingService.Log($"Loaded {images.Count} images");
                var processedImages =
                    images.Select(x => _localBinaryPatternService.Process(x))
                        .Select(y => new ClassifiedImage() {Classification = 0, ProcessedImage = y}).ToList();
                _loggingService.Log($"Processed {processedImages.Count} images" );
                processedImages.ForEach(_svmLearningSet.Add);
            }
        }

        private void OnLoadOccupiedClick(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var images = _multiReaderService.ReadAllImagesInFolder(folderBrowserDialog.SelectedPath, ".jpg");
                _loggingService.Log($"Loaded {images.Count} images");
                var processedImages =
                    images.Select(x => _localBinaryPatternService.Process(x))
                        .Select(y => new ClassifiedImage() { Classification = 1, ProcessedImage = y }).ToList();
                _loggingService.Log($"Processed {processedImages.Count} images");
                processedImages.ForEach(_svmLearningSet.Add);
            }
        }

        private void OnClassifyClick(object sender, System.EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openImageDialog.FileName;
                Bitmap bitmap = _imageReaderService.GetBitmap(filename);
                var processedImage = _localBinaryPatternService.Process(bitmap);
                _loggingService.Log($"Processed image for classification");

                var classificationresult = _svmClassificationService.Classify(processedImage);
                _loggingService.Log($"Classified image as {classificationresult}");

                if (classificationresult == 0)
                {
                    labelResult.Text = "Unoccupied";
                }
                else
                {
                    labelResult.Text = "Occupied";
                }

            }
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            _svmLearningSet.Clear();
            _svmClassificationService = new SvmClassificationService(_loggingService);
            _loggingService.Log("Learning set cleared. Classification service reset.");
        }

        private void OnClassifySetClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var images = _multiReaderService.ReadAllImagesInFolder(folderBrowserDialog.SelectedPath, ".jpg");
                _loggingService.Log($"Loaded {images.Count} images");
                var processedImages =
                    images.Select(x => _localBinaryPatternService.Process(x)).ToList();
                _loggingService.Log($"Processed {processedImages.Count} images");

                Stopwatch stopwatch = Stopwatch.StartNew();
                var results = processedImages.Select(x => _svmClassificationService.Classify(x)).ToList();
                _loggingService.Log($"Performed classification on {results.Count} images in {stopwatch.ElapsedMilliseconds} ms: Classified {results.Count(x => x==0)} unoccupied lots and {results.Count(x => x == 1)} occupied lots.");
                stopwatch.Stop();

            }
        }
    }
}
