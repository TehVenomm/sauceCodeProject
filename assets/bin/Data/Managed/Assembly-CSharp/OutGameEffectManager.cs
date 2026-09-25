// Decompiled with JetBrains decompiler
// Type: OutGameEffectManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class OutGameEffectManager : MonoBehaviourSingleton<OutGameEffectManager>
{
  private const string TOUCH_EFFECT_NAME = "ef_ui_tap_01";
  private const int MAX_TOUHC_EFFECT = 5;
  private const string AUTO_MOVE_EFFECT_NAME = "ef_ui_downenergy_01";
  private const string SILHOUETTE_EFFECT_NAME = "ef_ui_questselect_01";
  private LoadObject touchEffectPrefab;
  private int effectCount;
  private LoadObject moveEffectPrefab;
  private Transform autoEventEffect;
  private LoadObject silhouetteEffectPrefab;
  private Transform silhouetteEffect;
  private MAIN_SCENE sceneNow = MAIN_SCENE.MAX;
  private Transform sceneButtonEffect;

  public Vector3 lastTouchEffectPos { get; private set; }

  private IEnumerator Start()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    this.touchEffectPrefab = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_tap_01");
    this.moveEffectPrefab = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_downenergy_01");
    this.silhouetteEffectPrefab = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_01");
    ResourceManager.enableCache = true;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (MonoBehaviourSingleton<InputManager>.IsValid())
      InputManager.OnTouchOnAlways += new InputManager.OnTouchDelegate(this.OnTouchOn);
    // ISSUE: method pointer
    rymFXManager.DestroyFxDelegate = (rymFXManager.DestroyFxFunc) Delegate.Combine((Delegate) rymFXManager.DestroyFxDelegate, (Delegate) new rymFXManager.DestroyFxFunc((object) this, __methodptr(OnDestroyFx)));
  }

  private void OnDestroy()
  {
    if (MonoBehaviourSingleton<InputManager>.IsValid())
      InputManager.OnTouchOnAlways -= new InputManager.OnTouchDelegate(this.OnTouchOn);
    // ISSUE: method pointer
    rymFXManager.DestroyFxDelegate = new rymFXManager.DestroyFxFunc((object) this, __methodptr(OnDestroyFx));
  }

  private void OnTouchOn(InputManager.TouchInfo touch_info)
  {
    if (MonoBehaviourSingleton<PuniConManager>.IsValid() && touch_info.enable)
      return;
    this.PopTouchEffect(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(touch_info.position.ToVector3XY()));
  }

  private void OnDestroyFx(rymFX fx)
  {
    if (!(((Object) fx).name == "ef_ui_tap_01"))
      return;
    --this.effectCount;
  }

  public void PopTouchEffect(Vector3 pos)
  {
    this.lastTouchEffectPos = pos;
    if (this.touchEffectPrefab == null || Object.op_Equality(this.touchEffectPrefab.loadedObject, (Object) null) || this.effectCount >= 5)
      return;
    ResourceUtility.Realizes(this.touchEffectPrefab.loadedObject, ((Component) MonoBehaviourSingleton<UIManager>.I.uiCamera).transform, 5).position = pos;
    ++this.effectCount;
  }

  public void ShowAutoEventEffect()
  {
    if (Object.op_Inequality((Object) this.autoEventEffect, (Object) null))
      return;
    this.autoEventEffect = ResourceUtility.Realizes(this.moveEffectPrefab.loadedObject, ((Component) MonoBehaviourSingleton<UIManager>.I.uiCamera).transform, 5);
    ((Component) this.autoEventEffect).GetComponent<rymFX>().ChangeRenderQueue = 3999;
    this.autoEventEffect.position = this.lastTouchEffectPos;
    ((Component) this.autoEventEffect).gameObject.AddComponent<TransformInterpolator>();
  }

  public void HideAutoEventEffect()
  {
    if (Object.op_Equality((Object) this.autoEventEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.autoEventEffect).gameObject);
    this.autoEventEffect = (Transform) null;
  }

  public Coroutine MoveAutoEventEffect(Vector3 to)
  {
    if (Object.op_Equality((Object) this.autoEventEffect, (Object) null))
      return (Coroutine) null;
    TransformInterpolator component = ((Component) this.autoEventEffect).GetComponent<TransformInterpolator>();
    to = this.autoEventEffect.parent.InverseTransformPoint(to);
    Vector3 vector3 = Vector3.op_Subtraction(to, this.autoEventEffect.localPosition);
    Vector3 add_value = Vector3.op_Multiply(Vector3.Cross(((Vector3) ref vector3).normalized, Vector3.forward), Random.Range(-64f, 64f));
    add_value.z = 0.0f;
    component.Translate(0.25f, to, add_value: add_value, add_curve: Curves.arcHalfCurve);
    return component.Wait();
  }

  public void ShowSilhoutteffect(Transform t, int layer)
  {
    if (Object.op_Inequality((Object) this.silhouetteEffect, (Object) null))
      return;
    this.silhouetteEffect = ResourceUtility.Realizes(this.silhouetteEffectPrefab.loadedObject, t, layer);
    ((Component) this.silhouetteEffect).transform.localPosition = new Vector3(0.0f, 0.0f, 500f);
  }

  public void HideSilhoutteEffect()
  {
    if (Object.op_Equality((Object) this.silhouetteEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.silhouetteEffect).gameObject);
    this.silhouetteEffect = (Transform) null;
  }

  public void UpdateSceneButtonEffect(MAIN_SCENE scene, Transform button)
  {
    if (this.sceneNow == scene)
      return;
    this.sceneNow = scene;
  }

  public void ReleaseSceneButtonEffect()
  {
    if (Object.op_Equality((Object) this.sceneButtonEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.sceneButtonEffect).gameObject);
    this.sceneButtonEffect = (Transform) null;
    this.sceneNow = MAIN_SCENE.MAX;
  }
}
