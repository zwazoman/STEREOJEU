using UnityEngine;

public class ScoreFeedback : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3;

    float _timer;

    private void Start()
    {
        print(transform.position);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
            Destroy(gameObject);
    }
}
