using System;
using System.Collections.Generic;
using UnityEngine;

public interface IHomePeople
{
	bool isInitialized
	{
		get;
	}

	bool isPeopleInitialized
	{
		get;
	}

	HomeSelfCharacter selfChara
	{
		get;
	}

	List<HomeCharacterBase> charas
	{
		get;
	}

	void CreateSelfCharacter(Action<HomeStageAreaEvent> notice_callback);

	Vector3 GetTargetPos(HomeCharacterBase chara, WayPoint wayPoint);

	HomeNPCCharacter GetHomeNPCCharacter(int npcID);

	ILoungePeople CastToLoungePeople();

	void OnDestroyHomeCharacter(HomeCharacterBase chara);
}
