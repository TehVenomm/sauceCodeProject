// Decompiled with JetBrains decompiler
// Type: DegreePlate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DegreePlate : MonoBehaviour
{
  private const int DEGREE_PART = 4;
  private const string PREFAB_NAME = "DegreePlate";
  private const string UNKNOWN_FRAME = "DF_UNKNOWN";
  [SerializeField]
  private UILabel mText;
  [SerializeField]
  private UITexture mFrame;
  private string mFrameName;

  public static void Create(
    MonoBehaviour call_mono,
    List<int> degreeIds,
    bool isButton,
    Action<DegreePlate> onFinish)
  {
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(RESOURCE_CATEGORY.UI, nameof (DegreePlate));
    if (cachedResourceObject != null)
      (Object.Instantiate(cachedResourceObject.obj) as GameObject).GetComponent<DegreePlate>().Initialize(degreeIds, isButton, onFinish);
    else
      MonoBehaviourSingleton<ResourceManager>.I.Load((object) ResourceLoad.GetResourceLoad(call_mono, true), RESOURCE_CATEGORY.UI, nameof (DegreePlate), (ResourceManager.LoadComplateDelegate) ((x, y) =>
      {
        ++y[0].refCount;
        (Object.Instantiate(y[0].obj) as GameObject).GetComponent<DegreePlate>().Initialize(degreeIds, isButton, onFinish);
      }), (ResourceManager.LoadErrorDelegate) null);
  }

  public void Initialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
  {
    this.StartCoroutine(this.DoInitialize(degreeIds, isButton, onFinish));
  }

  private IEnumerator DoInitialize(
    List<int> degreeIds,
    bool isButton,
    Action<DegreePlate> onFinish)
  {
    if (degreeIds == null || 4 != degreeIds.Count)
    {
      if (onFinish != null)
        onFinish((DegreePlate) null);
    }
    else
    {
      DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint) degreeIds[0]);
      if (data == null || data.type != DEGREE_TYPE.FRAME && data.type != DEGREE_TYPE.SPECIAL_FRAME)
      {
        if (onFinish != null)
          onFinish((DegreePlate) null);
      }
      else
      {
        string str;
        if (data.type == DEGREE_TYPE.SPECIAL_FRAME)
          str = "";
        else
          str = $"{Singleton<DegreeTable>.I.GetData((uint) degreeIds[1]).name} {Singleton<DegreeTable>.I.GetData((uint) degreeIds[2]).name} {Singleton<DegreeTable>.I.GetData((uint) degreeIds[3]).name}";
        this.mText.text = str;
        ((Component) this.mText).gameObject.SetActive(false);
        yield return (object) this.StartCoroutine(this._SetFrame(ResourceName.GetDegreeFrameName(degreeIds[0])));
        ((Component) this.mText).gameObject.SetActive(true);
        this.SetEnableButtonCollider(isButton);
        if (onFinish != null)
          onFinish(this);
      }
    }
  }

  public void SetFrame(int degreeId)
  {
    this.StartCoroutine(this._SetFrame(ResourceName.GetDegreeFrameName(degreeId)));
  }

  public void SetUnknownFrame() => this.StartCoroutine(this._SetFrame("DF_UNKNOWN"));

  private IEnumerator _SetFrame(string frameName)
  {
    if (!(this.mFrameName == frameName))
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject frameTexture = loadingQueue.Load(true, RESOURCE_CATEGORY.DEGREE_FRAME, frameName);
      ((Behaviour) this.mFrame).enabled = false;
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.mFrame.mainTexture = frameTexture.loadedObject as Texture;
      this.mFrameName = frameName;
      ((Behaviour) this.mFrame).enabled = true;
    }
  }

  public void SetEnableButtonCollider(bool enable)
  {
    ((Component) ((Component) this).transform).GetComponent<Collider>().enabled = enable;
  }
}
