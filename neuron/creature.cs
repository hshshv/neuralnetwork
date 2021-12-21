using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class creature
    {
        private genome DNA;
        private network brian;
        private bool thisCreatureIsAlive = true;
        public int inputs = 1;
        public int outputs = 1;
        public bool alive()
        {
            return (thisCreatureIsAlive);
        }
        public creature(genome dna)
        {
            if(!thisGenomeIsLegit(dna))
            {
                kill();
                return;
            }
            DNA = dna;
            brian = getNetworkFromGenome(DNA);
        }
        public static network getNetworkFromGenome(genome gnm)
        {
            bool printNetworkCreation = true;
            network net = new network();
            net.inputNeurons = gnm.genes[0].parts[1];
            net.outputNeurons = gnm.genes[0].parts[2];
            double thisMultyplayer = 0;

            int numOfNeurons = gnm.genes[0].parts[0];
            if (printNetworkCreation)
            {
                Console.WriteLine("networkGen: recived this genome: ");
                gnm.print();
                Console.Write("networkGen: starting a ");
                Console.Write(numOfNeurons);
                Console.WriteLine(" nuerons network");
            }
            List<int> buffersLocations = new List<int>();
            for (int i = 0; i < numOfNeurons; ++i)
            {
                buffersLocations.Add(gnm.genes[i + 1].parts[0]);
                if (printNetworkCreation)
                {
                    Console.Write("networkGen: buffer added: ");
                    Console.WriteLine(gnm.genes[i + 1].parts[0]);
                }
            }
            buffersLocations.Sort();
            buffersLocations.Add(buffersLocations[buffersLocations.Count - 1] + 1);
            if(printNetworkCreation)
            { 
                Console.WriteLine("networkGen: added empty closer buffer. total num of buffs is: " + buffersLocations.Count);
            }
            for (int thisNeuron = 0; thisNeuron < numOfNeurons; ++thisNeuron)
            {
                net.addNeuron();
                if (printNetworkCreation)
                {
                    Console.Write("networkGen: neuron added. tatal num of nrns is now: ");
                    Console.WriteLine(net.neurons.Count);
                }
                for(int thisConnection = buffersLocations[thisNeuron]; thisConnection < buffersLocations[thisNeuron + 1] /*&& thisConnection < gnm.genes.Count*/; ++thisConnection)
                {
                    thisMultyplayer = gnm.genes[thisConnection].parts[1];
                    thisMultyplayer /= 100;
                    net.neurons[thisNeuron].addConnection(gnm.genes[thisConnection].parts[0], thisMultyplayer);
                    if(gnm.genes[thisConnection].parts[2] %2 == 1)//goes to the final layer
                    {
                        net.neurons[thisNeuron].connections[net.neurons[thisNeuron].connections.Count - 1].connectedToTheFinalLayer = true;
                    }
                }
            }

            return (net);
        }
        bool thisGenomeIsLegit(genome gnm)
        {
            int numOfNeurons = gnm.genes[0].parts[0];
            if(numOfNeurons < inputs)
            {
                return false;
            }
            if (gnm.genes.Count < 2 * numOfNeurons + 1)
            {
                return false;
            }
            List<int> buffersLocations = new List<int>();
            for(int i = 0; i < numOfNeurons; ++i)
            {
                if(gnm.genes[i+1].parts[0] >= gnm.genes.Count || gnm.genes[i + 1].parts[0] <= numOfNeurons)
                {
                    return false;
                }
                if(buffersLocations.Contains(gnm.genes[i+1].parts[0]))
                {
                    return false;
                }
                else
                {
                    buffersLocations.Add(gnm.genes[i + 1].parts[0]);
                }
            }
            return true;
        }
        void kill()
        {
            thisCreatureIsAlive = false;
        }
    }
}
