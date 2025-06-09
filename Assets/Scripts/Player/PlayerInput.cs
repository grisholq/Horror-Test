using UnityEngine;

public class PlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
    public Vector2 Movement => new Vector2(Horizontal, Vertical);
}
