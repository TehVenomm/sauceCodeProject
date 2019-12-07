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

	public Vector3 defaultPosition = Vector3.zero;

	public Quaternion defaultRotation = Quaternion.identity;

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
			return !animator.applyRootMotion;
		}
	}

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
			moveCollider.enabled = is_visible;
			if (namePlate != null)
			{
				namePlate.gameObject.SetActive(is_visible);
			}
			base.enabled = is_visible;
		}
	}

	protected abstract ModelLoaderBase LoadModel();

	protected virtual void InitAnim()
	{
		animCtrl = PlayerAnimCtrl.Get(animator, (sexType == 0) ? PLCA.IDLE_01 : PLCA.IDLE_01_F, OnAnimPlay, null, OnAnimEnd);
		animCtrl.moveAnim = PLCA.WALK;
	}

	protected virtual void InitCollider()
	{
		SetCollider(1.8f, 0.5f);
	}

	protected void SetCollider(float height, float radius)
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.isKinematic = true;
		rigidbody.drag = 0f;
		rigidbody.angularDrag = 100f;
		rigidbody.useGravity = false;
		rigidbody.constraints = (RigidbodyConstraints)84;
		CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
		capsuleCollider.direction = 1;
		capsuleCollider.height = height;
		capsuleCollider.radius = radius;
		capsuleCollider.center = new Vector3(0f, height * 0.5f, 0f);
		moveCollider = capsuleCollider;
	}

	protected virtual bool IsVisibleNamePlate()
	{
		return true;
	}

	protected virtual void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
	{
		animator.applyRootMotion = ((anim == anim_ctrl.moveAnim) ? true : false);
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
		_transform = base.transform;
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
			interpolator = base.gameObject.AddComponent<TransformInterpolator>();
			animator = loader.GetAnimator();
			if (!(animator == null))
			{
				animator.gameObject.AddComponent<RootMotionProxy>();
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
			interpolator = base.gameObject.AddComponent<TransformInterpolator>();
			animator = loader.GetAnimator();
			if (!(animator == null))
			{
				animator.gameObject.AddComponent<RootMotionProxy>();
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
		if (namePlate == null)
		{
			return;
		}
		if (!GameSaveData.instance.headName)
		{
			namePlate.gameObject.SetActive(value: false);
			return;
		}
		Vector3 position = _transform.position + new Vector3(0f, 1.9f, 0f);
		position = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(position);
		position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		if (position.z >= 0f && IsVisibleNamePlate())
		{
			position.z = 0f;
			namePlate.gameObject.SetActive(value: true);
			namePlate.position = position;
		}
		else
		{
			namePlate.gameObject.SetActive(value: false);
		}
	}

	private bool CheckBackPosition(Vector3 startDir)
	{
		Vector3 from = defaultPosition - _transform.position;
		from.Normalize();
		Vector3 position = _transform.position;
		if (position.x < defaultPosition.x || position.z > defaultPosition.z)
		{
			return true;
		}
		float angle = 0f - Vector3.Angle(from, Vector3.forward);
		_transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
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
				Object.DestroyImmediate(namePlate.gameObject);
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
						HomeNPCCharacter homeNPCCharacter = (HomeNPCCharacter)this;
						if (homeNPCCharacter != null && homeNPCCharacter.nearAnim != PLCA.IDLE_01)
						{
							if ((iHomePeople.selfChara._transform.position.ToVector2XZ() - _transform.position.ToVector2XZ()).sqrMagnitude < 9f)
							{
								PlayNearAnim(homeNPCCharacter);
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
					discussionTimer -= Time.deltaTime;
				}
				yield return null;
				continue;
			}
			SetupNextWayPoint();
			yield return null;
			IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
			moveTargetPos = currentIHomeManager.IHomePeople.GetTargetPos(this, wayPoint);
			while (true)
			{
				animCtrl.PlayMove();
				Vector3 position = _transform.position;
				Vector3 vector = moveTargetPos - position;
				float y = Quaternion.LookRotation(vector.ToVector2XZ().normalized.ToVector3XZ()).eulerAngles.y;
				float currentVelocity = 0f;
				y = Mathf.SmoothDampAngle(_transform.eulerAngles.y, y, ref currentVelocity, 0.1f);
				_transform.eulerAngles = new Vector3(0f, y, 0f);
				if (vector.magnitude < 0.75f)
				{
					break;
				}
				yield return null;
			}
			if (wayPoint.name.StartsWith("LEAF"))
			{
				break;
			}
			if (wayPoint.name.StartsWith("WAIT"))
			{
				while (true)
				{
					float y2 = wayPoint.transform.eulerAngles.y;
					float currentVelocity2 = 0f;
					y2 = Mathf.SmoothDampAngle(_transform.eulerAngles.y, y2, ref currentVelocity2, 0.1f);
					_transform.eulerAngles = new Vector3(0f, y2, 0f);
					currentVelocity2 = Mathf.Abs(currentVelocity2);
					if (currentVelocity2 > 15f)
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
					animator.applyRootMotion = false;
					if (currentVelocity2 < 0.01f)
					{
						break;
					}
					yield return null;
				}
				animCtrl.PlayIdleAnims(sexType);
				waitTime = Random.Range(3f, 8f);
			}
			else if (wayPoint.name == "CENTER")
			{
				waitTime = Random.Range(-3f, 8f);
			}
		}
		Object.Destroy(base.gameObject);
	}

	protected virtual void PlayNearAnim(HomeNPCCharacter npc)
	{
		animCtrl.Play(npc.nearAnim);
	}

	private void WaitInFreeMove()
	{
		waitTime -= Time.deltaTime;
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
			float angle = 320f;
			_transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
			animCtrl.SetMoveRunAnim(1);
			animCtrl.Play(PLCA.RUN_F);
			Vector3 savePos2;
			if ((float)Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).stumblePercent > (float)Random.Range(0, 100))
			{
				yield return new WaitForSeconds(0.5f);
				animCtrl.moveAnim = PLCA.STUN;
				animCtrl.Play(PLCA.STUN);
				yield return new WaitForSeconds(0.5f);
				savePos2 = _transform.position;
				while (!animCtrl.IsPlayingIdleAnims(0))
				{
					_transform.position = savePos2;
					yield return null;
				}
				animCtrl.moveAnim = PLCA.RUN_F;
				animCtrl.Play(PLCA.RUN_F);
			}
			Vector3 startDir = defaultPosition - _transform.position;
			startDir.Normalize();
			while (!CheckBackPosition(startDir))
			{
				yield return null;
			}
			animCtrl.PlayDefault();
			yield return new WaitForSeconds(0.3f);
			savePos2 = _transform.position;
			animCtrl.moveAnim = PLCA.TURN_L;
			animCtrl.Play(PLCA.TURN_L);
			yield return null;
			while (!animCtrl.IsPlayingIdleAnims(0))
			{
				_transform.position = savePos2;
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
			base.gameObject.SetActive(value: false);
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
		return GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.selfChara;
	}
}
