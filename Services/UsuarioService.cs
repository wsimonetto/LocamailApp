using LocamailApp.Data.Repository;
using LocamailApp.Exceptions;
using LocamailApp.Models;
using System.Threading.Tasks;

namespace LocamailApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;

        public UsuarioService(IPasswordService passwordService, IUsuarioRepository usuarioRepository)
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task CreateUserAsync(string nome, string email, string senha, string? emailRecuperacao = null)
        {
            // Passando null como o id, pois estamos criando um novo usuário
            if (await _usuarioRepository.ExistePorEmailOuEmailRecuperacaoAsync(null, email, emailRecuperacao))
            {
                throw new UsuarioJaExisteException("Já existe um usuário cadastrado com esse e-mail ou e-mail de recuperação.");
            }

            var senhaHash = _passwordService.HashPassword(senha);
            var novoUsuario = new UsuarioModel
            {
                Nome = nome,
                Email = email,
                EmailRecuperacao = emailRecuperacao,
                Senha = senhaHash,
                DataCriacao = DateTime.UtcNow,
                DataUltimaAtualizacao = DateTime.UtcNow
            };

            await _usuarioRepository.CreateAsync(novoUsuario);
        }

        public async Task<string> AlterarSenha(AlteracaoSenhaModel modelo)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(modelo.Email);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            // Verifique se a senha atual está correta
            if (!_passwordService.VerifyPassword(usuario.Senha, modelo.SenhaAtual))
            {
                throw new Exception("Senha atual incorreta.");
            }

            // Criptografa a nova senha
            var novaSenhaHash = _passwordService.HashPassword(modelo.NovaSenha);

            // Atualiza a senha do usuário
            usuario.Senha = novaSenhaHash;
            await _usuarioRepository.UpdateAsync(usuario);

            return "Senha alterada com sucesso.";
        }

        public async Task AtualizarEmailRecuperacaoAsync(string id, string novoEmailRecuperacao)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (await _usuarioRepository.ExistePorEmailOuEmailRecuperacaoAsync(id, usuario.Email, novoEmailRecuperacao))
            {
                throw new Exception("Já existe um usuário cadastrado com esse e-mail recuperação.");
            }

            usuario.EmailRecuperacao = novoEmailRecuperacao;
            usuario.DataUltimaAtualizacao = DateTime.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
        }

    }

}

