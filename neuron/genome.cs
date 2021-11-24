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
        public int chanceOfMutationForEachPart = 30;
        public int chanceOfCreatingANewGeneOrDeletingOne = 100;
        static Random rndm = new Random();


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
            for(int i = 0; i < genesToAdd; ++i)
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
            genomeToMutat.print();
            for (int thisGene = 0; thisGene < genomeToMutat.genes.Count; ++thisGene)
            {
                for (int thisPartOfTheGene = 0; thisPartOfTheGene < genomeToMutat.genes[thisGene].parts.Length; ++thisPartOfTheGene)
                {
                    if (rndm.Next(0, genomeToMutat.chanceOfMutationForEachPart) == 0)
                    {
                        genomeToMutat.genes[thisGene].parts[thisPartOfTheGene] = singelMutetedValue(genomeToMutat.genes[thisGene].parts[thisPartOfTheGene]);
                    }
                }
            }
            if (rndm.Next(0, genomeToMutat.chanceOfCreatingANewGeneOrDeletingOne) == 0)//adding a new gene at a randome location
            {
                int addNewGeneAt = rndm.Next(0, genomeToMutat.genes.Count);
                genomeToMutat.genes.Insert(addNewGeneAt, new gene(genomeToMutat.numberOfPartInEveryGene));
            }
            if (rndm.Next(0, genomeToMutat.chanceOfCreatingANewGeneOrDeletingOne) == 0)//removing an existing gene at a randome location
            {
                genomeToMutat.genes.RemoveAt(rndm.Next(0, genomeToMutat.genes.Count));
            }
            genomeToMutat.print();

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

