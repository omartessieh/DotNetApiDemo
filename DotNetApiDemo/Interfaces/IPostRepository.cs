using DotNetApiDemo.Entities;

namespace DotNetApiDemo.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(int id);
        Task<Post> GetByTitle(string title);
        Task<IEnumerable<Post>> SearchPost(string text);
        Task<IEnumerable<Post>> PagePost(int page, int pageSize);
        Task Add(Post post);
        Task Update(Post post);
        Task Delete(int id);
    }
}