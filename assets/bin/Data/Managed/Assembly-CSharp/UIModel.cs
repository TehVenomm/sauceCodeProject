// Decompiled with JetBrains decompiler
// Type: UIModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIModel : MonoBehaviour
{
  private static List<Transform> _models;
  private const float OffSetX = 5000f;
  private Transform model;
  private Transform _transform;
  private bool isLoading;

  public static UIModel Get(Transform t)
  {
    UIModel uiModel = ((Component) t).GetComponent<UIModel>();
    if (Object.op_Equality((Object) uiModel, (Object) null))
      uiModel = ((Component) t).gameObject.AddComponent<UIModel>();
    return uiModel;
  }

  private void OnDisable()
  {
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    ((Component) this.model).gameObject.SetActive(false);
  }

  private void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    ((Component) this.model).gameObject.SetActive(true);
  }

  public static void UpdateModelOffset()
  {
    UIModel.models.DoAction<Transform>((Action<Transform, int>) ((t, i) => t.position = new Vector3((float) (i + 1) * 5000f, 0.0f, 0.0f)));
  }

  public static List<Transform> models
  {
    get
    {
      if (UIModel._models == null)
        UIModel._models = new List<Transform>();
      return UIModel._models;
    }
  }

  private void Awake() => this._transform = ((Component) this).transform;

  public void Init(string resource_name)
  {
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      ((Component) this.model).gameObject.SetActive(true);
    }
    else
    {
      if (this.isLoading)
        return;
      this.StartCoroutine(this.DoInit(resource_name));
    }
  }

  public void Remove()
  {
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    UIModel.models.Remove(this.model);
    Object.Destroy((Object) ((Component) this.model).gameObject);
    this.model = (Transform) null;
  }

  public void SetActive(bool active)
  {
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    ((Component) this.model).gameObject.SetActive(active);
  }

  private IEnumerator DoInit(string resource_name)
  {
    this.isLoading = true;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject load_object = (LoadObject) loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.COMMON, resource_name);
    yield return (object) loadingQueue.Wait();
    this.model = load_object.Realizes(MonoBehaviourSingleton<AppMain>.I._transform);
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      UIModel.models.Add(this.model);
      UIModel.UpdateModelOffset();
    }
    this.isLoading = false;
  }

  private void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.model, (Object) null))
    {
      UIModel.models.Remove(this.model);
      Object.DestroyImmediate((Object) ((Component) this.model).gameObject);
    }
    UIModel.UpdateModelOffset();
  }
}
