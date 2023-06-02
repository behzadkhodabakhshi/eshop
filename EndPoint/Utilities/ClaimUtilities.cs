using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Utilities
{
    public static class ClaimUtilities
    {
        public static long? GetUserId(ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                long userId = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                return userId;

            }
            else
            {
                return null;
            }


        }
        public static List<string> GetRolse(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<string> rolse = new List<string>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    rolse.Add(item.Value);
                }
                return rolse;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
