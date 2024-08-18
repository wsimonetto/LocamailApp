using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LocamailApp.Models
{
    public class UsuarioModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // MongoDB ObjectId como string

        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonElement("email")]
        public string Email { get; set; } // E-mail do usuário

        [BsonElement("email_recuperacao")]
        public string? EmailRecuperacao { get; set; } // E-mail alternativo para recuperação de senha

        [BsonElement("senha")]
        public string Senha { get; set; } // Senha deve ser armazenada de forma segura (hash)

        [BsonElement("tema")]
        public string Tema { get; set; } // Tema de preferência do usuário (ex.: "dark" ou "light")

        [BsonElement("cor")]
        public string Cor { get; set; } // Cor de preferência do usuário

        [BsonElement("categorias")]
        public List<string> Categorias { get; set; } // Categorias personalizadas pelo usuário

        [BsonElement("rotulos")]
        public List<string> Rotulos { get; set; } // Rótulos personalizados pelo usuário

        [BsonElement("data_criacao")]
        public DateTime DataCriacao { get; set; } // Data de criação da conta

        [BsonElement("data_ultima_atualizacao")]
        public DateTime DataUltimaAtualizacao { get; set; } // Data da última atualização das preferências

    }

}
