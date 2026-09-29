// Decompiled with JetBrains decompiler
// Type: ChatStampListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ChatStampListItem : MonoBehaviour
{
  [SerializeField]
  private BoxCollider m_Colider;
  [SerializeField]
  private UITexture m_Texture;
  public System.Action onButton;
  private bool isDummy;
  private IEnumerator m_CoroutineLoadStamp;

  public int StampId { get; protected set; }

  public bool IsReady { get; protected set; }

  private void Awake()
  {
    UIButton component = ((Component) this.m_Texture).GetComponent<UIButton>();
    component.CacheDefaultColor();
    component.tweenTarget = (GameObject) null;
  }

  public void Init(int _stampId)
  {
    this.StampId = _stampId;
    this.IsReady = false;
    this.isDummy = false;
    this.SetActiveComponents(false);
    this.RequestLoadStamp();
  }

  public void SetAsDummy()
  {
    this.isDummy = true;
    this.SetActiveComponents(false);
  }

  public void SetActiveComponents(bool isActive)
  {
    if (this.isDummy & isActive)
      return;
    ((Collider) this.m_Colider).enabled = isActive;
    this.m_Texture.alpha = isActive ? 1f : 0.0f;
  }

  public void OnButten()
  {
    if (!this.IsReady || this.onButton == null)
      return;
    this.onButton();
  }

  private void RequestLoadStamp()
  {
    this.CancelLoadStamp();
    this.m_CoroutineLoadStamp = this.CoroutineLoadStamp();
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this._Update());
  }

  private IEnumerator CoroutineLoadStamp()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_stamp = load_queue.LoadChatStamp(this.StampId);
    while (load_queue.IsLoading())
      yield return (object) null;
    if (Object.op_Inequality(lo_stamp.loadedObject, (Object) null))
    {
      this.m_Texture.mainTexture = (Texture) (lo_stamp.loadedObject as Texture2D);
      this.SetActiveComponents(true);
      this.IsReady = true;
    }
    this.m_CoroutineLoadStamp = (IEnumerator) null;
  }

  private void CancelLoadStamp() => this.m_CoroutineLoadStamp = (IEnumerator) null;

  private IEnumerator _Update()
  {
    while (this.m_CoroutineLoadStamp != null && this.m_CoroutineLoadStamp.MoveNext())
      yield return (object) null;
  }

  private void OnEnable()
  {
    if (this.m_CoroutineLoadStamp == null)
      return;
    this.RequestLoadStamp();
  }
}
