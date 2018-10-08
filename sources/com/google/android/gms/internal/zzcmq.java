package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Base64;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public final class zzcmq extends zza {
    public static final Creator<zzcmq> CREATOR = new zzcmw();
    private static byte[][] zzfdl = new byte[0][];
    private static zzcmq zzjhy = new zzcmq("", null, zzfdl, zzfdl, zzfdl, zzfdl, null, null);
    private static final zzcmv zzjih = new zzcmr();
    private static final zzcmv zzjii = new zzcms();
    private static final zzcmv zzjij = new zzcmt();
    private static final zzcmv zzjik = new zzcmu();
    private String zzjhz;
    private byte[] zzjia;
    private byte[][] zzjib;
    private byte[][] zzjic;
    private byte[][] zzjid;
    private byte[][] zzjie;
    private int[] zzjif;
    private byte[][] zzjig;

    public zzcmq(String str, byte[] bArr, byte[][] bArr2, byte[][] bArr3, byte[][] bArr4, byte[][] bArr5, int[] iArr, byte[][] bArr6) {
        this.zzjhz = str;
        this.zzjia = bArr;
        this.zzjib = bArr2;
        this.zzjic = bArr3;
        this.zzjid = bArr4;
        this.zzjie = bArr5;
        this.zzjif = iArr;
        this.zzjig = bArr6;
    }

    private static void zza(StringBuilder stringBuilder, String str, int[] iArr) {
        stringBuilder.append(str);
        stringBuilder.append("=");
        if (iArr == null) {
            stringBuilder.append("null");
            return;
        }
        stringBuilder.append("(");
        int length = iArr.length;
        Object obj = 1;
        int i = 0;
        while (i < length) {
            int i2 = iArr[i];
            if (obj == null) {
                stringBuilder.append(", ");
            }
            stringBuilder.append(i2);
            i++;
            obj = null;
        }
        stringBuilder.append(")");
    }

    private static void zza(StringBuilder stringBuilder, String str, byte[][] bArr) {
        stringBuilder.append(str);
        stringBuilder.append("=");
        if (bArr == null) {
            stringBuilder.append("null");
            return;
        }
        stringBuilder.append("(");
        int length = bArr.length;
        Object obj = 1;
        int i = 0;
        while (i < length) {
            byte[] bArr2 = bArr[i];
            if (obj == null) {
                stringBuilder.append(", ");
            }
            stringBuilder.append("'");
            stringBuilder.append(Base64.encodeToString(bArr2, 3));
            stringBuilder.append("'");
            i++;
            obj = null;
        }
        stringBuilder.append(")");
    }

    private static List<String> zzb(byte[][] bArr) {
        if (bArr == null) {
            return Collections.emptyList();
        }
        List<String> arrayList = new ArrayList(bArr.length);
        for (byte[] encodeToString : bArr) {
            arrayList.add(Base64.encodeToString(encodeToString, 3));
        }
        Collections.sort(arrayList);
        return arrayList;
    }

    private static List<Integer> zze(int[] iArr) {
        if (iArr == null) {
            return Collections.emptyList();
        }
        List<Integer> arrayList = new ArrayList(iArr.length);
        for (int valueOf : iArr) {
            arrayList.add(Integer.valueOf(valueOf));
        }
        Collections.sort(arrayList);
        return arrayList;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzcmq)) {
            return false;
        }
        zzcmq zzcmq = (zzcmq) obj;
        return zzcmx.equals(this.zzjhz, zzcmq.zzjhz) && Arrays.equals(this.zzjia, zzcmq.zzjia) && zzcmx.equals(zzb(this.zzjib), zzb(zzcmq.zzjib)) && zzcmx.equals(zzb(this.zzjic), zzb(zzcmq.zzjic)) && zzcmx.equals(zzb(this.zzjid), zzb(zzcmq.zzjid)) && zzcmx.equals(zzb(this.zzjie), zzb(zzcmq.zzjie)) && zzcmx.equals(zze(this.zzjif), zze(zzcmq.zzjif)) && zzcmx.equals(zzb(this.zzjig), zzb(zzcmq.zzjig));
    }

    public final String toString() {
        String str;
        StringBuilder stringBuilder = new StringBuilder("ExperimentTokens");
        stringBuilder.append("(");
        if (this.zzjhz == null) {
            str = "null";
        } else {
            str = this.zzjhz;
            str = new StringBuilder((String.valueOf("'").length() + String.valueOf(str).length()) + String.valueOf("'").length()).append("'").append(str).append("'").toString();
        }
        stringBuilder.append(str);
        stringBuilder.append(", ");
        byte[] bArr = this.zzjia;
        stringBuilder.append("direct");
        stringBuilder.append("=");
        if (bArr == null) {
            stringBuilder.append("null");
        } else {
            stringBuilder.append("'");
            stringBuilder.append(Base64.encodeToString(bArr, 3));
            stringBuilder.append("'");
        }
        stringBuilder.append(", ");
        zza(stringBuilder, "GAIA", this.zzjib);
        stringBuilder.append(", ");
        zza(stringBuilder, "PSEUDO", this.zzjic);
        stringBuilder.append(", ");
        zza(stringBuilder, "ALWAYS", this.zzjid);
        stringBuilder.append(", ");
        zza(stringBuilder, "OTHER", this.zzjie);
        stringBuilder.append(", ");
        zza(stringBuilder, "weak", this.zzjif);
        stringBuilder.append(", ");
        zza(stringBuilder, "directs", this.zzjig);
        stringBuilder.append(")");
        return stringBuilder.toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzjhz, false);
        zzd.zza(parcel, 3, this.zzjia, false);
        zzd.zza(parcel, 4, this.zzjib, false);
        zzd.zza(parcel, 5, this.zzjic, false);
        zzd.zza(parcel, 6, this.zzjid, false);
        zzd.zza(parcel, 7, this.zzjie, false);
        zzd.zza(parcel, 8, this.zzjif, false);
        zzd.zza(parcel, 9, this.zzjig, false);
        zzd.zzai(parcel, zze);
    }
}
