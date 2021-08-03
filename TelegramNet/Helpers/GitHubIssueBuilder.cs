namespace TelegramNet.Helpers
{
    internal static class GitHubIssueBuilder
    {
        public static string Message(string message, string head, string body)
        {
            return
                $"{message}\nReport it on GitHub: https://github.com/DenVot/TelegramNet/issues/new?title={head.Replace("\n", "%0D%0A")}&body={body.Replace("\n", "%0D%0A")}";
        }
    }
}