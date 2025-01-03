﻿using Microsoft.AspNetCore.Identity;

namespace Messenger.Data;

static public class Seed
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        var roles = new List<string>
        {
            "Admin",
            "User",
            "Moderator"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}