using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Servicios
{
    public interface IServicioHash
    {
        ResultadoHashDTO Hash(string input);
        ResultadoHashDTO Hash(string input, byte[] sal);
    }
}