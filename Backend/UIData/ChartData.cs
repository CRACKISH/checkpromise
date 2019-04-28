using Checkpromise.Models;

namespace Checkpromise.UIData
{
    public enum Measure
    {
        UAH = '₴',
        USD = '$',
        Percent = '%'
    }

    public class ChartDataValue
    {
        public string Date;

        public float Value;

        public float Value2;

        public string Quantity;
    }

   public class ChartData
    {
        public string Label;

        public bool InvertArrow;

        public Measure Measure;

        public Measure Measure2;

        public ChartDataValue InitialData;

        public ChartDataValue CurrentData;

        public string Source;

        public ChartData()
        {
            Measure = Measure.UAH;
        }

        public ChartData(Chart chart)
        {
            Source = chart.Source;
        }
    }
}
