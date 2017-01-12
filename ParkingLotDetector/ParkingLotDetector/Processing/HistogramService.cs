using System.Collections.Generic;
using Accord.Statistics.Visualizations;
using ParkingLotDetector.Processing.Interfaces;

namespace ParkingLotDetector.Processing
{
    public class HistogramService : IHistogramService
    {
        public Histogram Calculate(List<double> processedData)
        {
            Histogram histogram = new Histogram();
            //histogram.FromData(processedData);
            return histogram;
        }
    }
}