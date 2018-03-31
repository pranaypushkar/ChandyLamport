// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeFactory.cs" company="PranayPushkar">
//   Author:Pranay Pushkar 
// </copyright>
// <summary>
//   The Creator Abstract Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Node
{
    /// <summary>
    /// The NodeFactory Creator Abstract Class
    /// </summary>
    public abstract class NodeFactory
    {
        /// <summary>
        /// The get node.
        /// </summary>
        /// <param name="nodeId">
        /// The node id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="INode"/>.
        /// </returns>
        public abstract INode GetNode(int nodeId, int value);
    }
}