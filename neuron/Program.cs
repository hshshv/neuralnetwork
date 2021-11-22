using System;
using System.Collections.Generic;



namespace neuronprog
{
    class Program
    {
        static void Main(string[] args)
        {
            network amazinetwork = new network();
            amazinetwork.addLayer();
            amazinetwork.layers[0].addNeuron(0, 0.2);
            amazinetwork.layers[0].neurons[0].input = 0.6;
            amazinetwork.layers[0].addNeuron(0, 4.0);
            amazinetwork.layers[0].neurons[1].input = 0.2;
            amazinetwork.addLayer();
            amazinetwork.layers[1].addNeuron(0, 0.7);
            amazinetwork.addLayer();
            amazinetwork.layers[2].addNeuron();//צריך לעשות שבכל שכבה יהיה אוטומטית ניורון אחד או משהו כזה

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
        public double input = 0.0;
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
        public void addConnection(int destnetion, double multyplaer)
        {
            connection emptyConnection = new connection(destnetion, multyplaer);
            connections.Add(emptyConnection);
        }


    }

    class layer
    {
        public List<neuron> neurons = new List<neuron>() ;
        public void printLayer()
        {
            foreach(neuron i in neurons)
            {
                Console.WriteLine("neuron: " + i.input);
            }
        }
        public layer(List<neuron> nrns)
        {
            neurons = nrns;
            for (int i = 0; i < nrns.Count; ++i)
            {
                neurons[i] = nrns[i];
            }
        }

        public layer() 
        {
            
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
    }

    class network
    {
        static bool printNetworkActivity = true;
        public List<layer> layers = new List<layer>();
        public void Fire()
        {
            if (printNetworkActivity)
            {
                Console.WriteLine("fireing the network");
            }
            for(int thisLayer = 0; thisLayer < layers.Count - 1; ++thisLayer)
            {
                if (printNetworkActivity)
                {
                    Console.WriteLine(" fireing layer " + thisLayer);
                }
                for (int thisNeuron = 0; thisNeuron < layers[thisLayer].neurons.Count; ++thisNeuron)
                {
                    if (printNetworkActivity)
                    {
                        Console.WriteLine("  fureing neuron " + thisNeuron);
                    }
                    //networkDiagnos();//radical step
                    
                    for(int thisConnection = 0; thisConnection < layers[thisLayer].neurons[thisNeuron].connections.Count; ++thisConnection)
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
                layers[layers.Count - 1].printLayer();
                
            }
        }
        public void networkDiagnos()
        {
            Console.WriteLine("///////////////");
            Console.WriteLine("diagnose");
            Console.WriteLine("this network has " + layers.Count + " layers");
            for (int thisLayer = 0; thisLayer < layers.Count; ++thisLayer)
            {
                Console.WriteLine(" layer " + thisLayer);
                Console.WriteLine(" this layer has " + layers[thisLayer].neurons.Count + " neurons");
                for (int thisNeuron = 0; thisNeuron < layers[thisLayer].neurons.Count; ++thisNeuron)
                {
                    Console.WriteLine("  neuron " + thisNeuron);
                    Console.WriteLine("  this neuron contains a value of " + layers[thisLayer].neurons[thisNeuron].input);
                    Console.WriteLine("  this neuron has " + layers[thisLayer].neurons[thisNeuron].connections.Count + " connections");
                    for (int thisConnection = 0; thisConnection < layers[thisLayer].neurons[thisNeuron].connections.Count; ++thisConnection)
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
            layers = new List<layer>();
            for(int i = 0; i < lyrs.Length; ++i)
            {
                layers[i] = lyrs[i];
            }
        }
        public network()  {  }
        public void addLayer()
        {
            layer emptylyr = new layer();
            layers.Add(emptylyr);
        }
    }

}
