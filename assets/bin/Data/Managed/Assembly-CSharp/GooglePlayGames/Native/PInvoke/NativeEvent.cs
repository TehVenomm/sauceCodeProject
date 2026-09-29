// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeEvent : BaseReferenceHolder, IEvent
{
  internal NativeEvent(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  public string Id
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Id(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string Name
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Name(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string Description
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_Description(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string ImageUrl
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Event.Event_ImageUrl(this.SelfPtr(), out_string, out_size)));
    }
  }

  public ulong CurrentCount => Event.Event_Count(this.SelfPtr());

  public GooglePlayGames.BasicApi.Events.EventVisibility Visibility
  {
    get
    {
      Types.EventVisibility eventVisibility = Event.Event_Visibility(this.SelfPtr());
      switch (eventVisibility)
      {
        case Types.EventVisibility.HIDDEN:
          return GooglePlayGames.BasicApi.Events.EventVisibility.Hidden;
        case Types.EventVisibility.REVEALED:
          return GooglePlayGames.BasicApi.Events.EventVisibility.Revealed;
        default:
          throw new InvalidOperationException("Unknown visibility: " + (object) eventVisibility);
      }
    }
  }

  protected override void CallDispose(HandleRef selfPointer) => Event.Event_Dispose(selfPointer);

  public override string ToString()
  {
    if (this.IsDisposed())
      return "[NativeEvent: DELETED]";
    return $"[NativeEvent: Id={this.Id}, Name={this.Name}, Description={this.Description}, ImageUrl={this.ImageUrl}, CurrentCount={this.CurrentCount}, Visibility={this.Visibility}]";
  }
}
