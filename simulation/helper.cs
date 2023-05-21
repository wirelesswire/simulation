using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = helper.r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //public static List<Organism> OnlyAlive(this List<Organism> list)
        //{
        //    return list.FindAll((Organism o) =>  { return !o.IsDead(); }  );


        //}
        public static List<IDieable> OnlyAlive(this List<IDieable> list)
        {
            return list.FindAll((IDieable o) => { return !o.IsDead(); });


        }
    }
    public static class helper
    {
        public static Random r = new Random();
        public static int nextCount = 0;
        public static int lastShown = 0;

        public static List<double> getAvgStats(List<Animal> animals )
        {
            List<double> avg = new List<double>();
            for (int i = 0; i < stats.ileJestWlasciwosci; i++)
            {
                avg.Add(0);
            }

            foreach (var item in animals)//liczy sume tych wartości
            {
                List<double> a = item.stats.wartosci();
                for (int i = 0; i < stats.ileJestWlasciwosci; i++)
                {
                    avg[i]+= a[i];


                }
            }
            for (int i = 0; i < avg.Count; i++) // liczy wartość średnią 
            {
                avg[i] = avg[i] / animals.Count;
            }

            return avg;




        }
        public static string avgStatsAsString(List<Animal> animals, string nazwaGrupy)
        {
            List<double> avg = getAvgStats(animals);
            string[] nazwyCech = stats.getNazwyWlasciwosci();
            if(avg.Count != nazwyCech.Length)
            {
                throw new Exception("nie zgadzają się ilości");
            }
            string ret = "";
            ret += nazwaGrupy + " : \n";
            for (int i = 0; i < avg.Count; i++)
            {
                ret += nazwyCech[i] +" : "  +  avg[i] +"\n";


            }
            return ret;


        }

        public static int getNextCount()
        {
            lastShown = nextCount;
            return nextCount;
        }
        public static int Next(int a)
        {
            nextCount++;
            return r.Next(a);
        }
        public static int Next(int a,int b )
        {
            nextCount++;
            return r.Next(a,b);
        }
        public static double NextDouble()
        {
            nextCount++;
            return r.NextDouble();
        }
    }
    public static class  distanceHelper{
        public static int  getDistance(int x, int y, int x2, int y2)
        {
            return Math.Abs(x - x2) + Math.Abs(y2 - y);
        }
        public static double getSightDistance(int x, int y, int x2, int y2)
        {
            return Math.Sqrt ( ((x - x2)*(x - x2) )+ ((y - y2)*(y - y2)));
        }
    }


}
