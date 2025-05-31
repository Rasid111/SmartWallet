using SmartWalletWebApi.Dtos.Income;

namespace SmartWalletWebApi.Service.Base;
public interface IIncomeService
{
    Task<IEnumerable<GetIncomeResponseDto>> AllIncomesAsync();
    public Task<IEnumerable<GetIncomeResponseDto>> GetByUserId(int userId);
    public Task<GetIncomeResponseDto?> GetByid(int id);
    public Task<int> AddIncome(AddIncomeRequestDto dto);
}
