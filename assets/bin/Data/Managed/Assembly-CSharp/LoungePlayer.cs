using Network;
using System.Collections;
using UnityEngine;

public class LoungePlayer : HomePlayerCharacterBase
{
	protected const float NeedMovingDistance = 0.5f;

	protected const float AFKTime = 150f;

	protected Vector3 moveTargetPoint;

	protected bool isMoving;

	protected float sitTimer;

	protected LoungeNamePlateStatus namePlateStatus;

	protected float afkTimer;

	protected bool isInitPos;

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
		if (LoungeCharaInfo == null)
		{
			return 0;
		}
		return LoungeCharaInfo.userId;
	}

	public void SetLoungeCharaInfo(CharaInfo info)
	{
		LoungeCharaInfo = info;
	}

	public void SetInitialPosition(Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		InitialPos = pos;
		base._transform.position = InitialPos.Value;
		isInitPos = true;
		base.moveTargetPos = InitialPos.Value;
		if (type == LOUNGE_ACTION_TYPE.SIT && !isSitting)
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
		if (IsValidMove() && !isSitting)
		{
			if (!isSitting && animCtrl != null && !isStanding)
			{
				animCtrl.PlayDefault();
			}
			if (Vector3.Distance(base._transform.position, base.moveTargetPos) > 0.5f)
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
		if (!(namePlate == null))
		{
			Vector3 position = (!(base.Head != null)) ? (base._transform.position + new Vector3(0f, 1.9f, 0f)) : (base.Head.position + new Vector3(0f, 0.4f, 0f));
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
				namePlate.gameObject.SetActive(value: false);
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
			yield return null;
		}
		while (!isInitPos)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(base._transform.position);
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(base._transform.position);
		}
		yield return StartCoroutine(base.DoSit());
	}

	protected override IEnumerator StandUp()
	{
		while (!isInitPos)
		{
			yield return null;
		}
		yield return StartCoroutine(base.StandUp());
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType == 0) ? PLCA.RUN : PLCA.RUN_F);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.speed = 1f;
	}

	protected virtual IEnumerator Move()
	{
		isInitPos = true;
		isMoving = true;
		while (true)
		{
			Vector3 position = base._transform.position;
			Vector3 vector = base.moveTargetPos - position;
			float y = Quaternion.LookRotation(vector.ToVector2XZ().normalized.ToVector3XZ()).eulerAngles.y;
			float currentVelocity = 0f;
			y = Mathf.SmoothDampAngle(base._transform.eulerAngles.y, y, ref currentVelocity, 0.1f);
			base._transform.eulerAngles = new Vector3(0f, y, 0f);
			if (vector.magnitude < 1f)
			{
				animCtrl.Play(PLCA.WALK);
			}
			else
			{
				animCtrl.PlayMove();
			}
			if (vector.magnitude < 0.5f)
			{
				break;
			}
			yield return null;
		}
		isMoving = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		return Load(this, base.gameObject, LoungeCharaInfo, null);
	}

	protected virtual PlayerLoader Load(LoungePlayer chara, GameObject go, CharaInfo chara_info, PlayerLoader.OnCompleteLoad callback)
	{
		PlayerLoader playerLoader = go.AddComponent<PlayerLoader>();
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		if (chara_info != null)
		{
			playerLoadInfo.Apply(chara_info, need_weapon: false, need_helm: true, need_leg: true, is_priority_visual_equip: true);
			chara.sexType = chara_info.sex;
		}
		playerLoader.StartLoad(playerLoadInfo, 8, 99, need_anim_event: false, need_foot_stamp: false, need_shadow: true, enable_light_probes: true, need_action_voice: false, need_high_reso_tex: false, need_res_ref_count: true, need_dev_frame_instantiate: true, SHADER_TYPE.NORMAL, callback);
		return playerLoader;
	}

	public virtual void OnRecvSit()
	{
		isSitting = true;
		StartCoroutine(DoSit());
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
	}

	public virtual void OnRecvStandUp()
	{
		isSitting = false;
		StartCoroutine(StandUp());
		if (chairPoint != null)
		{
			chairPoint.ResetSittingCharacter();
		}
		base.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
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
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("LoungePlayerCharacter", base.gameObject, "LOUNGE_FRIEND", LoungeCharaInfo);
		return true;
	}

	protected virtual bool IsValidMove()
	{
		if (animCtrl == null)
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
		if (isSit)
		{
			return false;
		}
		return true;
	}
}
