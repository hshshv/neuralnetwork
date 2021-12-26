using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            creature winner = Coordinator.evolution(100);

            Console.WriteLine("final scoer: " + winner.Scoer);
            Console.WriteLine("X: " + winner.parameters[0]);
            Console.WriteLine("Y: " + winner.parameters[1]);
            /*
            genome tst = new genome(3);
            tst.addGenes(7);
            
            tst.genes[0].parts = new int[] { 3, 2, 1 };//num ofnuerons (excluding the outputa), num of inputs, num of outputs;
            
            tst.genes[1].parts = new int[] { 4, 0, 0 };
            tst.genes[2].parts = new int[] { 5, 0, 0 };
            tst.genes[3].parts = new int[] { 6, 0, 0 };
            
            tst.genes[4].parts = new int[] { 2, 80, 0 }; //connection: to neuron 2, muultiply by 80, not to the output
            tst.genes[5].parts = new int[] { 2, 40, 0 };
            tst.genes[6].parts = new int[] { 0, 50, 1 }; //connrction: to nueron 0, mltply 50, connected to the final layer (%2 == 1)
            
            //creature tstcrt = new creature(tst);
            network tstnt = creature.getNetworkFromGenome(tst);

            tstnt.neurons[0].value = 5;
            tstnt.neurons[1].value = 8;

            tstnt.networkDiagnos();
            tstnt.Fire();
            /*
            tstnt.neurons[0].value = 0.1;
            tstnt.neurons[1].value = 0.6;
            tstnt.Fire();
            */
        }
    }
}
