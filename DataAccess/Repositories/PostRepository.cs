using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;
        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.DateCreated = DateTime.Now;
            post.LastModified = DateTime.Now;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return new Post();
            return post;
        }

        public async Task<Post> UpdatePost(string updatedContent, int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return new Post();

            post.LastModified = DateTime.Now;
            post.Content = updatedContent;
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
