using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ParkingLotDetector.Classification;
using ParkingLotDetector.Model;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetectorTests.Classification
{
    [TestFixture]
    public class SvmClassificationServiceTests
    {
        private SvmClassificationService _svmClassificationService;
        private Mock<ILoggingService> _loggingService;

        [SetUp]
        public void SetUp()
        {
            _loggingService = new Mock<ILoggingService>();
            _svmClassificationService = new SvmClassificationService(_loggingService.Object);
        }

        [Test]
        public void LearnClassify_Works()
        {
            SvmLearningSet learningSet = new SvmLearningSet()
            {
                Inputs = new List<double[]>()
                {
                    new double[] {1, 0, 1, 0, 0},
                    new double[] {1, 1, 1, 0, 0},
                    new double[] {0, 1, 0, 0, 0},
                    new double[] {1, 1, 0, 0, 0},
                    new double[] {0, 0, 1, 0, 1},
                    new double[] {0, 0, 1, 1, 0},
                    new double[] {0, 0, 0, 1, 1},
                    new double[] {0, 0, 0, 0, 1}
                },
                Outputs = new List<int>() { 1, 1, 1, 1, 0, 0, 0, 0 },
                Size = 8
            };
            ProcessedImage processedImage = new ProcessedImage() {Data = new double[] {0, 1, 1, 0, 0}};

            _svmClassificationService.Learn(learningSet);

            var result = _svmClassificationService.Classify(processedImage);

            Assert.AreEqual(1,result);

        }



    }
}