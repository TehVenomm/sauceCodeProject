// Decompiled with JetBrains decompiler
// Type: GoalPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GoalPool : Singleton<GoalPool>
{
  private Pool<Goal> pool = new Pool<Goal>();

  public T Alloc<T>() where T : Goal, new() => this.pool.Alloc<T>();

  public void Free(Goal goal) => this.pool.Free(goal);

  public void Clear() => this.pool.Clear();
}
