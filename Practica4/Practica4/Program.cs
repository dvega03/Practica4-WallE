using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lista;
using System.IO;


namespace WallE
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

            StreamReader entrada = new StreamReader("madrid.map.txt");

            
            string texto = entrada.ReadToEnd();
            string[] parrafos = texto.Split('\n');

            for(int i = 0; i < parrafos.Length; i++)
            {
                string[] palabras = parrafos[i].Split(' ');
                switch (palabras[0])
                {
                    case "place":
                        CreatePlace( parrafos,ref map,i);
                        break;
                    case "street":
                        CreateStreet(parrafos,ref map,i);
                        break;
                    case "garbage":
                        CreateItem(parrafos,ref map,i);
                        break;
                }
            }
        }

        private void CreatePlace(string[] parrafos, ref Map mapa,int i)
        {
            bool spaceShip = false;

            string []linea = parrafos[i].Split(' ');

            mapa.places[int.Parse(linea[1])].name = linea[2];

            if(linea[3] == "noSpaceShip")
            {
                spaceShip = false;
            }
            else
            {
                spaceShip = true;
            }
            mapa.places[int.Parse(linea[1])].spaceShip = spaceShip;

            
            int start = parrafos[i].IndexOf("(") + 1;
            int end = parrafos[i].IndexOf(")", start);
            string desc = parrafos[i].Substring(start, end - start);

            mapa.places[int.Parse(linea[1])].description = desc;




        }

        private void CreateStreet(string[] parrafos, ref Map mapa )
        {

        }

        private void CreateItem(string []parrafos, ref Map mapa)
        {


        }
    }
}
