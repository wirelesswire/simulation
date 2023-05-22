using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class stats
    {
        public double startingHunger = 500;//1
        public double hungerperaction = 10;//2
        public double eatingEfficency = 0.5f;//3
        public double chanceForNextAction = 0.1f;//4
        public double actionsPerturn = 2; //5 możesz się ruszyć 2 razy na ture i póżniej masz 10 % szans za każdym razem na kolejne , jeżwli to jest np 1.9 to jest 1 ruch i 90% na kolejny(ale tylko raz ) i póżniej 10 % tak długo jak się ruszasz (teoretycznie może być nieskończone )
        public double chanceTOMultiply = 0.1f;//6
        public double sight = 5;//7
        public int mutationMultiplier = 1;//8


        public static int ileJestWlasciwosci = 8;
        public  static string[] getNazwyWlasciwosci()
        {
            return new string[] { "głód", "koszt głodu na akcję " , "efektywność jedzenia", "szansa na następną akcję " , "akcje na turę ", "szansa na rozmnożenie","wzrok","współczynnik mutacji "};
        }
        public List<double> wartosci()
        {

            return new List<double>() { startingHunger, hungerperaction, eatingEfficency, chanceForNextAction, actionsPerturn, chanceTOMultiply, sight, mutationMultiplier };
        }
        public stats(double hunger, double costOfAction, double efficiency, double chanceFornextAction,double actionsperturn ,  double chanceToMultiply,double sight,int mutationMultiplier)
        {

            this.startingHunger = hunger;
            this.hungerperaction=costOfAction;
            this.eatingEfficency = efficiency;
            this.actionsPerturn = actionsperturn;
            this.chanceForNextAction=chanceFornextAction;
            this.chanceTOMultiply = chanceToMultiply;
            this.sight = sight;
            this.mutationMultiplier = mutationMultiplier;
            makeSureStatsAreInBounds();
        }
        private stats(List<double> wartosci, List<double> waga, List<int> mnożnik, List<int> znak)
        {
            startingHunger = wartosci[0] + waga[0] * mnożnik[0] * znak[0]*mutationMultiplier;
            hungerperaction = wartosci[1] + waga[1] * mnożnik[1] * znak[1]*mutationMultiplier;
            eatingEfficency = wartosci[2] + waga[2] * mnożnik[2] * znak[2];
            chanceForNextAction = wartosci[3] + waga[3] * mnożnik[3] * znak[3]*mutationMultiplier;
            actionsPerturn = wartosci[4] + waga[4] * mnożnik[4] * znak[4] * mutationMultiplier;
            chanceTOMultiply = wartosci[5] + waga[5] * mnożnik[5] * znak[5]*mutationMultiplier;
            sight = wartosci[6] + waga[6] * mnożnik[6] * znak[6]*mutationMultiplier;
            mutationMultiplier =(int) wartosci[7] +   znak[7] * 1; // +- 1 na każdą ewolucję 
            makeSureStatsAreInBounds();
        }
        public stats()
        {
            //zostawia wartości początkowe 
        }
        public void makeSureStatsAreInBounds()
        {
            if(startingHunger < 20){ startingHunger = 20; }
            if(hungerperaction < 0.5) {hungerperaction = 0.5; }
            if (eatingEfficency >1) { eatingEfficency = 1;}
            if (chanceForNextAction <0) { chanceForNextAction = 0; }

            if (chanceForNextAction>1) { chanceForNextAction = 1; }
            if(actionsPerturn <1 ) { actionsPerturn = 1;}
            if(chanceTOMultiply > 1) {  chanceTOMultiply = 1;}
            if(chanceTOMultiply < 0.001) { chanceTOMultiply = 0.001; }
            if(sight < 1.5) { sight = 1.5; }
            if(mutationMultiplier<1) { mutationMultiplier = 1;}


        }

        

        public stats getRandomForChild()
        {
            List<double> a = new List<double> { };
            List<int> b = new List<int>() {  };
            List<int> signs = new List<int>();

            for (int i = 1; i <= ileJestWlasciwosci; i++)//<1-x> włącznie 
            {
                a.Add(Math.Pow(10,-i));
                b.Add(i);            
                signs.Add(Math.Sign(helper.Next(-1, 1)));
            }



            a.Shuffle();
            b.Shuffle();
            signs.Shuffle();
            return new stats(wartosci(), a, b, signs);



        }




    }
}
