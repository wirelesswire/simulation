using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    /// <summary>
    /// klasa obiektu który znajduje się na mapie 
    /// </summary>
    public abstract class ObjectOnMap
    {
        public coords coords;
        protected Board board ;


        /// <summary>
        /// efekt upływu czasu na obiekcie 
        /// </summary>
        /// <param name="forward">kierunek upływu czasu</param>
        public abstract void epochPass(bool forward);
        public ObjectOnMap(int x, int y,Board b )
        {

            this.coords = new coords(x,y);
            this.board  = b;
        }
        public ObjectOnMap(coords a , Board b)
        {

            this.coords = a;
            this.board = b;
        }

        public ObjectOnMap()
        {

        }
        /// <summary>
        /// ustawia mapę na której obiekt się znajduje 
        /// </summary>
        /// <param name="b">mapa </param>
        public void SetBoard(Board b )
        {
            board = b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>mapę na której się znajduje </returns>
        public Board getBoard()
        {
            return board;
        }
        /// <summary>
        /// ustawia pozycję x i y obiektu 
        /// </summary>
        /// <param name="x">nowa pozycja x obiektu </param>
        /// <param name="y">nowa pozycja y obiektu </param>
        public void SetXY(int x , int y )
        {
            //if (coords == null) { // setT
            //    coords = new coords(x, y);
            //    return; 
            //}

            coords.x = x;
            coords.y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>pozycję x obiektu </returns>
        public int GetX()
        {
            return coords.x;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>pozycję y obiektu </returns>
        public int GetY()
        {
            return coords.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>reprezentację obiektu w formie literowej do konsoli </returns>
        public virtual string toString()
        {
            return " ";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>reprezentację obiektu w formie stringa </returns>

        public virtual string presentation()
        {
            return "obiekt :";
        }

    }


  

}
