using Introduccion.Entities;
using Introduccion.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas
{
    [TestClass]
    public class TransferenciasTestsConMocks
    {
        [TestMethod]
        public void TransferenciaInvalidaArrojaException()
        {
            // Preparación
            Exception expectedException = null;
            Cuenta origen = new Cuenta() { Fondos = 0 };
            Cuenta destino = new Cuenta() { Fondos = 0 };
            decimal montoATransferir = 5m;
            var mock = new Mock<IServicioValidacionesDeTransferencias>();
            string mensajeDeError = "mensaje de error";
            mock.Setup(x => x.RealizarValidaciones(origen, destino, montoATransferir)).Returns(mensajeDeError);
            var servicio = new ServicioDeTransferencias(mock.Object);

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
            Assert.AreEqual(mensajeDeError, expectedException.Message);
        }

        [TestMethod]
        public void TransferenciaValidaEditaLosFondosDeLasCuentas()
        {
            // Preparación
            Cuenta origen = new Cuenta() { Fondos = 10 };
            Cuenta destino = new Cuenta() { Fondos = 5 };
            decimal montoATransferir = 7m;
            var mock = new Mock<IServicioValidacionesDeTransferencias>();

            mock.Setup(x => x.RealizarValidaciones(origen, destino, montoATransferir)).Returns(string.Empty);

            var servicio = new ServicioDeTransferencias(mock.Object);

            // Prueba
            servicio.TransferirEntreCuentas(origen, destino, montoATransferir);

            // Verificación
            Assert.AreEqual(3, origen.Fondos);
            Assert.AreEqual(12, destino.Fondos);
        }
    }
}
