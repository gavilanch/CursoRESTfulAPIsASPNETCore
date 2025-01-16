using BibliotecaAPI.Entidades;

namespace BibliotecaAPI
{
    public interface IRepositorioValores
    {
        void InsertarValor(Valor valor);
        IEnumerable<Valor> ObtenerValores();
    }
}
