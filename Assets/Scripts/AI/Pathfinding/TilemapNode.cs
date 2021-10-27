using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapNode : INode {
    private Vector2 position;
    private HashSet<INode> neighbors = new HashSet<INode>();

    public TilemapNode(Vector2 position) {
        this.position = position;
    }

    public Vector2 GetPosition() {
        return this.position;
    }

    public void AddNeighbor(TilemapNode node) {
        this.neighbors.Add(node);
    }

    public void AddNeighbor(INode node) {
        this.neighbors.Add(node);
    }

    public HashSet<INode> GetNeighbors() {
        return this.neighbors;
    }

    public bool Equals(INode node) {
        if(this == node) return true;
        if(this.position == node.GetPosition()) return true;

        return false;
    }
}
