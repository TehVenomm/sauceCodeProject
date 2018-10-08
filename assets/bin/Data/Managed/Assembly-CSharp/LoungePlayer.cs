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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		InitialPos = pos;
		base._transform.set_position(InitialPos.Value);
		base.moveTargetPos = InitialPos.Value;
		if (type == LOUNGE_ACTION_TYPE.SIT)
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
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
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
		if (IsValidMove())
		{
			if (!isSitting)
			{
				animCtrl.PlayDefault(false);
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
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
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
			yield return (object)null;
		}
		chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this);
		yield return (object)this.StartCoroutine(base.DoSit());
	}

	protected override void InitAnim()
	{
		base.InitAnim();
		animCtrl.moveAnim = ((sexType != 0) ? PLCA.RUN_F : PLCA.RUN);
		animCtrl.transitionDuration = 0.15f;
		animCtrl.animator.set_speed(1f);
	}

	private IEnumerator Move()
	{
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
				animCtrl.Play(PLCA.WALK, false);
			}
			else
			{
				animCtrl.PlayMove(false);
			}
			if (diff.get_magnitude() < 0.5f)
			{
				break;
			}
			yield return (object)null;
		}
		isMoving = false;
	}

	protected override ModelLoaderBase LoadModel()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		return Load(this, this.get_gameObject(), LoungeCharaInfo, null);
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		isSitting = true;
		this.StartCoroutine(DoSit());
		base.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
	}

	public void OnRecvStandUp()
	{
		isSitting = false;
		if (!(chairPoint == null))
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		if (LoungeCharaInfo == null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(LoungeCharaInfo.userId) == null)
		{
			return false;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("LoungePlayerCharacter", this.get_gameObject(), "LOUNGE_FRIEND", LoungeCharaInfo, null, true);
		return true;
	}

	private bool IsValidMove()
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
		return true;
	}
}
