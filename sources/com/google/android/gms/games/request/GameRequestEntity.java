package com.google.android.gms.games.request;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzd;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "GameRequestEntityCreator")
@Reserved({1000})
@Deprecated
public final class GameRequestEntity extends zzd implements GameRequest {
    public static final Creator<GameRequestEntity> CREATOR = new zza();
    @Field(getter = "getData", mo13990id = 3)
    private final byte[] data;
    @Field(getter = "getStatus", mo13990id = 12)
    private final int status;
    @Field(getter = "getType", mo13990id = 7)
    private final int type;
    @Field(getter = "getGame", mo13990id = 1)
    private final GameEntity zzlp;
    @Field(getter = "getCreationTimestamp", mo13990id = 9)
    private final long zzoz;
    @Field(getter = "getSender", mo13990id = 2)
    private final PlayerEntity zzrk;
    @Field(getter = "getRequestId", mo13990id = 4)
    private final String zzrl;
    @Field(getter = "getRecipients", mo13990id = 5)
    private final ArrayList<PlayerEntity> zzrm;
    @Field(getter = "getExpirationTimestamp", mo13990id = 10)
    private final long zzrn;
    @Field(getter = "getRecipientStatusBundle", mo13990id = 11)
    private final Bundle zzro;

    @Constructor
    GameRequestEntity(@Param(mo13993id = 1) GameEntity gameEntity, @Param(mo13993id = 2) PlayerEntity playerEntity, @Param(mo13993id = 3) byte[] bArr, @Param(mo13993id = 4) String str, @Param(mo13993id = 5) ArrayList<PlayerEntity> arrayList, @Param(mo13993id = 7) int i, @Param(mo13993id = 9) long j, @Param(mo13993id = 10) long j2, @Param(mo13993id = 11) Bundle bundle, @Param(mo13993id = 12) int i2) {
        this.zzlp = gameEntity;
        this.zzrk = playerEntity;
        this.data = bArr;
        this.zzrl = str;
        this.zzrm = arrayList;
        this.type = i;
        this.zzoz = j;
        this.zzrn = j2;
        this.zzro = bundle;
        this.status = i2;
    }

    public GameRequestEntity(GameRequest gameRequest) {
        this.zzlp = new GameEntity(gameRequest.getGame());
        this.zzrk = new PlayerEntity(gameRequest.getSender());
        this.zzrl = gameRequest.getRequestId();
        this.type = gameRequest.getType();
        this.zzoz = gameRequest.getCreationTimestamp();
        this.zzrn = gameRequest.getExpirationTimestamp();
        this.status = gameRequest.getStatus();
        byte[] data2 = gameRequest.getData();
        if (data2 == null) {
            this.data = null;
        } else {
            this.data = new byte[data2.length];
            System.arraycopy(data2, 0, this.data, 0, data2.length);
        }
        List recipients = gameRequest.getRecipients();
        int size = recipients.size();
        this.zzrm = new ArrayList<>(size);
        this.zzro = new Bundle();
        for (int i = 0; i < size; i++) {
            Player player = (Player) ((Player) recipients.get(i)).freeze();
            String playerId = player.getPlayerId();
            this.zzrm.add((PlayerEntity) player);
            this.zzro.putInt(playerId, gameRequest.getRecipientStatus(playerId));
        }
    }

    static int zza(GameRequest gameRequest) {
        return (Arrays.hashCode(zzb(gameRequest)) * 31) + Objects.hashCode(gameRequest.getGame(), gameRequest.getRecipients(), gameRequest.getRequestId(), gameRequest.getSender(), Integer.valueOf(gameRequest.getType()), Long.valueOf(gameRequest.getCreationTimestamp()), Long.valueOf(gameRequest.getExpirationTimestamp()));
    }

    static boolean zza(GameRequest gameRequest, Object obj) {
        if (!(obj instanceof GameRequest)) {
            return false;
        }
        if (gameRequest == obj) {
            return true;
        }
        GameRequest gameRequest2 = (GameRequest) obj;
        return Objects.equal(gameRequest2.getGame(), gameRequest.getGame()) && Objects.equal(gameRequest2.getRecipients(), gameRequest.getRecipients()) && Objects.equal(gameRequest2.getRequestId(), gameRequest.getRequestId()) && Objects.equal(gameRequest2.getSender(), gameRequest.getSender()) && Arrays.equals(zzb(gameRequest2), zzb(gameRequest)) && Objects.equal(Integer.valueOf(gameRequest2.getType()), Integer.valueOf(gameRequest.getType())) && Objects.equal(Long.valueOf(gameRequest2.getCreationTimestamp()), Long.valueOf(gameRequest.getCreationTimestamp())) && Objects.equal(Long.valueOf(gameRequest2.getExpirationTimestamp()), Long.valueOf(gameRequest.getExpirationTimestamp()));
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
        return Objects.toStringHelper(gameRequest).add("Game", gameRequest.getGame()).add("Sender", gameRequest.getSender()).add("Recipients", gameRequest.getRecipients()).add("Data", gameRequest.getData()).add("RequestId", gameRequest.getRequestId()).add("Type", Integer.valueOf(gameRequest.getType())).add("CreationTimestamp", Long.valueOf(gameRequest.getCreationTimestamp())).add("ExpirationTimestamp", Long.valueOf(gameRequest.getExpirationTimestamp())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final long getCreationTimestamp() {
        return this.zzoz;
    }

    public final byte[] getData() {
        return this.data;
    }

    public final long getExpirationTimestamp() {
        return this.zzrn;
    }

    public final Game getGame() {
        return this.zzlp;
    }

    public final int getRecipientStatus(String str) {
        return this.zzro.getInt(str, 0);
    }

    public final List<Player> getRecipients() {
        return new ArrayList(this.zzrm);
    }

    public final String getRequestId() {
        return this.zzrl;
    }

    public final Player getSender() {
        return this.zzrk;
    }

    public final int getStatus() {
        return this.status;
    }

    public final int getType() {
        return this.type;
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
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, getGame(), i, false);
        SafeParcelWriter.writeParcelable(parcel, 2, getSender(), i, false);
        SafeParcelWriter.writeByteArray(parcel, 3, getData(), false);
        SafeParcelWriter.writeString(parcel, 4, getRequestId(), false);
        SafeParcelWriter.writeTypedList(parcel, 5, getRecipients(), false);
        SafeParcelWriter.writeInt(parcel, 7, getType());
        SafeParcelWriter.writeLong(parcel, 9, getCreationTimestamp());
        SafeParcelWriter.writeLong(parcel, 10, getExpirationTimestamp());
        SafeParcelWriter.writeBundle(parcel, 11, this.zzro, false);
        SafeParcelWriter.writeInt(parcel, 12, getStatus());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
