using Network;
using UnityEngine;

public class PortalObject
{
	public enum VIEW_TYPE
	{
		NONE = -1,
		NORMAL,
		NOT_TRAVELED,
		TO_HOME,
		TO_HARD_MAP,
		NOT_CLEAR_ORDER
	}

	protected Transform viewObject;

	protected Animator viewAnimator;

	protected ParticleSystem[] viewParticles;

	private readonly uint NOT_UNLOCKED_TIME = 7002u;

	public InGameSettingsManager.Portal parameter
	{
		get;
		protected set;
	}

	public Transform _transform
	{
		get;
		private set;
	}

	public FieldMapPortalInfo portalInfo
	{
		get;
		protected set;
	}

	public FieldMapTable.PortalTableData portalData
	{
		get;
		protected set;
	}

	public uint portalID
	{
		get;
		protected set;
	}

	public VIEW_TYPE viewType
	{
		get;
		protected set;
	}

	public bool isFull
	{
		get;
		protected set;
	}

	public bool isQuest
	{
		get;
		protected set;
	}

	public bool isLock
	{
		get;
		protected set;
	}

	public bool isClearOrder
	{
		get;
		protected set;
	}

	public bool isToHardMap
	{
		get;
		protected set;
	}

	public bool isUnlockedTime
	{
		get;
		protected set;
	}

	public int nowPoint
	{
		get;
		protected set;
	}

	public int maxPoint
	{
		get;
		protected set;
	}

	public UIPortalStatusGizmo uiGizmo
	{
		get;
		set;
	}

	public PortalObject()
		: this()
	{
	}

	public static PortalObject Create(FieldMapPortalInfo portal_info, Transform parent)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (portal_info == null)
		{
			return null;
		}
		if (portal_info.portalData == null)
		{
			return null;
		}
		Transform val = Utility.CreateGameObject("PortalObject", parent, 19);
		val.set_position(new Vector3(portal_info.portalData.srcX, 0f, portal_info.portalData.srcZ));
		PortalObject portalObject = val.get_gameObject().AddComponent<PortalObject>();
		if (portalObject == null)
		{
			return null;
		}
		portalObject.Initialize(portal_info);
		return portalObject;
	}

	protected virtual void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal;
		_transform = this.get_transform();
		SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
		val.set_center(new Vector3(0f, 0f, 0f));
		val.set_radius(1f);
		val.set_isTrigger(true);
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
		}
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
	}

	public void Initialize(FieldMapPortalInfo portal_info)
	{
		portalInfo = portal_info;
		portalData = portalInfo.portalData;
		portalID = portalData.portalID;
		isClearOrder = (FieldManager.IsOpenPortalClearOrder(portalData) || FieldManager.IsOpenPortal(portalData));
		if (GameSaveData.instance.isNewReleasePortal(portalID) && FieldManager.IsOpenPortal(portalData))
		{
			GameSaveData.instance.newReleasePortals.Remove(portalID);
		}
		isUnlockedTime = portal_info.portalData.isUnlockedTime();
		isToHardMap = FieldManager.IsToHardPortal(portalData);
		nowPoint = portal_info.GetNowPortalPoint();
		maxPoint = (int)portal_info.GetMaxPortalPoint();
		isFull = portal_info.IsFull();
		viewType = VIEW_TYPE.NORMAL;
		if (!isClearOrder || !isUnlockedTime)
		{
			viewType = VIEW_TYPE.NOT_CLEAR_ORDER;
		}
		else if (portalData.dstMapID == 0)
		{
			viewType = VIEW_TYPE.TO_HOME;
		}
		else if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(portalData))
		{
			viewType = VIEW_TYPE.NOT_TRAVELED;
		}
		else if (isToHardMap)
		{
			viewType = VIEW_TYPE.TO_HARD_MAP;
		}
		if (portalData.dstQuestID != 0)
		{
			if (portalData.dstMapID != 0)
			{
				int num = 0;
				ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == portalData.dstQuestID);
				if (clearStatusQuest != null)
				{
					num = clearStatusQuest.questStatus;
				}
				if (num != 3 && num != 4)
				{
					isLock = true;
					isQuest = true;
				}
			}
			else
			{
				isQuest = true;
			}
		}
		CreateView();
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
		}
	}

	private void CreateView()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (viewObject != null)
		{
			Object.Destroy(viewObject.get_gameObject());
			viewObject = null;
		}
		if (viewType == VIEW_TYPE.NOT_TRAVELED && !isFull)
		{
			if (MonoBehaviourSingleton<InGameLinkResourcesField>.IsValid())
			{
				viewObject = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesField>.I.portalIncomplete, _transform, -1);
			}
			if (viewObject != null)
			{
				viewAnimator = viewObject.GetComponent<Animator>();
				if (viewAnimator != null)
				{
					viewAnimator.set_speed(0f);
				}
				viewParticles = viewObject.GetComponentsInChildren<ParticleSystem>();
			}
		}
		else
		{
			viewObject = EffectManager.GetEffect(parameter.effectNames[(int)viewType], _transform);
		}
		UpdateView();
	}

	public void SetAndCreateView(VIEW_TYPE type)
	{
		viewType = type;
		CreateView();
	}

	public void UpdateView()
	{
		if (viewType == VIEW_TYPE.NOT_TRAVELED && !isFull)
		{
			float num = (float)nowPoint / (float)maxPoint;
			if (num > 1f)
			{
				num = 1f;
			}
			if (!isClearOrder)
			{
				num = 0f;
			}
			if (viewAnimator != null)
			{
				viewAnimator.set_speed(1f);
				viewAnimator.Play("ef_btl_warp_unuse_01", 0, num);
				viewAnimator.Update(0f);
				viewAnimator.set_speed(0f);
			}
			if (viewParticles != null)
			{
				bool flag = false;
				FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
				if (isClearOrder && (portalPointToPortalInfo == portalInfo || portalInfo.IsFull()))
				{
					flag = true;
				}
				int i = 0;
				for (int num2 = viewParticles.Length; i < num2; i++)
				{
					if (viewParticles[i] != null)
					{
						if (flag)
						{
							if (viewParticles[i].get_isStopped())
							{
								viewParticles[i].Play();
							}
						}
						else if (!viewParticles[i].get_isStopped())
						{
							viewParticles[i].Stop();
						}
					}
				}
			}
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Expected O, but got Unknown
		if (collider.get_gameObject().GetComponent<Self>() == null)
		{
			return;
		}
		if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			return;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0)
		{
			return;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
		{
			return;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.self == null)
		{
			return;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.self.isDead)
		{
			return;
		}
		string text;
		if (!isClearOrder || !isUnlockedTime)
		{
			text = portalData.notAppearText;
			if (portalData.appearQuestId != 0 && string.IsNullOrEmpty(text) && !MonoBehaviourSingleton<QuestManager>.I.IsClearQuest(portalData.appearQuestId))
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(portalData.appearQuestId);
				text = StringTable.Format(STRING_CATEGORY.IN_GAME, 7000u, questData.questText);
			}
			if (portalData.appearDeliveryId != 0 && string.IsNullOrEmpty(text) && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(portalData.appearDeliveryId))
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(portalData.appearDeliveryId);
				text = StringTable.Format(STRING_CATEGORY.IN_GAME, 7001u, deliveryTableData.name);
			}
			if (portalData.travelMapId != 0 && string.IsNullOrEmpty(text) && MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.travelMapId))
			{
				goto IL_0198;
			}
			goto IL_0198;
		}
		if (!isFull)
		{
			FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
			if (portalPointToPortalInfo == portalInfo || portalInfo.IsFull())
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_NOT_FULL", new object[2]
					{
						nowPoint.ToString(),
						maxPoint.ToString()
					}, null, true);
				}
			}
			else if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_NOT_ACTIVE", null, null, true);
			}
			return;
		}
		MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject = this;
		if (isQuest)
		{
			if (isLock)
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portalData, true, true))
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_QUEST_LOCK", null, null, true);
				}
			}
			else if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(portalData.dstQuestID, true))
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_QUEST", null, null, true);
			}
			return;
		}
		if (viewType == VIEW_TYPE.TO_HOME)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_HOME", null, null, true);
			}
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			return;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep != 0)
		{
			goto IL_03c0;
		}
		goto IL_03c0;
		IL_03c0:
		if (MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portalData, false, true))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_NEXT", null, null, true);
		}
		return;
		IL_0198:
		if (!isUnlockedTime)
		{
			text = StringTable.Get(STRING_CATEGORY.IN_GAME, NOT_UNLOCKED_TIME);
		}
		if (!string.IsNullOrEmpty(text) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", this.get_gameObject(), "PORTAL_NOT_APPEAR", new object[1]
			{
				text
			}, null, true);
		}
	}

	public void OnGetPortalPoint(int add_point)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		EffectManager.OneShot(parameter.pointGetEffectName, _transform.get_position(), _transform.get_rotation(), false);
		nowPoint += add_point;
		if (nowPoint >= maxPoint)
		{
			if (!portalInfo.IsFull())
			{
				Log.Warning(LOG.INGAME, "PortalObject.OnGetPortalPoint() Portal is not full. id : {0}", portalID);
			}
			isFull = true;
			CreateView();
			string text = string.Empty;
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
			if (fieldMapData != null)
			{
				text = fieldMapData.mapName;
			}
			if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
			{
				string text2 = StringTable.Format(STRING_CATEGORY.IN_GAME, 6001u, text);
				UIInGamePopupDialog.PushOpen(text2, false, 1.4f);
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				string text3 = StringTable.Format(STRING_CATEGORY.IN_GAME, 6002u, text);
				UIInGamePopupDialog.PushOpen(text3, false, 1.4f);
			}
			else
			{
				int num = parameter.clearCrystalNum;
				if (isToHardMap)
				{
					num = parameter.clearHardCrystalNum;
				}
				string text4 = StringTable.Format(STRING_CATEGORY.IN_GAME, 6000u, text, num);
				UIInGamePopupDialog.PushOpen(text4, false, 1.8f);
			}
			SoundManager.PlayOneShotUISE(40000069);
			SoundManager.PlayOneshotJingle(40000071, null, null);
		}
		else
		{
			UpdateView();
			SoundManager.PlayOneShotUISE(40000068);
		}
		if (uiGizmo != null)
		{
			uiGizmo.OnGetPortalPoint();
		}
	}
}
