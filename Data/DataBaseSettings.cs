namespace LocamailApp.Data
{
    public class DataBaseSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsuarioCollectionName { get; set; } = null!;
        public string EmailEnviadoCollectionName { get; set; } = null!;
        public string EmailRecebidoCollectionName { get; set; } = null!;
    }

}
