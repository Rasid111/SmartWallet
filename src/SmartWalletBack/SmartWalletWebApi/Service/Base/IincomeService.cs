using SmartWalletWebApi.Dtos.Income;

namespace SmartWalletWebApi.Service.Base;
public interface IIncomeService
{
    Task<IEnumerable<Income>> AllIncomesAsync();
    public Task<IEnumerable<Income>> GetByUserId(int userId);
    public Task<Income?> GetByid(int id);
    public Task<int> AddIncome(AddIncomeRequestDto dto);
}
