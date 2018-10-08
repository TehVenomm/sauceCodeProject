package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzclk;
import com.google.android.gms.internal.zzclo;
import com.google.android.gms.nearby.messages.internal.zzad;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.UUID;

public class MessageFilter extends zza {
    public static final Creator<MessageFilter> CREATOR = new zzc();
    public static final MessageFilter INCLUDE_ALL_MY_TYPES = new Builder().includeAllMyTypes().build();
    private int zzdxt;
    private final List<zzad> zzjds;
    private final List<zzclo> zzjdt;
    private final boolean zzjdu;
    private final List<zzclk> zzjdv;
    private final int zzjdw;

    public static final class Builder {
        private final List<zzclo> zzjdt = new ArrayList();
        private boolean zzjdu;
        private int zzjdw = 0;
        private final Set<zzad> zzjdx = new HashSet();
        private final Set<zzclk> zzjdy = new HashSet();

        private final Builder zzav(String str, String str2) {
            this.zzjdx.add(new zzad(str, str2));
            return this;
        }

        public final MessageFilter build() {
            boolean z = this.zzjdu || !this.zzjdx.isEmpty();
            zzbp.zza(z, (Object) "At least one of the include methods must be called.");
            return new MessageFilter(new ArrayList(this.zzjdx), this.zzjdt, this.zzjdu, new ArrayList(this.zzjdy), this.zzjdw);
        }

        public final Builder includeAllMyTypes() {
            this.zzjdu = true;
            return this;
        }

        public final Builder includeAudioBytes(int i) {
            boolean z = true;
            zzbp.zzb(this.zzjdw == 0, (Object) "includeAudioBytes() can only be called once per MessageFilter instance.");
            zzbp.zzb(i > 0, "Invalid value for numAudioBytes: " + i);
            if (i > 10) {
                z = false;
            }
            zzbp.zzb(z, (Object) "numAudioBytes is capped by AudioBytes.MAX_SIZE = 10");
            zzav(Message.MESSAGE_NAMESPACE_RESERVED, Message.MESSAGE_TYPE_AUDIO_BYTES);
            this.zzjdw = i;
            return this;
        }

        public final Builder includeEddystoneUids(String str, @Nullable String str2) {
            zzav(Message.MESSAGE_NAMESPACE_RESERVED, Message.MESSAGE_TYPE_EDDYSTONE_UID);
            this.zzjdt.add(zzclo.zzaw(str, str2));
            return this;
        }

        public final Builder includeFilter(MessageFilter messageFilter) {
            this.zzjdx.addAll(messageFilter.zzbau());
            this.zzjdt.addAll(messageFilter.zzbaw());
            this.zzjdy.addAll(messageFilter.zzbax());
            this.zzjdu |= messageFilter.zzbav();
            return this;
        }

        public final Builder includeIBeaconIds(UUID uuid, @Nullable Short sh, @Nullable Short sh2) {
            zzav(Message.MESSAGE_NAMESPACE_RESERVED, Message.MESSAGE_TYPE_I_BEACON_ID);
            this.zzjdt.add(zzclo.zza(uuid, sh, sh2));
            return this;
        }

        public final Builder includeNamespacedType(String str, String str2) {
            boolean z = (str == null || str.isEmpty() || str.contains("*")) ? false : true;
            zzbp.zzb(z, "namespace(%s) cannot be null, empty or contain (*).", str);
            z = (str2 == null || str2.contains("*")) ? false : true;
            zzbp.zzb(z, "type(%s) cannot be null or contain (*).", str2);
            return zzav(str, str2);
        }
    }

    MessageFilter(int i, List<zzad> list, List<zzclo> list2, boolean z, List<zzclk> list3, int i2) {
        List emptyList;
        List emptyList2;
        this.zzdxt = i;
        this.zzjds = Collections.unmodifiableList((List) zzbp.zzu(list));
        this.zzjdu = z;
        if (list2 == null) {
            emptyList = Collections.emptyList();
        }
        this.zzjdt = Collections.unmodifiableList(emptyList);
        if (list3 == null) {
            emptyList2 = Collections.emptyList();
        }
        this.zzjdv = Collections.unmodifiableList(emptyList2);
        this.zzjdw = i2;
    }

    private MessageFilter(List<zzad> list, List<zzclo> list2, boolean z, List<zzclk> list3, int i) {
        this(2, (List) list, (List) list2, z, (List) list3, i);
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof MessageFilter)) {
                return false;
            }
            MessageFilter messageFilter = (MessageFilter) obj;
            if (this.zzjdu != messageFilter.zzjdu || !zzbf.equal(this.zzjds, messageFilter.zzjds) || !zzbf.equal(this.zzjdt, messageFilter.zzjdt)) {
                return false;
            }
            if (!zzbf.equal(this.zzjdv, messageFilter.zzjdv)) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjds, this.zzjdt, Boolean.valueOf(this.zzjdu), this.zzjdv});
    }

    public String toString() {
        boolean z = this.zzjdu;
        String valueOf = String.valueOf(this.zzjds);
        return new StringBuilder(String.valueOf(valueOf).length() + 53).append("MessageFilter{includeAllMyTypes=").append(z).append(", messageTypes=").append(valueOf).append("}").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzjds, false);
        zzd.zzc(parcel, 2, this.zzjdt, false);
        zzd.zza(parcel, 3, this.zzjdu);
        zzd.zzc(parcel, 4, this.zzjdv, false);
        zzd.zzc(parcel, 5, this.zzjdw);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }

    public final List<zzad> zzbau() {
        return this.zzjds;
    }

    public final boolean zzbav() {
        return this.zzjdu;
    }

    final List<zzclo> zzbaw() {
        return this.zzjdt;
    }

    public final List<zzclk> zzbax() {
        return this.zzjdv;
    }
}
