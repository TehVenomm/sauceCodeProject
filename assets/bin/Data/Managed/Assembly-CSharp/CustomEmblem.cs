using System;
using UnityEngine;

public class CustomEmblem : GameSection
{
	protected enum UI
	{
		GRD_LIST_1,
		GRD_LIST_2,
		GRD_LIST_3,
		SPR_GUILD_EMBLEM_1,
		SPR_GUILD_EMBLEM_2,
		SPR_GUILD_EMBLEM_3,
		LAYER_1,
		LAYER_2,
		LAYER_3,
		SPR_EMBLEM_IMAGE
	}

	private int mLayer1ID;

	private int mLayer2ID;

	private int mLayer3ID;

	public override void Initialize()
	{
		UpdateList();
		mLayer1ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[0];
		mLayer2ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[1];
		mLayer3ID = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().EmblemLayerIDs[2];
		UpdateEmblems();
		base.Initialize();
	}

	public unsafe void UpdateList()
	{
		GuildItemInfoModel.EmblemInfo[] infos = GuildItemManager.I.GetEmblemLayer1Infos();
		_003CUpdateList_003Ec__AnonStorey322 _003CUpdateList_003Ec__AnonStorey;
		SetDynamicList((Enum)UI.GRD_LIST_1, "EmblemLayer1Item", infos.Length, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		infos = GuildItemManager.I.GetEmblemLayer2Infos();
		SetDynamicList((Enum)UI.GRD_LIST_2, "EmblemLayer2Item", infos.Length, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		infos = GuildItemManager.I.GetEmblemLayer3Infos();
		SetDynamicList((Enum)UI.GRD_LIST_3, "EmblemLayer3Item", infos.Length, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected void SetListItem(int i, Transform t, bool is_recycle, GuildItemInfoModel.EmblemInfo data)
	{
		SetSprite(t, UI.SPR_EMBLEM_IMAGE, data.image);
	}

	private void UpdateEmblems()
	{
		if (mLayer1ID == -1)
		{
			mLayer1ID = GuildManager.sDefaultEmblemIDLayer1;
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_1, GuildItemManager.I.GetItemSprite(mLayer1ID));
		if (mLayer2ID == -1)
		{
			mLayer2ID = GuildManager.sDefaultEmblemIDLayer2;
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_2, GuildItemManager.I.GetItemSprite(mLayer2ID));
		if (mLayer3ID == -1)
		{
			mLayer3ID = GuildManager.sDefaultEmblemIDLayer3;
		}
		SetSprite((Enum)UI.SPR_GUILD_EMBLEM_3, GuildItemManager.I.GetItemSprite(mLayer3ID));
	}

	private void OnQuery_RANDOM_EMBLEM()
	{
		int[] array = GuildItemManager.I.RandomEmblem(true);
		mLayer1ID = array[0];
		mLayer2ID = array[1];
		mLayer3ID = array[2];
		UpdateEmblems();
	}

	private void OnQuery_DONE()
	{
		MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(0, mLayer1ID);
		MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(1, mLayer2ID);
		MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam().SetEmblemID(2, mLayer3ID);
	}
}
