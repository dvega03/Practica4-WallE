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
            Assert.AreEqual("0: Place0 : Description de Place0", info, "ERROR: Deberia ser null ya que la posicion especificada no es valida");

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

        [TestMethod]
        public void PickItems()
        {
            //Arrange
            Map m = new Map(1, 4);

            m.places[0].itemsInPlace = new Lista.Lista();
            int items1, items2;

            for (int i = 0; i < m.items.Length; i++)
            {
                m.places[0].itemsInPlace.insertaFin(i);
                m.items[i].name = "Place" + i;
                m.items[i].description = "PlaceDesc" + i;
                
            }


            //Act
            items1 = m.places[0].itemsInPlace.cuentaEltos();
            m.PickItemPlace(0, 0);
            items2 = m.places[0].itemsInPlace.cuentaEltos();

            //Assert
            Assert.AreEqual(items1 - 1, items2, "ERROR: Deberia haber un item menos");

        }

        [TestMethod]
        public void DropItemPlace()
        {
            //Arrange
            Map m = new Map(1, 4);

            m.places[0].itemsInPlace = new Lista.Lista();
            int items1, items2;

            for (int i = 0; i < m.items.Length; i++)
            {
                m.places[0].itemsInPlace.insertaFin(i);
                m.items[i].name = "Place" + i;
                m.items[i].description = "PlaceDesc" + i;

            }


            //Act
            items1 = m.places[0].itemsInPlace.cuentaEltos();
            m.DropItemPlace(0, 0);
            items2 = m.places[0].itemsInPlace.cuentaEltos();

            //Assert
            Assert.AreEqual(items1 + 1, items2, "ERROR: Deberia haber un item menos");

        }

        [TestMethod]
        public void MoveNoHayPlaces()
        {
            //Arrange
            Map m = new Map(0, 0);
            int pl = 0;
            Direction dir = Direction.North;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(-1, newpos, "ERROR: Deberia devolver -1 porque no hay Places");
        }

        [TestMethod]
        public void MoveNorth()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int [4];
            for(int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            int pl = 0;
            Direction dir = Direction.North;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(1, newpos, "ERROR: Deberia devolver 1 porque existe la conexión al norte");
        }

        [TestMethod]
        public void MoveSouth()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            int pl = 0;
            Direction dir = Direction.South;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(2, newpos, "ERROR: Deberia devolver 2 porque existe la conexión al sur");
        }

        [TestMethod]
        public void MoveEast()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            int pl = 0;
            Direction dir = Direction.East;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(3, newpos, "ERROR: Deberia devolver 3 porque existe la conexión al este");
        }

        [TestMethod]
        public void MoveWest()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            int pl = 0;
            Direction dir = Direction.West;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(4, newpos, "ERROR: Deberia devolver 4 porque existe la conexión al oeste");
        }

        [TestMethod]
        public void MoveNorthNoHayConnection()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            m.places[0].connections[0] = -1;
            int pl = 0;
            Direction dir = Direction.North;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(-1, newpos, "ERROR: Deberia devolver -1 porque no existe la conexión al norte");
        }

        [TestMethod]
        public void MoveSouththNoHayConnection()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            m.places[0].connections[1] = -1;
            int pl = 0;
            Direction dir = Direction.South;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(-1, newpos, "ERROR: Deberia devolver -1 porque no existe la conexión al sur");
        }

        [TestMethod]
        public void MoveEastNoHayConnection()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            m.places[0].connections[2] = -1;
            int pl = 0;
            Direction dir = Direction.East;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(-1, newpos, "ERROR: Deberia devolver -1 porque no existe la conexión al este");
        }

        [TestMethod]
        public void MoveWestNoHayConnection()
        {
            //Arrange
            Map m = new Map(5, 0);
            m.places[0].connections = new int[4];
            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = i + 1;
            }
            m.places[0].connections[3] = -1;
            int pl = 0;
            Direction dir = Direction.West;

            //Act
            int newpos = m.Move(pl, dir);

            //Assert
            Assert.AreEqual(-1, newpos, "ERROR: Deberia devolver -1 porque no existe la conexión al oeste");
        }

        [TestMethod]
        public void IsSpaceShipNoHayPlace()
        {
            //Arrange
            Map m = new Map(0, 0);

            //Act
            bool spaceShip = m.isSpaceship(0);

            //Assert
            Assert.AreEqual(false, spaceShip, "ERROR: Deberia ser false ya que no hay place");

        }

        [TestMethod]
        public void IsSpaceShipTrue()
        {
            //Arrange
            Map m = new Map(1, 0);

            //Act
            m.places[0].spaceShip = true;
            bool spaceShip = m.isSpaceship(0);

            //Assert
            Assert.AreEqual(true, spaceShip, "ERROR: Deberia ser true");
        }

        [TestMethod]
        public void IsSpaceShipFalse()
        {
            //Arrange
            Map m = new Map(1, 0);

            //Act
            m.places[0].spaceShip = false;
            bool spaceShip = m.isSpaceship(0);

            //Assert
            Assert.AreEqual(false, spaceShip, "ERROR: Deberia ser true");
        }
    }
}
