using System.Collections.Generic;

namespace ParkingLotDetector.Model
{
    public class SvmLearningSet
    {
        public int Size { get; set; }

        public List<double[]> Inputs;

        public List<int> Outputs;

        public SvmLearningSet()
        {
            Inputs = new List<double[]>();
            Outputs = new List<int>();
        }

        public void Add(ClassifiedImage classifiedImage)
        {
            Inputs.Add(classifiedImage.ProcessedImage.Data);
            Outputs.Add(classifiedImage.Classification);
            Size++;
        }

        public double[][] GetInputs()
        {
            return Inputs.ToArray();
        }

        public int[] GetOutputs()
        {
            return Outputs.ToArray();
        }

        public void Clear()
        {
            Inputs.Clear();
            Outputs.Clear();
            Size = 0;
        }

    }
}