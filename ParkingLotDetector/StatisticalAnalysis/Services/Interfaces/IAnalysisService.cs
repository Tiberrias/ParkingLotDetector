using System.Collections.Generic;
using StatisticalAnalysis.Model;

namespace StatisticalAnalysis.Services.Interfaces
{
    public interface IAnalysisService
    {
        AnalysisResults GetCurrentResults();

        void ClearCurrentResults();

        void SetLearningTime(long learningTimeInMilliseconds);
        
        void SetClassificationTime(long classificationTimeInMilliseconds);

        void AnalyzeLearningSet(List<ClassificationItem> learningSetItems);

        void AnalyzeBenchmarkSet(List<ClassificationItem> benchmarkSetItems);
    }
}