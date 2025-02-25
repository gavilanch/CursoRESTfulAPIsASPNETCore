namespace BibliotecaAPI.DTOs
{
    public class AutorCreacionDTOConFoto: AutorCreacionDTO
    {
        public IFormFile? Foto { get; set; }
    }
}
