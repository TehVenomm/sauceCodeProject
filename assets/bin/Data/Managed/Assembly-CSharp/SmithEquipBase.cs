// Decompiled with JetBrains decompiler
// Type: SmithEquipBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public abstract class SmithEquipBase : SkillInfoBase
{
  protected SkillItemInfo[] equipAttachSkill;
  protected SmithEquipBase.EquipDialogType type;
  protected SmithEquipBase.SmithType smithType = SmithEquipBase.SmithType.GROW;
  protected System.Action updateTopAreaUI;
  protected System.Action updateMiddleAreaUI;

  protected EquipItemInfo GetEquipData()
  {
    return MonoBehaviourSingleton<SmithManager>.I.GetSmithEquipItemInfo(this.smithType);
  }

  protected EquipItemTable.EquipItemData GetEquipTableData()
  {
    return MonoBehaviourSingleton<SmithManager>.I.GetSmithEquipItemTable(this.smithType);
  }

  public override void Initialize()
  {
    this.SetupUIFunc();
    base.Initialize();
  }

  private void SetupUIFunc()
  {
    this.updateTopAreaUI = (System.Action) null;
    switch (this.type)
    {
      case SmithEquipBase.EquipDialogType.RESULT:
        this.updateTopAreaUI += new System.Action(this.EquipImg);
        break;
      default:
        this.updateTopAreaUI += this.GetEquipInfoFunc();
        this.updateTopAreaUI += new System.Action(this.EquipImg);
        break;
    }
    this.updateMiddleAreaUI = (System.Action) null;
    switch (this.type)
    {
      case SmithEquipBase.EquipDialogType.MATERIAL:
        this.updateMiddleAreaUI += new System.Action(this.NeededMaterial);
        break;
      case SmithEquipBase.EquipDialogType.RESULT:
        if (this.smithType != SmithEquipBase.SmithType.SKILL_GROW)
        {
          this.updateMiddleAreaUI += new System.Action(this.ResultEquipInfo);
          break;
        }
        this.updateMiddleAreaUI += new System.Action(this.ResultSKillInfo);
        break;
      default:
        this.updateMiddleAreaUI += new System.Action(this.LocalInventory);
        break;
    }
  }

  public override void UpdateUI()
  {
    if (this.updateTopAreaUI != null)
      this.updateTopAreaUI();
    if (this.updateMiddleAreaUI == null)
      return;
    this.updateMiddleAreaUI();
  }

  private System.Action GetEquipInfoFunc()
  {
    if (this.smithType == SmithEquipBase.SmithType.GENERATE && (this.type == SmithEquipBase.EquipDialogType.MATERIAL || this.type == SmithEquipBase.EquipDialogType.SELECT))
      return new System.Action(this.EquipTableParam);
    return this.smithType == SmithEquipBase.SmithType.EVOLVE && this.type == SmithEquipBase.EquipDialogType.MATERIAL ? new System.Action(this.EquipTableParam) : new System.Action(this.EquipParam);
  }

  protected virtual void EquipParam()
  {
  }

  protected virtual void EquipTableParam()
  {
  }

  protected virtual void EquipImg()
  {
  }

  protected virtual void NeededMaterial()
  {
  }

  protected virtual void ResultEquipInfo()
  {
  }

  protected virtual void ResultSKillInfo()
  {
  }

  protected virtual void LocalInventory()
  {
  }

  public SortBase.TYPE EquipmentTypeToSortBaseType(EQUIPMENT_TYPE type)
  {
    switch (type)
    {
      case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        return SortBase.TYPE.ONE_HAND_SWORD;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        return SortBase.TYPE.TWO_HAND_SWORD;
      case EQUIPMENT_TYPE.SPEAR:
        return SortBase.TYPE.SPEAR;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        return SortBase.TYPE.PAIR_SWORDS;
      case EQUIPMENT_TYPE.ARROW:
        return SortBase.TYPE.ARROW;
      case EQUIPMENT_TYPE.ARMOR:
        return SortBase.TYPE.ARMOR;
      case EQUIPMENT_TYPE.HELM:
        return SortBase.TYPE.HELM;
      case EQUIPMENT_TYPE.ARM:
        return SortBase.TYPE.ARM;
      case EQUIPMENT_TYPE.LEG:
        return SortBase.TYPE.LEG;
      default:
        return SortBase.TYPE.NONE;
    }
  }

  public EQUIPMENT_TYPE SortBaseTypeToEquipmentType(SortBase.TYPE type)
  {
    switch (type)
    {
      case SortBase.TYPE.ONE_HAND_SWORD:
        return EQUIPMENT_TYPE.ONE_HAND_SWORD;
      case SortBase.TYPE.TWO_HAND_SWORD:
        return EQUIPMENT_TYPE.TWO_HAND_SWORD;
      case SortBase.TYPE.SPEAR:
        return EQUIPMENT_TYPE.SPEAR;
      case SortBase.TYPE.PAIR_SWORDS:
        return EQUIPMENT_TYPE.PAIR_SWORDS;
      case SortBase.TYPE.ARROW:
        return EQUIPMENT_TYPE.ARROW;
      case SortBase.TYPE.ARMOR:
        return EQUIPMENT_TYPE.ARMOR;
      case SortBase.TYPE.HELM:
        return EQUIPMENT_TYPE.HELM;
      case SortBase.TYPE.ARM:
        return EQUIPMENT_TYPE.ARM;
      case SortBase.TYPE.LEG:
        return EQUIPMENT_TYPE.LEG;
      default:
        return EQUIPMENT_TYPE.ONE_HAND_SWORD;
    }
  }

  public enum EquipDialogType
  {
    SELECT,
    MATERIAL,
    RESULT,
  }

  public enum SmithType
  {
    GENERATE,
    GROW,
    EVOLVE,
    SKILL_GROW,
    ABILITY_CHANGE,
    REVERT_LITHOGRAPH,
  }
}
