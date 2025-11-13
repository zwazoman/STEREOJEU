using System.Collections.Generic;
using UnityEngine;

public class particleStackPlayer : MonoBehaviour
{
    public List<ParticleSystem> _stack;

    public void PopAndPlay()
    {
        var system = _stack[0];
        system.Play();
        _stack.RemoveAt(0);
    }
    
}
