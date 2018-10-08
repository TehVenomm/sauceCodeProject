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

	protected List<UIPlayerStatusGizmo> playerList = new List<UIPlayerStatusGizmo>();

	protected List<UIEnemyStatusGizmo> enemyList = new List<UIEnemyStatusGizmo>();

	protected List<UIPortalStatusGizmo> portalList = new List<UIPortalStatusGizmo>();

	protected List<UICannonGizmo> cannonList = new List<UICannonGizmo>();

	protected List<UIGrabStatusGizmo> grabList = new List<UIGrabStatusGizmo>();

	protected List<UISonarGizmo> sonarList = new List<UISonarGizmo>();

	protected List<UIWaveTargetGizmo> waveTargetList = new List<UIWaveTargetGizmo>();

	private int depth;

	private void LateUpdate()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (GameSaveData.instance.headName && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Vector3 val = Vector3.get_zero();
			StageObject stageObject = null;
			if (MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				val = MonoBehaviourSingleton<StageObjectManager>.I.self.GetCameraTargetPos();
				val.y = 0f;
				if (MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint != null)
				{
					stageObject = MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint.owner;
				}
			}
			List<StageObject> list = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				Enemy enemy = list[i] as Enemy;
				if (!enemy.isDead && enemy.isInitialized)
				{
					float num = Vector3.Distance(val, list[i]._position);
					if (num < enemy.enemyParameter.showStatusUIRange || stageObject == enemy)
					{
						enemy.CreateStatusGizmo();
					}
					else if (num > enemy.enemyParameter.showStatusUIRange + 1f)
					{
						enemy.DeleteStatusGizmo();
					}
				}
			}
		}
	}

	public UIPlayerStatusGizmo Create(Player owner)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		UIPlayerStatusGizmo uIPlayerStatusGizmo = null;
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			if (!playerList[i].get_gameObject().get_activeSelf())
			{
				uIPlayerStatusGizmo = playerList[i];
				break;
			}
		}
		if (uIPlayerStatusGizmo == null)
		{
			Transform val = ResourceUtility.Realizes(playerStatusGizmo, base._transform, -1);
			if (val == null)
			{
				return null;
			}
			uIPlayerStatusGizmo = val.GetComponent<UIPlayerStatusGizmo>();
			if (uIPlayerStatusGizmo == null)
			{
				Object.Destroy(val);
				return null;
			}
			playerList.Add(uIPlayerStatusGizmo);
		}
		uIPlayerStatusGizmo.targetPlayer = owner;
		SetDepth(uIPlayerStatusGizmo.get_gameObject());
		return uIPlayerStatusGizmo;
	}

	public UIEnemyStatusGizmo Create(Enemy owner)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		UIEnemyStatusGizmo uIEnemyStatusGizmo = null;
		int i = 0;
		for (int count = enemyList.Count; i < count; i++)
		{
			if (!enemyList[i].get_gameObject().get_activeSelf())
			{
				uIEnemyStatusGizmo = enemyList[i];
				break;
			}
		}
		if (uIEnemyStatusGizmo == null)
		{
			Transform val = ResourceUtility.Realizes(enemyStatusGizmo, base._transform, -1);
			if (val == null)
			{
				return null;
			}
			uIEnemyStatusGizmo = val.GetComponent<UIEnemyStatusGizmo>();
			if (uIEnemyStatusGizmo == null)
			{
				Object.Destroy(val);
				return null;
			}
			enemyList.Add(uIEnemyStatusGizmo);
		}
		uIEnemyStatusGizmo.targetEnemy = owner;
		SetDepth(uIEnemyStatusGizmo.get_gameObject());
		return uIEnemyStatusGizmo;
	}

	public UIPortalStatusGizmo Create(PortalObject owner)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		UIPortalStatusGizmo uIPortalStatusGizmo = null;
		int i = 0;
		for (int count = portalList.Count; i < count; i++)
		{
			if (!portalList[i].get_gameObject().get_activeSelf())
			{
				uIPortalStatusGizmo = portalList[i];
				break;
			}
		}
		if (uIPortalStatusGizmo == null)
		{
			Transform val = ResourceUtility.Realizes(portalStatusGizmo, base._transform, -1);
			if (val == null)
			{
				return null;
			}
			uIPortalStatusGizmo = val.GetComponent<UIPortalStatusGizmo>();
			if (uIPortalStatusGizmo == null)
			{
				Object.Destroy(val);
				return null;
			}
			portalList.Add(uIPortalStatusGizmo);
		}
		uIPortalStatusGizmo.portal = owner;
		SetDepth(uIPortalStatusGizmo.get_gameObject());
		return uIPortalStatusGizmo;
	}

	public UICannonGizmo Create(FieldGimmickCannonObject owner)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		UICannonGizmo orCreate = this.GetOrCreate<UICannonGizmo>(cannonList, cannonGizmo);
		orCreate.owner = owner;
		SetDepth(orCreate.get_gameObject());
		return orCreate;
	}

	public UISonarGizmo CreateSonar(FieldSonarObject sonar)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		UISonarGizmo orCreate = this.GetOrCreate<UISonarGizmo>(sonarList, sonarGizmo);
		orCreate.sonar = sonar;
		SetDepth(orCreate.get_gameObject());
		return orCreate;
	}

	public UIGrabStatusGizmo CreateGrab()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		UIGrabStatusGizmo orCreate = this.GetOrCreate<UIGrabStatusGizmo>(grabList, grabStatusGizmo);
		SetDepth(orCreate.get_gameObject());
		return orCreate;
	}

	public UIWaveTargetGizmo CreateWaveTarget(FieldWaveTargetObject wt)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		UIWaveTargetGizmo orCreate = this.GetOrCreate<UIWaveTargetGizmo>(waveTargetList, waveTargetGizmo);
		orCreate.waveTarget = wt;
		orCreate.Initialize();
		SetDepth(orCreate.get_gameObject());
		return orCreate;
	}

	protected T GetOrCreate<T>(List<T> objects, GameObject obj) where T : MonoBehaviour
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		T val = (T)(object)null;
		for (int i = 0; i < objects.Count; i++)
		{
			if (!objects[i].get_gameObject().get_activeSelf())
			{
				val = objects[i];
				break;
			}
		}
		if ((object)val == null)
		{
			Transform val2 = ResourceUtility.Realizes(obj, base._transform, -1);
			if (val2 == null)
			{
				return (T)(object)null;
			}
			val = val2.GetComponent<T>();
			if ((object)val == null)
			{
				Object.Destroy(val2);
				return (T)(object)null;
			}
			objects.Add(val);
		}
		return val;
	}

	private void SetDepth(GameObject go)
	{
		UIPanel component = go.GetComponent<UIPanel>();
		if (component != null)
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
