using UnityEngine;

public class UIPortalNextAreaName : MonoBehaviourSingleton<UIPortalNextAreaName>
{
	[SerializeField]
	private GameObject noticeObject;

	[SerializeField]
	protected UILabel nameLabel;

	[SerializeField]
	protected UITweenCtrl animCtrl;

	[SerializeField]
	protected TweenAlpha noticeTween;

	[SerializeField]
	protected Vector3 offset;

	protected PortalObject portal;

	protected PortalObject portalReq;

	protected override void Awake()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		nameLabel.fontStyle = 2;
		noticeObject.SetActive(false);
		this.get_gameObject().SetActive(FieldManager.IsValidInGameNoQuest());
	}

	private void Update()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null))
			{
				portalReq = null;
				int i = 0;
				for (int count = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; i < count; i++)
				{
					PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[i];
					if (portalObject.isFull && portalObject.portalData.dstMapID != 0 && Vector3.Distance(portalObject._transform.get_position(), self._position) < 10f)
					{
						portalReq = portalObject;
						break;
					}
				}
				if (!(portal == portalReq) && !noticeTween.get_isActiveAndEnabled())
				{
					if (noticeTween.value == 0f)
					{
						if (portalReq != null)
						{
							if (string.IsNullOrEmpty(portalReq.portalData.placeText))
							{
								FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalReq.portalData.dstMapID);
								if (fieldMapData == null)
								{
									return;
								}
								nameLabel.text = fieldMapData.mapName;
							}
							else
							{
								nameLabel.text = portalReq.portalData.placeText;
							}
							noticeObject.SetActive(true);
							noticeTween.PlayForward();
						}
						else
						{
							noticeObject.SetActive(false);
						}
						portal = portalReq;
					}
					else
					{
						noticeTween.PlayReverse();
					}
				}
			}
		}
	}

	private void LateUpdate()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (!(portal == null))
		{
			Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(portal._transform.get_position() + offset));
			if (position.z >= 0f)
			{
				position.z = 0f;
			}
			else
			{
				position.z = -100f;
			}
			base._transform.set_position(position);
		}
	}
}
