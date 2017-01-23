using System;
using Newtonsoft.Json;

namespace StatisticalAnalysis.Model
{
    public class AnalysisResults
    {
        public int TotalSetSize => LearningSetSize + BenchmarkSetSize;

        public int LearningSetSize;
        public int OccupiedInLearningSet;
        public int EmptyInLearningSet;

        public int BenchmarkSetSize;
        public int OccupiedInBenchmarkSet;
        public int EmptyInBenchmarkSet;

        public long LearningTimeInMilliseconds;
        public long ClassificationTimeInMilliseconds;

        public int TrueOccupied;
        public int FalseOccupied;
        public int TrueEmpty;
        public int FalseEmpty;

        public double Accuracy;
        public double F1Score;
        public double MatthewsCoefficient;

        public override string ToString()
        {
           return JsonConvert.SerializeObject(this).Replace(",",","+Environment.NewLine);   
        }
    }
}