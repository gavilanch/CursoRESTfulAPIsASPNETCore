namespace BibliotecaAPI.Entidades
{
    public class Error
    {
        public Guid Id { get; set; }
        public required string MensajeDeError { get; set; }
        public string? StrackTrace { get; set; }
        public DateTime Fecha { get; set; }
    }
}
