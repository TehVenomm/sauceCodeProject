// Decompiled with JetBrains decompiler
// Type: ConfigServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ConfigServer : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void Exit()
  {
    GameSaveData.Save();
    base.Exit();
  }

  public override void UpdateUI()
  {
    this.SetGrid((Enum) ConfigServer.UI.GRD_ORDER_QUEST, "ServerItem", Singleton<ServerListTable>.I.GetActiveServerList().Count, true, (Func<int, Transform, Transform>) ((i, t) => this.Realizes("ServerItem", t)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetActive(t, true);
      this.SetEvent(t, "SELECT_SERVER", i);
      ServerListTable.ServerData activeServer = Singleton<ServerListTable>.I.GetActiveServerList()[i];
      this.SetToggle(t, (Enum) ConfigServer.UI.TGL_SERVER, (int) GameSaveData.instance.currentServer.id == (int) activeServer.id);
      foreach (Component componentsInChild in ((Component) t).GetComponentsInChildren(typeof (UILabel), true))
        componentsInChild.GetComponent<UILabel>().SetTextOnly(activeServer.name);
      this.SetActive(t, (Enum) ConfigServer.UI.SERVER_NOTE, !string.IsNullOrEmpty(activeServer.note));
      if (string.IsNullOrEmpty(activeServer.note))
        return;
      this.SetLabelText(t, (Enum) ConfigServer.UI.SERVER_NOTE, activeServer.note);
      this.SetSupportEncoding(t, (Enum) ConfigServer.UI.SERVER_NOTE, true);
    }));
  }

  public virtual void OnQuery_SELECT_SERVER()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || eventData >= Singleton<ServerListTable>.I.GetActiveServerList().Count)
      GameSection.StopEvent();
    else
      this.StartCoroutine(this.DoChangeServer(eventData));
  }

  private IEnumerator DoChangeServer(int btnId)
  {
    if ((int) GameSaveData.instance.currentServer.id != (int) Singleton<ServerListTable>.I.GetActiveServerList()[btnId].id)
    {
      AccountManager.ResetAccount();
      GameSaveData.instance.SetCurrentServer(Singleton<ServerListTable>.I.GetActiveServerList()[btnId]);
      MonoBehaviourSingleton<HelpshiftManager>.I.Logout();
      MonoBehaviourSingleton<AppMain>.I.Reset();
      this.RefreshUI();
    }
    else
    {
      GameSection.BackSection();
      yield break;
    }
  }

  private enum UI
  {
    TGL_SERVER,
    SERVER_NOTE,
    DSV_ROOT,
    GRD_ORDER_QUEST,
  }
}
