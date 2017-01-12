using System.Collections.Generic;
using Accord.Statistics.Visualizations;

namespace ParkingLotDetector.Processing.Interfaces
{
    public interface IHistogramService
    {
        Histogram Calculate(List<double> processedData);
    }
}