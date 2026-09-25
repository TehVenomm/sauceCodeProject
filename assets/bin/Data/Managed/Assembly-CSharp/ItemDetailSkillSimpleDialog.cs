// Decompiled with JetBrains decompiler
// Type: ItemDetailSkillSimpleDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ItemDetailSkillSimpleDialog : ItemDetailSkillDialog
{
  private ItemDetailSkillSimpleDialog.InitParam m_initParam;

  public override void Initialize()
  {
    this.m_initParam = GameSection.GetEventData() as ItemDetailSkillSimpleDialog.InitParam;
    if (this.m_initParam != null)
      GameSection.SetEventData(this.m_initParam.EventDataForParentInitialization);
    base.Initialize();
    this.ForceInvisibleUIButtons();
  }

  private void ForceInvisibleUIButtons()
  {
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_CHANGE, false);
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_GROW, false);
    this.SetActive((Enum) ItemDetailSkill.UI.BTN_SELL, false);
  }

  private void OnQuery_SECTION_BACK()
  {
    if (this.m_initParam == null)
      return;
    GameSection.SetEventData(this.m_initParam.EventDataForPrevSectionInit);
  }

  public class InitParam
  {
    public object EventDataForParentInitialization;
    public object EventDataForPrevSectionInit;

    public InitParam()
    {
      this.EventDataForParentInitialization = (object) null;
      this.EventDataForPrevSectionInit = (object) null;
    }

    public InitParam(object _eventDataForParent, object _eventDataForPrevSection)
    {
      this.EventDataForParentInitialization = _eventDataForParent;
      this.EventDataForPrevSectionInit = _eventDataForPrevSection;
    }
  }
}
