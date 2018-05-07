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
           
            StreamReader entrada = new StreamReader(file);
            
            

            

            while (entrada.EndOfStream == false)
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
                        CreateItem(palabras);
                        break;
                }
            }

            entrada.Close();
            
        }

        private void CreatePlace(string[] palabras, StreamReader f)
        {


            bool spaceShip = false;

            places[int.Parse(palabras[1])].name = palabras[2];
            if (palabras[3] == "noSpaceShip") spaceShip = false;
            else spaceShip = true;

            places[int.Parse(palabras[1])].spaceShip = spaceShip;


            places[int.Parse(palabras[1])].description = ReadDescription(f);

            places[int.Parse(palabras[1])].connections = new int[4];

            places[int.Parse(palabras[1])].itemsInPlace = new Lista.Lista();

            if (places.Length - 1 == int.Parse(palabras[1]))
            {
                for (int i = 0; i < places.Length; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        places[i].connections[j] = -1;

                    }
                }
            }
        }

        private void CreateStreet(string[] palabras)
        {
            int place = int.Parse(palabras[3]);
            int placeToGo = int.Parse(palabras[6]);
            int index = 0;
            int indexReverse = 0;



            if (palabras[2] == "place")
            {
                switch (palabras[4])
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

        private void CreateItem(string[] palabras)
        {
            
            items[int.Parse(palabras[1])].name = palabras[2];
            items[int.Parse(palabras[1])].description = palabras[5];


            places[int.Parse(palabras[4])].itemsInPlace.insertaFin(int.Parse(palabras[1]));
        }

        private string ReadDescription(StreamReader f)
        {
            string description = null;

            string line = f.ReadLine();

            while(line != "")
            {
                description = description + line + '\n';
                line = f.ReadLine();

            }

            string []desc = description.Split('"');
            description = desc[1];
            
            return description;
        }

        public string GetPlaceInfo(int pl)
        {
            return places[pl].description;
        }

        public string GetMoves(int pl)
        {

            string moves = null;

            for (int i = 0; i < places[pl].connections.Length; i++)
            {
                if (places[pl].connections[i] != -1)
                {
                    string line = Dir2String(i) + ": " + places[pl].name + '\n';
                    moves = moves + line;
                }
            }

            return moves;

        }

        public int GetNumItems(int pl)
        {
            return places[pl].itemsInPlace.cuentaEltos();
        }

        public string GetItemsInfo(int it)
        {
            string info = it + ": " + items[it].name + " " + items[it].description;
            return info;
        }


        public string GetItemsPlace(int pl)
        {
            string info = null;

            for (int i = 0; i < places[pl].itemsInPlace.cuentaEltos(); i++)
            {
                string linea = places[pl].itemsInPlace.nEsimo(i) + ": " + items[places[pl].itemsInPlace.nEsimo(i)].name + " " + items[places[pl].itemsInPlace.nEsimo(i)].description + '\n';
                info = info + linea;
            }

            return info;

        }

        public void PickItemPlace(int pl, int it)
        {
            places[pl].itemsInPlace.borraElto(it);
        }

        public void DropItemPlace(int pl, int it)
        {
            places[pl].itemsInPlace.insertaNesimo(it, it);
        }

        public int Move(int pl, Direction dir)//Hacer excepcion de fuera de rango 
        {
            int lugarLlegada;

            int dirIndex = (int)dir;

            lugarLlegada = places[pl].connections[dirIndex];

            return lugarLlegada;

        }

        public bool isSpaceship(int pl)
        {
            return places[pl].spaceShip;
        }

        public void GuardaPartida(string usuario, int pos, string bag)
        {
            
            StreamWriter datosPartida = new StreamWriter(usuario);

            datosPartida.WriteLine(pos);
            datosPartida.WriteLine("Bag");
            if(bag != null)
            {
                string[] palBag = bag.Split(' ');
                int posicion = int.Parse(palBag[0][0].ToString());
                datosPartida.WriteLine(posicion);
            }
            
            datosPartida.WriteLine("Places");
            for (int i = 0; i < places.Length; i++)
            {
                for(int j = 0; j < places[i].itemsInPlace.cuentaEltos(); j++)
                {
                    int itemPos = places[i].itemsInPlace.nEsimo(j);
                    string placeInfo = i + " : " + itemPos;
                    datosPartida.WriteLine(placeInfo);
                }
            }
            datosPartida.Close();
        }

        public void CargaPartida(StreamReader f)
        {

            for(int i = 0; i < places.Length; i++)
            {
                places[i].itemsInPlace = new Lista.Lista();
            }
           

            while (f.EndOfStream == false)
            {
                string[] palabras = f.ReadLine().Split(' ');
                places[int.Parse(palabras[0])].itemsInPlace.insertaFin(int.Parse(palabras[2]));
            }

            f.Close();
        }

        private string Dir2String(int index)
        {
            string direction = null;
            Direction dir = (Direction)index;
            switch (dir)
            {
                case Direction.North:
                    direction = "north";
                    break;
                case Direction.South:
                    direction = "south";
                    break;
                case Direction.East:
                    direction = "east";
                    break;
                case Direction.West:
                    direction = "west";
                    break;
            }

            return direction;
        }



    }

}
