// Decompiled with JetBrains decompiler
// Type: UISpectatorButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
    if (!Object.op_Equality((Object) this.currentTarget, (Object) null))
      return;
    ((Component) this).gameObject.SetActive(false);
  }

  public void Initialize(UIStaticPanelChanger panelChange) => this.panelChange = panelChange;

  public void BeginSpect()
  {
    this.SetTarget(this.GetPlayers().First.Value);
    ((Component) this).gameObject.SetActive(true);
  }

  public void OnPrev()
  {
    LinkedList<Player> players = this.GetPlayers();
    LinkedListNode<Player> linkedListNode = players.Find(this.currentTarget);
    if (linkedListNode == null)
      this.BeginSpect();
    else
      this.SetTarget(linkedListNode.Previous != null ? linkedListNode.Previous.Value : players.Last.Value);
  }

  public void OnNext()
  {
    LinkedList<Player> players = this.GetPlayers();
    LinkedListNode<Player> linkedListNode = players.Find(this.currentTarget);
    if (linkedListNode == null)
      this.BeginSpect();
    else
      this.SetTarget(linkedListNode.Next != null ? linkedListNode.Next.Value : players.First.Value);
  }

  private void SetTarget(Player target)
  {
    this.currentTarget = target;
    this.viewingPlayerLabel.text = this.currentTarget.charaName;
    MonoBehaviourSingleton<InGameCameraManager>.I.target = this.currentTarget._transform;
  }

  public void EndSpect()
  {
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<CoopManager>.I.coopMyClient, (Object) null) && Object.op_Implicit((Object) MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer()))
      MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.GetPlayer()._transform;
    ((Component) this).gameObject.SetActive(false);
  }

  public void LateUpdate()
  {
    if (MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop || !Object.op_Equality((Object) this.currentTarget, (Object) null))
      return;
    this.BeginSpect();
  }

  private LinkedList<Player> GetPlayers()
  {
    LinkedList<Player> players = new LinkedList<Player>();
    foreach (StageObject player1 in MonoBehaviourSingleton<StageObjectManager>.I.playerList)
    {
      Player player2 = player1 as Player;
      if (Object.op_Inequality((Object) player2, (Object) null))
      {
        if (player1 is Self)
          players.AddFirst(player2);
        else
          players.AddLast(player2);
      }
    }
    return players;
  }

  private void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.panelChange, (Object) null))
      return;
    this.panelChange.UnLock();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!Object.op_Inequality((Object) this.panelChange, (Object) null))
      return;
    this.panelChange.Lock();
  }

  public bool IsEnable() => ((Component) this).gameObject.activeSelf;
}
