using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractHuman : MonoBehaviour, ISubscriber, IWithPosition, IWithTransform {
    public float speed = 1.0f;
    public string textureName = "template";
    public Vector2 direction;
    protected Sprite[] sprites;
    private int spriteIndex;
    private float flipTime;
    protected SpriteRenderer spriteRenderer;

    public virtual void Start() {
        this.sprites = Resources.LoadAll<Sprite>("Characters/" + textureName);
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public Vector2 GetPosition() {
        return this.transform.position;
    }

    public void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return true;
    }

    public Transform GetTransform() {
        return this.transform;
    }

    public float DistanceTo(GameObject gameObject) {
        return this.DistanceTo(gameObject.transform);
    }

    public float DistanceTo(Transform transform) {
        return this.DistanceTo(transform.position);
    }

    public float DistanceTo(Vector2 target) {
        return this.DistanceTo(new Vector3(target.x, target.y, 0));
    }

    public float DistanceTo(Vector3 target) {
        return Vector2.Distance(this.transform.position, target);
    }

    private int GetDirectionIndex(Vector2 direction) {
        if(direction.magnitude == 0) return -1;

        float angle = Vector2.SignedAngle(Vector2.right, direction) + 180.0f;

        if(angle >= 45 && angle < 135) {
            return 0;
        } else if(angle >= 135 && angle < 225) {
            return 2;
        } else if(angle >= 225 && angle < 315) {
            return 1;
        } else {
            return 3;
        }
    }

    public void LookAt(Transform transform) {
        LookAt((transform.position - this.transform.position).normalized);
    }

    public void LookAt(Vector2 direction) {
        if(direction == Vector2.zero) return;

        int directionIndex = this.GetDirectionIndex(direction);

        switch(directionIndex) {
            case 0:
            case 1:
                this.spriteIndex = directionIndex;
                break;
            case 2:
            case 3:
                if(this.spriteIndex < 2) this.spriteIndex = 3;
                this.spriteRenderer.flipX = directionIndex == 2;
                break;
        }
    }

    public virtual void Update() {
        int directionIndex = this.GetDirectionIndex(direction);

        LookAt(this.direction);

        flipTime += Time.deltaTime;

        if (flipTime > 0.175f) {
            switch(directionIndex) {
                case 0:
                case 1:
                    this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
                    break;
                case 2:
                case 3:
                    this.spriteIndex = this.spriteIndex == 3 ? 2 : 3;
                    break;
            }
            flipTime = 0;
        }

        if(this.spriteIndex >= 0) this.spriteRenderer.sprite = this.sprites[this.spriteIndex];
    }
}
