using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void OnLeftClick();
    public static event OnLeftClick LeftClickAction;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LeftClickAction?.Invoke();
        }
    }
}
