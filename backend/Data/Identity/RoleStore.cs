using backend.Models.Identity;
using LiteDB;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly LiteDatabase _db;
        private ILiteCollection<Role> _roles;

        public RoleStore(LiteDatabase db)
        {
            _db = db;
            _roles = _db.GetCollection<Role>("roles");
        }
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                _roles.Insert(role);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "CreateRoleError",
                    Description = e.Message
                }));
            }
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                _roles.Delete(role.Id);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "DeleteRoleError",
                    Description = e.Message
                }));
            }
        }

        public void Dispose()
        {
            _db.Dispose();

        }

        public Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_roles.FindOne(roleId) ?? null);
            } catch  {
                return Task.FromResult<Role?>(null);
            }
        }

        public Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_roles.FindOne(x => x.NormalizedName == normalizedRoleName) ?? null);
            } catch  {
                return Task.FromResult<Role?>(null);
            }
        }

        public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_roles.FindOne(x => x.Id == role.Id)?.NormalizedName ?? null);
            } catch  {
                return Task.FromResult<string?>(null);
            }
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_roles.FindOne(x => x.Id == role.Id)?.Id ?? string.Empty);
            } catch  {
                return Task.FromResult<string>(string.Empty);
            }
        }

        public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                return Task.FromResult(_roles.FindOne(x => x.Id == role.Id)?.Name ?? null);
            } catch  {
                return Task.FromResult<string?>(null);
            }
        }

        public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
        {
            try {
                var u = _roles.FindOne(x => x.Id == role.Id);
                if (u != null) {
                    u.NormalizedName = normalizedName;
                    _roles.Update(u);
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
        {
            try {
                var u = _roles.FindOne(x => x.Id == role.Id);
                if (u != null) {
                    u.Name = roleName;
                    _roles.Update(u);
                }
                return Task.FromResult(0);
            } catch  {
                return Task.FromResult(0);
            }
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            try {
                _roles.Update(role);
                return Task.FromResult(IdentityResult.Success);
            } catch (Exception e) {
                return Task.FromResult(IdentityResult.Failed(new IdentityError() {
                    Code = "UpdateRoleError",
                    Description = e.Message
                }));
            }
        }
    }
}

