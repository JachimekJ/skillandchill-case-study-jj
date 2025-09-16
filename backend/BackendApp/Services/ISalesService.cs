using BackendApp.DTOs;
using BackendApp.Models;

namespace BackendApp.Services
{
    public interface ISalesService
    {
        Task AddSalesAsync(SalesDto dto);
        Task<List<SalesReports>> GetSalesAsync(int distributorId);
    }
}
