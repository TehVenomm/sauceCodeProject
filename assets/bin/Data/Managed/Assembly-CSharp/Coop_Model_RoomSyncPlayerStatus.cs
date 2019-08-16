using System.Collections.Generic;

public class Coop_Model_RoomSyncPlayerStatus : Coop_Model_Base
{
	public int hp;

	public BuffParam.BuffSyncParam buff;

	public int wid;

	public List<int> exst = new List<int>();

	public Coop_Model_RoomSyncPlayerStatus()
	{
		base.packetType = PACKET_TYPE.ROOM_SYNC_PLAYER_STATUS;
	}

	public void SetExtraStatus(Self self, List<int> prevStatus)
	{
		bool flag = prevStatus != null && prevStatus.Count > 0;
		for (int i = 0; i < UIStatusIcon.NON_BUFF_STATUS.Length; i++)
		{
			UIStatusIcon.STATUS_TYPE status = UIStatusIcon.NON_BUFF_STATUS[i];
			if (!flag || !prevStatus.Contains(i))
			{
				AddExtraStatusIfEnabled(self, status);
			}
		}
	}

	private void AddExtraStatusIfEnabled(Self self, UIStatusIcon.STATUS_TYPE status)
	{
		if (StatusEnabled(self, status))
		{
			exst.Add((int)status);
		}
	}

	public static bool StatusEnabled(Player self, UIStatusIcon.STATUS_TYPE status)
	{
		return UIStatusIcon.CheckStatus(status, self, isFieldBuff: false);
	}

	public override string ToString()
	{
		return base.ToString() + $"hp={hp} wid={wid} buff={buff}";
	}
}
