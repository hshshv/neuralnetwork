using System;
using System.Collections.Generic;


namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            connection conn1 = new connection(0, 0.5);
            connection[] conns1 = {conn1};
            neuron nrn1 = new neuron(conns1);
            nrn1.input = 0.8;

            connection conn2 = new connection(0, 0.3);
            connection[] conns2 = { conn2 };
            neuron nrn2 = new neuron(conns2);
            nrn2.input = 0.7;

            neuron[] nrns1 = {nrn1, nrn2};

            layer lyr1 = new layer(nrns1);
            //

            connection conn3 = new connection(0, 2);
            connection[] conns3 = { conn3 };
            neuron nrn3 = new neuron(conns3);
            nrn3.input = 0.0;
            neuron[] nrns2 = { nrn3 };

            layer lyr2 = new layer(nrns2);
            //

            connection conn4 = new connection(0, 0);//finel neuron
            connection[] conns4 = { conn4 };
            neuron nrn4 = new neuron(conns4);
            nrn4.input = 0.0;
            neuron[] nrns3 = { nrn4 };

            layer lyr3 = new layer(nrns3);
            //


            layer[] lyrs = { lyr1, lyr2, lyr3 };
            network themostamazingnetwork = new network(lyrs);
            ///
            themostamazingnetwork.Fire();
            Console.WriteLine("finel layer: ");
            themostamazingnetwork.layers[themostamazingnetwork.layers.Length - 1].printLayer();
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
    }
    class neuron
    {
        public double input = 0.0;
        public connection[] connections;
        public void activate()
        {
            //later
        }

        
        public neuron(connection[] cncs)
        {
            connections = new connection[cncs.Length];
            for(int i = 0; i < cncs.Length; ++i)
            {
                connections[i] = cncs[i];
            }
        }
        
    }

    class layer
    {
        public neuron[] neurons;
        public void printLayer()
        {
            foreach(neuron i in neurons)
            {
                Console.WriteLine("neuron: " + i.input);
            }
        }
        public layer(neuron[] nrns)
        {
            neurons = new neuron[nrns.Length];
            for (int i = 0; i < nrns.Length; ++i)
            {
                neurons[i] = nrns[i];
            }
        }
    }

    class network
    {
        static bool printNetworkActivity = false;
        public layer[] layers;
        public void Fire()
        {
            if (printNetworkActivity)
            {
                Console.WriteLine("fireing the network");
            }
            for(int thisLayer = 0; thisLayer < layers.Length - 1; ++thisLayer)
            {
                if (printNetworkActivity)
                {
                    Console.WriteLine(" fireing layer " + thisLayer);
                }
                for (int thisNeuron = 0; thisNeuron < layers[thisLayer].neurons.Length; ++thisNeuron)
                {
                    if (printNetworkActivity)
                    {
                        Console.WriteLine("  fureing neuron " + thisNeuron);
                    }
                    //networkDiagnos();//radical step
                    
                    for(int thisConnection = 0; thisConnection < layers[thisLayer].neurons[thisNeuron].connections.Length; ++thisConnection)
                    {

                        int destnitionNeuron = layers[thisLayer].neurons[thisNeuron].connections[thisConnection].destenation;
                        double thisValue = layers[thisLayer].neurons[thisNeuron].input;
                        double thisMultyplayer = layers[thisLayer].neurons[thisNeuron].connections[thisConnection].multyplayer;
                        if (printNetworkActivity)
                        {
                            Console.WriteLine("   fireing throught connection " + thisConnection);
                            Console.Write("  neuron " + thisNeuron + " in layer " + thisLayer + " outputs " + thisValue + " multyplyed by " + thisMultyplayer + ". neuron " + destnitionNeuron + " in layer " + (thisLayer + 1) + " updated from " + layers[thisLayer + 1].neurons[destnitionNeuron].input + " to ");
                        }
                        layers[thisLayer + 1].neurons[destnitionNeuron].input = layers[thisLayer + 1].neurons[destnitionNeuron].input + thisValue * thisMultyplayer;
                        if (printNetworkActivity)
                        {
                            Console.WriteLine(layers[thisLayer + 1].neurons[destnitionNeuron].input);
                        }

                    }
                }
            }
            if (printNetworkActivity)
            {
                Console.WriteLine("finel layer: ");
                layers[layers.Length - 1].printLayer();
            }
        }
        public void networkDiagnos()
        {
            Console.WriteLine("///////////////");
            Console.WriteLine("diagnose");
            Console.WriteLine("this network has " + layers.Length + " layers");
            for (int thisLayer = 0; thisLayer < layers.Length; ++thisLayer)
            {
                Console.WriteLine(" layer " + thisLayer);
                Console.WriteLine(" this layer has " + layers[thisLayer].neurons.Length + " neurons");
                for (int thisNeuron = 0; thisNeuron < layers[thisLayer].neurons.Length; ++thisNeuron)
                {
                    Console.WriteLine("  neuron " + thisNeuron);
                    Console.WriteLine("  this neuron contains a value of " + layers[thisLayer].neurons[thisNeuron].input);
                    Console.WriteLine("  this neuron has " + layers[thisLayer].neurons[thisNeuron].connections.Length + " connections");
                    for (int thisConnection = 0; thisConnection < layers[thisLayer].neurons[thisNeuron].connections.Length; ++thisConnection)
                    {
                        Console.WriteLine("   connection " + thisConnection);

                        int destnitionNeuron = layers[thisLayer].neurons[thisNeuron].connections[thisConnection].destenation;
                        Console.Write("   this connection goes to neuron " + destnitionNeuron);
                        Console.WriteLine(" in layer " + (thisLayer + 1));
                        double thisValue = layers[thisLayer].neurons[thisNeuron].input;
                        double thisMultyplayer = layers[thisLayer].neurons[thisNeuron].connections[thisConnection].multyplayer;

                        Console.WriteLine("   this connections multiplycation is " + thisMultyplayer);
                       
                    }
                }
            }
            Console.WriteLine("///////////////");
        }
        public network(layer[] lyrs)
        {
            layers = new layer[lyrs.Length];
            for(int i = 0; i < lyrs.Length; ++i)
            {
                layers[i] = lyrs[i];
            }
        }
    }

}
