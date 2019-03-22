using Introduccion.Entities;
using Introduccion.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProyectoPruebas
{
    [TestClass]
    public class TransferenciasTestsSinMocks
    {
        [TestMethod]
        public void TransferenciaEntreCuentasConFondosInsuficientesArrojaUnError()
        {
            // Preparación
            Exception expectedException = null;
            Cuenta origen = new Cuenta() { Fondos = 0 };
            Cuenta destino = new Cuenta() { Fondos = 0 };
            decimal montoATransferir = 5m;
            var servicio = new ServicioDeTransferenciasSinMocks();

            // Prueba
            try
            {
                servicio.TransferirEntreCuentas(origen, destino, montoATransferir);
                Assert.Fail("Un error debió ser arrojado");
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Verificación
            Assert.IsTrue(expectedException is ApplicationException);
            Assert.AreEqual("La cuenta origen no tiene fondos suficientes para realizar la operación", expectedException.Message);
        }

        [TestMethod]
        public void TransferenciaEntreCuentasEditaLosFondos()
        {
            // Preparación
            Cuenta origen = new Cuenta() { Fondos = 10 };
            Cuenta destino = new Cuenta() { Fondos = 5 };
            decimal montoATransferir = 7m;
            var servicio = new ServicioDeTransferenciasSinMocks();

            // Prueba
            servicio.TransferirEntreCuentas(origen, destino, montoATransferir);

            // Verificación
            Assert.AreEqual(3, origen.Fondos);
            Assert.AreEqual(12, destino.Fondos);
        }
    }
}
