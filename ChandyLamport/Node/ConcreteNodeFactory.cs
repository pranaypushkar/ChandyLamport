// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcreteNodeFactory.cs" company="PranayPushkar">
//   Author: Pranay Pushkar
// </copyright>
// <summary>
//   Defines the ConcreteNodeFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Node
{
    /// <inheritdoc />
    /// <summary>
    /// The concrete node factory.
    /// </summary>
    public class ConcreteNodeFactory : NodeFactory
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
        public override INode GetNode(int nodeId, int value)
        {
           return new Node(nodeId, value);
        }
    }
}