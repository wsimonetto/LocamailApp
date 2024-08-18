using MongoDB.Driver;
using LocamailApp.Models;
using LocamailApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using LocamailApp.Data.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IMongoCollection<UsuarioModel> _usuarios;

    public UsuarioRepository(MongoDBContext context)
    {
        _usuarios = context.Usuario;
    }

    public async Task CreateAsync(UsuarioModel usuarioModel)
    {
        await _usuarios.InsertOneAsync(usuarioModel);
    }

    public async Task<UsuarioModel?> GetByIdAsync(string id)
    {
        return await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<UsuarioModel?> GetByEmailAsync(string email)
    {
        return await _usuarios.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<List<UsuarioModel>> GetAllAsync()
    {
        return await _usuarios.Find(_ => true).ToListAsync();
    }

    public async Task UpdateAsync(UsuarioModel usuarioModel)
    {
        var filter = Builders<UsuarioModel>.Filter.Eq(u => u.Id, usuarioModel.Id);
        await _usuarios.ReplaceOneAsync(filter, usuarioModel);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<UsuarioModel>.Filter.Eq(u => u.Id, id);
        await _usuarios.DeleteOneAsync(filter);
    }

    public async Task<UsuarioModel> ObterPorEmail(string email)
    {
        return await _usuarios.Find(usuario => usuario.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistePorEmailAsync(string email)
    {
        var filter = Builders<UsuarioModel>.Filter.Eq(u => u.Email, email);
        return await _usuarios.Find(filter).AnyAsync();
    }

    public async Task<bool> ExistePorEmailRecuperacaoAsync(string emailRecuperacao)
    {
        var filter = Builders<UsuarioModel>.Filter.Eq(u => u.EmailRecuperacao, emailRecuperacao);
        return await _usuarios.Find(filter).AnyAsync();
    }


    public async Task<bool> ExistePorEmailOuEmailRecuperacaoAsync(string? id, string email, string? emailRecuperacao)
    {
        var filter = Builders<UsuarioModel>.Filter.Or(
         Builders<UsuarioModel>.Filter.Eq(u => u.Email, email),
         emailRecuperacao != null ? Builders<UsuarioModel>.Filter.Eq(u => u.EmailRecuperacao, emailRecuperacao) : Builders<UsuarioModel>.Filter.Empty
     );

        if (id != null)
        {
            var idFilter = Builders<UsuarioModel>.Filter.Ne(u => u.Id, id);
            filter = Builders<UsuarioModel>.Filter.And(filter, idFilter);
        }

        return await _usuarios.Find(filter).AnyAsync();
    }



}
