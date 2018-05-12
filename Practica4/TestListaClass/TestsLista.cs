using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lista;

namespace TestListaClass
{
    [TestClass]
    public class TestsLista
    {


        public void IniciaListaLLena(out Lista.Lista lista)
        {
            lista = new Lista.Lista();

            int rep = 2;
            int limite = 3;

            for (int i = 0; i < rep; i++)
            {
                for (int j = 1; j <= limite; j++)
                {
                    lista.insertaFin(j);
                }
            }
        }

        [TestMethod]
        public void CuentaEltosListaVacia()
        {
            //Arrange
            Lista.Lista lista = new Lista.Lista();

            //Act
            int eltos = lista.cuentaEltos();

            //Assert
            Assert.AreEqual(0, eltos, "ERROR");
        }

        [TestMethod]
        public void CuentaEltosListaNoVacia()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);

            //Act
            int eltos = lista.cuentaEltos();

            //Assert 
            Assert.AreEqual(6, eltos, "ERROR");
        }

        [TestMethod]
        public void InsertaFinListaVacia()
        {
            //Arrange
            Lista.Lista lista = new Lista.Lista();
            int eltos = lista.cuentaEltos();

            //Act
            lista.insertaFin(0);

            //Assert
            Assert.AreEqual(1, eltos + 1, "ERROR");

        }

        [TestMethod]
        public void InsertaFinListaNoVacia()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);
            int eltos = lista.cuentaEltos();

            //Act
            lista.insertaFin(0);
            string cadena = lista.aCadena();

            //Assert
            Assert.AreEqual(7, eltos + 1, "ERROR");
            Assert.AreEqual("1231230", cadena, "ERROR"); //Comprobar que se inserta al final.

        }

        [TestMethod]
        public void BorraEltoListaVacia()
        {
            //Arrange
            Lista.Lista lista = new Lista.Lista();


            //Act
            bool borrado = lista.borraElto(1);
            int eltos = lista.cuentaEltos();

            //Assert
            Assert.AreEqual(0, eltos, "ERROR");
            Assert.IsFalse(borrado);

        }

        [TestMethod]
        public void BorraEltoListaNoVaciaElementoEnLista()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);
            int elto = lista.cuentaEltos();
            //Act
            bool borrado = lista.borraElto(2);
            //Assert
            Assert.IsTrue(borrado, "ERROR");
            Assert.AreEqual(elto - 1, lista.cuentaEltos(), "ERROR");
            Assert.AreEqual("13123", lista.aCadena(), "ERROR");


        }

        [TestMethod]
        public void BorraEltoListaNoVaciaElementoNoEstaEnLista()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);
            int elto = lista.cuentaEltos();
            //Act
            bool borrado = lista.borraElto(5);
            //Assert
            Assert.IsFalse(borrado, "ERROR");
            Assert.AreEqual(elto, lista.cuentaEltos(), "ERROR");
            Assert.AreEqual("123123", lista.aCadena(), "ERROR");


        }

        [TestMethod]
        public void NEsimoListaVacia()
        {
            //Arrange
            Lista.Lista lista = new Lista.Lista();

            //Act
            try
            {
                int dato = lista.nEsimo(0);
                Assert.Fail("ERROR : La lista esta vacia.");
            }

            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public void NEsimoListaNovaciaPosicionEnLista()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);
            //Act
            try
            {
                int dato = lista.nEsimo(1);
                Assert.AreEqual(2, dato, "ERROR : No se he obtenido el valor correcto en la posion especificada");

            }
            catch
            {
                Assert.Fail("ERROR : La lista no esta vacia y si que existe un nodo en la posicion especificada.");
            }
        }

        [TestMethod]
        public void NEsimoListaNoVaciaPosicionNoEnLista()
        {
            //Arrange
            Lista.Lista lista;
            IniciaListaLLena(out lista);

            //Act
            try
            {
                int dato = lista.nEsimo(-3);
                Assert.Fail("ERROR: Debe lanzar una excepcion.");
            }
            catch
            {

            }
        }
    }
}
