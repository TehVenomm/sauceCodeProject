using Network;
using UnityEngine;

public class PortalObject : MonoBehaviour
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

	public static PortalObject Create(FieldMapPortalInfo portal_info, Transform parent)
	{
		if (portal_info == null)
		{
			return null;
		}
		if (portal_info.portalData == null)
		{
			return null;
		}
		Transform transform = Utility.CreateGameObject("PortalObject", parent, 19);
		transform.position = new Vector3(portal_info.portalData.srcX, 0f, portal_info.portalData.srcZ);
		PortalObject portalObject = transform.gameObject.AddComponent<PortalObject>();
		if (portalObject == null)
		{
			return null;
		}
		portalObject.Initialize(portal_info);
		return portalObject;
	}

	protected virtual void Awake()
	{
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal;
		_transform = base.transform;
		SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
		sphereCollider.center = new Vector3(0f, 0f, 0f);
		sphereCollider.radius = 1f;
		sphereCollider.isTrigger = true;
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
		if (viewObject != null)
		{
			Object.Destroy(viewObject.gameObject);
			viewObject = null;
		}
		if (viewType == VIEW_TYPE.NOT_TRAVELED && !isFull)
		{
			if (MonoBehaviourSingleton<InGameLinkResourcesField>.IsValid())
			{
				viewObject = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesField>.I.portalIncomplete, _transform);
			}
			if (viewObject != null)
			{
				viewAnimator = viewObject.GetComponent<Animator>();
				if (viewAnimator != null)
				{
					viewAnimator.speed = 0f;
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
		if (viewType != VIEW_TYPE.NOT_TRAVELED || isFull)
		{
			return;
		}
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
			viewAnimator.speed = 1f;
			viewAnimator.Play("ef_btl_warp_unuse_01", 0, num);
			viewAnimator.Update(0f);
			viewAnimator.speed = 0f;
		}
		if (viewParticles == null)
		{
			return;
		}
		bool flag = false;
		FieldMapPortalInfo portalPointToPortalInfo = MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo();
		if (isClearOrder && (portalPointToPortalInfo == portalInfo || portalInfo.IsFull()))
		{
			flag = true;
		}
		int i = 0;
		for (int num2 = viewParticles.Length; i < num2; i++)
		{
			if (!(viewParticles[i] != null))
			{
				continue;
			}
			if (flag)
			{
				if (viewParticles[i].isStopped)
				{
					viewParticles[i].Play();
				}
			}
			else if (!viewParticles[i].isStopped)
			{
				viewParticles[i].Stop();
			}
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.GetComponent<Self>() == null || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0 || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || MonoBehaviourSingleton<StageObjectManager>.I.self == null || MonoBehaviourSingleton<StageObjectManager>.I.self.isDead)
		{
			return;
		}
		if (!isClearOrder || !isUnlockedTime)
		{
			string text = portalData.notAppearText;
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
			if (portalData.travelMapId != 0 && string.IsNullOrEmpty(text))
			{
				MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.travelMapId);
			}
			if (!isUnlockedTime)
			{
				text = StringTable.Get(STRING_CATEGORY.IN_GAME, NOT_UNLOCKED_TIME);
			}
			if (!string.IsNullOrEmpty(text) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_NOT_APPEAR", new object[1]
				{
					text
				});
			}
			return;
		}
		if (!isFull)
		{
			if (MonoBehaviourSingleton<FieldManager>.I.GetPortalPointToPortalInfo() == portalInfo || portalInfo.IsFull())
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_NOT_FULL", new object[2]
					{
						nowPoint.ToString(),
						maxPoint.ToString()
					});
				}
			}
			else if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_NOT_ACTIVE");
			}
			return;
		}
		MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject = this;
		if (isQuest)
		{
			if (isLock)
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portalData, check_dst_quest: true))
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_QUEST_LOCK");
				}
			}
			else if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(portalData.dstQuestID))
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_QUEST");
			}
		}
		else if (viewType == VIEW_TYPE.TO_HOME)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_LOUNGE");
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_HOME");
				}
			}
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			_ = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
			if (MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(portalData, check_dst_quest: false))
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("PortalObject.OnTriggerEnter", base.gameObject, "PORTAL_NEXT");
			}
		}
	}

	public void OnGetPortalPoint(int add_point)
	{
		EffectManager.OneShot(parameter.pointGetEffectName, _transform.position, _transform.rotation);
		nowPoint += add_point;
		if (nowPoint >= maxPoint)
		{
			if (!portalInfo.IsFull())
			{
				Log.Warning(LOG.INGAME, "PortalObject.OnGetPortalPoint() Portal is not full. id : {0}", portalID);
			}
			isFull = true;
			CreateView();
			string text = "";
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
			if (fieldMapData != null)
			{
				text = fieldMapData.mapName;
			}
			if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
			{
				UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6001u, text), is_important: false, 1.4f);
			}
			else if (QuestManager.IsValidInGameExplore())
			{
				UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6002u, text), is_important: false, 1.4f);
			}
			else
			{
				int num = parameter.clearCrystalNum;
				if (isToHardMap)
				{
					num = parameter.clearHardCrystalNum;
				}
				UIInGamePopupDialog.PushOpen(StringTable.Format(STRING_CATEGORY.IN_GAME, 6000u, text, num), is_important: false);
			}
			SoundManager.PlayOneShotUISE(40000069);
			SoundManager.PlayOneshotJingle(40000071);
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
