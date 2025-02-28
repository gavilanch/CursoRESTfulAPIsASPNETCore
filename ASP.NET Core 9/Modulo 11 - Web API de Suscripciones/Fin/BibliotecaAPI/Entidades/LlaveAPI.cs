namespace BibliotecaAPI.Entidades
{
    public class LlaveAPI
    {
        public int Id { get; set; }
        public required string Llave { get; set; }
        public TipoLlave TipoLlave { get; set; }
        public bool Activa { get; set; }
        public required string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<RestriccionIP> RestriccionesIP { get; set; } = [];
        public List<RestriccionDominio> RestriccionesDominio { get; set; } = [];
    }

    public enum TipoLlave
    {
        Gratuita = 1,
        Profesional = 2
    }
}
