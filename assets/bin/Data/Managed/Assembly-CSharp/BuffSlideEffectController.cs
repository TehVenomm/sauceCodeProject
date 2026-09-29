// Decompiled with JetBrains decompiler
// Type: BuffSlideEffectController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BuffSlideEffectController : MonoBehaviour
{
  public const float END_STATE_PLAY_HEIGHT = 0.8f;
  private Player m_targetPlayer;
  private Transform m_footNode;
  private Animator m_animator;
  private int m_animHashLoop;
  private int m_animHashEnd;

  public void Initialize(Player targetPlayer)
  {
    this.m_targetPlayer = targetPlayer;
    this.m_footNode = this.m_targetPlayer.FindNode("L_Foot");
    this.m_animator = ((Component) this).GetComponent<Animator>();
    this.m_animHashLoop = Animator.StringToHash("LOOP");
    this.m_animHashEnd = Animator.StringToHash("END");
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_animator, (Object) null) || Object.op_Equality((Object) this.m_targetPlayer, (Object) null))
      return;
    AnimatorStateInfo animatorStateInfo = this.m_animator.GetCurrentAnimatorStateInfo(0);
    int shortNameHash = ((AnimatorStateInfo) ref animatorStateInfo).shortNameHash;
    double y = (double) this.m_footNode.position.y;
    float height = StageManager.GetHeight(this.m_targetPlayer._position);
    int num1 = this.m_animHashLoop;
    double num2 = (double) height;
    if ((double) Mathf.Abs((float) (y - num2)) > 0.800000011920929)
      num1 = this.m_animHashEnd;
    if (num1 == shortNameHash)
      return;
    this.m_animator.Play(num1, 0, 0.0f);
  }
}
