using System.Diagnostics;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using ParkingLotDetector.Classification.Interfaces;
using ParkingLotDetector.Model;
using ParkingLotDetector.Services.Interfaces;

namespace ParkingLotDetector.Classification
{
    public class SvmClassificationService : ISvmClassificationService
    {
        private readonly ILoggingService _loggingService;
        private readonly MulticlassSupportVectorLearning<Linear> _multiclassSupportVectorLearning;
        private MulticlassSupportVectorMachine<Linear> _multiclassSupportVectorMachine;

        public SvmClassificationService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _multiclassSupportVectorLearning = new MulticlassSupportVectorLearning<Linear>();
        }

        public void Learn(SvmLearningSet svmLearningSet)
        {
            if (svmLearningSet.Size == 0)
            {
                _loggingService.Log($"Empty SVM Learning set, unable to learn...");
                return;
            }

            _loggingService.Log($"Started Learning SVM...");
            Stopwatch stopwatch = Stopwatch.StartNew();
            _multiclassSupportVectorLearning.Learner  = (p) => new SequentialMinimalOptimization<Linear>()
            {
                
            };

            _multiclassSupportVectorMachine = _multiclassSupportVectorLearning.Learn(svmLearningSet.GetInputs(),
                svmLearningSet.GetOutputs());
            _loggingService.Log($"Learning SVM completed in {stopwatch.ElapsedMilliseconds} ms. Learning set size: {svmLearningSet.Inputs.Count}, Feature Vector Lenght: {svmLearningSet.Inputs[0].Length}, Achieved {_multiclassSupportVectorMachine.SupportVectorCount} support vectors");
            stopwatch.Stop();
        }

        public void Forget()
        {
           
        }

        public int Classify(ProcessedImage processedImage)
        {
            return _multiclassSupportVectorMachine.Decide(processedImage.Data);
        }
    }
}