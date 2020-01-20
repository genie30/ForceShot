using UnityEngine;
using UniRx;
using UniRx.Triggers;

// 穴のコリジョン監視
public class HoleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    private void Start()
    {
        this.OnCollisionEnterAsObservable().Where(x => x.collider.tag == "Player").TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                GameManager.stateRP.Value = GameState.HoleIn;
                particle.Stop();
            });
    }
}
