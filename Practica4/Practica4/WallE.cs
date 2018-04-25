using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lista;
using Mapa;

namespace WallE
{

    class Program
    {
        static void Main(string[] args)
        {
            Map m = new Map(10,3);
            WallE w = new WallE();

            m.ReadMap("madrid.map.txt");
            string comando = null;

            while (!m.isSpaceship(w.GetPosition()) || comando != "quit")
            {
                Console.Write(">");
                comando = Console.ReadLine();
                ProcesaInput(comando, w, m);
            }
            
            

        }

        static void ProcesaInput(string com, WallE w, Map m)
        {
            string[] splitcom = com.Split(' ');

            switch(splitcom[0])
            {
                case "go":
                    w.Move(m, String2Dir(splitcom[1]));
                    break;
                case "pick":
                    w.PickItem(m, int.Parse(splitcom[1]));
                    break;
                case "drop":
                    w.DropItem(m, int.Parse(splitcom[1]));
                    break;
                case "items":
                    Console.WriteLine(m.GetItemsPlace(w.GetPosition()));
                    break;
                case "info":
                    Console.WriteLine(m.GetPlaceInfo(w.GetPosition()));
                    break;
                case "bag":
                    Console.WriteLine(w.Bag(m));
                    break;
                case "quit":
                    
                    break;
            }


        }

        public static Direction String2Dir(string coord)
        {
            Direction dir = Direction.North;

            switch(coord)        
            {
                case "north":
                    dir = Direction.North;
                    break;
                case "south":
                    dir = Direction.South;
                    break;
                case "east":
                    dir = Direction.East;
                    break;
                case "west":
                    dir = Direction.West;
                    break;
            }

            return dir;
            
        }

        

    }


    class WallE
    {
        int pos; //posicion de Wall-e en el mapa
        Lista.Lista bag; // lista de items recogidos por wall-e
                         //(son indices a la lista de items del mapa)

        public WallE()
        {
            pos = 0;
            bag = new Lista.Lista();
        }

        public int GetPosition()
        {
            return pos;
        }

        public void Move(Map m, Direction dir)
        {
            pos = m.Move(pos, dir);
        }
        public void PickItem(Map m, int it)
        {
            m.PickItemPlace(pos, it);
            bag.insertaFin(it);
        }
        public void DropItem(Map m, int it)
        {
            m.DropItemPlace(pos, it);
            bag.borraNesimo(it);
        }
        public string Bag(Map m)
        {
            string items = null;
            int i = 0;
            while (i < bag.cuentaEltos())
            {
                items = items + m.GetItemsInfo(bag.nEsimo(i)) + '\n';
                i++;
            }

            return items;
        }

        public bool atSpaceShip(Map map)
        {
            return map.isSpaceship(pos);
        }

    }



}
