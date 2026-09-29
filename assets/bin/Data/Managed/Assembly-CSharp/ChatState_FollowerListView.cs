// Decompiled with JetBrains decompiler
// Type: ChatState_FollowerListView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ChatState_FollowerListView : ChatState
{
  private static readonly string WINDOW_PREFAB_NAME = "HomeVariableMemberListController";
  private HomeVariableMemberListController m_ctrl;
  private bool isInitializing;

  public override void Enter(MainChat _manager)
  {
    base.Enter(_manager);
    if (Object.op_Equality((Object) this.m_manager, (Object) null))
      return;
    this.m_manager.ExecCoroutine(this.InitializeCoroutine());
  }

  private IEnumerator InitializeCoroutine()
  {
    if (this.IsInitialized || this.isInitializing)
    {
      this.m_manager.PopState();
      this.EndInitialize();
    }
    else
    {
      this.isInitializing = true;
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this.m_manager);
      LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.UI, ChatState_FollowerListView.WINDOW_PREFAB_NAME);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.m_ctrl = ((Component) ResourceUtility.Realizes((Object) (lo.loadedObject as GameObject), ((Component) this.m_manager).transform, 5)).GetComponent<HomeVariableMemberListController>();
      if (Object.op_Equality((Object) this.m_ctrl, (Object) null))
      {
        this.m_manager.PopState();
        this.EndInitialize();
      }
      else
      {
        this.m_ctrl.Initialize(new HomeVariableMemberListController.InitParam()
        {
          IsDisplayClanMember = false,
          IsDisplayMutualFollower = true,
          Mainchat = this.m_manager
        });
        while (this.m_ctrl.IsInitializing())
          yield return (object) null;
        this.isInitializing = false;
        this.EndInitialize();
      }
    }
  }

  public override System.Type GetNextState()
  {
    return Object.op_Equality((Object) this.m_manager, (Object) null) || !this.IsInitialized ? base.GetNextState() : this.m_manager.GetTopState();
  }

  public override void Exit()
  {
    Object.Destroy((Object) ((Component) this.m_ctrl).gameObject);
    this.m_ctrl = (HomeVariableMemberListController) null;
  }
}
