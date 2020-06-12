using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
[RequireComponent(typeof(RectTransform))]
public class TestClip : UIBehaviour, IClippable, ICanvasElement
{
    public Canvas m_Canvas;
    public GameObject m_Prefab;
    public Material m_Material;
 
 
    private RectMask2D m_ParentMask;
    private RectTransform m_RectTransform;
    private MeshRenderer m_MeshRenderer;
 
    public RectTransform rectTransform {
        get
        {
            if (m_RectTransform == null)
            {
                m_RectTransform = transform as RectTransform; ;
            }
            return m_RectTransform; }
    }
 
 
    public void Cull(Rect clipRect, bool validRect)
    {
       
    }
 
    public void RecalculateClipping()
    {
        UpdateClipParent();
    }
 
 
    public void SetClipRect(Rect value, bool validRect)
    {
        //在这里就可以将正确的Rect2DMask传入shader了
        if (m_MeshRenderer)
        {
            //缩放是屏幕高度的一半
            float scale = (m_Canvas.transform as RectTransform).sizeDelta.y / 2f;
            //将RectMask2D的区域传入
            m_MeshRenderer.sharedMaterial.SetVector("_ClipRect", new Vector4(value.xMin, value.yMin, value.xMax, value.yMax));
            //将Scale传入
            m_MeshRenderer.sharedMaterial.SetFloat("_Scale", scale);
        }
    }
 
    protected override void Awake()
    {
        base.Awake();
        m_MeshRenderer = Instantiate<GameObject>(m_Prefab, transform, false).GetComponent<MeshRenderer>();
        m_MeshRenderer.material = m_Material;
        //在这里通知 UGUI Rebuild
        //接着UGUI就会回调到SetClipRect方法中
        CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
    }
 
    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateClipParent();
       
    }
 
    protected override void OnDisable()
    {
        base.OnDisable();
        UpdateClipParent();
    }
 
 
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        UpdateClipParent();
    }
#endif
 
 
    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
        UpdateClipParent();
    }
 
    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
        UpdateClipParent();
    }
    private void UpdateClipParent()
    {
        var newParent = MaskUtilities.GetRectMaskForClippable(this);
 
        if (m_ParentMask != null && (newParent != m_ParentMask || !newParent.IsActive()))
        {
            m_ParentMask.RemoveClippable(this);
        }
        if (newParent != null && newParent.IsActive())
            newParent.AddClippable(this);
 
        m_ParentMask = newParent;
    }
 
    public void Rebuild(CanvasUpdate executing)
    {
 
    }
 
    public void LayoutComplete()
    {
    }
 
    public void GraphicUpdateComplete()
    {
    }
}