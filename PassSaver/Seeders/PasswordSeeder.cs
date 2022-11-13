using PassSaver.Entities;

namespace PassSaver.Seeders
{
    public class PasswordSeeder
    {
        private readonly PassSaverDbContext _dbContext;
        public PasswordSeeder(PassSaverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IEnumerable<Password> GetPasswords()
        {
            var result = new List<Password>()
            {

                new Password
                {
                    UserId =1,
                    Address = "gmail",
                    S_Password = "admin",

                },

                new Password
                {
                    UserId =2,
                    Address = "gmail",
                    S_Password = "mod",
                }
            };
            return result;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Passwords.Any())
                {
                    var Passwords = GetPasswords();
                    _dbContext.Passwords.AddRange(Passwords);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
