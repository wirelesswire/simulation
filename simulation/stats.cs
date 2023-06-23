using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa zajmująca się statystkykami organizmu żyjącego w symulacji 
    /// </summary>
    public class stats
    {
        public double startingHunger {get;set;}= 500;         //1 0-20
        public double hungerperaction {get;set;}= 10;         //2 0.5-inf
        public double eatingEfficency {get;set;}= 0.7f;       //3  0-1
        public double chanceForNextAction {get;set;}= 0.2f;   //4 0-1
        public double actionsPerturn {get;set;}= 2;           //5 1-inf możesz się ruszyć 2 razy na ture i póżniej masz 10 % szans za każdym razem na kolejne , jeżwli to jest np 1.9 to jest 1 ruch i 90% na kolejny(ale tylko raz ) i póżniej 10 % tak długo jak się ruszasz (teoretycznie może być nieskończone )
        public double chanceTOMultiply {get;set;}= 0.1f;      //6 0.001 -1 
        public double sight {get;set;}= 5;                    //7 1.5  - inf 
        public double maxAge {get;set;}= 100;                 //8 1-inf
        public double reproductionCost { get; set; } = 100;       //9 1-inf

        //public double currentHunger;//PAYMENTS
        //public double currentAge;

        public int mutationMultiplier = 1;          //9 1-inf


        public static int ileJestWlasciwosci = 10;
        /// <summary>
        /// 
        /// </summary>
        /// <returns>nazwy właściwości do pokazania użytkownikowi</returns>
        public  static string[] getNazwyWlasciwosci()
        {
            return new string[] { "głód", "koszt głodu na akcję " , "efektywność jedzenia", "szansa na następną akcję " , "akcje na turę ", "szansa na rozmnożenie","wzrok", "maksymalny wiek ","koszt rozmnożenia", "współczynnik mutacji "};
        }
        /// <summary>
        /// tworzy nowy obiekt na podstawie pytania użytkownika o cechy 
        /// </summary>
        /// <returns>obiekt stats </returns>
        /// <exception cref="Exception">błąd wprowadzania</exception>
        public static stats statsFactory()
        {
            double[] doubles =  helper.getSomeValuesOfType("podaj wartość odpowiadającą właściwości  ", "w statystykach wybranego osobnika " ,getNazwyWlasciwosci() );
            if(doubles.Length  != ileJestWlasciwosci)
            {
                throw new Exception("błąd przy wprowadzaniu danych ");
            }

            return new stats(doubles[0], doubles[1], doubles[2], doubles[3], doubles[4], doubles[5], doubles[6], doubles[7], doubles[8], (int )doubles[9]);
        }

        /// <summary>
        /// zwraca wartości w formie listy 
        /// </summary>
        /// <returns></returns>
        public List<double> wartosci()
        {

            return new List<double>() { startingHunger, hungerperaction, eatingEfficency, chanceForNextAction, actionsPerturn, chanceTOMultiply, sight, maxAge,reproductionCost  ,mutationMultiplier };
        }
        public stats(double hunger, double costOfAction, double efficiency, double chanceFornextAction,double actionsperturn ,  double chanceToMultiply,double sight,double maxAge,double reproductionCost , int mutationMultiplier)
        {

            this.startingHunger = hunger;
            this.hungerperaction=costOfAction;
            this.eatingEfficency = efficiency;
            this.actionsPerturn = actionsperturn;
            this.chanceForNextAction=chanceFornextAction;
            this.chanceTOMultiply = chanceToMultiply;
            this.sight = sight;
            this.maxAge = maxAge;
            this.reproductionCost = reproductionCost;   
            this.mutationMultiplier = mutationMultiplier;
            makeSureStatsAreInBounds();
        }
        private stats(List<double> wartosci, List<double> waga, List<int> mnoznik, List<int> znak)
        {
            startingHunger = wartosci[0] + waga[0] * mnoznik[0] * znak[0]*mutationMultiplier;
            hungerperaction = wartosci[1] + waga[1] * mnoznik[1] * znak[1]*mutationMultiplier;
            eatingEfficency = wartosci[2] + waga[2] * mnoznik[2] * znak[2];
            chanceForNextAction = wartosci[3] + waga[3] * mnoznik[3] * znak[3]*mutationMultiplier;
            actionsPerturn = wartosci[4] + waga[4] * mnoznik[4] * znak[4] * mutationMultiplier;
            chanceTOMultiply = wartosci[5] + waga[5] * mnoznik[5] * znak[5]*mutationMultiplier;
            sight = wartosci[6] + waga[6] * mnoznik[6] * znak[6]*mutationMultiplier;
            maxAge = wartosci[7] + waga[7] * mnoznik[7] * znak[7] * mutationMultiplier;
            mutationMultiplier = (int)wartosci[8] + 1; // +1  // znak[8] * 1; // +- 1 na każdą ewolucję 
            makeSureStatsAreInBounds();
        }
        public stats()
        {
            //zostawia wartości początkowe 
        }
        //public static stats statsFactory()
        //{
            
        //}
        /// <summary>
        /// upewnia się że wartości są w odpowiednich dla siebie przedziałach 
        /// </summary>
        public void makeSureStatsAreInBounds()
        {
            if(startingHunger < 20){ startingHunger = 20; }

            if(hungerperaction < 0.5) {hungerperaction = 0.5; }

            if (eatingEfficency >1) { eatingEfficency = 1;}
            if(eatingEfficency < 0) {  eatingEfficency = 0;}

            if (chanceForNextAction <0) { chanceForNextAction = 0; }
            if (chanceForNextAction>1) { chanceForNextAction = 1; }

            if(actionsPerturn <1 ) { actionsPerturn = 1;}

            if(chanceTOMultiply > 1) {  chanceTOMultiply = 1;}
            if(chanceTOMultiply < 0.001) { chanceTOMultiply = 0.001; }

            if(sight < 1.5) { sight = 1.5; }
            if (maxAge < 1) { maxAge = 1; }

            if(reproductionCost < 1) {  reproductionCost = 1;}
            
            if (mutationMultiplier<1) { mutationMultiplier = 1;}



        }


        /// <summary>
        /// tworzy nowe statysktyki dla potomstwa bazując na statykach rodzica 
        /// </summary>
        /// <returns>statystki dla dziecka </returns>
        /// <exception cref="Exception">błąd ilości wartości </exception>
        public stats getRandomForChild()
        {
            if (ileJestWlasciwosci != wartosci().Count)
            {
                throw new Exception();
            }
            List<double> a = new List<double> { };
            List<int> b = new List<int>() { };
            List<int> signs = new List<int>();

            for (int i = 1; i <= ileJestWlasciwosci; i++)//<1-x> włącznie 
            {
                a.Add(Math.Pow(10, - ((i%3)+1)));
                b.Add(i);
                signs.Add(Math.Sign(helper.Next(-1, 2)));
            }



            a.Shuffle();
            b.Shuffle();
            signs.Shuffle();
            return new stats(wartosci(), a, b, signs);



        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>statystyki w formie stringa </returns>
        public override string ToString()
        {
            string a = "";
            string[] b = getNazwyWlasciwosci();
            double[] c = wartosci().ToArray();

            for (int i = 0; i < b.Length; i++)
            {
                a += b[i] + " : " + c[i] +"\n";
            }

            return a;


        }




    }
}
