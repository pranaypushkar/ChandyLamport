// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="PranayPushkari">
//   Author: Pranay Pushkar 
// </copyright>
// <summary>
//   Defines the Node type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Node
{
    using System;
    using System.Collections.Generic;

    using ChandyLamport.Channel;

    /// <summary>
    /// The node.
    /// </summary>
    public class Node : INode
    {
        #region Private Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="nodeId">
        /// The node id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public Node(int nodeId, int value)
        {
            this.NodeId = nodeId;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        public int NodeId { get; private set; }

        public int Value { get; private set; } = 0;

        public bool NodeConnectedState { get; set; }

        public List<int> InboundNodes { get; set; }

        public List<int> OutboundNodes { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicating whether local state is recorded or not
        /// </summary>
        public bool IsLocalStateRecorded { get; set; }

        public bool IsMarkerReceived { get; set; } = false;

        public bool IsMarkerSent { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether is process initiator.
        /// </summary>
        public bool IsProcessInitiator { get; set; } = false;

        /// <inheritdoc />
        /// <summary>
        /// Gets the local state.
        /// </summary>
        public LocalState LocalState { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// The record local state.
        /// </summary>
        /// <returns>
        /// The <see cref="P:ChandyLamport.Node.Node.LocalState" />.
        /// </returns>
        public void RecordLocalState()
        {
            if (this.IsLocalStateRecorded == false)
            {
                this.IsLocalStateRecorded = true;
                this.LocalState = this.LocalState ?? new LocalState(this.NodeId, this.Value);
            }
        }

        public void RecordGobalState()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>
        /// The get local state.
        /// </summary>
        /// <param name="nodeId">
        /// The node id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void GetLocalState(out int nodeId, out int value)
        {
            nodeId = this.LocalState.GetLocalStateNodeId();
            value = this.LocalState.GetLocalStateValue();
        }

        /// <summary>
        /// The send marker.
        /// </summary>
        /// <param name="destinationNode">
        /// The destination Node.
        /// </param>
        public void SendMarker(INode destinationNode)
        {
            this.IsMarkerSent = true;
            var channel = new Channel(this.NodeId, destinationNode);
            channel.SendMarker();
        }

        public void SendValue(int value, INode destinationNode)
        {
            var channel = new Channel(this.NodeId, destinationNode);
            this.Value = this.Value - value;
            channel.SendValue(value);
        }

        /// <summary>
        /// The receive value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void ReceiveValue(int value)
        {
            this.Value = this.Value + value;
        }

        /// <summary>
        /// The receive marker.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        public void ReceiveMarker(object marker)
        {
            if (marker.ToString() == "Marker" && this.IsMarkerReceived == false)
            {
                this.IsMarkerReceived = true;
            }

            if (this.IsLocalStateRecorded == false)
            {
                this.RecordLocalState();
            }
        }

        #endregion
    }
}