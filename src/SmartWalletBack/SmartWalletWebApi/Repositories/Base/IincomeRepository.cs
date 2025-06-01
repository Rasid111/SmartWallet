namespace SmartWalletWebApi.Repositories.Base;

public interface IIncomeRepository
{
    Task<IEnumerable<Income>> AllIncomesAsync();
    public Task<IEnumerable<Income>> GetByUserId(int userId);
    public Task<Income?> GetByid(int id);
    public Task<int> AddIncome(Income income);

    public Task BulkCreate(List<Income> incomes);
}
