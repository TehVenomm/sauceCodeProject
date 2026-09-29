// Decompiled with JetBrains decompiler
// Type: UIExplorePlayerStatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIExplorePlayerStatusList : MonoBehaviour
{
  [SerializeField]
  private UIExplorePlayerStatus[] statuses = new UIExplorePlayerStatus[3];
  private ExploreStatus exploreStatus;

  public void Initialize(ExploreStatus exploreStatus)
  {
    if (this.exploreStatus != exploreStatus)
    {
      this.Clear();
      this.exploreStatus = exploreStatus;
      exploreStatus.onChangeExploreMemberList += new System.Action(this.OnChangeExploreMemberList);
    }
    this.OnChangeExploreMemberList();
  }

  private void Clear()
  {
    if (this.exploreStatus == null)
      return;
    this.exploreStatus.onChangeExploreMemberList -= new System.Action(this.OnChangeExploreMemberList);
    this.exploreStatus = (ExploreStatus) null;
  }

  private void OnDestroy() => this.Clear();

  private void OnChangeExploreMemberList()
  {
    for (int index = 0; index < this.statuses.Length; ++index)
      ((Component) this.statuses[index]).gameObject.SetActive(false);
    List<ExplorePlayerStatus> playerStatusList = this.exploreStatus.GetEnabledPlayerStatusList();
    bool flag = false;
    for (int index = 0; index < playerStatusList.Count; ++index)
    {
      ExplorePlayerStatus playerStatus = playerStatusList[index];
      if (playerStatus.isSelf)
      {
        flag = true;
      }
      else
      {
        int slotIndex = playerStatus.coopClient.slotIndex;
        if (slotIndex >= 0)
        {
          if (flag)
            --slotIndex;
          this.statuses[slotIndex].Initialize(playerStatus);
        }
      }
    }
  }
}
