package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzclm;
import java.util.Arrays;

public class Message extends zza implements ReflectedParcelable {
    public static final Creator<Message> CREATOR = new zza();
    public static final int MAX_CONTENT_SIZE_BYTES = 102400;
    public static final int MAX_TYPE_LENGTH = 32;
    public static final String MESSAGE_NAMESPACE_RESERVED = "__reserved_namespace";
    public static final String MESSAGE_TYPE_AUDIO_BYTES = "__audio_bytes";
    public static final String MESSAGE_TYPE_EDDYSTONE_UID = "__eddystone_uid";
    public static final String MESSAGE_TYPE_I_BEACON_ID = "__i_beacon_id";
    private static final zzclm[] zzjdo = new zzclm[]{zzclm.zzjfh};
    private final byte[] content;
    private final String type;
    private int versionCode;
    private final String zzjdp;
    @Deprecated
    private zzclm[] zzjdq;
    private final long zzjdr;

    Message(int i, @Nullable byte[] bArr, @Nullable String str, String str2, @Nullable zzclm[] zzclmArr, long j) {
        this.versionCode = i;
        this.type = (String) zzbp.zzu(str2);
        if (str == null) {
            str = "";
        }
        this.zzjdp = str;
        this.zzjdr = 0;
        zzbp.zzu(bArr);
        zzbp.zzb(bArr.length <= MAX_CONTENT_SIZE_BYTES, "Content length(%d) must not exceed MAX_CONTENT_SIZE_BYTES(%d)", Integer.valueOf(bArr.length), Integer.valueOf(MAX_CONTENT_SIZE_BYTES));
        this.content = bArr;
        if (zzclmArr == null || zzclmArr.length == 0) {
            zzclmArr = zzjdo;
        }
        this.zzjdq = zzclmArr;
        zzbp.zzb(str2.length() <= 32, "Type length(%d) must not exceed MAX_TYPE_LENGTH(%d)", Integer.valueOf(str2.length()), Integer.valueOf(32));
    }

    public Message(byte[] bArr) {
        this(bArr, "", "");
    }

    public Message(byte[] bArr, String str) {
        this(bArr, "", str);
    }

    public Message(byte[] bArr, String str, String str2) {
        this(bArr, str, str2, zzjdo);
    }

    private Message(byte[] bArr, String str, String str2, zzclm[] zzclmArr) {
        this(bArr, str, str2, zzclmArr, 0);
    }

    private Message(byte[] bArr, String str, String str2, zzclm[] zzclmArr, long j) {
        this(2, bArr, str, str2, zzclmArr, 0);
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Message)) {
                return false;
            }
            Message message = (Message) obj;
            if (!TextUtils.equals(this.zzjdp, message.zzjdp) || !TextUtils.equals(this.type, message.type) || !Arrays.equals(this.content, message.content)) {
                return false;
            }
            if (0 != 0) {
                return false;
            }
        }
        return true;
    }

    public byte[] getContent() {
        return this.content;
    }

    public String getNamespace() {
        return this.zzjdp;
    }

    public String getType() {
        return this.type;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjdp, this.type, Integer.valueOf(Arrays.hashCode(this.content)), Long.valueOf(0)});
    }

    public String toString() {
        String str = this.zzjdp;
        String str2 = this.type;
        return new StringBuilder((String.valueOf(str).length() + 59) + String.valueOf(str2).length()).append("Message{namespace='").append(str).append("', type='").append(str2).append("', content=[").append(this.content == null ? 0 : this.content.length).append(" bytes]}").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getContent(), false);
        zzd.zza(parcel, 2, getType(), false);
        zzd.zza(parcel, 3, getNamespace(), false);
        zzd.zza(parcel, 4, this.zzjdq, i, false);
        zzd.zza(parcel, 5, 0);
        zzd.zzc(parcel, 1000, this.versionCode);
        zzd.zzai(parcel, zze);
    }

    public final boolean zzkj(String str) {
        return MESSAGE_NAMESPACE_RESERVED.equals(getNamespace()) && str.equals(getType());
    }
}
