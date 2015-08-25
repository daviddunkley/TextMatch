namespace TextMatch.Services
{
    public interface ITextMatchService
    {
        string FindOccurences(string inputText, string subText);
    }
}
