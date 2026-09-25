// Decompiled with JetBrains decompiler
// Type: Ookushi_AssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class Ookushi_AssetBundle : MonoBehaviour
{
  private IEnumerator Start()
  {
    while (!AppMain.isInitialized)
      yield return (object) null;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestRequestItem");
    yield return (object) loadingQueue.Wait();
    ResourceUtility.Instantiate<Object>(lo.loadedObject);
  }
}
