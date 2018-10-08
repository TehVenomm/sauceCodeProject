package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.util.SparseArray;
import com.google.android.gms.common.internal.safeparcel.zzc;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zza;
import com.google.android.gms.common.util.zzb;
import com.google.android.gms.common.util.zzn;
import com.google.android.gms.common.util.zzo;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

public class zzbdi extends zzbda {
    public static final Creator<zzbdi> CREATOR = new zzbdj();
    private final String mClassName;
    private final int zzdxt;
    private final zzbdd zzfws;
    private final Parcel zzfwz;
    private final int zzfxa = 2;
    private int zzfxb;
    private int zzfxc;

    zzbdi(int i, Parcel parcel, zzbdd zzbdd) {
        this.zzdxt = i;
        this.zzfwz = (Parcel) zzbp.zzu(parcel);
        this.zzfws = zzbdd;
        if (this.zzfws == null) {
            this.mClassName = null;
        } else {
            this.mClassName = this.zzfws.zzakv();
        }
        this.zzfxb = 2;
    }

    private static void zza(StringBuilder stringBuilder, int i, Object obj) {
        switch (i) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                stringBuilder.append(obj);
                return;
            case 7:
                stringBuilder.append("\"").append(zzn.zzgk(obj.toString())).append("\"");
                return;
            case 8:
                stringBuilder.append("\"").append(zzb.encode((byte[]) obj)).append("\"");
                return;
            case 9:
                stringBuilder.append("\"").append(zzb.zzj((byte[]) obj));
                stringBuilder.append("\"");
                return;
            case 10:
                zzo.zza(stringBuilder, (HashMap) obj);
                return;
            case 11:
                throw new IllegalArgumentException("Method does not accept concrete type.");
            default:
                throw new IllegalArgumentException("Unknown type = " + i);
        }
    }

    private final void zza(StringBuilder stringBuilder, zzbcy<?, ?> zzbcy, Parcel parcel, int i) {
        double[] dArr = null;
        int i2 = 0;
        int length;
        if (zzbcy.zzfwn) {
            stringBuilder.append("[");
            int dataPosition;
            switch (zzbcy.zzfwm) {
                case 0:
                    int[] zzw = com.google.android.gms.common.internal.safeparcel.zzb.zzw(parcel, i);
                    length = zzw.length;
                    while (i2 < length) {
                        if (i2 != 0) {
                            stringBuilder.append(",");
                        }
                        stringBuilder.append(Integer.toString(zzw[i2]));
                        i2++;
                    }
                    break;
                case 1:
                    Object[] objArr;
                    length = com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i);
                    dataPosition = parcel.dataPosition();
                    if (length != 0) {
                        int readInt = parcel.readInt();
                        objArr = new BigInteger[readInt];
                        while (i2 < readInt) {
                            objArr[i2] = new BigInteger(parcel.createByteArray());
                            i2++;
                        }
                        parcel.setDataPosition(length + dataPosition);
                    }
                    zza.zza(stringBuilder, objArr);
                    break;
                case 2:
                    zza.zza(stringBuilder, com.google.android.gms.common.internal.safeparcel.zzb.zzx(parcel, i));
                    break;
                case 3:
                    zza.zza(stringBuilder, com.google.android.gms.common.internal.safeparcel.zzb.zzy(parcel, i));
                    break;
                case 4:
                    length = com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i);
                    i2 = parcel.dataPosition();
                    if (length != 0) {
                        dArr = parcel.createDoubleArray();
                        parcel.setDataPosition(length + i2);
                    }
                    zza.zza(stringBuilder, dArr);
                    break;
                case 5:
                    zza.zza(stringBuilder, com.google.android.gms.common.internal.safeparcel.zzb.zzz(parcel, i));
                    break;
                case 6:
                    zza.zza(stringBuilder, com.google.android.gms.common.internal.safeparcel.zzb.zzv(parcel, i));
                    break;
                case 7:
                    zza.zza(stringBuilder, com.google.android.gms.common.internal.safeparcel.zzb.zzaa(parcel, i));
                    break;
                case 8:
                case 9:
                case 10:
                    throw new UnsupportedOperationException("List of type BASE64, BASE64_URL_SAFE, or STRING_MAP is not supported");
                case 11:
                    Parcel[] zzae = com.google.android.gms.common.internal.safeparcel.zzb.zzae(parcel, i);
                    dataPosition = zzae.length;
                    for (int i3 = 0; i3 < dataPosition; i3++) {
                        if (i3 > 0) {
                            stringBuilder.append(",");
                        }
                        zzae[i3].setDataPosition(0);
                        zza(stringBuilder, zzbcy.zzakt(), zzae[i3]);
                    }
                    break;
                default:
                    throw new IllegalStateException("Unknown field type out.");
            }
            stringBuilder.append("]");
            return;
        }
        switch (zzbcy.zzfwm) {
            case 0:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i));
                return;
            case 1:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzk(parcel, i));
                return;
            case 2:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i));
                return;
            case 3:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, i));
                return;
            case 4:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzn(parcel, i));
                return;
            case 5:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzp(parcel, i));
                return;
            case 6:
                stringBuilder.append(com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i));
                return;
            case 7:
                stringBuilder.append("\"").append(zzn.zzgk(com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i))).append("\"");
                return;
            case 8:
                stringBuilder.append("\"").append(zzb.encode(com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i))).append("\"");
                return;
            case 9:
                stringBuilder.append("\"").append(zzb.zzj(com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i)));
                stringBuilder.append("\"");
                return;
            case 10:
                Bundle zzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                Set<String> keySet = zzs.keySet();
                keySet.size();
                stringBuilder.append("{");
                length = 1;
                for (String str : keySet) {
                    if (length == 0) {
                        stringBuilder.append(",");
                    }
                    stringBuilder.append("\"").append(str).append("\"");
                    stringBuilder.append(":");
                    stringBuilder.append("\"").append(zzn.zzgk(zzs.getString(str))).append("\"");
                    length = 0;
                }
                stringBuilder.append("}");
                return;
            case 11:
                Parcel zzad = com.google.android.gms.common.internal.safeparcel.zzb.zzad(parcel, i);
                zzad.setDataPosition(0);
                zza(stringBuilder, zzbcy.zzakt(), zzad);
                return;
            default:
                throw new IllegalStateException("Unknown field type out");
        }
    }

    private final void zza(StringBuilder stringBuilder, Map<String, zzbcy<?, ?>> map, Parcel parcel) {
        Entry entry;
        SparseArray sparseArray = new SparseArray();
        for (Entry entry2 : map.entrySet()) {
            sparseArray.put(((zzbcy) entry2.getValue()).zzfwp, entry2);
        }
        stringBuilder.append('{');
        int zzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        Object obj = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            entry2 = (Entry) sparseArray.get(65535 & readInt);
            if (entry2 != null) {
                if (obj != null) {
                    stringBuilder.append(",");
                }
                String str = (String) entry2.getKey();
                zzbcy zzbcy = (zzbcy) entry2.getValue();
                stringBuilder.append("\"").append(str).append("\":");
                if (zzbcy.zzaks()) {
                    switch (zzbcy.zzfwm) {
                        case 0:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, Integer.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, readInt))));
                            break;
                        case 1:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, com.google.android.gms.common.internal.safeparcel.zzb.zzk(parcel, readInt)));
                            break;
                        case 2:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, Long.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, readInt))));
                            break;
                        case 3:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, Float.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, readInt))));
                            break;
                        case 4:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, Double.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzn(parcel, readInt))));
                            break;
                        case 5:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, com.google.android.gms.common.internal.safeparcel.zzb.zzp(parcel, readInt)));
                            break;
                        case 6:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, Boolean.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, readInt))));
                            break;
                        case 7:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, readInt)));
                            break;
                        case 8:
                        case 9:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, readInt)));
                            break;
                        case 10:
                            zzb(stringBuilder, zzbcy, zzbcx.zza(zzbcy, zzk(com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, readInt))));
                            break;
                        case 11:
                            throw new IllegalArgumentException("Method does not accept concrete type.");
                        default:
                            throw new IllegalArgumentException("Unknown field out type = " + zzbcy.zzfwm);
                    }
                }
                zza(stringBuilder, zzbcy, parcel, readInt);
                obj = 1;
            }
        }
        if (parcel.dataPosition() != zzd) {
            throw new zzc("Overread allowed size end=" + zzd, parcel);
        }
        stringBuilder.append('}');
    }

    private Parcel zzakx() {
        switch (this.zzfxb) {
            case 0:
                this.zzfxc = zzd.zze(this.zzfwz);
                break;
            case 1:
                break;
        }
        zzd.zzai(this.zzfwz, this.zzfxc);
        this.zzfxb = 2;
        return this.zzfwz;
    }

    private final void zzb(StringBuilder stringBuilder, zzbcy<?, ?> zzbcy, Object obj) {
        if (zzbcy.zzfwl) {
            ArrayList arrayList = (ArrayList) obj;
            stringBuilder.append("[");
            int size = arrayList.size();
            for (int i = 0; i < size; i++) {
                if (i != 0) {
                    stringBuilder.append(",");
                }
                zza(stringBuilder, zzbcy.zzfwk, arrayList.get(i));
            }
            stringBuilder.append("]");
            return;
        }
        zza(stringBuilder, zzbcy.zzfwk, obj);
    }

    private static HashMap<String, String> zzk(Bundle bundle) {
        HashMap<String, String> hashMap = new HashMap();
        for (String str : bundle.keySet()) {
            hashMap.put(str, bundle.getString(str));
        }
        return hashMap;
    }

    public String toString() {
        zzbp.zzb(this.zzfws, (Object) "Cannot convert to JSON on client side.");
        Parcel zzakx = zzakx();
        zzakx.setDataPosition(0);
        StringBuilder stringBuilder = new StringBuilder(100);
        zza(stringBuilder, this.zzfws.zzgj(this.mClassName), zzakx);
        return stringBuilder.toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        Parcelable parcelable;
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, zzakx(), false);
        switch (this.zzfxa) {
            case 0:
                parcelable = null;
                break;
            case 1:
                parcelable = this.zzfws;
                break;
            case 2:
                parcelable = this.zzfws;
                break;
            default:
                throw new IllegalStateException("Invalid creation type: " + this.zzfxa);
        }
        zzd.zza(parcel, 3, parcelable, i, false);
        zzd.zzai(parcel, zze);
    }

    public final Object zzgh(String str) {
        throw new UnsupportedOperationException("Converting to JSON does not require this method.");
    }

    public final boolean zzgi(String str) {
        throw new UnsupportedOperationException("Converting to JSON does not require this method.");
    }

    public final Map<String, zzbcy<?, ?>> zzzx() {
        return this.zzfws == null ? null : this.zzfws.zzgj(this.mClassName);
    }
}
