// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INode.cs" company="PranayPushkar">
//   Author: Pranay Pushkar 
// </copyright>
// <summary>
//   Defines the INode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Node
{
    using System.Collections.Generic;

    /// <summary>
    /// The Node interface.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Gets the node id.
        /// </summary>
        int NodeId { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        int Value { get; }

        /// <summary>
        /// Gets the local state.
        /// </summary>
        LocalState LocalState { get; }

        /// <summary>
        /// Gets a value indicating whether a node is alive or not
        /// </summary>
        bool NodeConnectedState { get; }

        /// <summary>
        /// Gets or sets list of nodes connected with inbound connections
        /// </summary>
        List<int> InboundNodes { get; set; }

        /// <summary>
        /// Gets or sets list of nodes connected with outbound connections 
        /// </summary>
        List<int> OutboundNodes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether local state is recorded or not
        /// </summary>
        bool IsLocalStateRecorded { get; set; }

        bool IsMarkerReceived { get; set; }

        bool IsMarkerSent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is process initiator.
        /// </summary>
        bool IsProcessInitiator { get; set; }

        /// <summary>
        /// The receive marker.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        void ReceiveMarker(object marker);

        /// <summary>
        /// The send value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="destinationNode">
        /// The destination node.
        /// </param>
        void SendValue(int value, INode destinationNode);

        /// <summary>
        /// The receive value.
        /// </summary>
        /// <param name="valueFromInboundNode">
        /// The value from inbound node.
        /// </param>
        void ReceiveValue(int valueFromInboundNode);

        /// <summary>
        /// The send marker.
        /// </summary>
        /// <param name="destinationNode">
        /// The destination Node.
        /// </param>
        void SendMarker(INode destinationNode);

        /// <summary>
        /// The get local state.
        /// </summary>
        /// <param name="nodeId">
        /// The node id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void GetLocalState(out int nodeId, out int value);

        /// <summary>
        /// The record local state.
        /// </summary>
        void RecordLocalState();

        void RecordGobalState();
    }
}