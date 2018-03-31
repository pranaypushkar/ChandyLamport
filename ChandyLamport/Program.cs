// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="PranayPushkar">
//   Author: Pranay Pushkar
// </copyright>
// <summary>
//   Implementation of Chandy Lamport in C# V1.0
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using ChandyLamport.Node;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            NodeFactory[] nodeFactory = new NodeFactory[3];
            INode[] nodes = new INode[3];

            InitializeNodes(nodeFactory, nodes);
            Console.WriteLine("\nPress any key to start node communication !");
            Console.ReadLine();

            // Process for Node communication
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("Node communication started!\n");
            Console.WriteLine("--------------------------------------\n");
            Random rnd = new Random();
            while (true)
            {
                if (ExecuteNodeCommunication(rnd, nodes))
                {
                    continue;
                }

                // Process for Snapshot recording
                if (!(rnd.NextDouble() > 0.6))
                {
                    continue;
                }

                Console.WriteLine("\n--------------------------------------");
                Console.WriteLine("\nState recording started !\n\n");
                Console.WriteLine("--------------------------------------\n");

                // Randomly select a node to start snapshot recording
                int snapshotInitiatorNode = rnd.Next(1, nodes.Length + 1);
                nodes[snapshotInitiatorNode - 1].IsProcessInitiator = true;
                nodes[snapshotInitiatorNode - 1].IsMarkerReceived = true;
                nodes[snapshotInitiatorNode - 1].RecordLocalState();
                SendMarker(nodes[snapshotInitiatorNode - 1], nodes);

                int count = nodes.Count(node => node.IsMarkerReceived);

                if (count == nodes.Length)
                {
                    Console.WriteLine("\nLocal Snapshot\n");
                }

                Console.WriteLine("\n--------------------------------------");
                int value = 0;
                foreach (var node in nodes)
                {
                    value += node.Value;
                    Console.WriteLine($"Local Snapshot[Node {node.LocalState.GetLocalStateNodeId()} " + 
                                      $"has value { node.LocalState.GetLocalStateValue() }]");
                }

                Console.WriteLine($"\nTotal Value = {value}\n");
                Console.WriteLine("--------------------------------------\n");
            }
        }

        /// <summary>
        /// The send marker.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        private static void SendMarker(INode node, INode[] nodes)
        {
            foreach (var nodeId in node.OutboundNodes)
            {
                node.SendMarker(nodes[nodeId - 1]);
                foreach (var remainingNode in nodes)
                {
                    if (remainingNode.IsMarkerReceived == true && 
                        remainingNode.IsLocalStateRecorded == true && 
                        remainingNode.IsMarkerSent == false)
                    {
                        SendMarker(remainingNode, nodes);
                    }
                }
            }
        }

        /// <summary>
        /// The execute node communication.
        /// </summary>
        /// <param name="rnd">
        /// The random instance.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool ExecuteNodeCommunication(Random rnd, INode[] nodes)
        {
            // Node Communication
            int getInitiatorNode = rnd.Next(1, nodes.Length + 1);
            int destinationNodeId = rnd.Next(1, nodes.Length + 1);
            if (getInitiatorNode == destinationNodeId)
            {
                return true;
            }

            int value = rnd.Next(1, nodes[getInitiatorNode - 1].Value);
            nodes[getInitiatorNode - 1].SendValue(value, nodes[destinationNodeId - 1]);

            Console.WriteLine($"Node {getInitiatorNode} transferred value {value} to Node {destinationNodeId}.");
            return false;
        }

        /// <summary>
        /// The initialize nodes.
        /// </summary>
        /// <param name="nodeFactory">
        /// The node factory.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        private static void InitializeNodes(NodeFactory[] nodeFactory, INode[] nodes)
        {
            var nodesXmlDocument = ReadNodeNetworkXml();

            for (int i = 0; i <= nodesXmlDocument.ChildNodes.Count; i++)
            {
                nodeFactory[i] = new ConcreteNodeFactory();
            }

            Console.WriteLine("pranaypushkar@bitspilani>>\nCreating Nodes...\n");

            XmlNodeList nodeList = nodesXmlDocument.SelectNodes("/Nodes/Node");
            int netValue = 0;
            for (int i = 0; i <= nodesXmlDocument.ChildNodes.Count; i++)
            {
                nodes[i] = nodeFactory[i].GetNode(
                    int.Parse(nodeList[i]["NodeID"].InnerText), 
                    int.Parse(nodeList[i]["NodeValue"].InnerText));
                netValue += int.Parse(nodeList[i]["NodeValue"].InnerText);
                Console.WriteLine(
                    $" Node {i + 1} created with Id = {nodes[i].NodeId} and " + 
                    $"Node Value = {nodes[i].Value} at {GetTime()}");
                nodes[i].InboundNodes = new List<int>();
                nodes[i].OutboundNodes = new List<int>();
                XmlNodeList inboundNodeList = nodesXmlDocument.SelectNodes("/Nodes/Node/InBoundNodes");
                foreach (XmlElement item in inboundNodeList[i])
                {
                    for (int j = 0; j < item.ChildNodes.Count; j++)
                    {
                        nodes[i].InboundNodes.Add(int.Parse(item.ChildNodes[j].InnerText));
                    }
                }

                XmlNodeList outboundNodeList = nodesXmlDocument.SelectNodes("/Nodes/Node/OutBoundNodes");
                foreach (XmlElement item in outboundNodeList[i])
                {
                    for (int j = 0; j < item.ChildNodes.Count; j++)
                    {
                        nodes[i].OutboundNodes.Add(int.Parse(item.ChildNodes[j].InnerText));
                    }
                }
            }

            Console.WriteLine($"\nTotal Nodes created = {nodesXmlDocument.ChildNodes.Count + 1} " + 
                              $"with net value {netValue}.");
        }

        /// <summary>
        /// Method to get the time
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetTime()
        {
            return DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Method to read node network XML.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlDocument"/>.
        /// </returns>
        private static XmlDocument ReadNodeNetworkXml()
        {
            File.SetAttributes(@"..\..\Resources\NodeNetwork.xml", FileAttributes.Normal);

            XmlDocument nodesXmlDocument = new XmlDocument();
            nodesXmlDocument.Load(@"..\..\Resources\NodeNetwork.xml");
            return nodesXmlDocument;
        }
    }
}
