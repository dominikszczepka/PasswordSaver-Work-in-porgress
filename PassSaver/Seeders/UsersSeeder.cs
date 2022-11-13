using PassSaver.Entities;

namespace PassSaver.Seeders
{
    public class UsersSeeder
    {
        private readonly PassSaverDbContext _dbContext;
        public UsersSeeder(PassSaverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IEnumerable<User> GetUsers()
        {
            var result = new List<User>()
            {

                new User
                {
                    Username = "Admin",
                    UserEmail = "Admin@admin.com",
                    Passwords = new List<Password>()
                },

                new User
                {
                    Username = "Moderator",
                    UserEmail = "Moderator@mod.com",
                    Passwords = new List<Password>()
                }
            };
            return result;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Users.Any())
                {
                    var Users = GetUsers();
                    _dbContext.Users.AddRange(Users);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
