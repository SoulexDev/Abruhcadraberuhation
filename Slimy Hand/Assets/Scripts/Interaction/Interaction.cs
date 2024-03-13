using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Interactable _interactable;
    [SerializeField] private Interactable interactable
    {
        get { return _interactable; }
        set
        {
            if (heldInteractable != value)
            {
                UpInteract();
            }
            _interactable = value;
        }
    }
    [SerializeField] private Interactable heldInteractable;
    [SerializeField] private TextMeshProUGUI interactText;
    //[SerializeField] private CrosshairAnimator crosshairAnimator;

    private void Update()
    {
        if (!Player.Instance.canInteract)
        {
            interactText.text = "";
            return;
        }
        RaycastCalculation();
        PlayerInput();
    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DownInteract();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            UpInteract();
        }
        //if (heldInteractable != null && !heldInteractable.isInteractable)
        //{
        //    UpInteract();
        //}
    }
    void DownInteract()
    {
        if (interactable != null)
        {
            if (interactable.interactionType == InteractionType.Press)
                interactable.Interact();
            else
            {
                heldInteractable = interactable;
                interactable.StartInteraction();
            }
        }
    }
    void UpInteract()
    {
        if (interactable != null && interactable == heldInteractable)
        {
            interactable.EndInteraction();
            heldInteractable = null;
        }
    }
    void RaycastCalculation()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, 5, ~LayerMask.GetMask("Player", "Ignore Raycast")))
        {
            if (hit.transform.TryGetComponent(out Interactable newInteractable))
                interactable = newInteractable;
            else
                interactable = null;
        }
        else
            interactable = null;

        if (interactable != null && (hit.distance > 2.5f || !interactable.isInteractable))
            interactable = null;

        interactText.text = interactable != null ? interactable.interactMessage : "";
    }
}
public enum InteractionType { Press, Hold }