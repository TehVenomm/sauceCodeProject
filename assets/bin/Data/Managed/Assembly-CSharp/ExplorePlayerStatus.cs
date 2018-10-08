using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplorePlayerStatus
{
	public List<int> extraStatus = new List<int>();

	private int weaponEquipmentId;

	private EquipItemTable.EquipItemData weaponEquipItemData;

	private CharaInfo charaInfo;

	public bool isInitialized => weaponEquipItemData != null && (UnityEngine.Object)coopClient != (UnityEngine.Object)null;

	public int hp
	{
		get;
		private set;
	}

	public BuffParam buff
	{
		get;
		private set;
	}

	public EQUIPMENT_TYPE weaponType
	{
		get
		{
			if (weaponEquipItemData == null)
			{
				return EQUIPMENT_TYPE.ONE_HAND_SWORD;
			}
			return weaponEquipItemData.type;
		}
	}

	public ELEMENT_TYPE weaponElementType
	{
		get
		{
			if (weaponEquipItemData == null)
			{
				return ELEMENT_TYPE.MAX;
			}
			return (ELEMENT_TYPE)weaponEquipItemData.GetElemAtkType(null);
		}
	}

	public CoopClient coopClient
	{
		get;
		private set;
	}

	public int userId => charaInfo.userId;

	public string userName => charaInfo.name;

	public int hpMax
	{
		get;
		private set;
	}

	public int givenTotalDamage
	{
		get;
		private set;
	}

	public bool isSelf
	{
		get;
		private set;
	}

	public event Action onInitialize;

	public event Action onUpdateWeapon;

	public event Action onUpdateBuff;

	public event Action onUpdateHp;

	public ExplorePlayerStatus(CharaInfo charaInfo, bool isSelf)
	{
		this.isSelf = isSelf;
		this.charaInfo = charaInfo;
		MonoBehaviourSingleton<StatusManager>.I.CalcUserStatusParam(charaInfo, out int _, out int _, out int _hp);
		hpMax = _hp;
		buff = new BuffParam(null);
	}

	public void Activate(CoopClient coopClient)
	{
		this.coopClient = coopClient;
		charaInfo = coopClient.userInfo;
	}

	public void Sync(Coop_Model_RoomSyncPlayerStatus status)
	{
		UpdatePlayerStatus(status.hp, status.buff, status.wid);
		List<int> list = extraStatus;
		if (status.exst != null)
		{
			extraStatus = status.exst;
		}
		if (list == null || list.Count != extraStatus.Count)
		{
			if (this.onUpdateBuff != null)
			{
				this.onUpdateBuff();
			}
		}
		else
		{
			int num = 0;
			while (true)
			{
				if (num >= list.Count)
				{
					return;
				}
				int item = list[num];
				if (!status.exst.Contains(item))
				{
					break;
				}
				num++;
			}
			if (this.onUpdateBuff != null)
			{
				this.onUpdateBuff();
			}
		}
	}

	public void SyncFromPlayer(Player player)
	{
		if ((bool)player && player.isInitialized)
		{
			UpdatePlayerStatus(player.hp, player.buffParam.CreateSyncParam(BuffParam.BUFFTYPE.NONE), player.weaponData.eId);
			List<int> list = null;
			for (int i = 0; i < UIStatusIcon.NON_BUFF_STATUS.Length; i++)
			{
				UIStatusIcon.STATUS_TYPE sTATUS_TYPE = UIStatusIcon.NON_BUFF_STATUS[i];
				if (Coop_Model_RoomSyncPlayerStatus.StatusEnabled(player, sTATUS_TYPE))
				{
					if (!extraStatus.Contains((int)sTATUS_TYPE))
					{
						if (list == null)
						{
							list = new List<int>();
						}
						list.Add((int)sTATUS_TYPE);
					}
				}
				else if (extraStatus.Contains((int)sTATUS_TYPE) && list == null)
				{
					list = new List<int>();
				}
			}
			if (list != null)
			{
				extraStatus = list;
				if (this.onUpdateBuff != null)
				{
					this.onUpdateBuff();
				}
			}
		}
	}

	private void UpdatePlayerStatus(int hp, BuffParam.BuffSyncParam buffSyncParam, int weaponEquipmentId)
	{
		if (this.hp != hp)
		{
			this.hp = hp;
			if (this.hp > hpMax)
			{
				hpMax = hp;
			}
			if (this.onUpdateHp != null)
			{
				this.onUpdateHp();
			}
		}
		if (buffSyncParam != null)
		{
			buff.SetSyncParamForExplorePlayerStatus(buffSyncParam);
			if (this.onUpdateBuff != null)
			{
				this.onUpdateBuff();
			}
		}
		if (this.weaponEquipmentId != weaponEquipmentId)
		{
			this.weaponEquipmentId = weaponEquipmentId;
			bool flag = weaponEquipItemData == null;
			weaponEquipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponEquipmentId);
			if (flag && this.onInitialize != null)
			{
				this.onInitialize();
			}
			if (this.onUpdateWeapon != null)
			{
				this.onUpdateWeapon();
			}
		}
	}

	public void SyncTotalDamageToBoss(int total)
	{
		if (total > givenTotalDamage)
		{
			givenTotalDamage = total;
		}
	}

	public InGameRecorder.PlayerRecord CreateInGameRecord(CharaInfo _charaInfo)
	{
		if (_charaInfo != null)
		{
			charaInfo = _charaInfo;
		}
		InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
		playerRecord.id = ((!isSelf) ? charaInfo.userId : 0);
		playerRecord.isNPC = false;
		playerRecord.isSelf = isSelf;
		playerRecord.charaInfo = charaInfo;
		playerRecord.beforeLevel = charaInfo.level;
		playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(charaInfo, true, true, true, false);
		playerRecord.animID = playerRecord.playerLoadInfo.weaponModelID / 1000;
		playerRecord.givenTotalDamage = givenTotalDamage;
		return playerRecord;
	}
}
