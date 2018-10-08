using System.Collections.Generic;

public class SkillController
{
	private Brain brain;

	private List<int> skillList;

	public bool IsAct;

	public int skillIndex
	{
		get;
		private set;
	}

	public SkillController(Brain brain)
	{
		this.brain = brain;
		skillList = new List<int>();
	}

	public bool IsReady()
	{
		if (skillList.Count > 0)
		{
			return true;
		}
		return false;
	}

	public void AddSkillIndex(int skillIndex)
	{
		if (!skillList.Contains(skillIndex))
		{
			skillList.Add(skillIndex);
		}
	}

	public void RemoveSkillIndex()
	{
		if (skillList.Contains(skillIndex))
		{
			skillList.Remove(skillIndex);
		}
	}

	public void RemoveAll()
	{
		skillList.Clear();
	}

	public List<int> GetListSkill()
	{
		return skillList;
	}

	public void SetSkillIndex(int index)
	{
		skillIndex = index;
	}
}
