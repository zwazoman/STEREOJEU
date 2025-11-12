using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    void OnValidate()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Play()
    {
        _particleSystem.Play();
    }
    
}
