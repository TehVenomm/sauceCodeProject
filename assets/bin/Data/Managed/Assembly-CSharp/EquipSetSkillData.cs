using Network;

public class EquipSetSkillData
{
	public ulong equipItemUniqId;

	public int equipSlotNo;

	public int equipSetNo;

	public EquipSetSkillData()
	{
	}

	public EquipSetSkillData(SkillItem.EquipSetSlot setSkill)
	{
		if (ulong.TryParse(setSkill.euid, out ulong result))
		{
			equipItemUniqId = result;
		}
		else
		{
			Log.Error("Equip Item EquipUniqueId Error euid:{0} setNo:{1}", setSkill.euid, setSkill.setNo);
		}
		equipSlotNo = setSkill.slotNo;
		equipSetNo = setSkill.setNo;
	}
}
