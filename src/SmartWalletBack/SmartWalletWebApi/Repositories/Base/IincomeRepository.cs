public interface IBaseRepository
{
    public Task<List<Income>> GetByUserId(int userId);
    public Task<Income?> GetByid(int id);
    public Task<int> Create(IncomeDto dto);
}