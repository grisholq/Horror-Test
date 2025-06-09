using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject interactionDot;

    public void SetInteractionDotVisibility(bool visible)
    {
        interactionDot.SetActive(visible);
    }
}