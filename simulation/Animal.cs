using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa przedstawiająca zwierzę które może się poruszać po mapie 
    /// </summary>
    public abstract class Animal : Organism, Reproducable
    {

        int REPRODUCTION_FOOD_LEVEL = 5;

        //public double maxAge = 100;
        //public double hungerperaction = 2;
        //public double eatingEfficency = 0.5f;
        //public double chanceForNextAction = 0.1f;
        //public double chanceTOMultiply = 0.2f;

        //public double sight = 10;
        //public double actions = 1;



        public double hunger = 500;
        public double actionsLeft = 1;


        public stats stats;


        protected List<Node> pathToFood;


        /// <summary>
        /// znajduje najkrótszą możliwą drogę do celu 
        /// </summary>
        /// <param name="grid">mapa</param>
        /// <param name="start">początek </param>
        /// <param name="end">cel</param>
        /// <returns>zwraca drogę do celu</returns>
        public List<Node> FindPath(bool[,] grid, coords start, coords end)
        {

            int startX = start.x;
            int startY = start.y;
            int endX = end.x;
            int endY = end.y;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Sprawdzenie czy punkt końcowy jest na planszy i jest możliwy do osiągnięcia
            if (endX < 0 || endX >= rows || endY < 0 || endY >= cols)
            {
                //Console.Write("niemożliwy do  osiagniecia " + endX + " " + endY + "\n");
                return null;
            }
            if (!grid[endX, endY])
            {
                Console.Write("niemożliwy do  stąpniecia  " + endX + " " + endY + "\n");
                return null;
            }

            Queue<Node> queue = new Queue<Node>();
            bool[,] visited = new bool[rows, cols];
            Node startNode = new Node(startX, startY);
            visited[startX, startY] = true;
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();

                // Jeśli dotarliśmy do punktu końcowego to zwracamy ścieżkę
                if (currentNode.x == endX && currentNode.y == endY)
                {
                    List<Node> path = new List<Node>();
                    while (currentNode != null)
                    {
                        path.Insert(0, currentNode);
                        currentNode = currentNode.parent;
                    }
                    return path;
                }

                // Dodajemy sąsiednie wierzchołki do kolejki
                int[] dx = { 0, 0, 1, -1 };
                int[] dy = { 1, -1, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    int nextX = currentNode.x + dx[i];
                    int nextY = currentNode.y + dy[i];
                    if (nextX >= 0 && nextX < rows && nextY >= 0 && nextY < cols && grid[nextX, nextY] && !visited[nextX, nextY])
                    {
                        visited[nextX, nextY] = true;
                        Node nextNode = new Node(nextX, nextY);
                        nextNode.parent = currentNode;
                        queue.Enqueue(nextNode);
                    }
                }
            }

            // Nie znaleziono ścieżki
            return null;
        }
        /// <summary>
        /// ustawia statystyki na podstqawie statystyk rodzica 
        /// </summary>
        /// <param name="a">statystyki rodzica </param>
        public void setStats(stats a)
        {
            this.stats = a;
        }
        /// <summary>
        /// przywraca maksymalną liczbę akcji 
        /// </summary>
        public void resetActions()
        {
            actionsLeft = stats.actionsPerturn;
            //actions = actionsLeft;
           
        }

        /// <summary>
        /// sprawdza czy zwierze jest w stanie zjeść inny organizm na mapie
        /// </summary>
        /// <param name="o">organizm do potencjalnego zjedzenia</param>
        /// <returns>zwraca czy może zjeść dany organizm </returns>
        public abstract bool doIEatIt(Organism o);
        /// <summary>
        /// sprawdza czy zwierze jest w stanie zjeść inny obiekt na mapie
        /// </summary>
        /// <param name="o">obiekt do potencjalnego zjedzenia </param>
        /// <returns>zwraca czy może zjeść dany obiekt </returns>
        public abstract bool doIEatIt(ObjectOnMap o);


        /// <summary>
        ///
        /// </summary>
        /// <returns> zwraca wartość odżywczą osobnika </returns>
        public override double getNutritionalValue()
        {
            return nutritiousness;
        }
        /// <summary>
        /// odpowiada za przetworzenie zjadania innego orgainzmu w czasie 
        /// </summary>
        /// <param name="o">organizm króry został zjedzony</param>
        /// <param name="forwards">kierunek czau </param>
        public virtual void Eat(Organism o,bool forwards )
        {
            if (forwards)
            {
                this.hunger += o.getNutritionalValue() * stats.eatingEfficency;
                o.Die(forwards);
                if (this.hunger > nutritiousness)
                {
                    nutritiousness = (int)this.hunger;
                }
            }
            else
            {
                double amount = o.getNutritionalValue() * stats.eatingEfficency;
                if (this.hunger > nutritiousness)
                {
                    nutritiousness -= (int)amount;
                }
                this.hunger -= amount;
                o.Die(forwards);

            }
        }
        /// <summary>
        /// abstrakcyjna metoda pozwalająca każdej podklasie mieć osobną logikę pooruszania sie 
        /// </summary>
        /// <param name="herbivores">obiekty które potencjalnie może zjeść </param>
        /// <returns></returns>
        public abstract Act MoveSpecific(List<Organism> herbivores);




        public Animal(int x, int y, Board b, stats s) : base(x, y, b) { this.stats = s; applyStats(); }
        public Animal() : base() { pathToFood = null; stats = new stats(); applyStats(); }
        public Animal(coords c, Board b, stats s) : base(c, b) { this.stats = s; pathToFood = null; applyStats(); }
        /// <summary>
        /// ustawia statystyki aby współpracowały z klasą stats
        /// </summary>      
        public void applyStats()
        {
            if (stats == null)
            {
                throw new Exception("nie może być tu null ");
            }
           

            this.hunger = stats.startingHunger;//1
            //this.hungerperaction = stats.hungerperaction;//2
            //this.eatingEfficency = stats.eatingEfficency;//3
            //this.chanceForNextAction = stats.chanceForNextAction;//4

            //this.actions = stats.actionsPerturn;//5

            //this.chanceTOMultiply = stats.chanceTOMultiply;//6
            //this.stats.sight = stats.sight;//7
            //this.maxAge = stats.maxAge;//8

            this.nutritiousness = (int)this.hunger;

        }
        /// <summary>
        /// zwraca wartość odżywczą w formie inta 
        /// </summary>
        /// <returns></returns>
        public int getNutritiousness()
        {
            return this.nutritiousness; 
        }
        /// <summary>
        /// zmienia zwierzę w ciało 
        /// </summary>
        /// <returns>zwierzę jako ciało </returns>
        public Corpse toCorpse()
        {
            return new Corpse(this);
        }
        /// <summary>
        /// robi akcję umierania zwierzęcia 
        /// </summary>
        /// <returns>akcję umierania zwierzęcia </returns>
        public Act Corpseify()
        {
            return new Die(this,toCorpse(),this.coords,this.coords);
        }
        /// <summary>
        /// sprawdza czy może się rozmnożyć 
        /// </summary>
        /// <returns>czy może się rozmnożyć</returns>
        public bool CanReproduce()
        {
            if (!(helper.NextDouble() < stats.chanceTOMultiply ))
            {
                return false;
            }

            if (possibleReproducableCells() == null) return false;

            return hunger > REPRODUCTION_FOOD_LEVEL;
        }

        /// <summary>
        /// zwraca potencjalne  koordynaty  w prostokącie w obszarze  x na y  z każdej strony     
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <returns> bez tych które nie  miszczą sie w ramach planszy  </returns>

       
        public List<coords> cellsAroundRectangle(coords range)
        {
            List<coords> cellsAround = new List<coords>();
            for (int dx = -range.x; dx <= range.x; dx++)
            {
                for (int dy = -range.y; dy <= range.y; dy++)
                {
                    if (dx == 0 && dy == 0) continue;  // pomija pole na którym stoi 

                    coords tmp = coords.add(new coords(dx, dy));

                    if (tmp.isInBounds(board.GetSize(), board.GetSize()))// czy nie wychodzi poza mapę 
                    {
                        cellsAround.Add(tmp);

                    }
                }


            }
            if (cellsAround.Count == 0) return null;
            return cellsAround;

        }
        /// <summary>
        /// zwraca puste pola w prostokącie 
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <returns> bez tych które są zapełnione   </returns>
        public List<coords> emptyCellsAroundRectangle(coords range)
        {
            List<coords> a = cellsAroundRectangle(range);
            List<coords> ret = new List<coords>();
            if (a == null) return null;
            for (int i = 0; i < a.Count; i++)
            {
                if (board.IsEmpty(a[i]))
                {
                    ret.Add(a[i]);
                }
            }

            if (ret.Count == 0) return null;

            return ret;
        }

        /// <summary>
        /// zwraca pola na które może zająć potomstwo 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="asDelta"></param>
        /// <returns> bez tych które się nie mieszczą , tych które są zapełnione i tych dalszych niż 1 na 1 w kazdą strone (dopuszcza ukosy )</returns>

        public List<coords> possibleReproducableCells()
        {
            List<coords> emptyCells = emptyCellsAroundRectangle(new coords(1, 1));
            if (emptyCells == null) return null;
            if (emptyCells.Count == 0) return null;
            return emptyCells;
        }

        /// <summary>
        ///  zwraca wszystkie komórki obok na których jest coś co mogę zjeść 
        /// </summary>
        /// <returns></returns>
        public List<coords> possibleEdibleCells()
        {
            List<coords> a = cellsAroundRectangle(new coords(1, 1));
            List<coords> ret = new List<coords>();

            if (a == null) return null;
            for (int i = 0; i < a.Count; i++)
            {
                if (doIEatIt(board.GetObjectOnMap(a[i])) && !(a[i].absXeqY()))
                { // jeżeli nie jem wyrzucam 
                    ret.Add(a[i]);
                }
            }
            if (ret.Count == 0) return null;
            return a;
        }
        /// <summary>
        /// zwraca pola na które możesz  sie ruszyć // jeżeli jest puste  to możesz się tam  poruszyć ale bez chodzenia po ukosie  
        /// </summary>
        /// <param name="board"></param>
        /// <param name="asDelta"></param>
        /// <returns>bez tych które się nie mieszczą , tych które są zapełnione czymś na czego nie mogę zjeść  lub 
        /// przeszkodą naturalną i tych dalszych niż 1 na 1 w kazdą strone (nie dopuszcza ukosów )</returns>
        public List<coords> possibleStepableCells()
        {
            List<coords> a = new List<coords>();

            if (emptyCellsAroundRectangle(new coords(1, 1)) != null)
            {
                a.AddRange(emptyCellsAroundRectangle(new coords(1, 1)));
            }
            if (possibleEdibleCells() != null)
            {
                foreach (var item in possibleEdibleCells())
                {
                    if (!a.Contains(item))
                    {
                        a.Add(item);
                    }
                }
            }

            List<coords> ret = new List<coords>();
            foreach (var item in a)
            {
                if (!(item.asDelta(coords).absXeqY()))// nie jest ukos
                {

                    ret.Add(item);
                }
            }
            if (ret.Count == 0) return null;
            return ret;

        }

        /// <summary>
        /// odpowiada za rozmnożenie się zwierzęcia 
        /// </summary>
        /// <param name="childFutureCoords">pozycja potomstwa </param>
        /// <returns>statystki potomstwa</returns>
        /// <exception cref="Exception">błąd przy sprawdzaniu </exception>
        public stats Reproduce(out coords childFutureCoords)
        {

            List<coords> emptyCells = possibleReproducableCells();
            if (emptyCells.Count == 0)
            {
                throw new Exception("nie może być tu zero ");
            }


            // Randomly select an empty adjacent cell to reproduce into
            coords newCell = emptyCells[helper.Next(emptyCells.Count)];


            childFutureCoords = newCell;
            this.hunger -= 100;//PAYMENT
            return this.stats.getRandomForChild();


        }
        /// <summary>
        /// znajduje najbliże pożywienie 
        /// </summary>
        /// <param name="organisms">wszystkie możliwe do zjedzenia organizmy </param>
        /// <returns>najbliższe pożywienie </returns>

        protected Organism GetNearestFood(List<Organism> organisms)
        {
            // Inicjujemy odległość i najbliższą roślinę jako null
            double minDistance = double.MaxValue;
            Organism nearestFood = null;

            // Dla każdej rośliny na planszy sprawdzamy odległość od mięsożercy
            foreach (Organism herbivore in organisms)
            {
                double distance = this.coords.absDistanceOnBoard(herbivore.coords);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = herbivore;
                }
            }

            if (nearestFood != null)
            {
                if (coords.absDistance(nearestFood.coords) > stats.sight)
                {
                    nearestFood = null;
                }
            }

            return nearestFood;
        }
        /// <summary>
        ///  dodaje efekty upływu czasu do zwierzęcia  zgodnie z kierunkiem jego upływu -> odnawia wszystkie możliwe do wykonania akcje na następną iterację
        /// </summary>
        /// <param name="forward"></param>
        public override void epochPass(bool forward)
        {

            base.epochPass(forward);

            resetActions();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>czy może się jescze poruszyć </returns>
        public bool canMoveMore()
        {
            //return true;
            bool val = true;
            if (actionsLeft >= 1)
            {
             val=   true;
            }
            else if(actionsLeft <1 && actionsLeft > 0)
            {
                val =  helper.NextDouble() < actionsLeft;
            }
            else if (actionsLeft <= 0)
            {
                val = false;
                
            }

            if(val == false) // gdy nie masz już więcej normalnych ruchów  masz szanse na kolejny 
            {
                if (helper.NextDouble() < stats.chanceForNextAction)
                {
                    val = true;
                }
            } 

            return val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>akcję poruszania się losowo po mapie </returns>
        /// <exception cref="Exception"></exception>
        protected Act moveRandomly()
        {

            //List<coords> possibleMoves = possibleStepableCells();// tutaj dostaje tylko deltę 
            List<coords> possibleMoves = emptyCellsAroundRectangle(new coords(1, 1));// tutaj dostaje tylko deltę 

            if (possibleMoves == null)
            {
                throw new Exception("tu nie wchodzi  nie powinno przynajmniej ");
                return new DraxStanding(this, new coords(0, 0)); // nic nie robisz  i nigdy nie powinno tu wejść 
            }
            coords a = possibleMoves[helper.Next(possibleMoves.Count)];

            return new Move(this, coords, a);//normalny ruch 

        }
        /// <summary>
        /// pobiera cenę należną za poruszenie się  
        /// </summary>
        /// <param name="forward">kierunek upływu czasu </param>
        public void MoveCost(bool forward)
        {
            if (forward)
            {

                actionsLeft--;///PAYMENT
                hunger -= stats.hungerperaction;///PAYMENT

            }
            else
            {
                actionsLeft++;
                hunger += stats.hungerperaction;
            }
        }
        /// <summary>
        /// odpowiada za całościowe poruszanie się organizmu 
        /// </summary>
        /// <param name="orgs">możliwe do zjedzenia organizmy </param>
        /// <returns>akcję wykonaną podczas ruchu </returns>
        /// <exception cref="Exception"></exception>
        public Act Move(List<Organism> orgs)
        {
            if (hunger <= 0 || age > stats.maxAge)
            {
                return Corpseify();
            }
            Act act = MoveSpecific(orgs);
            if (!act.to.isInBounds(board.GetSize()))
            {
                throw new Exception("wyszło poza ");
            }
            else if ((!board.IsEmpty(act.to) && act is Move))
            {
                throw new Exception("problem z pustostanem ");
            }
            else
            {

            }

            act.setMoves(canMoveMore());

            return act;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>reprezentację zwierzęcia jako string </returns>
        public override  string presentation()
        {
            return base.presentation() + " zwierzę : ";
        }


    }
}
