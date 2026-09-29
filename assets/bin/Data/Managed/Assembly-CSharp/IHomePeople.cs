// Decompiled with JetBrains decompiler
// Type: IHomePeople
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public interface IHomePeople
{
  bool isInitialized { get; }

  bool isPeopleInitialized { get; }

  HomeSelfCharacter selfChara { get; }

  List<HomeCharacterBase> charas { get; }

  void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback);

  Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint);

  HomeNPCCharacter GetHomeNPCCharacter(int npcID);

  ILoungePeople CastToLoungePeople();

  void OnDestroyHomeCharacter(HomeCharacterBase chara);
}
