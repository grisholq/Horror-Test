using Zenject;
using UnityEngine;

public class EntryPoint : IInitializable
{
    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
