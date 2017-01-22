using System.Collections.Generic;
using StatisticalAnalysis.Model;

namespace StatisticalAnalysis.Services.Interfaces
{
    public interface IPathsLoader
    {
        List<ClassificationItem> FindClassificationItems(string parentFolderPath, string imageExtension);
    }
}