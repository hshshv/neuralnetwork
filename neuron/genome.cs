using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace neuronprog
{
    class genome
    {
        public List<gene> genes = new List<gene>();
        static Random rndm = new Random();

        public genome() { }
        public void addGene(int partsInThisGene)
        {
            genes.Add(new gene(partsInThisGene));
        }
        public void print()
        {
            for (int i = 0; i < genes.Count; ++i)
            {
                genes[i].print();
                Console.WriteLine();
            }
        }

        public void mutate(int chanceOfMutationForEachPart)
        {
            for (int thisGene = 0; thisGene < genes.Count; ++thisGene)
            {
                for (int thisPartOfTheGene = 0; thisPartOfTheGene < genes[thisGene].parts.Length; ++thisPartOfTheGene)
                {
                    if (rndm.Next(0, chanceOfMutationForEachPart) == 0)
                    {
                        genes[thisGene].parts[thisPartOfTheGene] = singelMutetedValue(genes[thisGene].parts[thisPartOfTheGene]);
                    }
                }
            }
        }
        public static genome mutate(genome genomeToMutat, int chanceOfMutationForEachPart) //this is used to make a mutated copy of a specific genome
        {
            for (int thisGene = 0; thisGene < genomeToMutat.genes.Count; ++thisGene)
            {
                for (int thisPartOfTheGene = 0; thisPartOfTheGene < genomeToMutat.genes[thisGene].parts.Length; ++thisPartOfTheGene)
                {
                    if (rndm.Next(0, chanceOfMutationForEachPart) == 0)
                    {
                        genomeToMutat.genes[thisGene].parts[thisPartOfTheGene] = singelMutetedValue(genomeToMutat.genes[thisGene].parts[thisPartOfTheGene]);
                    }
                }
            }
            return (genomeToMutat);
        }

        static int singelMutetedValue(int valueBefor)
        {
            double tempValueHplder = valueBefor * (rndm.NextDouble() * 2);
            int valueAfter = Convert.ToInt32(tempValueHplder);
            if (valueAfter == 0)
            {
                return (1);
            }
            return (valueAfter);
        }
    }
}


    class gene
    {
        public int[] parts;//you should make a general class, not for the spesific case of 3 part in every gene
        public gene(int partsInThisGene) 
        {
            parts = new int[partsInThisGene];
            for(int i = 0; i < parts.Length; ++i)
            {
                parts[i] = 100;//100 is the dflt value for a part in the gene
            }
        }
        

        public void print()
        {
            Console.Write("{" + parts[0] + ", " + parts[1] + ", " + parts[2] + "}");
        }
    }

