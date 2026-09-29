// Decompiled with JetBrains decompiler
// Type: StateMachine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class StateMachine
{
  private Brain brain;
  private State current;
  public SpanTimer processSpan = new SpanTimer(0.1f);
  private StateMachine _subFsm;

  public STATE_TYPE currentType { get; private set; }

  public StateMachine(Brain brain)
  {
    this.brain = brain;
    this.currentType = STATE_TYPE.NONE;
  }

  public StateMachine subFsm
  {
    get
    {
      if (this._subFsm == null)
        this._subFsm = new StateMachine(this.brain);
      return this._subFsm;
    }
  }

  public STATE_TYPE GetFixStateType()
  {
    return this._subFsm != null ? this._subFsm.GetFixStateType() : this.currentType;
  }

  public void SetCurrentState(STATE_TYPE type)
  {
    this.current = StateType.GetState(type);
    this.currentType = type;
  }

  public void ChangeState(STATE_TYPE type)
  {
    if (this.current != null)
      this.current.Exit(this, this.brain);
    this.SetCurrentState(type);
    if (this.current == null)
      return;
    this.current.Enter(this, this.brain);
  }

  public void Update()
  {
    if (this.current != null && this.processSpan.IsReady())
      this.current.Process(this, this.brain);
    if (this._subFsm == null)
      return;
    this._subFsm.Update();
  }

  public void HandleEvent(BRAIN_EVENT ev, object param = null)
  {
    if (this.current != null)
      this.current.HandleEvent(this, this.brain, ev, param);
    if (this._subFsm == null)
      return;
    this._subFsm.HandleEvent(ev, param);
  }

  public override string ToString()
  {
    string str = string.Concat((object) this.currentType);
    if (this._subFsm != null)
      str = $"{str} > {this._subFsm.ToString()}";
    return str;
  }
}
