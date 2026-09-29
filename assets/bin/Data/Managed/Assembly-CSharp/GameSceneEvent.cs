// Decompiled with JetBrains decompiler
// Type: GameSceneEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class GameSceneEvent
{
  public static GameSceneEvent current = new GameSceneEvent();
  public static GameSceneEvent request = (GameSceneEvent) null;
  private static GameSceneEvent stay = (GameSceneEvent) null;
  private static Stack stayStack = (Stack) null;
  public bool isExecute;
  public string eventName = string.Empty;
  public GameObject sender;
  public object userData;

  public static void Initialize()
  {
    GameSceneEvent.ResetCurrent();
    GameSceneEvent.stay = (GameSceneEvent) null;
    GameSceneEvent.stayStack = (Stack) null;
  }

  public GameSceneEvent() => this._Init();

  public GameSceneEvent(GameSceneEvent e)
  {
    this.isExecute = e.isExecute;
    this.eventName = e.eventName;
    this.sender = e.sender;
    this.userData = e.userData;
  }

  public void _Init()
  {
    this.isExecute = false;
    this.eventName = string.Empty;
    this.sender = (GameObject) null;
    this.userData = (object) null;
  }

  public static void ResetCurrent() => GameSceneEvent.current._Init();

  public static void Stay()
  {
    if (GameSceneEvent.stay != null)
    {
      Log.Error(LOG.GAMESCENE, "ERR :: GameSceneEvent Is Already Staying");
    }
    else
    {
      GameSceneEvent.stay = new GameSceneEvent(GameSceneEvent.current);
      GameSceneEvent.ResetCurrent();
    }
  }

  public static bool IsStay() => GameSceneEvent.stay != null;

  public static void Resume(object userData = null, bool is_send_query = false)
  {
    if (GameSceneEvent.stay == null || !MonoBehaviourSingleton<GameSceneManager>.IsValid())
      return;
    GameSceneEvent.current = GameSceneEvent.stay;
    GameSceneEvent.stay = (GameSceneEvent) null;
    if (userData != null)
      GameSceneEvent.current.userData = userData;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("GameSceneEvent.Resume", GameSceneEvent.current.sender, GameSceneEvent.current.eventName, GameSceneEvent.current.userData, is_send_query: is_send_query);
  }

  public static void PushStay()
  {
    if (GameSceneEvent.stayStack == null)
      GameSceneEvent.stayStack = new Stack();
    GameSceneEvent.stayStack.Push((object) GameSceneEvent.stay);
    GameSceneEvent.stay = (GameSceneEvent) null;
  }

  public static void PopStay()
  {
    if (GameSceneEvent.stayStack == null || GameSceneEvent.stayStack.Count == 0)
      return;
    GameSceneEvent.stay = (GameSceneEvent) GameSceneEvent.stayStack.Pop();
  }

  public static void ChangeStay(string event_name, object user_data = null)
  {
    if (GameSceneEvent.stay == null)
      return;
    GameSceneEvent.stay.eventName = event_name;
    if (user_data == null)
      return;
    GameSceneEvent.stay.userData = user_data;
  }

  public static void ChangeStayEventData(object user_data)
  {
    if (GameSceneEvent.stay == null || user_data == null)
      return;
    GameSceneEvent.stay.userData = user_data;
  }

  public static void Cancel()
  {
    GameSceneEvent.ResetCurrent();
    GameSceneEvent.stay = (GameSceneEvent) null;
  }
}
