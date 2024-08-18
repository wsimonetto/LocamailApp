using LocamailApp.Models;

namespace LocamailApp.Services
{
    public interface IEmailEnviadoService
    {
        Task<IEnumerable<EmailEnviadoModel>> GetAllAsync();
        Task<EmailEnviadoModel?> GetByIdAsync(string id);
        Task CreateAsync(EmailEnviadoModel emailEnviado);
        Task UpdateAsync(EmailEnviadoModel emailEnviado);
        Task DeleteAsync(string id);
        Task<bool> ExistePorEmailAsync(string email);
        Task<int> GetMaxIntIdAsync();
    }

}
