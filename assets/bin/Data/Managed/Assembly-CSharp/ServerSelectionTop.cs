// Decompiled with JetBrains decompiler
// Type: ServerSelectionTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ServerSelectionTop : GameSection
{
  private bool isStartSection;
  private GameObject cutSceneObjectRoot;
  private GameObject cutOP;
  private Animation cutSceneAnimation;
  private GameObject titleObjectRoot;

  public override void Initialize()
  {
    this.isStartSection = false;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedCutSceneObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Opening");
    LoadObject loadedTitleObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<AppMain>.I.mainCamera, (Object) null))
      ((Behaviour) MonoBehaviourSingleton<AppMain>.I.mainCamera).enabled = false;
    GameObject gameObject = new GameObject();
    ((Object) gameObject).name = "BG_CutObject";
    gameObject.transform.parent = ((Component) MonoBehaviourSingleton<AppMain>.I).transform;
    this.cutSceneObjectRoot = ResourceUtility.Instantiate<Object>(loadedCutSceneObj.loadedObject) as GameObject;
    this.cutSceneObjectRoot.transform.parent = gameObject.transform;
    this.titleObjectRoot = ResourceUtility.Instantiate<Object>(loadedTitleObj.loadedObject) as GameObject;
    this.titleObjectRoot.transform.parent = gameObject.transform;
    Transform transform = this.cutSceneObjectRoot.transform.Find("CUT_op");
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      this.cutOP = ((Component) transform).gameObject;
      this.cutSceneAnimation = this.cutOP.GetComponent<Animation>();
      this.cutOP.SetActive(false);
      if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyOpening)
        this.cutOP.transform.localScale = SpecialDeviceManager.SpecialDeviceInfo.OpeningCutScale;
    }
    this.cutSceneAnimation.Stop();
    base.Initialize();
    TitleTop.isFirstServerSelection = false;
  }

  public override void Exit()
  {
    GameSaveData.Save();
    base.Exit();
  }

  public override void StartSection()
  {
    base.StartSection();
    this.isStartSection = true;
    this.RefreshUI();
  }

  public override void UpdateUI()
  {
    if (!this.isStartSection)
      return;
    this.SetGrid((Enum) ServerSelectionTop.UI.GRD_ORDER_QUEST, "ServerSelectionItem", Singleton<ServerListTable>.I.GetActiveServerList().Count, true, (Func<int, Transform, Transform>) ((i, t) => this.Realizes("ServerSelectionItem", t)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetActive(t, true);
      this.SetEvent(t, "SELECT_SERVER", i);
      ServerListTable.ServerData activeServer = Singleton<ServerListTable>.I.GetActiveServerList()[i];
      foreach (Component componentsInChild in ((Component) t).GetComponentsInChildren(typeof (UILabel), true))
        componentsInChild.GetComponent<UILabel>().SetTextOnly(activeServer.name);
      this.SetActive(t, (Enum) ServerSelectionTop.UI.SERVER_NOTE, !string.IsNullOrEmpty(activeServer.note));
      if (string.IsNullOrEmpty(activeServer.note))
        return;
      this.SetLabelText(t, (Enum) ServerSelectionTop.UI.SERVER_NOTE, activeServer.note);
      this.SetSupportEncoding(t, (Enum) ServerSelectionTop.UI.SERVER_NOTE, true);
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
    AccountManager.ResetAccount();
    GameSaveData.instance.SetCurrentServer(Singleton<ServerListTable>.I.GetActiveServerList()[btnId]);
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "Opening");
    yield break;
  }

  private enum UI
  {
    TGL_SERVER,
    SERVER_NOTE,
    DSV_ROOT,
    GRD_ORDER_QUEST,
  }
}
