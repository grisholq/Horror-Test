using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image interactionDot;

    public void SetInteractionDotActivity(bool active)
    {
        if (active)
        {
            interactionDot.color = SetColorAlpha(interactionDot.color, 1);
        }
        else
        {
            interactionDot.color = SetColorAlpha(interactionDot.color, 0.3f);
        }
    }

    private Color SetColorAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}