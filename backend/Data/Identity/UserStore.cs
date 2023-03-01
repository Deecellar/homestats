using System;
using backend.Models.Identity;
using LiteDB;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Identity
{
    public class UserStore : IUserStore<User>
    {
        private readonly LiteDatabase _db;
        private ILiteCollection<User> _users;

        public UserStore(LiteDatabase db)
        {
            _db = db;
            _users = _db.GetCollection<User>("users");
        }
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try {
                _users.Insert(user);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "CreateUserError",
                    Description = e.Message
                }));
            }
            
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            try {
                _users.Delete(user.Id);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "DeleteUserError",
                    Description = e.Message
                }));
            }
        }

        public void Dispose()
        {
            _db.Dispose();

        }

        public Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_users.FindOne(userId) ?? null);
            } catch  {
                return Task.FromResult<User?>(null);
            }
        }

        public Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_users.FindOne(x => x.NormalizedUserName == normalizedUserName) ?? null);
            } catch  {
                return Task.FromResult<User?>(null);
            }
        }

        public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_users.FindOne(x => x.Id == user.Id)?.NormalizedUserName ?? null);
            } catch  {
                return Task.FromResult<string?>(null);
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_users.FindOne(x => x.Id == user.Id)?.Id ?? string.Empty);
            } catch  {
                return Task.FromResult<string>(string.Empty);
            }
        }

        public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_users.FindOne(x => x.Id == user.Id)?.UserName ?? null);
            } catch  {
                return Task.FromResult<string?>(null);
            }
        }

        public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
        {
            try {
                var u = _users.FindOne(x => x.Id == user.Id);
                if (u != null) {
                    u.NormalizedUserName = normalizedName;
                    _users.Update(u);
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
        {
            try {
                var u = _users.FindOne(x => x.Id == user.Id);
                if (u != null) {
                    u.UserName = userName;
                    _users.Update(u);
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try {
                _users.Update(user);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "UpdateUserError",
                    Description = e.Message
                }));
            }
        }
    }

        
}