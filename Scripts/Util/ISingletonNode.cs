using Godot;
using System;

public interface ISingletonNode<T>
{
    /// <summary>
    /// Gets the singleton instance of the node in the tree. Scripts attatched to autoload or single instance nodes should implement this interface.
    /// </summary>
    /// <typeparam name="T">The singleton type</typeparam>
    /// <param name="from">Node to call from</param>
    /// <returns>the singleton instance</returns>
    /// <exception cref="NotImplementedException">method not implemented</exception>
    public static T GetInstance(Node from) => throw new NotImplementedException();
}
