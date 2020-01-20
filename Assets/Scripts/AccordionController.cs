using UnityEngine;

// アコーディオンメニューのアニメーション管理
public class AccordionController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnClick()
    {
        anim.SetBool("Open", !anim.GetBool("Open"));
    }
}
