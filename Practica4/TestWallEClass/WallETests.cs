﻿using System;
using Mapa;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestWallEClass
{
    [TestClass]
    public class WallETests
    {
        [TestMethod]
        public void Move()
        {
            //Arrange
            Map m = new Map(2, 0);
            WallE.WallE w = new WallE.WallE();

            m.places[0].connections = new int[4];

            for (int i = 0; i < m.places[0].connections.Length; i++)
            {
                m.places[0].connections[i] = -1;
            }

            m.places[0].connections[0] = 1;

            

            //Act
            Direction dir = Direction.North;
            w.Move(m, dir);

            //Assert

            Assert.AreEqual(1, w.GetPosition(), "ERROR: La posicion cambia con el Move");
        }

        [TestMethod]
        public void PickItems()
        {
            //Arrange
            Map m = new Map(1,1);
            WallE.WallE w = new WallE.WallE();
            m.items = new Map.Item[1];
            m.places[0].itemsInPlace = new Lista.Lista();
            m.items[0].name = "Item1";
            m.items[0].description = "ItemDesc1";
            m.places[0].itemsInPlace.insertaFin(0);
            //Act
            w.PickItem(m, 0);
            //Assert
            Assert.AreEqual(0, m.places[0].itemsInPlace.cuentaEltos(), "ERROR: No ha quitado el elemento de la lista");
            Assert.AreEqual(1, w.bag.cuentaEltos(), "ERROR: No ha añadido el elemento a la lista");
            Assert.AreEqual("0: Item1 ItemDesc1" + '\n', w.Bag(m), "ERROR: No es el objeto especificado");
            Assert.AreEqual(null, m.GetItemsPlace(0), "ERROR: No es el item correcto");
        }

        [TestMethod]
        public void DropItems()
        {
            //Arrange
            Map m = new Map(1, 1);
            WallE.WallE w = new WallE.WallE();
            m.items = new Map.Item[1];
            m.places[0].itemsInPlace = new Lista.Lista();
            m.items[0].name = "Item1";
            m.items[0].description = "ItemDesc1";
            w.bag.insertaFin(0);
            //Act
            w.DropItem(m, 0);
            //Assert
            Assert.AreEqual(1, m.places[0].itemsInPlace.cuentaEltos(), "ERROR: No ha quitado el elemento de la lista");
            Assert.AreEqual(0, w.bag.cuentaEltos(), "ERROR: No ha añadido el elemento a la lista");
            Assert.AreEqual(null, w.Bag(m), "ERROR: No es el objeto especificado");
            Assert.AreEqual("0: Item1 ItemDesc1" + '\n', m.GetItemsPlace(0), "ERROR: No es el item correcto");
        }

        [TestMethod]
        public void Bag()
        {
            //Arrange
            string infoBagExpected = null;
            Map m = new Map(1, 3);
            WallE.WallE w = new WallE.WallE();
            m.items = new Map.Item[1];
            m.places[0].itemsInPlace = new Lista.Lista();
            for(int i = 0; i < m.items.Length; i++)
            {
                m.items[0].name = "Item1";
                m.items[0].description = "ItemDesc1";
                w.bag.insertaFin(0);
                infoBagExpected = i+": " + m.items[0].name + " " + m.items[0].description + '\n';
            }
            //Act
            string infoBag = w.Bag(m);
            //Assert
            Assert.AreEqual(infoBagExpected, infoBag, "ERROR: No ha leido bien los datos");
            
        }

        [TestMethod]
        public void AtSpaceShipTrue()
        {
            //Arrange
            Map m = new Map(1, 0);
            WallE.WallE w = new WallE.WallE();
            w.pos = 0;
            m.places[0].spaceShip = true;
            //Act
            //Assert
            Assert.IsTrue(w.atSpaceShip(m), "ERROR: No se han leigo bien los datos");

        }

        [TestMethod]
        public void AtSpaceShipFalse()
        {
            //Arrange
            Map m = new Map(1, 0);
            WallE.WallE w = new WallE.WallE();
            w.pos = 0;
            m.places[0].spaceShip = false;
            //Act
            //Assert
            Assert.IsFalse(w.atSpaceShip(m), "ERROR: No se han leigo bien los datos");

        }


    }
}
