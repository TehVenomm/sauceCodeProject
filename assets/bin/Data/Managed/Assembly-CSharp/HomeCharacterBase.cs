using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomeCharacterBase : MonoBehaviour
{
	public enum STATE
	{
		FREE,
		OUT_CONTROLL,
		BACK_POSITION,
		LEAVE,
		STOP,
		STOP_END
	}

	public Vector3 defaultPosition = Vector3.get_zero();

	public Quaternion defaultRotation = Quaternion.get_identity();

	private static readonly PLCA[] talkAnims = new PLCA[3]
	{
		PLCA.IDLE_01,
		PLCA.IDLE_02,
		PLCA.IDLE_03
	};

	protected int sexType;

	protected IHomePeople iHomePeople;

	protected Transform namePlate;

	protected Animator animator;

	protected PlayerAnimCtrl animCtrl;

	protected CapsuleCollider moveCollider;

	protected TransformInterpolator interpolator;

	protected WayPoint wayPoint;

	protected List<WayPoint> wayHistory = new List<WayPoint>();

	protected float waitTime;

	protected float discussionTimer;

	protected STATE state;

	protected ManualCoroutineList coroutines = new ManualCoroutineList();

	public bool isLoading
	{
		get;
		protected set;
	}

	public ModelLoaderBase loader
	{
		get;
		protected set;
	}

	public Transform _transform
	{
		get;
		private set;
	}

	public Vector3 moveTargetPos
	{
		get;
		protected set;
	}

	public Vector3 emotionTargetPos
	{
		get;
		set;
	}

	public bool isStop
	{
		get
		{
			if (animator == null)
			{
				return true;
			}
			return !animator.get_applyRootMotion();
		}
	}

	protected HomeCharacterBase()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_000c: Unknown result type (might be due to invalid IL or missing references)
	//IL_0011: Unknown result type (might be due to invalid IL or missing references)


	public void SetHomePeople(IHomePeople homePeople)
	{
		iHomePeople = homePeople;
	}

	public void SetWaitTime(float time)
	{
		waitTime = time;
	}

	public void SetWayPoint(WayPoint wayPoint)
	{
		this.wayPoint = wayPoint;
	}

	public virtual FriendCharaInfo GetFriendCharaInfo()
	{
		return null;
	}

	public Transform GetNamePlate()
	{
		return namePlate;
	}

	public void StopDiscussion()
	{
		discussionTimer = -1f;
	}

	public virtual bool DispatchEvent()
	{
		return false;
	}

	public void PushOutControll()
	{
		state = STATE.OUT_CONTROLL;
		coroutines.Push(1);
	}

	public void PushBackPosition()
	{
		state = STATE.BACK_POSITION;
		coroutines.Push(2);
	}

	public void PushLeave()
	{
		state = STATE.LEAVE;
		coroutines.Push(3);
	}

	public void PopState()
	{
		coroutines.Pop();
		state = (STATE)coroutines.Peek();
	}

	public void StopMoving()
	{
		if (state != STATE.STOP)
		{
			state = STATE.STOP;
			if (this is HomePlayerCharacter)
			{
				animCtrl.PlayIdleAnims(sexType);
			}
			if (this is LoungeMoveNPC)
			{
				animCtrl.PlayDefault();
			}
			coroutines.Push(4);
		}
	}

	public void SetVisible(bool is_visible)
	{
		if (!(loader == null) && !isLoading)
		{
			loader.SetEnabled(is_visible);
			moveCollider.set_enabled(is_visible);
			if (namePlate != null)
			{
				namePlate.get_gameObject().SetActive(is_visible);
			}
			this.set_enabled(is_visible);
		}
	}

	protected abstract ModelLoaderBase LoadModel();

	protected virtual void InitAnim()
	{
		animCtrl = PlayerAnimCtrl.Get(animator, (sexType != 0) ? PLCA.IDLE_01_F : PLCA.IDLE_01, OnAnimPlay, null, OnAnimEnd);
		animCtrl.moveAnim = PLCA.WALK;
	}

	protected virtual void InitCollider()
	{
		SetCollider(1.8f, 0.5f);
	}

	protected void SetCollider(float height, float radius)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Rigidbody val = this.get_gameObject().AddComponent<Rigidbody>();
		val.set_isKinematic(true);
		val.set_drag(0f);
		val.set_angularDrag(100f);
		val.set_useGravity(false);
		val.set_constraints(84);
		CapsuleCollider val2 = this.get_gameObject().AddComponent<CapsuleCollider>();
		val2.set_direction(1);
		val2.set_height(height);
		val2.set_radius(radius);
		val2.set_center(new Vector3(0f, height * 0.5f, 0f));
		moveCollider = val2;
	}

	protected virtual bool IsVisibleNamePlate()
	{
		return true;
	}

	protected virtual void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		animator.set_applyRootMotion((anim == anim_ctrl.moveAnim) ? true : false);
	}

	protected void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		if (this is LoungeMoveNPC)
		{
			animCtrl.PlayDefault();
		}
		else
		{
			animCtrl.PlayIdleAnims(sexType);
		}
	}

	private void Awake()
	{
		isLoading = true;
		_transform = this.get_transform();
	}

	private IEnumerator Start()
	{
		loader = LoadModel();
		if (!(this is HomePlayerCharacter))
		{
			while (loader.IsLoading())
			{
				yield return null;
			}
			CreateNamePlate();
			UpdateNamePlatePos();
			InitCollider();
			ChangeScale();
			interpolator = this.get_gameObject().AddComponent<TransformInterpolator>();
			animator = loader.GetAnimator();
			if (!(animator == null))
			{
				animator.get_gameObject().AddComponent<RootMotionProxy>();
				InitAnim();
				coroutines.Add(new ManualCoroutine(0, this, DoFreeMove(), _active: false));
				coroutines.Add(new ManualCoroutine(1, this, DoOutControll(), _active: false));
				coroutines.Add(new ManualCoroutine(2, this, DoBackPosition(), _active: false));
				coroutines.Add(new ManualCoroutine(3, this, DoLeave(), _active: false));
				coroutines.Add(new ManualCoroutine(4, this, DoStop(), _active: false));
				coroutines.Push(0);
				isLoading = false;
			}
		}
		else
		{
			isLoading = false;
			while (loader.IsLoading())
			{
				yield return null;
			}
			CreateNamePlate();
			UpdateNamePlatePos();
			InitCollider();
			ChangeScale();
			interpolator = this.get_gameObject().AddComponent<TransformInterpolator>();
			animator = loader.GetAnimator();
			if (!(animator == null))
			{
				animator.get_gameObject().AddComponent<RootMotionProxy>();
				InitAnim();
				coroutines.Add(new ManualCoroutine(0, this, DoFreeMove(), _active: false));
				coroutines.Add(new ManualCoroutine(1, this, DoOutControll(), _active: false));
				coroutines.Add(new ManualCoroutine(2, this, DoBackPosition(), _active: false));
				coroutines.Add(new ManualCoroutine(3, this, DoLeave(), _active: false));
				coroutines.Add(new ManualCoroutine(4, this, DoStop(), _active: false));
				coroutines.Push(0);
			}
		}
	}

	protected virtual void CreateNamePlate()
	{
		if (GetFriendCharaInfo() != null)
		{
			namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateNamePlate(GetFriendCharaInfo().name);
		}
	}

	protected virtual void ChangeScale()
	{
	}

	private void LateUpdate()
	{
		UpdateNamePlatePos();
	}

	protected virtual void UpdateNamePlatePos()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (namePlate == null)
		{
			return;
		}
		if (!GameSaveData.instance.headName)
		{
			namePlate.get_gameObject().SetActive(false);
			return;
		}
		Vector3 val = _transform.get_position() + new Vector3(0f, 1.9f, 0f);
		val = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(val);
		val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
		if (val.z >= 0f && IsVisibleNamePlate())
		{
			val.z = 0f;
			namePlate.get_gameObject().SetActive(true);
			namePlate.set_position(val);
		}
		else
		{
			namePlate.get_gameObject().SetActive(false);
		}
	}

	private bool CheckBackPosition(Vector3 startDir)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = defaultPosition - _transform.get_position();
		val.Normalize();
		Vector3 position = _transform.get_position();
		if (position.x < defaultPosition.x || position.z > defaultPosition.z)
		{
			return true;
		}
		float num = 0f - Vector3.Angle(val, Vector3.get_forward());
		_transform.set_rotation(Quaternion.AngleAxis(num, Vector3.get_up()));
		return false;
	}

	protected virtual void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			if (iHomePeople != null)
			{
				iHomePeople.OnDestroyHomeCharacter(this);
			}
			if (namePlate != null)
			{
				Object.DestroyImmediate(namePlate.get_gameObject());
				namePlate = null;
			}
		}
	}

	private IEnumerator DoFreeMove()
	{
		while (true)
		{
			if (waitTime > 0f)
			{
				WaitInFreeMove();
				yield return null;
				continue;
			}
			if (discussionTimer != 0f)
			{
				if (discussionTimer < 0f)
				{
					if (this is HomeNPCCharacter && iHomePeople.selfChara != null)
					{
						HomeNPCCharacter npc = (HomeNPCCharacter)this;
						if (npc != null && npc.nearAnim != PLCA.IDLE_01)
						{
							Vector2 val = iHomePeople.selfChara._transform.get_position().ToVector2XZ() - _transform.get_position().ToVector2XZ();
							if (val.get_sqrMagnitude() < 9f)
							{
								PlayNearAnim(npc);
							}
							else if (animCtrl.playingAnim != animCtrl.defaultAnim)
							{
								animCtrl.PlayDefault();
							}
							yield return null;
							continue;
						}
					}
					if (this is HomePlayerCharacter)
					{
						if (Random.Range(0, 10) == 0)
						{
							animCtrl.Play(PlayerAnimCtrl.emotionAnims);
							discussionTimer = Random.Range(2f, 4f);
						}
						else if (Random.Range(0, 2) == 0)
						{
							animCtrl.Play(PlayerAnimCtrl.talkAnims);
							discussionTimer = Random.Range(5f, 10f);
						}
						else
						{
							animCtrl.PlayIdleAnims(sexType);
							discussionTimer = Random.Range(3f, 6f);
						}
					}
				}
				else
				{
					discussionTimer -= Time.get_deltaTime();
				}
				yield return null;
				continue;
			}
			SetupNextWayPoint();
			yield return null;
			IHomeManager iHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
			moveTargetPos = iHomeManager.IHomePeople.GetTargetPos(this, wayPoint);
			while (true)
			{
				animCtrl.PlayMove();
				Vector3 pos = _transform.get_position();
				Vector3 diff = moveTargetPos - pos;
				Vector2 val2 = diff.ToVector2XZ();
				Vector2 dir3 = val2.get_normalized();
				Quaternion val3 = Quaternion.LookRotation(dir3.ToVector3XZ());
				Vector3 eulerAngles = val3.get_eulerAngles();
				float y = eulerAngles.y;
				float num = 0f;
				Vector3 eulerAngles2 = _transform.get_eulerAngles();
				y = Mathf.SmoothDampAngle(eulerAngles2.y, y, ref num, 0.1f);
				_transform.set_eulerAngles(new Vector3(0f, y, 0f));
				if (diff.get_magnitude() < 0.75f)
				{
					break;
				}
				yield return null;
			}
			if (wayPoint.get_name().StartsWith("LEAF"))
			{
				break;
			}
			if (wayPoint.get_name().StartsWith("WAIT"))
			{
				while (true)
				{
					Vector3 eulerAngles3 = wayPoint.get_transform().get_eulerAngles();
					float dir2 = eulerAngles3.y;
					float vel2 = 0f;
					Vector3 eulerAngles4 = _transform.get_eulerAngles();
					dir2 = Mathf.SmoothDampAngle(eulerAngles4.y, dir2, ref vel2, 0.1f);
					_transform.set_eulerAngles(new Vector3(0f, dir2, 0f));
					vel2 = Mathf.Abs(vel2);
					if (vel2 > 15f)
					{
						animCtrl.Play(PLCA.WALK);
					}
					else if (!string.IsNullOrEmpty(wayPoint.waitAnimStateName))
					{
						PLCA anim = PlayerAnimCtrl.StringToEnum(wayPoint.waitAnimStateName);
						animCtrl.Play(anim);
					}
					else if (this is LoungeMoveNPC)
					{
						animCtrl.PlayDefault();
					}
					else
					{
						animCtrl.PlayIdleAnims(sexType);
					}
					animator.set_applyRootMotion(false);
					if (vel2 < 0.01f)
					{
						break;
					}
					yield return null;
				}
				animCtrl.PlayIdleAnims(sexType);
				waitTime = Random.Range(3f, 8f);
			}
			else if (wayPoint.get_name() == "CENTER")
			{
				waitTime = Random.Range(-3f, 8f);
			}
		}
		Object.Destroy(this.get_gameObject());
	}

	protected virtual void PlayNearAnim(HomeNPCCharacter npc)
	{
		animCtrl.Play(npc.nearAnim);
	}

	private void WaitInFreeMove()
	{
		waitTime -= Time.get_deltaTime();
		if (!string.IsNullOrEmpty(wayPoint.waitAnimStateName))
		{
			PLCA anim = PlayerAnimCtrl.StringToEnum(wayPoint.waitAnimStateName);
			animCtrl.Play(anim);
		}
		else if (this is LoungeMoveNPC)
		{
			animCtrl.PlayDefault();
		}
		else
		{
			animCtrl.PlayIdleAnims(sexType);
		}
	}

	private void SetupNextWayPoint()
	{
		if (!wayHistory.Contains(this.wayPoint))
		{
			wayHistory.Add(this.wayPoint);
		}
		List<WayPoint> list = new List<WayPoint>();
		int i = 0;
		for (int num = this.wayPoint.links.Length; i < num; i++)
		{
			WayPoint wayPoint = this.wayPoint.links[i];
			if (!(wayPoint == null) && !wayHistory.Contains(wayPoint))
			{
				list.Add(wayPoint);
			}
		}
		if (list.Count > 0)
		{
			this.wayPoint = list[Random.Range(0, list.Count)];
		}
		else
		{
			this.wayPoint = this.wayPoint.links[Random.Range(0, this.wayPoint.links.Length)];
		}
	}

	private IEnumerator DoOutControll()
	{
		while (true)
		{
			yield return null;
		}
	}

	private IEnumerator DoBackPosition()
	{
		while (true)
		{
			yield return null;
			float maxAngle = 320f;
			_transform.set_rotation(Quaternion.AngleAxis(maxAngle, Vector3.get_up()));
			animCtrl.SetMoveRunAnim(1);
			animCtrl.Play(PLCA.RUN_F);
			float stumblePercent = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).stumblePercent;
			Vector3 savePos2;
			if (stumblePercent > (float)Random.Range(0, 100))
			{
				yield return (object)new WaitForSeconds(0.5f);
				animCtrl.moveAnim = PLCA.STUN;
				animCtrl.Play(PLCA.STUN);
				yield return (object)new WaitForSeconds(0.5f);
				savePos2 = _transform.get_position();
				while (!animCtrl.IsPlayingIdleAnims(0))
				{
					_transform.set_position(savePos2);
					yield return null;
				}
				animCtrl.moveAnim = PLCA.RUN_F;
				animCtrl.Play(PLCA.RUN_F);
			}
			Vector3 startDir = defaultPosition - _transform.get_position();
			startDir.Normalize();
			while (!CheckBackPosition(startDir))
			{
				yield return null;
			}
			animCtrl.PlayDefault();
			yield return (object)new WaitForSeconds(0.3f);
			savePos2 = _transform.get_position();
			animCtrl.moveAnim = PLCA.TURN_L;
			animCtrl.Play(PLCA.TURN_L);
			yield return null;
			while (!animCtrl.IsPlayingIdleAnims(0))
			{
				_transform.set_position(savePos2);
				yield return null;
			}
			animCtrl.SetMoveRunAnim(1);
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private IEnumerator DoLeave()
	{
		while (true)
		{
			yield return null;
			while (!animCtrl.IsPlayingIdleAnims(0))
			{
				yield return null;
			}
			this.get_gameObject().SetActive(false);
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private IEnumerator DoStop()
	{
		while (true)
		{
			yield return null;
			if (this is HomePlayerCharacter)
			{
				animCtrl.Play(talkAnims);
			}
			while (!GetSelfCharacter().IsEnableControl())
			{
				yield return null;
			}
			if (this is HomePlayerCharacter)
			{
				animCtrl.PlayIdleAnims(sexType);
			}
			if (this is LoungeMoveNPC)
			{
				animCtrl.PlayDefault();
			}
			coroutines.Pop();
			state = (STATE)coroutines.Peek();
		}
	}

	private HomeSelfCharacter GetSelfCharacter()
	{
		IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
		return currentIHomeManager.IHomePeople.selfChara;
	}
}
