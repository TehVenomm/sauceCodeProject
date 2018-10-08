using System.Collections.Generic;
using UnityEngine;

public class UISpectatorButton : MonoBehaviourSingleton<UISpectatorButton>
{
	[SerializeField]
	protected UIButton prevButton;

	[SerializeField]
	protected UIButton nextButton;

	[SerializeField]
	protected UILabel viewingPlayerLabel;

	private Player currentTarget;

	protected UIStaticPanelChanger panelChange;

	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (currentTarget == null)
		{
			this.get_gameObject().SetActive(false);
		}
	}

	public void Initialize(UIStaticPanelChanger panelChange)
	{
		this.panelChange = panelChange;
	}

	public void BeginSpect()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Player value = GetPlayers().First.Value;
		SetTarget(value);
		this.get_gameObject().SetActive(true);
	}

	public void OnPrev()
	{
		LinkedList<Player> players = GetPlayers();
		LinkedListNode<Player> linkedListNode = players.Find(currentTarget);
		if (linkedListNode == null)
		{
			BeginSpect();
		}
		else
		{
			Player target = (linkedListNode.Previous != null) ? linkedListNode.Previous.Value : players.Last.Value;
			SetTarget(target);
		}
	}

	public void OnNext()
	{
		LinkedList<Player> players = GetPlayers();
		LinkedListNode<Player> linkedListNode = players.Find(currentTarget);
		if (linkedListNode == null)
		{
			BeginSpect();
		}
		else
		{
			Player target = (linkedListNode.Next != null) ? linkedListNode.Next.Value : players.First.Value;
			SetTarget(target);
		}
	}

	private void SetTarget(Player target)
	{
		currentTarget = target;
		viewingPlayerLabel.text = currentTarget.charaName;
		MonoBehaviourSingleton<InGameCameraManager>.I.target = currentTarget._transform;
	}

	public void EndSpect()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient != null && Object.op_Implicit(MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer()))
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer()._transform;
		}
		this.get_gameObject().SetActive(false);
	}

	public void LateUpdate()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && currentTarget == null)
		{
			BeginSpect();
		}
	}

	private LinkedList<Player> GetPlayers()
	{
		LinkedList<Player> linkedList = new LinkedList<Player>();
		foreach (StageObject player2 in MonoBehaviourSingleton<StageObjectManager>.I.playerList)
		{
			Player player = player2 as Player;
			if (player != null)
			{
				if (player2 is Self)
				{
					linkedList.AddFirst(player);
				}
				else
				{
					linkedList.AddLast(player);
				}
			}
		}
		return linkedList;
	}

	private void OnEnable()
	{
		if (panelChange != null)
		{
			panelChange.UnLock();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (panelChange != null)
		{
			panelChange.Lock();
		}
	}

	public bool IsEnable()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return this.get_gameObject().get_activeSelf();
	}
}
