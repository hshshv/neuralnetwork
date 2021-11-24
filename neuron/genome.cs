using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class genome
    {
        public List<gene> genes = new List<gene>();

        public genome() { }
        public void addGene()
        {
            genes.Add(new gene());
        }
        public void addGene(int p1, int p2, int p3)
        {
            gene geneToAdd = new gene(p1, p2, p3);
            genes.Add(geneToAdd);
        }
        public void print()
        {
            for(int i = 0; i < genes.Count; ++i)
            {
                genes[i].print();
                Console.WriteLine();
            }
        }
    }

    class gene
    {
        public int[] parts = new int[3];
        public gene() { }
        public gene(int part1, int part2, int part3)
        {
            parts[0] = part1;
            parts[1] = part2;
            parts[2] = part3;
        }

        public void print()
        {
            Console.Write("{" + parts[0] + ", " + parts[1] + ", " + parts[2] + "}");
        }
    }
}
