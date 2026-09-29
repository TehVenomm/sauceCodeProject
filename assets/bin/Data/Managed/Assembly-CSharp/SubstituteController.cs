// Decompiled with JetBrains decompiler
// Type: SubstituteController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SubstituteController
{
  private Player owner;
  private List<SubstituteEffect> list = new List<SubstituteEffect>();
  private InGameSettingsManager.BuffParamInfo info;
  private GameObject effectRoot;

  public void Initialize(Player p)
  {
    this.owner = p;
    this.list.Clear();
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.info = MonoBehaviourSingleton<InGameSettingsManager>.I.buff;
  }

  public void TryFinalize()
  {
    this.owner = (Player) null;
    this.End();
    this.list.Clear();
    this.info = (InGameSettingsManager.BuffParamInfo) null;
    if (Object.op_Inequality((Object) this.effectRoot, (Object) null))
      Object.Destroy((Object) this.effectRoot);
    this.effectRoot = (GameObject) null;
  }

  public void Create(int num)
  {
    if (Object.op_Equality((Object) this.effectRoot, (Object) null))
    {
      this.effectRoot = new GameObject();
      this.effectRoot.transform.SetParent(MonoBehaviourSingleton<EffectManager>.I._transform);
      this.effectRoot.transform.localPosition = Vector3.zero;
      this.effectRoot.transform.localScale = Vector3.one;
      this.effectRoot.transform.localRotation = Quaternion.identity;
    }
    this.effectRoot.SetActive(this.owner.isActedBattleStart);
    for (int index = 0; index < num; ++index)
    {
      if (index < this.list.Count)
      {
        this.list[index].Create(this.effectRoot.transform);
      }
      else
      {
        SubstituteEffect substituteEffect = new SubstituteEffect();
        substituteEffect.Initialize(this.effectRoot.transform, index, this.owner, index == 0 ? (SubstituteEffect) null : this.list[index - 1], this.info);
        this.list.Add(substituteEffect);
      }
    }
  }

  public void Sub()
  {
    for (int index = this.list.Count - 1; index >= 0; --index)
    {
      SubstituteEffect substituteEffect = this.list[index];
      if (substituteEffect.IsEnable())
      {
        substituteEffect.End();
        break;
      }
    }
    int enableNum = this.GetEnableNum();
    if (enableNum == 0 || !Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnSyncSubstitute(enableNum);
  }

  public void End()
  {
    int index = 0;
    for (int count = this.list.Count; index < count; ++index)
      this.list[index].End();
  }

  public void Update(bool isLerp = true)
  {
    if (this.list.IsNullOrEmpty<SubstituteEffect>())
      return;
    int index = 0;
    for (int count = this.list.Count; index < count; ++index)
      this.list[index].Update(isLerp);
  }

  public void ActiveEffectRoot()
  {
    if (Object.op_Equality((Object) this.effectRoot, (Object) null) || this.effectRoot.activeSelf)
      return;
    this.Update(false);
    this.effectRoot.SetActive(true);
  }

  private int GetEnableNum()
  {
    int enableNum = 0;
    int index = 0;
    for (int count = this.list.Count; index < count; ++index)
    {
      if (this.list[index].IsEnable())
        ++enableNum;
    }
    return enableNum;
  }

  public void Sync(int num)
  {
    this.ActiveEffectRoot();
    int enableNum = this.GetEnableNum();
    if (enableNum == num)
      return;
    if (enableNum < num)
    {
      this.Create(num);
    }
    else
    {
      int num1 = 0;
      for (int index = enableNum - num; num1 < index; ++num1)
        this.Sub();
    }
  }
}
