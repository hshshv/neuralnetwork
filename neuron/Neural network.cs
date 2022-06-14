using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class connection
    {
        public int destenation;
        public double multyplayer;
        public bool connectedToTheFinalLayer;
        public connection(int des, double mlty)
        {
            destenation = des;
            multyplayer = mlty;
        }
        public connection()
        {
            destenation = 0;
            multyplayer = 0;
        }
    }
    class neuron
    {
        //neuron typs
        public const int logical = 0;
        public const int Input = 1;
        public const int Static = 2;
        public const int Memory = 3;
        public const int Output = 4;
        public const int nueornTyps = 4;
        static double activetionLevel = 0.5;
        public static string[] typeNames = {"logical", "input", "static", "memory", "output" };
        //end of neuron typs

        private double myValue = 0.0;
        public List<connection> connections = new List<connection>();
        private int myType;
        public bool RecivedInfo = false;
        //public string name;
        public bool isOn()
        {
            return (myValue >= activetionLevel);
        }
        public neuron(List<connection> cncs)
        {
            connections = cncs;
            for (int i = 0; i < cncs.Count; ++i)
            {
                connections[i] = cncs[i];
            }
        }
        /*public neuron(List<connection> cncs, string neuronName) : this(cncs)
        {
            name = neuronName;
        }*/
        public neuron() { }
        public neuron(int Ntype)
        {
            myType = Ntype % nueornTyps;
            if(myType == Static || myType == Input)
            {
                RecivedInfo = true;
            }
            else
            {
                RecivedInfo = false;
            }
        }
        /*public neuron(int destention, double multypliiir, double initialValue) : this(destention, multypliiir)
        {
            value = initialValue;
        }*/
        public void addConnection(int destnetion, double multyplaer)
        {
            connection newConnection = new connection(destnetion, multyplaer);
            connections.Add(newConnection);
        }
        public void clear(bool clearMemory)
        {
            if(myType != Static && myType != Input && myType != Memory)//memory neurons will also remember if they'v been fired at or not
            {
                RecivedInfo = false;
            }
            if (myType != Static && myType != Input && (myType != Memory || clearMemory))
            {
                myValue = 0;
            }
        }
        public void clear()
        {
            clear(false);
        }
        public int type()
        {
            return (myType);
        }
        public bool isAnInputer()
        {
            return (myType == Input || myType == Static);
        }
        public bool needsInfo()//nrns that requier info from privius nrns in order to fire the next nrns (so nrns
        {
            return (myType == logical || myType == Memory);
        }
        public void setStatic(double nValue)
        {
            myType = logical;
            myValue = nValue;
            doSigmoid();
            myType = Static;
        }
        public double value()
        {
            return (myValue);
        }
        public void setValue(double nValue)
        {
            if(myType == Input)
            {
                myValue = nValue;
            }
        }
        public void changeValue(double addValue)
        {
            if(myType != Input && myType != Static)
            {
                myValue += addValue;
                RecivedInfo = true;
            }
        }
        public void doSigmoid()
        {
            if(myValue == 0)
            {
                return;
            }
            if(myType != Static)
            {
                myValue = 1.0 / (1.0 + Math.Pow(Math.E, -value()));
            }
        }
    }
    class network
    {
        static bool printNetworkActivity = false;//warning: dont do it. you cant handle it. //practicly const, not formally cause of the anoyyyyying warnings about unreachable code
        public List<neuron> neurons = new List<neuron>();
        public neuron[] outputLayer = new neuron[Coordinator.numOfOutputs];//צריך איכשהו להגדיר את זה דרך המתזמנן הכללי ולא מכאן
        public int inputNeurons = Coordinator.numOfInputs;
        public int outputNeurons = Coordinator.numOfOutputs;
        public network(int inpNueurons, int outNeurons)
        {
            inputNeurons = inpNueurons;
            outputNeurons = outNeurons;
            outputLayer = new neuron[outNeurons];
            for (int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                outputLayer[thisOutput] = new neuron(neuron.Output);
            }

        }
        public void addNeuron()
        {
            neurons.Add(new neuron());
        }
        public void addNeuron(int neuronType)
        {
            neurons.Add(new neuron(neuronType));
        }
        public void Fire()
        {           
            if (printNetworkActivity)
            {
                Console.WriteLine("fireing the network");
            }
            //outputLayer = new neuron[outputNeurons];
            clear();
            for (int i = 0; i < outputLayer.Length; ++i)
            {
                outputLayer[i] = new neuron();
            }

            for (int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                if(!neurons[thisNeuron].RecivedInfo && neurons[thisNeuron].needsInfo())
                {
                    if(printNetworkActivity)
                    {
                        Console.WriteLine("  neuron " + thisNeuron + " didn't recived any information from the previous neurons, so it wont fire");
                        continue;
                    }
                }
                if (printNetworkActivity)
                {
                    Console.WriteLine("  fureing neuron " + thisNeuron);
                }
                
                if(Coordinator.doSigmoid)
                {
                    if(printNetworkActivity)
                        Console.Write("The value of neuron " + thisNeuron + " has been sigmofaid from " + neurons[thisNeuron].value());
                    neurons[thisNeuron].doSigmoid();
                    if(printNetworkActivity)
                        Console.WriteLine(" to " + neurons[thisNeuron].value());
                }

                for (int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                {

                    int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                    double thisValue = neurons[thisNeuron].value();
                    double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;
                    if(printNetworkActivity)
                        Console.WriteLine("   fireing throught connection " + thisConnection);
                    if(neurons[thisNeuron].connections[thisConnection].connectedToTheFinalLayer)
                    {
                        if (printNetworkActivity)
                            Console.Write("  neuron " + thisNeuron + " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". output " + destnitionNeuron + " updated from " + outputLayer[destnitionNeuron].value() + " to ");
                        outputLayer[destnitionNeuron].changeValue(thisValue * thisMultyplayer);
                        if (printNetworkActivity)
                            Console.WriteLine(outputLayer[destnitionNeuron].value());
                    }
                    else
                    {
                        if (printNetworkActivity)
                            Console.Write("  neuron " + thisNeuron + " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". neuron " + destnitionNeuron + " updated from " + neurons[destnitionNeuron].value() + " to ");
                        
                        neurons[destnitionNeuron].changeValue(thisValue * thisMultyplayer);
                        if (printNetworkActivity)
                            Console.WriteLine(neurons[destnitionNeuron].value());
                    }
                }
            }

            if (printNetworkActivity)
            {
                Console.WriteLine("output neurons: ");
                for (int i = 0; i < outputLayer.Length; ++i)
                {
                    Console.WriteLine("output[" + i + "]: " + outputLayer[i].value());
                }

            }
        }
        public void networkDiagnos()
        {
            Console.WriteLine("///////////////");
            Console.WriteLine("diagnose");
            Console.WriteLine("This network has " + neurons.Count + " internal neurons and " + outputNeurons + " outputs");
            for (int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                Console.WriteLine("neuron " + thisNeuron + ". type: " + neuron.typeNames[neurons[thisNeuron].type()] + ", value: " + neurons[thisNeuron].value() + ", connections: " + neurons[thisNeuron].connections.Count);
                for (int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                {
                    Console.Write("\tconnection [" + thisConnection + "]. destenation: ");

                    int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                    
                    if(neurons[thisNeuron].connections[thisConnection].connectedToTheFinalLayer)
                    {
                        Console.Write("output [" + destnitionNeuron + "]");
                    }
                    else
                    {
                        Console.Write("neuron [" + destnitionNeuron + "]");
                    }
                    double thisValue = neurons[thisNeuron].value();
                    double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;

                    Console.WriteLine(". multiplycation: " + thisMultyplayer);
                    
                }
            }
            printOutputs(true);
            Console.WriteLine("///////////////");
        }
        public void printOutputs(bool doExactValue)
        {
            Console.Write("outputs: [");
            for (int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                if (doExactValue)
                {
                    Console.Write(outputLayer[thisOutput].value());
                }
                else
                {
                    Console.Write(outputLayer[thisOutput].isOn());
                }
                if (thisOutput < outputLayer.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }
        /*public void printOutput()
        {
            Console.WriteLine("Network Outputs:");
            for (int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                Console.WriteLine("Output [" + thisOutput + "]: " + outputLayer[thisOutput].value());
            }
        }*/
        public void clear(bool clearMemory)
        {
            for(int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                neurons[thisNeuron].clear();
            }
            for(int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                outputLayer[thisOutput].clear();
            }
        }
        public void clear()
        {
            clear(false);
        }
        public void reset()
        {
            clear(true);
        }
        public int strongestOutput()
        {
            int strongestOutputYet = 0;
            for(int thisOutput = 1; thisOutput < outputLayer.Length; ++thisOutput)
            {
                if(outputLayer[thisOutput].value() > outputLayer[strongestOutputYet].value())
                {
                    strongestOutputYet = thisOutput;
                }
            }
            return (strongestOutputYet);
        }
        public bool noOutput()
        {
            for(int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                if(outputLayer[thisOutput].isOn())
                {
                    return (false);
                }
            }
            return (true);
        }
        public void transformInto(network originNet)
        {
            neurons = originNet.neurons;
            outputLayer = originNet.outputLayer;
            inputNeurons = originNet.inputNeurons;
            outputNeurons = originNet.outputNeurons;
        }
        /*public void killEmptyConnections()
        {
            for(int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                for(int thisConnection = 0;thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
            }
        }*/

    }
}
