// Decompiled with JetBrains decompiler
// Type: MultiLockMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MultiLockMarker : MonoBehaviour
{
  public const int kLayerMask = 16 /*0x10*/;
  [SerializeField]
  private Animator animator;
  private Transform _transform;
  private List<int> _lockOrder = new List<int>();
  private float _lockInterval;
  private InGameSettingsManager.Player.ArrowActionInfo info;

  public List<int> lockOrder => this._lockOrder;

  private void Update()
  {
    if ((double) this._lockInterval < 0.0)
      return;
    this._lockInterval -= Time.deltaTime;
  }

  public void Init()
  {
    this._transform = ((Component) this).transform;
    this._lockOrder.Clear();
    this._lockInterval = 0.0f;
    this.info = MonoBehaviourSingleton<InGameSettingsManager>.IsValid() ? MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo : (InGameSettingsManager.Player.ArrowActionInfo) null;
  }

  private bool CanLock()
  {
    return this.info != null && this._lockOrder.Count < this.info.soulLockRegionMax && (double) this._lockInterval <= 0.0;
  }

  public bool Lock(int sumLockNum, bool isBoost)
  {
    if (!this.CanLock())
      return false;
    this._lockOrder.Add(sumLockNum + 1);
    this._lockInterval = isBoost ? this.info.soulBoostLockRegionInterval : this.info.soulLockRegionInterval;
    this.animator.SetInteger("state", this._lockOrder.Count);
    EffectManager.GetEffect("ef_btl_wsk2_bow_lock_02", this._transform);
    return true;
  }

  public void Hide()
  {
    if (this._lockOrder.Count > 0)
      return;
    this.animator.SetInteger("state", -1);
  }

  public void Reset()
  {
    this._lockOrder.Clear();
    this._lockInterval = 0.0f;
    this.animator.SetInteger("state", 0);
  }

  public void EndBoost(bool isHide)
  {
    int arrowNormalLockNum = MonoBehaviourSingleton<StageObjectManager>.I.self.GetSoulArrowNormalLockNum();
    for (int index = 0; index < this._lockOrder.Count; ++index)
    {
      if (this._lockOrder[index] > arrowNormalLockNum)
      {
        this._lockOrder.RemoveRange(index, this._lockOrder.Count - index);
        break;
      }
    }
    if (this._lockOrder.Count > 0)
      this.animator.SetInteger("state", this._lockOrder.Count);
    else
      this.animator.SetInteger("state", isHide ? -1 : 0);
  }
}
