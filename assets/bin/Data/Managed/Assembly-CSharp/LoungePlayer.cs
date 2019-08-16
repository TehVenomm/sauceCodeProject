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
		return (LoungeCharaInfo != null) ? LoungeCharaInfo.userId : 0;
	}

	public void SetLoungeCharaInfo(CharaInfo info)
	{
		LoungeCharaInfo = info;
	}

	public void SetInitialPosition(Vector3 pos, LOUNGE_ACTION_TYPE type)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		InitialPos = pos;
		base._transform.set_position(InitialPos.Value);
		isInitPos = true;
		base.moveTargetPos = InitialPos.Value;
		if (type == LOUNGE_ACTION_TYPE.SIT && !isSitting)
		{
			OnRecvSit();
		}
	}

	public void SetMoveTargetPosition(Vector3 pos)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		afkTimer += Time.get_deltaTime();
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
			float num = Vector3.Distance(base._transform.get_position(), base.moveTargetPos);
			if (num > 0.5f)
			{
				base.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
				isMoving = true;
				isSitting = false;
				this.StartCoroutine(Move());
			}
		}
	}

	protected override void CreateNamePlate()
	{
		if (LoungeCharaInfo != null)
		{
			namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeNamePlate(LoungeCharaInfo.name);
			namePlateStatus = namePlate.get_gameObject().AddComponent<LoungeNamePlateStatus>();
			namePlateStatus.SetPlayer(this);
		}
	}

	protected override void UpdateNamePlatePos()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (!(namePlate == null))
		{
			Vector3 val = (!(base.Head != null)) ? (base._transform.get_position() + new Vector3(0f, 1.9f, 0f)) : (base.Head.get_position() + new Vector3(0f, 0.4f, 0f));
			val = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(val);
			val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
			if (val.z >= 0f)
			{
				val.z = 0f;
				namePlateStatus.SetActiveNamePlate(GameSaveData.instance.headName);
				namePlate.set_position(val);
			}
			else
			{
				namePlate.get_gameObject().SetActive(false);
			}
		}
	}

	protected override IEnumerator DoSit()
	{
		while (isMoving)
		{
			sitTimer += Time.get_deltaTime();
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
			chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(base._transform.get_position());
		}
		if (MonoBehaviourSingleton<ClanManager>.IsValid())
		{
			chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(base._transform.get_position());
		}
		yield return this.StartCoroutine(base.DoSit());
	}

	protected override IEnumerator StandUp()
	{
		while (!isInitPos)
		{
			yield return null;
		}
		yield return this.StartCoroutine(base.StandUp());
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType != 0) ? PLCA.RUN_F : PLCA.RUN);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.set_speed(1f);
	}

	protected virtual IEnumerator Move()
	{
		isInitPos = true;
		isMoving = true;
		while (true)
		{
			Vector3 pos = base._transform.get_position();
			Vector3 diff = base.moveTargetPos - pos;
			Vector2 val = diff.ToVector2XZ();
			Vector2 dir = val.get_normalized();
			Quaternion val2 = Quaternion.LookRotation(dir.ToVector3XZ());
			Vector3 eulerAngles = val2.get_eulerAngles();
			float rot2 = eulerAngles.y;
			float vel = 0f;
			Vector3 eulerAngles2 = base._transform.get_eulerAngles();
			rot2 = Mathf.SmoothDampAngle(eulerAngles2.y, rot2, ref vel, 0.1f);
			base._transform.set_eulerAngles(new Vector3(0f, rot2, 0f));
			if (diff.get_magnitude() < 1f)
			{
				animCtrl.Play(PLCA.WALK);
			}
			else
			{
				animCtrl.PlayMove();
			}
			if (diff.get_magnitude() < 0.5f)
			{
				break;
			}
			yield return null;
		}
		isMoving = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		return Load(this, this.get_gameObject(), LoungeCharaInfo, null);
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
		this.StartCoroutine(DoSit());
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
	}

	public virtual void OnRecvStandUp()
	{
		isSitting = false;
		this.StartCoroutine(StandUp());
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
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("LoungePlayerCharacter", this.get_gameObject(), "LOUNGE_FRIEND", LoungeCharaInfo);
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
