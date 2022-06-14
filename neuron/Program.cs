using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            genome POCENOM = new genome(3);
            int[] gnim = { 1, 2, 3 };

            gnim = new int[] { 4, 4, 4 }; POCENOM.addGene(new gene(gnim));

            gnim = new int[] { 5, 1, 0 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 6, 1, 0 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 7, 1, 0 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 9, 1, 0 }; POCENOM.addGene(new gene(gnim));

            gnim = new int[] { 0, 10, 1 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 1, 10, 1 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 2, 1, 1 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 3, 1000, 1 }; POCENOM.addGene(new gene(gnim));
            gnim = new int[] { 3, 0, 1 }; POCENOM.addGene(new gene(gnim));

            creature POCO = new creature(POCENOM);
            Console.WriteLine("POCO is alive: " + POCO.alive());
            Console.WriteLine("POCO. analesis: ");
            POCO.brian.networkDiagnos();
            Console.WriteLine("POCO RUN:");
            POCO.run(true);
            
            /*
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
           */ 
        }
    }
}
