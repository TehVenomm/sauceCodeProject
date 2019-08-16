using Network;

public class Coop_Model_PlayerInitialize : Coop_Model_ObjectSyncPositionBase
{
	public int sid;

	public int hp;

	public int healHp;

	public int target_id;

	public bool stopcounter;

	public bool act_battle_start;

	public CharaInfo.EquipItem weapon_item;

	public int weapon_index;

	public BuffParam.BuffSyncParam buff_sync_param;

	public int cannonId;

	public int bulletIndex;

	public int fishingState;

	public int gatherGimmickId;

	public int carryingGimmickId;

	public Coop_Model_PlayerInitialize()
	{
		base.packetType = PACKET_TYPE.PLAYER_INITIALIZE;
	}

	public override bool IsPromiseOverAgainCheck()
	{
		return true;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
