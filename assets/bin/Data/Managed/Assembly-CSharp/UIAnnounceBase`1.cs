// Decompiled with JetBrains decompiler
// Type: UIAnnounceBase`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIAnnounceBase<T> : MonoBehaviourSingleton<T> where T : DisableNotifyMonoBehaviour
{
  [SerializeField]
  protected FontStyle style;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected UITweener[] starAnim;
  [SerializeField]
  protected UITweener[] effAnim;
  [SerializeField]
  protected UITweener[] loopAnim;
  [SerializeField]
  protected UITweener[] endAnim;
  private IEnumerator routineWork;

  protected virtual float GetDispSec() => 3f;

  protected override void Awake()
  {
    this.OnAfterAwake();
    base.Awake();
    this.InitAnim();
    this.OnBeforeAwake();
  }

  protected void Start() => this.OnStart();

  private void InitAnim()
  {
    this.OnBeforeInitAnimation();
    int index1 = 0;
    for (int length = this.starAnim.Length; index1 < length; ++index1)
    {
      ((Behaviour) this.starAnim[index1]).enabled = false;
      this.starAnim[index1].ResetToBeginning();
    }
    int index2 = 0;
    for (int length = this.effAnim.Length; index2 < length; ++index2)
    {
      ((Behaviour) this.effAnim[index2]).enabled = false;
      this.effAnim[index2].Sample(1f, true);
    }
    int index3 = 0;
    for (int length = this.loopAnim.Length; index3 < length; ++index3)
      ((Behaviour) this.loopAnim[index3]).enabled = false;
    int index4 = 0;
    for (int length = this.endAnim.Length; index4 < length; ++index4)
      ((Behaviour) this.endAnim[index4]).enabled = false;
    this.OnAfterInitAnimation();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (this.routineWork == null)
      return;
    if (Object.op_Inequality((Object) this.panelChange, (Object) null))
      this.panelChange.Lock();
    this.routineWork = (IEnumerator) null;
    this.InitAnim();
  }

  protected bool AnnounceStart(Player player)
  {
    return !Object.op_Equality((Object) player, (Object) null) && (TutorialStep.HasAllTutorialCompleted() || player is Self) && this.AnnounceStart();
  }

  protected bool AnnounceStart()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return false;
    if (this.routineWork != null)
      this.StopCoroutine(this.routineWork);
    else if (Object.op_Inequality((Object) this.panelChange, (Object) null))
      this.panelChange.UnLock();
    this.routineWork = this.Direction();
    this.StartCoroutine(this.routineWork);
    return true;
  }

  protected IEnumerator Direction()
  {
    this.OnBeforeStartAnimation();
    int index1 = 0;
    for (int length = this.endAnim.Length; index1 < length; ++index1)
      ((Behaviour) this.endAnim[index1]).enabled = false;
    int index2 = 0;
    for (int length = this.starAnim.Length; index2 < length; ++index2)
    {
      this.starAnim[index2].ResetToBeginning();
      this.starAnim[index2].PlayForward();
    }
    int index3 = 0;
    for (int length = this.effAnim.Length; index3 < length; ++index3)
    {
      this.effAnim[index3].ResetToBeginning();
      this.effAnim[index3].PlayForward();
    }
    int index4 = 0;
    for (int length = this.loopAnim.Length; index4 < length; ++index4)
    {
      this.loopAnim[index4].ResetToBeginning();
      this.loopAnim[index4].PlayForward();
    }
    yield return (object) new WaitForSeconds(this.GetDispSec());
    int index5 = 0;
    for (int length = this.endAnim.Length; index5 < length; ++index5)
    {
      this.endAnim[index5].ResetToBeginning();
      this.endAnim[index5].PlayForward();
    }
    int i = 0;
    for (int n = this.endAnim.Length; i < n; ++i)
    {
      while (((Behaviour) this.endAnim[i]).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.panelChange, (Object) null))
      this.panelChange.Lock();
    this.routineWork = (IEnumerator) null;
    this.OnAfterAnimation();
  }

  protected virtual void OnBeforeAwake()
  {
  }

  protected virtual void OnAfterAwake()
  {
  }

  protected virtual void OnStart()
  {
  }

  protected virtual void OnBeforeInitAnimation()
  {
  }

  protected virtual void OnAfterInitAnimation()
  {
  }

  protected virtual void OnBeforeStartAnimation()
  {
  }

  protected virtual void OnAfterAnimation()
  {
  }
}
