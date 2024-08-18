using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LocamailApp.Models
{
    public class AlteracaoSenhaModel
    {
        [BsonElement("email")]
        public string? Email { get; set; } = string.Empty;// E-mail do usuário (ou Id, se preferir)

        [BsonElement("senha_atual")]
        public string? SenhaAtual { get; set; } = string.Empty;// Senha atual para validação

        [BsonElement("nova_senha")]
        public string? NovaSenha { get; set; } = string.Empty;// Nova senha que o usuário deseja definir

        public AlteracaoSenhaModel() { }
    }

}
