// Decompiled with JetBrains decompiler
// Type: Network.UserStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class UserStatus
{
  public int sex;
  public int faceId;
  public int hairId;
  public int hairColorId;
  public int skinId;
  public int voiceId;
  public XorInt level = (XorInt) 0;
  public XorInt exp = (XorInt) 0;
  public XorInt expPrev = (XorInt) 0;
  public XorInt expNext = (XorInt) 0;
  public XorInt hp = (XorInt) 0;
  public XorInt atk = (XorInt) 0;
  public XorInt def = (XorInt) 0;
  public int money;
  public int crystal;
  public int eSetNo;
  public int ueSetNo;
  public int titleId;
  public int maxFollow;
  public int maxEquipItem;
  public int maxSkillItem;
  public int maxAbilityItem;
  public int tutorialStep;
  public string tutorialBit;
  public int tutorialQuestId;
  public int researchLv;
  public int questGrade;
  public int fieldGrade;
  public int present;
  public int clanId;
  public string armorUniqId;
  public string armUniqId;
  public string legUniqId;
  public string helmUniqId;
  public int showHelm;
  public DateTime nextDonationTime;
  public int fairyNum;
  public int maxEquipItemTargetNum;

  public int Money
  {
    get => this.money;
    set => this.money = value;
  }

  public int Crystal
  {
    get => this.crystal;
    set => this.crystal = value;
  }

  public int Exp
  {
    get => (int) this.exp;
    set => this.exp = (XorInt) value;
  }

  public int ExpPrev
  {
    get => (int) this.expPrev;
    set => this.expPrev = (XorInt) value;
  }

  public int ExpNext
  {
    get => (int) this.expNext;
    set => this.expNext = (XorInt) value;
  }

  public int RelativeExp => this.Exp - this.ExpPrev;

  public int RelativeExpNext => this.ExpNext - this.ExpPrev;

  public float ExpProgress01
  {
    get => this.RelativeExpNext > 0 ? (float) this.RelativeExp / (float) this.RelativeExpNext : 1f;
  }

  public long TutorialBit => long.Parse(this.tutorialBit);

  public bool IsTutorialBitReady => this.tutorialBit != null;
}
