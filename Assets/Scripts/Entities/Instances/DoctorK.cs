using UnityEngine;

public class DoctorK : StateHuman, IInteractable {
    [SerializeField] private bool hooded = false;

    protected override void Start() {
        base.Start();

        EventManager.Get().Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    private void OnToggle(ToggleEvent toggleEvent) {
        if(toggleEvent.GetTarget().Equals(transform)) return;
        hooded = !hooded;
        UpdateTextures();
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    public bool IsInteractable() {
        return true;
    }

    protected override string GetTextureName() {
        return hooded ? "hooded" : "drk";
    }

    public override float GetSpeed() {
        return 0.8f;
    }
}
