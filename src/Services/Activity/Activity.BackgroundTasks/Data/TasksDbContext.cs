using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.BackgroundTasks.Data
{
    public class TasksDbContext
        : DbContext
    {
        public TasksDbContext(DbContextOptions options) 
            :base(options)
        {
        }

    }
}
