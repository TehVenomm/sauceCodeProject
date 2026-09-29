// Decompiled with JetBrains decompiler
// Type: GuildItemManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildItemManager
{
  private static GuildItemManager _instance;
  private readonly List<GuildItemInfoModel.EmblemInfo> _emblemInfos = new List<GuildItemInfoModel.EmblemInfo>();
  private bool _init;

  public static GuildItemManager I
  {
    get => GuildItemManager._instance ?? (GuildItemManager._instance = new GuildItemManager());
  }

  public List<GuildItemInfoModel.EmblemInfo> GetEmblemInfos() => this._emblemInfos;

  public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer1Infos()
  {
    return this._emblemInfos.FindAll((Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.type == 0)).ToArray();
  }

  public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer2Infos()
  {
    return this._emblemInfos.FindAll((Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.type == 1)).ToArray();
  }

  public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer3Infos()
  {
    return this._emblemInfos.FindAll((Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.type == 2)).ToArray();
  }

  public string GetItemSprite(int id)
  {
    GuildItemInfoModel.EmblemInfo emblemInfo = this._emblemInfos.Find((Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.id == id));
    return emblemInfo == null ? "" : emblemInfo.image;
  }

  public int[] RandomEmblem(bool isFree)
  {
    int[] numArray = new int[3];
    if (isFree)
    {
      GuildItemInfoModel.EmblemInfo[] all1 = Array.FindAll<GuildItemInfoModel.EmblemInfo>(this.GetEmblemLayer1Infos(), (Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.price == 0));
      numArray[0] = all1[Random.Range(0, all1.Length)].id;
      GuildItemInfoModel.EmblemInfo[] all2 = Array.FindAll<GuildItemInfoModel.EmblemInfo>(this.GetEmblemLayer2Infos(), (Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.price == 0));
      numArray[1] = all2[Random.Range(0, all2.Length)].id;
      GuildItemInfoModel.EmblemInfo[] all3 = Array.FindAll<GuildItemInfoModel.EmblemInfo>(this.GetEmblemLayer3Infos(), (Predicate<GuildItemInfoModel.EmblemInfo>) (x => x.price == 0));
      numArray[2] = all3[Random.Range(0, all3.Length)].id;
    }
    return numArray;
  }

  public void Init(System.Action callback)
  {
    if (this._init)
    {
      if (callback == null)
        return;
      callback();
    }
    else
    {
      this._emblemInfos.Clear();
      GuildItemInfoModel.RequestAllItemInfo postData = new GuildItemInfoModel.RequestAllItemInfo();
      Protocol.SendAsync<GuildItemInfoModel.RequestAllItemInfo, GuildItemInfoModel>(GuildItemInfoModel.RequestAllItemInfo.path, postData, (Action<GuildItemInfoModel>) (ret =>
      {
        if (ret.Error == Error.None)
        {
          foreach (GuildItemInfoModel.EmblemInfo emblemInfo in ret.result.emblem)
            this._emblemInfos.Add(emblemInfo);
        }
        this._init = true;
        if (callback == null)
          return;
        callback();
      }));
    }
  }
}
