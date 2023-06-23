using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa pomocnicza zarządzająca pozycjami 
    /// </summary>
    public struct coords
    {
        public int x;
        public int y;
        public coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public coords(Node n)
        {
            this.x = n.x;
            this.y = n.y;
        }
        /// <summary>
        /// zwraca przemieszcenie z podanej pozycji do tej 
        /// </summary>
        /// <param name="x">pozycja x </param>
        /// <param name="y">pozycja y </param>
        /// <param name="forwards">kierunek upływu czasu</param>
        /// <returns>przemieszczenie z podanej pozycji do tej  </returns>
        public coords asDelta(int x, int y, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x, this.y - y);
            }
            return new coords(x - this.x, y - this.y);
        }
        /// <summary>
        /// zwraca przemieszcenie z podanej pozycji do tej 
        /// </summary>
        /// <param name="x">pozycja </param>
        /// <param name="forwards">kierunek upływu czasu</param>
        /// <returns>przemieszczenie z podanej pozycji do tej  </returns>
        public coords asDelta(coords x, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x.x, this.y - x.y);
            }
            return new coords(x.x - this.x, x.y - this.y);

        }
        /// <summary>
        /// zwraca przemieszcenie z podanej pozycji do tej 
        /// </summary>
        /// <param name="x">pozycja </param>
        /// <param name="forwards">kierunek upływu czasu</param>
        /// <returns>przemieszczenie z podanej pozycji do tej  </returns>
        public coords asDelta(int[] x, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x[0], this.y - x[1]);
            }
            return new coords(x[0] - this.x, x[1] - this.y);

        }
        /// <summary>
        /// suma dwóch pozycji stosowana do dodawamia pozycji i przemieszczenia 
        /// </summary>
        /// <param name="c">pozycja do dodania </param>
        /// <returns>suma pozycji </returns>
        public coords add(coords c)
        {
            return new coords(this.x+c.x,this.y + c.y);
        }
        /// <summary>
        /// sprawdza czy pozycje są takie same 
        /// </summary>
        /// <param name="c">pozycja do porównania</param>
        /// <returns>czy są takie same </returns>
        public bool XandYequal(coords c )
        {
            if(x == c.x && y== c.y)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// sprawdza czy pozycja znajduje się w obrzerzach mapy 
        /// </summary>
        /// <param name="boundX">brzeg x </param>
        /// <param name="boundY">brzeg y </param>
        /// <returns>zwraca czy się mieści </returns>
        public bool isInBounds (int boundX, int boundY)
        {
            if (x >= 0 && x  < boundX && y  >= 0 && y  < boundY)
            {
                return true;
            }
            return false;


        }
        /// <summary>
        /// sprawdza czy pozycja znajduje się w obrzerzach mapy kwadratowej 
        /// </summary>
        /// <param name="boundX">brzeg</param>
        /// <returns>zwraca czy się mieści </returns>
        public bool isInBounds(int boundX)
        {
            if (x >= 0 && x < boundX && y >= 0 && y < boundX)
            {
                return true;
            }
            return false;


        }
        /// <summary>
        /// sprawdza czy dodanie pozycji x spowoduje wykroczenie poza obrzerza mapy 
        /// </summary>
        /// <param name="boundX">brzeg</param>
        /// <returns>zwraca czy pozycja wynikowa się mieści </returns>
        public bool isInBounds(coords c, int boundX , int boundY)
        {
            return this.add(c).isInBounds(boundX, boundY) ;

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>czy wartość absolutna x jest równa wartości absolutnej y </returns>
        public bool absXeqY()
        {
            return Math.Abs(this.x) == Math.Abs(this.y);
        }
        /// <summary>
        /// liczy dystans na mapie (jako iż nie można chodzić po skosie to liczy inaczej niż po prostu dzieląca ich odległość)
        /// </summary>
        /// <param name="c">pozycja do porównania</param>
        /// <returns>dystans do przebycia</returns>
        public int  absDistanceOnBoard(coords c )
        {
            return  Math.Abs(x - c.x) + Math.Abs(y - c.y);
        }
        /// <summary>
        /// liczy dystans na mapie (używany do wzroku )
        /// </summary>
        /// <param name="c">pozycja do porównania</param>
        /// <returns>dystans </returns>
        public double  absDistance(coords c)
        {
            return Math.Sqrt(((x - c.x) * (x - c.x)) + ((y - c.y) * (y - c.y)));
        }

    }
}
