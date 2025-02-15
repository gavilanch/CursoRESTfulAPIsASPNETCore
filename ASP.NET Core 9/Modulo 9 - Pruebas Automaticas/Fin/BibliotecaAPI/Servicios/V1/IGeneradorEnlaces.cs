using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Servicios.V1
{
    public interface IGeneradorEnlaces
    {
        Task GenerarEnlaces(AutorDTO autorDTO);
        Task<ColeccionDeRecursosDTO<AutorDTO>> GenerarEnlaces(List<AutorDTO> autores);
    }
}