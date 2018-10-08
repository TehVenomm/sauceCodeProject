using System.Collections.Generic;
using UnityEngine;

public class UIStatusGizmoManager : MonoBehaviourSingleton<UIStatusGizmoManager>
{
	[SerializeField]
	protected GameObject playerStatusGizmo;

	[SerializeField]
	protected GameObject enemyStatusGizmo;

	[SerializeField]
	protected GameObject portalStatusGizmo;

	[SerializeField]
	protected GameObject cannonGizmo;

	[SerializeField]
	protected GameObject grabStatusGizmo;

	[SerializeField]
	protected GameObject sonarGizmo;

	[SerializeField]
	protected GameObject waveTargetGizmo;

	[SerializeField]
	protected GameObject chatGimmickGizmo;

	protected List<UIPlayerStatusGizmo> playerList = new List<UIPlayerStatusGizmo>();

	protected List<UIEnemyStatusGizmo> enemyList = new List<UIEnemyStatusGizmo>();

	protected List<UIPortalStatusGizmo> portalList = new List<UIPortalStatusGizmo>();

	protected List<UICannonGizmo> cannonList = new List<UICannonGizmo>();

	protected List<UIGrabStatusGizmo> grabList = new List<UIGrabStatusGizmo>();

	protected List<UISonarGizmo> sonarList = new List<UISonarGizmo>();

	protected List<UIWaveTargetGizmo> waveTargetList = new List<UIWaveTargetGizmo>();

	protected List<UIChatGimmickGizmo> chatGimmickList = new List<UIChatGimmickGizmo>();

	private int depth;

	private void LateUpdate()
	{
		if (GameSaveData.instance.headName && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Vector3 a = Vector3.zero;
			StageObject x = null;
			if ((Object)MonoBehaviourSingleton<StageObjectManager>.I.self != (Object)null)
			{
				a = MonoBehaviourSingleton<StageObjectManager>.I.self.GetCameraTargetPos();
				a.y = 0f;
				if ((Object)MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint != (Object)null)
				{
					x = MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint.owner;
				}
			}
			List<StageObject> list = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
			int num = 0;
			int count = list.Count;
			Enemy enemy;
			float num2;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				enemy = (list[num] as Enemy);
				if (!enemy.isDead && enemy.isInitialized)
				{
					num2 = Vector3.Distance(a, list[num]._position);
					if (enemy.uiShowDistance > 0f)
					{
						break;
					}
					if (num2 < enemy.enemyParameter.showStatusUIRange || (Object)x == (Object)enemy)
					{
						enemy.CreateStatusGizmo();
					}
					else if (num2 > enemy.enemyParameter.showStatusUIRange + 1f)
					{
						enemy.DeleteStatusGizmo();
					}
				}
				num++;
			}
			if (num2 < enemy.uiShowDistance || (Object)x == (Object)enemy)
			{
				enemy.CreateStatusGizmo();
			}
			else if (num2 > enemy.uiShowDistance + 1f)
			{
				enemy.DeleteStatusGizmo();
			}
		}
	}

	public UIPlayerStatusGizmo Create(Player owner)
	{
		UIPlayerStatusGizmo uIPlayerStatusGizmo = null;
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			if (!playerList[i].gameObject.activeSelf)
			{
				uIPlayerStatusGizmo = playerList[i];
				break;
			}
		}
		if ((Object)uIPlayerStatusGizmo == (Object)null)
		{
			Transform transform = ResourceUtility.Realizes(playerStatusGizmo, base._transform, -1);
			if ((Object)transform == (Object)null)
			{
				return null;
			}
			uIPlayerStatusGizmo = transform.GetComponent<UIPlayerStatusGizmo>();
			if ((Object)uIPlayerStatusGizmo == (Object)null)
			{
				Object.Destroy(transform);
				return null;
			}
			playerList.Add(uIPlayerStatusGizmo);
		}
		uIPlayerStatusGizmo.targetPlayer = owner;
		SetDepth(uIPlayerStatusGizmo.gameObject);
		return uIPlayerStatusGizmo;
	}

	public UIEnemyStatusGizmo Create(Enemy owner)
	{
		UIEnemyStatusGizmo uIEnemyStatusGizmo = null;
		int i = 0;
		for (int count = enemyList.Count; i < count; i++)
		{
			if (!enemyList[i].gameObject.activeSelf)
			{
				uIEnemyStatusGizmo = enemyList[i];
				break;
			}
		}
		if ((Object)uIEnemyStatusGizmo == (Object)null)
		{
			Transform transform = ResourceUtility.Realizes(enemyStatusGizmo, base._transform, -1);
			if ((Object)transform == (Object)null)
			{
				return null;
			}
			uIEnemyStatusGizmo = transform.GetComponent<UIEnemyStatusGizmo>();
			if ((Object)uIEnemyStatusGizmo == (Object)null)
			{
				Object.Destroy(transform);
				return null;
			}
			enemyList.Add(uIEnemyStatusGizmo);
		}
		uIEnemyStatusGizmo.targetEnemy = owner;
		SetDepth(uIEnemyStatusGizmo.gameObject);
		return uIEnemyStatusGizmo;
	}

	public UIPortalStatusGizmo Create(PortalObject owner)
	{
		UIPortalStatusGizmo uIPortalStatusGizmo = null;
		int i = 0;
		for (int count = portalList.Count; i < count; i++)
		{
			if (!portalList[i].gameObject.activeSelf)
			{
				uIPortalStatusGizmo = portalList[i];
				break;
			}
		}
		if ((Object)uIPortalStatusGizmo == (Object)null)
		{
			Transform transform = ResourceUtility.Realizes(portalStatusGizmo, base._transform, -1);
			if ((Object)transform == (Object)null)
			{
				return null;
			}
			uIPortalStatusGizmo = transform.GetComponent<UIPortalStatusGizmo>();
			if ((Object)uIPortalStatusGizmo == (Object)null)
			{
				Object.Destroy(transform);
				return null;
			}
			portalList.Add(uIPortalStatusGizmo);
		}
		uIPortalStatusGizmo.portal = owner;
		SetDepth(uIPortalStatusGizmo.gameObject);
		return uIPortalStatusGizmo;
	}

	public UICannonGizmo Create(FieldGimmickCannonObject owner)
	{
		UICannonGizmo orCreate = GetOrCreate(cannonList, cannonGizmo);
		orCreate.owner = owner;
		SetDepth(orCreate.gameObject);
		return orCreate;
	}

	public UISonarGizmo CreateSonar(FieldSonarObject sonar)
	{
		UISonarGizmo orCreate = GetOrCreate(sonarList, sonarGizmo);
		orCreate.sonar = sonar;
		SetDepth(orCreate.gameObject);
		return orCreate;
	}

	public UIGrabStatusGizmo CreateGrab()
	{
		UIGrabStatusGizmo orCreate = GetOrCreate(grabList, grabStatusGizmo);
		SetDepth(orCreate.gameObject);
		return orCreate;
	}

	public UIWaveTargetGizmo CreateWaveTarget(FieldWaveTargetObject wt)
	{
		UIWaveTargetGizmo orCreate = GetOrCreate(waveTargetList, waveTargetGizmo);
		orCreate.waveTarget = wt;
		orCreate.Initialize();
		SetDepth(orCreate.gameObject);
		return orCreate;
	}

	public UIChatGimmickGizmo CreateGimmick(FieldChatGimmickObject cg)
	{
		UIChatGimmickGizmo orCreate = GetOrCreate(chatGimmickList, chatGimmickGizmo);
		orCreate.Initialize(cg);
		SetDepth(orCreate.gameObject);
		return orCreate;
	}

	protected T GetOrCreate<T>(List<T> objects, GameObject obj) where T : MonoBehaviour
	{
		T val = (T)null;
		for (int i = 0; i < objects.Count; i++)
		{
			T val2 = objects[i];
			if (!val2.gameObject.activeSelf)
			{
				val = objects[i];
				break;
			}
		}
		if ((Object)val == (Object)null)
		{
			Transform transform = ResourceUtility.Realizes(obj, base._transform, -1);
			if ((Object)transform == (Object)null)
			{
				return (T)null;
			}
			val = transform.GetComponent<T>();
			if ((Object)val == (Object)null)
			{
				Object.Destroy(transform);
				return (T)null;
			}
			objects.Add(val);
		}
		return val;
	}

	private void SetDepth(GameObject go)
	{
		UIPanel component = go.GetComponent<UIPanel>();
		if ((Object)component != (Object)null)
		{
			component.depth = depth;
			depth++;
			if (depth > 10000)
			{
				depth = 0;
			}
		}
	}
}
