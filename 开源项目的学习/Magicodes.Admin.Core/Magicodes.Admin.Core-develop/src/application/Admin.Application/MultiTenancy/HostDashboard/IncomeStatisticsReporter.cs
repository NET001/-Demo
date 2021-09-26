// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IncomeStatisticsReporter.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Magicodes.Admin.Application.MultiTenancy.HostDashboard.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.MultiTenancy.Payments;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.MultiTenancy.HostDashboard
{
    public class IncomeStatisticsService : AdminDomainServiceBase, IIncomeStatisticsService
    {
        private readonly IRepository<SubscriptionPayment, long> _subscriptionPaymentRepository;

        public IncomeStatisticsService(IRepository<SubscriptionPayment, long> subscriptionPaymentRepository)
        {
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
        }

        public async Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval)
        {
            List<IncomeStastistic> incomeStastistics;

            switch (dateInterval)
            {
                case ChartDateInterval.Daily:
                    incomeStastistics = await GetDailyIncomeStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Weekly:
                    incomeStastistics = await GetWeeklyIncomeStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Monthly:
                    incomeStastistics = await GetMonthlyIncomeStatisticsData(startDate, endDate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dateInterval), dateInterval, null);
            }

            incomeStastistics.ForEach(i => { i.Label = i.Date.ToString(L("DateFormatShort")); });

            return incomeStastistics.OrderBy(i => i.Date).ToList();
        }

        private async Task<List<IncomeStastistic>> GetDailyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await _subscriptionPaymentRepository.GetAll()
                .Where(s => s.CreationTime >= startDate &&
                            s.CreationTime <= endDate &&
                            s.Status == SubscriptionPaymentStatus.Completed)
                .GroupBy(s => new DateTime(s.CreationTime.Year, s.CreationTime.Month, s.CreationTime.Day))
                .Select(s => new IncomeStastistic
                {
                    Date = s.First().CreationTime.Date,
                    Amount = s.Sum(c => c.Amount)
                })
                .ToListAsync();

            FillGapsInDailyIncomeStatistics(dailyRecords, startDate, endDate);
            return dailyRecords.OrderBy(s => s.Date).ToList();
        }

        private static void FillGapsInDailyIncomeStatistics(ICollection<IncomeStastistic> dailyRecords,
            DateTime startDate, DateTime endDate)
        {
            var currentDay = startDate;
            while (currentDay <= endDate)
            {
                if (dailyRecords.All(d => d.Date != currentDay.Date))
                    dailyRecords.Add(new IncomeStastistic(currentDay));

                currentDay = currentDay.AddDays(1);
            }
        }

        private async Task<List<IncomeStastistic>> GetWeeklyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await GetDailyIncomeStatisticsData(startDate, endDate);
            var firstDayOfWeek = DateTimeFormatInfo.CurrentInfo == null
                ? DayOfWeek.Sunday
                : DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;

            var incomeStastistics = new List<IncomeStastistic>();
            decimal weeklyAmount = 0;
            var weekStart = dailyRecords.First().Date;
            var isFirstWeek = weekStart.DayOfWeek == firstDayOfWeek;

            dailyRecords.ForEach(dailyRecord =>
            {
                if (dailyRecord.Date.DayOfWeek == firstDayOfWeek)
                {
                    if (!isFirstWeek) incomeStastistics.Add(new IncomeStastistic(weekStart, weeklyAmount));

                    isFirstWeek = false;
                    weekStart = dailyRecord.Date;
                    weeklyAmount = 0;
                }

                weeklyAmount += dailyRecord.Amount;
            });

            incomeStastistics.Add(new IncomeStastistic(weekStart, weeklyAmount));
            return incomeStastistics;
        }

        private async Task<List<IncomeStastistic>> GetMonthlyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await GetDailyIncomeStatisticsData(startDate, endDate);
            var query = dailyRecords.GroupBy(d => new
                {
                    d.Date.Year,
                    d.Date.Month
                })
                .Select(grouping => new IncomeStastistic
                {
                    Date = FindMonthlyDate(startDate, grouping.Key.Year, grouping.Key.Month),
                    Amount = grouping.DefaultIfEmpty().Sum(x => x.Amount)
                });

            return query.ToList();
        }

        private static DateTime FindMonthlyDate(DateTime startDate, int groupYear, int groupMonth)
        {
            if (groupYear == startDate.Year && groupMonth == startDate.Month)
                return new DateTime(groupYear, groupMonth, startDate.Day);

            return new DateTime(groupYear, groupMonth, 1);
        }
    }
}