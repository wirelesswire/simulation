using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public abstract class Animal : Organism, Reproducable
    {
        int REPRODUCTION_FOOD_LEVEL = 5;

        public int age = 0;
        public int hunger = 500;

        public float hungerperaction = 2;
        public float eatingEfficency = 0.5f;
        public float chanceForNextAction = 0.1f;
        public float chanceTOMultiply = 0.2f;



        public int sight = 10;
        public int actions = 1;
        public int actionsLeft = 1;
        protected List<Node> pathToFood;

        public List<Node> FindPath(bool[,] grid, int startX, int startY, int endX, int endY)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Sprawdzenie czy punkt końcowy jest na planszy i jest możliwy do osiągnięcia
            if (endX < 0 || endX >= rows || endY < 0 || endY >= cols )
            {
                //Console.Write("niemożliwy do  osiagniecia " + endX + " " + endY + "\n");
                return null;
            }
            if(!grid[endX, endY])
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
        public void resetActions()
        {
            actions = actionsLeft;
        }

        public abstract void Eat(Organism o);
        public abstract Act MoveSpecific(List<Organism> herbivores, int size, Board b);


        public Animal(int x, int y) : base(x, y) { }
        public Animal() : base() { pathToFood = null; }

        public bool CanReproduce(Board b )
        {
            if(!( helper.Next(10) < (chanceTOMultiply * 10)))
            {
                return false ;
            }

            if(possibleReproducableCells(b) == null) return false;

            return hunger > REPRODUCTION_FOOD_LEVEL ;
        }
        public List<int[]> possibleReproducableCells(Board board,bool asDelta = false )
        {
            List<int[]> emptyCells = new List<int[]>();
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;  // Skip center cell

                    int x = this.GetX() + dx;
                    int y = this.GetY() + dy;
                    if (x >= 0 && x < board.GetSize() && y >= 0 && y < board.GetSize() && board.IsEmpty(x, y))
                    {
                        if (asDelta) // zwraca różnicę 
                        {
                            emptyCells.Add(new int[] { dx, dy });
                        }
                        else// zwraca pole 
                        {
                            emptyCells.Add(new int[] { x, y });

                        }
                    }
                }
            }

            if (emptyCells.Count == 0)
            {
                // No available adjacent cells to reproduce into
                return null;
            }
            return emptyCells;
        }
        public List<int[]> possibleStepableCells(Board board, bool asDelta = false)
        {
            List<int[]> tmp = possibleReproducableCells(board, asDelta);
            if(tmp == null)
            {
                return null;
            }
            List<int[]> ret = new List<int[]>();
            foreach (var item in tmp)
            {
                if (!( Math.Abs( item[0]) == Math.Abs(item[1]) ) ){
                    ret.Add(item);
                }
            }
            if (ret.Count == 0)
            {
                // No available adjacent cells to reproduce into
                return null;
            }
            return ret;

        }

        public Organism Reproduce(Organism parent, Board board)
        {

            List<int[]> emptyCells = possibleReproducableCells(board);
            if (emptyCells.Count == 0)
            {
                throw new Exception("nie może być tu zero ");
            }


            // Randomly select an empty adjacent cell to reproduce into
            int[] newCell = emptyCells[helper.Next(emptyCells.Count)];

            // Create and return a new organism at the selected cell
            if (parent is Herbivore)
            {
                return new Herbivore(newCell[0], newCell[1]);
            }
            else if (parent is Carnivore)
            {
                return new Carnivore(newCell[0], newCell[1]);
            }
            else
            {
                throw new ArgumentException("Parent organism must be Herbivore or Carnivore.");
            }
        }

        protected Organism GetNearestFood(List<Organism> herbivores)
        {
            // Inicjujemy odległość i najbliższą roślinę jako null
            double minDistance = double.MaxValue;
            Organism nearestFood = null;

            // Dla każdej rośliny na planszy sprawdzamy odległość od mięsożercy
            foreach (Organism herbivore in herbivores)
            {
                double distance = distanceHelper.getDistance(x, y, herbivore.GetX(), herbivore.GetY());
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = herbivore;
                }
            }

            if (nearestFood != null)
            {
                if (distanceHelper.getSightDistance(x, y, nearestFood.GetX(), nearestFood.GetY()) > sight)
                {
                    nearestFood = null;
                }
            }

            return nearestFood;
        }

     
        protected Act moveRandomly(int size, Board b)
        {
            
            List<int[]> possibleMoves = possibleStepableCells(b,true);// tutaj dostaje tylko deltę 
            if(possibleMoves == null)
            {
                return new Act(0, 0, Act.actionTaken.nothing, 0);
            }
            int[] a = possibleMoves[helper.Next(possibleMoves.Count)];

            return new Act(a[0], a[1], actionsLeft);

        }

        public Act Move(List<Organism> herbivores, int size, Board b)
        {
            Act act = MoveSpecific(herbivores, size, b);
            if (x + act.getdX() >= 0 && x + act.getdX() < size && y + act.getdY() >= 0 && y + act.getdY() < size)
            {

            }
            else
            {
                if (! (x + act.getdX() >= 0))
                {
                    throw new Exception("x0  ");
                }
                if (! (x + act.getdX() < size))
                {
                    throw new Exception("xs  ");
                }
                if (! (y + act.getdY() >= 0))
                {
                    throw new Exception("y0  ");
                }
                if (! (y + act.getdY() < size))
                {
                    throw new Exception("ys  ");
                }

                throw new Exception("reszta czyli chyba nic   ");
            }



            actionsLeft--;
            return act;
        }




    }
}
