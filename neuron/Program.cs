using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            msho avner = new msho(5, "hamesh");
            msho avner2 = new msho(5, "hamesh");
            avner2 = new msho(avner.nam, avner.text);
            avner.nam = 4;
            Console.WriteLine(avner2.nam + avner2.text);
            */
            //Console.WriteLine(mike.direction + (180 * Math.Atan((20 - mike.y) / (20 - mike.x)) / Math.PI));
            
            Coordinator.printEvoSetting();
            creature winner = Coordinator.evolution(1000000);
            Console.ReadKey();
            Console.WriteLine("\n **************** \n");
            Console.WriteLine("final scoer: " + winner.Scoer);
            Console.WriteLine("X: " + winner.parameters[0]);
            Console.WriteLine("Y: " + winner.parameters[1]);
            Console.WriteLine("best creature genome: ");
            winner.DNA.print();
            Console.WriteLine("BEST network:");
            winner.brian.networkDiagnos();
            
            /*
            genome tst = new genome(3);
            tst.addGenes(12);
            
            tst.genes[0].parts = new int[] { 5, 1, 1 };//num of nuerons (excluding the outputa), num of inputs, num of outputs;
            
            tst.genes[1].parts = new int[] { 6, neuron.Input, 0 };
            tst.genes[2].parts = new int[] { 8, neuron.logical, 0 };
            tst.genes[3].parts = new int[] { 9, neuron.logical, 0 };
            tst.genes[4].parts = new int[] { 10, neuron.logical, 0 };
            tst.genes[5].parts = new int[] { 11, neuron.logical, 0 };

            tst.genes[6].parts = new int[] { 1, 50, 0 }; //connection: to neuron 2, muultiply by 80, not to the output
            tst.genes[7].parts = new int[] { 3, 50, 0 };
            tst.genes[8].parts = new int[] { 2, 50, 0 }; //connrction: to nueron 0, mltply 50, connected to the final layer (%2 == 1)
            tst.genes[9].parts = new int[] { 1, 50, 0 }; //connection: to neuron 2, muultiply by 80, not to the output
            tst.genes[10].parts = new int[] { 4, 50, 0 };
            tst.genes[11].parts = new int[] { 0, 50, 1 };

            //tst.transformInto(genome.cleanVersionOf(tst));
            tst.clean();
            Console.WriteLine("clean:");
            tst.print();

            /*
            creature tstcrt = new creature(tst);
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
