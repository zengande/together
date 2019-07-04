﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.SeedWork;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Repositories
{
    public class ActivityRepository
        : IActivityRepository
    {
        private ActivityDbContext _context;
        public ActivityRepository(ActivityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.AggregatesModel.ActivityAggregate.Activity> AddAsync(Domain.AggregatesModel.ActivityAggregate.Activity activity)
        {
            return (await _context.Activities.AddAsync(activity)).Entity;
        }

        public async Task<IQueryable<Domain.AggregatesModel.ActivityAggregate.Activity>> FindListAsync(List<int> activityIds)
        {
            return await Task.FromResult(_context.Activities.Where(a => activityIds.Any(id => id == a.Id))
                .Include(a => a.ActivityStatus));
        }

        public async Task<Domain.AggregatesModel.ActivityAggregate.Activity> FindOneAsync(Expression<Func<Domain.AggregatesModel.ActivityAggregate.Activity, bool>> where)
        {
            return await _context.Activities.FirstOrDefaultAsync(where);
        }

        public async Task<Domain.AggregatesModel.ActivityAggregate.Activity> GetAsync(int activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            // TODO: 延迟加载
            if (activity != null)
            {
                await _context.Entry(activity)
                    .Collection(a => a.Participants)
                    .LoadAsync();
                await _context.Entry(activity)
                    .Reference(a => a.ActivityStatus)
                    .LoadAsync();
            }
            return activity;
        }

        public async Task UpdateAsync(Domain.AggregatesModel.ActivityAggregate.Activity activity)
        {
            await Task.Run(() =>
            {
                _context.Entry(activity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            });
        }
    }
}
