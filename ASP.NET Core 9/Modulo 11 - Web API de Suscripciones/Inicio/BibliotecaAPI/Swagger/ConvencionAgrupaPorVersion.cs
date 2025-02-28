using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace BibliotecaAPI.Swagger
{
    public class ConvencionAgrupaPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // Ejemplo: "Controllers.V1"
            var namespaceDelControlador = controller.ControllerType.Namespace;
            var version = namespaceDelControlador!.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = version;
        }
    }
}
