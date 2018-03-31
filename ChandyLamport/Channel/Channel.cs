// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="PranayPushkar">
//   Author: Pranay Pushkar 
// </copyright>
// <summary>
//   Defines the Channel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Channel
{
    using System;
    using System.Collections;
    using System.Threading;

    using ChandyLamport.Node;

    /// <inheritdoc />
    /// <summary>
    /// The channel.
    /// </summary>
    public class Channel : IChannel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="sourceNodeId">
        /// The source node id.
        /// </param>
        /// <param name="destinationNode">
        /// The destination node.
        /// </param>
        public Channel(int sourceNodeId, INode destinationNode)
        {
            this.SourceNodeId = sourceNodeId;
            this.DestinationNode = destinationNode;
        }

        /// <summary>
        /// Gets the source node id.
        /// </summary>
        private int SourceNodeId { get; }

        /// <summary>
        /// Gets the destination node.
        /// </summary>
        private INode DestinationNode { get; }

        /// <inheritdoc />
        /// <summary>
        /// The send value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SendValue(int value)
        {
            Queue fifoChannel = new Queue();
            fifoChannel.Enqueue(value);

            // Add random latency to simulate fifo channel
            Thread.Sleep(new Random().Next(100, 1000));
            this.DestinationNode.ReceiveValue(Convert.ToInt32(fifoChannel.Dequeue()));
        }

        /// <inheritdoc />
        /// <summary>
        /// The send marker.
        /// </summary>
        public void SendMarker()
        {
            Queue fifoChannel = new Queue();
            fifoChannel.Enqueue("Marker");

            // Add random latency to simulate fifo channel
            Thread.Sleep(new Random().Next(100, 1000));
            this.DestinationNode.ReceiveMarker(fifoChannel.Dequeue());
        }
    }
}
