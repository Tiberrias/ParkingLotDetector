using ParkingLotDetector.Model;

namespace ParkingLotDetector.Classification.Interfaces
{
    public interface ISvmClassificationService
    {
        void Learn(SvmLearningSet svmLearningSet);

        void Forget();

        int Classify(ProcessedImage processedImage);
    }
}