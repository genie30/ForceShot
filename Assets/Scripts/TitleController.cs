using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.U2D;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Image title, sub;

    private void Start()
    {
        var sa = GameManager.instance.SetAtlas;
        // 言語に合わせたスプライトの読み込み
        title.sprite = sa.GetSprite("FORCESHOT");
        sub.sprite = sa.GetSprite("SUB");

        // 次のシーン読み込み
        var async = SceneManager.LoadSceneAsync("CourseSelect", LoadSceneMode.Single);
        async.allowSceneActivation = false;
        // 読み込み終わってタップしたら次のシーンへ
        this.UpdateAsObservable().Where(x => async.progress >= 0.9f).Where(x => Input.GetMouseButtonDown(0))
            .Subscribe(_ => 
            {
                if (anim.GetBool("end"))
                {
                    async.allowSceneActivation = true;
                }
                else
                {
                    AnimState();
                }
            }).AddTo(this);
    }

    private void AnimState()
    {
        anim.SetBool("end", true);
    }
}
