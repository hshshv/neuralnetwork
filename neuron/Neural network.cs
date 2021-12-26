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
        public double value = 0.0;
        public List<connection> connections = new List<connection>();
        public string name;
        public /*actually bool*/ void activate()
        {
            //later

        }

        public neuron(List<connection> cncs)
        {
            connections = cncs;
            for (int i = 0; i < cncs.Count; ++i)
            {
                connections[i] = cncs[i];
            }
        }
        public neuron(List<connection> cncs, string neuronName) : this(cncs)
        {
            name = neuronName;
        }

        public neuron() { }

        public neuron(int destention, double multypliiir)
        {
            connection myconn = new connection(destention, multypliiir);
            connections.Add(myconn);
        }

        public neuron(int destention, double multypliiir, double initialValue) : this(destention, multypliiir)
        {
            value = initialValue;
        }
        public void addConnection(int destnetion, double multyplaer)
        {
            connection newConnection = new connection(destnetion, multyplaer);
            connections.Add(newConnection);
        }


    }
    class network
    {
        const  bool printNetworkActivity = false;
        const bool doSigmoid = true;
        public List<neuron> neurons = new List<neuron>();
        public neuron[] outputLayer = new neuron[4];//צריך איכשהו להגדיר את זה דרך המתזמנן הכללי ולא מכאן
        public int inputNeurons;
        public int outputNeurons;
        public network() 
        {
            for(int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                outputLayer[thisOutput] = new neuron();
            }
        }
        public network(int inpNueurons, int outNeurons)
        {
            inputNeurons = inpNueurons;
            outputNeurons = outNeurons;
            outputLayer = new neuron[4];
        }

        public void addNeuron()
        {
            neurons.Add(new neuron());
        }
        public void addNeuron(int des, double mltp)
        {
            neuron newRon = new neuron(des, mltp);
            neurons.Add(newRon);
        }

        public void addNeuron(int des, double mltp, double initialValue)
        {
            neuron newRon = new neuron(des, mltp, initialValue);
            neurons.Add(newRon);
        }

        public void Fire()
        {           
            if (printNetworkActivity)
            {
                Console.WriteLine("fireing the network");
            }
            //outputLayer = new neuron[outputNeurons];
            for(int i = 0; i < outputLayer.Length; ++i)
            {
                outputLayer[i] = new neuron();
            }

            for (int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                if (printNetworkActivity)
                {
                    Console.WriteLine("  fureing neuron " + thisNeuron);
                }
                //networkDiagnos();//radical step
                if(doSigmoid)
                {
                    if(printNetworkActivity)
                        Console.Write("The value of neuron " + thisNeuron + " has been sigmofaid from " + neurons[thisNeuron].value);
                    neurons[thisNeuron].value = 1.0 / (1.0 + Math.Pow(Math.E, -neurons[thisNeuron].value));
                    if(printNetworkActivity)
                        Console.WriteLine(" to " + neurons[thisNeuron].value);
                }

                for (int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                {

                    int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                    double thisValue = neurons[thisNeuron].value;
                    double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;
                    if(printNetworkActivity)
                        Console.WriteLine("   fireing throught connection " + thisConnection);
                    if(neurons[thisNeuron].connections[thisConnection].connectedToTheFinalLayer)
                    {
                        if (printNetworkActivity)
                            Console.Write("  neuron " + thisNeuron + " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". output " + destnitionNeuron + " updated from " + outputLayer[destnitionNeuron].value + " to ");
                        outputLayer[destnitionNeuron].value = outputLayer[destnitionNeuron].value + thisValue * thisMultyplayer;
                        if (printNetworkActivity)
                            Console.WriteLine(outputLayer[destnitionNeuron].value);
                    }
                    else
                    {
                        if (printNetworkActivity)
                            Console.Write("  neuron " + thisNeuron + " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". neuron " + destnitionNeuron + " updated from " + neurons[destnitionNeuron].value + " to ");
                        
                        neurons[destnitionNeuron].value = neurons[destnitionNeuron].value + thisValue * thisMultyplayer;
                        if (printNetworkActivity)
                            Console.WriteLine(neurons[destnitionNeuron].value);
                    }
                }
            }

            if (printNetworkActivity)
            {
                Console.WriteLine("output neurons: ");
                for (int i = 0; i < outputLayer.Length; ++i)
                {
                    Console.WriteLine("output[" + i + "]: " + outputLayer[i].value);
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
                Console.WriteLine("  neuron " + thisNeuron);
                Console.WriteLine("  this neuron contains a value of " + neurons[thisNeuron].value);
                Console.WriteLine("  this neuron has " + neurons[thisNeuron].connections.Count + " connections");
                for (int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                {
                    Console.WriteLine("   connection " + thisConnection);

                    int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                    
                    if(neurons[thisNeuron].connections[thisConnection].connectedToTheFinalLayer)
                    {
                        Console.Write("   this connection goes to output number " + destnitionNeuron);
                    }
                    else
                    {
                        Console.Write("   this connection goes to neuron " + destnitionNeuron);
                    }
                    double thisValue = neurons[thisNeuron].value;
                    double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;

                    Console.WriteLine("   this connections multiplycation is " + thisMultyplayer);

                }
            }
            Console.WriteLine("Network Outputs:");
            for(int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                Console.WriteLine("Output [" + thisOutput + "]: " + outputLayer[thisOutput].value);
            }
            Console.WriteLine("///////////////");
        }
        public void clear()
        {
            for(int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                neurons[thisNeuron].value = 0;
            }
            for(int thisOutput = 0; thisOutput < outputLayer.Length; ++thisOutput)
            {
                outputLayer[thisOutput].value = 0;
            }
        }

        public int strongestOutput()
        {
            int strongestOutputYet = 0;
            for(int thisOutput = 1; thisOutput < outputLayer.Length; ++thisOutput)
            {
                if(outputLayer[thisOutput].value > outputLayer[strongestOutputYet].value)
                {
                    strongestOutputYet = thisOutput;
                }
            }
            return (strongestOutputYet);
        }
    }
}
