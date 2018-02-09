using System;
using System.Text;
using EmergencyCenter.Units.Contracts;

namespace EmergencyCenter.Units
{
    public class Report : IReport
    {
        private const string AuthorNullOrWhiteSpaceMessage = "Report author cannot be null or whitespace..";
        private const string ContentNullOrWhiteSpaceMessage = "Report content cannot be null or whitespace.";

        public Report(ReportType reportType, string author, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException(ContentNullOrWhiteSpaceMessage);
            }
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException(AuthorNullOrWhiteSpaceMessage);
            }

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
