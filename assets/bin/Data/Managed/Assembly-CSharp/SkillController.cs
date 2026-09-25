// Decompiled with JetBrains decompiler
// Type: SkillController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class SkillController
{
  private Brain brain;
  private List<int> skillList;
  public bool IsAct;

  public SkillController(Brain brain)
  {
    this.brain = brain;
    this.skillList = new List<int>();
  }

  public bool IsReady() => this.skillList.Count > 0;

  public void AddSkillIndex(int skillIndex)
  {
    if (this.skillList.Contains(skillIndex))
      return;
    this.skillList.Add(skillIndex);
  }

  public void RemoveSkillIndex()
  {
    if (!this.skillList.Contains(this.skillIndex))
      return;
    this.skillList.Remove(this.skillIndex);
  }

  public void RemoveAll() => this.skillList.Clear();

  public List<int> GetListSkill() => this.skillList;

  public int skillIndex { get; private set; }

  public void SetSkillIndex(int index) => this.skillIndex = index;
}
