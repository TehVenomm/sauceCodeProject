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
	protected Vector3 offset = Vector3.zero;

	protected PortalObject portal;

	protected PortalObject portalReq;

	protected override void Awake()
	{
		base.Awake();
		nameLabel.fontStyle = FontStyle.Italic;
		noticeObject.SetActive(value: false);
		base.gameObject.SetActive(FieldManager.IsValidInGameNoQuest());
	}

	private void Update()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self == null)
		{
			return;
		}
		portalReq = null;
		int i = 0;
		for (int count = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; i < count; i++)
		{
			PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[i];
			if (portalObject.isFull && portalObject.portalData.dstMapID != 0 && Vector3.Distance(portalObject._transform.position, self._position) < 10f)
			{
				portalReq = portalObject;
				break;
			}
		}
		if (portal == portalReq || noticeTween.isActiveAndEnabled)
		{
			return;
		}
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
				noticeObject.SetActive(value: true);
				noticeTween.PlayForward();
			}
			else
			{
				noticeObject.SetActive(value: false);
			}
			portal = portalReq;
		}
		else
		{
			noticeTween.PlayReverse();
		}
	}

	private void LateUpdate()
	{
		if (!(portal == null))
		{
			Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(portal._transform.position + offset));
			if (position.z >= 0f)
			{
				position.z = 0f;
			}
			else
			{
				position.z = -100f;
			}
			base._transform.position = position;
		}
	}
}
