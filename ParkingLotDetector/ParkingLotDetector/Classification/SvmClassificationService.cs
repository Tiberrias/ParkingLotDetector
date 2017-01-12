using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using ParkingLotDetector.Classification.Interfaces;
using ParkingLotDetector.Model;

namespace ParkingLotDetector.Classification
{
    public class SvmClassificationService : ISvmClassificationService
    {
        private MulticlassSupportVectorLearning<Linear> _multiclassSupportVectorLearning;
        private MulticlassSupportVectorMachine<Linear> _multiclassSupportVectorMachine;

        public SvmClassificationService()
        {
           _multiclassSupportVectorLearning = new MulticlassSupportVectorLearning<Linear>();
        }

        public void Learn(SvmLearningSet svmLearningSet)
        {
            _multiclassSupportVectorLearning.Learner  = (p) => new SequentialMinimalOptimization<Linear>()
            {
                
            };

            _multiclassSupportVectorMachine = _multiclassSupportVectorLearning.Learn(svmLearningSet.Inputs,
                svmLearningSet.Outputs);
            
        }

        public void Forget()
        {
            throw new System.NotImplementedException();
        }

        public int Classify(ProcessedImage processedImage)
        {
            return _multiclassSupportVectorMachine.Decide(processedImage.Data);
        }
    }
}