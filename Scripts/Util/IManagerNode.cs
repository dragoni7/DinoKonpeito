using Godot;
using System;

public interface IManagerNode<T>
{
    /// <summary>
    /// Gets an instance of the node in the tree. Nodes that are frequently called upon (like managers) should implement this interface.
    /// </summary>
    /// <typeparam name="T">The node type</typeparam>
    /// <param name="from">Node to call from</param>
    /// <returns>the node instance</returns>
    /// <exception cref="NotImplementedException">method not implemented</exception>
    public static T GetInstance(Node from) => throw new NotImplementedException();
}
