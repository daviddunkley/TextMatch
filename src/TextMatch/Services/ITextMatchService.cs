namespace TextMatch.Services
{
    public interface ITextMatchService
    {
        string FindMatches(string inputText, string subText);
    }
}
