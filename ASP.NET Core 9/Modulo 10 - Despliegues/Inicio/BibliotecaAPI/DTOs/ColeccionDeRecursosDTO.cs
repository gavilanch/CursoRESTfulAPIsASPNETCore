namespace BibliotecaAPI.DTOs
{
    public class ColeccionDeRecursosDTO<T>: RecursoDTO where T: RecursoDTO
    {
        public IEnumerable<T> Valores { get; set; } = [];
    }
}
