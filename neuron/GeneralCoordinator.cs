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
        public const int reportAndCleanDnaEveryThisManyGenerations = 100;
        public const int stepsInEveryCreaturesLife = 40;
        public const int numOfInputs = 4;//
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
        public const int testsPerRun = 2;
        public const bool doSigmoid = false;
        //end of settings
        static Random rndm = new Random();
        public static creature evolution(int generationsToEvolveIfThatIsHowYouSpellIt)// LEMMIE BE CLEAR. SOMNG HERE IS MESSED UP BAD.
        {
            genome emptyDNA = new genome(3);
            emptyDNA.addGenes(lenghtOfOriginalDNA);
            emptyDNA.genes[0].parts[0] = Coordinator.numOfInputs;
            int avregNumOfConnectionsPerNeuron = (lenghtOfOriginalDNA - Coordinator.numOfInputs - 1) / Coordinator.numOfInputs;
            for (int thisNeuron = 1; thisNeuron <= emptyDNA.genes[0].parts[0]; ++thisNeuron)
            {
                emptyDNA.genes[thisNeuron].parts[genome.bufferStart] = (avregNumOfConnectionsPerNeuron * (thisNeuron - 1)) + Coordinator.numOfInputs + 1;
            }
            //
            creature LUCA = new creature(emptyDNA);
            while (!LUCA.alive())
            {
                LUCA = new creature(genome.mutate(LUCA.DNA));
            }
            creature BEST = new creature(LUCA.DNA);
            creature lastBest = new creature(LUCA.DNA);
            Generation serviceGeneration = new Generation(BEST, 1);
            //
            Console.WriteLine("starting evo");
            for (int generatioNumber = 0; generatioNumber < generationsToEvolveIfThatIsHowYouSpellIt; ++generatioNumber)
            {
                Console.WriteLine("generetion " + generatioNumber);
                serviceGeneration = new Generation(BEST, numOfCreatursInEachGeneration);
                lastBest = new creature(GdolHador(serviceGeneration));//prob
                if (generatioNumber % reportAndCleanDnaEveryThisManyGenerations == 0)
                {
                    Console.Write("\n\twe are currently in generetion " + generatioNumber + ". ");
                    BEST.bug.print();
                    Console.WriteLine(". genome length: " + BEST.DNA.genes.Count + " genes");
                    BEST.clean();
                    Console.WriteLine("\tafter some cleaning, the new genome length is " + BEST.DNA.genes.Count + " genes\n");
                }
                if (BEST.Scoer < lastBest.Scoer)
                {
                    if (alawaysUseTheBestEverCreatureForTheNextGeneration)
                    {
                        BEST = new creature(lastBest);
                    }
                    Console.WriteLine("generetion [" + generatioNumber + "] got a new high score: " + BEST.Scoer + ". [X: " + BEST.bug.x + ", Y: " + BEST.bug.y + "]. stars: " + (initialFireStrength - BEST.fireStrength) + ". genome length: " + BEST.DNA.genes.Count + " genes");
                    //BEST.run(true);
                    if (BEST.Scoer > 100 - minimumDistanceToExtinguishFire)
                    {
                        //alawaysUseTheBestEverCreatureForTheNextGeneration = false;
                    }
                    if (BEST.Scoer > 100)
                    {
                        Console.WriteLine("a very good creture evolved. here are some runs of this creatures: ");
                        BEST.run(true, 10);
                        Console.WriteLine("cleaning genome and repeting life run");
                        Console.WriteLine("BEST gnm cleaning:\ndirty:");
                        BEST.DNA.print();
                        Console.WriteLine("dirty brain analsis: ");
                        BEST.brian.networkDiagnos();
                        Console.WriteLine("clean:");
                        BEST.clean();
                        BEST.DNA.print();
                        Console.WriteLine("clean brain analsis: ");
                        BEST.brian.networkDiagnos();
                        Console.WriteLine("here are some runs of the clean creature:");
                        BEST.run(true, 10);
                        punishWaterWaste = true;
                        while (true) ;
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
            Console.WriteLine("\treporting and clean DNA every " + reportAndCleanDnaEveryThisManyGenerations +" generations");
            Console.WriteLine("\tsteps in every creaturs life: " + stepsInEveryCreaturesLife);
            Console.WriteLine("\tlife cycles of avery creature: " + testsPerRun);
            Console.WriteLine("\t inputs: " + numOfInputs + ", outputs: " + numOfOutputs);
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
            serviceDNA.addGenes(1);//prob this crt should die. well we dont care? I guess.
            creature serviceCreature = new creature(serviceDNA);

            //Console.WriteLine("making a new generation based on creture who got this score: " + father.Scoer);
            //father.DNA.print();

            for (int thisCreature = 0; thisCreature < numOfCreaturesToMake; ++thisCreature)
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
    class msho
    {
        public int nam;
        public string text;
        public msho(int num, string txt)
        {
            nam = num;
            text = txt;
        }
    }
    
    
}
