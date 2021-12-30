using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class creature
    {
        static Random rndm = new Random();
        public genome DNA;
        public network brian;
        private bool thisCreatureIsAlive = true;
        public int inputs = Coordinator.numOfIntputs;
        public int outputs = Coordinator.numOfOutputs;
        
        public int[] parameters = new int[Coordinator.numOfParameters];
        public davarPoneVeholek bug = new davarPoneVeholek();
        public int fireStrength = Coordinator.initialFireStrength;
        public double Scoer = 0;
        
        public bool alive()
        {
            return (thisCreatureIsAlive);
        }
        public creature(genome dna)
        {
            if(thisGenomeIsLegit(ref dna))
            {
                brian = getNetworkFromGenome(dna);
            }
            else
            {
                kill();
                //return;
            }
            DNA = dna;
        }
        public static network getNetworkFromGenome(genome gnm)
        {
            bool printNetworkCreation = false;
            if (printNetworkCreation)
            {
                Console.WriteLine("networkGen: recived this genome: ");
                gnm.print();
                Console.WriteLine("This genome has " + gnm.genes.Count + " genes");
                Console.Write("networkGen: starting a ");
                Console.Write(gnm.genes[0].parts[0]);
                Console.WriteLine(" nuerons network");
            }

            network net = new network(Coordinator.numOfIntputs, Coordinator.numOfOutputs);
            int numOfNeurons = gnm.genes[0].parts[0];
            //net.inputNeurons = gnm.genes[0].parts[1];/////why why why
            //net.outputNeurons = gnm.genes[0].parts[2];///this is not sapouz to be genetic
            double thisMultyplayer = 0;
            
            
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
                net.addNeuron(gnm.genes[thisNeuron + 1].parts[1]);
                if(gnm.genes[thisNeuron + 1].parts[1] == neuron.Static)
                {
                    net.neurons[thisNeuron].setStatic(gnm.genes[thisNeuron + 1].parts[2]);
                }
                if (printNetworkCreation)
                {
                    Console.WriteLine("networkGen: neuron added. tatal num of nrns is now: " + net.neurons.Count);
                }
                for(int thisConnection = buffersLocations[thisNeuron]; thisConnection < buffersLocations[thisNeuron + 1] /*&& thisConnection < gnm.genes.Count*/; ++thisConnection)
                {
                    thisMultyplayer = gnm.genes[thisConnection].parts[1];
                    thisMultyplayer /= 10;
                    net.neurons[thisNeuron].addConnection(gnm.genes[thisConnection].parts[0], thisMultyplayer);
                    if(gnm.genes[thisConnection].parts[2] %2 == 1)//goes to the final layer
                    {
                        net.neurons[thisNeuron].connections[net.neurons[thisNeuron].connections.Count - 1].connectedToTheFinalLayer = true;
                    }
                }
            }

            return (net);
        }
        bool thisGenomeIsLegit(ref genome gnm)
        {
            try
            {
                if (gnm.genes[0].parts[0] == 0) { }
            }
            catch
            {
                gnm = null;
            }

            if(gnm == null)
            {
                Console.WriteLine("captured null");

                return false;
            }

            int numOfNeurons = gnm.genes[0].parts[0];
            if(numOfNeurons < inputs)
            {
                gnm.genes[0].parts[0] = inputs;
                numOfNeurons = gnm.genes[0].parts[0];
                //return false;
            }
            if (gnm.genes.Count < 2 * numOfNeurons + 1)
            {
                return false;
            }
            List<int> buffersLocations = new List<int>();
            for(int thisBuffer = 1; thisBuffer < numOfNeurons + 1; ++thisBuffer)
            {
                if(gnm.genes[thisBuffer].parts[0] >= gnm.genes.Count || gnm.genes[thisBuffer].parts[0] <= numOfNeurons)
                {
                    //return false;
                    gnm.genes[thisBuffer].parts[0] = numOfNeurons + 1;
                }
                if (buffersLocations.Contains(gnm.genes[thisBuffer].parts[0]))
                {
                    gnm.genes[thisBuffer].parts[0] = numOfNeurons + 1;
                    while(buffersLocations.Contains(gnm.genes[thisBuffer].parts[0]) && gnm.genes[thisBuffer].parts[0] < gnm.genes.Count)
                    {
                        ++gnm.genes[thisBuffer].parts[0];
                    }
                    if(gnm.genes[thisBuffer].parts[0] >= gnm.genes.Count)
                    {
                        return false;
                    }
                }
                buffersLocations.Add(gnm.genes[thisBuffer].parts[0]);
                if(thisBuffer < Coordinator.numOfIntputs)
                {
                    gnm.genes[thisBuffer].parts[1] = neuron.Input;
                }
                else if(gnm.genes[thisBuffer].parts[1] != neuron.Input)//currently Im not allowing memory \ static \ output nuerons at all.
                {
                    gnm.genes[thisBuffer].parts[1] = neuron.logical;
                }
            }
            //מתקן מודולו - לא הכרחי אך אסטטי
            for (int thisConnection = numOfNeurons + 1; thisConnection < gnm.genes.Count; ++thisConnection)
            {
                //Console.WriteLine("checking destenations. total outputs is: " + outputs + ", total neurons is: " + numOfNeurons);
                if(gnm.genes[thisConnection].parts[2] % 2 == 1 /*goes to the final layer*/)
                {
                    gnm.genes[thisConnection].parts[2] = 1;
                    //Console.Write("destenation changed from: " + gnm.genes[thisConnection].parts[0]);
                    gnm.genes[thisConnection].parts[0] = gnm.genes[thisConnection].parts[0] % outputs;
                    //Console.WriteLine(" to: " + gnm.genes[thisConnection].parts[0]);
                }
                else
                {
                    gnm.genes[thisConnection].parts[2] = 0;
                    //Console.Write("destenation changed from: " + gnm.genes[thisConnection].parts[0]);
                    gnm.genes[thisConnection].parts[0] = gnm.genes[thisConnection].parts[0] % numOfNeurons;
                    //Console.WriteLine(" to: " + gnm.genes[thisConnection].parts[0]);
                }
            }
            return true;
        }
        public void TransformInto(creature crt)
        {
            DNA = crt.DNA;
            brian = crt.brian;
            parameters = crt.parameters; //it doesnt work. aintit
            Scoer = crt.Scoer;
        }
        void kill()
        {
            thisCreatureIsAlive = false;
            //Console.WriteLine("creature.kill()");
        }
        public void printStatus()
        {
            bug.print();
            Console.Write(". I just ");
            switch (brian.strongestOutput())
            {
                case 0: Console.WriteLine("turned lefta") ; break;
                case 1: Console.WriteLine("turned righta"); break;
                case 2: Console.WriteLine("stepped forwarda"); break;
                case 3: Console.WriteLine("attempted to extingwish the fire a"); break;
            }
        }
        public double run()
        {
            return (run(false));
        }
        public double run(bool doLifeReport)
        {
            double lowestScore = 10000;
            for (int test = 0; test < Coordinator.testsPerRun; ++test)
            {
                fireStrength = Coordinator.initialFireStrength;
                switch (rndm.Next(0, 2))//jini ES!
                {
                    case 0: Coordinator.fireX *= -1; break;
                    case 1: Coordinator.fireY *= -1; break;
                }
                
                
                bug.reset();
                for (int step = 0; step < Coordinator.stepsInEveryCreaturesLife; ++step)
                {
                    brian.neurons[0].setValue(90 - bug.direction + (180 * Math.Atan((Coordinator.fireY - bug.y) / (Coordinator.fireX - bug.x)) / Math.PI)); // fire from ritght
                    brian.neurons[1].setValue(180 - brian.neurons[0].value()); // fire form left
                    brian.neurons[2].setValue(bug.distanceFrom(Coordinator.fireX, Coordinator.fireY));/*1 / (krich.bug.distanceFrom(fireX, fireY) + 0.0001); // heat sensor
                /*
                krich.brian.neurons[0].setValue(krich.bug.x);
                krich.brian.neurons[1].setValue(krich.bug.y);
                */
                    brian.Fire();
                    if (doLifeReport)
                    {
                        Console.WriteLine("step " + step);
                    }
                    //this multy action thing is gooood
                    for (int thisOutput = 0; thisOutput < brian.outputNeurons; ++thisOutput)
                    {
                        if (brian.outputLayer[thisOutput].isOn())
                        {
                            doAction(thisOutput, doLifeReport);
                        }
                    }
                    if (brian.noOutput() && doLifeReport)
                    {
                        doAction(brian.strongestOutput(), doLifeReport);
                    }
                    if (doLifeReport)
                    {
                        bug.print();
                        Console.WriteLine(". fire strength: " + fireStrength);
                        Console.WriteLine();
                    }
                }
                calaulateScoer();
                if(lowestScore > Scoer)
                {
                    lowestScore = Scoer;
                }
                
            }
            Scoer = lowestScore;
            return (Scoer);
        }
        public void calaulateScoer()
        {
            Scoer = 100 - bug.distanceFrom(Coordinator.fireX, Coordinator.fireY) + 10 * (Coordinator.initialFireStrength - fireStrength);
        }
        private void doAction(int actionNumber, bool doLifeReport)
        {
            switch(actionNumber)
            {
                case 0: bug.turn(10); if (doLifeReport) { Console.WriteLine("\tturned left"); } break;
                case 1: bug.turn(-10); if (doLifeReport) { Console.WriteLine("\tturned right"); } break;
                case 2: bug.step(1); if (doLifeReport) { Console.WriteLine("\tstepped forward"); } break;
                case 3:
                    if (bug.distanceFrom(Coordinator.fireX, Coordinator.fireY) < Coordinator.minimumDistanceToExtinguishFire)
                    {
                        --fireStrength;
                        if(doLifeReport)
                        {
                            Console.Write("I succsessffuullyy exed fire. my location is ");
                            bug.print();
                            Console.WriteLine(", which is " + bug.distanceFrom(Coordinator.fireX, Coordinator.fireY) + " units away from the fire, which is in (" + Coordinator.fireX + ", " + Coordinator.fireY + "). the minimum distance to extingwish fire is " + Coordinator.minimumDistanceToExtinguishFire + " units.");
                        }
                    }
                    else
                    {
                        //++fireStrength;// creature gets punished for wasting water
                    }
                    if (doLifeReport)
                    {
                        Console.WriteLine("\tattempted to extingwish the fire (" + Coordinator.fireX + ", " + Coordinator.fireY + ")");
                        
                    }
                    break;
            }
        }
    }
}