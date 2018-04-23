using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lista;
using System.IO;


namespace Mapa
{
    // posibles direcciones
    public enum Direction { North, South, East, West };
    class Map
    {

        // items basura
        struct Item
        {
            public string name, description;
        }
        // lugares del mapa
        struct Place
        {
            public string name, description;
            public bool spaceShip;
            public int[] connections; // vector de 4 componentes
            // con el lugar al norte, sur, este y oeste
            // -1 si no hay conexion
            public Lista.Lista itemsInPlace; // lista de enteros, indices al vector de items
            
        }
        Place[] places; // vector de lugares del mapa
        Item[] items; // vector de items del juego
        int nPlaces, nItems; // numero de lugares y numero de items del mapa

        public Map(int nPlaces, int nItems)
        {
            places = new Place[nPlaces];
            items = new Item[nItems];
        }

        public void ReadMap(string file)
        {
            Map map = new Map(10, 3);
            StreamReader entrada = new StreamReader(file);
            Lista.Lista lista = new Lista.Lista();
            lista = null;

            for (int i = 0; i < places.Length; i++)
            {
                for (int j = 0; j < map.places[i].connections.Length; j++)
                {
                    
                     map.places[i].connections[j] = -1;
                    
                }
            }
                
           while(entrada.EndOfStream == false)
           {
               string linea = entrada.ReadLine();


               string[] palabras = linea.Split(' ');
               switch (palabras[0])
               {
                   case "place":
                       CreatePlace(palabras, entrada);
                       break;
                   case "street":
                       CreateStreet(palabras);
                       break;
                   case "garbage":
                       CreateItem(palabras, ref lista);
                       break;
               }
           }
        }

        private void CreatePlace(string []palabras,  StreamReader f)
        {


            bool spaceShip = false;

            places[int.Parse(palabras[1])].name = palabras[2];
            if (palabras[3] == "noSpaceship") spaceShip = false;
            else spaceShip = true;

            places[int.Parse(palabras[1])].spaceShip = spaceShip;


            places[int.Parse(palabras[1])].description = ReadDescription(f);

        }

        private void CreateStreet(string[] palabras)
        {
            int place = int.Parse(palabras[3]);
            int placeToGo = int.Parse(palabras[6]);
            int index = 0;
            int indexReverse = 0;
            
            

            if(palabras[2] == "place")
            {
                switch(palabras[4])
                {
                    case "north":
                        index = 0;
                        indexReverse = 1;
                        break;

                    case "south":
                        index = 1;
                        indexReverse = 0;
                        break;

                    case "east":
                        index = 2;
                        indexReverse = 3;
                        break;

                    case "west":
                        index = 3;
                        indexReverse = 2;
                        break;
                }

                places[place].connections[index] = placeToGo;
                places[placeToGo].connections[indexReverse] = place;
            }
        }

        private void CreateItem(string []palabras, ref Lista.Lista lista)
        {
            items[int.Parse(palabras[1])].name = palabras[2];
            items[int.Parse(palabras[1])].description = palabras[5];
            
            lista.insertaFin(int.Parse(palabras[1]));
        }

        private string ReadDescription(StreamReader f)
        {
            string desc = string.Empty;
            

            while(f.ReadLine() != string.Empty)
            {
                desc = desc + f.ReadLine() + '\n';
            }

            string description = desc.Replace('"'.ToString(), string.Empty);

            return description;
        }

        public string GetPlaceInfo(int pl)
        {
            return places[pl].description;
        }

        public string GetMoves(int pl)
        {
            string conexiones = "";
            for (int i = 0; i < places[pl].connections.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (places[pl].connections[i] != -1)
                        {
                            conexiones = conexiones + "North: " + places[places[pl].connections[i]].name + "\n";
                        }
                        else
                        {
                            conexiones = conexiones + "North: No hay nada más al Norte\n";
                        }
                        break;
                    case 1:
                        if (places[pl].connections[i] != -1)
                        {
                            conexiones = conexiones + "South: " + places[places[pl].connections[i]].name + "\n";
                        }
                        else
                        {
                            conexiones = conexiones + "South: No hay nada más al Sur\n";
                        }
                        break;
                    case 2:
                        if (places[pl].connections[i] != -1)
                        {
                            conexiones = conexiones + "East: " + places[places[pl].connections[i]].name + "\n";
                        }
                        else
                        {
                            conexiones = conexiones + "East: No hay nada más al Este\n";
                        }
                        break;
                    case 3:
                        if (places[pl].connections[i] != -1)
                        {
                            conexiones = conexiones + "West: " + places[places[pl].connections[i]].name + "\n";
                        }
                        else
                        {
                            conexiones = conexiones + "West: No hay nada más al Oeste\n";
                        }
                        break;
                }
            }
            return conexiones;
        }

        /*public int GetNumItems (int pl)
        {
            
        }*/


    }
    
}
