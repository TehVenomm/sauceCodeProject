package com.google.android.gms.games.request;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

@Deprecated
public final class GameRequestEntity extends zzc implements GameRequest {
    public static final Creator<GameRequestEntity> CREATOR = new zza();
    private final long mCreationTimestamp;
    private final int zzbyx;
    private final String zzcjq;
    private final int zzeda;
    private final GameEntity zzhiw;
    private final byte[] zzhnh;
    private final PlayerEntity zzhob;
    private final ArrayList<PlayerEntity> zzhoc;
    private final long zzhod;
    private final Bundle zzhoe;

    GameRequestEntity(GameEntity gameEntity, PlayerEntity playerEntity, byte[] bArr, String str, ArrayList<PlayerEntity> arrayList, int i, long j, long j2, Bundle bundle, int i2) {
        this.zzhiw = gameEntity;
        this.zzhob = playerEntity;
        this.zzhnh = bArr;
        this.zzcjq = str;
        this.zzhoc = arrayList;
        this.zzeda = i;
        this.mCreationTimestamp = j;
        this.zzhod = j2;
        this.zzhoe = bundle;
        this.zzbyx = i2;
    }

    public GameRequestEntity(GameRequest gameRequest) {
        this.zzhiw = new GameEntity(gameRequest.getGame());
        this.zzhob = new PlayerEntity(gameRequest.getSender());
        this.zzcjq = gameRequest.getRequestId();
        this.zzeda = gameRequest.getType();
        this.mCreationTimestamp = gameRequest.getCreationTimestamp();
        this.zzhod = gameRequest.getExpirationTimestamp();
        this.zzbyx = gameRequest.getStatus();
        Object data = gameRequest.getData();
        if (data == null) {
            this.zzhnh = null;
        } else {
            this.zzhnh = new byte[data.length];
            System.arraycopy(data, 0, this.zzhnh, 0, data.length);
        }
        List recipients = gameRequest.getRecipients();
        int size = recipients.size();
        this.zzhoc = new ArrayList(size);
        this.zzhoe = new Bundle();
        for (int i = 0; i < size; i++) {
            Player player = (Player) ((Player) recipients.get(i)).freeze();
            String playerId = player.getPlayerId();
            this.zzhoc.add((PlayerEntity) player);
            this.zzhoe.putInt(playerId, gameRequest.getRecipientStatus(playerId));
        }
    }

    static int zza(GameRequest gameRequest) {
        return Arrays.hashCode(new Object[]{gameRequest.getGame(), gameRequest.getRecipients(), gameRequest.getRequestId(), gameRequest.getSender(), zzb(gameRequest), Integer.valueOf(gameRequest.getType()), Long.valueOf(gameRequest.getCreationTimestamp()), Long.valueOf(gameRequest.getExpirationTimestamp())});
    }

    static boolean zza(GameRequest gameRequest, Object obj) {
        if (!(obj instanceof GameRequest)) {
            return false;
        }
        if (gameRequest == obj) {
            return true;
        }
        GameRequest gameRequest2 = (GameRequest) obj;
        return zzbf.equal(gameRequest2.getGame(), gameRequest.getGame()) && zzbf.equal(gameRequest2.getRecipients(), gameRequest.getRecipients()) && zzbf.equal(gameRequest2.getRequestId(), gameRequest.getRequestId()) && zzbf.equal(gameRequest2.getSender(), gameRequest.getSender()) && Arrays.equals(zzb(gameRequest2), zzb(gameRequest)) && zzbf.equal(Integer.valueOf(gameRequest2.getType()), Integer.valueOf(gameRequest.getType())) && zzbf.equal(Long.valueOf(gameRequest2.getCreationTimestamp()), Long.valueOf(gameRequest.getCreationTimestamp())) && zzbf.equal(Long.valueOf(gameRequest2.getExpirationTimestamp()), Long.valueOf(gameRequest.getExpirationTimestamp()));
    }

    private static int[] zzb(GameRequest gameRequest) {
        List recipients = gameRequest.getRecipients();
        int size = recipients.size();
        int[] iArr = new int[size];
        for (int i = 0; i < size; i++) {
            iArr[i] = gameRequest.getRecipientStatus(((Player) recipients.get(i)).getPlayerId());
        }
        return iArr;
    }

    static String zzc(GameRequest gameRequest) {
        return zzbf.zzt(gameRequest).zzg("Game", gameRequest.getGame()).zzg("Sender", gameRequest.getSender()).zzg("Recipients", gameRequest.getRecipients()).zzg("Data", gameRequest.getData()).zzg("RequestId", gameRequest.getRequestId()).zzg("Type", Integer.valueOf(gameRequest.getType())).zzg("CreationTimestamp", Long.valueOf(gameRequest.getCreationTimestamp())).zzg("ExpirationTimestamp", Long.valueOf(gameRequest.getExpirationTimestamp())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final GameRequest freeze() {
        return this;
    }

    public final long getCreationTimestamp() {
        return this.mCreationTimestamp;
    }

    public final byte[] getData() {
        return this.zzhnh;
    }

    public final long getExpirationTimestamp() {
        return this.zzhod;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final int getRecipientStatus(String str) {
        return this.zzhoe.getInt(str, 0);
    }

    public final List<Player> getRecipients() {
        return new ArrayList(this.zzhoc);
    }

    public final String getRequestId() {
        return this.zzcjq;
    }

    public final Player getSender() {
        return this.zzhob;
    }

    public final int getStatus() {
        return this.zzbyx;
    }

    public final int getType() {
        return this.zzeda;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isConsumed(String str) {
        return getRecipientStatus(str) == 1;
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzc(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getGame(), i, false);
        zzd.zza(parcel, 2, getSender(), i, false);
        zzd.zza(parcel, 3, getData(), false);
        zzd.zza(parcel, 4, getRequestId(), false);
        zzd.zzc(parcel, 5, getRecipients(), false);
        zzd.zzc(parcel, 7, getType());
        zzd.zza(parcel, 9, getCreationTimestamp());
        zzd.zza(parcel, 10, getExpirationTimestamp());
        zzd.zza(parcel, 11, this.zzhoe, false);
        zzd.zzc(parcel, 12, getStatus());
        zzd.zzai(parcel, zze);
    }
}
