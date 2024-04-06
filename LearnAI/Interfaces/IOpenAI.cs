namespace LearnAI.Interfaces
{
    public interface IOpenAI
    {
        public Task<IOpenAI> initialize();
        public Task<string> AskAsync(string input);
    }
}