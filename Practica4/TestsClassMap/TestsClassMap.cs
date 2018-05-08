using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapa;

namespace TestsClassMap
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetMovesNoHayPlace()
        {
            //Arrange
            Map m = new Map(0, 0);
            //Act
            
            string moves = m.GetMoves(3);
            //Assert
            Assert.AreEqual(null, moves,"ERROR: Deberia ser null ya que no hay array de places");
        }

        [TestMethod]
        public void GetMovesNoHayConnections()
        {
            //Arrange
            Map m = new Map(1, 0);
            m.places[0].connections = new int[4];
            for(int i = 0; i < m.places[0].connections.Length; i++)
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
            for(int i = 0; i < m.places.Length; i++)
            {
                m.places[0].connections = new int[4];
            }
            m.places[1].name = "Place1";

            for(int j = 0; j < m.places[0].connections.Length; j++)
            {
                m.places[0].connections[j] = -1;
            }
            m.places[0].connections[0] = 1;

            //Act
            string moves = m.GetMoves(0);

            //Assert
            Assert.AreEqual("lol", moves, "ERROR: No lee los datos adecuadamente");
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
            m.places[0].itemsInPlace.insertaFin(1);
            //Act
            int items = m.GetNumItems(0);
            //Assert
            Assert.AreEqual(1, items, "ERROR: No cuenta bien los valores.");
        }

        
    }
}
