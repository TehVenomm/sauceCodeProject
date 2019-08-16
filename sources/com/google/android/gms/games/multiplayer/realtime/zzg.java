package com.google.android.gms.games.multiplayer.realtime;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import java.util.List;

public final class zzg implements zzh {
    private final RoomUpdateCallback zzpu;
    private final RoomStatusUpdateCallback zzpv;
    private final OnRealTimeMessageReceivedListener zzqg;

    public zzg(@NonNull RoomUpdateCallback roomUpdateCallback, @Nullable RoomStatusUpdateCallback roomStatusUpdateCallback, @Nullable OnRealTimeMessageReceivedListener onRealTimeMessageReceivedListener) {
        this.zzpu = roomUpdateCallback;
        this.zzpv = roomStatusUpdateCallback;
        this.zzqg = onRealTimeMessageReceivedListener;
    }

    public final void onConnectedToRoom(@Nullable Room room) {
        if (this.zzpv != null) {
            this.zzpv.onConnectedToRoom(room);
        }
    }

    public final void onDisconnectedFromRoom(@Nullable Room room) {
        if (this.zzpv != null) {
            this.zzpv.onDisconnectedFromRoom(room);
        }
    }

    public final void onJoinedRoom(int i, @Nullable Room room) {
        if (this.zzpu != null) {
            this.zzpu.onJoinedRoom(i, room);
        }
    }

    public final void onLeftRoom(int i, @NonNull String str) {
        if (this.zzpu != null) {
            this.zzpu.onLeftRoom(i, str);
        }
    }

    public final void onP2PConnected(@NonNull String str) {
        if (this.zzpv != null) {
            this.zzpv.onP2PConnected(str);
        }
    }

    public final void onP2PDisconnected(@NonNull String str) {
        if (this.zzpv != null) {
            this.zzpv.onP2PDisconnected(str);
        }
    }

    public final void onPeerDeclined(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeerDeclined(room, list);
        }
    }

    public final void onPeerInvitedToRoom(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeerInvitedToRoom(room, list);
        }
    }

    public final void onPeerJoined(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeerJoined(room, list);
        }
    }

    public final void onPeerLeft(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeerLeft(room, list);
        }
    }

    public final void onPeersConnected(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeersConnected(room, list);
        }
    }

    public final void onPeersDisconnected(@Nullable Room room, @NonNull List<String> list) {
        if (this.zzpv != null) {
            this.zzpv.onPeersDisconnected(room, list);
        }
    }

    public final void onRealTimeMessageReceived(@NonNull RealTimeMessage realTimeMessage) {
        if (this.zzqg != null) {
            this.zzqg.onRealTimeMessageReceived(realTimeMessage);
        }
    }

    public final void onRoomAutoMatching(@Nullable Room room) {
        if (this.zzpv != null) {
            this.zzpv.onRoomAutoMatching(room);
        }
    }

    public final void onRoomConnected(int i, @Nullable Room room) {
        if (this.zzpu != null) {
            this.zzpu.onRoomConnected(i, room);
        }
    }

    public final void onRoomConnecting(@Nullable Room room) {
        if (this.zzpv != null) {
            this.zzpv.onRoomConnecting(room);
        }
    }

    public final void onRoomCreated(int i, @Nullable Room room) {
        if (this.zzpu != null) {
            this.zzpu.onRoomCreated(i, room);
        }
    }
}
