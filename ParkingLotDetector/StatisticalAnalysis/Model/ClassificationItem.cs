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
    }
}