// Decompiled with JetBrains decompiler
// Type: CustomEmblem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CustomEmblem : GameSection
{
  private int mLayer1ID;
  private int mLayer2ID;
  private int mLayer3ID;

  public override void Initialize()
  {
    this.UpdateList();
    this.mLayer1ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[0];
    this.mLayer2ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[1];
    this.mLayer3ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[2];
    this.UpdateEmblems();
    base.Initialize();
  }

  public void UpdateList()
  {
    GuildItemInfoModel.EmblemInfo[] infos = GuildItemManager.I.GetEmblemLayer1Infos();
    this.SetDynamicList((Enum) CustomEmblem.UI.GRD_LIST_1, "EmblemLayer1Item", infos.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, infos[i])));
    infos = GuildItemManager.I.GetEmblemLayer2Infos();
    this.SetDynamicList((Enum) CustomEmblem.UI.GRD_LIST_2, "EmblemLayer2Item", infos.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, infos[i])));
    infos = GuildItemManager.I.GetEmblemLayer3Infos();
    this.SetDynamicList((Enum) CustomEmblem.UI.GRD_LIST_3, "EmblemLayer3Item", infos.Length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, infos[i])));
  }

  protected void SetListItem(
    int i,
    Transform t,
    bool is_recycle,
    GuildItemInfoModel.EmblemInfo data)
  {
    this.SetSprite(t, (Enum) CustomEmblem.UI.SPR_EMBLEM_IMAGE, data.image);
  }

  private void UpdateEmblems()
  {
    if (this.mLayer1ID == -1)
      this.mLayer1ID = GuildManager.sDefaultEmblemIDLayer1;
    this.SetSprite((Enum) CustomEmblem.UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(this.mLayer1ID));
    if (this.mLayer2ID == -1)
      this.mLayer2ID = GuildManager.sDefaultEmblemIDLayer2;
    this.SetSprite((Enum) CustomEmblem.UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(this.mLayer2ID));
    if (this.mLayer3ID == -1)
      this.mLayer3ID = GuildManager.sDefaultEmblemIDLayer3;
    this.SetSprite((Enum) CustomEmblem.UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(this.mLayer3ID));
  }

  private void OnQuery_RANDOM_EMBLEM()
  {
    int[] numArray = GuildItemManager.I.RandomEmblem(true);
    this.mLayer1ID = numArray[0];
    this.mLayer2ID = numArray[1];
    this.mLayer3ID = numArray[2];
    this.UpdateEmblems();
  }

  private void OnQuery_DONE()
  {
    MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(0, this.mLayer1ID);
    MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(1, this.mLayer2ID);
    MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(2, this.mLayer3ID);
  }

  protected enum UI
  {
    GRD_LIST_1,
    GRD_LIST_2,
    GRD_LIST_3,
    SPR_GUILD_EMBLEM_1,
    SPR_GUILD_EMBLEM_2,
    SPR_GUILD_EMBLEM_3,
    LAYER_1,
    LAYER_2,
    LAYER_3,
    SPR_EMBLEM_IMAGE,
  }
}
