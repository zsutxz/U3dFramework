
using UnityEngine;
using DG.Tweening;

public class TaskPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        Vector3 temp = transform.localPosition;
        temp.x = -800;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        transform.DOLocalMoveX(-800, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}