package com.google.android.gms.nearby.messages;

import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.messages.internal.zzl;
import java.util.Arrays;
import java.util.UUID;

public class IBeaconId {
    public static final int LENGTH = 20;
    private final zzl zzjdn;

    public IBeaconId(UUID uuid, short s, short s2) {
        this.zzjdn = new zzl(uuid, Short.valueOf(s), Short.valueOf(s2));
    }

    private IBeaconId(byte[] bArr) {
        zzbp.zzb(bArr.length == 20, (Object) "iBeacon ID must be a UUID, a major, and a minor (20 total bytes).");
        this.zzjdn = new zzl(bArr);
    }

    public static IBeaconId from(Message message) {
        boolean zzkj = message.zzkj(Message.MESSAGE_TYPE_I_BEACON_ID);
        String type = message.getType();
        zzbp.zzb(zzkj, new StringBuilder(String.valueOf(type).length() + 55).append("Message type '").append(type).append("' is not Message.MESSAGE_TYPE_I_BEACON_ID").toString());
        return new IBeaconId(message.getContent());
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof IBeaconId)) {
            return false;
        }
        return zzbf.equal(this.zzjdn, ((IBeaconId) obj).zzjdn);
    }

    public short getMajor() {
        return this.zzjdn.zzbaz().shortValue();
    }

    public short getMinor() {
        return this.zzjdn.zzbba().shortValue();
    }

    public UUID getProximityUuid() {
        return this.zzjdn.getProximityUuid();
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjdn});
    }

    public String toString() {
        String valueOf = String.valueOf(getProximityUuid());
        short major = getMajor();
        return new StringBuilder(String.valueOf(valueOf).length() + 53).append("IBeaconId{proximityUuid=").append(valueOf).append(", major=").append(major).append(", minor=").append(getMinor()).append("}").toString();
    }
}
