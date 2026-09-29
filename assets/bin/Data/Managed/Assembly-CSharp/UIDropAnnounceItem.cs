// Decompiled with JetBrains decompiler
// Type: UIDropAnnounceItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIDropAnnounceItem : MonoBehaviour
{
  [SerializeField]
  protected UILabel itemName;
  [SerializeField]
  protected UITweener[] animStart;
  [SerializeField]
  protected UITweener[] animEnd;
  private Transform _transform;
  private Vector3Interpolator anim = new Vector3Interpolator();
  private bool isStop;
  protected Action<UIDropAnnounceItem> onEndCallback;

  protected void Awake()
  {
    int index1 = 0;
    for (int length = this.animStart.Length; index1 < length; ++index1)
    {
      ((Behaviour) this.animStart[index1]).enabled = false;
      this.animStart[index1].Sample(1f, true);
    }
    int index2 = 0;
    for (int length = this.animEnd.Length; index2 < length; ++index2)
      ((Behaviour) this.animEnd[index2]).enabled = false;
    this._transform = ((Component) this).transform;
  }

  public void StartAnnounce(
    string text,
    Color color,
    bool stop,
    Action<UIDropAnnounceItem> end_callback)
  {
    if (!((Component) this).gameObject.activeSelf)
      ((Component) this).gameObject.SetActive(true);
    this.itemName.text = text;
    this.itemName.color = color;
    this.onEndCallback = end_callback;
    this.isStop = stop;
    int index = 0;
    for (int length = this.animStart.Length; index < length; ++index)
    {
      this.animStart[index].ResetToBeginning();
      this.animStart[index].PlayForward();
    }
    this.StartCoroutine(this.Direction());
  }

  protected IEnumerator Direction()
  {
    int i = 0;
    int n;
    for (n = this.animStart.Length; i < n; ++i)
    {
      while (((Behaviour) this.animStart[i]).enabled)
        yield return (object) null;
    }
    while (this.isStop)
      yield return (object) null;
    yield return (object) new WaitForSeconds(0.3f);
    if (this.onEndCallback != null)
      this.onEndCallback(this);
    int index = 0;
    for (int length = this.animEnd.Length; index < length; ++index)
    {
      this.animEnd[index].ResetToBeginning();
      this.animEnd[index].PlayForward();
    }
    n = 0;
    for (i = this.animEnd.Length; n < i; ++n)
    {
      while (((Behaviour) this.animEnd[n]).enabled)
        yield return (object) null;
    }
    ((Component) this).gameObject.SetActive(false);
  }

  public void MovePos(bool stop, Vector3 pos, float time)
  {
    this.anim.Set(time, this._transform.localPosition, pos, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    this.anim.Play();
    this.isStop = stop;
  }

  private void LateUpdate()
  {
    if (!this.anim.IsPlaying())
      return;
    this._transform.localPosition = this.anim.Update();
  }
}
