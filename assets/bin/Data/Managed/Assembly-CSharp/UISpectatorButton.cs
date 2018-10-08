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
		if ((Object)currentTarget == (Object)null)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void Initialize(UIStaticPanelChanger panelChange)
	{
		this.panelChange = panelChange;
	}

	public void BeginSpect()
	{
		Player value = GetPlayers().First.Value;
		SetTarget(value);
		base.gameObject.SetActive(true);
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
		if ((Object)MonoBehaviourSingleton<CoopManager>.I.coopMyClient != (Object)null && (bool)MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer()._transform;
		}
		base.gameObject.SetActive(false);
	}

	public void LateUpdate()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && (Object)currentTarget == (Object)null)
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
			if ((Object)player != (Object)null)
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
		if ((Object)panelChange != (Object)null)
		{
			panelChange.UnLock();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if ((Object)panelChange != (Object)null)
		{
			panelChange.Lock();
		}
	}

	public bool IsEnable()
	{
		return base.gameObject.activeSelf;
	}
}
