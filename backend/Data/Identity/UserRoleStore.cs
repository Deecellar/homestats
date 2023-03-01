using backend.Models.Identity;
using LiteDB;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Identity
{
    public class UserRoleStore : IUserRoleStore<UserRole> {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<UserRole> _userRoles;
        private readonly ILiteCollection<User> _users;
        private readonly ILiteCollection<Role> _roles;

        public UserRoleStore(LiteDatabase db) {
            _db = db;
            _userRoles = _db.GetCollection<UserRole>("UserRoles");
            _users = _db.GetCollection<User>("Users");
            _roles = _db.GetCollection<Role>("Roles");
        }

        public Task AddToRoleAsync(UserRole user, string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                _userRoles.Insert(new UserRole() {
                    RoleId = _roles.FindOne(x => x.NormalizedName == normalizedRoleName).Id,
                    UserId = user.UserId
                });
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<IdentityResult> CreateAsync(UserRole user, CancellationToken cancellationToken)
        {
            try {
                _userRoles.Insert(user);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "CreateUserRoleError",
                    Description = e.Message
                }));
            }
        }

        public Task<IdentityResult> DeleteAsync(UserRole user, CancellationToken cancellationToken)
        {
            try {
                var userRoleToDelete = _db.GetCollection("UserRoles").FindOne(Query.EQ("UserId", user.UserId));
                _userRoles.Delete(userRoleToDelete.AsDocument["_id"].AsObjectId);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "DeleteUserRoleError",
                    Description = e.Message
                }));
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Task<UserRole?> FindByIdAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_userRoles.FindOne(x => x.UserId == userId && x.RoleId == roleId) ?? null);
            } catch  {
                return Task.FromResult<UserRole?>(null);
            }
        }

        public Task<UserRole?> FindByNameAsync(string normalizedUserName, string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_userRoles.FindOne(x => _users.FindOne(y => y.NormalizedUserName == normalizedUserName).Id == x.UserId && _roles.FindOne(y => y.NormalizedName == normalizedRoleName).Id == x.RoleId) ?? null);
            } catch  {
                return Task.FromResult<UserRole?>(null);
            }
        }

        public Task<IList<string>> GetRolesAsync(UserRole user, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult<IList<string>>(_userRoles.Find(x => x.UserId == user.UserId).Select(x => _roles.FindOne(y => y.Id == x.RoleId).Name).Select(x => x ?? string.Empty).ToList());
            } catch  {
                return Task.FromResult<IList<string>>(new List<string>());
            }
        }

        public Task<IList<UserRole>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult<IList<UserRole>>(_userRoles.Find(x => _roles.FindOne(y => y.NormalizedName == normalizedRoleName).Id == x.RoleId).ToList());
            } catch  {
                return Task.FromResult<IList<UserRole>>(new List<UserRole>());
            }
        }

        public Task<IList<UserRole>> GetUsersInRoleAsync(string normalizedRoleName, string userId, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult<IList<UserRole>>(_userRoles.Find(x => _roles.FindOne(y => y.NormalizedName == normalizedRoleName).Id == x.RoleId && x.UserId == userId).ToList());
            } catch  {
                return Task.FromResult<IList<UserRole>>(new List<UserRole>());
            }
        }

        public Task<bool> IsInRoleAsync(UserRole user, string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_userRoles.Exists(x => x.UserId == user.UserId && _roles.FindOne(y => y.NormalizedName == normalizedRoleName).Id == x.RoleId));
            } catch  {
                return Task.FromResult(false);
            }
        }

        public Task RemoveFromRoleAsync(UserRole user, string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                _userRoles.DeleteMany(x => x.UserId == user.UserId && _roles.FindOne(y => y.NormalizedName == normalizedRoleName).Id == x.RoleId);
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task SetRoleNameAsync(UserRole user, string roleName, CancellationToken cancellationToken)
        {
            try {
                var r = _roles.FindOne(x => x.Name == roleName);
                if (r != null) {
                    var ur = _userRoles.FindOne(x => x.UserId == user.UserId && x.RoleId == user.RoleId);
                    if (ur != null) {
                        ur.RoleId = r.Id;
                        _userRoles.Update(ur);
                    }
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<IdentityResult> UpdateAsync(UserRole user, CancellationToken cancellationToken)
        {
            try {
                _userRoles.Update(user);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "UpdateUserRoleError",
                    Description = e.Message
                }));
            }
        }

        public Task<string> GetUserIdAsync(UserRole user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId);
        }

        public Task<string> GetUserNameAsync(UserRole user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_users.FindOne(x => x.Id == user.UserId).UserName ?? string.Empty);
        }

        public Task SetUserNameAsync(UserRole user, string userName, CancellationToken cancellationToken)
        {
            try {
                var u = _users.FindOne(x => x.UserName == userName);
                if (u != null) {
                    var ur = _userRoles.FindOne(x => x.UserId == user.UserId && x.RoleId == user.RoleId);
                    if (ur != null) {
                        ur.UserId = u.Id;
                        _userRoles.Update(ur);
                    }
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<string> GetNormalizedUserNameAsync(UserRole user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_users.FindOne(x => x.Id == user.UserId).NormalizedUserName ?? string.Empty);
        }

        public Task SetNormalizedUserNameAsync(UserRole user, string? normalizedName, CancellationToken cancellationToken)
        {
            try {
                var u = _users.FindOne(x => x.NormalizedUserName == normalizedName);
                if (u != null) {
                    var ur = _userRoles.FindOne(x => x.UserId == user.UserId && x.RoleId == user.RoleId);
                    if (ur != null) {
                        ur.UserId = u.Id;
                        _userRoles.Update(ur);
                    }
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<UserRole?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_userRoles.FindOne(x => x.UserId == userId) ?? null);
            } catch  {
                return Task.FromResult<UserRole?>(null);
            }
        }

        public Task<UserRole?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_userRoles.FindOne(x => _users.FindOne(y => y.NormalizedUserName == normalizedUserName).Id == x.UserId) ?? null);
            } catch  {
                return Task.FromResult<UserRole?>(null);
            }
        }
    }

        
}