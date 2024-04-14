﻿using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebApp.Identity
{
    public class MyUserStore : IUserStore<MyUser>
    {
        public async Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync("Insert into Users " +
                    "([Id],[UserName],[NormalizedUserName],[PasswordHash]) " +
                    "Values (@Id ,@UserName ,@NormalizedUserName ,@PasswordHash)",
                    new
                    {
                        id = user.Id,
                        username = user.UserName,
                        normalizeduserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync("delete from Users where Id = @id",
                    new
                    {
                        id = user.Id
                    });
                return IdentityResult.Success;
            }
        }

        public void Dispose()
        {   
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Integrated Security=SSPI;" +
                "Persist Security Info=False;" +
                "Initial Catalog=IDENTITY_APP;" +
                @"Data Source=DESKTOP-LUIZ\\SQLEXPRESS");
            connection.Open();
            return connection;
        }

        public async Task<MyUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyUser>(
                    "select * from Users where Id = @id",
                    new {id = userId });
            }
        }

        public async Task<MyUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<MyUser>(
                    "select * from Users where normalizedUserName = @normalizedUserName",
                    new { name = normalizedUserName });
            }
        }

        public Task<string?> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MyUser user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync("Update Users " +
                    "set [Id] = @Id, " +
                    "[UserName] = @UserName, " +
                    "[NormalizedUserName] = @NormalizedUserName, " +
                    "[PasswordHash] = @PasswordHash " +
                    "where [Id] = @id",
                    new
                    {
                        id = user.Id,
                        username = user.UserName,
                        normalizeduserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
                return IdentityResult.Success;
            }
        }
    }
}
