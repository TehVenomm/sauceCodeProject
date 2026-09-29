// Decompiled with JetBrains decompiler
// Type: AppCloseProcess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AppCloseProcess : MonoBehaviour
{
  private IEnumerator Start()
  {
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, true);
    ResourceManager.internalMode = false;
    yield return (object) ResourceSizeInfo.Init();
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
    {
      "MainCamera",
      "InputManager"
    });
    if (MonoBehaviourSingleton<SoundManager>.IsValid())
      MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
    while (load_queue.IsLoading())
      yield return (object) null;
    GameSceneGlobalSettings.SetOrientation(false);
    MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
    if (Object.op_Inequality((Object) Camera.main, (Object) null))
      Object.DestroyImmediate((Object) ((Component) Camera.main).gameObject);
    foreach (ResourceObject loadedObject in lo_common_prefabs.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<AppMain>.I._transform);
    MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
    MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, true);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
    MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "AppClose");
    MonoBehaviourSingleton<AppMain>.I.OnLoadFinished();
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, false);
    Object.Destroy((Object) this);
  }
}
