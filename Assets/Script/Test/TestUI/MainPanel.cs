
using UnityEngine;
using DG.Tweening;

using U3dFramework;

public class MainPanel : BasePanel
{

    void Start()
    {

    }

    public override void OnEnter()
    {

        Vector3 temp = transform.localPosition;
        temp.x = -800;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }

    public override void OnPause()
    {
        //canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        //canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        transform.DOLocalMoveX(-800, .5f);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}