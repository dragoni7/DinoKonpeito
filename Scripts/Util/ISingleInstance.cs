using Godot;
using System;

public interface ISingleInstance<T> where T : Node
{
    public const string _defaultPath = "/root/Game/";

    /// <summary>
    /// Gets an instance of the node in the tree. Assumes the node only has one instance (like autoloads).
    /// </summary>
    /// <typeparam name="T">The node type</typeparam>
    /// <param name="from">Node to call from</param>
    /// <returns>the node instance</returns>
    /// <exception cref="NotImplementedException">method not implemented</exception>
    public static T GetInstance(Node from) => throw new NotImplementedException();
}
