using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapa;

namespace TestsClassMap
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void GetMovesNoHayPlace()
        {
            //Arrange
            Map m = new Map(0, 0);
            //Act

            string moves = m.GetMoves(3);
            //Assert
            Assert.AreEqual(null, moves, "ERROR: Deberia ser null ya que no hay array de places");
        }

        [TestMethod]
        public void GetMovesNoHayConnections()
        {
            //Arrange
            Map m = new Map(1, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = -1;
            }

            //Act
            string moves = m.GetMoves(0);

            //Assert
            Assert.AreEqual(null, moves, "ERROR: Deberia ser null ya que no hay connections");

        }
        [TestMethod]
        public void GetMovesHayConections()
        {
            //Arrange
            Map m = new Map(2, 0);
            for (int i = 0; i < m.places.Length; i++)
            {
                m.places[0].connections = new int[4];
            }
            m.places[1].name = "Place1";

            for (int j = 0; j < m.places[0].connections.Length; j++)
            {
                m.places[0].connections[j] = -1;
            }
            m.places[0].connections[0] = 1;

            //Act
            string moves = m.GetMoves(0);

            //Assert
            Assert.AreEqual("north : Place1" + '\n', moves, "ERROR: No lee los datos adecuadamente");
        }

        [TestMethod]
        public void GetNumItemsNoHayPlaces()
        {
            //Arrange
            Map m = new Map(0, 0);
            //Act

            int items = m.GetNumItems(0);
            //Assert
            Assert.AreEqual(0, items, "ERROR: Deberia ser null ya que no hay array de places");
            
        }

        [TestMethod]
        public void GetNumItemsListaNoLlena()
        {
            //Arrange
            Map m = new Map(1, 0);
            m.places[0].itemsInPlace = new Lista.Lista();
            //Act
            int items = m.GetNumItems(0);
            //Assert
            Assert.AreEqual(0, items, "ERROR: Deberia ser null ya que no hay lista de items");

        }

        [TestMethod]
        public void GetNumItemsListaLlena()
        {
            //Arrange

            Map m = new Map(1, 0);

            m.places[0].itemsInPlace = new Lista.Lista();
            m.places[0].itemsInPlace.insertaIni(1);


            //Act
            int items = m.GetNumItems(0);
            int eltos = m.places[0].itemsInPlace.cuentaEltos();
            //Assert
            Assert.AreEqual(1, items, "ERROR: No cuenta bien los valores.");

        }

        [TestMethod]
        public void GetItemInfoArrayVacio()
        {
            //Arrange
            Map m = new Map(0, 0);

            //Act
            string info =  m.GetItemsInfo(0);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que no existe una lista.");
        }

        [TestMethod]
        public void GetItemsInfoNoEstaItem()
        {
            //Arrange
            Map m = new Map(0, 1);
            
            //Act
            string info = m.GetItemsInfo(0);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que no existe un item.");
            
        }

        [TestMethod]
        public void GetItemsInfoPosicionNoValidaArray()
        {
            //Arrange
            Map m = new Map(0, 1);

            //Act
            string info = m.GetItemsInfo(-3);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que la posicion especificada no es valida");

        }

        [TestMethod]
        public void GetItemsInfoHayItem()
        {
            //Arrange
            Map m = new Map(0, 1);
            m.items[0].name = "Place0";
            m.items[0].description = "Description de Place0";

            //Act
            string info = m.GetItemsInfo(0);

            //Assert
            Assert.AreEqual("0: Place0 Description de Place0", info, "ERROR: Deberia ser null ya que la posicion especificada no es valida");

        }

        [TestMethod]
        public void GetItemsPlaceNoHayPlace()
        {
            //Arrange
            Map m = new Map(0, 1);
            
            //Act
            string info = m.GetItemsPlace(0);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que no hay place");
            
        }

        [TestMethod]
        public void GetItemsPlaceNoHayItems()
        {
            //Arrange
            Map m = new Map(1, 0);

            //Act
            string info = m.GetItemsPlace(0);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que no hay items");

        }

        [TestMethod]
        public void GetItemsPlaceNoHayItemsInPlace()
        {
            //Arrange
            Map m = new Map(1, 0);
            m.places[0].itemsInPlace = null;

            //Act
            string info = m.GetItemsPlace(0);

            //Assert
            Assert.AreEqual(null, info, "ERROR: Deberia ser null ya que no hay items");

        }

        [TestMethod]
        public void GetItemsPlaceHayItems()
        {
            //Arrange
            Map m = new Map(1, 4);

            string expected = null;

            m.places[0].itemsInPlace = new Lista.Lista();

            for(int i = 0; i < m.items.Length; i++)
            {
                m.places[0].itemsInPlace.insertaFin(i);
                m.items[i].name = "Place" + i;
                m.items[i].description = "PlaceDesc" + i;
                expected += i + ": " + "Place"+ i + " " + "PlaceDesc"+ i + '\n';
            }
            
            
            //Act
            string info = m.GetItemsPlace(0);

            //Assert
            Assert.AreEqual(expected, info, "ERROR: Deberia ser null ya que no hay items");
           

        }

    }
}
