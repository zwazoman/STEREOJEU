using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class FirstQTE : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private SwipeInteraction _interactableItem;
    [SerializeField] private QTEManager _managerQTE;
    [SerializeField] private QTECreator _creatorQTE;

    public bool FailQTE;

    private void Start()
    {
        StartGame().Forget();
    }

    private async UniTask StartGame()
    {
        await UniTask.Delay(1);
        _playableDirector.Pause();

        _interactableItem.Activate();
        _creatorQTE.CreateQTE(0, _interactableItem, true).Forget();

        await UniTask.WaitUntil(() => _interactableItem.SuccesSwipe);
        print("good");
        _interactableItem.Deactivate();

        FmodCallbacks.Instance.StartMusic();

        //_managerQTE.StartQTESystem();

        _playableDirector.Play();
    }
}
