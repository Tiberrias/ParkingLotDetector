using System;
using System.Collections.Generic;
using System.Linq;
using StatisticalAnalysis.Model;
using StatisticalAnalysis.Services.Interfaces;

namespace StatisticalAnalysis.Services
{
    public class AnalysisService : IAnalysisService
    {
        private AnalysisResults _currentResults;

        public AnalysisService()
        {
            _currentResults = new AnalysisResults();
        }

        public AnalysisResults GetCurrentResults()
        {
            return _currentResults;
        }

        public void ClearCurrentResults()
        {
            _currentResults = new AnalysisResults();
        }

        public void SetLearningTime(long learningTimeInMilliseconds)
        {
            _currentResults.LearningTimeInMilliseconds = learningTimeInMilliseconds;
        }

        public void SetClassificationTime(long classificationTimeInMilliseconds)
        {
            _currentResults.ClassificationTimeInMilliseconds = classificationTimeInMilliseconds;
        }

        public void AnalyzeLearningSet(List<ClassificationItem> learningSetItems)
        {
            _currentResults.LearningSetSize = learningSetItems.Count;
            _currentResults.OccupiedInLearningSet = learningSetItems.Count(x => x.IsOccupied);
            _currentResults.EmptyInLearningSet = _currentResults.LearningSetSize - _currentResults.OccupiedInLearningSet;
        }

        public void AnalyzeBenchmarkSet(List<ClassificationItem> benchmarkSetItems)
        {
            _currentResults.BenchmarkSetSize = benchmarkSetItems.Count;
            _currentResults.OccupiedInBenchmarkSet = benchmarkSetItems.Count(x => x.IsOccupied);
            _currentResults.EmptyInBenchmarkSet = _currentResults.BenchmarkSetSize - _currentResults.OccupiedInBenchmarkSet;

            _currentResults.TrueOccupied =
                benchmarkSetItems.Count(x => x.AfterClassification && x.IsClassifiedAsOccupied && x.IsOccupied);
            _currentResults.FalseOccupied =
                benchmarkSetItems.Count(x => x.AfterClassification && x.IsClassifiedAsOccupied && !x.IsOccupied);
            _currentResults.TrueEmpty =
                benchmarkSetItems.Count(x => x.AfterClassification && !x.IsClassifiedAsOccupied && !x.IsOccupied);
            _currentResults.FalseEmpty =
                benchmarkSetItems.Count(x => x.AfterClassification && !x.IsClassifiedAsOccupied && x.IsOccupied);

            _currentResults.Accuracy = (double)(_currentResults.TrueEmpty + _currentResults.TrueOccupied) /
                                       _currentResults.BenchmarkSetSize;

            _currentResults.F1Score = (double)(2 * _currentResults.TrueOccupied) /
                                      (2 * _currentResults.TrueOccupied + _currentResults.FalseOccupied +
                                       _currentResults.FalseEmpty);

            double x1 = (_currentResults.TrueOccupied * _currentResults.TrueEmpty -
                         _currentResults.FalseOccupied * _currentResults.FalseEmpty);
            double u1 = (_currentResults.TrueOccupied + _currentResults.FalseOccupied);
            double u2 = (_currentResults.TrueOccupied + _currentResults.FalseEmpty);
            double u3 = (_currentResults.TrueEmpty + _currentResults.FalseOccupied);
            double u4 = (_currentResults.TrueEmpty + _currentResults.FalseEmpty);
            double x2 = Math.Sqrt(u1 * u2 * u3 * u4);
            _currentResults.MatthewsCoefficient = x1 / x2;


        }
    }
}