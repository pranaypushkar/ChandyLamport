// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChannel.cs" company="PranayPushkar">
//    Author: Pranay Pushkar  
// </copyright>
// <summary>
//   Defines the IChannel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChandyLamport.Channel
{
    /// <summary>
    /// The Channel interface.
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// The send value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        void SendValue(int value);

        /// <summary>
        /// The send marker.
        /// </summary>
        void SendMarker();
    }
}