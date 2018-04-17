using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista
{

    class Lista
    {
        private class Nodo
        {
            public int dato;
            public Nodo sig;
        }
        Nodo pri;
        public Lista()
        {
            pri = null;
        }

        public void insertaIni(int e)
        {
            Nodo n = new Nodo();

            n.dato = e;
            n.sig = pri;
            pri = n;
        }
        public void verLista()
        {
            int i = 0;

            Nodo aux = pri;
            while (aux != null)
            {

                Console.WriteLine("Nodo " + i + " --> Dato " + aux.dato);
                aux = aux.sig;
                i++;
            }

        }
        public void insertaFin(int e)
        {
            Nodo aux = pri;
            Nodo n = new Nodo();
            n.dato = e;

            if (aux != null)
            {
                while (aux.sig != null)
                {
                    aux = aux.sig;
                }
                aux.sig = n;
                n.sig = null;
            }
            else
            {
                pri = n;
                n.sig = null;
            }

        }
        private Nodo buscaNodo(int e)
        {
            Nodo aux = pri;
            Nodo ret;
            while (aux != null && aux.dato != e)
            {
                aux = aux.sig;
            }
            if (aux != null) ret = aux;
            else ret = null;

            return ret;
        }
        public bool buscaElto(int e)
        {
            bool Found = false;
            if (buscaNodo(e) != null) Found = true;
            else Found = false;

            return Found;
        }
        public int suma()
        {
            int sumaTotal = 0;
            Nodo aux = pri;
            while (aux != null)
            {
                sumaTotal = sumaTotal + aux.dato;
                aux = aux.sig;
            }
            return sumaTotal;
        }
        public int cuentaEltos()
        {
            Nodo aux = pri;
            int cont = 0;
            while (aux != null)
            {
                cont++;
                aux = aux.sig;
            }

            return cont;
        }
        public int cuentaOcurrencias(int e)
        {
            Nodo aux = pri;
            int ocurrencias = 0;
            while (aux != null)
            {
                if (aux.dato == e)
                {
                    ocurrencias++;
                }

                aux = aux.sig;
            }

            return ocurrencias;
        }
        private Nodo nEsimoNodo(int e)
        {
            Nodo aux = pri;
            int cont = 0;
            Nodo ret = null;
            while (aux != null && cont != e)
            {
                cont++;
                aux = aux.sig;
            }
            if (aux != null) ret = aux;
            else ret = null;

            return ret;
        }

        public int nEsimo(int e)
        {
            int retInt = 0;
            if (nEsimoNodo(e) == null)
            {
                retInt = -1;

            }
            else
            {
                retInt = nEsimoNodo(e).dato;
            }
            return retInt;

        }
        public void insertaNesimo(int n, int e)
        {
            Nodo m = new Nodo();
            if (n > 0)
            {
                Nodo aux = nEsimoNodo(n);
                Nodo anteriorNodo = nEsimoNodo(n - 1);
                anteriorNodo.sig = m;
                m.sig = aux;
                m.dato = e;
            }
            else
            {
                insertaIni(e);
            }
        }
        public void borraElto(int e)
        {
            Nodo n = new Nodo();
            n = buscaNodo(e);
            Nodo aux = pri; //Anterior Nodo
            if (n != null)
            {
                if (aux.dato == n.dato)
                {
                    pri = aux.sig;
                }
                else
                {
                    while (aux != null && aux.sig.dato != e)
                    {
                        aux = aux.sig;
                    }
                    aux.sig = n.sig;
                    Console.WriteLine("Elemento borrado satisfactoriamente.");

                }
            }
            else
            {
                Console.WriteLine("ERROR. No existe el nodo con dato " + e);
            }

        }
        public void borraTodos(int e)
        {
            int i;
            Nodo aux;
            Nodo anteriorNodo;
            int max = cuentaEltos();
            for (i = 0; i < cuentaEltos(); i++)
            {
                aux = nEsimoNodo(i);
                if (i > 0)
                {

                    anteriorNodo = nEsimoNodo(i - 1);
                    if (aux.dato == e)
                    {
                        anteriorNodo.sig = aux.sig;
                    }
                }
                else
                {
                    pri = aux.sig;
                }
            }
        }
        public void borraNesimo(int n)
        {
            Nodo m = nEsimoNodo(n);
            Nodo anteriorNodo;
            if (m != null)
            {
                if (n > 0)
                {
                    anteriorNodo = nEsimoNodo(n - 1);
                    anteriorNodo.sig = m.sig;

                }
                else
                {
                    pri = m.sig;
                }
            }
            else
            {
                Console.WriteLine("ERROR. Nodo no encontrado.");
            }
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            Lista Lista = new Lista();
            int Input = 0;

            do
            {
                Input = muestraMenu();
                switch (Input)
                {
                    case 1:
                        Console.Write("Dato a introducir : ");
                        Lista.insertaIni(int.Parse(Console.ReadLine()));
                        break;
                    case 2:
                        Lista.verLista();
                        break;
                    case 3:
                        Console.Write("Dato a introducir : ");
                        Lista.insertaFin(int.Parse(Console.ReadLine()));
                        break;
                    case 4:
                        Console.Write("Dato a introducir : ");
                        if (Lista.buscaElto(int.Parse(Console.ReadLine())) == true)
                        {
                            Console.WriteLine("Existe un nodo con el dato pedido.");
                        }
                        else Console.WriteLine("No existe un nodo en toda la lista con ese dato.");
                        break;
                    case 5:
                        Console.WriteLine("La suma de los datos de cada nodo es : " + Lista.suma());
                        break;
                    case 6:
                        Console.WriteLine("El numero de nodos que hay en la Lista : " + Lista.cuentaEltos());
                        break;
                    case 7:
                        Console.WriteLine("Dato a introducir : ");
                        Console.WriteLine("Ocurrencias del dato en la Lista : " + Lista.cuentaOcurrencias(int.Parse(Console.ReadLine())));
                        break;
                    case 8:
                        Console.WriteLine("Nodo a buscar : ");
                        int x = int.Parse(Console.ReadLine());
                        if ((Lista.nEsimo(x)) == -1)
                        {
                            Console.WriteLine("ERROR. No existe el nodo.");
                        }
                        else
                        {
                            Console.WriteLine("Nodo " + x + " con dato " + Lista.nEsimo(x));
                        }

                        break;
                    case 9:
                        Console.WriteLine("Dato a introducir : ");
                        int i = int.Parse(Console.ReadLine());
                        Console.WriteLine("Posición : ");
                        int j = int.Parse(Console.ReadLine());
                        Lista.insertaNesimo(j, i);
                        break;
                    case 10:
                        Console.WriteLine("Dato a introducir : ");
                        Lista.borraElto(int.Parse(Console.ReadLine()));
                        break;
                    case 11:
                        Console.WriteLine("Dato a introducir");
                        Lista.borraTodos(int.Parse(Console.ReadLine()));
                        break;
                    case 12:
                        Console.WriteLine("Posición del nodo a borrar : ");
                        Lista.borraNesimo(int.Parse(Console.ReadLine()));
                        break;
                    


                }
            } while (Input != 0);
        }
        public static int muestraMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Lista");
            Console.WriteLine();
            Console.WriteLine("1. Añade nodo al principio");
            Console.WriteLine("2. Muestra Lista");
            Console.WriteLine("3. Añade nodo al final");
            Console.WriteLine("4. Buscar nodo");
            Console.WriteLine("5. Suma total de los datos");
            Console.WriteLine("6. Número de nodos de la Lista");
            Console.WriteLine("7. Ocurrencias del dato especificado");
            Console.WriteLine("8. Busca un nodo (EMPIEZA EN 0 LA LISTA)");
            Console.WriteLine("9. Inserta un nuevo Nodo en la posición deseada");
            Console.WriteLine("10. Borra el primer elemento e");
            Console.WriteLine("11. Borra todos los elementos e");
            Console.WriteLine("12. Borra un nodo en una posición");
            Console.WriteLine("13. Invierte la lista");
            Console.WriteLine("0. Salir");
            int i = int.Parse(Console.ReadLine());
            Console.Clear();
            return i;
        }


    }
}
