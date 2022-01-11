using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace neuronprog
{
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

        private static int thingThatsEffectsOutput = 2;
        private static int thingThatsDoesNotEffectsOutput = 1;
        
        public genome(int partPerGene)
        {
            numberOfPartInEveryGene = partPerGene;
        }
        public void addGene()
        {
            genes.Add(new gene(numberOfPartInEveryGene));
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
                genes[i].print();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void mutate()
        {
            genes = mutate(this).genes;
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
        public static void cleanVersionOf(genome dirty)
        {
            bool[] effectedByInput = new bool[dirty.genes.Count];
            int[] effectsOutput = new int[dirty.genes.Count];
            
            for (int thisNeuron = 1; thisNeuron <= dirty.genes[0].parts[0]; ++thisNeuron)//checks roots
            {
                if(dirty.genes[thisNeuron].parts[neuronType] == neuron.Static || dirty.genes[thisNeuron].parts[neuronType] == neuron.Input)
                {
                    fireThisNeuron(ref dirty, ref effectedByInput, thisNeuron);
                }
            }

            for (int thisNeuron = 1; thisNeuron <= dirty.genes[0].parts[0]; ++thisNeuron)//checks branches
            {
                effectsOutput[thisNeuron] = thisneuroneffectsTheOutput(ref dirty, ref effectsOutput, thisNeuron);
                for (int thisConnection = dirty.genes[thisNeuron].parts[bufferStart]; thisConnection < dirty.genes[thisNeuron + 1].parts[genome.bufferStart] && thisConnection < dirty.genes.Count; ++thisConnection)
                {
                    effectsOutput[thisConnection] = thisneuroneffectsTheOutput(ref dirty, ref effectsOutput, dirty.genes[thisConnection].parts[destenetion] + 1);
                }
            }

            //copy dirty to a new genome (only copy parts that are bothe effecting output and effected by input)

        }
        public static void fireThisNeuron(ref genome gnm, ref bool[] effectedByInput, int thisNeuron)
        {
            if(effectedByInput[thisNeuron])
            {
                return;
            }
            effectedByInput[thisNeuron] = true;
            for (int thisConnection = gnm.genes[thisNeuron].parts[bufferStart]; thisConnection < gnm.genes[thisNeuron + 1].parts[genome.bufferStart] && thisConnection < gnm.genes.Count; ++thisConnection)
            {
                if (!(gnm.genes[gnm.genes[thisConnection].parts[destenetion] + 1].parts[neuronType] == neuron.Static || gnm.genes[gnm.genes[thisConnection].parts[destenetion] + 1].parts[neuronType] == neuron.Input))
                {
                    effectedByInput[thisConnection] = true;
                    fireThisNeuron(ref gnm,  ref effectedByInput, gnm.genes[thisConnection].parts[destenetion] + 1);
                }
            }
        }
        public static int thisneuroneffectsTheOutput(ref genome gnm, ref int[] effectingOutput, int thisNeuron)
        {
            if(effectingOutput[thisNeuron] == thingThatsEffectsOutput)
            {
                return (thingThatsEffectsOutput);
            }
            if(effectingOutput[thisNeuron] == thingThatsDoesNotEffectsOutput)
            {
                return (thingThatsDoesNotEffectsOutput);
            }

            for (int thisConnection = gnm.genes[thisNeuron].parts[bufferStart]; thisConnection < gnm.genes[thisNeuron + 1].parts[genome.bufferStart] && thisConnection < gnm.genes.Count; ++thisConnection)
            {
                if(gnm.genes[gnm.genes[thisConnection].parts[destenetion] + 1].parts[externalOrInternal] == externalNueron || thisneuroneffectsTheOutput(ref gnm, ref effectingOutput, gnm.genes[thisConnection].parts[destenetion] + 1) == thingThatsEffectsOutput)
                {
                    effectingOutput[thisConnection] = thingThatsEffectsOutput;
                    effectingOutput[thisNeuron] = thingThatsEffectsOutput;
                    return (thingThatsEffectsOutput);
                }
            }
            effectingOutput[thisNeuron] = thingThatsDoesNotEffectsOutput;
            return (thingThatsDoesNotEffectsOutput);
        }
    }


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
    }

}