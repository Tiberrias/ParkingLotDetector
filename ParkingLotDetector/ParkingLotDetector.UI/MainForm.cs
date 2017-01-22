using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using StatisticalAnalysis.Model;
using StatisticalAnalysis.Services;
using StatisticalAnalysis.Services.Interfaces;

namespace ParkingLotDetector.UI
{
    public partial class MainForm : Form
    {
        private IImageReaderService _imageReaderService;
        private IImageProcessingService _imageProcessingService;
        private IMultiReaderService _multiReaderService;
        private ILoggingService _loggingService;
        private SvmLearningSet _svmLearningSet;
        private SvmClassificationService _svmClassificationService;

        private IMassClassificationService _massClassificationService;
        private IAnalysisService _analysisService;
        private IPathsLoader _pathsLoader;

        delegate void SetTextCallback(string text);

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
            _imageProcessingService = new LocalBinaryPatternService();
            _multiReaderService = new MultiReaderService(_imageReaderService);

            _svmClassificationService = new SvmClassificationService(_loggingService);
            _svmLearningSet = new SvmLearningSet();

            _pathsLoader = new PathsLoader();
            _analysisService = new AnalysisService();
            _massClassificationService = new MassClassificationService(_imageProcessingService, _svmClassificationService, _loggingService, _imageReaderService, _analysisService);
        }

        private void OnMessageLogged(string log)
        {
            AddTextToLogWindow(log + Environment.NewLine);
        }

        private void AddTextToLogWindow(string text)
        {
            if (logBox.InvokeRequired)
            {
                SetTextCallback d = AddTextToLogWindow;
                Invoke(d, text);
            }
            else
            {
                logBox.Text += text;
                logBox.SelectionStart = logBox.Text.Length;
                logBox.ScrollToCaret();
            }
        }

        private void OnLearnClick(object sender, System.EventArgs e)
        {
            if (_svmLearningSet == null)
                return;

            backgroundLearner.RunWorkerAsync();
        }

        private void OnLoadEmptyClick(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var images = _multiReaderService.ReadAllImagesInFolder(folderBrowserDialog.SelectedPath, ".jpg");
                _loggingService.Log($"Loaded {images.Count} images");
                var processedImages =
                    images.Select(x => _imageProcessingService.Process(x))
                        .Select(y => new ClassifiedImage() { Classification = 0, ProcessedImage = y }).ToList();
                _loggingService.Log($"Processed {processedImages.Count} images");
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
                    images.Select(x => _imageProcessingService.Process(x))
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
                var processedImage = _imageProcessingService.Process(bitmap);
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
            _svmClassificationService.Forget();
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
                    images.Select(x => _imageProcessingService.Process(x)).ToList();
                _loggingService.Log($"Processed {processedImages.Count} images");

                Stopwatch stopwatch = Stopwatch.StartNew();

                var results = processedImages.Select(x => _svmClassificationService.Classify(x)).ToList();
                _loggingService.Log($"Performed classification on {results.Count} images in {stopwatch.ElapsedMilliseconds} ms: Classified {results.Count(x => x == 0)} unoccupied lots and {results.Count(x => x == 1)} occupied lots.");
                stopwatch.Stop();

            }
        }

        private void OnBackgroundLearnerDoWork(object sender, DoWorkEventArgs e)
        {
            _svmClassificationService.Learn(_svmLearningSet);
        }

        private void OnButtonStatisticalAnalysisClick(object sender, EventArgs e)
        {
            _svmLearningSet.Clear();
            _svmClassificationService.Forget();
            _svmClassificationService = new SvmClassificationService(_loggingService);
            _analysisService.ClearCurrentResults();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var classificationItems = _pathsLoader.FindClassificationItems(folderBrowserDialog.SelectedPath, "jpg");
                if (classificationItems == null || classificationItems.Count == 0)
                {
                    return;
                }
                var learningSetSize = (int)numericUpDownLearningSetSize.Value;
                var benchmarkSetSize = (int)numericUpDownBenchmarkSetSize.Value;

                Tuple<List<ClassificationItem>, int, int> benchmarkDefinition =
                    new Tuple<List<ClassificationItem>, int, int>(classificationItems, learningSetSize, benchmarkSetSize);
                backgroundBenchmarkPerformer.RunWorkerAsync(benchmarkDefinition);
            }
        }

        private void OnBackgroundBenchmarkPerformerDoWork(object sender, DoWorkEventArgs e)
        {
            var parameters = e.Argument as Tuple<List<ClassificationItem>, int, int>;

            _massClassificationService.ClassificationBenchmark(parameters.Item1, parameters.Item2, parameters.Item3);
        }

        private void OnBackgroundBenchmarkPerformerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _loggingService.Log(_analysisService.GetCurrentResults().ToString());
        }
    }
}
