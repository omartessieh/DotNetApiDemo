using DotNetApiDemo.Data;
using DotNetApiDemo.Entities;
using DotNetApiDemo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace DotNetApiDemo.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetById(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            return post;
        }

        public async Task<Post> GetByTitle(string title)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower());
            return post;
        }

        public async Task<IEnumerable<Post>> PagePost(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }
            int skipAmount = (page - 1) * pageSize;
            if (skipAmount < 0)
            {
                skipAmount = 0;
            }
            var posts = await _context.Posts.Skip(skipAmount).Take(pageSize).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> SearchPost(string text)
        {
            text = text.ToLower();
            var posts = await _context.Posts.Where(x => x.Title.ToLower().Contains(text) || x.Description.ToLower().Contains(text)).ToListAsync();
            return posts;
        }

        public async Task Add(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}