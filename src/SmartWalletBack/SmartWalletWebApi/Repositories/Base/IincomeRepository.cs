public interface IincomeRepository
{
    Task<IEnumerable<Income>> AllIncomesAsync();
    public Task<IEnumerable<Income>> GetByUserId(int userId);
    public Task<Income?> GetByid(int id);
    public Task<int> AddIncome(Income income);
}
