﻿using Microsoft.EntityFrameworkCore.Migrations;
using ParrotdiseShop.Core.Models;

#nullable disable

namespace ParrotdiseShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PopulateAspNetRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                                    VALUES('{Guid.NewGuid()}', 
                                            '{RoleName.Admin}', 
                                            '{RoleName.Admin.ToUpperInvariant()}', 
                                            '{Guid.NewGuid()}')");
            migrationBuilder.Sql(@$"INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                                    VALUES('{Guid.NewGuid()}', 
                                            '{RoleName.Employee}', 
                                            '{RoleName.Employee.ToUpperInvariant()}', 
                                            '{Guid.NewGuid()}')");
            migrationBuilder.Sql(@$"INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                                    VALUES('{Guid.NewGuid()}', 
                                            '{RoleName.Customer}', 
                                            '{RoleName.Customer.ToUpperInvariant()}', 
                                            '{Guid.NewGuid()}')");
            migrationBuilder.Sql(@$"INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                                    VALUES('{Guid.NewGuid()}', 
                                            '{RoleName.Company}', 
                                            '{RoleName.Company.ToUpperInvariant()}', 
                                            '{Guid.NewGuid()}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"DELETE FROM AspNetRoles 
                                    WHERE Name IN ('{RoleName.Admin}', 
                                                    '{RoleName.Employee}', 
                                                    '{RoleName.Customer}', 
                                                    '{RoleName.Company}')");
        }
    }
}