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
}
