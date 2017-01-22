using System.Collections.Generic;
using StatisticalAnalysis.Model;

namespace StatisticalAnalysis.Services.Interfaces
{
    public interface IMassClassificationService
    {
        List<ClassificationItem> ClassificationBenchmark(
            List<ClassificationItem> inputSet,
            int learningSetSize,
            int benchmarkSetSize);
    }
}