// Decompiled with JetBrains decompiler
// Type: RegionMapPortal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
[Serializable]
public class RegionMapPortal : MonoBehaviour
{
  private Transform _transform;
  [SerializeField]
  private int _entranceId;
  [SerializeField]
  private int _exitId;
  [SerializeField]
  private RegionMapLocation _from;
  [SerializeField]
  private RegionMapLocation _to;
  [SerializeField]
  private MeshRenderer road;
  [SerializeField]
  private Transform effectRoot;

  public int entranceId => this._entranceId;

  public int exitId => this._exitId;

  public RegionMapLocation fromLocation => this._from;

  public RegionMapLocation toLocation => this._to;

  public bool IsVisited()
  {
    return MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint) this.entranceId) || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint) this.exitId);
  }

  public bool IsShow()
  {
    return FieldManager.IsShowPortal((uint) this.entranceId) && FieldManager.IsShowPortal((uint) this.exitId);
  }

  public void Init(RegionMapLocation fromLoc, RegionMapLocation toLoc)
  {
    this._from = fromLoc;
    this._to = toLoc;
    this._transform = ((Component) this).transform;
    for (int index = 0; index < this._transform.childCount; ++index)
    {
      Transform child = this._transform.GetChild(index);
      if (((Object) ((Component) child).gameObject).name.StartsWith("road"))
        this.road = ((Component) child).GetComponent<MeshRenderer>();
      else if (((Object) ((Component) child).gameObject).name.StartsWith("effect"))
        this.effectRoot = child;
    }
  }

  public void Open()
  {
    ((Renderer) this.road).material.SetTextureOffset("_AlphaTex", new Vector2(-1f, 0.0f));
  }

  public void Open(
    Transform effect,
    Animator animator,
    bool reverse,
    float endTime,
    System.Action onComplete)
  {
    effect.parent = this.effectRoot;
    effect.localPosition = Vector3.zero;
    this.StartCoroutine(this.DoOpen(effect, animator, reverse, endTime, onComplete));
  }

  private IEnumerator DoOpen(
    Transform effect,
    Animator animator,
    bool reverse,
    float endTime,
    System.Action onComplete)
  {
    if (reverse)
      ((Renderer) this.road).material.SetFloat("_Reverse", 1f);
    while (true)
    {
      yield return (object) null;
      AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
      float normalizedTime = ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime;
      if ((double) normalizedTime <= (double) endTime)
        ((Renderer) this.road).material.SetTextureOffset("_AlphaTex", new Vector2(1f - normalizedTime, 0.0f));
      else
        break;
    }
    ((Behaviour) animator).enabled = false;
    if (onComplete != null)
      onComplete();
    float timer = 0.0f;
    while (true)
    {
      yield return (object) null;
      timer += Time.deltaTime;
      if ((double) timer <= (double) endTime)
        ((Renderer) this.road).material.SetTextureOffset("_AlphaTex", new Vector2((float) (1.0 - ((double) timer + (double) endTime)), 0.0f));
      else
        break;
    }
  }
}
