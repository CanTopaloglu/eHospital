using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.DataAccess.Extensions;
public static class UserManagerExtentions
{
    public static async Task<User?> FindByIdentityNumber(this UserManager<User> userManager, string 
        identityNumber, CancellationToken cancellationToken = default)
    {
        return await userManager.Users.FirstOrDefaultAsync(p => p.IdentityNumber == identityNumber, cancellationToken); // Extensions method identity üzerinden.
    }
}
