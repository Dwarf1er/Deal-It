using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPannel : MonoBehaviour, ISubscriber {
    public string message;
    protected Text text;
    private Vector3 offsetPosition;
    private Vector3 basePosition;
    private bool open = false;
    private readonly float STEPS = 100;

    protected virtual void Start() {
        this.text = GetComponentInChildren<Text>();
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }

    protected abstract Vector3 GetOffset();

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }

    public bool IsOpen() {
        return this.open;
    }

    public void Open() {
        this.basePosition = this.transform.position;
        this.offsetPosition = this.transform.position + this.GetOffset();

        StartCoroutine(AnimateOpen());
    }

    public void Close() {
        if(!this.open) return;

        this.open = false;
        StartCoroutine(AnimateClose());
    }

    private IEnumerator AnimateOpen() {
        transform.position = offsetPosition;

        for(float i = 0; i <= STEPS; i++) {
            transform.position = Vector3.Lerp(transform.position, basePosition, i / STEPS);
            yield return new WaitForSeconds(0.001f);
        }

        this.open = true;
    }

    private IEnumerator AnimateClose() {
        for(float i = 0; i <= STEPS; i++) {
            transform.position = Vector3.Lerp(transform.position, offsetPosition, i / STEPS);
            yield return new WaitForSeconds(0.001f);
        }

        Destroy(this.gameObject);
    }
}