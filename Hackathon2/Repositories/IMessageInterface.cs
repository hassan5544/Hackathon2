using Hackathon2.Models;

namespace Hackathon2.Repositories
{
    public interface IMessageInterface
    {
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message> GetMessageById(int id);
        Task AddMessage(Message message);
        Task UpdateMessage(Message message);
        Task DeleteMessage(int id);
    }
}
