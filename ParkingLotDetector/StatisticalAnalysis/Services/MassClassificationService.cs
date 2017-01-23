using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ParkingLotDetector.Classification.Interfaces;
using ParkingLotDetector.Model;
using ParkingLotDetector.Processing.Interfaces;
using ParkingLotDetector.Services.Interfaces;
using StatisticalAnalysis.Model;
using StatisticalAnalysis.Services.Interfaces;

namespace StatisticalAnalysis.Services
{
    public class MassClassificationService : IMassClassificationService
    {
        private readonly IImageProcessingService _imageProcessingService;
        private readonly ISvmClassificationService _svmClassificationService;
        private readonly ILoggingService _loggingService;
        private readonly IImageReaderService _imageReaderService;
        private readonly IAnalysisService _analysisService;

        public MassClassificationService(IImageProcessingService imageProcessingService, ISvmClassificationService svmClassificationService, ILoggingService loggingService, IImageReaderService imageReaderService, IAnalysisService analysisService)
        {
            _imageProcessingService = imageProcessingService;
            _svmClassificationService = svmClassificationService;
            _loggingService = loggingService;
            _imageReaderService = imageReaderService;
            _analysisService = analysisService;
        }

        public List<ClassificationItem> ClassificationBenchmark(List<ClassificationItem> inputSet, int learningSetSize, int benchmarkSetSize)
        {
            if (inputSet.Count == 0 || inputSet.Count < learningSetSize + benchmarkSetSize)
            {
                _loggingService.Log("Classification Benchmark - Unable to perform: input set too small.");
                return null;
            }
            if (learningSetSize <= 0 || benchmarkSetSize <= 0)
            {
                _loggingService.Log("Classification Benchmark - Unable to perform: invalid learning/benchmark set sizes.");
                return null;
            }

            _loggingService.Log($"Classification Benchmark - Beginning benchmark for {_imageProcessingService.GetType()}");

            HashSet<string> usedPaths = new HashSet<string>();
            Random random = new Random();
            List<ClassificationItem> learningSet = new List<ClassificationItem>();
            List<ClassificationItem> benchmarkSet = new List<ClassificationItem>();

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (learningSet.Count < learningSetSize)
            {
                var newItem = inputSet[random.Next(inputSet.Count)];
                if (!usedPaths.Contains(newItem.Path))
                {
                    learningSet.Add(newItem);
                    usedPaths.Add(newItem.Path);
                }
            }
            var timeElapsed = stopwatch.ElapsedMilliseconds;
            _analysisService.AnalyzeLearningSet(learningSet);
            _loggingService.Log($"Classification Benchmark - Found learning set of size {learningSet.Count} in: {timeElapsed} ms.");
            stopwatch.Restart();

            while (benchmarkSet.Count < benchmarkSetSize)
            {
                var newItem = new ClassificationItem(inputSet[random.Next(inputSet.Count)]);
                if (!usedPaths.Contains(newItem.Path))
                {
                    benchmarkSet.Add(newItem);
                    usedPaths.Add(newItem.Path);
                }
            }
            timeElapsed = stopwatch.ElapsedMilliseconds;
            _loggingService.Log($"Classification Benchmark - Found benchmark set of size {benchmarkSet.Count} in: {timeElapsed} ms.");
            stopwatch.Restart();

            _loggingService.Log("Classification Benchmark - Started processing learning set...");
            SvmLearningSet svmLearningSet = new SvmLearningSet()
            {
                Inputs = new List<double[]>(learningSetSize),
                Outputs = new List<int>(learningSetSize)
            };
            foreach (var learningSetItem in learningSet)
            {
                svmLearningSet.Add(new ClassifiedImage()
                {
                   ProcessedImage = _imageProcessingService.Process(_imageReaderService.GetBitmap(learningSetItem.Path)),
                   Classification = learningSetItem.IsOccupied ? 1 : 0
                });

            }
            timeElapsed = stopwatch.ElapsedMilliseconds;
            _loggingService.Log($"Classification Benchmark - Processed learning set of size {learningSet.Count} in: {timeElapsed} ms.");
            stopwatch.Restart();
            
            _svmClassificationService.Learn(svmLearningSet);

            timeElapsed = stopwatch.ElapsedMilliseconds;
            _analysisService.SetLearningTime(timeElapsed);
            _loggingService.Log($"Classification Benchmark - Learned set of size {learningSet.Count} in: {timeElapsed} ms.");
            stopwatch.Restart();

            _loggingService.Log("Classification Benchmark - Started classifying benchmark set...");
            var benchmarkSetProcessingCount = 0;
            foreach (var benchmarkSetItem in benchmarkSet)
            {
                var processedImage =
                    _imageProcessingService.Process(_imageReaderService.GetBitmap(benchmarkSetItem.Path));

                benchmarkSetItem.IsClassifiedAsOccupied = _svmClassificationService.Classify(processedImage) == 1;
                benchmarkSetItem.AfterClassification = true;

                benchmarkSetProcessingCount++;
                if (benchmarkSetProcessingCount % 1000 == 0)
                {
                    _loggingService.Log($"Performed classification on {(benchmarkSetProcessingCount*100)/benchmarkSetSize}% of benchmark set.");
                }
            }

            timeElapsed = stopwatch.ElapsedMilliseconds;
            _analysisService.SetClassificationTime(timeElapsed);
            _loggingService.Log($"Classification Benchmark - Classified set of size {benchmarkSet.Count} in: {timeElapsed} ms.");
            stopwatch.Stop();
            
            _analysisService.AnalyzeBenchmarkSet(benchmarkSet);
            _loggingService.Log("Classification Benchmark finished.");
            return benchmarkSet;
        }
    }
}