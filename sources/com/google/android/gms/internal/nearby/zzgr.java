package com.google.android.gms.internal.nearby;

import android.os.ParcelUuid;
import android.support.annotation.Nullable;
import android.util.SparseArray;
import com.google.android.gms.common.util.Hex;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.UUID;

public final class zzgr {
    private static final ParcelUuid zzgm = ParcelUuid.fromString("00000000-0000-1000-8000-00805F9B34FB");
    private final int zzgn;
    @Nullable
    private final List<ParcelUuid> zzgo;
    @Nullable
    private final SparseArray<byte[]> zzgp;
    @Nullable
    private final Map<ParcelUuid, byte[]> zzgq;
    private final int zzgr;
    @Nullable
    private final String zzgs;
    private final byte[] zzgt;

    private zzgr(@Nullable List<ParcelUuid> list, @Nullable SparseArray<byte[]> sparseArray, @Nullable Map<ParcelUuid, byte[]> map, int i, int i2, @Nullable String str, byte[] bArr) {
        this.zzgo = list;
        this.zzgp = sparseArray;
        this.zzgq = map;
        this.zzgs = str;
        this.zzgn = i;
        this.zzgr = i2;
        this.zzgt = bArr;
    }

    private static int zza(byte[] bArr, int i, int i2, int i3, List<ParcelUuid> list) {
        while (i2 > 0) {
            list.add(zze(zza(bArr, i, i3)));
            i2 -= i3;
            i += i3;
        }
        return i;
    }

    private static byte[] zza(byte[] bArr, int i, int i2) {
        byte[] bArr2 = new byte[i2];
        System.arraycopy(bArr, i, bArr2, 0, i2);
        return bArr2;
    }

    /* JADX WARNING: Removed duplicated region for block: B:33:0x00a4 A[Catch:{ Exception -> 0x003b }] */
    @com.google.android.gms.common.util.VisibleForTesting
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.google.android.gms.internal.nearby.zzgr zzd(@android.support.annotation.Nullable byte[] r12) {
        /*
            r8 = 0
            if (r12 != 0) goto L_0x0005
            r0 = r8
        L_0x0004:
            return r0
        L_0x0005:
            r0 = 0
            r4 = -1
            java.util.ArrayList r1 = new java.util.ArrayList
            r1.<init>()
            r5 = -2147483648(0xffffffff80000000, float:-0.0)
            android.util.SparseArray r2 = new android.util.SparseArray
            r2.<init>()
            java.util.HashMap r3 = new java.util.HashMap
            r3.<init>()
            r6 = r8
        L_0x0019:
            int r7 = r12.length     // Catch:{ Exception -> 0x003b }
            if (r0 >= r7) goto L_0x009e
            int r7 = r0 + 1
            byte r0 = r12[r0]
            r0 = r0 & 255(0xff, float:3.57E-43)
            if (r0 == 0) goto L_0x009e
            int r0 = r0 + -1
            int r9 = r7 + 1
            byte r7 = r12[r7]
            r7 = r7 & 255(0xff, float:3.57E-43)
            switch(r7) {
                case 1: goto L_0x0031;
                case 2: goto L_0x0036;
                case 3: goto L_0x0036;
                case 4: goto L_0x0058;
                case 5: goto L_0x0058;
                case 6: goto L_0x005d;
                case 7: goto L_0x005d;
                case 8: goto L_0x0063;
                case 9: goto L_0x0063;
                case 10: goto L_0x006d;
                case 22: goto L_0x0070;
                case 255: goto L_0x0085;
                default: goto L_0x002f;
            }
        L_0x002f:
            int r0 = r0 + r9
            goto L_0x0019
        L_0x0031:
            byte r4 = r12[r9]
            r4 = r4 & 255(0xff, float:3.57E-43)
            goto L_0x002f
        L_0x0036:
            r7 = 2
            zza(r12, r9, r0, r7, r1)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x003b:
            r0 = move-exception
            r1 = r0
            java.lang.String r0 = java.util.Arrays.toString(r12)
            java.lang.String r0 = java.lang.String.valueOf(r0)
            int r2 = r0.length()
            if (r2 == 0) goto L_0x00ad
            java.lang.String r2 = "Unable to parse scan record: "
            java.lang.String r0 = r2.concat(r0)
        L_0x0051:
            java.lang.String r2 = "BleRecord"
            android.util.Log.w(r2, r0, r1)
            r0 = r8
            goto L_0x0004
        L_0x0058:
            r7 = 4
            zza(r12, r9, r0, r7, r1)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x005d:
            r7 = 16
            zza(r12, r9, r0, r7, r1)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x0063:
            java.lang.String r6 = new java.lang.String     // Catch:{ Exception -> 0x003b }
            byte[] r7 = zza(r12, r9, r0)     // Catch:{ Exception -> 0x003b }
            r6.<init>(r7)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x006d:
            byte r5 = r12[r9]
            goto L_0x002f
        L_0x0070:
            r7 = 2
            byte[] r7 = zza(r12, r9, r7)     // Catch:{ Exception -> 0x003b }
            android.os.ParcelUuid r7 = zze(r7)     // Catch:{ Exception -> 0x003b }
            int r10 = r9 + 2
            int r11 = r0 + -2
            byte[] r10 = zza(r12, r10, r11)     // Catch:{ Exception -> 0x003b }
            r3.put(r7, r10)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x0085:
            int r7 = r9 + 1
            byte r7 = r12[r7]     // Catch:{ Exception -> 0x003b }
            r7 = r7 & 255(0xff, float:3.57E-43)
            int r7 = r7 << 8
            byte r10 = r12[r9]     // Catch:{ Exception -> 0x003b }
            r10 = r10 & 255(0xff, float:3.57E-43)
            int r7 = r7 + r10
            int r10 = r9 + 2
            int r11 = r0 + -2
            byte[] r10 = zza(r12, r10, r11)     // Catch:{ Exception -> 0x003b }
            r2.put(r7, r10)     // Catch:{ Exception -> 0x003b }
            goto L_0x002f
        L_0x009e:
            boolean r0 = r1.isEmpty()     // Catch:{ Exception -> 0x003b }
            if (r0 == 0) goto L_0x00a5
            r1 = r8
        L_0x00a5:
            com.google.android.gms.internal.nearby.zzgr r0 = new com.google.android.gms.internal.nearby.zzgr     // Catch:{ Exception -> 0x003b }
            r7 = r12
            r0.<init>(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ Exception -> 0x003b }
            goto L_0x0004
        L_0x00ad:
            java.lang.String r0 = new java.lang.String
            java.lang.String r2 = "Unable to parse scan record: "
            r0.<init>(r2)
            goto L_0x0051
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.nearby.zzgr.zzd(byte[]):com.google.android.gms.internal.nearby.zzgr");
    }

    private static ParcelUuid zze(byte[] bArr) {
        if (bArr == null) {
            throw new IllegalArgumentException("uuidBytes cannot be null");
        }
        int length = bArr.length;
        if (length != 2 && length != 4 && length != 16) {
            throw new IllegalArgumentException("uuidBytes length invalid - " + length);
        } else if (length == 16) {
            ByteBuffer order = ByteBuffer.wrap(bArr).order(ByteOrder.LITTLE_ENDIAN);
            return new ParcelUuid(new UUID(order.getLong(8), order.getLong(0)));
        } else {
            return new ParcelUuid(new UUID(((length == 2 ? ((long) (bArr[0] & 255)) + ((long) ((bArr[1] & 255) << 8)) : ((((long) (bArr[0] & 255)) + ((long) ((bArr[1] & 255) << 8))) + ((long) ((bArr[2] & 255) << 16))) + ((long) ((bArr[3] & 255) << 24))) << 32) + zzgm.getUuid().getMostSignificantBits(), zzgm.getUuid().getLeastSignificantBits()));
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzgr)) {
            return false;
        }
        return Arrays.equals(this.zzgt, ((zzgr) obj).zzgt);
    }

    public final int hashCode() {
        return Arrays.hashCode(this.zzgt);
    }

    public final String toString() {
        String sb;
        String sb2;
        int i = 0;
        int i2 = this.zzgn;
        String valueOf = String.valueOf(this.zzgo);
        SparseArray<byte[]> sparseArray = this.zzgp;
        StringBuilder sb3 = new StringBuilder();
        if (sparseArray.size() <= 0) {
            sb = "{}";
        } else {
            sb3.append('{');
            for (int i3 = 0; i3 < sparseArray.size(); i3++) {
                if (i3 > 0) {
                    sb3.append(", ");
                }
                byte[] bArr = (byte[]) sparseArray.valueAt(i3);
                sb3.append(sparseArray.keyAt(i3));
                sb3.append('=');
                sb3.append(bArr == null ? null : Hex.bytesToStringUppercase(bArr));
            }
            sb3.append('}');
            sb = sb3.toString();
        }
        Map<ParcelUuid, byte[]> map = this.zzgq;
        StringBuilder sb4 = new StringBuilder();
        if (map.keySet().size() <= 0) {
            sb2 = "{}";
        } else {
            sb4.append('{');
            for (Entry entry : map.entrySet()) {
                if (i > 0) {
                    sb4.append(", ");
                }
                sb4.append(entry.getKey());
                sb4.append('=');
                byte[] bArr2 = (byte[]) entry.getValue();
                sb4.append(bArr2 == null ? null : Hex.bytesToStringUppercase(bArr2));
                i++;
            }
            sb4.append('}');
            sb2 = sb4.toString();
        }
        int i4 = this.zzgr;
        String str = this.zzgs;
        return new StringBuilder(String.valueOf(valueOf).length() + 139 + String.valueOf(sb).length() + String.valueOf(sb2).length() + String.valueOf(str).length()).append("BleRecord [mAdvertiseFlags=").append(i2).append(", mServiceUuids=").append(valueOf).append(", mManufacturerSpecificData=").append(sb).append(", mServiceData=").append(sb2).append(", mTxPowerLevel=").append(i4).append(", mDeviceName=").append(str).append("]").toString();
    }
}
