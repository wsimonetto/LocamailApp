using LocamailApp.Models;

namespace LocamailApp.Services
{
    public interface IUsuarioService
    {
        Task CreateUserAsync(string nome, string email, string senha, string? emailRecuperacao = null);
        Task<string> AlterarSenha(AlteracaoSenhaModel modelo);
        Task AtualizarEmailRecuperacaoAsync(string id, string novoEmailRecuperacao);

    }

}
