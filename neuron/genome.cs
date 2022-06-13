using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace neuronprog
{
    class gene
    {
        public int[] parts;//you should make a general class, not for the spesific case of 3 part in every gene
        public gene(int partsInThisGene)
        {
            parts = new int[partsInThisGene];
            for (int i = 0; i < parts.Length; ++i)
            {
                parts[i] = 100;//100 is the dflt value for a part in the gene
            }
        }
        public gene(int[] prts)
        {
            parts = prts;
        }
        public void print()
        {
            Console.Write("{");
            for (int i = 0; i < parts.Length; ++i)
            {
                Console.Write(parts[i]);
                if (i != parts.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write("}");
        }
        public void transformInto(gene newOne)
        {
            if (newOne.parts.Length != parts.Length)
            {
                return;
            }
            for (int thisPart = 0; thisPart < parts.Length; ++thisPart)
            {
                parts[thisPart] = newOne.parts[thisPart];
            }
        }
    }
    class genome
    {
        public List<gene> genes = new List<gene>();
        public int numberOfPartInEveryGene;
       
        static Random rndm = new Random();
        public const int destenetion = 0;
        public const int multyplayer = 1;
        public const int externalOrInternal = 2;//בודק קשר האם חיצונית שכבה מוביל אל ה
        public const int bufferStart = 0;
        public const int neuronType = 1;
        public const int staticNeuronValue = 2;
        public const int internalNeuron = 0;
        public const int externalNueron = 1;

        private static int thingInCheck = 3;
        private static int thingThatEffectsOutput = 2;
        private static int thingThatDoesNotEffectsOutput = 1;
        
        public genome(int partPerGene)
        {
            numberOfPartInEveryGene = partPerGene;
        }
        public genome(genome srcgenome)
        {
            numberOfPartInEveryGene = srcgenome.numberOfPartInEveryGene;
            genes = srcgenome.genes;
        }
        public void addGene()
        {
            genes.Add(new gene(numberOfPartInEveryGene));
        }
        public void addGene(gene geneToAdd)
        {
            addGene();
            genes[genes.Count - 1].transformInto(geneToAdd);
        }
        public void addGenes(int genesToAdd)
        {
            for (int i = 0; i < genesToAdd; ++i)
            {
                addGene();
            }
        }
        public void print()
        {
            for (int i = 0; i < genes.Count; ++i)
            {
                Console.Write("[" + i + "] ");
                genes[i].print();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void transformInto(genome newGnm)
        {
            numberOfPartInEveryGene = newGnm.numberOfPartInEveryGene;
            genes = new List<gene>();
            for(int thisGene = 0; thisGene < newGnm.genes.Count; ++thisGene)
            {
                addGene(newGnm.genes[thisGene]);
            }
        }
        public void mutate()
        {
            genes = mutate(this).genes;
        }
        public void clean()
        {
            transformInto(cleanVersionOf(this));
        }
        public static genome mutate(genome genomeToMutat) //this is used to make a mutated copy of a specific genome
        {
            //genomeToMutat.print();
            for (int thisGene = 0; thisGene < genomeToMutat.genes.Count; ++thisGene)
            {
                for (int thisPartOfTheGene = 0; thisPartOfTheGene < genomeToMutat.genes[thisGene].parts.Length; ++thisPartOfTheGene)
                {
                    if (rndm.Next(0, Coordinator.chanceOfMutationForEachPart) == 0)
                    {
                        genomeToMutat.genes[thisGene].parts[thisPartOfTheGene] = singelMutetedValue(genomeToMutat.genes[thisGene].parts[thisPartOfTheGene]);
                    }
                }
            }
            if (rndm.Next(0, Coordinator.chanceOfCreatingANewGene) == 0)//adding a new gene at a randome location
            {
                int addNewGeneAt = rndm.Next(0, genomeToMutat.genes.Count);
                genomeToMutat.genes.Insert(addNewGeneAt, new gene(genomeToMutat.numberOfPartInEveryGene));
            }
            if (rndm.Next(0, Coordinator.chanceOfDeletingAGene) == 0 && genomeToMutat.genes.Count > 1)//removing an existing gene at a randome location
            {
                genomeToMutat.genes.RemoveAt(rndm.Next(0, genomeToMutat.genes.Count));
            }
            //genomeToMutat.print();

            return (genomeToMutat);
        }
        static int singelMutetedValue(int valueBefor)
        {
            if (valueBefor == 0)
            {
                valueBefor = 1;
            }
            double tempValueHplder = valueBefor * (rndm.NextDouble() * 2);
            int valueAfter = Convert.ToInt32(tempValueHplder);
            return (valueAfter);
        }
        public static genome cleanVersionOf(genome dirty)
        {
            if (!creature.thisGenomeIsLegit(dirty)) 
            {
                return dirty;
            }
            bool[] geneEffectedByInput = new bool[dirty.genes.Count];
            int[] geneEffectsOutput = new int[dirty.genes.Count];
            
            for (int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)//checks roots
            {
                if(dirty.genes[thisNeuronLocation].parts[neuronType] == neuron.Static || dirty.genes[thisNeuronLocation].parts[neuronType] == neuron.Input)
                {
                    fireThisNeuron(dirty, geneEffectedByInput, thisNeuronLocation);
                }
            }
            for (int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)//checks branches
            {
                geneEffectsOutput[thisNeuronLocation] = thisNeuroneffectsTheOutput(geneEffectsOutput, thisNeuronLocation);
                /*
                firstConnection = dirty.firstConnectionOfNeuron(thisNeuronLocation - 1);
                lastConnection = dirty.lastConnectionOfNeuron(thisNeuronLocation - 1);
                for (int thisConnectionLocation = firstConnection; thisConnectionLocation < lastConnection; ++thisConnectionLocation)
                {
                    geneEffectsOutput[thisConnectionLocation] = thisNeuroneffectsTheOutput(ref dirty, ref geneEffectsOutput, dirty.genes[thisConnectionLocation].parts[destenetion] + 1);//!! can't give a connection to this function which checks neurons
                }*/
            }

            //this one might be unneccery
            for (int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)//checks branches
            {
                if(geneEffectsOutput[thisNeuronLocation] == thingInCheck)
                {
                    geneEffectsOutput[thisNeuronLocation] = thingThatDoesNotEffectsOutput;
                }
            }

            /* IM HERE */ /*attract error text;*/
            /*if(false)
            {
                Console.WriteLine("///thing. these are the neurons:");
                for(int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)
                {
                    Console.Write("[" + thisNeuronLocation + "]: ");
                    if (geneEffectedByInput[thisNeuronLocation])
                        Console.Write("effected by input, ");
                    else
                        Console.Write("isnt effected by input, ");
                    if(geneEffectsOutput[thisNeuronLocation] == thingThatEffectsOutput)
                        Console.Write("effects the output");
                    else
                        Console.Write("does not effect the output");
                    Console.WriteLine();
                }
                Console.WriteLine("////endthing");
            }*/

            genome clean = new genome(dirty.genes[0].parts.Length);
            clean.addGene(dirty.genes[0]);
            List<int> thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber = new List<int>();
            int neuronsDeletedSoFar = 0;
            for (int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)//copys importent neurons
            {
                if ((geneEffectedByInput[thisNeuronLocation] == true && geneEffectsOutput[thisNeuronLocation] == thingThatEffectsOutput) || dirty.genes[thisNeuronLocation].parts[neuronType] == neuron.Input/*so we wont delet unused inputs*/)
                {
                    clean.addGene(dirty.genes[thisNeuronLocation]);
                }
                else
                {
                    //Console.WriteLine("gene " + (thisNeuronLocation) + " was deleted");
                    ++neuronsDeletedSoFar;
                }
                thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber.Add(neuronsDeletedSoFar);
                //Console.WriteLine(thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber[thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber.Count - 1] + " neurons have been deleted before imp num " + (thisNeuronLocation - 1));
            }

            clean.genes[0].parts[0] = clean.genes.Count - 1;

            int firstConnectionOtThisNeuron;
            int lastConnectionOfThisNeuron;
            neuronsDeletedSoFar = 0;
            //Console.WriteLine("going over " + dirty.genes[0].parts[0] + " neurons");
           
            //copys and fixes connections
            for (int thisNeuronLocation = 1; thisNeuronLocation <= dirty.genes[0].parts[0]; ++thisNeuronLocation)
            {
                if (geneEffectedByInput[thisNeuronLocation] == true && geneEffectsOutput[thisNeuronLocation] == thingThatEffectsOutput)
                {
                    clean.genes[thisNeuronLocation - thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber[thisNeuronLocation - 1]].parts[bufferStart] = clean.genes.Count;
                    firstConnectionOtThisNeuron = dirty.firstConnectionOfNeuron(thisNeuronLocation - 1);
                    lastConnectionOfThisNeuron = dirty.lastConnectionOfNeuron(thisNeuronLocation - 1);
                    //Console.WriteLine("neuron " + (thisNeuronLocation - 1) + " starts at gene " + firstConnectionOtThisNeuron + ", and ends at gene " + lastConnectionOfThisNeuron);

                    for (int thisConnectionLocation = firstConnectionOtThisNeuron; thisConnectionLocation <= lastConnectionOfThisNeuron; ++thisConnectionLocation)////
                    {
                        //Console.Write("checking connection " + thisConnectionLocation + " - ");
                        if(dirty.genes[thisConnectionLocation].parts[multyplayer] != 0 && /*geneEffectedByInput[thisConnectionLocation] == true /*מיותר לכאורה כי אנחנו הרי יוצאים מנרנ שמושפע מהקלט*/ geneEffectsOutput[thisConnectionLocation] == thingThatEffectsOutput)
                        {
                            clean.addGene(dirty.genes[thisConnectionLocation]);
                            //Console.WriteLine("gene " + (thisConnectionLocation) + " was added to clean");
                            if (clean.genes[clean.genes.Count - 1].parts[externalOrInternal] != externalNueron)
                            {
                                clean.genes[clean.genes.Count - 1].parts[destenetion] -= thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber[clean.genes[clean.genes.Count - 1].parts[destenetion]];//the problem was here.
                                if (thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber[thisNeuronLocation - 1] > 0)
                                {
                                    //Console.WriteLine("gene " + thisConnectionLocation + " hufkat by " + thisManyNeuronsHaveBeenDeletedBeforeNeuronNumber[thisNeuronLocation - 1]);
                                }
                            }
                        }
                        else
                        {/*
                            //Console.Write("gene " + (thisConnectionLocation) + " was deleted. ");
                            if(dirty.genes[thisConnectionLocation].parts[multyplayer] == 0)
                            {
                                Console.Write("its multyplayer was 0. ");
                            }
                            if(geneEffectedByInput[thisConnectionLocation] == false)
                            {
                                Console.Write("it was not connected to the input. ");
                            }
                            if (geneEffectsOutput[thisConnectionLocation] != thingThatsEffectsOutput)
                            {
                                Console.Write("it was not connected to the output. ");
                            }
                            Console.WriteLine();
                            */
                        }
                       // Console.WriteLine("4 part " + drtm); dirty.print(); ++drtm;
                    }
                }
            }
            return (clean);
            int thisNeuroneffectsTheOutput(int[] geneEffectingOutput, int thisNeuronLocation)
            {
                //Console.WriteLine("this neouron effects output");
                if (geneEffectingOutput[thisNeuronLocation] != 0)
                {
                    return (geneEffectingOutput[thisNeuronLocation]);
                }

                geneEffectingOutput[thisNeuronLocation] = thingInCheck;

                int firstConnection = dirty.firstConnectionOfNeuron(thisNeuronLocation - 1);
                int lastConnection = dirty.lastConnectionOfNeuron(thisNeuronLocation - 1);
                for (int thisConnectionLocation = firstConnection; thisConnectionLocation <= lastConnection; ++thisConnectionLocation)
                {
                    if (dirty.genes[thisConnectionLocation].parts[multyplayer] == 0)
                    {
                        geneEffectingOutput[thisConnectionLocation] = thingThatDoesNotEffectsOutput;
                        continue;
                    }
                    // if(gnm.genes[gnm.genes[thisConnection].parts[destenetion] + 1].parts[externalOrInternal] == externalNueron || thisneuroneffectsTheOutput(ref gnm, ref effectingOutput, gnm.genes[thisConnection].parts[destenetion] + 1) == thingThatsEffectsOutput)
                    if (dirty.genes[thisConnectionLocation].parts[externalOrInternal] == externalNueron)
                    {
                        geneEffectingOutput[thisConnectionLocation] = thingThatEffectsOutput;
                        geneEffectingOutput[thisNeuronLocation] = thingThatEffectsOutput;
                    }
                    //Console.WriteLine("imabawto check whther gene " + (gnm.genes[thisConnectionLocation].parts[destenetion] + 1) + " effects the output or not");
                    else if (geneEffectingOutput[dirty.genes[thisConnectionLocation].parts[destenetion] + 1] != thingInCheck)
                    {
                        if (dirty.genes[dirty.genes[thisConnectionLocation].parts[destenetion] + 1].parts[neuronType] != neuron.Static)
                        {
                            if (thisNeuroneffectsTheOutput(geneEffectingOutput, dirty.genes[thisConnectionLocation].parts[destenetion] + 1) == thingThatEffectsOutput)
                            {
                                if (dirty.genes[thisConnectionLocation].parts[multyplayer] != 0) //this line is sketchy cause I just plonked this 'if' here to prevent approving of 0-multip connection, not sure its the right place to do it
                                {
                                    geneEffectingOutput[thisConnectionLocation] = thingThatEffectsOutput;
                                }
                                geneEffectingOutput[thisNeuronLocation] = thingThatEffectsOutput;
                            }
                        }
                        else
                        {
                            geneEffectingOutput[thisConnectionLocation] = thingThatDoesNotEffectsOutput;
                        }
                    }
                }
                return (geneEffectingOutput[thisNeuronLocation]);
            }
            void fireThisNeuron(genome gnm, bool[] geneEffectedByInput, int thisNeuronLocation)
            {
                if (geneEffectedByInput[thisNeuronLocation])
                {
                    //Console.WriteLine("gene " + thisNeuronLocation + " was alredy fired");
                    return;
                }
                geneEffectedByInput[thisNeuronLocation] = true;
                //Console.WriteLine("gene " + thisNeuronLocation + " was fired");

                int firstConnection = gnm.firstConnectionOfNeuron(thisNeuronLocation - 1);
                int lastConnection = gnm.lastConnectionOfNeuron(thisNeuronLocation - 1);
                int destenationLocation = 0;
                int destenationNeuronType = 0;

                for (int thisConnectionLocation = firstConnection; thisConnectionLocation <= lastConnection; ++thisConnectionLocation)
                {
                    geneEffectedByInput[thisConnectionLocation] = true;

                    destenationLocation = gnm.genes[thisConnectionLocation].parts[destenetion] + 1;
                    destenationNeuronType = gnm.genes[destenationLocation].parts[neuronType];

                    if (gnm.genes[thisConnectionLocation].parts[externalOrInternal] == externalNueron)
                    {
                        //Console.WriteLine("I wont fire gene " + destenationLocation + " because it leads to the output layer");
                        continue;
                    }
                    if (destenationNeuronType == neuron.Static || destenationNeuronType == neuron.Input)
                    {
                        //Console.WriteLine("I wont fire gene " + destenationLocation + " because it is pointing to a static or input neuron");
                        continue;
                    }
                    if (gnm.genes[thisConnectionLocation].parts[multyplayer] == 0)
                    {
                        //Console.WriteLine("I wont fire gene " + destenationLocation + " because the multyplayer from gene " + thisNeuronLocation + " (gene " + thisConnectionLocation + ") is 0");

                        continue;
                    }
                    //Console.WriteLine("Im in gene " + thisNeuronLocation + " (neuron). imma baoto fire through gene " + thisConnectionLocation + " which leads to gene " + destenationLocation)
                    fireThisNeuron(gnm, geneEffectedByInput, destenationLocation);
                }
            }
        }   
        public int firstConnectionOfNeuron(int neuronNumber)
        {
            return(genes[neuronNumber + 1].parts[bufferStart]);
        }
        public int lastConnectionOfNeuron(int neuronNumber)
        {
            if (neuronNumber + 1 >= genes[0].parts[0])
            {
                return(genes.Count - 1);
            }
            else
            {
                return(genes[neuronNumber + 2].parts[bufferStart] - 1);
            }
        }
    }
}