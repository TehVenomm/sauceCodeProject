// Decompiled with JetBrains decompiler
// Type: UIPortalNextAreaName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIPortalNextAreaName : MonoBehaviourSingleton<UIPortalNextAreaName>
{
  [SerializeField]
  private GameObject noticeObject;
  [SerializeField]
  protected UILabel nameLabel;
  [SerializeField]
  protected UITweenCtrl animCtrl;
  [SerializeField]
  protected TweenAlpha noticeTween;
  [SerializeField]
  protected Vector3 offset = Vector3.zero;
  protected PortalObject portal;
  protected PortalObject portalReq;

  protected override void Awake()
  {
    base.Awake();
    this.nameLabel.fontStyle = (FontStyle) 2;
    this.noticeObject.SetActive(false);
    ((Component) this).gameObject.SetActive(FieldManager.IsValidInGameNoQuest());
  }

  private void Update()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    this.portalReq = (PortalObject) null;
    int index = 0;
    for (int count = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; index < count; ++index)
    {
      PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[index];
      if (portalObject.isFull && portalObject.portalData.dstMapID != 0U && (double) Vector3.Distance(portalObject._transform.position, self._position) < 10.0)
      {
        this.portalReq = portalObject;
        break;
      }
    }
    if (Object.op_Equality((Object) this.portal, (Object) this.portalReq) || ((Behaviour) this.noticeTween).isActiveAndEnabled)
      return;
    if ((double) this.noticeTween.value == 0.0)
    {
      if (Object.op_Inequality((Object) this.portalReq, (Object) null))
      {
        if (string.IsNullOrEmpty(this.portalReq.portalData.placeText))
        {
          FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.portalReq.portalData.dstMapID);
          if (fieldMapData == null)
            return;
          this.nameLabel.text = fieldMapData.mapName;
        }
        else
          this.nameLabel.text = this.portalReq.portalData.placeText;
        this.noticeObject.SetActive(true);
        this.noticeTween.PlayForward();
      }
      else
        this.noticeObject.SetActive(false);
      this.portal = this.portalReq;
    }
    else
      this.noticeTween.PlayReverse();
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.portal, (Object) null))
      return;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(Vector3.op_Addition(this.portal._transform.position, this.offset)));
    worldPoint.z = (double) worldPoint.z < 0.0 ? -100f : 0.0f;
    this._transform.position = worldPoint;
  }
}
