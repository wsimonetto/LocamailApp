using LocamailApp.Models;

public interface IEmailRecebidoService
{
    Task<IEnumerable<EmailRecebidoModel>> GetAllAsync();
    Task<EmailRecebidoModel?> GetByIdAsync(string id);
    Task CreateAsync(EmailRecebidoModel emailRecebidoModel);
    Task UpdateAsync(EmailRecebidoModel emailRecebidoModel);
    Task DeleteAsync(string id);
    Task<int> GetMaxIntIdAsync();
}
