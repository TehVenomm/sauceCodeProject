using System.Collections.Generic;

public class Coop_Model_EnemyAngry : Coop_Model_ObjectSyncPositionBase
{
	public int angryActionId;

	public uint angryId;

	public List<uint> execAngryIds = new List<uint>();

	public Coop_Model_EnemyAngry()
	{
		base.packetType = PACKET_TYPE.ENEMY_ANGRY;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)15))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
