// Decompiled with JetBrains decompiler
// Type: UIBingoPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIBingoPanel : MonoBehaviourSingleton<UIBingoPanel>
{
  [SerializeField]
  private GameObject noticeObject;
  [SerializeField]
  protected UITweenCtrl animCtrl;
  [SerializeField]
  protected TweenAlpha noticeTween;
  [SerializeField]
  protected Vector3 offset;
  protected IFieldGimmickObject bingo;
  protected IFieldGimmickObject bingoReq;

  protected override void Awake()
  {
    base.Awake();
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
    List<IFieldGimmickObject> fieldGimmick = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickList[4];
    if (fieldGimmick.IsNullOrEmpty<IFieldGimmickObject>())
      return;
    this.bingoReq = (IFieldGimmickObject) null;
    float num1 = float.MaxValue;
    for (int index = 0; index < fieldGimmick.Count; ++index)
    {
      IFieldGimmickObject fieldGimmickObject = fieldGimmick[index];
      if (fieldGimmickObject != null)
      {
        float num2 = Vector3.Distance(fieldGimmickObject.GetTransform().position, self._position);
        if ((double) num2 < 10.0 && (double) num2 < (double) num1)
        {
          this.bingoReq = fieldGimmickObject;
          num1 = num2;
        }
      }
    }
    if (this.bingo == this.bingoReq || ((Behaviour) this.noticeTween).isActiveAndEnabled)
      return;
    if ((double) this.noticeTween.value == 0.0)
    {
      if (this.bingoReq != null)
      {
        this.noticeObject.SetActive(true);
        this.noticeTween.PlayForward();
      }
      else
        this.noticeObject.SetActive(false);
      this.bingo = this.bingoReq;
    }
    else
      this.noticeTween.PlayReverse();
  }

  private void LateUpdate()
  {
    if (this.bingo == null)
      return;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(Vector3.op_Addition(this.bingo.GetTransform().position, this.offset)));
    worldPoint.z = (double) worldPoint.z < 0.0 ? -100f : 0.0f;
    this._transform.position = worldPoint;
  }
}
