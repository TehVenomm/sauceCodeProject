// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi.Multiplayer;

public interface RealTimeMultiplayerListener
{
  void OnRoomSetupProgress(float percent);

  void OnRoomConnected(bool success);

  void OnLeftRoom();

  void OnParticipantLeft(Participant participant);

  void OnPeersConnected(string[] participantIds);

  void OnPeersDisconnected(string[] participantIds);

  void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);
}
