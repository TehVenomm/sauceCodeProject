package com.google.android.gms.nearby.messages;

import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.messages.internal.zzc;
import com.google.android.gms.nearby.messages.internal.zzg;
import java.util.Arrays;

public class EddystoneUid {
    public static final int INSTANCE_LENGTH = 6;
    public static final int LENGTH = 16;
    public static final int NAMESPACE_LENGTH = 10;
    private final zzg zzjdm;

    public EddystoneUid(String str) {
        this(zzc.zzkk(str));
    }

    public EddystoneUid(String str, String str2) {
        this.zzjdm = new zzg(str, str2);
    }

    private EddystoneUid(byte[] bArr) {
        zzbp.zzb(bArr.length == 16, (Object) "Bytes must be a namespace plus instance (16 bytes).");
        this.zzjdm = new zzg(bArr);
    }

    public static EddystoneUid from(Message message) {
        boolean zzkj = message.zzkj(Message.MESSAGE_TYPE_EDDYSTONE_UID);
        String type = message.getType();
        zzbp.zzb(zzkj, new StringBuilder(String.valueOf(type).length() + 58).append("Message type '").append(type).append("' is not Message.MESSAGE_TYPE_EDDYSTONE_UID.").toString());
        return new EddystoneUid(message.getContent());
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof EddystoneUid)) {
            return false;
        }
        return zzbf.equal(this.zzjdm, ((EddystoneUid) obj).zzjdm);
    }

    public String getHex() {
        return this.zzjdm.getHex();
    }

    public String getInstance() {
        byte[] bytes = this.zzjdm.getBytes();
        return bytes.length < 16 ? null : zzc.zzr(Arrays.copyOfRange(bytes, 10, 16));
    }

    public String getNamespace() {
        return zzc.zzr(Arrays.copyOfRange(this.zzjdm.getBytes(), 0, 10));
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjdm});
    }

    public String toString() {
        String hex = getHex();
        return new StringBuilder(String.valueOf(hex).length() + 17).append("EddystoneUid{id=").append(hex).append("}").toString();
    }
}
