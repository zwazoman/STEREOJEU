using System.Collections.Generic;
using UnityEngine;

public class particleStackPlayer : MonoBehaviour
{
    public List<ParticleSystem> _stack;

    public void PopAndPlay()
    {
        var system = _stack[_stack.Count - 1];
        system.Play();
        _stack.RemoveAt(_stack.Count - 1);
    }
    
}
