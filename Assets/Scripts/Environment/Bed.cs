using UnityEngine;

public class Bed : MonoBehaviour, IInteractable {
    public bool IsInteractable() {
        return true;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }
}
