using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            genome tst = new genome(3);
            tst.addGenes(9);
            
            tst.genes[0].parts = new int[] { 4, 0, 0 };
            tst.genes[1].parts = new int[] { 5, 0, 0 };
            tst.genes[2].parts = new int[] { 6, 0, 0 };
            tst.genes[3].parts = new int[] { 7, 0, 0 };
            tst.genes[4].parts = new int[] { 8, 0, 0 };
            tst.genes[5].parts = new int[] { 2, 80, 0 };
            tst.genes[6].parts = new int[] { 2, 40, 0 };
            tst.genes[7].parts = new int[] { 3, 50, 0 };
            tst.genes[8].parts = new int[] { 0, 0, 1 };

            //creature tstcrt = new creature(tst);
            network tstnt = creature.getNetworkFromGenome(tst);
            tstnt.networkDiagnos();
            /*
            tstnt.neurons[0].value = 0.1;
            tstnt.neurons[1].value = 0.6;
            tstnt.Fire();
            */
        }
    }
}
