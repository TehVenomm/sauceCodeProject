// Decompiled with JetBrains decompiler
// Type: EffectSubLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EffectSubLoader : MonoBehaviourSingleton<EffectSubLoader>
{
  private Queue<IEnumerator> actions = new Queue<IEnumerator>();
  private LoadingQueue loadQueue;

  public static void CreateInstance()
  {
    if (MonoBehaviourSingleton<EffectSubLoader>.IsValid())
      return;
    Utility.CreateGameObjectAndComponent(nameof (EffectSubLoader));
  }

  public void CacheAnimDataUseResource(
    AnimEventData animEventData,
    LoadingQueue.EffectNameAnalyzer name_analyzer = null,
    List<AnimEventData.EventData> cntAtkDataList = null)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    this.loadQueue.CacheAnimDataUseResource(animEventData, name_analyzer, cntAtkDataList);
  }

  public void CacheEffect(RESOURCE_CATEGORY category, string ename)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    this.loadQueue.CacheEffect(category, ename);
  }

  public void CacheBulletDataUseResource(BulletData bulletData, Player player = null)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    this.loadQueue.CacheBulletDataUseResource(bulletData, player);
  }

  public void CacheSE(int se_id, List<LoadObject> los = null)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    this.loadQueue.CacheSE(se_id, los);
  }

  public void StartLoad(System.Action onFinish = null)
  {
    this.StartCoroutine(this.ProcessAction(onFinish));
  }

  private IEnumerator ProcessAction(System.Action onFinish)
  {
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (onFinish != null)
      onFinish();
  }
}
