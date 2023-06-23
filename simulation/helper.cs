using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// rozszerzenia klas ułatwiające robienia operacji jednolinowo 
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// liczy wszystkie akcje 
        /// </summary>
        /// <param name="list">epoki</param>
        /// <returns>ilosć akcji </returns>
        public static  int CountAllActs(this List<epoch> list){
            int a = 0;
            foreach (var item in list)
            {
                foreach (var item1 in item.acts)
                {
                    a++;
                }
            }
            return a;
        }


        /// <summary>
        /// miesza listę tj sortuje losowo
        /// </summary>
        /// <typeparam name="T">typ listy </typeparam>
        /// <param name="list">lista do pomieszania</param>
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

        /// <summary>
        /// zwraca tylko żywe osobniki z podanej listy 
        /// </summary>
        /// <param name="list">lista osobników </param>
        /// <returns>żywe osobniki</returns>
        public static List<IDieable> OnlyAlive(this List<IDieable> list)
        {
            return list.FindAll((IDieable o) => { return !o.IsDead(); });


        }
    }
    /// <summary>
    /// klasa zbierająca pomocnicze funkje używane w różnych częściach aplikacji 
    /// </summary>
    public static class helper
    {
        /// <summary>
        /// globalny obiekt random umożliwia tworzenie statystyk użycia jego funkcji 
        /// </summary>
        public static Random r = new Random();
        /// <summary>
        /// ilość użyć funkcji "next"(każdego rodzaju ) 
        /// </summary>
        public static int nextCount = 0;
        /// <summary>
        /// ostatnio wyświetlona liczba użyć funkcji "next" pozwala policzyć ilość użyć na epokę
        /// </summary>
        public static int lastShown = 0;

        public static int additionaloutOfstats = 2;
       

        /// <summary>
        /// liczy średnie wartości statystyk przekazywanych genetycznie dla danej grupy 
        /// </summary>
        /// <param name="animals">grupa dla której średnia będzie liczona </param>
        /// <returns>średnie statystyki </returns>
        public static List<double> getAvgStats(List<Animal> animals )
        {
            List<double> avg = new List<double>();
            for (int i = 0; i < stats.ileJestWlasciwosci+additionaloutOfstats; i++)
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
                avg[stats.ileJestWlasciwosci] += item.hunger;
                avg[stats.ileJestWlasciwosci + 1] += item.actionsLeft;
            }
            for (int i = 0; i < avg.Count; i++) // liczy wartość średnią 
            {
                avg[i] = avg[i] / animals.Count;
            }

            return avg;




        }
        /// <summary>
        /// liczy średnie wartości ststystyk dla danej grupy  i zwraca jako sformatowany string 
        /// </summary>
        /// <param name="animals">grupa </param>
        /// <param name="nazwaGrupy">określenie typu np : mięsożercy  </param>
        /// <returns>sformatowany string </returns>
        /// <exception cref="Exception">gdyby liczba p[arametrów się zmieniła ale nie koniecznie na pewno i do końca (nie wszędzie ) to jest przypominajka </exception>
        public static string avgStatsAsString(List<Animal> animals, string nazwaGrupy)
        {
            List<double> avg = getAvgStats(animals);
            string[] nazwyCech = stats.getNazwyWlasciwosci();
            nazwyCech =  nazwyCech.Append("głód ").Append("ruchy").ToArray();
            //nazwyCech.Append("ruchy");
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>ilość użytych randomów </returns>
        public static int getNextCount()
        {
            lastShown = nextCount;
            return nextCount;
        }
        /// <summary>
        /// nakładka na random licząca użycia
        /// </summary>
        /// <param name="a">max-1 numer wylosowany</param>
        /// <returns>losowy numer [0:a)</returns>
        public static int Next(int a)
        {
            nextCount++;
            return r.Next(a);
        }
        /// <summary>
        /// nakładka na random licząca użycia
        /// </summary>
        /// <param name="a">min numer wylosowany</param>
        /// <param name="b">max-1 numer wylosowany</param>
        /// <returns>losowy numer [a:b)</returns>
        public static int Next(int a,int b )
        {
            nextCount++;
            return r.Next(a,b);
        }
        /// <summary>
        /// nakładka na random licząca użycia 
        /// </summary>
        /// <returns>losowa liczbe [0:1)</returns>
        public static double NextDouble()
        {
            nextCount++;
            return r.NextDouble();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt1"></param>
        /// <param name="prompt2"></param>
        /// <param name="nazwy"></param>
        /// <param name="przedzialy"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double[] getSomeValuesOfType(string prompt1,string prompt2 , string[] nazwy , int[][] przedzialy  = null  )
        {
            if(nazwy.Length != przedzialy.Length)
            {
                throw new Exception("nieprawidłowe dane ");
            }

            double[] values = new double[nazwy.Length];

            for (int i = 0; i < nazwy.Length; i++)
            {
                if(przedzialy != null)
                {
                    Console.WriteLine(prompt1 + nazwy[i] + prompt2 + "(  " + przedzialy[i][0] + " - " + przedzialy[i][1] + " ) ");

                }
                else
                {
                    Console.WriteLine(prompt1 + nazwy[i] + prompt2 );
                }
                double value = 0;
                while (true)
                {
                    if(przedzialy != null)
                    {
                        if (!double.TryParse(Console.ReadLine(), out double a))
                        {
                            Console.WriteLine("podana wartość jest niepoprawna ");

                        }
                        else
                        {
                            if (!(przedzialy[i][0] <= a && a <= przedzialy[i][1]) && przedzialy[i][0] != -1 && przedzialy[i][0] != -1) // [-1,-1] traktowany jako brak pzedziału 
                            {
                                Console.WriteLine("wartość nie mieści się w przedziale ");
                                continue;
                            }

                            value = a;
                            break;
                        }
                    }
                    else
                    {
                        if (!double.TryParse(Console.ReadLine(), out double a))
                        {
                            Console.WriteLine("podana wartość jest niepoprawna ");

                        }
                        else
                        {
                            if (!(przedzialy[i][0] <= a && a <= przedzialy[i][1])) //bez przedziału 
                            {
                                Console.WriteLine("wartość nie mieści się w przedziale ");
                                continue;
                            }

                            value = a;
                            break;
                        }
                    }

                   



                }
                Console.WriteLine("podana wartość jest poprawna i  została zapisana  ( " + nazwy[i] + " = " + value + ")");
                values[i] = value;

            }
            return values;  
        }


    }
    //public static class  distanceHelper{
    //    public static int  getDistance(int x, int y, int x2, int y2)
    //    {
    //        return Math.Abs(x - x2) + Math.Abs(y2 - y);
    //    }
    //    public static double getSightDistance(int x, int y, int x2, int y2)
    //    {
    //        return Math.Sqrt ( ((x - x2)*(x - x2) )+ ((y - y2)*(y - y2)));
    //    }
    //}


}
