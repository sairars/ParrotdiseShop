using Microsoft.EntityFrameworkCore.Migrations;
using ParrotdiseShop.Core.Utilities;

#nullable disable

namespace ParrotdiseShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedFirstAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, 
                                                                EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, 
                                                                PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, 
                                                                LockoutEnd, LockoutEnabled, AccessFailedCount, 
                                                                City, Discriminator, Name, PostalCode, Province, StreetAddress) 
                                    VALUES ('ef8a1c19-4fa4-46a4-80ad-e0217e9ee949', 'admin@parrotdise.com', 'ADMIN@PARROTDISE.COM', 
                                            'admin@parrotdise.com', 'ADMIN@PARROTDISE.COM', 0, 
                                            'AQAAAAEAACcQAAAAECIWOkMGMAM1ZdIefE/5fjtNrfxMxZR5Ab72Us+TfPrOtjAnlsrVh7xIoU6lDa7tsA==', 
                                            'RA5XRAAK4SGOP2ZOFVNLEW25BXVA74N7', 'f47034ab-4202-44e7-8177-1163f3a85171', 
                                            '617-877-9334', 0, 0, NULL, 1, 0, 'Mississauga', 'ApplicationUser', 
                                            'Parrotdise Admin', '456789', 'ON', '411 Main Street')");


            migrationBuilder.Sql(@$"INSERT INTO AspNetUserRoles (UserId, RoleId) 
                                    SELECT 'ef8a1c19-4fa4-46a4-80ad-e0217e9ee949', Id
                                    FROM AspNetRoles
                                    WHERE Name = '{RoleName.Admin}'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"DELETE FROM AspNetUserRoles 
                                    WHERE UserId = 'ef8a1c19-4fa4-46a4-80ad-e0217e9ee949' 
                                        AND RoleId IN ( SELECT Id 
                                                        FROM dbo.AspNetRoles 
                                                        WHERE Name = '{RoleName.Admin}')");

            migrationBuilder.Sql(@$"DELETE FROM AspNetUsers 
                                    WHERE Id = 'ef8a1c19-4fa4-46a4-80ad-e0217e9ee949'");
        }
    }
}
