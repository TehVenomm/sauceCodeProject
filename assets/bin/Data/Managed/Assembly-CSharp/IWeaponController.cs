// Decompiled with JetBrains decompiler
// Type: IWeaponController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public interface IWeaponController
{
  void Init(Player _player);

  void OnLoadComplete();

  void Update();

  void OnEndAction();

  void OnActDead();

  void OnActReaction();

  void OnActAvoid();

  void OnActSkillAction();

  void OnRelease();

  void OnActAttack(int id);

  void OnBuffStart(BuffParam.BuffData data);

  void OnBuffEnd(BuffParam.BUFFTYPE type);

  void OnChangeWeapon();

  void OnAttackedHitFix(AttackedHitStatusFix status);
}
