using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoopShopInfos.Models
{
    public class CoopShopInfosContext : DbContext
    {
        public CoopShopInfosContext (DbContextOptions<CoopShopInfosContext> options)
            : base(options)
        {
        }

        public DbSet<CoopShopInfos.Models.Product> Product { get; set; }
    }
}
