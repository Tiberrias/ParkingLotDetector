using NUnit.Framework;
using ParkingLotDetector.Classification;
using ParkingLotDetector.Model;

namespace ParkingLotDetectorTests.Classification
{
    [TestFixture]
    public class SvmClassificationServiceTests
    {
        private SvmClassificationService _svmClassificationService;

        [SetUp]
        public void SetUp()
        {
            _svmClassificationService = new SvmClassificationService();
        }

        [Test]
        public void LearnClassify_Works()
        {
            SvmLearningSet learningSet = new SvmLearningSet()
            {
                Inputs = new[]
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
                Outputs = new[] { 1, 1, 1, 1, 0, 0, 0, 0 },
                Size = 8
            };
            ProcessedImage processedImage = new ProcessedImage() {Data = new double[] {0, 1, 1, 0, 0}};

            _svmClassificationService.Learn(learningSet);

            var result = _svmClassificationService.Classify(processedImage);

            Assert.AreEqual(1,result);

        }



    }
}