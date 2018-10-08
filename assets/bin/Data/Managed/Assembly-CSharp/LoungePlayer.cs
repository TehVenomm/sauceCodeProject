using Network;
using System.Collections;
using UnityEngine;

public class LoungePlayer : HomePlayerCharacterBase
{
	private const float NeedMovingDistance = 0.5f;

	private const float AFKTime = 150f;

	private Vector3 moveTargetPoint;

	private bool isMoving;

	private float sitTimer;

	private LoungeNamePlateStatus namePlateStatus;

	private float afkTimer;

	public CharaInfo LoungeCharaInfo
	{
		get;
		private set;
	}

	public Vector3? InitialPos
	{
		get;
		private set;
	}

	public override int GetUserId()
	{
		return (LoungeCharaInfo != null) ? LoungeCharaInfo.userId : 0;
	}

	public void SetLoungeCharaInfo(CharaInfo info)
	{
		LoungeCharaInfo = info;
	}

	public void SetInitialPosition(Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		InitialPos = pos;
		base._transform.position = InitialPos.Value;
		base.moveTargetPos = InitialPos.Value;
		if (type == LOUNGE_ACTION_TYPE.SIT)
		{
			OnRecvSit();
		}
	}

	public void SetMoveTargetPosition(Vector3 pos)
	{
		if (!isPlayingSitAnimation)
		{
			base.moveTargetPos = pos;
		}
	}

	public void ResetAction()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
	}

	public void ResetAFKTimer()
	{
		afkTimer = 0f;
	}

	private void Update()
	{
		afkTimer += Time.deltaTime;
		if (afkTimer > 150f)
		{
			if (base.CurrentActionType == LOUNGE_ACTION_TYPE.NONE || base.CurrentActionType == LOUNGE_ACTION_TYPE.SIT)
			{
				base.CurrentActionType = LOUNGE_ACTION_TYPE.AFK;
			}
			else
			{
				ResetAFKTimer();
			}
		}
		if (IsValidMove())
		{
			if (!isSitting)
			{
				animCtrl.PlayDefault(false);
			}
			float num = Vector3.Distance(base._transform.position, base.moveTargetPos);
			if (num > 0.5f)
			{
				base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
				isMoving = true;
				isSitting = false;
				StartCoroutine(Move());
			}
		}
	}

	protected override void CreateNamePlate()
	{
		if (LoungeCharaInfo != null)
		{
			namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeNamePlate(LoungeCharaInfo.name);
			namePlateStatus = namePlate.gameObject.AddComponent<LoungeNamePlateStatus>();
			namePlateStatus.SetPlayer(this);
		}
	}

	protected override void UpdateNamePlatePos()
	{
		if (!((Object)namePlate == (Object)null))
		{
			Vector3 position = (!((Object)base.Head != (Object)null)) ? (base._transform.position + new Vector3(0f, 1.9f, 0f)) : (base.Head.position + new Vector3(0f, 0.4f, 0f));
			position = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(position);
			position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			if (position.z >= 0f)
			{
				position.z = 0f;
				namePlateStatus.SetActiveNamePlate(GameSaveData.instance.headName);
				namePlate.position = position;
			}
			else
			{
				namePlate.gameObject.SetActive(false);
			}
		}
	}

	protected override IEnumerator DoSit()
	{
		while (isMoving)
		{
			sitTimer += Time.deltaTime;
			if (10f < sitTimer)
			{
				yield break;
			}
			yield return (object)null;
		}
		chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this);
		yield return (object)StartCoroutine(base.DoSit());
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType != 0) ? PLCA.RUN_F : PLCA.RUN);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.speed = 1f;
	}

	private IEnumerator Move()
	{
		isMoving = true;
		while (true)
		{
			Vector3 pos = base._transform.position;
			Vector3 diff = base.moveTargetPos - pos;
			Vector2 dir = diff.ToVector2XZ().normalized;
			Vector3 eulerAngles = Quaternion.LookRotation(dir.ToVector3XZ()).eulerAngles;
			float rot2 = eulerAngles.y;
			float vel = 0f;
			Vector3 eulerAngles2 = base._transform.eulerAngles;
			rot2 = Mathf.SmoothDampAngle(eulerAngles2.y, rot2, ref vel, 0.1f);
			base._transform.eulerAngles = new Vector3(0f, rot2, 0f);
			if (diff.magnitude < 1f)
			{
				animCtrl.Play(PLCA.WALK, false);
			}
			else
			{
				animCtrl.PlayMove(false);
			}
			if (diff.magnitude < 0.5f)
			{
				break;
			}
			yield return (object)null;
		}
		isMoving = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		return Load(this, base.gameObject, LoungeCharaInfo, null);
	}

	private PlayerLoader Load(LoungePlayer chara, GameObject go, CharaInfo chara_info, PlayerLoader.OnCompleteLoad callback)
	{
		PlayerLoader playerLoader = go.AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		if (chara_info != null)
		{
			playerLoadInfo.Apply(chara_info, false, true, true, true);
			chara.sexType = chara_info.sex;
		}
		playerLoader.StartLoad(playerLoadInfo, 8, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, callback, true, -1);
		return playerLoader;
	}

	public void OnRecvSit()
	{
		isSitting = true;
		StartCoroutine(DoSit());
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
	}

	public void OnRecvStandUp()
	{
		isSitting = false;
		if (!((Object)chairPoint == (Object)null))
		{
			chairPoint.ResetSittingCharacter();
			base.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
		}
	}

	public void OnRecvToGacha()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.TO_GACHA;
	}

	public void OnRecvToEquip()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.TO_EQUIP;
	}

	public void OnRecvAFK()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.AFK;
	}

	public void OnRecvNone()
	{
		base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
	}

	public override bool DispatchEvent()
	{
		if (LoungeCharaInfo == null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(LoungeCharaInfo.userId) == null)
		{
			return false;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("LoungePlayerCharacter", base.gameObject, "LOUNGE_FRIEND", LoungeCharaInfo, null, true);
		return true;
	}

	private bool IsValidMove()
	{
		if ((Object)animCtrl == (Object)null)
		{
			return false;
		}
		if (isMoving)
		{
			return false;
		}
		if (isPlayingSitAnimation)
		{
			return false;
		}
		return true;
	}
}
