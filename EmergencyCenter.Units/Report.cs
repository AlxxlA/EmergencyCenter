using System.Text;
using EmergencyCenter.Units.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Units
{
    public class Report : IReport
    {
        public Report(ReportType reportType, string author, string content)
        {
            Validator.ValidateStringNullOrEmpty(author, "Report author cannot be null or empty.");
            Validator.ValidateStringNullOrEmpty(content, "Report content cannot be null or empty.");

            this.ReportType = reportType;
            this.Author = author;
            this.Content = content;
        }

        public ReportType ReportType { get; }

        public string Author { get; }

        public string Content { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("---Report---");
            sb.AppendLine(this.ReportType.ToString());
            sb.AppendLine($"Author : {this.Author}");
            sb.AppendLine(this.Content);
            sb.Append(new string('-', 12));

            return sb.ToString();
        }
    }
}