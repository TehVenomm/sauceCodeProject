// Decompiled with JetBrains decompiler
// Type: UIAnnounceBand
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIAnnounceBand : MonoBehaviourSingleton<UIAnnounceBand>
{
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected UILabel label;
  [SerializeField]
  protected UILabel conditionLabel;
  [SerializeField]
  protected float dispTime = 3f;
  [SerializeField]
  protected UITweener[] animStart;
  [SerializeField]
  protected UITweener[] animEnd;
  [SerializeField]
  protected GameObject animRoot;
  private bool isDone;
  public bool isWait;
  private List<string> announceQueue = new List<string>();

  private bool isStartable => !this.isDone && !this.isWait;

  protected override void Awake()
  {
    base.Awake();
    this.InitAnim();
  }

  private void InitAnim()
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
    ((Component) this).gameObject.SetActive(false);
    this.animRoot.SetActive(false);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!this.isDone)
      return;
    this.InitAnim();
    this.announceQueue.Clear();
    this.isDone = false;
    this.panelChange.Lock();
  }

  private void LateUpdate()
  {
    if (!this.isStartable)
      return;
    this.PlayAnnounce();
  }

  public void SetAnnounce(string messeage, string conditionTitle)
  {
    ((Component) this).gameObject.SetActive(true);
    this.announceQueue.Add(messeage);
    this.announceQueue.Add(conditionTitle);
  }

  private bool PlayAnnounce()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
    {
      ((Component) this).gameObject.SetActive(false);
      return false;
    }
    if (this.announceQueue.Count <= 0)
      return false;
    this.animRoot.SetActive(true);
    this.label.text = this.announceQueue[0];
    this.label.supportEncoding = true;
    this.announceQueue.RemoveAt(0);
    this.conditionLabel.text = this.announceQueue[0];
    this.announceQueue.RemoveAt(0);
    this.isDone = true;
    this.panelChange.UnLock();
    this.StartCoroutine(this.Direction());
    return true;
  }

  private void FinishAnnounce()
  {
    if (this.PlayAnnounce())
      return;
    this.animRoot.SetActive(false);
    ((Component) this).gameObject.SetActive(false);
    this.panelChange.Lock();
    this.isDone = false;
  }

  protected IEnumerator Direction()
  {
    int index1 = 0;
    for (int length = this.animEnd.Length; index1 < length; ++index1)
      this.animEnd[index1].ResetToBeginning();
    int index2 = 0;
    for (int length = this.animStart.Length; index2 < length; ++index2)
    {
      this.animStart[index2].ResetToBeginning();
      this.animStart[index2].PlayForward();
    }
    int i = 0;
    int n;
    for (n = this.animStart.Length; i < n; ++i)
    {
      while (((Behaviour) this.animStart[i]).enabled)
        yield return (object) null;
    }
    yield return (object) new WaitForSeconds(this.dispTime);
    int index3 = 0;
    for (int length = this.animEnd.Length; index3 < length; ++index3)
      this.animEnd[index3].PlayForward();
    n = 0;
    for (i = this.animEnd.Length; n < i; ++n)
    {
      while (((Behaviour) this.animEnd[n]).enabled)
        yield return (object) null;
    }
    this.FinishAnnounce();
  }
}
