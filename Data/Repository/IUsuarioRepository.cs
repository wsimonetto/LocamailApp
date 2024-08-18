using LocamailApp.Models;

namespace LocamailApp.Data.Repository
{
    public interface IUsuarioRepository
    {
        Task CreateAsync(UsuarioModel usuario);
        Task<UsuarioModel?> GetByIdAsync(string id);
        Task<UsuarioModel?> GetByEmailAsync(string email);
        Task<List<UsuarioModel>> GetAllAsync();
        Task UpdateAsync(UsuarioModel usuario);
        Task DeleteAsync(string id);
        Task<bool> ExistePorEmailAsync(string email);
        Task<bool> ExistePorEmailRecuperacaoAsync(string emailRecuperacao);
        Task<bool> ExistePorEmailOuEmailRecuperacaoAsync(string id, string email, string? emailRecuperacao);
    }

}
