﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Interfaces.Business;
using ToDo.Interfaces.Data;
using ToDo.Models;

namespace ToDo.Business
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository repo;

        private const int pageSize = 10;

        public ItemService(IItemRepository repo)
        {
            this.repo = repo;
        }

        public PagedList<Item> GetItems(int page = 1)
        {
            var items = repo.GetItems()
                            .OrderBy(i => i.DueDate);
            return new PagedList<Item>(items, page, pageSize);
        }

        public async Task<int> AddItem(Item item)
        {
            repo.InsertItem(item);
            return await repo.Save();
        }

        public async Task<int> UpdateItem(Item item)
        {
            repo.UpdateItem(item);
            return await repo.Save();
        }

        public async Task<int> DeleteItem(int itemId)
        {
            var item = await repo.GetItemByID(itemId);
            repo.DeleteItem(item);
            return await repo.Save();
        }

        public PagedList<Item> SearchItem(string query = "", int page = 1)
        {
            var items = repo.GetItems().ToList()
                            .Where(i => i.Title.ToLower().Contains(query.ToLower()))
                            .OrderBy(i => i.DueDate);

            return new PagedList<Item>(items, page, pageSize);
        }

        public Statistics GetStatistics()
        {
            DataTable mainTable = CreateMainStatisticsTable();

            BuildStatisticsTableStructure(mainTable);

            Statistics returnData = new Statistics();
            List<string> Owners = new List<string>();
            long totalHours = 0;
            returnData.NextMonthStatistic = new System.Collections.Generic.List<DateStatic>();
            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                DateTime rowDate = (DateTime)mainTable.Rows[i][1];

                if (rowDate > DateTime.Now && rowDate < DateTime.Now.AddMonths(1))
                {
                    returnData.NextMonthStatistic.Add(new DateStatic { Date = (DateTime)mainTable.Rows[i][1], Hours = (long)mainTable.Rows[i][2] });
                }

                for (int subI = 0; subI < ((DataTable)mainTable.Rows[i][0]).Rows.Count; subI++)
                {
                    returnData.TotalCount++;

                    if ((long)((DataTable)mainTable.Rows[i][0]).Rows[subI][0] > 100)
                    {
                        returnData.BigTaskCount++;
                    }
                    else if ((long)((DataTable)mainTable.Rows[i][0]).Rows[subI][0] < 25)
                    {
                        returnData.SmallTaskCount++;
                    }

                    totalHours += (long)((DataTable)mainTable.Rows[i][0]).Rows[subI][0];
                    Owners.Add((string)((DataTable)mainTable.Rows[i][0]).Rows[subI][2]);
                }
            }

            returnData.PeopleCount = Owners.Distinct().Count();
            returnData.AvarageHours = totalHours / returnData.TotalCount;
            returnData.AvarageHoursPerPerson = totalHours / returnData.PeopleCount;

            return returnData;
        }

        private void BuildStatisticsTableStructure(DataTable mainTable)
        {
            var AllItems = repo.GetItems();
            DateTime workingDate = AllItems.Min(i => i.DueDate);
            DateTime maxDueDate = AllItems.Max(i => i.DueDate);

            while (workingDate <= maxDueDate)
            {
                DataTable subTable = CreateSubStatisticsTable();
                var startDate = new DateTime(workingDate.Year, workingDate.Month, workingDate.Day, 0, 0, 0);
                var endDate = new DateTime(workingDate.Year, workingDate.Month, workingDate.Day, 23, 59, 59);
                var items = AllItems.Where(i => i.DueDate >= startDate && i.DueDate <= endDate).ToList();
                foreach (var item in items)
                {
                    DataRow subDr = CreateSubTableRow(subTable, item);
                    subTable.Rows.Add(subDr);
                }

                DataRow dr = CreateMainTableRow(mainTable, workingDate, subTable, items);
                mainTable.Rows.Add(dr);
                workingDate = workingDate.AddDays(1);
            }
        }

        private static DataRow CreateMainTableRow(DataTable mainTable, DateTime workingDate, DataTable subTable, List<Item> items)
        {
            DataRow dr = mainTable.NewRow();
            dr[0] = subTable;
            dr[1] = new DateTime(workingDate.Year, workingDate.Month, workingDate.Day, 0, 0, 0);
            dr[2] = items.Sum(i => i.Hours);
            return dr;
        }

        private static DataRow CreateSubTableRow(DataTable subTable, Item item)
        {
            DataRow subDr = subTable.NewRow();
            subDr[0] = item.Hours;
            subDr[1] = item.DueDate;
            subDr[2] = item.Owner;
            return subDr;
        }

        private static DataTable CreateSubStatisticsTable()
        {
            DataTable subTable = new DataTable();
            subTable.Columns.Add("column1", typeof(long));
            subTable.Columns.Add("column2", typeof(DateTime));
            subTable.Columns.Add("column3", typeof(string));
            return subTable;
        }

        private static DataTable CreateMainStatisticsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("column1", typeof(DataTable));
            dt.Columns.Add("column2", typeof(DateTime));
            dt.Columns.Add("column3", typeof(long));
            return dt;
        }
    }
}