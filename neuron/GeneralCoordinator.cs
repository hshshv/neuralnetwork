using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    static class Coordinator
    {
        //settings
        static int lenghtOfOriginalDNA = 15;
        static int numOfCreatursInEachGeneration = 250;
        static int stepsInEveryCreaturesLife = 40;
        public const int numOfIntputs = 3;
        public const int numOfOutputs = 4;
        public const int numOfParameters = 10;
        static int fireX = 20;
        static int fireY = 20;
        static int minimumDistanceToExtinguishFire = 5;
        public static int initialFireStrength = 5;
        public const int chanceOfMutationForEachPart = 25;

        //end of settings
        static Random rndm = new Random();

        public static creature evolution(int generationsToEvolveIfThatIsHowYouSpellIt)
        {
            genome emptyDNA = new genome(3);
            emptyDNA.addGenes(lenghtOfOriginalDNA);
            
            creature LUCA = new creature(emptyDNA);
            while(!LUCA.alive())
            {
                LUCA = new creature(genome.mutate(LUCA.DNA));
            }
            creature BEST = new creature(LUCA.DNA);
            creature lastBest = new creature(LUCA.DNA);
            Generation serviceGeneration = new Generation(BEST, 1);
            Console.WriteLine("starting evo");
            for(int generatioNumber = 0; generatioNumber < generationsToEvolveIfThatIsHowYouSpellIt; ++generatioNumber)
            {
                serviceGeneration = new Generation(BEST, numOfCreatursInEachGeneration);
                BEST = GdolHador(serviceGeneration);
                if (generatioNumber % 500 == 0)
                {
                    Console.WriteLine("\twe are currently in generetion " + generatioNumber);
                    //Console.WriteLine("\tgeneretion [" + generatioNumber + "] got this score: " + BEST.Scoer + ". [X: " + BEST.bug.x + ", Y: " + BEST.bug.y + "]. fire strength: " + BEST.fireStrength);
                    //Console.WriteLine("\t(The distance from this location to the fire is: " + BEST.bug.distanceFrom(fireX, fireY));
                }
                if (BEST.Scoer <= lastBest.Scoer)
                {
                    //BEST.TransformInto(lastBest);
                }
                else
                {
                    lastBest.TransformInto(BEST);
                    Console.WriteLine("generetion [" + generatioNumber + "] got a new high score: " + BEST.Scoer + ". [X: " + BEST.bug.x + ", Y: " + BEST.bug.y + "]. stars: " + (5 - BEST.fireStrength));
                    //BEST.brian.networkDiagnos();
                    //RunCreature(BEST, true);
                    //Console.WriteLine("press any key to continue");
                    //Console.ReadKey();
                }
            }

            Console.WriteLine("end of evo. best creature was this: ");
            lastBest.DNA.print();
            RunCreature(lastBest, true);
            return (BEST);
        }
        static creature GdolHador(Generation Hador)
        {
            int biggestScoerCreture = 0;
            for (int thisCreature = 0; thisCreature < Hador.population.Count; ++thisCreature)
            {
                if("this is" == "not neccery") { 
                /*
                for (int step = 0; step < stepsInEveryCreaturesLife; ++step)
                {
                    Hador.population[thisCreature].brian.clear();

                    Hador.population[thisCreature].brian.neurons[0].setValue(/*Hador.population[thisCreature].bug.direction +  0.1 * (180 * Math.Atan((fireY - Hador.population[thisCreature].bug.y)/(fireX - Hador.population[thisCreature].bug.x))  / Math.PI)); // fire from ritght
                    Hador.population[thisCreature].brian.neurons[1].setValue(0.1 * (180 - Hador.population[thisCreature].brian.neurons[0].value())); // fire form left
                    Hador.population[thisCreature].brian.neurons[2].setValue(Hador.population[thisCreature].bug.distanceFrom(fireX, fireY));/*1 / (krich.bug.distanceFrom(fireX, fireY) + 0.0001); // heat sensor
                    /*
                    krich.brian.clear();
                krich.brian.neurons[0].setValue(90 - krich.bug.direction + (180 * Math.Atan((fireY - krich.bug.y)/(fireX - krich.bug.x))  / Math.PI)); // fire from ritght
                krich.brian.neurons[1].setValue(180 - krich.brian.neurons[0].value()); // fire form left
                krich.brian.neurons[2].setValue(krich.bug.distanceFrom(fireX, fireY));/*1 / (krich.bug.distanceFrom(fireX, fireY) + 0.0001); // heat sensor
                
                    /*
                    Hador.population[thisCreature].brian.neurons[0].value = Hador.population[thisCreature].bug.x;
                    Hador.population[thisCreature].brian.neurons[1].value = Hador.population[thisCreature].bug.y;
                    
                    Hador.population[thisCreature].brian.Fire();
                    switch (Hador.population[thisCreature].brian.strongestOutput())
                    {
                        case 0: Hador.population[thisCreature].bug.turn(1 /*10 * krich.brian.outputLayer[0].value); break;
                        case 1: Hador.population[thisCreature].bug.turn(-1 /*-10 * krich.brian.outputLayer[1].value); break;
                        case 2: Hador.population[thisCreature].bug.step(1/*10 * krich.brian.outputLayer[2].value); break;
                        case 3: if (Hador.population[thisCreature].bug.distanceFrom(fireX, fireY) < minimumDistanceToExtinguishFire) { --Hador.population[thisCreature].fireStrength; } break;

                    }
                }
                 */
            }
                Hador.population[thisCreature].Scoer = RunCreature(Hador.population[thisCreature]);

                ///////
                if (Hador.population[thisCreature].Scoer > Hador.population[biggestScoerCreture].Scoer)
                {
                    biggestScoerCreture = thisCreature;
                }
            }
            return (Hador.population[biggestScoerCreture]);
        }
        static double RunCreature(creature krich)
        {
            return(RunCreature(krich, false));
        }
        static double RunCreature(creature krich, bool lifereport)//???how work no ref \(O_O)/ OK sudden stop work \ or not
        {
            switch(rndm.Next(0, 4))//jini ES!
            {
                case 0: fireX = 20; fireY = 20; break;
                case 1: fireX = -20; fireY = 20; break;
                case 2: fireX = 20; fireY = -20; break;
                case 3: fireX = -20; fireY = -20; break;
            }

            krich.bug.reset();
            for (int step = 0; step < stepsInEveryCreaturesLife; ++step)
            {
                krich.brian.clear();
                krich.brian.neurons[0].setValue(90 - krich.bug.direction + (180 * Math.Atan((fireY - krich.bug.y)/(fireX - krich.bug.x))  / Math.PI)); // fire from ritght
                krich.brian.neurons[1].setValue(180 - krich.brian.neurons[0].value()); // fire form left
                krich.brian.neurons[2].setValue(krich.bug.distanceFrom(fireX, fireY));/*1 / (krich.bug.distanceFrom(fireX, fireY) + 0.0001); // heat sensor
                /*
                krich.brian.neurons[0].setValue(krich.bug.x);
                krich.brian.neurons[1].setValue(krich.bug.y);
                */
                krich.brian.Fire();
                
                //this multy action thing is gooood
                if (krich.brian.outputLayer[0].isOn()) { krich.bug.turn(10); }
                if (krich.brian.outputLayer[1].isOn()) { krich.bug.turn(-10); }
                if (krich.brian.outputLayer[2].isOn()) { krich.bug.step(1); }
                if (krich.brian.outputLayer[3].isOn()) 
                { 
                    if (krich.bug.distanceFrom(fireX, fireY) < minimumDistanceToExtinguishFire) 
                    { 
                        --krich.fireStrength; 
                    } 
                    else
                    {
                        ++krich.fireStrength;// creature gets punished for wasting water
                    }
                }

                
                if(lifereport)/*/**/
                {
                    Console.Write("step " + step + ": ");
                    krich.bug.print();
                    Console.Write(". I just ");
                    switch (krich.brian.strongestOutput())
                    {
                        case 0: Console.WriteLine("turned lefto"); break;
                        case 1: Console.WriteLine("turned righto"); break;
                        case 2: Console.WriteLine("stepped forwardo"); break;
                        case 3: Console.WriteLine("attempted to extingwish the fire o"); break;
                    }
                }
            }
            return(scoer(krich)); 

            // + 50 - 10 * krich.fireStrength;
            //krich.Scoer = 100 - Math.Sqrt(Math.Pow(goalXLOcation - krich.parameters[0], 2.0) + Math.Pow(goalYLocation - krich.parameters[1], 2.0));
            //krich.Scoer = 0 - krich.parameters[0];
            //krich.Scoer = (100 - Math.Abs((krich.parameters[0] - krich.parameters[1]))) * krich.parameters[0];

        }
        static double scoer(creature creatureToScoer)
        {
            return (100 - creatureToScoer.bug.distanceFrom(fireX, fireY) + 10 * (initialFireStrength - creatureToScoer.fireStrength));
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
