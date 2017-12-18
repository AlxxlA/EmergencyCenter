using System.Text;

namespace EmergencyCenter.Units
{
    public class Report
    {
        public Report(ReportType reportType, string author, string content)
        {
            this.ReportType = reportType;
            this.Author = author;
            this.Content = content;
        }

        public ReportType ReportType { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("---Report---");
            sb.AppendLine(this.ReportType.ToString());
            sb.AppendLine($"Author : {this.Author}");
            sb.AppendLine(this.Content);
            sb.AppendLine(new string('-', 12));

            return sb.ToString();
        }
    }
}