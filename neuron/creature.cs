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

        private const int keep = 1;
        private const int inCheck = 2;
        
        public bool alive()
        {
            return (thisCreatureIsAlive);
        }
        public creature(genome dna)
        {
            if(thisGenomeIsLegit(dna))
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
            for (int thisNeuronLocation = 1; thisNeuronLocation <= numOfNeurons; ++thisNeuronLocation)
            {
                buffersLocations.Add(gnm.genes[thisNeuronLocation].parts[genome.bufferStart]);
                if (printNetworkCreation)
                {
                    Console.Write("networkGen: buffer added: ");
                    Console.WriteLine(gnm.genes[thisNeuronLocation].parts[genome.destenetion]);
                }
            }
            //buffersLocations.Sort();///prob #559
            buffersLocations.Add(buffersLocations[buffersLocations.Count - 1] + 1);
            if(printNetworkCreation)
            { 
                Console.WriteLine("networkGen: added empty closer buffer. total num of buffs is: " + buffersLocations.Count);
            }
            for (int thisNeuron = 0; thisNeuron < numOfNeurons; ++thisNeuron)
            {
                net.addNeuron(gnm.genes[thisNeuron + 1].parts[genome.neuronType]);
                if(gnm.genes[thisNeuron + 1].parts[genome.neuronType] == neuron.Static)
                {
                    net.neurons[thisNeuron].setStatic(gnm.genes[thisNeuron + 1].parts[genome.staticNeuronValue]);
                }
                if (printNetworkCreation)
                {
                    Console.WriteLine("networkGen: neuron added. tatal num of nrns is now: " + net.neurons.Count);
                }
                for(int thisConnection = buffersLocations[thisNeuron]; thisConnection < buffersLocations[thisNeuron + 1] && thisConnection < gnm.genes.Count/*מיותר לכאורה*/; ++thisConnection)
                {
                    thisMultyplayer = gnm.genes[thisConnection].parts[genome.multyplayer];
                    thisMultyplayer /= 10;
                    if(thisMultyplayer == 0)
                    {
                        thisMultyplayer = 1;
                    }
                    net.neurons[thisNeuron].addConnection(gnm.genes[thisConnection].parts[genome.destenetion], thisMultyplayer);
                    if(gnm.genes[thisConnection].parts[genome.externalOrInternal] %2 == 1)//goes to the final layer
                    {
                        net.neurons[thisNeuron].connections[net.neurons[thisNeuron].connections.Count - 1].connectedToTheFinalLayer = true;
                    }
                }
            }

            return (net);
        }
        /*
        public static void cleanVersionOf(genome dirty)
        {
            int[] keepList = new int[dirty.genes.Count];
            thisNeouronIsImportent(ref dirty, dirty.genes[0].parts[0], ref keepList);
        }
        private static bool thisNeouronIsImportent(ref genome gnm, int nrn, ref int[] keepList)
        {
            if(keepList[nrn] == keep)
            {
                return true;
            }
            if(gnm.genes[nrn].parts[genome.neuronType] == neuron.Input)
            {
                keepList[nrn] = keep;
                return true;
            }
            if(keepList[nrn] == inCheck)
            {
                return false;///? כן. כי לנרנים חשובים יהיה לפחות מקור אחד שהוא לא בבדיקה. או שרגע
            }
            keepList[nrn] = inCheck;
            for(int thisConnection = gnm.genes[0].parts[0] + 1; thisConnection < gnm.genes.Count; ++thisConnection)
            {
                if(gnm.genes[thisConnection].parts[genome.destenetion] == nrn && gnm.genes[thisConnection].parts[genome.multyplayer] != 0 && gnm.genes[thisConnection].parts[genome.externalOrInternal] != genome.externalNueron)
                {
                    //find source neuron
                    int originNeuron = -1;
                    for(int thisBuffer = 1; thisBuffer <= gnm.genes[0].parts[0]; ++thisBuffer)
                    {
                        if(gnm.genes[thisBuffer].parts[genome.bufferStart] > thisConnection)
                        {
                            originNeuron = thisBuffer - 1;
                            break;
                        }
                    }
                    if(originNeuron == -1)
                    {
                        originNeuron = gnm.genes[0].parts[0] - 1;
                    }
                    if(thisNeouronIsImportent(ref gnm, originNeuron, ref keepList))
                    {
                        keepList[nrn] = keep;
                        return true;
                    }
                }
            }
            return false;
        }*/
        public static bool thisGenomeIsLegit(genome gnm) //AKA officer G (enforces some structure on the randome genome)
        /*
         * prob #559
         * we need to make sure that the buffers locations are in order, so we can diturmen where every chapter of connections ends.
         * the solution Im offering right now is to just check if there is a buffer that obay that rule, and if so, decide that this genome is not legit.
         * so like this we dont actually need to take care of the problem, cause the evolution will do this for us.
         * prob with this: as menshined in the eggplant documentation, our goal in this function is to make sure that wee suppurt as many possible genomes as we can.
         * that means finding a solusion to probs like this insted of counting on the evolution to fix it.
         * if such, my improved offer is: to attempt fixing the prob at some point but now when I say it I actuallty think we need to try first to just let the evo to take care of it, cause it looks like that whats gonna happend anyway.
         * 
         * well. after some thoughts. this current situation isnt ideal, I would rather just getting rid o fthe genome entayerly, and preforming mutation directly on thee network, where neurons dont infultrat each other. but this will take time to change, and is a bit agienst my goals here.
         * therefore, I have implemented some patches and for now we will continou with the current evo model. also I want pizza right now.
         */
            
        {
            //null filtering
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

            //IO coordinating
            if(gnm.genes[0].parts[0] < Coordinator.numOfIntputs)
            {
                gnm.genes[0].parts[0] = Coordinator.numOfIntputs;
                //return false;
            }
            gnm.genes[0].parts[1] = Coordinator.numOfIntputs;
            gnm.genes[0].parts[2] = Coordinator.numOfOutputs;

            int numOfNeurons = gnm.genes[0].parts[0];
            if (gnm.genes.Count < 2 * numOfNeurons + 1)
            {
                return false;
            }

            //buffers area
            gnm.genes[1].parts[genome.bufferStart] = gnm.genes[0].parts[0] + 1; // making suree we are not skipping any connections// see prob #559

            //List<int> buffersLocations = new List<int>();
            for(int thisBuffer = 1; thisBuffer <= numOfNeurons; ++thisBuffer)
            {
                if (thisBuffer > 1)
                {
                    if (gnm.genes[thisBuffer].parts[genome.bufferStart] <= gnm.genes[thisBuffer - 1].parts[genome.bufferStart])
                    {
                        gnm.genes[thisBuffer].parts[genome.bufferStart] = gnm.genes[thisBuffer - 1].parts[genome.bufferStart] + 1;
                    }
                }
                if (gnm.genes[thisBuffer].parts[genome.bufferStart] >= gnm.genes.Count || gnm.genes[thisBuffer].parts[genome.bufferStart] <= numOfNeurons)
                {
                    return false;
                  //  gnm.genes[thisBuffer].parts[genome.bufferStart] = numOfNeurons + 1;//
                }
                
                /*
                if (buffersLocations.Contains(gnm.genes[thisBuffer].parts[genome.bufferStart]))
                {
                    gnm.genes[thisBuffer].parts[genome.bufferStart] = numOfNeurons + 1;
                    while(buffersLocations.Contains(gnm.genes[thisBuffer].parts[genome.bufferStart]) && gnm.genes[thisBuffer].parts[genome.bufferStart] < gnm.genes.Count)
                    {
                        ++gnm.genes[thisBuffer].parts[genome.destenetion];
                    }
                    if(gnm.genes[thisBuffer].parts[genome.destenetion] >= gnm.genes.Count)
                    {
                        return false;//too many buffers in this genome
                    }
                }
                buffersLocations.Add(gnm.genes[thisBuffer].parts[genome.bufferStart]);
                */
                //neuron type handeling
                if(thisBuffer < Coordinator.numOfIntputs)
                {
                    gnm.genes[thisBuffer].parts[genome.neuronType] = neuron.Input;
                }
                else if(gnm.genes[thisBuffer].parts[genome.neuronType] != neuron.Input)//currently Im not allowing memory \ static \ output nuerons at all.
                {
                    gnm.genes[thisBuffer].parts[genome.neuronType] = neuron.logical;
                }
            }

            //משער מספר פלטים אם אין מידע בגנום - לדעתי מיותר
            gnm.genes[0].parts[2] = Coordinator.numOfOutputs;
            if (gnm.genes[0].parts[2] <= 0)
            {
                int highestPointerToOutput = 0;
                for(int thisConnection = gnm.genes[0].parts[0] + 1; thisConnection <= gnm.genes.Count; ++thisConnection)
                {
                    if(gnm.genes[thisConnection].parts[genome.externalOrInternal] == genome.externalNueron)
                    {
                        if(gnm.genes[thisConnection].parts[genome.destenetion] > highestPointerToOutput)
                        {
                            highestPointerToOutput = gnm.genes[thisConnection].parts[genome.destenetion];
                        }
                    }
                }
                gnm.genes[0].parts[2] = highestPointerToOutput + 1;
            }
            //מתקן מודולו - לא הכרחי אך אסטטי
            //bool thisIsTheOnlyConnectionInTheNeuron;
            for (int thisConnection = numOfNeurons + 1; thisConnection < gnm.genes.Count; ++thisConnection)
            {
                if(gnm.genes[thisConnection].parts[genome.externalOrInternal] % 2 == genome.externalNueron)
                {
                    gnm.genes[thisConnection].parts[genome.externalOrInternal] = genome.externalNueron;
                    gnm.genes[thisConnection].parts[genome.destenetion] = gnm.genes[thisConnection].parts[genome.destenetion] % gnm.genes[0].parts[2];
                }
                else
                {
                    gnm.genes[thisConnection].parts[genome.externalOrInternal] = genome.internalNeuron;
                    gnm.genes[thisConnection].parts[genome.destenetion] = gnm.genes[thisConnection].parts[genome.destenetion] % numOfNeurons;
                }
                /*
                if(gnm.genes[thisConnection].parts[genome.multyplayer] == 0) //empty connection killing//fix needed: if this is the only connection in the neuron - delete the buffer
                {
                    
                    if(thisConnection == gnm.genes.Count - 1 && gnm.genes[numOfNeurons].parts[genome.bufferStart] == thisConnection)
                    {
                        thisIsTheOnlyConnectionInTheNeuron = true;
                    }
                    else if(thisConnection == numOfNeurons + 1 && gnm.genes[2].parts[genome.bufferStart] == thisConnection + 1)//very small networks might break here / actually no, the 'if' above should catch networks with only one connection
                    {
                        thisIsTheOnlyConnectionInTheNeuron = true;
                    }
                    else if(/*this is a connection in a mono connection neuron)
                        for(int thisBuffer = 1; thisBuffer <= numOfNeurons; ++thisBuffer)
                    {
                        if(gnm.genes[thisBuffer].parts[genome.bufferStart] > thisConnection)
                        {
                            --gnm.genes[thisBuffer].parts[genome.bufferStart];
                        }
                    }
                    gnm.genes.RemoveAt(thisConnection);
                }*/
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
                if (doLifeReport) { Console.WriteLine("***test " + test + " out of " + Coordinator.testsPerRun); }
                fireStrength = Coordinator.initialFireStrength;
                switch (rndm.Next(0, 2))//jini ES!
                {
                    case 0: Coordinator.fireX *= -1; break;
                    case 1: Coordinator.fireY *= -1; break;
                }
                
                
                bug.reset();
                for (int step = 0; step < Coordinator.stepsInEveryCreaturesLife; ++step)
                {
                    if(90 - bug.direction + bug.directionTo(Coordinator.fireX, Coordinator.fireY) > 90)
                    {
                        brian.neurons[0].setValue(1);
                        brian.neurons[1].setValue(0);
                    }
                    else
                    {
                        brian.neurons[0].setValue(0);
                        brian.neurons[1].setValue(1);
                    }
                    //brian.neurons[0].setValue(); // fire from ritght
                    //brian.neurons[1].setValue(180 - brian.neurons[0].value()); // fire form left
                    brian.neurons[2].setValue(bug.distanceFrom(Coordinator.fireX, Coordinator.fireY));/*1 / (krich.bug.distanceFrom(fireX, fireY) + 0.0001); // heat sensor*/
                    brian.neurons[3].setValue(bug.directionTo(Coordinator.fireX, Coordinator.fireY) / 10);

                /*
                krich.brian.neurons[0].setValue(krich.bug.x);
                krich.brian.neurons[1].setValue(krich.bug.y);
                */
                    brian.Fire();
                    if (false & doLifeReport)
                    {
                        Console.WriteLine("step " + step);
                    }
                    //this multy action thing is gooood
                    for (int thisOutput = 0; thisOutput < brian.outputNeurons; ++thisOutput)
                    {
                        if (brian.outputLayer[thisOutput].isOn())
                        {
                            doAction(thisOutput, false & doLifeReport);
                        }
                    }
                    if (brian.noOutput() && doLifeReport)
                    {
                        doAction(brian.strongestOutput(), false & doLifeReport);
                    }
                    if (false & doLifeReport)
                    {
                        bug.print();
                        Console.WriteLine(". fire strength: " + fireStrength);
                        Console.WriteLine();
                    }
                }
                calaulateScoer();
                if (doLifeReport) Console.WriteLine("scoer: " + Scoer);
                if(lowestScore > Scoer)
                {
                    lowestScore = Scoer;
                }
                
            }
            Scoer = lowestScore;
            if(doLifeReport) Console.WriteLine("final scoer: " + Scoer);
            return (Scoer);

            void calaulateScoer()
            {
                Scoer = 100 - bug.distanceFrom(Coordinator.fireX, Coordinator.fireY) + 10 * (Coordinator.initialFireStrength - fireStrength);
            }
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
                    else if (Coordinator.punishWaterWaste)
                    {
                        ++fireStrength;// creature gets punished for wasting water
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