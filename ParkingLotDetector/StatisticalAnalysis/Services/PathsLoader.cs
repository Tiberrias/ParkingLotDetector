using System.Collections.Generic;
using System.IO;
using StatisticalAnalysis.Model;
using StatisticalAnalysis.Services.Interfaces;

namespace StatisticalAnalysis.Services
{
    public class PathsLoader : IPathsLoader
    {
        public List<ClassificationItem> FindClassificationItems(string parentFolderPath, string imageExtension)
        {
            var filePaths = Directory.GetFiles(parentFolderPath, "*." + imageExtension, SearchOption.AllDirectories);

            var resultList = new List<ClassificationItem>();

            foreach (var filePath in filePaths)
            {
                if (filePath.Contains("Occupied"))
                {
                    resultList.Add(new ClassificationItem()
                    {
                        Path = filePath,
                        AfterClassification = false,
                        IsOccupied = true
                    });
                }
                else if (filePath.Contains("Empty"))
                {
                    resultList.Add(new ClassificationItem()
                    {
                        Path = filePath,
                        AfterClassification = false,
                        IsOccupied = false
                    });
                }
            }

            return  resultList;
        }
    }
}