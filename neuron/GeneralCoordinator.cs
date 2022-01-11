using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    static class Coordinator
    {
        //settings
        public const int lenghtOfOriginalDNA = 15;
        public const int numOfCreatursInEachGeneration = 300;
        public const int stepsInEveryCreaturesLife = 40;
        public const int numOfIntputs = 4;//
        public const int numOfOutputs = 4;
        public const int numOfParameters = 0;
        public static int fireX = 20;
        public static int fireY = 20;
        public const int minimumDistanceToExtinguishFire = 5;
        public const int initialFireStrength = 5;
        public const int chanceOfMutationForEachPart = 20;
        public const int chanceOfCreatingANewGene = 200;
        public const int chanceOfDeletingAGene = 210;
        public static bool alawaysUseTheBestEverCreatureForTheNextGeneration = true;
        public static bool punishWaterWaste = false;
        public const int testsPerRun = 3;
        //end of settings
        static Random rndm = new Random();
        public static creature evolution(int generationsToEvolveIfThatIsHowYouSpellIt)
        {
            genome emptyDNA = new genome(3);
            emptyDNA.addGenes(lenghtOfOriginalDNA);

            creature LUCA = new creature(emptyDNA);
            while (!LUCA.alive())
            {
                LUCA = new creature(genome.mutate(LUCA.DNA));
            }
            creature BEST = new creature(LUCA.DNA);
            creature lastBest = new creature(LUCA.DNA);
            Generation serviceGeneration = new Generation(BEST, 1);
            Console.WriteLine("starting evo");
            for (int generatioNumber = 0; generatioNumber < generationsToEvolveIfThatIsHowYouSpellIt; ++generatioNumber)
            {
                serviceGeneration = new Generation(BEST, numOfCreatursInEachGeneration);
                BEST = GdolHador(serviceGeneration);
                if (generatioNumber % 500 == 0)
                {
                    Console.Write("\twe are currently in generetion " + generatioNumber + ". ");
                    BEST.bug.print();
                    Console.WriteLine(". genome length: " + BEST.DNA.genes.Count + " genes");
                }
                if (BEST.Scoer <= lastBest.Scoer)
                {
                    if(alawaysUseTheBestEverCreatureForTheNextGeneration)
                    {
                        BEST.TransformInto(lastBest);
                    }
                }
                else
                {
                    lastBest.TransformInto(BEST);
                    Console.WriteLine("generetion [" + generatioNumber + "] got a new high score: " + BEST.Scoer + ". [X: " + BEST.bug.x + ", Y: " + BEST.bug.y + "]. stars: " + (initialFireStrength - BEST.fireStrength) + ". genome length: " + BEST.DNA.genes.Count + " genes");
                    //BEST.run(true);
                    if(BEST.Scoer > 100 - minimumDistanceToExtinguishFire)
                    {
                        //alawaysUseTheBestEverCreatureForTheNextGeneration = false;
                    }
                    if (initialFireStrength - BEST.fireStrength >= 3)
                    {
                        while (true)
                        {
                            BEST.brian.networkDiagnos();
                            BEST.run(true);
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                        }
                    }
                }
            }

            Console.WriteLine("end of evo. best creature was this: ");
            lastBest.DNA.print();
            lastBest.run(true);
            return (BEST);
        }
        static creature GdolHador(Generation Hador)
        {
            int biggestScoerCreture = 0;
            for (int thisCreature = 0; thisCreature < Hador.population.Count; ++thisCreature)
            {
                Hador.population[thisCreature].run();
                if (Hador.population[thisCreature].Scoer > Hador.population[biggestScoerCreture].Scoer)
                {
                    biggestScoerCreture = thisCreature;
                }
            }
            return (Hador.population[biggestScoerCreture]);
        }
        public static void printEvoSetting()
        {
            Console.WriteLine("evo setting:");
            Console.WriteLine("\tlegnth of original dna: " + lenghtOfOriginalDNA);
            Console.WriteLine("\tnum of creature in each generation: " + numOfCreatursInEachGeneration);
            Console.WriteLine("\tsteps in every creaturs life: " + stepsInEveryCreaturesLife);
            Console.WriteLine("\tlife cycles of avery creature: " + testsPerRun);
            Console.WriteLine("\t inputs: " + numOfIntputs + ", outputs: " + numOfOutputs);
            Console.WriteLine("\tnum of creature parameters: " + numOfParameters);
            Console.WriteLine("\tinitial fire location: X=" + fireX + ", y=" + fireY);
            Console.WriteLine("\tinitial fire strength: " + initialFireStrength);
            Console.WriteLine("\tminimum distance to extinguish fire: " + minimumDistanceToExtinguishFire);
            Console.WriteLine("\tchance of a mutetion: 1/" + chanceOfMutationForEachPart + ", chance for a new gene: 1/" + chanceOfCreatingANewGene + ", chance of deletinig a gene: 1/" + chanceOfDeletingAGene);
            Console.WriteLine("\talways evolve the highest scoring creature of all times: " + alawaysUseTheBestEverCreatureForTheNextGeneration);
            Console.WriteLine("\tpunish water wast: " + punishWaterWaste);
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
            
            //Console.WriteLine("making a new generation based on creture who got this score: " + father.Scoer);
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

            //Console.WriteLine("A new generation has been made");
        }
    }
}
