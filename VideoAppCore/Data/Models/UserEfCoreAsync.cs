using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    public class UserEfCoreAsync : IUserAsync
    {
        private readonly ApplicationDbContext _context;

        public UserEfCoreAsync(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<User> AddUserAsync(User model)
        {
            model.API_NAME = this.ToString() + "AddUserAsync";
            model.CREATION_DATE = DateTime.Now;


            _context.Add(model);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUserListAsync()
        {
            //return await _context.Users.FromSqlRaw<User>("select * from Users").ToListAsync(); // .Users.FromSql<User>("select * from Users").ToListAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task RemoveUserAsync(int id)
        {
            var video = await _context.Users.FindAsync(id);
            if (video != null)
            {
                _context.Users.Remove(video);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> UpdateUserAsync(User model)
        {
            model.API_NAME = this.ToString() + "UpdateUserAsync";

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return model;
        }
        public async Task<User> GetUserByEMail(string email)
        {
            string query = "SELECT top(1) * FROM USERS WHERE EMAIL = {0}";

            //SqlParameter param = new SqlParameter("@WORK_DATE", workDate);


            return await _context.Users.FromSqlRaw<User>(query, email).FirstOrDefaultAsync();
        }

        public async Task<User> LogIn(string email, string password)
        {
            string query = "SELECT top(1) * FROM USERS WHERE EMAIL = {0} AND PASSWORD = {1} ";

            return await _context.Users.FromSqlRaw<User>(query, email, password).FirstOrDefaultAsync();
        }

    }
}
