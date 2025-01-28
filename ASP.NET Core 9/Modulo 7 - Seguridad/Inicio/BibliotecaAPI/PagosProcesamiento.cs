using Microsoft.Extensions.Options;

namespace BibliotecaAPI
{
    public class PagosProcesamiento
    {
        private TarifaOpciones _tarifaOpciones;

        public PagosProcesamiento(IOptionsMonitor<TarifaOpciones> optionsMonitor)
        {
            _tarifaOpciones = optionsMonitor.CurrentValue;

            optionsMonitor.OnChange(nuevaTarifa =>
            {
                Console.WriteLine("tarifa actualizada");
                _tarifaOpciones = nuevaTarifa;
            });
        }

        public void ProcesarPago()
        {
            // Aqui usamos las tarifas
        }

        public TarifaOpciones ObtenerTarifas()
        {
            return _tarifaOpciones;
        }
    }
}
