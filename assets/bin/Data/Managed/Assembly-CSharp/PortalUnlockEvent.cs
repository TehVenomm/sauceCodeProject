// Decompiled with JetBrains decompiler
// Type: PortalUnlockEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PortalUnlockEvent : MonoBehaviour
{
  private List<PortalObject> unlockedPortalList = new List<PortalObject>(10);
  private List<PortalObject.VIEW_TYPE> viewTypes = new List<PortalObject.VIEW_TYPE>(10);
  private System.Action onEndAllEventAction;
  private const float MOVE_TIME = 1.2f;

  public void AddPortal(PortalObject obj) => this.unlockedPortalList.Add(obj);

  public void SetOnEndAllEvent(System.Action action) => this.onEndAllEventAction = action;

  private IEnumerator Start()
  {
    for (int index = 0; index < this.unlockedPortalList.Count; ++index)
    {
      this.viewTypes.Add(this.unlockedPortalList[index].viewType);
      this.unlockedPortalList[index].SetAndCreateView(PortalObject.VIEW_TYPE.NOT_CLEAR_ORDER);
    }
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_EVENT, true);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, true);
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.UNLOCK_EVENT;
    ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = false;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedEffect = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_warp_lockbreak_01");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Object effectPrefab = loadedEffect.loadedObject;
    Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    Vector3 cameraOriginalPostion = mainCameraTransform.position;
    Vector3 cameraOffset = Vector3.op_Subtraction(cameraOriginalPostion, MonoBehaviourSingleton<StageObjectManager>.I.self._position);
    float timer = 0.0f;
    Vector3 cameraStartPos;
    Vector3 targetCameraPos;
    for (int i = 0; i < this.unlockedPortalList.Count; ++i)
    {
      timer = 0.0f;
      cameraStartPos = mainCameraTransform.position;
      targetCameraPos = Vector3.op_Addition(this.unlockedPortalList[i]._transform.position, cameraOffset);
      while ((double) timer < 1.2000000476837158)
      {
        mainCameraTransform.position = Vector3.Lerp(cameraStartPos, targetCameraPos, timer / 1.2f);
        timer += Time.deltaTime;
        yield return (object) null;
      }
      ResourceUtility.Realizes(effectPrefab, this.unlockedPortalList[i]._transform);
      SoundManager.PlayOneShotUISE(40000159);
      yield return (object) new WaitForSeconds(1.7f);
      this.unlockedPortalList[i].SetAndCreateView(this.viewTypes[i]);
      yield return (object) new WaitForSeconds(0.5f);
    }
    timer = 0.0f;
    cameraStartPos = mainCameraTransform.position;
    targetCameraPos = cameraOriginalPostion;
    while ((double) timer < 1.2000000476837158)
    {
      mainCameraTransform.position = Vector3.Lerp(cameraStartPos, targetCameraPos, timer / 1.2f);
      timer += Time.deltaTime;
      yield return (object) null;
    }
    this.onEndAllEventAction.SafeInvoke();
    Object.Destroy((Object) this);
  }

  private void OnDestroy()
  {
    for (int index = 0; index < this.unlockedPortalList.Count; ++index)
    {
      if (this.unlockedPortalList[index].viewType != this.viewTypes[index])
        this.unlockedPortalList[index].SetAndCreateView(this.viewTypes[index]);
    }
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_EVENT, false);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, false);
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.UNLOCK_EVENT;
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    ((Behaviour) MonoBehaviourSingleton<InGameCameraManager>.I).enabled = true;
    MonoBehaviourSingleton<InGameCameraManager>.I.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
  }
}
