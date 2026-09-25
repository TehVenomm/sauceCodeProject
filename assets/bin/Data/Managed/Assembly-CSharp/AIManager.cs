// Decompiled with JetBrains decompiler
// Type: AIManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AIManager : MonoBehaviourSingleton<AIManager>
{
  private MetaAI metaAI = new MetaAI();
  private SpanTimer metaAISpanTimer = new SpanTimer(1f);

  protected override void Awake()
  {
    base.Awake();
    Singleton<GoalPool>.Create();
    Singleton<BrainBlackboard>.Create();
  }

  private void Update()
  {
    if (!this.metaAISpanTimer.IsReady())
      return;
    this.metaAI.Update();
  }

  private void OnDestroy()
  {
    if (Singleton<GoalPool>.IsValid())
      Singleton<GoalPool>.I.Clear();
    if (!Singleton<BrainBlackboard>.IsValid())
      return;
    Singleton<BrainBlackboard>.I.Clear();
  }
}
