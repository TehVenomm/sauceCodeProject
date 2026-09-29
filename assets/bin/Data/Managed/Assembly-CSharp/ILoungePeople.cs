// Decompiled with JetBrains decompiler
// Type: ILoungePeople
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public interface ILoungePeople
{
  List<LoungePlayer> loungePlayers { get; }

  bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry);

  bool ChangeEquipLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry);

  bool DestroyLoungePlayer(int id);

  void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type);

  void MoveLoungePlayer(int id, Vector3 targetPos);

  void OnDestroyLoungePlayer(LoungePlayer chara);

  void UpdateLoungePlayersInfo(PartyModel.SlotInfo slotInfo);

  LoungePlayer GetLoungePlayer(int id);
}
