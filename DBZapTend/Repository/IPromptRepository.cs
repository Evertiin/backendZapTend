using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IPromptRepository
    {
        Task<IEnumerable<Prompt>> GetPrompts();

        Task<Prompt> GetPrompt(int id);

        Task<Prompt> CreatePrompt(Prompt prompt);

        Task<Prompt> UpdatePrompt(Prompt prompt);

        Task<Prompt> DeletePrompt(int id);
    }
}
