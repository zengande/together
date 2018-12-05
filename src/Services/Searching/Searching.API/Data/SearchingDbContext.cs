using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Searching.API.Data
{
    public class SearchingDbContext
        :DbContext
    {
        public SearchingDbContext(DbContextOptions<SearchingDbContext> options):base(options)
        {

        }
    }
}
