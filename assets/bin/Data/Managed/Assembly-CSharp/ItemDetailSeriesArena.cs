// Decompiled with JetBrains decompiler
// Type: ItemDetailSeriesArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Linq;
using UnityEngine;

#nullable disable
public class ItemDetailSeriesArena : UIBehaviour
{
  public void SetUpItem(Transform t) => this.StartCoroutine(this.loadBanner(t));

  private IEnumerator loadBanner(Transform t)
  {
    Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR)).FirstOrDefault<Network.EventData>();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, ResourceName.GetEventBanner(eventData.bannerId));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Texture2D loadedObject = obj.loadedObject as Texture2D;
    if (Object.op_Inequality((Object) loadedObject, (Object) null))
    {
      Transform ctrl = this.FindCtrl(t, (Enum) ItemDetailSeriesArena.UI.TEX_EVENT_BANNER);
      this.SetTexture(ctrl, (Texture) loadedObject);
      this.SetActive(ctrl, true);
    }
  }

  protected enum UI
  {
    TEX_EVENT_BANNER,
  }
}
