using UnityEngine;
using UnityEngine.InputSystem;

public class jenpeuxplus : MonoBehaviour
{
    #if UNITY_EDITOR

    private bool ff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ff |= Keyboard.current.rightArrowKey.wasPressedThisFrame;
        ff &= !Keyboard.current.rightArrowKey.wasReleasedThisFrame;
        
        Time.timeScale = ff ? 5 : 1;
    }
    
    #endif
}
