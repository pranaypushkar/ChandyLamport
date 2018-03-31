// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalState.cs" company="PranayPushkar">
//    Author: Pranay Pushkar
// </copyright>
// <summary>
//   Defines the LocalState type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport
{
    /// <summary>
    /// Class to hold local state of the node.
    /// </summary>
    public class LocalState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalState"/> class.
        /// </summary>
        /// <param name="nodeId">
        /// The node id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public LocalState(int nodeId, int value)
        {
            this.NodeId = nodeId;
            this.Value = value;
        }

        /// <summary>
        /// Gets the value of a node.
        /// </summary>
        private int Value { get; }

        /// <summary>
        /// Gets the node id of the node.
        /// </summary>
        private int NodeId { get; }

        /// <summary>
        /// The get local state value.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetLocalStateValue()
        {
            return this.Value;
        }

        /// <summary>
        /// The get local state node id.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetLocalStateNodeId()
        {
            return this.NodeId;
        }
    }
}
