// Decompiled with JetBrains decompiler
// Type: ResourceLoad
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ResourceLoad : DisableNotifyMonoBehaviour
{
  public BetterList<ResourceObject> list;

  public bool destroyNotify { set; get; }

  public void SetReference(ResourceObject resobj)
  {
    if (resobj == null || Object.op_Equality(resobj.obj, (Object) null))
      return;
    ++resobj.refCount;
    if (this.list == null)
      return;
    this.list.Add(resobj);
  }

  public void SetReference(ResourceObject[] resobjs)
  {
    if (resobjs == null)
      return;
    int index1 = 0;
    for (int length = resobjs.Length; index1 < length; ++index1)
    {
      if (resobjs[index1] != null)
        ++resobjs[index1].refCount;
    }
    if (this.list == null)
      return;
    int index2 = 0;
    for (int length = resobjs.Length; index2 < length; ++index2)
    {
      if (resobjs[index2] != null)
        this.list.Add(resobjs[index2]);
    }
  }

  public bool IsStock(ResourceObject obj)
  {
    return obj.category == RESOURCE_CATEGORY.EFFECT_UI || obj.category == RESOURCE_CATEGORY.EFFECT_ACTION && obj.name != null && !obj.name.Contains("_bg_");
  }

  protected override void OnDisable()
  {
    if (this.destroyNotify)
      return;
    base.OnDisable();
  }

  private void OnDestroy()
  {
    if (this.destroyNotify)
      base.OnDisable();
    if (AppMain.isApplicationQuit || this.list == null || this.list.buffer == null)
      return;
    MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(this.list.buffer);
  }

  public static ResourceLoad GetResourceLoad(MonoBehaviour mono_behaviour, bool destroy_notify = false)
  {
    ResourceLoad resourceLoad = ((Component) mono_behaviour).gameObject.GetComponent<ResourceLoad>();
    if (Object.op_Equality((Object) resourceLoad, (Object) null))
    {
      resourceLoad = ((Component) mono_behaviour).gameObject.AddComponent<ResourceLoad>();
      resourceLoad.SetNotifyMaster((DisableNotifyMonoBehaviour) MonoBehaviourSingleton<ResourceManager>.I);
      resourceLoad.destroyNotify = destroy_notify;
    }
    return resourceLoad;
  }

  public static void LoadIconTexture(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string name,
    System.Action load_start_callback,
    Action<Texture> loaded_callback,
    bool isEventAsset = false)
  {
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
    if (cachedResourceObject != null)
    {
      loaded_callback(cachedResourceObject.obj as Texture);
    }
    else
    {
      if (load_start_callback != null)
        load_start_callback();
      if (string.IsNullOrEmpty(name))
        loaded_callback((Texture) null);
      else if (ResourceDefine.types[(int) category] == ResourceManager.CATEGORY_TYPE.HASH256)
        MonoBehaviourSingleton<ResourceManager>.I.LoadAssetBundle((isEventAsset ? 1 : 0) != 0, (object) ResourceLoad.GetResourceLoad(mono_behaviour, true), category, category.ToHash256String(name), new string[1]
        {
          name
        }, new ResourceManager.LoadComplateDelegate(ResourceLoad.OnLoadIconTextureComplate), new ResourceManager.LoadErrorDelegate(ResourceLoad.OnLoadIconTextureError), userData: (object) loaded_callback);
      else
        MonoBehaviourSingleton<ResourceManager>.I.LoadAssetBundle(isEventAsset, (object) ResourceLoad.GetResourceLoad(mono_behaviour, true), category, name, new ResourceManager.LoadComplateDelegate(ResourceLoad.OnLoadIconTextureComplate), new ResourceManager.LoadErrorDelegate(ResourceLoad.OnLoadIconTextureError), userData: (object) loaded_callback);
    }
  }

  public static void LoadRushResultIconTexture(UITexture ui_tex, int quest_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.RUSH_RESULT_IMAGE, ResourceName.GetRushResultIconName(quest_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadRushResultTitleTexture(UITexture ui_tex, int quest_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.RUSH_RESULT_IMAGE, ResourceName.GetRushResultTitleName(quest_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadItemIconTexture(UITexture ui_tex, int icon_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadNPCIconTexture(UITexture ui_tex, int icon_id, bool is_smile)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.NPC_ICON, ResourceName.GetNPCIcon(icon_id, is_smile), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadEnemyIconTexture(UITexture ui_tex, int icon_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.ENEMY_ICON, ResourceName.GetEnemyIcon(icon_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadFieldIconTexture(
    UITexture ui_tex,
    FieldMapTable.FieldMapTableData fieldData)
  {
    string name = !fieldData.IsExistQuestIconId() ? ResourceName.GetQuestIcon(fieldData.stageName) : ResourceName.GetQuestIcon((int) fieldData.questIconId);
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.QUEST_ICON, name, (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadGatherPointIconTexture(UITexture ui_tex, uint icon_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.INGAME_GATHER_POINT, ResourceName.GetGatherPointIcon(icon_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadCommonImageTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetCommmonImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadShopImageTexture(
    UITexture ui_tex,
    uint image_id,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageName((int) image_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }), true);
  }

  public static void LoadShopImageOfferTexture(
    UITexture ui_tex,
    uint image_id,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageOfferName((int) image_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void LoadShopImageGemOfferTexture(
    UITexture ui_tex,
    uint image_id,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageGemOfferName((int) image_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void LoadShopImageMaterialTexture(
    UITexture ui_tex,
    string name,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, name, (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void LoadPointIconImageTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadGrayPointIconImageTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetGrayPointIconImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadCommonTexture(UITexture ui_tex, string tex_name)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, tex_name, (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadPointShopBannerTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointShopBannerImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }), true);
  }

  public static void LoadPointShopBGTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointSHopBGImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }), true);
  }

  public static void LoadHomePointSHopBannerTexture(UITexture ui_tex, uint image_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetHomePointSHopBannerImageName((int) image_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadEventBannerResultTexture(UITexture ui_tex, uint event_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.EVENT_BANNER_RESULT, ResourceName.GetQuestEventBannerResult((int) event_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadEventBannerResultBGTexture(UITexture ui_tex, uint event_id)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.EVENT_BANNER_RESULT, ResourceName.GetQuestEventBannerResultBG((int) event_id), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadWithSetUITexture(
    UITexture ui_tex,
    RESOURCE_CATEGORY category,
    string name)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, category, name, (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  public static void LoadBlackMarketOfferTexture(
    UITexture ui_tex,
    string imageName,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, $"DMO_{imageName}", (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void LoadBlackMarketIconTexture(
    UITexture ui_tex,
    string imageName,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, $"DMI_{imageName}", (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void LoadFortuneWheelIconTexture(
    UITexture ui_tex,
    string imageName,
    Action<Texture> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.SHOP_IMG, imageName, (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(tex);
    }));
  }

  public static void ItemIconLoadItemIconTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadAccessoryIconTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.ICON_ACCESSORY, ResourceName.GetAccessoryIcon(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadIconBGTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadQuestItemIconTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.ENEMY_ICON, ResourceName.GetEnemyIcon(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadEnemyIconItemTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.ENEMY_ICON_ITEM, ResourceName.GetEnemyIconItem(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadCommonTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetCommmonImageName(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadStampTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.UI_CHAT_STAMP, ResourceName.GetChatStamp(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void ItemIconLoadDegreeIconTexture(
    ItemIcon item_icon,
    DEGREE_TYPE type,
    Action<ItemIcon, Texture, DEGREE_TYPE> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetDegreeIcon(type), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, type);
    }));
  }

  public static void ItemIconLoadPointShopPointIconTexture(
    ItemIcon item_icon,
    int icon_id,
    Action<ItemIcon, Texture, int> callback)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(icon_id), (System.Action) null, (Action<Texture>) (tex =>
    {
      if (callback == null)
        return;
      callback(item_icon, tex, icon_id);
    }));
  }

  public static void LoadEvolveIconTexture(UITexture ui_tex, uint evolveId)
  {
    ResourceLoad.LoadIconTexture((MonoBehaviour) ui_tex, RESOURCE_CATEGORY.EVOLVE_ICON, ResourceName.GetEvolveIcon(evolveId), (System.Action) (() => ui_tex.mainTexture = (Texture) null), (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) ui_tex, (Object) null))
        return;
      ui_tex.mainTexture = tex;
    }));
  }

  private static void OnLoadIconTextureComplate(
    ResourceManager.LoadRequest request,
    ResourceObject[] objs)
  {
    ++objs[0].refCount;
    (request.userData as Action<Texture>)(objs[0].obj as Texture);
  }

  private static void OnLoadIconTextureError(
    ResourceManager.LoadRequest request,
    ResourceManager.ERROR_CODE error_node)
  {
    if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      (request.userData as Action<Texture>)(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.errorIcon);
    else
      (request.userData as Action<Texture>)((Texture) null);
  }

  public void ReleaseAllResources()
  {
    if (!MonoBehaviourSingleton<ResourceManager>.IsValid() || MonoBehaviourSingleton<ResourceManager>.I.cache == null || this.list == null || this.list.buffer == null)
      return;
    MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(this.list.buffer);
    this.list.Release();
  }
}
