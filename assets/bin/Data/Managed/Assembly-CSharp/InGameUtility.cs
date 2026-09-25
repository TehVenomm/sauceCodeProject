// Decompiled with JetBrains decompiler
// Type: InGameUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class InGameUtility
{
  public const float COEFFICIENT_DAMAGE = 4.5f;
  public static readonly Vector3 VECTOR3_ZERO = Vector3.zero;
  public static readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;

  public static AtkAttribute CalcPlayerATK(InGameUtility.PlayerAtkCalcData calcData)
  {
    AtkAttribute srcAtk = new AtkAttribute();
    AttackHitInfo atkInfo = calcData.atkInfo;
    if (atkInfo != null)
      srcAtk.Add(atkInfo.atk);
    SkillInfo.SkillParam skillParam = calcData.skillParam;
    if (skillParam != null && atkInfo != null && atkInfo.isSkillReference)
      srcAtk.Add(skillParam.atk);
    if (atkInfo == null)
      srcAtk.Add(calcData.weaponAtk);
    else if (atkInfo.IsReferenceAtkValue)
      srcAtk.Add(calcData.weaponAtk);
    srcAtk.normal += calcData.statusAtk;
    srcAtk.Add(calcData.guardEquipAtk);
    float num = srcAtk.normal;
    ELEMENT_TYPE type1 = srcAtk.GetElementType();
    AtkAttribute val1 = new AtkAttribute();
    val1.Copy(srcAtk);
    val1.Mul(calcData.passiveAtkRate);
    srcAtk.Add(val1);
    srcAtk.Add(calcData.passiveAtkConstant);
    if ((double) calcData.passiveAtkAllElementConstant != 0.0)
      srcAtk.AddElementValueWithCheck(calcData.passiveAtkAllElementConstant);
    srcAtk.CheckMinus();
    AtkAttribute val2 = new AtkAttribute();
    val2.Copy(srcAtk);
    val2.Mul(calcData.buffAtkRate);
    srcAtk.Add(val2);
    srcAtk.Add(calcData.buffAtkConstant);
    if ((double) calcData.buffAtkAllElementConstant > 0.0)
      srcAtk.AddElementValueWithCheck(calcData.buffAtkAllElementConstant);
    if (skillParam != null && atkInfo != null && atkInfo.isSkillReference)
    {
      if (skillParam.tableData.skillAtkType == ELEMENT_TYPE.MAX)
        type1 = ELEMENT_TYPE.MAX;
      else
        num = 0.0f;
      ELEMENT_TYPE type2 = skillParam.tableData.skillAtkType;
      float val3 = skillParam.atkRate;
      if (calcData.atkInfo.skillElementIndex > 0 && skillParam.tableData.GetAttackElementNum() > calcData.atkInfo.skillElementIndex)
      {
        type2 = skillParam.tableData.GetAttackElementByIndex(calcData.atkInfo.skillElementIndex);
        val3 = skillParam.GetAttackRateByIndex(calcData.atkInfo.skillElementIndex);
      }
      srcAtk.ChangeElementType(type2);
      srcAtk.Mul(val3);
    }
    else if (calcData.isAtkElementOnly && type1 != ELEMENT_TYPE.MAX)
    {
      num = 0.0f;
      srcAtk.ChangeElementType(type1);
    }
    srcAtk.CheckMinus();
    if ((double) num > 0.0 && (double) srcAtk.normal < 1.0)
      srcAtk.normal = 1f;
    if (type1 != ELEMENT_TYPE.MAX && srcAtk.GetElementType() == ELEMENT_TYPE.MAX)
      srcAtk.SetTargetElement(type1, 1f);
    return srcAtk;
  }

  public static AtkAttribute CalcEnemyATK(InGameUtility.EnemyAtkCalcData calcData)
  {
    AtkAttribute srcAtk = new AtkAttribute();
    AttackHitInfo atkInfo = calcData.atkInfo;
    if (atkInfo != null)
      srcAtk.Add(atkInfo.atk);
    AtkAttribute val1 = new AtkAttribute();
    val1.Set(0.0f);
    val1.Add(calcData.buffAtkRate);
    AtkAttribute val2 = new AtkAttribute();
    val2.Copy(srcAtk);
    val2.Mul(val1);
    srcAtk.Add(val2);
    srcAtk.Add(calcData.buffAtkConstant);
    if ((double) calcData.buffAtkAllElementConstant > 0.0)
      srcAtk.AddElementValueWithCheck(calcData.buffAtkAllElementConstant);
    return srcAtk;
  }

  public static float CalcLevelRate(int enemyLevel)
  {
    int num = enemyLevel;
    if (enemyLevel <= 0)
      num = 1;
    return (float) (100.0 + (double) (num - 1) * 10.0);
  }

  public static float CalcDamageRateToPlayer(
    float levelRate,
    float defense,
    float tolerance,
    int threshold = -1,
    float coefficient = 1f)
  {
    return threshold > 0 && (double) defense > (double) threshold ? levelRate / (float) ((double) levelRate * 0.5 + (double) threshold * 4.5 + ((double) defense - (double) threshold) * (double) coefficient + (double) tolerance * 4.5) : levelRate / (float) ((double) levelRate * 0.5 + ((double) defense + (double) tolerance) * 4.5);
  }

  public static float CalcDamageDetailToEnemy(float atk, float defense, float tolerance)
  {
    return atk * (1f - tolerance) - defense;
  }

  public static bool IsDamageUpAtkTypeNormal(Player player, AttackHitInfo info)
  {
    return (info.attackType == AttackHitInfo.ATTACK_TYPE.NORMAL || info.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_COMBO03 || info.attackType == AttackHitInfo.ATTACK_TYPE.BURST_SPEAR_COMBO3 || info.attackType == AttackHitInfo.ATTACK_TYPE.BOOST_BOMB_ARROW || info.attackType == AttackHitInfo.ATTACK_TYPE.BOOST_ARROW_RAIN || info.attackType == AttackHitInfo.ATTACK_TYPE.ARROW_RAIN) && (player.attackMode == Player.ATTACK_MODE.ARROW || !info.toEnemy.isSpecialAttack && (player.attackMode != Player.ATTACK_MODE.SPEAR || player.attackID != player.playerParameter.spearActionInfo.Heat_SpAttackId) && player.actionID != (Character.ACTION_ID) 37 && player.actionID != (Character.ACTION_ID) 38);
  }

  public static bool GetBombLevelByAttackInfo(AttackInfo attackInfo, out int lv)
  {
    return int.TryParse(attackInfo.name.Substring(attackInfo.name.LastIndexOf("_") + 1), out lv);
  }

  public class PlayerAtkCalcData
  {
    public SkillInfo.SkillParam skillParam;
    public AttackHitInfo atkInfo;
    public AtkAttribute weaponAtk;
    public float statusAtk;
    public AtkAttribute guardEquipAtk;
    public AtkAttribute buffAtkRate;
    public AtkAttribute passiveAtkRate;
    public AtkAttribute buffAtkConstant;
    public float buffAtkAllElementConstant;
    public AtkAttribute passiveAtkConstant;
    public float passiveAtkAllElementConstant;
    public bool isAtkElementOnly;
  }

  public class EnemyAtkCalcData
  {
    public AttackHitInfo atkInfo;
    public AtkAttribute buffAtkRate;
    public AtkAttribute buffAtkConstant;
    public float buffAtkAllElementConstant;
  }
}
