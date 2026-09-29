// Decompiled with JetBrains decompiler
// Type: IFieldGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public interface IFieldGimmickObject
{
  int GetId();

  void Initialize(FieldMapTable.FieldGimmickPointTableData pointData);

  void RequestDestroy();

  void SetTransform(Transform trans);

  Transform GetTransform();

  string GetObjectName();

  float GetTargetRadius();

  float GetTargetSqrRadius();

  void UpdateTargetMarker(bool isNear);

  bool IsSearchableNearest();
}
