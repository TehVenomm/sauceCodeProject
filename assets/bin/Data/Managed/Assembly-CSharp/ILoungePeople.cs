using System.Collections.Generic;
using UnityEngine;

public interface ILoungePeople
{
	List<LoungePlayer> loungePlayers
	{
		get;
	}

	bool CreateLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry);

	bool ChangeEquipLoungePlayer(PartyModel.SlotInfo slotInfo, bool useMovingEntry);

	bool DestroyLoungePlayer(int id);

	void SetInitialPositionLoungePlayer(int id, Vector3 initialPos, LOUNGE_ACTION_TYPE type);

	void MoveLoungePlayer(int id, Vector3 targetPos);

	void OnDestroyLoungePlayer(LoungePlayer chara);

	void UpdateLoungePlayersInfo(PartyModel.SlotInfo slotInfo);

	LoungePlayer GetLoungePlayer(int id);
}
