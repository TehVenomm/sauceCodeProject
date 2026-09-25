// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeQuestMilestone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Quests;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeQuestMilestone : BaseReferenceHolder, IQuestMilestone
{
  internal NativeQuestMilestone(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  public string Id
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_Id(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string EventId
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_EventId(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string QuestId
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => QuestMilestone.QuestMilestone_QuestId(this.SelfPtr(), out_string, out_size)));
    }
  }

  public ulong CurrentCount => QuestMilestone.QuestMilestone_CurrentCount(this.SelfPtr());

  public ulong TargetCount => QuestMilestone.QuestMilestone_TargetCount(this.SelfPtr());

  public byte[] CompletionRewardData
  {
    get
    {
      return PInvokeUtilities.OutParamsToArray<byte>((PInvokeUtilities.OutMethod<byte>) ((out_bytes, out_size) => QuestMilestone.QuestMilestone_CompletionRewardData(this.SelfPtr(), out_bytes, out_size)));
    }
  }

  public MilestoneState State
  {
    get
    {
      Types.QuestMilestoneState questMilestoneState = QuestMilestone.QuestMilestone_State(this.SelfPtr());
      switch (questMilestoneState)
      {
        case Types.QuestMilestoneState.NOT_STARTED:
          return MilestoneState.NotStarted;
        case Types.QuestMilestoneState.NOT_COMPLETED:
          return MilestoneState.NotCompleted;
        case Types.QuestMilestoneState.COMPLETED_NOT_CLAIMED:
          return MilestoneState.CompletedNotClaimed;
        case Types.QuestMilestoneState.CLAIMED:
          return MilestoneState.Claimed;
        default:
          throw new InvalidOperationException("Unknown state: " + (object) questMilestoneState);
      }
    }
  }

  internal bool Valid() => QuestMilestone.QuestMilestone_Valid(this.SelfPtr());

  protected override void CallDispose(HandleRef selfPointer)
  {
    QuestMilestone.QuestMilestone_Dispose(selfPointer);
  }

  public override string ToString()
  {
    return $"[NativeQuestMilestone: Id={this.Id}, EventId={this.EventId}, QuestId={this.QuestId}, CurrentCount={this.CurrentCount}, TargetCount={this.TargetCount}, State={this.State}]";
  }

  internal static NativeQuestMilestone FromPointer(IntPtr pointer)
  {
    return pointer == IntPtr.Zero ? (NativeQuestMilestone) null : new NativeQuestMilestone(pointer);
  }
}
