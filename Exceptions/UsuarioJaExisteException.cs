namespace LocamailApp.Exceptions
{
    public class UsuarioJaExisteException : Exception
    {
        public UsuarioJaExisteException() : base("Já existe um usuário cadastrado com esse e-mail ou e-mail de recuperação.") { }

        public UsuarioJaExisteException(string message) : base(message) { }

        public UsuarioJaExisteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
