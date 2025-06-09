using UnityEngine;

public class PlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
    public Vector2 Movement => new Vector2(Horizontal, Vertical);
    public bool LMBDown => Input.GetMouseButtonDown(0);
    public bool LMBUp => Input.GetMouseButtonUp(0);
}
