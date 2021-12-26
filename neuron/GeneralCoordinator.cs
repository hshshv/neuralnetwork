using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    static class Coordinator
    {
        //settings
        static int lenghtOfOriginalDNA = 10;
        static int numOfCreatursInEachGeneration = 100;
        static int stepsInEveryCreaturesLife = 30;
        //end of settings

        public static creature evolution(int generationsToEvolveIfThatIsHowYouSpellIt)
        {
            genome emptyDNA = new genome(3);
            emptyDNA.addGenes(lenghtOfOriginalDNA);
            
            Console.WriteLine("making LUCA");
            creature LUCA = new creature(emptyDNA);
            while(!LUCA.alive())
            {
                //Console.WriteLine("LUCA attempt failed");
                LUCA = new creature(genome.mutate(LUCA.DNA));
            }
            Console.WriteLine("made LUCA. here is the DNA: ");
            LUCA.DNA.print();

            creature BEST = new creature(LUCA.DNA);
            //Console.Write("made BEST. here is the DNA: ");
            BEST.DNA.print();

            creature lastBest = new creature(LUCA.DNA);
            Generation serviceGeneration = new Generation(BEST, 1);
            Console.WriteLine("starting evo");
            for(int generatioNumber = 0; generatioNumber < generationsToEvolveIfThatIsHowYouSpellIt; ++generatioNumber)
            {
                serviceGeneration = new Generation(BEST, numOfCreatursInEachGeneration);
                BEST = GdolHador(serviceGeneration);
                
                Console.WriteLine();
                Console.WriteLine("*RUN*");
                Console.WriteLine();

                Console.WriteLine("this best: " + BEST.Scoer + ", last best: " + lastBest.Scoer);
                if(BEST.Scoer <= lastBest.Scoer)
                {
                    Console.WriteLine("This generetions best creature didn't beat the previus generetions best score. changing to last best Krich");
                    BEST.TransformInto(lastBest);

                }
                else
                {
                    Console.WriteLine("new high score. updated the best");
                    lastBest.TransformInto(BEST);
                }
                Console.WriteLine("best score of last generation is: " + BEST.Scoer);
                Console.WriteLine("X: " + BEST.parameters[0]);
                Console.WriteLine("Y: " + BEST.parameters[1]);
                //Console.WriteLine("best creature genome: ");
                //BEST.DNA.print();

            }

            return (BEST);
        }
        static creature GdolHador(Generation Hador)
        {
            int biggestScoerCreture = 0;
            for(int thisCreature = 0; thisCreature < Hador.population.Count; ++thisCreature)
            {
                RunCreature(Hador.population[thisCreature]);
                if(Hador.population[thisCreature].Scoer > Hador.population[biggestScoerCreture].Scoer)
                {
                    biggestScoerCreture = thisCreature;
                }
            }
            return (Hador.population[biggestScoerCreture]);
        }
        static void RunCreature(creature krich)
        {
            double goalXLOcation = 15;
            double goalYLocation = 15;

            for(int step = 0; step < stepsInEveryCreaturesLife; ++step)
            {
                krich.brian.clear();

                krich.brian.neurons[0].value = krich.parameters[0];// X\Y location
                krich.brian.neurons[1].value = krich.parameters[1];// the other location

                krich.brian.Fire();

                ++krich.parameters[krich.brian.strongestOutput()];
            }

            krich.Scoer = 100 - Math.Sqrt(Math.Pow(goalXLOcation - krich.parameters[0], 2.0) + Math.Pow(goalYLocation - krich.parameters[1], 2.0));

        }
    }
    
    class Generation
    {
        public List<creature> population = new List<creature>();
        public Generation(creature father, int numOfCreaturesToMake)
        {
            genome serviceDNA = new genome(3);
            serviceDNA.addGenes(1);
            creature serviceCreature = new creature(serviceDNA);
            
            Console.WriteLine("making a new generation based on creture who got this score: " + father.Scoer);
            //father.DNA.print();

            for(int thisCreature = 0; thisCreature < numOfCreaturesToMake; ++thisCreature)
            {
                //Console.WriteLine("making creature number " + thisCreature);
                serviceCreature = new creature(genome.mutate(father.DNA));
                //Console.WriteLine("made number " + thisCreature);

                while (!serviceCreature.alive())
                {
                    serviceCreature = new creature(genome.mutate(father.DNA));
                    //Console.WriteLine("GG: creature died of extream mutation");
                }
                population.Add(serviceCreature);
                //Console.WriteLine("GG: new creature was born");
            }

            Console.WriteLine("A new generation has been made");
        }
    }
}
