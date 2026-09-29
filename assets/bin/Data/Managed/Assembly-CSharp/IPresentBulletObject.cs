// Decompiled with JetBrains decompiler
// Type: IPresentBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public interface IPresentBulletObject
{
  void Initialize(int id, BulletData bulletData, Transform transform);

  void SetPosition(Vector3 position);

  void SetSkillParam(SkillInfo.SkillParam skillParam);

  int GetPresentBulletId();

  void OnPicked();
}
