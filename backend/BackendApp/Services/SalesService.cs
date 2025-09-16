using BackendApp.Data;
using BackendApp.DTOs;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Services
{
    public class SalesService : ISalesService
    {
        private readonly AppDbContext _context;

        public SalesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSalesAsync(SalesDto dto)
        {
            var total = dto.Professional + dto.Pharmacy + dto.EcommerceB2C +
                        dto.EcommerceB2B + dto.ThirdParty + dto.Other;

            var eurTotal = total * 0.22m; // 💡 na sztywno kurs, później NBP API

            var sales = new SalesReports
            {
                DistributorId = dto.DistributorId,
                Quarter = dto.Quarter,
                Currency = dto.Currency,
                Professional = dto.Professional,
                Pharmacy = dto.Pharmacy,
                EcommerceB2C = dto.EcommerceB2C,
                EcommerceB2B = dto.EcommerceB2B,
                ThirdParty = dto.ThirdParty,
                Other = dto.Other,
                NewClients = dto.NewClients,
                Total = total,
                EurTotal = eurTotal
            };

            _context.SalesReports.Add(sales);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SalesReports>> GetSalesAsync(int distributorId)
        {
            return await _context.SalesReports
                .Where(s => s.DistributorId == distributorId)
                .ToListAsync();
        }
    }
}
