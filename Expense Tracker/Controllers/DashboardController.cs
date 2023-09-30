﻿using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Expense_Tracker.Controllers
{    
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            // Last 7 Days

            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;


            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            //Total Income
            int TotalIncome = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
                ViewBag.TotalIncome = TotalIncome.ToString("C0");



            //Total Expense
            int TotalExpense = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            //Balance
            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString("C0");

            //Doughnut Chart - Expense By Category
            //Following syncfusion documentation used anonymous object instead of creating spread class and it corresponding object
            ViewBag.DoughnutChartData = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon+ " "+ k.First().Category.Title,
                    amount = k.Sum( j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0")
                })
                .ToList();

            return View();
        }
    }
}
