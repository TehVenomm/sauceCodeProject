// Decompiled with JetBrains decompiler
// Type: IFieldGimmickCannon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public interface IFieldGimmickCannon : IFieldGimmickObject
{
  Vector3 GetPosition();

  Transform GetCannonTransform();

  Transform GetBaseTransform();

  Vector3 GetBaseTransformForward();

  bool IsUsing();

  bool IsAbleToUse();

  bool IsCooling();

  bool IsAimCamera();

  void OnBoard(Player player);

  void OnLeave();

  void Shot();

  void ApplyCannonVector(Vector3 cannonVec);
}
