using System;
using UnityEngine;

public class InputService
{
    public Action OnTouched;

    public void IsTouching()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            OnTouched?.Invoke();
        }
    }
}
