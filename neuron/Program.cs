﻿using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            network amazinetwork = new network(2, 1);
            amazinetwork.addNeuron(2, 0.5, 1);
            amazinetwork.addNeuron(2, 2, 2);
            amazinetwork.addNeuron(3, 0.3);
            amazinetwork.addNeuron();
            amazinetwork.Fire();
            //amazinetwork.networkDiagnos();
        }
            
    }

    class connection
    {
        public int destenation;
        public double multyplayer;
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
        public void activate()
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

        public neuron(int destention, double multypliiir, double initialValue): this(destention, multypliiir)
        {
            value = initialValue;
        }
        public void addConnection(int destnetion, double multyplaer)
        {
            connection emptyConnection = new connection(destnetion, multyplaer);
            connections.Add(emptyConnection);
        }


    }
    class network
    {
        static bool printNetworkActivity = true;
        public List<neuron> neurons = new List<neuron>();
        public int inputNeurons;
        public int outputNeurons;
        public network() { }
        public network(int inpNueurons, int outNeurons)
        {
            inputNeurons = inpNueurons;
            outputNeurons = outNeurons;
        }
        public void addNeuron()
        {
            neuron emptyNeuron = new neuron();
            neurons.Add(emptyNeuron);
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
            
                for (int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
                {
                    if (printNetworkActivity)
                    {
                        Console.WriteLine("  fureing neuron " + thisNeuron);
                    }
                    //networkDiagnos();//radical step
                    
                    for(int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                    {

                        int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                        double thisValue = neurons[thisNeuron].value;
                        double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;
                        if (printNetworkActivity)
                        {
                            Console.WriteLine("   fireing throught connection " + thisConnection);
                            Console.Write("  neuron " + thisNeuron +  " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". neuron " + destnitionNeuron  + " updated from " + neurons[destnitionNeuron].value + " to ");
                        }
                        neurons[destnitionNeuron].value = neurons[destnitionNeuron].value + thisValue * thisMultyplayer;
                        if (printNetworkActivity)
                        {
                            Console.WriteLine(neurons[destnitionNeuron].value);
                        }

                    }
                }
            
            if (printNetworkActivity)
            {
                Console.WriteLine("output neurons: ");
                for(int i = neurons.Count - outputNeurons; i < neurons.Count; ++i)
                {
                    Console.WriteLine("neuron " + i + " = " + neurons[i].value);
                }
                                
            }
        }
        public void networkDiagnos()
        {
            Console.WriteLine("///////////////");
            Console.WriteLine("diagnose");
            for (int thisNeuron = 0; thisNeuron < neurons.Count; ++thisNeuron)
            {
                    Console.WriteLine("  neuron " + thisNeuron);
                    Console.WriteLine("  this neuron contains a value of " + neurons[thisNeuron].value);
                    Console.WriteLine("  this neuron has " + neurons[thisNeuron].connections.Count + " connections");
                    for (int thisConnection = 0; thisConnection < neurons[thisNeuron].connections.Count; ++thisConnection)
                    {
                        Console.WriteLine("   connection " + thisConnection);

                        int destnitionNeuron = neurons[thisNeuron].connections[thisConnection].destenation;
                        Console.Write("   this connection goes to neuron " + destnitionNeuron);
                        double thisValue = neurons[thisNeuron].value;
                        double thisMultyplayer = neurons[thisNeuron].connections[thisConnection].multyplayer;

                        Console.WriteLine("   this connections multiplycation is " + thisMultyplayer);
                       
                    }
             }
            Console.WriteLine("///////////////");
        }
    }

}
