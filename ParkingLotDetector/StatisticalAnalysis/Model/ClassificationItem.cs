namespace StatisticalAnalysis.Model
{
    public class ClassificationItem
    {
        public string Path;
        public bool IsOccupied;
        public bool AfterClassification;
        public bool IsClassifiedAsOccupied;

        public ClassificationItem()
        {}

        public ClassificationItem(ClassificationItem classificationItem)
        {
            Path = classificationItem.Path;
            IsOccupied = classificationItem.IsOccupied;
            AfterClassification = classificationItem.AfterClassification;
            IsClassifiedAsOccupied = classificationItem.IsClassifiedAsOccupied;
        }

        public override string ToString()
        {
            string line = "";
            if (AfterClassification)
            {
                if (IsOccupied && IsClassifiedAsOccupied)
                {
                    line = "TP - ";
                }
                else if (IsOccupied && !IsClassifiedAsOccupied)
                {
                    line = "FN - ";
                }
                else if (!IsOccupied && IsClassifiedAsOccupied)
                {
                    line = "FP - ";
                }
                else if (!IsOccupied && !IsClassifiedAsOccupied)
                {
                    line = "TN - ";
                }
            }
            else
            {
                line = "NC - ";
            }
            line += Path;
            return line;
        }
    }
}