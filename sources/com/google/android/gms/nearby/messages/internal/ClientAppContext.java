package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;

public final class ClientAppContext extends zza implements ReflectedParcelable {
    public static final Creator<ClientAppContext> CREATOR = new zzd();
    private int versionCode;
    private boolean zzjea;
    private String zzjec;
    private String zzjfr;
    @Nullable
    private String zzjfs;
    private int zzjft;

    ClientAppContext(int i, String str, @Nullable String str2, boolean z, int i2, @Nullable String str3) {
        this.versionCode = i;
        this.zzjfr = (String) zzbp.zzu(str);
        if (!(str2 == null || str2.isEmpty() || str2.startsWith("0p:"))) {
            Log.w("NearbyMessages", String.format("ClientAppContext: 0P identifier(%s) without 0P prefix(%s)", new Object[]{str2, "0p:"}));
            String valueOf = String.valueOf("0p:");
            String valueOf2 = String.valueOf(str2);
            str2 = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
        }
        this.zzjfs = str2;
        this.zzjea = z;
        this.zzjft = i2;
        this.zzjec = str3;
    }

    public ClientAppContext(String str, @Nullable String str2, boolean z, @Nullable String str3, int i) {
        this(1, str, str2, z, i, str3);
    }

    @Nullable
    static final ClientAppContext zza(@Nullable ClientAppContext clientAppContext, @Nullable String str, @Nullable String str2, boolean z) {
        return clientAppContext != null ? clientAppContext : (str == null && str2 == null) ? null : new ClientAppContext(str, str2, z, null, 0);
    }

    private static boolean zzax(String str, String str2) {
        return TextUtils.isEmpty(str) ? TextUtils.isEmpty(str2) : str.equals(str2);
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof ClientAppContext)) {
                return false;
            }
            ClientAppContext clientAppContext = (ClientAppContext) obj;
            if (!zzax(this.zzjfr, clientAppContext.zzjfr) || !zzax(this.zzjfs, clientAppContext.zzjfs) || this.zzjea != clientAppContext.zzjea || !zzax(this.zzjec, clientAppContext.zzjec)) {
                return false;
            }
            if (this.zzjft != clientAppContext.zzjft) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjfr, this.zzjfs, Boolean.valueOf(this.zzjea), this.zzjec, Integer.valueOf(this.zzjft)});
    }

    public final String toString() {
        return String.format("{realClientPackageName: %s, zeroPartyIdentifier: %s, useRealClientApiKey: %b, apiKey: %s, callingContext: %d}", new Object[]{this.zzjfr, this.zzjfs, Boolean.valueOf(this.zzjea), this.zzjec, Integer.valueOf(this.zzjft)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.zzjfr, false);
        zzd.zza(parcel, 3, this.zzjfs, false);
        zzd.zza(parcel, 4, this.zzjea);
        zzd.zzc(parcel, 5, this.zzjft);
        zzd.zza(parcel, 6, this.zzjec, false);
        zzd.zzai(parcel, zze);
    }
}
