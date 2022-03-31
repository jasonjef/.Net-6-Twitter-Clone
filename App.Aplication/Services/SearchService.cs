using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Services
{
    public class SearchService
    {
        private readonly AppDbContext _db;

        public SearchService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Post>> Search(string query)
        {
            return await _db.Posts
                .Include(e => e.User)
                .Where(e => e.Content.Contains(query) || e.User.UserName.Contains(query)).ToListAsync();
        }

    }
}