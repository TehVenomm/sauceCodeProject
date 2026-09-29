// Decompiled with JetBrains decompiler
// Type: CoopClientCollector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CoopClientCollector
{
  public const int MAX_CLIENT = 8;
  private CoopClient[] clientList = new CoopClient[8];

  public CoopClient GetAt(int idx)
  {
    return idx < 0 || idx >= 8 ? (CoopClient) null : this.clientList[idx];
  }

  public void Add(CoopClient client)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      if (Object.op_Equality((Object) this.clientList[index], (Object) null))
      {
        this.clientList[index] = client;
        break;
      }
    }
  }

  public void Remove(CoopClient client)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      if (Object.op_Equality((Object) this.clientList[index], (Object) client))
      {
        this.clientList[index] = (CoopClient) null;
        break;
      }
    }
  }

  public void Clear()
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
      this.clientList[index] = (CoopClient) null;
  }

  public CoopClient Find(Predicate<CoopClient> predicate)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      CoopClient client = this.clientList[index];
      if (!Object.op_Equality((Object) client, (Object) null) && predicate(client))
        return client;
    }
    return (CoopClient) null;
  }

  public int IndexOf(CoopClient client)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      if (this.clientList[index].userId == client.userId)
        return index;
    }
    return -1;
  }

  public void ForEach(Action<CoopClient> action)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      CoopClient client = this.clientList[index];
      if (!Object.op_Equality((Object) client, (Object) null))
        action(client);
    }
  }

  public Player FindPlayer(Predicate<Player> predicate)
  {
    int index = 0;
    for (int length = this.clientList.Length; index < length; ++index)
    {
      CoopClient client = this.clientList[index];
      if (!Object.op_Equality((Object) client, (Object) null))
      {
        Player player = client.GetPlayer();
        if (Object.op_Inequality((Object) player, (Object) null) && predicate(player))
          return player;
      }
    }
    return (Player) null;
  }

  public CoopClient FindStageHost(int stage_id)
  {
    return this.Find((Predicate<CoopClient>) (c => c.stageId == stage_id && c.isStageHost));
  }

  public CoopClient FindPartyOwner() => this.Find((Predicate<CoopClient>) (c => c.isPartyOwner));

  public CoopClient FindByClientId(int client_id)
  {
    for (int index = 0; index < this.clientList.Length; ++index)
    {
      if (!Object.op_Equality((Object) this.clientList[index], (Object) null))
      {
        CoopClient client = this.clientList[index];
        if (client.clientId == client_id)
          return client;
      }
    }
    return (CoopClient) null;
  }

  public CoopClient FindByToken(string token)
  {
    return this.Find((Predicate<CoopClient>) (c => c.userToken == token));
  }

  public CoopClient FindByPlayerId(int player_id)
  {
    return this.Find((Predicate<CoopClient>) (c => c.playerId == player_id));
  }

  public CoopClient FindByUserId(int user_id)
  {
    return this.Find((Predicate<CoopClient>) (c => c.userId == user_id));
  }

  public bool HasSeriesProgress()
  {
    return Object.op_Inequality((Object) this.Find((Predicate<CoopClient>) (c => !c.isLeave && !c.IsBattleEnd() && !c.isBattleRetire && c.IsStageRequest() && !c.isSeriesProgressEnd)), (Object) null);
  }

  public bool HasLoadingPlayer()
  {
    return Object.op_Inequality((Object) this.FindPlayer((Predicate<Player>) (p => p.isLoading)), (Object) null);
  }
}
