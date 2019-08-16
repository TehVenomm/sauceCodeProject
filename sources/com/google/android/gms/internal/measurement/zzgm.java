package com.google.android.gms.internal.measurement;

import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.internal.measurement.zzey.zzd;
import java.io.IOException;
import java.lang.reflect.Field;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import sun.misc.Unsafe;

final class zzgm<T> implements zzgx<T> {
    private static final int[] zzakh = new int[0];
    private static final Unsafe zzaki = zzhv.zzwv();
    private final int[] zzakj;
    private final Object[] zzakk;
    private final int zzakl;
    private final int zzakm;
    private final zzgi zzakn;
    private final boolean zzako;
    private final boolean zzakp;
    private final boolean zzakq;
    private final boolean zzakr;
    private final int[] zzaks;
    private final int zzakt;
    private final int zzaku;
    private final zzgq zzakv;
    private final zzfs zzakw;
    private final zzhp<?, ?> zzakx;
    private final zzen<?> zzaky;
    private final zzgb zzakz;

    private zzgm(int[] iArr, Object[] objArr, int i, int i2, zzgi zzgi, boolean z, boolean z2, int[] iArr2, int i3, int i4, zzgq zzgq, zzfs zzfs, zzhp<?, ?> zzhp, zzen<?> zzen, zzgb zzgb) {
        this.zzakj = iArr;
        this.zzakk = objArr;
        this.zzakl = i;
        this.zzakm = i2;
        this.zzakp = zzgi instanceof zzey;
        this.zzakq = z;
        this.zzako = zzen != null && zzen.zze(zzgi);
        this.zzakr = false;
        this.zzaks = iArr2;
        this.zzakt = i3;
        this.zzaku = i4;
        this.zzakv = zzgq;
        this.zzakw = zzfs;
        this.zzakx = zzhp;
        this.zzaky = zzen;
        this.zzakn = zzgi;
        this.zzakz = zzgb;
    }

    private static <UT, UB> int zza(zzhp<UT, UB> zzhp, T t) {
        return zzhp.zzt(zzhp.zzx(t));
    }

    private final int zza(T t, byte[] bArr, int i, int i2, int i3, int i4, int i5, int i6, int i7, long j, int i8, zzdk zzdk) throws IOException {
        int zza;
        Unsafe unsafe = zzaki;
        long j2 = (long) (this.zzakj[i8 + 2] & 1048575);
        switch (i7) {
            case 51:
                if (i5 == 1) {
                    unsafe.putObject(t, j, Double.valueOf(zzdl.zzc(bArr, i)));
                    zza = i + 8;
                    break;
                } else {
                    return i;
                }
            case 52:
                if (i5 == 5) {
                    unsafe.putObject(t, j, Float.valueOf(zzdl.zzd(bArr, i)));
                    zza = i + 4;
                    break;
                } else {
                    return i;
                }
            case 53:
            case 54:
                if (i5 == 0) {
                    zza = zzdl.zzb(bArr, i, zzdk);
                    unsafe.putObject(t, j, Long.valueOf(zzdk.zzadb));
                    break;
                } else {
                    return i;
                }
            case 55:
            case 62:
                if (i5 == 0) {
                    zza = zzdl.zza(bArr, i, zzdk);
                    unsafe.putObject(t, j, Integer.valueOf(zzdk.zzada));
                    break;
                } else {
                    return i;
                }
            case 56:
            case 65:
                if (i5 == 1) {
                    unsafe.putObject(t, j, Long.valueOf(zzdl.zzb(bArr, i)));
                    zza = i + 8;
                    break;
                } else {
                    return i;
                }
            case 57:
            case 64:
                if (i5 == 5) {
                    unsafe.putObject(t, j, Integer.valueOf(zzdl.zza(bArr, i)));
                    zza = i + 4;
                    break;
                } else {
                    return i;
                }
            case 58:
                if (i5 == 0) {
                    zza = zzdl.zzb(bArr, i, zzdk);
                    unsafe.putObject(t, j, Boolean.valueOf(zzdk.zzadb != 0));
                    break;
                } else {
                    return i;
                }
            case 59:
                if (i5 != 2) {
                    return i;
                }
                int zza2 = zzdl.zza(bArr, i, zzdk);
                int i9 = zzdk.zzada;
                if (i9 == 0) {
                    unsafe.putObject(t, j, "");
                } else if ((536870912 & i6) == 0 || zzhy.zzf(bArr, zza2, zza2 + i9)) {
                    unsafe.putObject(t, j, new String(bArr, zza2, i9, zzez.UTF_8));
                    zza2 += i9;
                } else {
                    throw zzfi.zzvb();
                }
                unsafe.putInt(t, j2, i4);
                return zza2;
            case 60:
                if (i5 != 2) {
                    return i;
                }
                int zza3 = zzdl.zza(zzbx(i8), bArr, i, i2, zzdk);
                Object obj = unsafe.getInt(t, j2) == i4 ? unsafe.getObject(t, j) : null;
                if (obj == null) {
                    unsafe.putObject(t, j, zzdk.zzadc);
                } else {
                    unsafe.putObject(t, j, zzez.zza(obj, zzdk.zzadc));
                }
                unsafe.putInt(t, j2, i4);
                return zza3;
            case 61:
                if (i5 == 2) {
                    zza = zzdl.zze(bArr, i, zzdk);
                    unsafe.putObject(t, j, zzdk.zzadc);
                    break;
                } else {
                    return i;
                }
            case 63:
                if (i5 != 0) {
                    return i;
                }
                zza = zzdl.zza(bArr, i, zzdk);
                int i10 = zzdk.zzada;
                zzfe zzbz = zzbz(i8);
                if (zzbz == null || zzbz.zzg(i10)) {
                    unsafe.putObject(t, j, Integer.valueOf(i10));
                    break;
                } else {
                    zzu(t).zzb(i3, (Object) Long.valueOf((long) i10));
                    return zza;
                }
                break;
            case 66:
                if (i5 == 0) {
                    zza = zzdl.zza(bArr, i, zzdk);
                    unsafe.putObject(t, j, Integer.valueOf(zzeb.zzaz(zzdk.zzada)));
                    break;
                } else {
                    return i;
                }
            case 67:
                if (i5 == 0) {
                    zza = zzdl.zzb(bArr, i, zzdk);
                    unsafe.putObject(t, j, Long.valueOf(zzeb.zzbm(zzdk.zzadb)));
                    break;
                } else {
                    return i;
                }
            case 68:
                if (i5 == 3) {
                    zza = zzdl.zza(zzbx(i8), bArr, i, i2, (i3 & -8) | 4, zzdk);
                    Object obj2 = unsafe.getInt(t, j2) == i4 ? unsafe.getObject(t, j) : null;
                    if (obj2 != null) {
                        unsafe.putObject(t, j, zzez.zza(obj2, zzdk.zzadc));
                        break;
                    } else {
                        unsafe.putObject(t, j, zzdk.zzadc);
                        break;
                    }
                } else {
                    return i;
                }
            default:
                return i;
        }
        unsafe.putInt(t, j2, i4);
        return zza;
    }

    private final int zza(T t, byte[] bArr, int i, int i2, int i3, int i4, int i5, int i6, long j, int i7, long j2, zzdk zzdk) throws IOException {
        zzff zzff;
        int zza;
        int i8;
        int i9;
        int i10;
        zzff zzff2 = (zzff) zzaki.getObject(t, j2);
        if (!zzff2.zzrx()) {
            int size = zzff2.size();
            zzff = zzff2.zzap(size == 0 ? 10 : size << 1);
            zzaki.putObject(t, j2, zzff);
        } else {
            zzff = zzff2;
        }
        switch (i7) {
            case 18:
            case 35:
                if (i5 == 2) {
                    zzeh zzeh = (zzeh) zzff;
                    int zza2 = zzdl.zza(bArr, i, zzdk);
                    int i11 = zzdk.zzada + zza2;
                    while (zza2 < i11) {
                        zzeh.zzf(zzdl.zzc(bArr, zza2));
                        zza2 += 8;
                    }
                    if (zza2 == i11) {
                        return zza2;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 1) {
                    return i;
                } else {
                    zzeh zzeh2 = (zzeh) zzff;
                    zzeh2.zzf(zzdl.zzc(bArr, i));
                    int i12 = i + 8;
                    while (i12 < i2) {
                        int zza3 = zzdl.zza(bArr, i12, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i12;
                        }
                        zzeh2.zzf(zzdl.zzc(bArr, zza3));
                        i12 = zza3 + 8;
                    }
                    return i12;
                }
            case 19:
            case 36:
                if (i5 == 2) {
                    zzeu zzeu = (zzeu) zzff;
                    int zza4 = zzdl.zza(bArr, i, zzdk);
                    int i13 = zzdk.zzada + zza4;
                    while (zza4 < i13) {
                        zzeu.zzc(zzdl.zzd(bArr, zza4));
                        zza4 += 4;
                    }
                    if (zza4 == i13) {
                        return zza4;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 5) {
                    return i;
                } else {
                    zzeu zzeu2 = (zzeu) zzff;
                    zzeu2.zzc(zzdl.zzd(bArr, i));
                    int i14 = i + 4;
                    while (i14 < i2) {
                        int zza5 = zzdl.zza(bArr, i14, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i14;
                        }
                        zzeu2.zzc(zzdl.zzd(bArr, zza5));
                        i14 = zza5 + 4;
                    }
                    return i14;
                }
            case 20:
            case 21:
            case 37:
            case 38:
                if (i5 == 2) {
                    zzfw zzfw = (zzfw) zzff;
                    int zza6 = zzdl.zza(bArr, i, zzdk);
                    int i15 = zzdk.zzada + zza6;
                    while (zza6 < i15) {
                        zza6 = zzdl.zzb(bArr, zza6, zzdk);
                        zzfw.zzby(zzdk.zzadb);
                    }
                    if (zza6 == i15) {
                        return zza6;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 0) {
                    return i;
                } else {
                    zzfw zzfw2 = (zzfw) zzff;
                    int zzb = zzdl.zzb(bArr, i, zzdk);
                    zzfw2.zzby(zzdk.zzadb);
                    while (zzb < i2) {
                        int zza7 = zzdl.zza(bArr, zzb, zzdk);
                        if (i3 != zzdk.zzada) {
                            return zzb;
                        }
                        zzb = zzdl.zzb(bArr, zza7, zzdk);
                        zzfw2.zzby(zzdk.zzadb);
                    }
                    return zzb;
                }
            case 22:
            case 29:
            case 39:
            case 43:
                return i5 == 2 ? zzdl.zza(bArr, i, zzff, zzdk) : i5 == 0 ? zzdl.zza(i3, bArr, i, i2, zzff, zzdk) : i;
            case 23:
            case 32:
            case 40:
            case 46:
                if (i5 == 2) {
                    zzfw zzfw3 = (zzfw) zzff;
                    int zza8 = zzdl.zza(bArr, i, zzdk);
                    int i16 = zzdk.zzada + zza8;
                    while (zza8 < i16) {
                        zzfw3.zzby(zzdl.zzb(bArr, zza8));
                        zza8 += 8;
                    }
                    if (zza8 == i16) {
                        return zza8;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 1) {
                    return i;
                } else {
                    zzfw zzfw4 = (zzfw) zzff;
                    zzfw4.zzby(zzdl.zzb(bArr, i));
                    int i17 = i + 8;
                    while (i17 < i2) {
                        int zza9 = zzdl.zza(bArr, i17, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i17;
                        }
                        zzfw4.zzby(zzdl.zzb(bArr, zza9));
                        i17 = zza9 + 8;
                    }
                    return i17;
                }
            case 24:
            case 31:
            case 41:
            case 45:
                if (i5 == 2) {
                    zzfa zzfa = (zzfa) zzff;
                    int zza10 = zzdl.zza(bArr, i, zzdk);
                    int i18 = zzdk.zzada + zza10;
                    while (zza10 < i18) {
                        zzfa.zzbu(zzdl.zza(bArr, zza10));
                        zza10 += 4;
                    }
                    if (zza10 == i18) {
                        return zza10;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 5) {
                    return i;
                } else {
                    zzfa zzfa2 = (zzfa) zzff;
                    zzfa2.zzbu(zzdl.zza(bArr, i));
                    int i19 = i + 4;
                    while (i19 < i2) {
                        int zza11 = zzdl.zza(bArr, i19, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i19;
                        }
                        zzfa2.zzbu(zzdl.zza(bArr, zza11));
                        i19 = zza11 + 4;
                    }
                    return i19;
                }
            case 25:
            case 42:
                if (i5 == 2) {
                    zzdn zzdn = (zzdn) zzff;
                    int zza12 = zzdl.zza(bArr, i, zzdk);
                    int i20 = zza12 + zzdk.zzada;
                    while (zza12 < i20) {
                        zza12 = zzdl.zzb(bArr, zza12, zzdk);
                        zzdn.addBoolean(zzdk.zzadb != 0);
                    }
                    if (zza12 == i20) {
                        return zza12;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 0) {
                    return i;
                } else {
                    zzdn zzdn2 = (zzdn) zzff;
                    int zzb2 = zzdl.zzb(bArr, i, zzdk);
                    zzdn2.addBoolean(zzdk.zzadb != 0);
                    while (zzb2 < i2) {
                        int zza13 = zzdl.zza(bArr, zzb2, zzdk);
                        if (i3 != zzdk.zzada) {
                            return zzb2;
                        }
                        zzb2 = zzdl.zzb(bArr, zza13, zzdk);
                        zzdn2.addBoolean(zzdk.zzadb != 0);
                    }
                    return zzb2;
                }
            case 26:
                if (i5 != 2) {
                    return i;
                }
                if ((536870912 & j) == 0) {
                    int zza14 = zzdl.zza(bArr, i, zzdk);
                    int i21 = zzdk.zzada;
                    if (i21 < 0) {
                        throw zzfi.zzuu();
                    }
                    if (i21 == 0) {
                        zzff.add("");
                    } else {
                        zzff.add(new String(bArr, zza14, i21, zzez.UTF_8));
                        zza14 += i21;
                    }
                    while (i10 < i2) {
                        int zza15 = zzdl.zza(bArr, i10, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i10;
                        }
                        i10 = zzdl.zza(bArr, zza15, zzdk);
                        int i22 = zzdk.zzada;
                        if (i22 < 0) {
                            throw zzfi.zzuu();
                        } else if (i22 == 0) {
                            zzff.add("");
                        } else {
                            zzff.add(new String(bArr, i10, i22, zzez.UTF_8));
                            i10 += i22;
                        }
                    }
                    return i10;
                }
                int zza16 = zzdl.zza(bArr, i, zzdk);
                int i23 = zzdk.zzada;
                if (i23 < 0) {
                    throw zzfi.zzuu();
                }
                if (i23 == 0) {
                    zzff.add("");
                } else {
                    if (!zzhy.zzf(bArr, zza16, zza16 + i23)) {
                        throw zzfi.zzvb();
                    }
                    zzff.add(new String(bArr, zza16, i23, zzez.UTF_8));
                    zza16 += i23;
                }
                while (i9 < i2) {
                    int zza17 = zzdl.zza(bArr, i9, zzdk);
                    if (i3 != zzdk.zzada) {
                        return i9;
                    }
                    i9 = zzdl.zza(bArr, zza17, zzdk);
                    int i24 = zzdk.zzada;
                    if (i24 < 0) {
                        throw zzfi.zzuu();
                    } else if (i24 == 0) {
                        zzff.add("");
                    } else {
                        if (!zzhy.zzf(bArr, i9, i9 + i24)) {
                            throw zzfi.zzvb();
                        }
                        zzff.add(new String(bArr, i9, i24, zzez.UTF_8));
                        i9 += i24;
                    }
                }
                return i9;
            case 27:
                return i5 == 2 ? zzdl.zza(zzbx(i6), i3, bArr, i, i2, zzff, zzdk) : i;
            case 28:
                if (i5 != 2) {
                    return i;
                }
                int zza18 = zzdl.zza(bArr, i, zzdk);
                int i25 = zzdk.zzada;
                if (i25 < 0) {
                    throw zzfi.zzuu();
                } else if (i25 > bArr.length - zza18) {
                    throw zzfi.zzut();
                } else {
                    if (i25 == 0) {
                        zzff.add(zzdp.zzadh);
                    } else {
                        zzff.add(zzdp.zzb(bArr, zza18, i25));
                        zza18 += i25;
                    }
                    while (i8 < i2) {
                        int zza19 = zzdl.zza(bArr, i8, zzdk);
                        if (i3 != zzdk.zzada) {
                            return i8;
                        }
                        i8 = zzdl.zza(bArr, zza19, zzdk);
                        int i26 = zzdk.zzada;
                        if (i26 < 0) {
                            throw zzfi.zzuu();
                        } else if (i26 > bArr.length - i8) {
                            throw zzfi.zzut();
                        } else if (i26 == 0) {
                            zzff.add(zzdp.zzadh);
                        } else {
                            zzff.add(zzdp.zzb(bArr, i8, i26));
                            i8 += i26;
                        }
                    }
                    return i8;
                }
            case 30:
            case 44:
                if (i5 == 2) {
                    zza = zzdl.zza(bArr, i, zzff, zzdk);
                } else if (i5 != 0) {
                    return i;
                } else {
                    zza = zzdl.zza(i3, bArr, i, i2, zzff, zzdk);
                }
                zzhs zzhs = ((zzey) t).zzahz;
                if (zzhs == zzhs.zzwq()) {
                    zzhs = null;
                }
                zzhs zzhs2 = (zzhs) zzgz.zza(i4, zzff, zzbz(i6), zzhs, this.zzakx);
                if (zzhs2 == null) {
                    return zza;
                }
                ((zzey) t).zzahz = zzhs2;
                return zza;
            case 33:
            case 47:
                if (i5 == 2) {
                    zzfa zzfa3 = (zzfa) zzff;
                    int zza20 = zzdl.zza(bArr, i, zzdk);
                    int i27 = zzdk.zzada + zza20;
                    while (zza20 < i27) {
                        zza20 = zzdl.zza(bArr, zza20, zzdk);
                        zzfa3.zzbu(zzeb.zzaz(zzdk.zzada));
                    }
                    if (zza20 == i27) {
                        return zza20;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 0) {
                    return i;
                } else {
                    zzfa zzfa4 = (zzfa) zzff;
                    int zza21 = zzdl.zza(bArr, i, zzdk);
                    zzfa4.zzbu(zzeb.zzaz(zzdk.zzada));
                    while (zza21 < i2) {
                        int zza22 = zzdl.zza(bArr, zza21, zzdk);
                        if (i3 != zzdk.zzada) {
                            return zza21;
                        }
                        zza21 = zzdl.zza(bArr, zza22, zzdk);
                        zzfa4.zzbu(zzeb.zzaz(zzdk.zzada));
                    }
                    return zza21;
                }
            case 34:
            case 48:
                if (i5 == 2) {
                    zzfw zzfw5 = (zzfw) zzff;
                    int zza23 = zzdl.zza(bArr, i, zzdk);
                    int i28 = zzdk.zzada + zza23;
                    while (zza23 < i28) {
                        zza23 = zzdl.zzb(bArr, zza23, zzdk);
                        zzfw5.zzby(zzeb.zzbm(zzdk.zzadb));
                    }
                    if (zza23 == i28) {
                        return zza23;
                    }
                    throw zzfi.zzut();
                } else if (i5 != 0) {
                    return i;
                } else {
                    zzfw zzfw6 = (zzfw) zzff;
                    int zzb3 = zzdl.zzb(bArr, i, zzdk);
                    zzfw6.zzby(zzeb.zzbm(zzdk.zzadb));
                    while (zzb3 < i2) {
                        int zza24 = zzdl.zza(bArr, zzb3, zzdk);
                        if (i3 != zzdk.zzada) {
                            return zzb3;
                        }
                        zzb3 = zzdl.zzb(bArr, zza24, zzdk);
                        zzfw6.zzby(zzeb.zzbm(zzdk.zzadb));
                    }
                    return zzb3;
                }
            case 49:
                if (i5 != 3) {
                    return i;
                }
                zzgx zzbx = zzbx(i6);
                int i29 = (i3 & -8) | 4;
                int zza25 = zzdl.zza(zzbx, bArr, i, i2, i29, zzdk);
                zzff.add(zzdk.zzadc);
                while (zza25 < i2) {
                    int zza26 = zzdl.zza(bArr, zza25, zzdk);
                    if (i3 != zzdk.zzada) {
                        return zza25;
                    }
                    zza25 = zzdl.zza(zzbx, bArr, zza26, i2, i29, zzdk);
                    zzff.add(zzdk.zzadc);
                }
                return zza25;
            default:
                return i;
        }
    }

    /* JADX WARNING: type inference failed for: r3v9, types: [int] */
    /* JADX WARNING: type inference failed for: r3v19 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final <K, V> int zza(T r17, byte[] r18, int r19, int r20, int r21, long r22, com.google.android.gms.internal.measurement.zzdk r24) throws java.io.IOException {
        /*
            r16 = this;
            sun.misc.Unsafe r5 = zzaki
            r0 = r16
            r1 = r21
            java.lang.Object r6 = r0.zzby(r1)
            r0 = r17
            r1 = r22
            java.lang.Object r4 = r5.getObject(r0, r1)
            r0 = r16
            com.google.android.gms.internal.measurement.zzgb r3 = r0.zzakz
            boolean r3 = r3.zzo(r4)
            if (r3 == 0) goto L_0x00d3
            r0 = r16
            com.google.android.gms.internal.measurement.zzgb r3 = r0.zzakz
            java.lang.Object r3 = r3.zzq(r6)
            r0 = r16
            com.google.android.gms.internal.measurement.zzgb r7 = r0.zzakz
            r7.zzb(r3, r4)
            r0 = r17
            r1 = r22
            r5.putObject(r0, r1, r3)
        L_0x0032:
            r0 = r16
            com.google.android.gms.internal.measurement.zzgb r4 = r0.zzakz
            com.google.android.gms.internal.measurement.zzfz r11 = r4.zzr(r6)
            r0 = r16
            com.google.android.gms.internal.measurement.zzgb r4 = r0.zzakz
            java.util.Map r12 = r4.zzm(r3)
            r0 = r18
            r1 = r19
            r2 = r24
            int r4 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r1, r2)
            r0 = r24
            int r3 = r0.zzada
            if (r3 < 0) goto L_0x0056
            int r5 = r20 - r4
            if (r3 <= r5) goto L_0x005b
        L_0x0056:
            com.google.android.gms.internal.measurement.zzfi r3 = com.google.android.gms.internal.measurement.zzfi.zzut()
            throw r3
        L_0x005b:
            int r13 = r4 + r3
            K r6 = r11.zzakc
            V r3 = r11.zzaba
            r9 = r3
            r5 = r4
            r10 = r6
        L_0x0064:
            if (r5 >= r13) goto L_0x00c8
            int r4 = r5 + 1
            byte r3 = r18[r5]
            if (r3 >= 0) goto L_0x0078
            r0 = r18
            r1 = r24
            int r4 = com.google.android.gms.internal.measurement.zzdl.zza(r3, r0, r4, r1)
            r0 = r24
            int r3 = r0.zzada
        L_0x0078:
            r5 = r3 & 7
            int r6 = r3 >>> 3
            switch(r6) {
                case 1: goto L_0x008b;
                case 2: goto L_0x00a7;
                default: goto L_0x007f;
            }
        L_0x007f:
            r0 = r18
            r1 = r20
            r2 = r24
            int r3 = com.google.android.gms.internal.measurement.zzdl.zza(r3, r0, r4, r1, r2)
            r5 = r3
            goto L_0x0064
        L_0x008b:
            com.google.android.gms.internal.measurement.zzig r6 = r11.zzakb
            int r6 = r6.zzxa()
            if (r5 != r6) goto L_0x007f
            com.google.android.gms.internal.measurement.zzig r6 = r11.zzakb
            r7 = 0
            r3 = r18
            r5 = r20
            r8 = r24
            int r3 = zza(r3, r4, r5, r6, r7, r8)
            r0 = r24
            java.lang.Object r4 = r0.zzadc
            r5 = r3
            r10 = r4
            goto L_0x0064
        L_0x00a7:
            com.google.android.gms.internal.measurement.zzig r6 = r11.zzakd
            int r6 = r6.zzxa()
            if (r5 != r6) goto L_0x007f
            com.google.android.gms.internal.measurement.zzig r6 = r11.zzakd
            V r3 = r11.zzaba
            java.lang.Class r7 = r3.getClass()
            r3 = r18
            r5 = r20
            r8 = r24
            int r4 = zza(r3, r4, r5, r6, r7, r8)
            r0 = r24
            java.lang.Object r3 = r0.zzadc
            r9 = r3
            r5 = r4
            goto L_0x0064
        L_0x00c8:
            if (r5 == r13) goto L_0x00cf
            com.google.android.gms.internal.measurement.zzfi r3 = com.google.android.gms.internal.measurement.zzfi.zzva()
            throw r3
        L_0x00cf:
            r12.put(r10, r9)
            return r13
        L_0x00d3:
            r3 = r4
            goto L_0x0032
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zza(java.lang.Object, byte[], int, int, int, long, com.google.android.gms.internal.measurement.zzdk):int");
    }

    private static int zza(byte[] bArr, int i, int i2, zzig zzig, Class<?> cls, zzdk zzdk) throws IOException {
        switch (zzig) {
            case BOOL:
                int zzb = zzdl.zzb(bArr, i, zzdk);
                zzdk.zzadc = Boolean.valueOf(zzdk.zzadb != 0);
                return zzb;
            case BYTES:
                return zzdl.zze(bArr, i, zzdk);
            case DOUBLE:
                zzdk.zzadc = Double.valueOf(zzdl.zzc(bArr, i));
                return i + 8;
            case FIXED32:
            case SFIXED32:
                zzdk.zzadc = Integer.valueOf(zzdl.zza(bArr, i));
                return i + 4;
            case FIXED64:
            case SFIXED64:
                zzdk.zzadc = Long.valueOf(zzdl.zzb(bArr, i));
                return i + 8;
            case FLOAT:
                zzdk.zzadc = Float.valueOf(zzdl.zzd(bArr, i));
                return i + 4;
            case ENUM:
            case INT32:
            case UINT32:
                int zza = zzdl.zza(bArr, i, zzdk);
                zzdk.zzadc = Integer.valueOf(zzdk.zzada);
                return zza;
            case INT64:
            case UINT64:
                int zzb2 = zzdl.zzb(bArr, i, zzdk);
                zzdk.zzadc = Long.valueOf(zzdk.zzadb);
                return zzb2;
            case MESSAGE:
                return zzdl.zza(zzgt.zzvy().zzf(cls), bArr, i, i2, zzdk);
            case SINT32:
                int zza2 = zzdl.zza(bArr, i, zzdk);
                zzdk.zzadc = Integer.valueOf(zzeb.zzaz(zzdk.zzada));
                return zza2;
            case SINT64:
                int zzb3 = zzdl.zzb(bArr, i, zzdk);
                zzdk.zzadc = Long.valueOf(zzeb.zzbm(zzdk.zzadb));
                return zzb3;
            case STRING:
                return zzdl.zzd(bArr, i, zzdk);
            default:
                throw new RuntimeException("unsupported field type.");
        }
    }

    static <T> zzgm<T> zza(Class<T> cls, zzgg zzgg, zzgq zzgq, zzfs zzfs, zzhp<?, ?> zzhp, zzen<?> zzen, zzgb zzgb) {
        char c;
        int i;
        int i2;
        int i3;
        char c2;
        int i4;
        char charAt;
        int i5;
        char charAt2;
        int i6;
        int charAt3;
        int i7;
        int i8;
        int i9;
        int i10;
        int charAt4;
        int i11;
        int[] iArr;
        int i12;
        char c3;
        int i13;
        int i14;
        char charAt5;
        int i15;
        char charAt6;
        char charAt7;
        char charAt8;
        char charAt9;
        char charAt10;
        int i16;
        char charAt11;
        char charAt12;
        int i17;
        int i18;
        int i19;
        int i20;
        int i21;
        int i22;
        int objectFieldOffset;
        int i23;
        int i24;
        int i25;
        int i26;
        Field zza;
        int i27;
        char charAt13;
        char c4;
        Field zza2;
        Field zza3;
        int i28;
        char charAt14;
        int i29;
        char charAt15;
        int i30;
        char charAt16;
        char charAt17;
        int i31;
        char charAt18;
        if (zzgg instanceof zzgv) {
            zzgv zzgv = (zzgv) zzgg;
            boolean z = zzgv.zzvr() == zzd.zzaim;
            String zzvz = zzgv.zzvz();
            int length = zzvz.length();
            int i32 = 1;
            char charAt19 = zzvz.charAt(0);
            if (charAt19 >= 55296) {
                char c5 = charAt19 & 8191;
                int i33 = 13;
                while (true) {
                    i31 = i32 + 1;
                    charAt18 = zzvz.charAt(i32);
                    if (charAt18 < 55296) {
                        break;
                    }
                    c5 |= (charAt18 & 8191) << i33;
                    i33 += 13;
                    i32 = i31;
                }
                c = (charAt18 << i33) | c5;
                i = i31;
            } else {
                c = charAt19;
                i = 1;
            }
            int i34 = i + 1;
            char charAt20 = zzvz.charAt(i);
            if (charAt20 >= 55296) {
                char c6 = charAt20 & 8191;
                int i35 = 13;
                while (true) {
                    i2 = i34 + 1;
                    charAt17 = zzvz.charAt(i34);
                    if (charAt17 < 55296) {
                        break;
                    }
                    c6 |= (charAt17 & 8191) << i35;
                    i35 += 13;
                    i34 = i2;
                }
                charAt20 = (charAt17 << i35) | c6;
            } else {
                i2 = i34;
            }
            if (charAt20 == 0) {
                charAt4 = 0;
                iArr = zzakh;
                charAt = 0;
                charAt3 = 0;
                charAt2 = 0;
                i12 = 0;
                c3 = 0;
                i13 = i2;
                i14 = 0;
            } else {
                int i36 = i2 + 1;
                char charAt21 = zzvz.charAt(i2);
                if (charAt21 >= 55296) {
                    int i37 = 13;
                    char c7 = charAt21 & 8191;
                    while (true) {
                        i3 = i36 + 1;
                        charAt12 = zzvz.charAt(i36);
                        if (charAt12 < 55296) {
                            break;
                        }
                        c7 |= (charAt12 & 8191) << i37;
                        i37 += 13;
                        i36 = i3;
                    }
                    charAt21 = (charAt12 << i37) | c7;
                } else {
                    i3 = i36;
                }
                int i38 = i3 + 1;
                char charAt22 = zzvz.charAt(i3);
                if (charAt22 >= 55296) {
                    char c8 = charAt22 & 8191;
                    int i39 = 13;
                    while (true) {
                        i16 = i38 + 1;
                        charAt11 = zzvz.charAt(i38);
                        if (charAt11 < 55296) {
                            break;
                        }
                        c8 |= (charAt11 & 8191) << i39;
                        i39 += 13;
                        i38 = i16;
                    }
                    c2 = (charAt11 << i39) | c8;
                    i4 = i16;
                } else {
                    c2 = charAt22;
                    i4 = i38;
                }
                int i40 = i4 + 1;
                charAt = zzvz.charAt(i4);
                if (charAt >= 55296) {
                    int i41 = 13;
                    char c9 = charAt & 8191;
                    while (true) {
                        i5 = i40 + 1;
                        charAt10 = zzvz.charAt(i40);
                        if (charAt10 < 55296) {
                            break;
                        }
                        c9 |= (charAt10 & 8191) << i41;
                        i41 += 13;
                        i40 = i5;
                    }
                    charAt = (charAt10 << i41) | c9;
                } else {
                    i5 = i40;
                }
                int i42 = i5 + 1;
                charAt2 = zzvz.charAt(i5);
                if (charAt2 >= 55296) {
                    char c10 = charAt2 & 8191;
                    int i43 = 13;
                    while (true) {
                        i6 = i42 + 1;
                        charAt9 = zzvz.charAt(i42);
                        if (charAt9 < 55296) {
                            break;
                        }
                        c10 |= (charAt9 & 8191) << i43;
                        i43 += 13;
                        i42 = i6;
                    }
                    charAt2 = (charAt9 << i43) | c10;
                } else {
                    i6 = i42;
                }
                int i44 = i6 + 1;
                charAt3 = zzvz.charAt(i6);
                if (charAt3 >= 55296) {
                    int i45 = charAt3 & 8191;
                    int i46 = 13;
                    while (true) {
                        i7 = i44 + 1;
                        charAt8 = zzvz.charAt(i44);
                        if (charAt8 < 55296) {
                            break;
                        }
                        i45 |= (charAt8 & 8191) << i46;
                        i46 += 13;
                        i44 = i7;
                    }
                    charAt3 = (charAt8 << i46) | i45;
                } else {
                    i7 = i44;
                }
                int i47 = i7 + 1;
                int charAt23 = zzvz.charAt(i7);
                if (charAt23 >= 55296) {
                    int i48 = 13;
                    int i49 = charAt23 & 8191;
                    while (true) {
                        i8 = i47 + 1;
                        charAt7 = zzvz.charAt(i47);
                        if (charAt7 < 55296) {
                            break;
                        }
                        i49 |= (charAt7 & 8191) << i48;
                        i48 += 13;
                        i47 = i8;
                    }
                    charAt23 = (charAt7 << i48) | i49;
                } else {
                    i8 = i47;
                }
                int i50 = i8 + 1;
                char charAt24 = zzvz.charAt(i8);
                if (charAt24 >= 55296) {
                    int i51 = 13;
                    int i52 = charAt24 & 8191;
                    while (true) {
                        i15 = i50 + 1;
                        charAt6 = zzvz.charAt(i50);
                        if (charAt6 < 55296) {
                            break;
                        }
                        i52 |= (charAt6 & 8191) << i51;
                        i51 += 13;
                        i50 = i15;
                    }
                    i9 = (charAt6 << i51) | i52;
                    i10 = i15;
                } else {
                    i9 = charAt24;
                    i10 = i50;
                }
                int i53 = i10 + 1;
                charAt4 = zzvz.charAt(i10);
                if (charAt4 >= 55296) {
                    int i54 = charAt4 & 8191;
                    int i55 = 13;
                    while (true) {
                        i11 = i53 + 1;
                        charAt5 = zzvz.charAt(i53);
                        if (charAt5 < 55296) {
                            break;
                        }
                        i54 |= (charAt5 & 8191) << i55;
                        i55 += 13;
                        i53 = i11;
                    }
                    charAt4 = (charAt5 << i55) | i54;
                } else {
                    i11 = i53;
                }
                iArr = new int[(charAt4 + charAt23 + i9)];
                i12 = charAt23;
                c3 = charAt21;
                i13 = i11;
                i14 = (charAt21 << 1) + c2;
            }
            Unsafe unsafe = zzaki;
            Object[] zzwa = zzgv.zzwa();
            int i56 = 0;
            Class cls2 = zzgv.zzvt().getClass();
            int[] iArr2 = new int[(charAt3 * 3)];
            Object[] objArr = new Object[(charAt3 << 1)];
            int i57 = charAt4 + i12;
            int i58 = 0;
            int i59 = charAt4;
            while (i13 < length) {
                int i60 = i13 + 1;
                int charAt25 = zzvz.charAt(i13);
                if (charAt25 >= 55296) {
                    int i61 = 13;
                    int i62 = charAt25 & 8191;
                    while (true) {
                        i30 = i60 + 1;
                        charAt16 = zzvz.charAt(i60);
                        if (charAt16 < 55296) {
                            break;
                        }
                        i62 |= (charAt16 & 8191) << i61;
                        i61 += 13;
                        i60 = i30;
                    }
                    i18 = (charAt16 << i61) | i62;
                    i19 = i30;
                } else {
                    i18 = charAt25;
                    i19 = i60;
                }
                int i63 = i19 + 1;
                char charAt26 = zzvz.charAt(i19);
                if (charAt26 >= 55296) {
                    int i64 = 13;
                    int i65 = charAt26 & 8191;
                    while (true) {
                        i29 = i63 + 1;
                        charAt15 = zzvz.charAt(i63);
                        if (charAt15 < 55296) {
                            break;
                        }
                        i65 |= (charAt15 & 8191) << i64;
                        i64 += 13;
                        i63 = i29;
                    }
                    i20 = (charAt15 << i64) | i65;
                    i21 = i29;
                } else {
                    i20 = charAt26;
                    i21 = i63;
                }
                int i66 = i20 & 255;
                if ((i20 & 1024) != 0) {
                    iArr[i56] = i58;
                    i56++;
                }
                if (i66 >= 51) {
                    int i67 = i22 + 1;
                    char charAt27 = zzvz.charAt(i22);
                    if (charAt27 >= 55296) {
                        char c11 = charAt27 & 8191;
                        int i68 = 13;
                        while (true) {
                            i28 = i67 + 1;
                            charAt14 = zzvz.charAt(i67);
                            if (charAt14 < 55296) {
                                break;
                            }
                            c11 |= (charAt14 & 8191) << i68;
                            i68 += 13;
                            i67 = i28;
                        }
                        c4 = (charAt14 << i68) | c11;
                        i25 = i28;
                    } else {
                        c4 = charAt27;
                        i25 = i67;
                    }
                    int i69 = i66 - 51;
                    if (i69 == 9 || i69 == 17) {
                        objArr[((i58 / 3) << 1) + 1] = zzwa[i17];
                        i17++;
                    } else if (i69 == 12 && (c & 1) == 1) {
                        objArr[((i58 / 3) << 1) + 1] = zzwa[i17];
                        i17++;
                    }
                    int i70 = c4 << 1;
                    Object obj = zzwa[i70];
                    if (obj instanceof Field) {
                        zza2 = (Field) obj;
                    } else {
                        zza2 = zza(cls2, (String) obj);
                        zzwa[i70] = zza2;
                    }
                    objectFieldOffset = (int) unsafe.objectFieldOffset(zza2);
                    int i71 = i70 + 1;
                    Object obj2 = zzwa[i71];
                    if (obj2 instanceof Field) {
                        zza3 = (Field) obj2;
                    } else {
                        zza3 = zza(cls2, (String) obj2);
                        zzwa[i71] = zza3;
                    }
                    i23 = (int) unsafe.objectFieldOffset(zza3);
                    i24 = 0;
                } else {
                    int i72 = i17 + 1;
                    Field zza4 = zza(cls2, (String) zzwa[i17]);
                    if (i66 == 9 || i66 == 17) {
                        objArr[((i58 / 3) << 1) + 1] = zza4.getType();
                        i17 = i72;
                    } else if (i66 == 27 || i66 == 49) {
                        objArr[((i58 / 3) << 1) + 1] = zzwa[i72];
                        i17 = i72 + 1;
                    } else {
                        if (i66 == 12 || i66 == 30 || i66 == 44) {
                            if ((c & 1) == 1) {
                                objArr[((i58 / 3) << 1) + 1] = zzwa[i72];
                                i17 = i72 + 1;
                            }
                        } else if (i66 == 50) {
                            int i73 = i59 + 1;
                            iArr[i59] = i58;
                            i17 = i72 + 1;
                            objArr[(i58 / 3) << 1] = zzwa[i72];
                            if ((i20 & 2048) != 0) {
                                objArr[((i58 / 3) << 1) + 1] = zzwa[i17];
                                i17++;
                                i59 = i73;
                            } else {
                                i59 = i73;
                            }
                        }
                        i17 = i72;
                    }
                    objectFieldOffset = (int) unsafe.objectFieldOffset(zza4);
                    if ((c & 1) != 1 || i66 > 17) {
                        i23 = 0;
                        i24 = 0;
                    } else {
                        int i74 = i22 + 1;
                        char charAt28 = zzvz.charAt(i22);
                        if (charAt28 >= 55296) {
                            int i75 = charAt28 & 8191;
                            int i76 = 13;
                            while (true) {
                                i27 = i74 + 1;
                                charAt13 = zzvz.charAt(i74);
                                if (charAt13 < 55296) {
                                    break;
                                }
                                i75 |= (charAt13 & 8191) << i76;
                                i76 += 13;
                                i74 = i27;
                            }
                            i22 = i27;
                            i26 = (charAt13 << i76) | i75;
                        } else {
                            i22 = i74;
                            i26 = charAt28;
                        }
                        int i77 = (c3 << 1) + (i26 / 32);
                        Object obj3 = zzwa[i77];
                        if (obj3 instanceof Field) {
                            zza = (Field) obj3;
                        } else {
                            zza = zza(cls2, (String) obj3);
                            zzwa[i77] = zza;
                        }
                        i23 = (int) unsafe.objectFieldOffset(zza);
                        i24 = i26 % 32;
                    }
                    if (i66 >= 18 && i66 <= 49) {
                        iArr[i57] = objectFieldOffset;
                        i57++;
                    }
                }
                int i78 = i58 + 1;
                iArr2[i58] = i18;
                int i79 = i78 + 1;
                iArr2[i78] = objectFieldOffset | ((i20 & 256) != 0 ? DriveFile.MODE_READ_ONLY : 0) | ((i20 & 512) != 0 ? 536870912 : 0) | (i66 << 20);
                iArr2[i79] = i23 | (i24 << 20);
                i58 = i79 + 1;
                i13 = i25;
            }
            return new zzgm<>(iArr2, objArr, charAt, charAt2, zzgv.zzvt(), z, false, iArr, charAt4, i12 + charAt4, zzgq, zzfs, zzhp, zzen, zzgb);
        }
        if (((zzhm) zzgg).zzvr() == zzd.zzaim) {
        }
        throw new NoSuchMethodError();
    }

    private final <K, V, UT, UB> UB zza(int i, int i2, Map<K, V> map, zzfe zzfe, UB ub, zzhp<UT, UB> zzhp) {
        zzfz zzr = this.zzakz.zzr(zzby(i));
        Iterator it = map.entrySet().iterator();
        UB ub2 = ub;
        while (it.hasNext()) {
            Entry entry = (Entry) it.next();
            if (!zzfe.zzg(((Integer) entry.getValue()).intValue())) {
                UB ub3 = ub2 == null ? zzhp.zzwp() : ub2;
                zzdx zzas = zzdp.zzas(zzga.zza(zzr, entry.getKey(), entry.getValue()));
                try {
                    zzga.zza(zzas.zzsf(), zzr, entry.getKey(), entry.getValue());
                    zzhp.zza(ub3, i2, zzas.zzse());
                    it.remove();
                    ub2 = ub3;
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            }
        }
        return ub2;
    }

    private final <UT, UB> UB zza(Object obj, int i, UB ub, zzhp<UT, UB> zzhp) {
        int i2 = this.zzakj[i];
        Object zzp = zzhv.zzp(obj, (long) (zzca(i) & 1048575));
        if (zzp == null) {
            return ub;
        }
        zzfe zzbz = zzbz(i);
        if (zzbz == null) {
            return ub;
        }
        return zza(i, i2, this.zzakz.zzm(zzp), zzbz, ub, zzhp);
    }

    private static Field zza(Class<?> cls, String str) {
        try {
            return cls.getDeclaredField(str);
        } catch (NoSuchFieldException e) {
            Field[] declaredFields = cls.getDeclaredFields();
            for (Field field : declaredFields) {
                if (str.equals(field.getName())) {
                    return field;
                }
            }
            String name = cls.getName();
            String arrays = Arrays.toString(declaredFields);
            throw new RuntimeException(new StringBuilder(String.valueOf(str).length() + 40 + String.valueOf(name).length() + String.valueOf(arrays).length()).append("Field ").append(str).append(" for ").append(name).append(" not found. Known fields are ").append(arrays).toString());
        }
    }

    private static void zza(int i, Object obj, zzim zzim) throws IOException {
        if (obj instanceof String) {
            zzim.zzb(i, (String) obj);
        } else {
            zzim.zza(i, (zzdp) obj);
        }
    }

    private static <UT, UB> void zza(zzhp<UT, UB> zzhp, T t, zzim zzim) throws IOException {
        zzhp.zza(zzhp.zzx(t), zzim);
    }

    private final <K, V> void zza(zzim zzim, int i, Object obj, int i2) throws IOException {
        if (obj != null) {
            zzim.zza(i, this.zzakz.zzr(zzby(i2)), this.zzakz.zzn(obj));
        }
    }

    private final void zza(Object obj, int i, zzgy zzgy) throws IOException {
        if (zzcc(i)) {
            zzhv.zza(obj, (long) (i & 1048575), (Object) zzgy.zzsn());
        } else if (this.zzakp) {
            zzhv.zza(obj, (long) (i & 1048575), (Object) zzgy.readString());
        } else {
            zzhv.zza(obj, (long) (i & 1048575), (Object) zzgy.zzso());
        }
    }

    private final void zza(T t, T t2, int i) {
        long zzca = (long) (zzca(i) & 1048575);
        if (zza(t2, i)) {
            Object zzp = zzhv.zzp(t, zzca);
            Object zzp2 = zzhv.zzp(t2, zzca);
            if (zzp != null && zzp2 != null) {
                zzhv.zza((Object) t, zzca, zzez.zza(zzp, zzp2));
                zzb(t, i);
            } else if (zzp2 != null) {
                zzhv.zza((Object) t, zzca, zzp2);
                zzb(t, i);
            }
        }
    }

    private final boolean zza(T t, int i) {
        if (this.zzakq) {
            int zzca = zzca(i);
            long j = (long) (1048575 & zzca);
            switch ((zzca & 267386880) >>> 20) {
                case 0:
                    return zzhv.zzo(t, j) != 0.0d;
                case 1:
                    return zzhv.zzn(t, j) != 0.0f;
                case 2:
                    return zzhv.zzl(t, j) != 0;
                case 3:
                    return zzhv.zzl(t, j) != 0;
                case 4:
                    return zzhv.zzk(t, j) != 0;
                case 5:
                    return zzhv.zzl(t, j) != 0;
                case 6:
                    return zzhv.zzk(t, j) != 0;
                case 7:
                    return zzhv.zzm(t, j);
                case 8:
                    Object zzp = zzhv.zzp(t, j);
                    if (zzp instanceof String) {
                        return !((String) zzp).isEmpty();
                    }
                    if (zzp instanceof zzdp) {
                        return !zzdp.zzadh.equals(zzp);
                    }
                    throw new IllegalArgumentException();
                case 9:
                    return zzhv.zzp(t, j) != null;
                case 10:
                    return !zzdp.zzadh.equals(zzhv.zzp(t, j));
                case 11:
                    return zzhv.zzk(t, j) != 0;
                case 12:
                    return zzhv.zzk(t, j) != 0;
                case 13:
                    return zzhv.zzk(t, j) != 0;
                case 14:
                    return zzhv.zzl(t, j) != 0;
                case 15:
                    return zzhv.zzk(t, j) != 0;
                case 16:
                    return zzhv.zzl(t, j) != 0;
                case 17:
                    return zzhv.zzp(t, j) != null;
                default:
                    throw new IllegalArgumentException();
            }
        } else {
            int zzcb = zzcb(i);
            return ((1 << (zzcb >>> 20)) & zzhv.zzk(t, (long) (1048575 & zzcb))) != 0;
        }
    }

    private final boolean zza(T t, int i, int i2) {
        return zzhv.zzk(t, (long) (zzcb(i2) & 1048575)) == i;
    }

    private final boolean zza(T t, int i, int i2, int i3) {
        return this.zzakq ? zza(t, i) : (i2 & i3) != 0;
    }

    private static boolean zza(Object obj, int i, zzgx zzgx) {
        return zzgx.zzv(zzhv.zzp(obj, (long) (1048575 & i)));
    }

    private final void zzb(T t, int i) {
        if (!this.zzakq) {
            int zzcb = zzcb(i);
            long j = (long) (1048575 & zzcb);
            zzhv.zzb((Object) t, j, (1 << (zzcb >>> 20)) | zzhv.zzk(t, j));
        }
    }

    private final void zzb(T t, int i, int i2) {
        zzhv.zzb((Object) t, (long) (zzcb(i2) & 1048575), i);
    }

    /* JADX WARNING: CFG modification limit reached, blocks count: 388 */
    /* JADX WARNING: Removed duplicated region for block: B:169:0x0774  */
    /* JADX WARNING: Removed duplicated region for block: B:7:0x0033  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zzb(T r21, com.google.android.gms.internal.measurement.zzim r22) throws java.io.IOException {
        /*
            r20 = this;
            r4 = 0
            r6 = 0
            r0 = r20
            boolean r5 = r0.zzako
            if (r5 == 0) goto L_0x07a1
            r0 = r20
            com.google.android.gms.internal.measurement.zzen<?> r5 = r0.zzaky
            r0 = r21
            com.google.android.gms.internal.measurement.zzeo r5 = r5.zzh(r0)
            com.google.android.gms.internal.measurement.zzhc<FieldDescriptorType, java.lang.Object> r7 = r5.zzaex
            boolean r7 = r7.isEmpty()
            if (r7 != 0) goto L_0x07a1
            java.util.Iterator r5 = r5.iterator()
            java.lang.Object r4 = r5.next()
            java.util.Map$Entry r4 = (java.util.Map.Entry) r4
            r6 = r4
        L_0x0025:
            r0 = r20
            int[] r4 = r0.zzakj
            int r13 = r4.length
            sun.misc.Unsafe r14 = zzaki
            r4 = 0
            r7 = -1
            r8 = 0
            r9 = r6
            r12 = r4
        L_0x0031:
            if (r12 >= r13) goto L_0x079f
            r0 = r20
            int r15 = r0.zzca(r12)
            r0 = r20
            int[] r4 = r0.zzakj
            r16 = r4[r12]
            r4 = 267386880(0xff00000, float:2.3665827E-29)
            r4 = r4 & r15
            int r17 = r4 >>> 20
            r4 = 0
            r0 = r20
            boolean r6 = r0.zzakq
            if (r6 != 0) goto L_0x079a
            r6 = 17
            r0 = r17
            if (r0 > r6) goto L_0x079a
            r0 = r20
            int[] r4 = r0.zzakj
            int r6 = r12 + 2
            r10 = r4[r6]
            r4 = 1048575(0xfffff, float:1.469367E-39)
            r4 = r4 & r10
            if (r4 == r7) goto L_0x0796
            long r6 = (long) r4
            r0 = r21
            int r6 = r14.getInt(r0, r6)
        L_0x0066:
            r7 = 1
            int r8 = r10 >>> 20
            int r7 = r7 << r8
            r10 = r7
            r11 = r4
        L_0x006c:
            if (r9 == 0) goto L_0x0093
            r0 = r20
            com.google.android.gms.internal.measurement.zzen<?> r4 = r0.zzaky
            int r4 = r4.zza(r9)
            r0 = r16
            if (r4 > r0) goto L_0x0093
            r0 = r20
            com.google.android.gms.internal.measurement.zzen<?> r4 = r0.zzaky
            r0 = r22
            r4.zza(r0, r9)
            boolean r4 = r5.hasNext()
            if (r4 == 0) goto L_0x0091
            java.lang.Object r4 = r5.next()
            java.util.Map$Entry r4 = (java.util.Map.Entry) r4
        L_0x008f:
            r9 = r4
            goto L_0x006c
        L_0x0091:
            r4 = 0
            goto L_0x008f
        L_0x0093:
            r4 = 1048575(0xfffff, float:1.469367E-39)
            r4 = r4 & r15
            long r0 = (long) r4
            r18 = r0
            switch(r17) {
                case 0: goto L_0x00a3;
                case 1: goto L_0x00b9;
                case 2: goto L_0x00cd;
                case 3: goto L_0x00e3;
                case 4: goto L_0x00f9;
                case 5: goto L_0x010d;
                case 6: goto L_0x0124;
                case 7: goto L_0x0139;
                case 8: goto L_0x014e;
                case 9: goto L_0x0163;
                case 10: goto L_0x017e;
                case 11: goto L_0x0195;
                case 12: goto L_0x01aa;
                case 13: goto L_0x01bf;
                case 14: goto L_0x01d4;
                case 15: goto L_0x01eb;
                case 16: goto L_0x0200;
                case 17: goto L_0x0217;
                case 18: goto L_0x0232;
                case 19: goto L_0x024a;
                case 20: goto L_0x0262;
                case 21: goto L_0x027a;
                case 22: goto L_0x0292;
                case 23: goto L_0x02aa;
                case 24: goto L_0x02c2;
                case 25: goto L_0x02da;
                case 26: goto L_0x02f2;
                case 27: goto L_0x0309;
                case 28: goto L_0x0326;
                case 29: goto L_0x033d;
                case 30: goto L_0x0355;
                case 31: goto L_0x036d;
                case 32: goto L_0x0385;
                case 33: goto L_0x039d;
                case 34: goto L_0x03b5;
                case 35: goto L_0x03cd;
                case 36: goto L_0x03e5;
                case 37: goto L_0x03fd;
                case 38: goto L_0x0415;
                case 39: goto L_0x042d;
                case 40: goto L_0x0445;
                case 41: goto L_0x045d;
                case 42: goto L_0x0475;
                case 43: goto L_0x048d;
                case 44: goto L_0x04a5;
                case 45: goto L_0x04bd;
                case 46: goto L_0x04d5;
                case 47: goto L_0x04ed;
                case 48: goto L_0x0505;
                case 49: goto L_0x051d;
                case 50: goto L_0x053a;
                case 51: goto L_0x054d;
                case 52: goto L_0x056c;
                case 53: goto L_0x0589;
                case 54: goto L_0x05a8;
                case 55: goto L_0x05c7;
                case 56: goto L_0x05e4;
                case 57: goto L_0x0603;
                case 58: goto L_0x0620;
                case 59: goto L_0x063d;
                case 60: goto L_0x065a;
                case 61: goto L_0x067d;
                case 62: goto L_0x069c;
                case 63: goto L_0x06b9;
                case 64: goto L_0x06d6;
                case 65: goto L_0x06f3;
                case 66: goto L_0x0712;
                case 67: goto L_0x072f;
                case 68: goto L_0x074e;
                default: goto L_0x009d;
            }
        L_0x009d:
            int r4 = r12 + 3
            r12 = r4
            r7 = r11
            r8 = r6
            goto L_0x0031
        L_0x00a3:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            double r18 = com.google.android.gms.internal.measurement.zzhv.zzo(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zza(r1, r2)
            goto L_0x009d
        L_0x00b9:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            float r4 = com.google.android.gms.internal.measurement.zzhv.zzn(r0, r1)
            r0 = r22
            r1 = r16
            r0.zza(r1, r4)
            goto L_0x009d
        L_0x00cd:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = r14.getLong(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzi(r1, r2)
            goto L_0x009d
        L_0x00e3:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = r14.getLong(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zza(r1, r2)
            goto L_0x009d
        L_0x00f9:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzc(r1, r4)
            goto L_0x009d
        L_0x010d:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = r14.getLong(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzc(r1, r2)
            goto L_0x009d
        L_0x0124:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzf(r1, r4)
            goto L_0x009d
        L_0x0139:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            boolean r4 = com.google.android.gms.internal.measurement.zzhv.zzm(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzb(r1, r4)
            goto L_0x009d
        L_0x014e:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r16
            r1 = r22
            zza(r0, r4, r1)
            goto L_0x009d
        L_0x0163:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r7 = r0.zzbx(r12)
            r0 = r22
            r1 = r16
            r0.zza(r1, r4, r7)
            goto L_0x009d
        L_0x017e:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            com.google.android.gms.internal.measurement.zzdp r4 = (com.google.android.gms.internal.measurement.zzdp) r4
            r0 = r22
            r1 = r16
            r0.zza(r1, r4)
            goto L_0x009d
        L_0x0195:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzd(r1, r4)
            goto L_0x009d
        L_0x01aa:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzn(r1, r4)
            goto L_0x009d
        L_0x01bf:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzm(r1, r4)
            goto L_0x009d
        L_0x01d4:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = r14.getLong(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzj(r1, r2)
            goto L_0x009d
        L_0x01eb:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = r14.getInt(r0, r1)
            r0 = r22
            r1 = r16
            r0.zze(r1, r4)
            goto L_0x009d
        L_0x0200:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = r14.getLong(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzb(r1, r2)
            goto L_0x009d
        L_0x0217:
            r4 = r6 & r10
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r7 = r0.zzbx(r12)
            r0 = r22
            r1 = r16
            r0.zzb(r1, r4, r7)
            goto L_0x009d
        L_0x0232:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zza(r7, r4, r0, r8)
            goto L_0x009d
        L_0x024a:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzb(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0262:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzc(r7, r4, r0, r8)
            goto L_0x009d
        L_0x027a:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzd(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0292:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzh(r7, r4, r0, r8)
            goto L_0x009d
        L_0x02aa:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzf(r7, r4, r0, r8)
            goto L_0x009d
        L_0x02c2:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzk(r7, r4, r0, r8)
            goto L_0x009d
        L_0x02da:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzn(r7, r4, r0, r8)
            goto L_0x009d
        L_0x02f2:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zza(r7, r4, r0)
            goto L_0x009d
        L_0x0309:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r8 = r0.zzbx(r12)
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zza(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0326:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzb(r7, r4, r0)
            goto L_0x009d
        L_0x033d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzi(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0355:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzm(r7, r4, r0, r8)
            goto L_0x009d
        L_0x036d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzl(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0385:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzg(r7, r4, r0, r8)
            goto L_0x009d
        L_0x039d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzj(r7, r4, r0, r8)
            goto L_0x009d
        L_0x03b5:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 0
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zze(r7, r4, r0, r8)
            goto L_0x009d
        L_0x03cd:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zza(r7, r4, r0, r8)
            goto L_0x009d
        L_0x03e5:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzb(r7, r4, r0, r8)
            goto L_0x009d
        L_0x03fd:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzc(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0415:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzd(r7, r4, r0, r8)
            goto L_0x009d
        L_0x042d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzh(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0445:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzf(r7, r4, r0, r8)
            goto L_0x009d
        L_0x045d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzk(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0475:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzn(r7, r4, r0, r8)
            goto L_0x009d
        L_0x048d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzi(r7, r4, r0, r8)
            goto L_0x009d
        L_0x04a5:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzm(r7, r4, r0, r8)
            goto L_0x009d
        L_0x04bd:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzl(r7, r4, r0, r8)
            goto L_0x009d
        L_0x04d5:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzg(r7, r4, r0, r8)
            goto L_0x009d
        L_0x04ed:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzj(r7, r4, r0, r8)
            goto L_0x009d
        L_0x0505:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r8 = 1
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zze(r7, r4, r0, r8)
            goto L_0x009d
        L_0x051d:
            r0 = r20
            int[] r4 = r0.zzakj
            r7 = r4[r12]
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            java.util.List r4 = (java.util.List) r4
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r8 = r0.zzbx(r12)
            r0 = r22
            com.google.android.gms.internal.measurement.zzgz.zzb(r7, r4, r0, r8)
            goto L_0x009d
        L_0x053a:
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r20
            r1 = r22
            r2 = r16
            r0.zza(r1, r2, r4, r12)
            goto L_0x009d
        L_0x054d:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            double r18 = zzf(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zza(r1, r2)
            goto L_0x009d
        L_0x056c:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            float r4 = zzg(r0, r1)
            r0 = r22
            r1 = r16
            r0.zza(r1, r4)
            goto L_0x009d
        L_0x0589:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = zzi(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzi(r1, r2)
            goto L_0x009d
        L_0x05a8:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = zzi(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zza(r1, r2)
            goto L_0x009d
        L_0x05c7:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzc(r1, r4)
            goto L_0x009d
        L_0x05e4:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = zzi(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzc(r1, r2)
            goto L_0x009d
        L_0x0603:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzf(r1, r4)
            goto L_0x009d
        L_0x0620:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            boolean r4 = zzj(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzb(r1, r4)
            goto L_0x009d
        L_0x063d:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r16
            r1 = r22
            zza(r0, r4, r1)
            goto L_0x009d
        L_0x065a:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r7 = r0.zzbx(r12)
            r0 = r22
            r1 = r16
            r0.zza(r1, r4, r7)
            goto L_0x009d
        L_0x067d:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            com.google.android.gms.internal.measurement.zzdp r4 = (com.google.android.gms.internal.measurement.zzdp) r4
            r0 = r22
            r1 = r16
            r0.zza(r1, r4)
            goto L_0x009d
        L_0x069c:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzd(r1, r4)
            goto L_0x009d
        L_0x06b9:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzn(r1, r4)
            goto L_0x009d
        L_0x06d6:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zzm(r1, r4)
            goto L_0x009d
        L_0x06f3:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = zzi(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzj(r1, r2)
            goto L_0x009d
        L_0x0712:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            int r4 = zzh(r0, r1)
            r0 = r22
            r1 = r16
            r0.zze(r1, r4)
            goto L_0x009d
        L_0x072f:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            long r18 = zzi(r0, r1)
            r0 = r22
            r1 = r16
            r2 = r18
            r0.zzb(r1, r2)
            goto L_0x009d
        L_0x074e:
            r0 = r20
            r1 = r21
            r2 = r16
            boolean r4 = r0.zza((T) r1, r2, r12)
            if (r4 == 0) goto L_0x009d
            r0 = r21
            r1 = r18
            java.lang.Object r4 = r14.getObject(r0, r1)
            r0 = r20
            com.google.android.gms.internal.measurement.zzgx r7 = r0.zzbx(r12)
            r0 = r22
            r1 = r16
            r0.zzb(r1, r4, r7)
            goto L_0x009d
        L_0x0771:
            r4 = 0
        L_0x0772:
            if (r4 == 0) goto L_0x078a
            r0 = r20
            com.google.android.gms.internal.measurement.zzen<?> r6 = r0.zzaky
            r0 = r22
            r6.zza(r0, r4)
            boolean r4 = r5.hasNext()
            if (r4 == 0) goto L_0x0771
            java.lang.Object r4 = r5.next()
            java.util.Map$Entry r4 = (java.util.Map.Entry) r4
            goto L_0x0772
        L_0x078a:
            r0 = r20
            com.google.android.gms.internal.measurement.zzhp<?, ?> r4 = r0.zzakx
            r0 = r21
            r1 = r22
            zza(r4, (T) r0, r1)
            return
        L_0x0796:
            r4 = r7
            r6 = r8
            goto L_0x0066
        L_0x079a:
            r10 = r4
            r6 = r8
            r11 = r7
            goto L_0x006c
        L_0x079f:
            r4 = r9
            goto L_0x0772
        L_0x07a1:
            r5 = r4
            goto L_0x0025
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zzb(java.lang.Object, com.google.android.gms.internal.measurement.zzim):void");
    }

    private final void zzb(T t, T t2, int i) {
        int zzca = zzca(i);
        int i2 = this.zzakj[i];
        long j = (long) (zzca & 1048575);
        if (zza(t2, i2, i)) {
            Object zzp = zzhv.zzp(t, j);
            Object zzp2 = zzhv.zzp(t2, j);
            if (zzp != null && zzp2 != null) {
                zzhv.zza((Object) t, j, zzez.zza(zzp, zzp2));
                zzb(t, i2, i);
            } else if (zzp2 != null) {
                zzhv.zza((Object) t, j, zzp2);
                zzb(t, i2, i);
            }
        }
    }

    private final zzgx zzbx(int i) {
        int i2 = (i / 3) << 1;
        zzgx zzgx = (zzgx) this.zzakk[i2];
        if (zzgx != null) {
            return zzgx;
        }
        zzgx zzf = zzgt.zzvy().zzf((Class) this.zzakk[i2 + 1]);
        this.zzakk[i2] = zzf;
        return zzf;
    }

    private final Object zzby(int i) {
        return this.zzakk[(i / 3) << 1];
    }

    private final zzfe zzbz(int i) {
        return (zzfe) this.zzakk[((i / 3) << 1) + 1];
    }

    private final boolean zzc(T t, T t2, int i) {
        return zza(t, i) == zza(t2, i);
    }

    private final int zzca(int i) {
        return this.zzakj[i + 1];
    }

    private final int zzcb(int i) {
        return this.zzakj[i + 2];
    }

    private static boolean zzcc(int i) {
        return (536870912 & i) != 0;
    }

    private final int zzcd(int i) {
        if (i < this.zzakl || i > this.zzakm) {
            return -1;
        }
        return zzq(i, 0);
    }

    private static List<?> zze(Object obj, long j) {
        return (List) zzhv.zzp(obj, j);
    }

    private static <T> double zzf(T t, long j) {
        return ((Double) zzhv.zzp(t, j)).doubleValue();
    }

    private static <T> float zzg(T t, long j) {
        return ((Float) zzhv.zzp(t, j)).floatValue();
    }

    private static <T> int zzh(T t, long j) {
        return ((Integer) zzhv.zzp(t, j)).intValue();
    }

    private static <T> long zzi(T t, long j) {
        return ((Long) zzhv.zzp(t, j)).longValue();
    }

    private static <T> boolean zzj(T t, long j) {
        return ((Boolean) zzhv.zzp(t, j)).booleanValue();
    }

    private final int zzp(int i, int i2) {
        if (i < this.zzakl || i > this.zzakm) {
            return -1;
        }
        return zzq(i, i2);
    }

    private final int zzq(int i, int i2) {
        int length = (this.zzakj.length / 3) - 1;
        while (i2 <= length) {
            int i3 = (length + i2) >>> 1;
            int i4 = i3 * 3;
            int i5 = this.zzakj[i4];
            if (i == i5) {
                return i4;
            }
            if (i < i5) {
                length = i3 - 1;
            } else {
                i2 = i3 + 1;
            }
        }
        return -1;
    }

    private static zzhs zzu(Object obj) {
        zzhs zzhs = ((zzey) obj).zzahz;
        if (zzhs != zzhs.zzwq()) {
            return zzhs;
        }
        zzhs zzwr = zzhs.zzwr();
        ((zzey) obj).zzahz = zzwr;
        return zzwr;
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean equals(T r12, T r13) {
        /*
            r11 = this;
            r1 = 1
            r10 = 1048575(0xfffff, float:1.469367E-39)
            r0 = 0
            int[] r2 = r11.zzakj
            int r4 = r2.length
            r3 = r0
        L_0x0009:
            if (r3 >= r4) goto L_0x01e0
            int r2 = r11.zzca(r3)
            r5 = r2 & r10
            long r6 = (long) r5
            r5 = 267386880(0xff00000, float:2.3665827E-29)
            r2 = r2 & r5
            int r2 = r2 >>> 20
            switch(r2) {
                case 0: goto L_0x001e;
                case 1: goto L_0x003a;
                case 2: goto L_0x0054;
                case 3: goto L_0x0068;
                case 4: goto L_0x007c;
                case 5: goto L_0x008e;
                case 6: goto L_0x00a3;
                case 7: goto L_0x00b6;
                case 8: goto L_0x00c9;
                case 9: goto L_0x00e0;
                case 10: goto L_0x00f7;
                case 11: goto L_0x010e;
                case 12: goto L_0x0121;
                case 13: goto L_0x0134;
                case 14: goto L_0x0147;
                case 15: goto L_0x015c;
                case 16: goto L_0x016f;
                case 17: goto L_0x0184;
                case 18: goto L_0x019b;
                case 19: goto L_0x019b;
                case 20: goto L_0x019b;
                case 21: goto L_0x019b;
                case 22: goto L_0x019b;
                case 23: goto L_0x019b;
                case 24: goto L_0x019b;
                case 25: goto L_0x019b;
                case 26: goto L_0x019b;
                case 27: goto L_0x019b;
                case 28: goto L_0x019b;
                case 29: goto L_0x019b;
                case 30: goto L_0x019b;
                case 31: goto L_0x019b;
                case 32: goto L_0x019b;
                case 33: goto L_0x019b;
                case 34: goto L_0x019b;
                case 35: goto L_0x019b;
                case 36: goto L_0x019b;
                case 37: goto L_0x019b;
                case 38: goto L_0x019b;
                case 39: goto L_0x019b;
                case 40: goto L_0x019b;
                case 41: goto L_0x019b;
                case 42: goto L_0x019b;
                case 43: goto L_0x019b;
                case 44: goto L_0x019b;
                case 45: goto L_0x019b;
                case 46: goto L_0x019b;
                case 47: goto L_0x019b;
                case 48: goto L_0x019b;
                case 49: goto L_0x019b;
                case 50: goto L_0x01a9;
                case 51: goto L_0x01b7;
                case 52: goto L_0x01b7;
                case 53: goto L_0x01b7;
                case 54: goto L_0x01b7;
                case 55: goto L_0x01b7;
                case 56: goto L_0x01b7;
                case 57: goto L_0x01b7;
                case 58: goto L_0x01b7;
                case 59: goto L_0x01b7;
                case 60: goto L_0x01b7;
                case 61: goto L_0x01b7;
                case 62: goto L_0x01b7;
                case 63: goto L_0x01b7;
                case 64: goto L_0x01b7;
                case 65: goto L_0x01b7;
                case 66: goto L_0x01b7;
                case 67: goto L_0x01b7;
                case 68: goto L_0x01b7;
                default: goto L_0x001a;
            }
        L_0x001a:
            r2 = r1
        L_0x001b:
            if (r2 != 0) goto L_0x01db
        L_0x001d:
            return r0
        L_0x001e:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0038
            double r8 = com.google.android.gms.internal.measurement.zzhv.zzo(r12, r6)
            long r8 = java.lang.Double.doubleToLongBits(r8)
            double r6 = com.google.android.gms.internal.measurement.zzhv.zzo(r13, r6)
            long r6 = java.lang.Double.doubleToLongBits(r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x0038:
            r2 = r0
            goto L_0x001b
        L_0x003a:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0052
            float r2 = com.google.android.gms.internal.measurement.zzhv.zzn(r12, r6)
            int r2 = java.lang.Float.floatToIntBits(r2)
            float r5 = com.google.android.gms.internal.measurement.zzhv.zzn(r13, r6)
            int r5 = java.lang.Float.floatToIntBits(r5)
            if (r2 == r5) goto L_0x001a
        L_0x0052:
            r2 = r0
            goto L_0x001b
        L_0x0054:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0066
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r12, r6)
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r13, r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x0066:
            r2 = r0
            goto L_0x001b
        L_0x0068:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x007a
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r12, r6)
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r13, r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x007a:
            r2 = r0
            goto L_0x001b
        L_0x007c:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x008c
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x008c:
            r2 = r0
            goto L_0x001b
        L_0x008e:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x00a0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r12, r6)
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r13, r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x00a0:
            r2 = r0
            goto L_0x001b
        L_0x00a3:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x00b3
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x00b3:
            r2 = r0
            goto L_0x001b
        L_0x00b6:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x00c6
            boolean r2 = com.google.android.gms.internal.measurement.zzhv.zzm(r12, r6)
            boolean r5 = com.google.android.gms.internal.measurement.zzhv.zzm(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x00c6:
            r2 = r0
            goto L_0x001b
        L_0x00c9:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x00dd
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            if (r2 != 0) goto L_0x001a
        L_0x00dd:
            r2 = r0
            goto L_0x001b
        L_0x00e0:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x00f4
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            if (r2 != 0) goto L_0x001a
        L_0x00f4:
            r2 = r0
            goto L_0x001b
        L_0x00f7:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x010b
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            if (r2 != 0) goto L_0x001a
        L_0x010b:
            r2 = r0
            goto L_0x001b
        L_0x010e:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x011e
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x011e:
            r2 = r0
            goto L_0x001b
        L_0x0121:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0131
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x0131:
            r2 = r0
            goto L_0x001b
        L_0x0134:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0144
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x0144:
            r2 = r0
            goto L_0x001b
        L_0x0147:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0159
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r12, r6)
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r13, r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x0159:
            r2 = r0
            goto L_0x001b
        L_0x015c:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x016c
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r6)
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r6)
            if (r2 == r5) goto L_0x001a
        L_0x016c:
            r2 = r0
            goto L_0x001b
        L_0x016f:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0181
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r12, r6)
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r13, r6)
            int r2 = (r8 > r6 ? 1 : (r8 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x001a
        L_0x0181:
            r2 = r0
            goto L_0x001b
        L_0x0184:
            boolean r2 = r11.zzc(r12, r13, r3)
            if (r2 == 0) goto L_0x0198
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            if (r2 != 0) goto L_0x001a
        L_0x0198:
            r2 = r0
            goto L_0x001b
        L_0x019b:
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            goto L_0x001b
        L_0x01a9:
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            goto L_0x001b
        L_0x01b7:
            int r2 = r11.zzcb(r3)
            r5 = r2 & r10
            long r8 = (long) r5
            int r5 = com.google.android.gms.internal.measurement.zzhv.zzk(r12, r8)
            r2 = r2 & r10
            long r8 = (long) r2
            int r2 = com.google.android.gms.internal.measurement.zzhv.zzk(r13, r8)
            if (r5 != r2) goto L_0x01d8
            java.lang.Object r2 = com.google.android.gms.internal.measurement.zzhv.zzp(r12, r6)
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r13, r6)
            boolean r2 = com.google.android.gms.internal.measurement.zzgz.zzd(r2, r5)
            if (r2 != 0) goto L_0x001a
        L_0x01d8:
            r2 = r0
            goto L_0x001b
        L_0x01db:
            int r2 = r3 + 3
            r3 = r2
            goto L_0x0009
        L_0x01e0:
            com.google.android.gms.internal.measurement.zzhp<?, ?> r2 = r11.zzakx
            java.lang.Object r2 = r2.zzx(r12)
            com.google.android.gms.internal.measurement.zzhp<?, ?> r3 = r11.zzakx
            java.lang.Object r3 = r3.zzx(r13)
            boolean r2 = r2.equals(r3)
            if (r2 == 0) goto L_0x001d
            boolean r0 = r11.zzako
            if (r0 == 0) goto L_0x0208
            com.google.android.gms.internal.measurement.zzen<?> r0 = r11.zzaky
            com.google.android.gms.internal.measurement.zzeo r0 = r0.zzh(r12)
            com.google.android.gms.internal.measurement.zzen<?> r1 = r11.zzaky
            com.google.android.gms.internal.measurement.zzeo r1 = r1.zzh(r13)
            boolean r0 = r0.equals(r1)
            goto L_0x001d
        L_0x0208:
            r0 = r1
            goto L_0x001d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.equals(java.lang.Object, java.lang.Object):boolean");
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final int hashCode(T r10) {
        /*
            r9 = this;
            r2 = 37
            r0 = 0
            int[] r1 = r9.zzakj
            int r4 = r1.length
            r3 = r0
            r1 = r0
        L_0x0008:
            if (r3 >= r4) goto L_0x0254
            int r0 = r9.zzca(r3)
            int[] r5 = r9.zzakj
            r5 = r5[r3]
            r6 = 1048575(0xfffff, float:1.469367E-39)
            r6 = r6 & r0
            long r6 = (long) r6
            r8 = 267386880(0xff00000, float:2.3665827E-29)
            r0 = r0 & r8
            int r0 = r0 >>> 20
            switch(r0) {
                case 0: goto L_0x0024;
                case 1: goto L_0x0034;
                case 2: goto L_0x0040;
                case 3: goto L_0x004c;
                case 4: goto L_0x0058;
                case 5: goto L_0x0060;
                case 6: goto L_0x006c;
                case 7: goto L_0x0074;
                case 8: goto L_0x0080;
                case 9: goto L_0x008e;
                case 10: goto L_0x009c;
                case 11: goto L_0x00a9;
                case 12: goto L_0x00b2;
                case 13: goto L_0x00bb;
                case 14: goto L_0x00c4;
                case 15: goto L_0x00d1;
                case 16: goto L_0x00da;
                case 17: goto L_0x00e7;
                case 18: goto L_0x00f6;
                case 19: goto L_0x00f6;
                case 20: goto L_0x00f6;
                case 21: goto L_0x00f6;
                case 22: goto L_0x00f6;
                case 23: goto L_0x00f6;
                case 24: goto L_0x00f6;
                case 25: goto L_0x00f6;
                case 26: goto L_0x00f6;
                case 27: goto L_0x00f6;
                case 28: goto L_0x00f6;
                case 29: goto L_0x00f6;
                case 30: goto L_0x00f6;
                case 31: goto L_0x00f6;
                case 32: goto L_0x00f6;
                case 33: goto L_0x00f6;
                case 34: goto L_0x00f6;
                case 35: goto L_0x00f6;
                case 36: goto L_0x00f6;
                case 37: goto L_0x00f6;
                case 38: goto L_0x00f6;
                case 39: goto L_0x00f6;
                case 40: goto L_0x00f6;
                case 41: goto L_0x00f6;
                case 42: goto L_0x00f6;
                case 43: goto L_0x00f6;
                case 44: goto L_0x00f6;
                case 45: goto L_0x00f6;
                case 46: goto L_0x00f6;
                case 47: goto L_0x00f6;
                case 48: goto L_0x00f6;
                case 49: goto L_0x00f6;
                case 50: goto L_0x0103;
                case 51: goto L_0x0110;
                case 52: goto L_0x0127;
                case 53: goto L_0x013a;
                case 54: goto L_0x014d;
                case 55: goto L_0x0160;
                case 56: goto L_0x016f;
                case 57: goto L_0x0182;
                case 58: goto L_0x0191;
                case 59: goto L_0x01a4;
                case 60: goto L_0x01b9;
                case 61: goto L_0x01cc;
                case 62: goto L_0x01df;
                case 63: goto L_0x01ee;
                case 64: goto L_0x01fd;
                case 65: goto L_0x020c;
                case 66: goto L_0x021f;
                case 67: goto L_0x022e;
                case 68: goto L_0x0241;
                default: goto L_0x001f;
            }
        L_0x001f:
            r0 = r1
        L_0x0020:
            int r3 = r3 + 3
            r1 = r0
            goto L_0x0008
        L_0x0024:
            int r0 = r1 * 53
            double r6 = com.google.android.gms.internal.measurement.zzhv.zzo(r10, r6)
            long r6 = java.lang.Double.doubleToLongBits(r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0034:
            int r0 = r1 * 53
            float r1 = com.google.android.gms.internal.measurement.zzhv.zzn(r10, r6)
            int r1 = java.lang.Float.floatToIntBits(r1)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0040:
            int r0 = r1 * 53
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x004c:
            int r0 = r1 * 53
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0058:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0060:
            int r0 = r1 * 53
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x006c:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0074:
            int r0 = r1 * 53
            boolean r1 = com.google.android.gms.internal.measurement.zzhv.zzm(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzs(r1)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0080:
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            java.lang.String r0 = (java.lang.String) r0
            int r0 = r0.hashCode()
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x008e:
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            if (r0 == 0) goto L_0x0276
            int r0 = r0.hashCode()
        L_0x0098:
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x009c:
            int r0 = r1 * 53
            java.lang.Object r1 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00a9:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00b2:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00bb:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00c4:
            int r0 = r1 * 53
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00d1:
            int r0 = r1 * 53
            int r1 = com.google.android.gms.internal.measurement.zzhv.zzk(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00da:
            int r0 = r1 * 53
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00e7:
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            if (r0 == 0) goto L_0x0273
            int r0 = r0.hashCode()
        L_0x00f1:
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x00f6:
            int r0 = r1 * 53
            java.lang.Object r1 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0103:
            int r0 = r1 * 53
            java.lang.Object r1 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0110:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            double r6 = zzf(r10, r6)
            long r6 = java.lang.Double.doubleToLongBits(r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0127:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            float r1 = zzg(r10, r6)
            int r1 = java.lang.Float.floatToIntBits(r1)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x013a:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            long r6 = zzi(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x014d:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            long r6 = zzi(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0160:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x016f:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            long r6 = zzi(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0182:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0191:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            boolean r1 = zzj(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzs(r1)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01a4:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            java.lang.String r0 = (java.lang.String) r0
            int r0 = r0.hashCode()
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01b9:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r0 = r0.hashCode()
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01cc:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            java.lang.Object r1 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01df:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01ee:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x01fd:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x020c:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            long r6 = zzi(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x021f:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            int r1 = zzh(r10, r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x022e:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            int r0 = r1 * 53
            long r6 = zzi(r10, r6)
            int r1 = com.google.android.gms.internal.measurement.zzez.zzbx(r6)
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0241:
            boolean r0 = r9.zza((T) r10, r5, r3)
            if (r0 == 0) goto L_0x001f
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r10, r6)
            int r0 = r0.hashCode()
            int r1 = r1 * 53
            int r0 = r0 + r1
            goto L_0x0020
        L_0x0254:
            int r0 = r1 * 53
            com.google.android.gms.internal.measurement.zzhp<?, ?> r1 = r9.zzakx
            java.lang.Object r1 = r1.zzx(r10)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
            boolean r1 = r9.zzako
            if (r1 == 0) goto L_0x0272
            int r0 = r0 * 53
            com.google.android.gms.internal.measurement.zzen<?> r1 = r9.zzaky
            com.google.android.gms.internal.measurement.zzeo r1 = r1.zzh(r10)
            int r1 = r1.hashCode()
            int r0 = r0 + r1
        L_0x0272:
            return r0
        L_0x0273:
            r0 = r2
            goto L_0x00f1
        L_0x0276:
            r0 = r2
            goto L_0x0098
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.hashCode(java.lang.Object):int");
    }

    public final T newInstance() {
        return this.zzakv.newInstance(this.zzakn);
    }

    /* JADX WARNING: type inference failed for: r4v14, types: [int] */
    /* JADX WARNING: type inference failed for: r31v0, types: [int] */
    /* JADX WARNING: type inference failed for: r19v0, types: [int] */
    /* JADX WARNING: type inference failed for: r10v4, types: [int] */
    /* JADX WARNING: type inference failed for: r7v37 */
    /* JADX WARNING: type inference failed for: r4v20 */
    /* JADX WARNING: type inference failed for: r4v21 */
    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final int zza(T r44, byte[] r45, int r46, int r47, int r48, com.google.android.gms.internal.measurement.zzdk r49) throws java.io.IOException {
        /*
            r43 = this;
            sun.misc.Unsafe r42 = zzaki
            r40 = -1
            r41 = 0
            r4 = 0
            r22 = 0
            r20 = -1
            r7 = r20
            r6 = r46
        L_0x000f:
            r0 = r47
            if (r6 >= r0) goto L_0x0444
            int r12 = r6 + 1
            byte r4 = r45[r6]
            if (r4 >= 0) goto L_0x0025
            r0 = r45
            r1 = r49
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r0, r12, r1)
            r0 = r49
            int r4 = r0.zzada
        L_0x0025:
            int r20 = r4 >>> 3
            r21 = r4 & 7
            r0 = r20
            if (r0 <= r7) goto L_0x007f
            int r5 = r22 / 3
            r0 = r43
            r1 = r20
            int r22 = r0.zzp(r1, r5)
        L_0x0037:
            r5 = -1
            r0 = r22
            if (r0 != r5) goto L_0x0088
            r22 = 0
            r6 = r12
            r10 = r40
            r11 = r41
        L_0x0043:
            r0 = r48
            if (r4 != r0) goto L_0x0049
            if (r48 != 0) goto L_0x03df
        L_0x0049:
            r0 = r43
            boolean r5 = r0.zzako
            if (r5 == 0) goto L_0x03c8
            r0 = r49
            com.google.android.gms.internal.measurement.zzel r5 = r0.zzadd
            com.google.android.gms.internal.measurement.zzel r7 = com.google.android.gms.internal.measurement.zzel.zztp()
            if (r5 == r7) goto L_0x03c8
            r0 = r43
            com.google.android.gms.internal.measurement.zzgi r5 = r0.zzakn
            r0 = r49
            com.google.android.gms.internal.measurement.zzel r7 = r0.zzadd
            int r8 = r4 >>> 3
            com.google.android.gms.internal.measurement.zzey$zze r5 = r7.zza(r5, r8)
            if (r5 != 0) goto L_0x03b5
            com.google.android.gms.internal.measurement.zzhs r8 = zzu(r44)
            r5 = r45
            r7 = r47
            r9 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r5, r6, r7, r8, r9)
            r40 = r10
            r7 = r20
            r6 = r5
            r41 = r11
            goto L_0x000f
        L_0x007f:
            r0 = r43
            r1 = r20
            int r22 = r0.zzcd(r1)
            goto L_0x0037
        L_0x0088:
            r0 = r43
            int[] r5 = r0.zzakj
            int r6 = r22 + 1
            r34 = r5[r6]
            r5 = 267386880(0xff00000, float:2.3665827E-29)
            r5 = r5 & r34
            int r25 = r5 >>> 20
            r5 = 1048575(0xfffff, float:1.469367E-39)
            r5 = r5 & r34
            long r8 = (long) r5
            r5 = 17
            r0 = r25
            if (r0 > r5) goto L_0x02f7
            r0 = r43
            int[] r5 = r0.zzakj
            int r6 = r22 + 2
            r5 = r5[r6]
            r6 = 1
            int r7 = r5 >>> 20
            int r16 = r6 << r7
            r6 = 1048575(0xfffff, float:1.469367E-39)
            r5 = r5 & r6
            r0 = r40
            if (r5 == r0) goto L_0x00d3
            r6 = -1
            r0 = r40
            if (r0 == r6) goto L_0x00c8
            r0 = r40
            long r6 = (long) r0
            r0 = r42
            r1 = r44
            r2 = r41
            r0.putInt(r1, r6, r2)
        L_0x00c8:
            long r6 = (long) r5
            r0 = r42
            r1 = r44
            int r41 = r0.getInt(r1, r6)
            r40 = r5
        L_0x00d3:
            switch(r25) {
                case 0: goto L_0x00dd;
                case 1: goto L_0x00f6;
                case 2: goto L_0x010f;
                case 3: goto L_0x010f;
                case 4: goto L_0x012b;
                case 5: goto L_0x0147;
                case 6: goto L_0x0162;
                case 7: goto L_0x017d;
                case 8: goto L_0x019f;
                case 9: goto L_0x01cd;
                case 10: goto L_0x0212;
                case 11: goto L_0x012b;
                case 12: goto L_0x0231;
                case 13: goto L_0x0162;
                case 14: goto L_0x0147;
                case 15: goto L_0x026e;
                case 16: goto L_0x028e;
                case 17: goto L_0x02ae;
                default: goto L_0x00d6;
            }
        L_0x00d6:
            r6 = r12
            r10 = r40
            r11 = r41
            goto L_0x0043
        L_0x00dd:
            r5 = 1
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r45
            double r6 = com.google.android.gms.internal.measurement.zzdl.zzc(r0, r12)
            r0 = r44
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r8, r6)
            int r5 = r12 + 8
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x00f6:
            r5 = 5
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r45
            float r5 = com.google.android.gms.internal.measurement.zzdl.zzd(r0, r12)
            r0 = r44
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r8, r5)
            int r5 = r12 + 4
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x010f:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r12, r1)
            r0 = r49
            long r10 = r0.zzadb
            r6 = r42
            r7 = r44
            r6.putLong(r7, r8, r10)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x012b:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r12, r1)
            r0 = r49
            int r6 = r0.zzada
            r0 = r42
            r1 = r44
            r0.putInt(r1, r8, r6)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x0147:
            r5 = 1
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r45
            long r10 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r12)
            r6 = r42
            r7 = r44
            r6.putLong(r7, r8, r10)
            int r5 = r12 + 8
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x0162:
            r5 = 5
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r45
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r12)
            r0 = r42
            r1 = r44
            r0.putInt(r1, r8, r5)
            int r5 = r12 + 4
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x017d:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r6 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r12, r1)
            r0 = r49
            long r10 = r0.zzadb
            r12 = 0
            int r5 = (r10 > r12 ? 1 : (r10 == r12 ? 0 : -1))
            if (r5 == 0) goto L_0x019d
            r5 = 1
        L_0x0192:
            r0 = r44
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r8, r5)
            r41 = r41 | r16
            r7 = r20
            goto L_0x000f
        L_0x019d:
            r5 = 0
            goto L_0x0192
        L_0x019f:
            r5 = 2
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r5 = 536870912(0x20000000, float:1.0842022E-19)
            r5 = r5 & r34
            if (r5 != 0) goto L_0x01c4
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zzc(r0, r12, r1)
        L_0x01b2:
            r0 = r49
            java.lang.Object r6 = r0.zzadc
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x01c4:
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zzd(r0, r12, r1)
            goto L_0x01b2
        L_0x01cd:
            r5 = 2
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r43
            r1 = r22
            com.google.android.gms.internal.measurement.zzgx r5 = r0.zzbx(r1)
            r0 = r45
            r1 = r47
            r2 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r5, r0, r12, r1, r2)
            r6 = r41 & r16
            if (r6 != 0) goto L_0x01fa
            r0 = r49
            java.lang.Object r6 = r0.zzadc
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
        L_0x01f3:
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x01fa:
            r0 = r42
            r1 = r44
            java.lang.Object r6 = r0.getObject(r1, r8)
            r0 = r49
            java.lang.Object r7 = r0.zzadc
            java.lang.Object r6 = com.google.android.gms.internal.measurement.zzez.zza(r6, r7)
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
            goto L_0x01f3
        L_0x0212:
            r5 = 2
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zze(r0, r12, r1)
            r0 = r49
            java.lang.Object r6 = r0.zzadc
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x0231:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r12, r1)
            r0 = r49
            int r6 = r0.zzada
            r0 = r43
            r1 = r22
            com.google.android.gms.internal.measurement.zzfe r7 = r0.zzbz(r1)
            if (r7 == 0) goto L_0x024f
            boolean r7 = r7.zzg(r6)
            if (r7 == 0) goto L_0x025d
        L_0x024f:
            r0 = r42
            r1 = r44
            r0.putInt(r1, r8, r6)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x025d:
            com.google.android.gms.internal.measurement.zzhs r7 = zzu(r44)
            long r8 = (long) r6
            java.lang.Long r6 = java.lang.Long.valueOf(r8)
            r7.zzb(r4, r6)
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x026e:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r12, r1)
            r0 = r49
            int r6 = r0.zzada
            int r6 = com.google.android.gms.internal.measurement.zzeb.zzaz(r6)
            r0 = r42
            r1 = r44
            r0.putInt(r1, r8, r6)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x028e:
            if (r21 != 0) goto L_0x00d6
            r0 = r45
            r1 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r12, r1)
            r0 = r49
            long r6 = r0.zzadb
            long r10 = com.google.android.gms.internal.measurement.zzeb.zzbm(r6)
            r6 = r42
            r7 = r44
            r6.putLong(r7, r8, r10)
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x02ae:
            r5 = 3
            r0 = r21
            if (r0 != r5) goto L_0x00d6
            r0 = r43
            r1 = r22
            com.google.android.gms.internal.measurement.zzgx r10 = r0.zzbx(r1)
            int r5 = r20 << 3
            r14 = r5 | 4
            r11 = r45
            r13 = r47
            r15 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r10, r11, r12, r13, r14, r15)
            r6 = r41 & r16
            if (r6 != 0) goto L_0x02df
            r0 = r49
            java.lang.Object r6 = r0.zzadc
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
        L_0x02d8:
            r41 = r41 | r16
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x02df:
            r0 = r42
            r1 = r44
            java.lang.Object r6 = r0.getObject(r1, r8)
            r0 = r49
            java.lang.Object r7 = r0.zzadc
            java.lang.Object r6 = com.google.android.gms.internal.measurement.zzez.zza(r6, r7)
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r6)
            goto L_0x02d8
        L_0x02f7:
            r5 = 27
            r0 = r25
            if (r0 != r5) goto L_0x0340
            r5 = 2
            r0 = r21
            if (r0 != r5) goto L_0x043a
            r0 = r42
            r1 = r44
            java.lang.Object r5 = r0.getObject(r1, r8)
            com.google.android.gms.internal.measurement.zzff r5 = (com.google.android.gms.internal.measurement.zzff) r5
            boolean r6 = r5.zzrx()
            if (r6 != 0) goto L_0x0441
            int r6 = r5.size()
            if (r6 != 0) goto L_0x033d
            r6 = 10
        L_0x031a:
            com.google.android.gms.internal.measurement.zzff r14 = r5.zzap(r6)
            r0 = r42
            r1 = r44
            r0.putObject(r1, r8, r14)
        L_0x0325:
            r0 = r43
            r1 = r22
            com.google.android.gms.internal.measurement.zzgx r9 = r0.zzbx(r1)
            r10 = r4
            r11 = r45
            r13 = r47
            r15 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r9, r10, r11, r12, r13, r14, r15)
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x033d:
            int r6 = r6 << 1
            goto L_0x031a
        L_0x0340:
            r5 = 49
            r0 = r25
            if (r0 > r5) goto L_0x0368
            r0 = r34
            long r0 = (long) r0
            r23 = r0
            r14 = r43
            r15 = r44
            r16 = r45
            r17 = r12
            r18 = r47
            r19 = r4
            r26 = r8
            r28 = r49
            int r5 = r14.zza((T) r15, r16, r17, r18, r19, r20, r21, r22, r23, r25, r26, r28)
            if (r5 != r12) goto L_0x0435
            r6 = r5
            r10 = r40
            r11 = r41
            goto L_0x0043
        L_0x0368:
            r5 = 50
            r0 = r25
            if (r0 != r5) goto L_0x0390
            r5 = 2
            r0 = r21
            if (r0 != r5) goto L_0x043a
            r24 = r43
            r25 = r44
            r26 = r45
            r27 = r12
            r28 = r47
            r29 = r22
            r30 = r8
            r32 = r49
            int r5 = r24.zza((T) r25, r26, r27, r28, r29, r30, r32)
            if (r5 != r12) goto L_0x0435
            r6 = r5
            r10 = r40
            r11 = r41
            goto L_0x0043
        L_0x0390:
            r26 = r43
            r27 = r44
            r28 = r45
            r29 = r12
            r30 = r47
            r31 = r4
            r32 = r20
            r33 = r21
            r35 = r25
            r36 = r8
            r38 = r22
            r39 = r49
            int r5 = r26.zza((T) r27, r28, r29, r30, r31, r32, r33, r34, r35, r36, r38, r39)
            if (r5 != r12) goto L_0x0435
            r6 = r5
            r10 = r40
            r11 = r41
            goto L_0x0043
        L_0x03b5:
            r4 = r44
            com.google.android.gms.internal.measurement.zzey$zzb r4 = (com.google.android.gms.internal.measurement.zzey.zzb) r4
            r4.zzuq()
            com.google.android.gms.internal.measurement.zzey$zzb r44 = (com.google.android.gms.internal.measurement.zzey.zzb) r44
            r0 = r44
            com.google.android.gms.internal.measurement.zzeo<java.lang.Object> r4 = r0.zzaic
            java.lang.NoSuchMethodError r4 = new java.lang.NoSuchMethodError
            r4.<init>()
            throw r4
        L_0x03c8:
            com.google.android.gms.internal.measurement.zzhs r8 = zzu(r44)
            r5 = r45
            r7 = r47
            r9 = r49
            int r5 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r5, r6, r7, r8, r9)
            r40 = r10
            r7 = r20
            r6 = r5
            r41 = r11
            goto L_0x000f
        L_0x03df:
            r7 = r4
            r5 = r10
            r8 = r6
            r9 = r11
        L_0x03e3:
            r4 = -1
            if (r5 == r4) goto L_0x03ee
            long r4 = (long) r5
            r0 = r42
            r1 = r44
            r0.putInt(r1, r4, r9)
        L_0x03ee:
            r0 = r43
            int r4 = r0.zzakt
            r6 = 0
            r5 = r4
        L_0x03f4:
            r0 = r43
            int r4 = r0.zzaku
            if (r5 >= r4) goto L_0x0412
            r0 = r43
            int[] r4 = r0.zzaks
            r4 = r4[r5]
            r0 = r43
            com.google.android.gms.internal.measurement.zzhp<?, ?> r9 = r0.zzakx
            r0 = r43
            r1 = r44
            java.lang.Object r4 = r0.zza(r1, r4, (UB) r6, r9)
            com.google.android.gms.internal.measurement.zzhs r4 = (com.google.android.gms.internal.measurement.zzhs) r4
            int r5 = r5 + 1
            r6 = r4
            goto L_0x03f4
        L_0x0412:
            if (r6 == 0) goto L_0x041d
            r0 = r43
            com.google.android.gms.internal.measurement.zzhp<?, ?> r4 = r0.zzakx
            r0 = r44
            r4.zzf(r0, r6)
        L_0x041d:
            if (r48 != 0) goto L_0x0428
            r0 = r47
            if (r8 == r0) goto L_0x044b
            com.google.android.gms.internal.measurement.zzfi r4 = com.google.android.gms.internal.measurement.zzfi.zzva()
            throw r4
        L_0x0428:
            r0 = r47
            if (r8 > r0) goto L_0x0430
            r0 = r48
            if (r7 == r0) goto L_0x044b
        L_0x0430:
            com.google.android.gms.internal.measurement.zzfi r4 = com.google.android.gms.internal.measurement.zzfi.zzva()
            throw r4
        L_0x0435:
            r7 = r20
            r6 = r5
            goto L_0x000f
        L_0x043a:
            r6 = r12
            r10 = r40
            r11 = r41
            goto L_0x0043
        L_0x0441:
            r14 = r5
            goto L_0x0325
        L_0x0444:
            r7 = r4
            r5 = r40
            r8 = r6
            r9 = r41
            goto L_0x03e3
        L_0x044b:
            return r8
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zza(java.lang.Object, byte[], int, int, int, com.google.android.gms.internal.measurement.zzdk):int");
    }

    public final void zza(T t, zzgy zzgy, zzel zzel) throws IOException {
        zzhp<?, ?> zzhp;
        Object obj;
        Object obj2;
        if (zzel == null) {
            throw new NullPointerException();
        }
        zzhp = this.zzakx;
        zzen<?> zzen = this.zzaky;
        Object obj3 = null;
        zzeo zzeo = null;
        while (true) {
            int zzsy = zzgy.zzsy();
            int zzcd = zzcd(zzsy);
            if (zzcd >= 0) {
                int zzca = zzca(zzcd);
                switch ((267386880 & zzca) >>> 20) {
                    case 0:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.readDouble());
                        zzb(t, zzcd);
                        continue;
                    case 1:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.readFloat());
                        zzb(t, zzcd);
                        continue;
                    case 2:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzsi());
                        zzb(t, zzcd);
                        continue;
                    case 3:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzsh());
                        zzb(t, zzcd);
                        continue;
                    case 4:
                        zzhv.zzb((Object) t, (long) (1048575 & zzca), zzgy.zzsj());
                        zzb(t, zzcd);
                        continue;
                    case 5:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzsk());
                        zzb(t, zzcd);
                        continue;
                    case 6:
                        zzhv.zzb((Object) t, (long) (1048575 & zzca), zzgy.zzsl());
                        zzb(t, zzcd);
                        continue;
                    case 7:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzsm());
                        zzb(t, zzcd);
                        continue;
                    case 8:
                        zza((Object) t, zzca, zzgy);
                        zzb(t, zzcd);
                        continue;
                    case 9:
                        if (!zza(t, zzcd)) {
                            zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zza(zzbx(zzcd), zzel));
                            zzb(t, zzcd);
                            break;
                        } else {
                            zzhv.zza((Object) t, (long) (1048575 & zzca), zzez.zza(zzhv.zzp(t, (long) (1048575 & zzca)), zzgy.zza(zzbx(zzcd), zzel)));
                            continue;
                        }
                    case 10:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), (Object) zzgy.zzso());
                        zzb(t, zzcd);
                        continue;
                    case 11:
                        zzhv.zzb((Object) t, (long) (1048575 & zzca), zzgy.zzsp());
                        zzb(t, zzcd);
                        continue;
                    case 12:
                        int zzsq = zzgy.zzsq();
                        zzfe zzbz = zzbz(zzcd);
                        if (zzbz != null && !zzbz.zzg(zzsq)) {
                            obj3 = zzgz.zza(zzsy, zzsq, obj3, zzhp);
                            break;
                        } else {
                            zzhv.zzb((Object) t, (long) (1048575 & zzca), zzsq);
                            zzb(t, zzcd);
                            continue;
                        }
                        break;
                    case 13:
                        zzhv.zzb((Object) t, (long) (1048575 & zzca), zzgy.zzsr());
                        zzb(t, zzcd);
                        continue;
                    case 14:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzss());
                        zzb(t, zzcd);
                        continue;
                    case 15:
                        zzhv.zzb((Object) t, (long) (1048575 & zzca), zzgy.zzst());
                        zzb(t, zzcd);
                        continue;
                    case 16:
                        zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzsu());
                        zzb(t, zzcd);
                        continue;
                    case 17:
                        if (!zza(t, zzcd)) {
                            zzhv.zza((Object) t, (long) (1048575 & zzca), zzgy.zzb(zzbx(zzcd), zzel));
                            zzb(t, zzcd);
                            break;
                        } else {
                            zzhv.zza((Object) t, (long) (1048575 & zzca), zzez.zza(zzhv.zzp(t, (long) (1048575 & zzca)), zzgy.zzb(zzbx(zzcd), zzel)));
                            continue;
                        }
                    case 18:
                        zzgy.zze(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 19:
                        zzgy.zzf(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 20:
                        zzgy.zzh(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 21:
                        zzgy.zzg(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 22:
                        zzgy.zzi(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 23:
                        zzgy.zzj(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 24:
                        zzgy.zzk(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 25:
                        zzgy.zzl(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 26:
                        if (!zzcc(zzca)) {
                            zzgy.readStringList(this.zzakw.zza(t, (long) (1048575 & zzca)));
                            break;
                        } else {
                            zzgy.zzm(this.zzakw.zza(t, (long) (1048575 & zzca)));
                            continue;
                        }
                    case 27:
                        zzgy.zza(this.zzakw.zza(t, (long) (1048575 & zzca)), zzbx(zzcd), zzel);
                        continue;
                    case 28:
                        zzgy.zzn(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 29:
                        zzgy.zzo(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 30:
                        List zza = this.zzakw.zza(t, (long) (zzca & 1048575));
                        zzgy.zzp(zza);
                        obj3 = zzgz.zza(zzsy, zza, zzbz(zzcd), obj3, zzhp);
                        continue;
                    case 31:
                        zzgy.zzq(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 32:
                        zzgy.zzr(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 33:
                        zzgy.zzs(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 34:
                        zzgy.zzt(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 35:
                        zzgy.zze(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 36:
                        zzgy.zzf(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 37:
                        zzgy.zzh(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 38:
                        zzgy.zzg(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 39:
                        zzgy.zzi(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 40:
                        zzgy.zzj(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 41:
                        zzgy.zzk(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 42:
                        zzgy.zzl(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 43:
                        zzgy.zzo(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 44:
                        List zza2 = this.zzakw.zza(t, (long) (zzca & 1048575));
                        zzgy.zzp(zza2);
                        obj3 = zzgz.zza(zzsy, zza2, zzbz(zzcd), obj3, zzhp);
                        continue;
                    case 45:
                        zzgy.zzq(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 46:
                        zzgy.zzr(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 47:
                        zzgy.zzs(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 48:
                        zzgy.zzt(this.zzakw.zza(t, (long) (1048575 & zzca)));
                        continue;
                    case 49:
                        zzgy.zzb(this.zzakw.zza(t, (long) (1048575 & zzca)), zzbx(zzcd), zzel);
                        continue;
                    case 50:
                        Object zzby = zzby(zzcd);
                        long zzca2 = (long) (zzca(zzcd) & 1048575);
                        Object zzp = zzhv.zzp(t, zzca2);
                        if (zzp == null) {
                            obj2 = this.zzakz.zzq(zzby);
                            zzhv.zza((Object) t, zzca2, obj2);
                        } else if (this.zzakz.zzo(zzp)) {
                            obj2 = this.zzakz.zzq(zzby);
                            this.zzakz.zzb(obj2, zzp);
                            zzhv.zza((Object) t, zzca2, obj2);
                        } else {
                            obj2 = zzp;
                        }
                        zzgy.zza(this.zzakz.zzm(obj2), this.zzakz.zzr(zzby), zzel);
                        continue;
                    case 51:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Double.valueOf(zzgy.readDouble()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 52:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Float.valueOf(zzgy.readFloat()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 53:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Long.valueOf(zzgy.zzsi()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 54:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Long.valueOf(zzgy.zzsh()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 55:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzgy.zzsj()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 56:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Long.valueOf(zzgy.zzsk()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 57:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzgy.zzsl()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 58:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Boolean.valueOf(zzgy.zzsm()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 59:
                        zza((Object) t, zzca, zzgy);
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 60:
                        if (zza(t, zzsy, zzcd)) {
                            zzhv.zza((Object) t, (long) (zzca & 1048575), zzez.zza(zzhv.zzp(t, (long) (1048575 & zzca)), zzgy.zza(zzbx(zzcd), zzel)));
                        } else {
                            zzhv.zza((Object) t, (long) (zzca & 1048575), zzgy.zza(zzbx(zzcd), zzel));
                            zzb(t, zzcd);
                        }
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 61:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) zzgy.zzso());
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 62:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzgy.zzsp()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 63:
                        int zzsq2 = zzgy.zzsq();
                        zzfe zzbz2 = zzbz(zzcd);
                        if (zzbz2 != null && !zzbz2.zzg(zzsq2)) {
                            obj3 = zzgz.zza(zzsy, zzsq2, obj3, zzhp);
                            break;
                        } else {
                            zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzsq2));
                            zzb(t, zzsy, zzcd);
                            continue;
                        }
                        break;
                    case 64:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzgy.zzsr()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 65:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Long.valueOf(zzgy.zzss()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 66:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Integer.valueOf(zzgy.zzst()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 67:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), (Object) Long.valueOf(zzgy.zzsu()));
                        zzb(t, zzsy, zzcd);
                        continue;
                    case 68:
                        zzhv.zza((Object) t, (long) (zzca & 1048575), zzgy.zzb(zzbx(zzcd), zzel));
                        zzb(t, zzsy, zzcd);
                        continue;
                    default:
                        if (obj3 == null) {
                            try {
                                obj3 = zzhp.zzwp();
                            } catch (zzfh e) {
                                break;
                            }
                        }
                        try {
                            if (!zzhp.zza(obj3, zzgy)) {
                                for (int i = this.zzakt; i < this.zzaku; i++) {
                                    obj3 = zza((Object) t, this.zzaks[i], (UB) obj3, zzhp);
                                }
                                if (obj3 != null) {
                                    zzhp.zzf(t, obj3);
                                    return;
                                }
                                return;
                            }
                            continue;
                        } catch (zzfh e2) {
                            break;
                        }
                }
                try {
                    zzhp.zza(zzgy);
                    if (obj3 == null) {
                        obj3 = zzhp.zzy(t);
                    }
                    if (!zzhp.zza(obj3, zzgy)) {
                        for (int i2 = this.zzakt; i2 < this.zzaku; i2++) {
                            obj3 = zza((Object) t, this.zzaks[i2], (UB) obj3, zzhp);
                        }
                        if (obj3 != null) {
                            zzhp.zzf(t, obj3);
                            return;
                        }
                        return;
                    }
                } catch (Throwable th) {
                    th = th;
                    obj = obj3;
                }
            } else if (zzsy == Integer.MAX_VALUE) {
                for (int i3 = this.zzakt; i3 < this.zzaku; i3++) {
                    obj3 = zza((Object) t, this.zzaks[i3], (UB) obj3, zzhp);
                }
                if (obj3 != null) {
                    zzhp.zzf(t, obj3);
                    return;
                }
                return;
            } else {
                Object zza3 = !this.zzako ? null : zzen.zza(zzel, this.zzakn, zzsy);
                if (zza3 != null) {
                    if (zzeo == null) {
                        zzeo = zzen.zzi(t);
                    }
                    obj3 = zzen.zza(zzgy, zza3, zzel, zzeo, obj3, zzhp);
                } else {
                    zzhp.zza(zzgy);
                    if (obj3 == null) {
                        obj3 = zzhp.zzy(t);
                    }
                    try {
                        if (!zzhp.zza(obj3, zzgy)) {
                            for (int i4 = this.zzakt; i4 < this.zzaku; i4++) {
                                obj3 = zza((Object) t, this.zzaks[i4], (UB) obj3, zzhp);
                            }
                            if (obj3 != null) {
                                zzhp.zzf(t, obj3);
                                return;
                            }
                            return;
                        }
                    } catch (Throwable th2) {
                        th = th2;
                        obj = obj3;
                    }
                }
            }
        }
        for (int i5 = this.zzakt; i5 < this.zzaku; i5++) {
            obj = zza((Object) t, this.zzaks[i5], (UB) obj, zzhp);
        }
        if (obj != null) {
            zzhp.zzf(t, obj);
        }
        throw th;
    }

    /* JADX WARNING: CFG modification limit reached, blocks count: 549 */
    /* JADX WARNING: Removed duplicated region for block: B:164:0x05fb  */
    /* JADX WARNING: Removed duplicated region for block: B:176:0x0637  */
    /* JADX WARNING: Removed duplicated region for block: B:331:0x0bfe  */
    /* JADX WARNING: Removed duplicated region for block: B:9:0x0034  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(T r11, com.google.android.gms.internal.measurement.zzim r12) throws java.io.IOException {
        /*
            r10 = this;
            int r0 = r12.zztk()
            int r1 = com.google.android.gms.internal.measurement.zzey.zzd.zzaip
            if (r0 != r1) goto L_0x060d
            com.google.android.gms.internal.measurement.zzhp<?, ?> r0 = r10.zzakx
            zza(r0, (T) r11, r12)
            r0 = 0
            r2 = 0
            boolean r1 = r10.zzako
            if (r1 == 0) goto L_0x0c1d
            com.google.android.gms.internal.measurement.zzen<?> r1 = r10.zzaky
            com.google.android.gms.internal.measurement.zzeo r1 = r1.zzh(r11)
            com.google.android.gms.internal.measurement.zzhc<FieldDescriptorType, java.lang.Object> r3 = r1.zzaex
            boolean r3 = r3.isEmpty()
            if (r3 != 0) goto L_0x0c1d
            java.util.Iterator r1 = r1.descendingIterator()
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
            r2 = r0
        L_0x002c:
            int[] r0 = r10.zzakj
            int r0 = r0.length
            int r3 = r0 + -3
            r0 = r2
        L_0x0032:
            if (r3 < 0) goto L_0x05f9
            int r4 = r10.zzca(r3)
            int[] r2 = r10.zzakj
            r5 = r2[r3]
            r2 = r0
        L_0x003d:
            if (r2 == 0) goto L_0x005c
            com.google.android.gms.internal.measurement.zzen<?> r0 = r10.zzaky
            int r0 = r0.zza(r2)
            if (r0 <= r5) goto L_0x005c
            com.google.android.gms.internal.measurement.zzen<?> r0 = r10.zzaky
            r0.zza(r12, r2)
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x005a
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
        L_0x0058:
            r2 = r0
            goto L_0x003d
        L_0x005a:
            r0 = 0
            goto L_0x0058
        L_0x005c:
            r0 = 267386880(0xff00000, float:2.3665827E-29)
            r0 = r0 & r4
            int r0 = r0 >>> 20
            switch(r0) {
                case 0: goto L_0x0068;
                case 1: goto L_0x007b;
                case 2: goto L_0x008e;
                case 3: goto L_0x00a1;
                case 4: goto L_0x00b4;
                case 5: goto L_0x00c7;
                case 6: goto L_0x00da;
                case 7: goto L_0x00ee;
                case 8: goto L_0x0102;
                case 9: goto L_0x0116;
                case 10: goto L_0x012e;
                case 11: goto L_0x0144;
                case 12: goto L_0x0158;
                case 13: goto L_0x016c;
                case 14: goto L_0x0180;
                case 15: goto L_0x0194;
                case 16: goto L_0x01a8;
                case 17: goto L_0x01bc;
                case 18: goto L_0x01d4;
                case 19: goto L_0x01e9;
                case 20: goto L_0x01fe;
                case 21: goto L_0x0213;
                case 22: goto L_0x0228;
                case 23: goto L_0x023d;
                case 24: goto L_0x0252;
                case 25: goto L_0x0267;
                case 26: goto L_0x027c;
                case 27: goto L_0x0290;
                case 28: goto L_0x02a8;
                case 29: goto L_0x02bc;
                case 30: goto L_0x02d1;
                case 31: goto L_0x02e6;
                case 32: goto L_0x02fb;
                case 33: goto L_0x0310;
                case 34: goto L_0x0325;
                case 35: goto L_0x033a;
                case 36: goto L_0x034f;
                case 37: goto L_0x0364;
                case 38: goto L_0x0379;
                case 39: goto L_0x038e;
                case 40: goto L_0x03a3;
                case 41: goto L_0x03b8;
                case 42: goto L_0x03cd;
                case 43: goto L_0x03e2;
                case 44: goto L_0x03f7;
                case 45: goto L_0x040c;
                case 46: goto L_0x0421;
                case 47: goto L_0x0436;
                case 48: goto L_0x044b;
                case 49: goto L_0x0460;
                case 50: goto L_0x0478;
                case 51: goto L_0x0486;
                case 52: goto L_0x049a;
                case 53: goto L_0x04ae;
                case 54: goto L_0x04c2;
                case 55: goto L_0x04d6;
                case 56: goto L_0x04ea;
                case 57: goto L_0x04fe;
                case 58: goto L_0x0512;
                case 59: goto L_0x0526;
                case 60: goto L_0x053a;
                case 61: goto L_0x0552;
                case 62: goto L_0x0568;
                case 63: goto L_0x057c;
                case 64: goto L_0x0590;
                case 65: goto L_0x05a4;
                case 66: goto L_0x05b8;
                case 67: goto L_0x05cc;
                case 68: goto L_0x05e0;
                default: goto L_0x0064;
            }
        L_0x0064:
            int r3 = r3 + -3
            r0 = r2
            goto L_0x0032
        L_0x0068:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            double r6 = com.google.android.gms.internal.measurement.zzhv.zzo(r11, r6)
            r12.zza(r5, r6)
            goto L_0x0064
        L_0x007b:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            float r0 = com.google.android.gms.internal.measurement.zzhv.zzn(r11, r6)
            r12.zza(r5, r0)
            goto L_0x0064
        L_0x008e:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r6)
            r12.zzi(r5, r6)
            goto L_0x0064
        L_0x00a1:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r6)
            r12.zza(r5, r6)
            goto L_0x0064
        L_0x00b4:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zzc(r5, r0)
            goto L_0x0064
        L_0x00c7:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r6)
            r12.zzc(r5, r6)
            goto L_0x0064
        L_0x00da:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zzf(r5, r0)
            goto L_0x0064
        L_0x00ee:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            boolean r0 = com.google.android.gms.internal.measurement.zzhv.zzm(r11, r6)
            r12.zzb(r5, r0)
            goto L_0x0064
        L_0x0102:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            zza(r5, r0, r12)
            goto L_0x0064
        L_0x0116:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            r12.zza(r5, r0, r4)
            goto L_0x0064
        L_0x012e:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzdp r0 = (com.google.android.gms.internal.measurement.zzdp) r0
            r12.zza(r5, r0)
            goto L_0x0064
        L_0x0144:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zzd(r5, r0)
            goto L_0x0064
        L_0x0158:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zzn(r5, r0)
            goto L_0x0064
        L_0x016c:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zzm(r5, r0)
            goto L_0x0064
        L_0x0180:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r6)
            r12.zzj(r5, r6)
            goto L_0x0064
        L_0x0194:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r6)
            r12.zze(r5, r0)
            goto L_0x0064
        L_0x01a8:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r6)
            r12.zzb(r5, r6)
            goto L_0x0064
        L_0x01bc:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            r12.zzb(r5, r0, r4)
            goto L_0x0064
        L_0x01d4:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zza(r5, r0, r12, r4)
            goto L_0x0064
        L_0x01e9:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzb(r5, r0, r12, r4)
            goto L_0x0064
        L_0x01fe:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzc(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0213:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzd(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0228:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzh(r5, r0, r12, r4)
            goto L_0x0064
        L_0x023d:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzf(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0252:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzk(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0267:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzn(r5, r0, r12, r4)
            goto L_0x0064
        L_0x027c:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgz.zza(r5, r0, r12)
            goto L_0x0064
        L_0x0290:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            com.google.android.gms.internal.measurement.zzgz.zza(r5, r0, r12, r4)
            goto L_0x0064
        L_0x02a8:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgz.zzb(r5, r0, r12)
            goto L_0x0064
        L_0x02bc:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzi(r5, r0, r12, r4)
            goto L_0x0064
        L_0x02d1:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzm(r5, r0, r12, r4)
            goto L_0x0064
        L_0x02e6:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzl(r5, r0, r12, r4)
            goto L_0x0064
        L_0x02fb:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzg(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0310:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zzj(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0325:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 0
            com.google.android.gms.internal.measurement.zzgz.zze(r5, r0, r12, r4)
            goto L_0x0064
        L_0x033a:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zza(r5, r0, r12, r4)
            goto L_0x0064
        L_0x034f:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzb(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0364:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzc(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0379:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzd(r5, r0, r12, r4)
            goto L_0x0064
        L_0x038e:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzh(r5, r0, r12, r4)
            goto L_0x0064
        L_0x03a3:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzf(r5, r0, r12, r4)
            goto L_0x0064
        L_0x03b8:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzk(r5, r0, r12, r4)
            goto L_0x0064
        L_0x03cd:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzn(r5, r0, r12, r4)
            goto L_0x0064
        L_0x03e2:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzi(r5, r0, r12, r4)
            goto L_0x0064
        L_0x03f7:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzm(r5, r0, r12, r4)
            goto L_0x0064
        L_0x040c:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzl(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0421:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzg(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0436:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zzj(r5, r0, r12, r4)
            goto L_0x0064
        L_0x044b:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            r4 = 1
            com.google.android.gms.internal.measurement.zzgz.zze(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0460:
            int[] r0 = r10.zzakj
            r5 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            com.google.android.gms.internal.measurement.zzgz.zzb(r5, r0, r12, r4)
            goto L_0x0064
        L_0x0478:
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            r10.zza(r12, r5, r0, r3)
            goto L_0x0064
        L_0x0486:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            double r6 = zzf(r11, r6)
            r12.zza(r5, r6)
            goto L_0x0064
        L_0x049a:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            float r0 = zzg(r11, r6)
            r12.zza(r5, r0)
            goto L_0x0064
        L_0x04ae:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = zzi(r11, r6)
            r12.zzi(r5, r6)
            goto L_0x0064
        L_0x04c2:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = zzi(r11, r6)
            r12.zza(r5, r6)
            goto L_0x0064
        L_0x04d6:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zzc(r5, r0)
            goto L_0x0064
        L_0x04ea:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = zzi(r11, r6)
            r12.zzc(r5, r6)
            goto L_0x0064
        L_0x04fe:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zzf(r5, r0)
            goto L_0x0064
        L_0x0512:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            boolean r0 = zzj(r11, r6)
            r12.zzb(r5, r0)
            goto L_0x0064
        L_0x0526:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            zza(r5, r0, r12)
            goto L_0x0064
        L_0x053a:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            r12.zza(r5, r0, r4)
            goto L_0x0064
        L_0x0552:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzdp r0 = (com.google.android.gms.internal.measurement.zzdp) r0
            r12.zza(r5, r0)
            goto L_0x0064
        L_0x0568:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zzd(r5, r0)
            goto L_0x0064
        L_0x057c:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zzn(r5, r0)
            goto L_0x0064
        L_0x0590:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zzm(r5, r0)
            goto L_0x0064
        L_0x05a4:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = zzi(r11, r6)
            r12.zzj(r5, r6)
            goto L_0x0064
        L_0x05b8:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            int r0 = zzh(r11, r6)
            r12.zze(r5, r0)
            goto L_0x0064
        L_0x05cc:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            long r6 = zzi(r11, r6)
            r12.zzb(r5, r6)
            goto L_0x0064
        L_0x05e0:
            boolean r0 = r10.zza((T) r11, r5, r3)
            if (r0 == 0) goto L_0x0064
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r4
            long r6 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r6)
            com.google.android.gms.internal.measurement.zzgx r4 = r10.zzbx(r3)
            r12.zzb(r5, r0, r4)
            goto L_0x0064
        L_0x05f8:
            r0 = 0
        L_0x05f9:
            if (r0 == 0) goto L_0x0c15
            com.google.android.gms.internal.measurement.zzen<?> r2 = r10.zzaky
            r2.zza(r12, r0)
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x05f8
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
            goto L_0x05f9
        L_0x060d:
            boolean r0 = r10.zzakq
            if (r0 == 0) goto L_0x0c16
            r0 = 0
            r2 = 0
            boolean r1 = r10.zzako
            if (r1 == 0) goto L_0x0c1a
            com.google.android.gms.internal.measurement.zzen<?> r1 = r10.zzaky
            com.google.android.gms.internal.measurement.zzeo r1 = r1.zzh(r11)
            com.google.android.gms.internal.measurement.zzhc<FieldDescriptorType, java.lang.Object> r3 = r1.zzaex
            boolean r3 = r3.isEmpty()
            if (r3 != 0) goto L_0x0c1a
            java.util.Iterator r1 = r1.iterator()
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
            r2 = r0
        L_0x0630:
            int[] r0 = r10.zzakj
            int r4 = r0.length
            r3 = 0
            r0 = r2
        L_0x0635:
            if (r3 >= r4) goto L_0x0bfc
            int r5 = r10.zzca(r3)
            int[] r2 = r10.zzakj
            r6 = r2[r3]
            r2 = r0
        L_0x0640:
            if (r2 == 0) goto L_0x065f
            com.google.android.gms.internal.measurement.zzen<?> r0 = r10.zzaky
            int r0 = r0.zza(r2)
            if (r0 > r6) goto L_0x065f
            com.google.android.gms.internal.measurement.zzen<?> r0 = r10.zzaky
            r0.zza(r12, r2)
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x065d
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
        L_0x065b:
            r2 = r0
            goto L_0x0640
        L_0x065d:
            r0 = 0
            goto L_0x065b
        L_0x065f:
            r0 = 267386880(0xff00000, float:2.3665827E-29)
            r0 = r0 & r5
            int r0 = r0 >>> 20
            switch(r0) {
                case 0: goto L_0x066b;
                case 1: goto L_0x067e;
                case 2: goto L_0x0691;
                case 3: goto L_0x06a4;
                case 4: goto L_0x06b7;
                case 5: goto L_0x06ca;
                case 6: goto L_0x06dd;
                case 7: goto L_0x06f1;
                case 8: goto L_0x0705;
                case 9: goto L_0x0719;
                case 10: goto L_0x0731;
                case 11: goto L_0x0747;
                case 12: goto L_0x075b;
                case 13: goto L_0x076f;
                case 14: goto L_0x0783;
                case 15: goto L_0x0797;
                case 16: goto L_0x07ab;
                case 17: goto L_0x07bf;
                case 18: goto L_0x07d7;
                case 19: goto L_0x07ec;
                case 20: goto L_0x0801;
                case 21: goto L_0x0816;
                case 22: goto L_0x082b;
                case 23: goto L_0x0840;
                case 24: goto L_0x0855;
                case 25: goto L_0x086a;
                case 26: goto L_0x087f;
                case 27: goto L_0x0893;
                case 28: goto L_0x08ab;
                case 29: goto L_0x08bf;
                case 30: goto L_0x08d4;
                case 31: goto L_0x08e9;
                case 32: goto L_0x08fe;
                case 33: goto L_0x0913;
                case 34: goto L_0x0928;
                case 35: goto L_0x093d;
                case 36: goto L_0x0952;
                case 37: goto L_0x0967;
                case 38: goto L_0x097c;
                case 39: goto L_0x0991;
                case 40: goto L_0x09a6;
                case 41: goto L_0x09bb;
                case 42: goto L_0x09d0;
                case 43: goto L_0x09e5;
                case 44: goto L_0x09fa;
                case 45: goto L_0x0a0f;
                case 46: goto L_0x0a24;
                case 47: goto L_0x0a39;
                case 48: goto L_0x0a4e;
                case 49: goto L_0x0a63;
                case 50: goto L_0x0a7b;
                case 51: goto L_0x0a89;
                case 52: goto L_0x0a9d;
                case 53: goto L_0x0ab1;
                case 54: goto L_0x0ac5;
                case 55: goto L_0x0ad9;
                case 56: goto L_0x0aed;
                case 57: goto L_0x0b01;
                case 58: goto L_0x0b15;
                case 59: goto L_0x0b29;
                case 60: goto L_0x0b3d;
                case 61: goto L_0x0b55;
                case 62: goto L_0x0b6b;
                case 63: goto L_0x0b7f;
                case 64: goto L_0x0b93;
                case 65: goto L_0x0ba7;
                case 66: goto L_0x0bbb;
                case 67: goto L_0x0bcf;
                case 68: goto L_0x0be3;
                default: goto L_0x0667;
            }
        L_0x0667:
            int r3 = r3 + 3
            r0 = r2
            goto L_0x0635
        L_0x066b:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            double r8 = com.google.android.gms.internal.measurement.zzhv.zzo(r11, r8)
            r12.zza(r6, r8)
            goto L_0x0667
        L_0x067e:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            float r0 = com.google.android.gms.internal.measurement.zzhv.zzn(r11, r8)
            r12.zza(r6, r0)
            goto L_0x0667
        L_0x0691:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r8)
            r12.zzi(r6, r8)
            goto L_0x0667
        L_0x06a4:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r8)
            r12.zza(r6, r8)
            goto L_0x0667
        L_0x06b7:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zzc(r6, r0)
            goto L_0x0667
        L_0x06ca:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r8)
            r12.zzc(r6, r8)
            goto L_0x0667
        L_0x06dd:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zzf(r6, r0)
            goto L_0x0667
        L_0x06f1:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            boolean r0 = com.google.android.gms.internal.measurement.zzhv.zzm(r11, r8)
            r12.zzb(r6, r0)
            goto L_0x0667
        L_0x0705:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            zza(r6, r0, r12)
            goto L_0x0667
        L_0x0719:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            r12.zza(r6, r0, r5)
            goto L_0x0667
        L_0x0731:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzdp r0 = (com.google.android.gms.internal.measurement.zzdp) r0
            r12.zza(r6, r0)
            goto L_0x0667
        L_0x0747:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zzd(r6, r0)
            goto L_0x0667
        L_0x075b:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zzn(r6, r0)
            goto L_0x0667
        L_0x076f:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zzm(r6, r0)
            goto L_0x0667
        L_0x0783:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r8)
            r12.zzj(r6, r8)
            goto L_0x0667
        L_0x0797:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = com.google.android.gms.internal.measurement.zzhv.zzk(r11, r8)
            r12.zze(r6, r0)
            goto L_0x0667
        L_0x07ab:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzl(r11, r8)
            r12.zzb(r6, r8)
            goto L_0x0667
        L_0x07bf:
            boolean r0 = r10.zza((T) r11, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            r12.zzb(r6, r0, r5)
            goto L_0x0667
        L_0x07d7:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zza(r6, r0, r12, r5)
            goto L_0x0667
        L_0x07ec:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzb(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0801:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzc(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0816:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzd(r6, r0, r12, r5)
            goto L_0x0667
        L_0x082b:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzh(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0840:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzf(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0855:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzk(r6, r0, r12, r5)
            goto L_0x0667
        L_0x086a:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzn(r6, r0, r12, r5)
            goto L_0x0667
        L_0x087f:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgz.zza(r6, r0, r12)
            goto L_0x0667
        L_0x0893:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            com.google.android.gms.internal.measurement.zzgz.zza(r6, r0, r12, r5)
            goto L_0x0667
        L_0x08ab:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgz.zzb(r6, r0, r12)
            goto L_0x0667
        L_0x08bf:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzi(r6, r0, r12, r5)
            goto L_0x0667
        L_0x08d4:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzm(r6, r0, r12, r5)
            goto L_0x0667
        L_0x08e9:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzl(r6, r0, r12, r5)
            goto L_0x0667
        L_0x08fe:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzg(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0913:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zzj(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0928:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 0
            com.google.android.gms.internal.measurement.zzgz.zze(r6, r0, r12, r5)
            goto L_0x0667
        L_0x093d:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zza(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0952:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzb(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0967:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzc(r6, r0, r12, r5)
            goto L_0x0667
        L_0x097c:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzd(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0991:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzh(r6, r0, r12, r5)
            goto L_0x0667
        L_0x09a6:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzf(r6, r0, r12, r5)
            goto L_0x0667
        L_0x09bb:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzk(r6, r0, r12, r5)
            goto L_0x0667
        L_0x09d0:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzn(r6, r0, r12, r5)
            goto L_0x0667
        L_0x09e5:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzi(r6, r0, r12, r5)
            goto L_0x0667
        L_0x09fa:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzm(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a0f:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzl(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a24:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzg(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a39:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zzj(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a4e:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            r5 = 1
            com.google.android.gms.internal.measurement.zzgz.zze(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a63:
            int[] r0 = r10.zzakj
            r6 = r0[r3]
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            java.util.List r0 = (java.util.List) r0
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            com.google.android.gms.internal.measurement.zzgz.zzb(r6, r0, r12, r5)
            goto L_0x0667
        L_0x0a7b:
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            r10.zza(r12, r6, r0, r3)
            goto L_0x0667
        L_0x0a89:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            double r8 = zzf(r11, r8)
            r12.zza(r6, r8)
            goto L_0x0667
        L_0x0a9d:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            float r0 = zzg(r11, r8)
            r12.zza(r6, r0)
            goto L_0x0667
        L_0x0ab1:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = zzi(r11, r8)
            r12.zzi(r6, r8)
            goto L_0x0667
        L_0x0ac5:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = zzi(r11, r8)
            r12.zza(r6, r8)
            goto L_0x0667
        L_0x0ad9:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zzc(r6, r0)
            goto L_0x0667
        L_0x0aed:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = zzi(r11, r8)
            r12.zzc(r6, r8)
            goto L_0x0667
        L_0x0b01:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zzf(r6, r0)
            goto L_0x0667
        L_0x0b15:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            boolean r0 = zzj(r11, r8)
            r12.zzb(r6, r0)
            goto L_0x0667
        L_0x0b29:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            zza(r6, r0, r12)
            goto L_0x0667
        L_0x0b3d:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            r12.zza(r6, r0, r5)
            goto L_0x0667
        L_0x0b55:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzdp r0 = (com.google.android.gms.internal.measurement.zzdp) r0
            r12.zza(r6, r0)
            goto L_0x0667
        L_0x0b6b:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zzd(r6, r0)
            goto L_0x0667
        L_0x0b7f:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zzn(r6, r0)
            goto L_0x0667
        L_0x0b93:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zzm(r6, r0)
            goto L_0x0667
        L_0x0ba7:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = zzi(r11, r8)
            r12.zzj(r6, r8)
            goto L_0x0667
        L_0x0bbb:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            int r0 = zzh(r11, r8)
            r12.zze(r6, r0)
            goto L_0x0667
        L_0x0bcf:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            long r8 = zzi(r11, r8)
            r12.zzb(r6, r8)
            goto L_0x0667
        L_0x0be3:
            boolean r0 = r10.zza((T) r11, r6, r3)
            if (r0 == 0) goto L_0x0667
            r0 = 1048575(0xfffff, float:1.469367E-39)
            r0 = r0 & r5
            long r8 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r11, r8)
            com.google.android.gms.internal.measurement.zzgx r5 = r10.zzbx(r3)
            r12.zzb(r6, r0, r5)
            goto L_0x0667
        L_0x0bfb:
            r0 = 0
        L_0x0bfc:
            if (r0 == 0) goto L_0x0c10
            com.google.android.gms.internal.measurement.zzen<?> r2 = r10.zzaky
            r2.zza(r12, r0)
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x0bfb
            java.lang.Object r0 = r1.next()
            java.util.Map$Entry r0 = (java.util.Map.Entry) r0
            goto L_0x0bfc
        L_0x0c10:
            com.google.android.gms.internal.measurement.zzhp<?, ?> r0 = r10.zzakx
            zza(r0, (T) r11, r12)
        L_0x0c15:
            return
        L_0x0c16:
            r10.zzb((T) r11, r12)
            goto L_0x0c15
        L_0x0c1a:
            r1 = r0
            goto L_0x0630
        L_0x0c1d:
            r1 = r0
            goto L_0x002c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zza(java.lang.Object, com.google.android.gms.internal.measurement.zzim):void");
    }

    /* JADX WARNING: type inference failed for: r4v4, types: [int] */
    /* JADX WARNING: type inference failed for: r25v0, types: [int] */
    /* JADX WARNING: type inference failed for: r13v0, types: [int] */
    /* JADX WARNING: type inference failed for: r6v8, types: [int] */
    /* JADX WARNING: type inference failed for: r4v32 */
    /* JADX WARNING: Code restructure failed: missing block: B:79:0x0242, code lost:
        if (r6 != r11) goto L_0x0244;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:86:0x0266, code lost:
        if (r6 == r11) goto L_0x003a;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:88:0x0286, code lost:
        if (r6 == r11) goto L_0x003a;
     */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(T r36, byte[] r37, int r38, int r39, com.google.android.gms.internal.measurement.zzdk r40) throws java.io.IOException {
        /*
            r35 = this;
            r0 = r35
            boolean r4 = r0.zzakq
            if (r4 == 0) goto L_0x0295
            sun.misc.Unsafe r34 = zzaki
            r16 = 0
            r14 = -1
            r5 = r14
        L_0x000c:
            r0 = r38
            r1 = r39
            if (r0 >= r1) goto L_0x028a
            int r11 = r38 + 1
            byte r4 = r37[r38]
            if (r4 >= 0) goto L_0x0024
            r0 = r37
            r1 = r40
            int r11 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r0, r11, r1)
            r0 = r40
            int r4 = r0.zzada
        L_0x0024:
            int r14 = r4 >>> 3
            r15 = r4 & 7
            if (r14 <= r5) goto L_0x004a
            int r5 = r16 / 3
            r0 = r35
            int r16 = r0.zzp(r14, r5)
        L_0x0032:
            r5 = -1
            r0 = r16
            if (r0 != r5) goto L_0x0051
            r16 = 0
            r6 = r11
        L_0x003a:
            com.google.android.gms.internal.measurement.zzhs r8 = zzu(r36)
            r5 = r37
            r7 = r39
            r9 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r5, r6, r7, r8, r9)
            r5 = r14
            goto L_0x000c
        L_0x004a:
            r0 = r35
            int r16 = r0.zzcd(r14)
            goto L_0x0032
        L_0x0051:
            r0 = r35
            int[] r5 = r0.zzakj
            int r6 = r16 + 1
            r28 = r5[r6]
            r5 = 267386880(0xff00000, float:2.3665827E-29)
            r5 = r5 & r28
            int r19 = r5 >>> 20
            r5 = 1048575(0xfffff, float:1.469367E-39)
            r5 = r5 & r28
            long r6 = (long) r5
            r5 = 17
            r0 = r19
            if (r0 > r5) goto L_0x01e0
            switch(r19) {
                case 0: goto L_0x0070;
                case 1: goto L_0x0082;
                case 2: goto L_0x0095;
                case 3: goto L_0x0095;
                case 4: goto L_0x00ad;
                case 5: goto L_0x00c5;
                case 6: goto L_0x00da;
                case 7: goto L_0x00ef;
                case 8: goto L_0x010e;
                case 9: goto L_0x0138;
                case 10: goto L_0x0177;
                case 11: goto L_0x00ad;
                case 12: goto L_0x0190;
                case 13: goto L_0x00da;
                case 14: goto L_0x00c5;
                case 15: goto L_0x01a8;
                case 16: goto L_0x01c4;
                default: goto L_0x006e;
            }
        L_0x006e:
            r6 = r11
            goto L_0x003a
        L_0x0070:
            r5 = 1
            if (r15 != r5) goto L_0x02a6
            r0 = r37
            double r4 = com.google.android.gms.internal.measurement.zzdl.zzc(r0, r11)
            r0 = r36
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r6, r4)
            int r38 = r11 + 8
            r5 = r14
            goto L_0x000c
        L_0x0082:
            r5 = 5
            if (r15 != r5) goto L_0x02a6
            r0 = r37
            float r4 = com.google.android.gms.internal.measurement.zzdl.zzd(r0, r11)
            r0 = r36
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r6, r4)
            int r38 = r11 + 4
            r5 = r14
            goto L_0x000c
        L_0x0095:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r11, r1)
            r0 = r40
            long r8 = r0.zzadb
            r4 = r34
            r5 = r36
            r4.putLong(r5, r6, r8)
            r5 = r14
            goto L_0x000c
        L_0x00ad:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11, r1)
            r0 = r40
            int r4 = r0.zzada
            r0 = r34
            r1 = r36
            r0.putInt(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x00c5:
            r5 = 1
            if (r15 != r5) goto L_0x02a6
            r0 = r37
            long r8 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r11)
            r4 = r34
            r5 = r36
            r4.putLong(r5, r6, r8)
            int r38 = r11 + 8
            r5 = r14
            goto L_0x000c
        L_0x00da:
            r5 = 5
            if (r15 != r5) goto L_0x02a6
            r0 = r37
            int r4 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11)
            r0 = r34
            r1 = r36
            r0.putInt(r1, r6, r4)
            int r38 = r11 + 4
            r5 = r14
            goto L_0x000c
        L_0x00ef:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r11, r1)
            r0 = r40
            long r4 = r0.zzadb
            r8 = 0
            int r4 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1))
            if (r4 == 0) goto L_0x010c
            r4 = 1
        L_0x0104:
            r0 = r36
            com.google.android.gms.internal.measurement.zzhv.zza(r0, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x010c:
            r4 = 0
            goto L_0x0104
        L_0x010e:
            r5 = 2
            if (r15 != r5) goto L_0x02a6
            r4 = 536870912(0x20000000, float:1.0842022E-19)
            r4 = r4 & r28
            if (r4 != 0) goto L_0x012f
            r0 = r37
            r1 = r40
            int r4 = com.google.android.gms.internal.measurement.zzdl.zzc(r0, r11, r1)
        L_0x011f:
            r0 = r40
            java.lang.Object r5 = r0.zzadc
            r0 = r34
            r1 = r36
            r0.putObject(r1, r6, r5)
            r38 = r4
            r5 = r14
            goto L_0x000c
        L_0x012f:
            r0 = r37
            r1 = r40
            int r4 = com.google.android.gms.internal.measurement.zzdl.zzd(r0, r11, r1)
            goto L_0x011f
        L_0x0138:
            r5 = 2
            if (r15 != r5) goto L_0x02a6
            r0 = r35
            r1 = r16
            com.google.android.gms.internal.measurement.zzgx r4 = r0.zzbx(r1)
            r0 = r37
            r1 = r39
            r2 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r4, r0, r11, r1, r2)
            r0 = r34
            r1 = r36
            java.lang.Object r4 = r0.getObject(r1, r6)
            if (r4 != 0) goto L_0x0165
            r0 = r40
            java.lang.Object r4 = r0.zzadc
            r0 = r34
            r1 = r36
            r0.putObject(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x0165:
            r0 = r40
            java.lang.Object r5 = r0.zzadc
            java.lang.Object r4 = com.google.android.gms.internal.measurement.zzez.zza(r4, r5)
            r0 = r34
            r1 = r36
            r0.putObject(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x0177:
            r5 = 2
            if (r15 != r5) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zze(r0, r11, r1)
            r0 = r40
            java.lang.Object r4 = r0.zzadc
            r0 = r34
            r1 = r36
            r0.putObject(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x0190:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11, r1)
            r0 = r40
            int r4 = r0.zzada
            r0 = r34
            r1 = r36
            r0.putInt(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x01a8:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11, r1)
            r0 = r40
            int r4 = r0.zzada
            int r4 = com.google.android.gms.internal.measurement.zzeb.zzaz(r4)
            r0 = r34
            r1 = r36
            r0.putInt(r1, r6, r4)
            r5 = r14
            goto L_0x000c
        L_0x01c4:
            if (r15 != 0) goto L_0x02a6
            r0 = r37
            r1 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zzb(r0, r11, r1)
            r0 = r40
            long r4 = r0.zzadb
            long r8 = com.google.android.gms.internal.measurement.zzeb.zzbm(r4)
            r4 = r34
            r5 = r36
            r4.putLong(r5, r6, r8)
            r5 = r14
            goto L_0x000c
        L_0x01e0:
            r5 = 27
            r0 = r19
            if (r0 != r5) goto L_0x0226
            r5 = 2
            if (r15 != r5) goto L_0x02a6
            r0 = r34
            r1 = r36
            java.lang.Object r5 = r0.getObject(r1, r6)
            com.google.android.gms.internal.measurement.zzff r5 = (com.google.android.gms.internal.measurement.zzff) r5
            boolean r8 = r5.zzrx()
            if (r8 != 0) goto L_0x02a9
            int r8 = r5.size()
            if (r8 != 0) goto L_0x0223
            r8 = 10
        L_0x0201:
            com.google.android.gms.internal.measurement.zzff r10 = r5.zzap(r8)
            r0 = r34
            r1 = r36
            r0.putObject(r1, r6, r10)
        L_0x020c:
            r0 = r35
            r1 = r16
            com.google.android.gms.internal.measurement.zzgx r5 = r0.zzbx(r1)
            r6 = r4
            r7 = r37
            r8 = r11
            r9 = r39
            r11 = r40
            int r38 = com.google.android.gms.internal.measurement.zzdl.zza(r5, r6, r7, r8, r9, r10, r11)
            r5 = r14
            goto L_0x000c
        L_0x0223:
            int r8 = r8 << 1
            goto L_0x0201
        L_0x0226:
            r5 = 49
            r0 = r19
            if (r0 > r5) goto L_0x0249
            r0 = r28
            long r0 = (long) r0
            r17 = r0
            r8 = r35
            r9 = r36
            r10 = r37
            r12 = r39
            r13 = r4
            r20 = r6
            r22 = r40
            int r6 = r8.zza((T) r9, r10, r11, r12, r13, r14, r15, r16, r17, r19, r20, r22)
            if (r6 == r11) goto L_0x003a
        L_0x0244:
            r38 = r6
            r5 = r14
            goto L_0x000c
        L_0x0249:
            r5 = 50
            r0 = r19
            if (r0 != r5) goto L_0x026a
            r5 = 2
            if (r15 != r5) goto L_0x02a6
            r18 = r35
            r19 = r36
            r20 = r37
            r21 = r11
            r22 = r39
            r23 = r16
            r24 = r6
            r26 = r40
            int r6 = r18.zza((T) r19, r20, r21, r22, r23, r24, r26)
            if (r6 != r11) goto L_0x0244
            goto L_0x003a
        L_0x026a:
            r20 = r35
            r21 = r36
            r22 = r37
            r23 = r11
            r24 = r39
            r25 = r4
            r26 = r14
            r27 = r15
            r29 = r19
            r30 = r6
            r32 = r16
            r33 = r40
            int r6 = r20.zza((T) r21, r22, r23, r24, r25, r26, r27, r28, r29, r30, r32, r33)
            if (r6 != r11) goto L_0x0244
            goto L_0x003a
        L_0x028a:
            r0 = r38
            r1 = r39
            if (r0 == r1) goto L_0x02a5
            com.google.android.gms.internal.measurement.zzfi r4 = com.google.android.gms.internal.measurement.zzfi.zzva()
            throw r4
        L_0x0295:
            r9 = 0
            r4 = r35
            r5 = r36
            r6 = r37
            r7 = r38
            r8 = r39
            r10 = r40
            r4.zza((T) r5, r6, r7, r8, r9, r10)
        L_0x02a5:
            return
        L_0x02a6:
            r6 = r11
            goto L_0x003a
        L_0x02a9:
            r10 = r5
            goto L_0x020c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zza(java.lang.Object, byte[], int, int, com.google.android.gms.internal.measurement.zzdk):void");
    }

    public final void zzc(T t, T t2) {
        if (t2 == null) {
            throw new NullPointerException();
        }
        for (int i = 0; i < this.zzakj.length; i += 3) {
            int zzca = zzca(i);
            long j = (long) (1048575 & zzca);
            int i2 = this.zzakj[i];
            switch ((zzca & 267386880) >>> 20) {
                case 0:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzo(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 1:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzn(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 2:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzl(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 3:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzl(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 4:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 5:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzl(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 6:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 7:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzm(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 8:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzp(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 9:
                    zza(t, t2, i);
                    break;
                case 10:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzp(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 11:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 12:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 13:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 14:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzl(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 15:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zzb((Object) t, j, zzhv.zzk(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 16:
                    if (!zza(t2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzl(t2, j));
                        zzb(t, i);
                        break;
                    }
                case 17:
                    zza(t, t2, i);
                    break;
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    this.zzakw.zza(t, t2, j);
                    break;
                case 50:
                    zzgz.zza(this.zzakz, t, t2, j);
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                    if (!zza(t2, i2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzp(t2, j));
                        zzb(t, i2, i);
                        break;
                    }
                case 60:
                    zzb(t, t2, i);
                    break;
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                    if (!zza(t2, i2, i)) {
                        break;
                    } else {
                        zzhv.zza((Object) t, j, zzhv.zzp(t2, j));
                        zzb(t, i2, i);
                        break;
                    }
                case 68:
                    zzb(t, t2, i);
                    break;
            }
        }
        if (!this.zzakq) {
            zzgz.zza(this.zzakx, t, t2);
            if (this.zzako) {
                zzgz.zza(this.zzaky, t, t2);
            }
        }
    }

    public final void zzj(T t) {
        for (int i = this.zzakt; i < this.zzaku; i++) {
            long zzca = (long) (zzca(this.zzaks[i]) & 1048575);
            Object zzp = zzhv.zzp(t, zzca);
            if (zzp != null) {
                zzhv.zza((Object) t, zzca, this.zzakz.zzp(zzp));
            }
        }
        int length = this.zzaks.length;
        for (int i2 = this.zzaku; i2 < length; i2++) {
            this.zzakw.zzb(t, (long) this.zzaks[i2]);
        }
        this.zzakx.zzj(t);
        if (this.zzako) {
            this.zzaky.zzj(t);
        }
    }

    public final int zzt(T t) {
        int i;
        if (this.zzakq) {
            Unsafe unsafe = zzaki;
            int i2 = 0;
            int i3 = 0;
            while (true) {
                int i4 = i2;
                if (i3 >= this.zzakj.length) {
                    return zza(this.zzakx, t) + i4;
                }
                int zzca = zzca(i3);
                int i5 = (267386880 & zzca) >>> 20;
                int i6 = this.zzakj[i3];
                long j = (long) (zzca & 1048575);
                int i7 = (i5 < zzet.DOUBLE_LIST_PACKED.mo17089id() || i5 > zzet.SINT64_LIST_PACKED.mo17089id()) ? 0 : this.zzakj[i3 + 2] & 1048575;
                switch (i5) {
                    case 0:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzb(i6, 0.0d);
                            break;
                        }
                    case 1:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzb(i6, 0.0f);
                            break;
                        }
                    case 2:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzd(i6, zzhv.zzl(t, j));
                            break;
                        }
                    case 3:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zze(i6, zzhv.zzl(t, j));
                            break;
                        }
                    case 4:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzg(i6, zzhv.zzk(t, j));
                            break;
                        }
                    case 5:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzg(i6, 0);
                            break;
                        }
                    case 6:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzj(i6, 0);
                            break;
                        }
                    case 7:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, true);
                            break;
                        }
                    case 8:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            Object zzp = zzhv.zzp(t, j);
                            if (!(zzp instanceof zzdp)) {
                                i4 += zzee.zzc(i6, (String) zzp);
                                break;
                            } else {
                                i4 += zzee.zzc(i6, (zzdp) zzp);
                                break;
                            }
                        }
                    case 9:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzgz.zzc(i6, zzhv.zzp(t, j), zzbx(i3));
                            break;
                        }
                    case 10:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, (zzdp) zzhv.zzp(t, j));
                            break;
                        }
                    case 11:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzh(i6, zzhv.zzk(t, j));
                            break;
                        }
                    case 12:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzl(i6, zzhv.zzk(t, j));
                            break;
                        }
                    case 13:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzk(i6, 0);
                            break;
                        }
                    case 14:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzh(i6, 0);
                            break;
                        }
                    case 15:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzi(i6, zzhv.zzk(t, j));
                            break;
                        }
                    case 16:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzf(i6, zzhv.zzl(t, j));
                            break;
                        }
                    case 17:
                        if (!zza(t, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, (zzgi) zzhv.zzp(t, j), zzbx(i3));
                            break;
                        }
                    case 18:
                        i4 += zzgz.zzw(i6, zze(t, j), false);
                        break;
                    case 19:
                        i4 += zzgz.zzv(i6, zze(t, j), false);
                        break;
                    case 20:
                        i4 += zzgz.zzo(i6, zze(t, j), false);
                        break;
                    case 21:
                        i4 += zzgz.zzp(i6, zze(t, j), false);
                        break;
                    case 22:
                        i4 += zzgz.zzs(i6, zze(t, j), false);
                        break;
                    case 23:
                        i4 += zzgz.zzw(i6, zze(t, j), false);
                        break;
                    case 24:
                        i4 += zzgz.zzv(i6, zze(t, j), false);
                        break;
                    case 25:
                        i4 += zzgz.zzx(i6, zze(t, j), false);
                        break;
                    case 26:
                        i4 += zzgz.zzc(i6, zze(t, j));
                        break;
                    case 27:
                        i4 += zzgz.zzc(i6, zze(t, j), zzbx(i3));
                        break;
                    case 28:
                        i4 += zzgz.zzd(i6, zze(t, j));
                        break;
                    case 29:
                        i4 += zzgz.zzt(i6, zze(t, j), false);
                        break;
                    case 30:
                        i4 += zzgz.zzr(i6, zze(t, j), false);
                        break;
                    case 31:
                        i4 += zzgz.zzv(i6, zze(t, j), false);
                        break;
                    case 32:
                        i4 += zzgz.zzw(i6, zze(t, j), false);
                        break;
                    case 33:
                        i4 += zzgz.zzu(i6, zze(t, j), false);
                        break;
                    case 34:
                        i4 += zzgz.zzq(i6, zze(t, j), false);
                        break;
                    case 35:
                        int zzac = zzgz.zzac((List) unsafe.getObject(t, j));
                        if (zzac > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzac);
                            }
                            i4 += zzac + zzee.zzbi(i6) + zzee.zzbk(zzac);
                            break;
                        } else {
                            break;
                        }
                    case 36:
                        int zzab = zzgz.zzab((List) unsafe.getObject(t, j));
                        if (zzab > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzab);
                            }
                            i4 += zzab + zzee.zzbi(i6) + zzee.zzbk(zzab);
                            break;
                        } else {
                            break;
                        }
                    case 37:
                        int zzu = zzgz.zzu((List) unsafe.getObject(t, j));
                        if (zzu > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzu);
                            }
                            i4 += zzu + zzee.zzbi(i6) + zzee.zzbk(zzu);
                            break;
                        } else {
                            break;
                        }
                    case 38:
                        int zzv = zzgz.zzv((List) unsafe.getObject(t, j));
                        if (zzv > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzv);
                            }
                            i4 += zzv + zzee.zzbi(i6) + zzee.zzbk(zzv);
                            break;
                        } else {
                            break;
                        }
                    case 39:
                        int zzy = zzgz.zzy((List) unsafe.getObject(t, j));
                        if (zzy > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzy);
                            }
                            i4 += zzy + zzee.zzbi(i6) + zzee.zzbk(zzy);
                            break;
                        } else {
                            break;
                        }
                    case 40:
                        int zzac2 = zzgz.zzac((List) unsafe.getObject(t, j));
                        if (zzac2 > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzac2);
                            }
                            i4 += zzac2 + zzee.zzbi(i6) + zzee.zzbk(zzac2);
                            break;
                        } else {
                            break;
                        }
                    case 41:
                        int zzab2 = zzgz.zzab((List) unsafe.getObject(t, j));
                        if (zzab2 > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzab2);
                            }
                            i4 += zzab2 + zzee.zzbi(i6) + zzee.zzbk(zzab2);
                            break;
                        } else {
                            break;
                        }
                    case 42:
                        int zzad = zzgz.zzad((List) unsafe.getObject(t, j));
                        if (zzad > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzad);
                            }
                            i4 += zzad + zzee.zzbi(i6) + zzee.zzbk(zzad);
                            break;
                        } else {
                            break;
                        }
                    case 43:
                        int zzz = zzgz.zzz((List) unsafe.getObject(t, j));
                        if (zzz > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzz);
                            }
                            i4 += zzz + zzee.zzbi(i6) + zzee.zzbk(zzz);
                            break;
                        } else {
                            break;
                        }
                    case 44:
                        int zzx = zzgz.zzx((List) unsafe.getObject(t, j));
                        if (zzx > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzx);
                            }
                            i4 += zzx + zzee.zzbi(i6) + zzee.zzbk(zzx);
                            break;
                        } else {
                            break;
                        }
                    case 45:
                        int zzab3 = zzgz.zzab((List) unsafe.getObject(t, j));
                        if (zzab3 > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzab3);
                            }
                            i4 += zzab3 + zzee.zzbi(i6) + zzee.zzbk(zzab3);
                            break;
                        } else {
                            break;
                        }
                    case 46:
                        int zzac3 = zzgz.zzac((List) unsafe.getObject(t, j));
                        if (zzac3 > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzac3);
                            }
                            i4 += zzac3 + zzee.zzbi(i6) + zzee.zzbk(zzac3);
                            break;
                        } else {
                            break;
                        }
                    case 47:
                        int zzaa = zzgz.zzaa((List) unsafe.getObject(t, j));
                        if (zzaa > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzaa);
                            }
                            i4 += zzaa + zzee.zzbi(i6) + zzee.zzbk(zzaa);
                            break;
                        } else {
                            break;
                        }
                    case 48:
                        int zzw = zzgz.zzw((List) unsafe.getObject(t, j));
                        if (zzw > 0) {
                            if (this.zzakr) {
                                unsafe.putInt(t, (long) i7, zzw);
                            }
                            i4 += zzw + zzee.zzbi(i6) + zzee.zzbk(zzw);
                            break;
                        } else {
                            break;
                        }
                    case 49:
                        i4 += zzgz.zzd(i6, zze(t, j), zzbx(i3));
                        break;
                    case 50:
                        i4 += this.zzakz.zzb(i6, zzhv.zzp(t, j), zzby(i3));
                        break;
                    case 51:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzb(i6, 0.0d);
                            break;
                        }
                    case 52:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzb(i6, 0.0f);
                            break;
                        }
                    case 53:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzd(i6, zzi(t, j));
                            break;
                        }
                    case 54:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zze(i6, zzi(t, j));
                            break;
                        }
                    case 55:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzg(i6, zzh(t, j));
                            break;
                        }
                    case 56:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzg(i6, 0);
                            break;
                        }
                    case 57:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzj(i6, 0);
                            break;
                        }
                    case 58:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, true);
                            break;
                        }
                    case 59:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            Object zzp2 = zzhv.zzp(t, j);
                            if (!(zzp2 instanceof zzdp)) {
                                i4 += zzee.zzc(i6, (String) zzp2);
                                break;
                            } else {
                                i4 += zzee.zzc(i6, (zzdp) zzp2);
                                break;
                            }
                        }
                    case 60:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzgz.zzc(i6, zzhv.zzp(t, j), zzbx(i3));
                            break;
                        }
                    case 61:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, (zzdp) zzhv.zzp(t, j));
                            break;
                        }
                    case 62:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzh(i6, zzh(t, j));
                            break;
                        }
                    case 63:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzl(i6, zzh(t, j));
                            break;
                        }
                    case 64:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzk(i6, 0);
                            break;
                        }
                    case 65:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzh(i6, 0);
                            break;
                        }
                    case 66:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzi(i6, zzh(t, j));
                            break;
                        }
                    case 67:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzf(i6, zzi(t, j));
                            break;
                        }
                    case 68:
                        if (!zza(t, i6, i3)) {
                            break;
                        } else {
                            i4 += zzee.zzc(i6, (zzgi) zzhv.zzp(t, j), zzbx(i3));
                            break;
                        }
                }
                i2 = i4;
                i3 += 3;
            }
        } else {
            Unsafe unsafe2 = zzaki;
            int i8 = -1;
            int i9 = 0;
            int i10 = 0;
            int i11 = 0;
            while (true) {
                int i12 = i9;
                if (i10 < this.zzakj.length) {
                    int zzca2 = zzca(i10);
                    int i13 = this.zzakj[i10];
                    int i14 = (267386880 & zzca2) >>> 20;
                    int i15 = 0;
                    if (i14 <= 17) {
                        i15 = this.zzakj[i10 + 2];
                        int i16 = 1048575 & i15;
                        if (i16 != i8) {
                            i11 = unsafe2.getInt(t, (long) i16);
                            i8 = i16;
                        }
                        i = 1 << (i15 >>> 20);
                    } else if (!this.zzakr || i14 < zzet.DOUBLE_LIST_PACKED.mo17089id() || i14 > zzet.SINT64_LIST_PACKED.mo17089id()) {
                        i = 0;
                    } else {
                        i15 = 1048575 & this.zzakj[i10 + 2];
                        i = 0;
                    }
                    long j2 = (long) (zzca2 & 1048575);
                    switch (i14) {
                        case 0:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzb(i13, 0.0d);
                                break;
                            }
                        case 1:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzb(i13, 0.0f);
                                break;
                            }
                        case 2:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzd(i13, unsafe2.getLong(t, j2));
                                break;
                            }
                        case 3:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zze(i13, unsafe2.getLong(t, j2));
                                break;
                            }
                        case 4:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzg(i13, unsafe2.getInt(t, j2));
                                break;
                            }
                        case 5:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzg(i13, 0);
                                break;
                            }
                        case 6:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzj(i13, 0);
                                break;
                            }
                        case 7:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, true);
                                break;
                            }
                        case 8:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                Object object = unsafe2.getObject(t, j2);
                                if (!(object instanceof zzdp)) {
                                    i12 += zzee.zzc(i13, (String) object);
                                    break;
                                } else {
                                    i12 += zzee.zzc(i13, (zzdp) object);
                                    break;
                                }
                            }
                        case 9:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzgz.zzc(i13, unsafe2.getObject(t, j2), zzbx(i10));
                                break;
                            }
                        case 10:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, (zzdp) unsafe2.getObject(t, j2));
                                break;
                            }
                        case 11:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzh(i13, unsafe2.getInt(t, j2));
                                break;
                            }
                        case 12:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzl(i13, unsafe2.getInt(t, j2));
                                break;
                            }
                        case 13:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzk(i13, 0);
                                break;
                            }
                        case 14:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzh(i13, 0);
                                break;
                            }
                        case 15:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzi(i13, unsafe2.getInt(t, j2));
                                break;
                            }
                        case 16:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzf(i13, unsafe2.getLong(t, j2));
                                break;
                            }
                        case 17:
                            if ((i & i11) == 0) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, (zzgi) unsafe2.getObject(t, j2), zzbx(i10));
                                break;
                            }
                        case 18:
                            i12 += zzgz.zzw(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 19:
                            i12 += zzgz.zzv(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 20:
                            i12 += zzgz.zzo(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 21:
                            i12 += zzgz.zzp(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 22:
                            i12 += zzgz.zzs(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 23:
                            i12 += zzgz.zzw(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 24:
                            i12 += zzgz.zzv(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 25:
                            i12 += zzgz.zzx(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 26:
                            i12 += zzgz.zzc(i13, (List) unsafe2.getObject(t, j2));
                            break;
                        case 27:
                            i12 += zzgz.zzc(i13, (List) unsafe2.getObject(t, j2), zzbx(i10));
                            break;
                        case 28:
                            i12 += zzgz.zzd(i13, (List) unsafe2.getObject(t, j2));
                            break;
                        case 29:
                            i12 += zzgz.zzt(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 30:
                            i12 += zzgz.zzr(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 31:
                            i12 += zzgz.zzv(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 32:
                            i12 += zzgz.zzw(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 33:
                            i12 += zzgz.zzu(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 34:
                            i12 += zzgz.zzq(i13, (List) unsafe2.getObject(t, j2), false);
                            break;
                        case 35:
                            int zzac4 = zzgz.zzac((List) unsafe2.getObject(t, j2));
                            if (zzac4 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzac4);
                                }
                                i12 += zzac4 + zzee.zzbi(i13) + zzee.zzbk(zzac4);
                                break;
                            } else {
                                break;
                            }
                        case 36:
                            int zzab4 = zzgz.zzab((List) unsafe2.getObject(t, j2));
                            if (zzab4 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzab4);
                                }
                                i12 += zzab4 + zzee.zzbi(i13) + zzee.zzbk(zzab4);
                                break;
                            } else {
                                break;
                            }
                        case 37:
                            int zzu2 = zzgz.zzu((List) unsafe2.getObject(t, j2));
                            if (zzu2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzu2);
                                }
                                i12 += zzu2 + zzee.zzbi(i13) + zzee.zzbk(zzu2);
                                break;
                            } else {
                                break;
                            }
                        case 38:
                            int zzv2 = zzgz.zzv((List) unsafe2.getObject(t, j2));
                            if (zzv2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzv2);
                                }
                                i12 += zzv2 + zzee.zzbi(i13) + zzee.zzbk(zzv2);
                                break;
                            } else {
                                break;
                            }
                        case 39:
                            int zzy2 = zzgz.zzy((List) unsafe2.getObject(t, j2));
                            if (zzy2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzy2);
                                }
                                i12 += zzy2 + zzee.zzbi(i13) + zzee.zzbk(zzy2);
                                break;
                            } else {
                                break;
                            }
                        case 40:
                            int zzac5 = zzgz.zzac((List) unsafe2.getObject(t, j2));
                            if (zzac5 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzac5);
                                }
                                i12 += zzac5 + zzee.zzbi(i13) + zzee.zzbk(zzac5);
                                break;
                            } else {
                                break;
                            }
                        case 41:
                            int zzab5 = zzgz.zzab((List) unsafe2.getObject(t, j2));
                            if (zzab5 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzab5);
                                }
                                i12 += zzab5 + zzee.zzbi(i13) + zzee.zzbk(zzab5);
                                break;
                            } else {
                                break;
                            }
                        case 42:
                            int zzad2 = zzgz.zzad((List) unsafe2.getObject(t, j2));
                            if (zzad2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzad2);
                                }
                                i12 += zzad2 + zzee.zzbi(i13) + zzee.zzbk(zzad2);
                                break;
                            } else {
                                break;
                            }
                        case 43:
                            int zzz2 = zzgz.zzz((List) unsafe2.getObject(t, j2));
                            if (zzz2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzz2);
                                }
                                i12 += zzz2 + zzee.zzbi(i13) + zzee.zzbk(zzz2);
                                break;
                            } else {
                                break;
                            }
                        case 44:
                            int zzx2 = zzgz.zzx((List) unsafe2.getObject(t, j2));
                            if (zzx2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzx2);
                                }
                                i12 += zzx2 + zzee.zzbi(i13) + zzee.zzbk(zzx2);
                                break;
                            } else {
                                break;
                            }
                        case 45:
                            int zzab6 = zzgz.zzab((List) unsafe2.getObject(t, j2));
                            if (zzab6 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzab6);
                                }
                                i12 += zzab6 + zzee.zzbi(i13) + zzee.zzbk(zzab6);
                                break;
                            } else {
                                break;
                            }
                        case 46:
                            int zzac6 = zzgz.zzac((List) unsafe2.getObject(t, j2));
                            if (zzac6 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzac6);
                                }
                                i12 += zzac6 + zzee.zzbi(i13) + zzee.zzbk(zzac6);
                                break;
                            } else {
                                break;
                            }
                        case 47:
                            int zzaa2 = zzgz.zzaa((List) unsafe2.getObject(t, j2));
                            if (zzaa2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzaa2);
                                }
                                i12 += zzaa2 + zzee.zzbi(i13) + zzee.zzbk(zzaa2);
                                break;
                            } else {
                                break;
                            }
                        case 48:
                            int zzw2 = zzgz.zzw((List) unsafe2.getObject(t, j2));
                            if (zzw2 > 0) {
                                if (this.zzakr) {
                                    unsafe2.putInt(t, (long) i15, zzw2);
                                }
                                i12 += zzw2 + zzee.zzbi(i13) + zzee.zzbk(zzw2);
                                break;
                            } else {
                                break;
                            }
                        case 49:
                            i12 += zzgz.zzd(i13, (List) unsafe2.getObject(t, j2), zzbx(i10));
                            break;
                        case 50:
                            i12 += this.zzakz.zzb(i13, unsafe2.getObject(t, j2), zzby(i10));
                            break;
                        case 51:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzb(i13, 0.0d);
                                break;
                            }
                        case 52:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzb(i13, 0.0f);
                                break;
                            }
                        case 53:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzd(i13, zzi(t, j2));
                                break;
                            }
                        case 54:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zze(i13, zzi(t, j2));
                                break;
                            }
                        case 55:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzg(i13, zzh(t, j2));
                                break;
                            }
                        case 56:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzg(i13, 0);
                                break;
                            }
                        case 57:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzj(i13, 0);
                                break;
                            }
                        case 58:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, true);
                                break;
                            }
                        case 59:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                Object object2 = unsafe2.getObject(t, j2);
                                if (!(object2 instanceof zzdp)) {
                                    i12 += zzee.zzc(i13, (String) object2);
                                    break;
                                } else {
                                    i12 += zzee.zzc(i13, (zzdp) object2);
                                    break;
                                }
                            }
                        case 60:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzgz.zzc(i13, unsafe2.getObject(t, j2), zzbx(i10));
                                break;
                            }
                        case 61:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, (zzdp) unsafe2.getObject(t, j2));
                                break;
                            }
                        case 62:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzh(i13, zzh(t, j2));
                                break;
                            }
                        case 63:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzl(i13, zzh(t, j2));
                                break;
                            }
                        case 64:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzk(i13, 0);
                                break;
                            }
                        case 65:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzh(i13, 0);
                                break;
                            }
                        case 66:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzi(i13, zzh(t, j2));
                                break;
                            }
                        case 67:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzf(i13, zzi(t, j2));
                                break;
                            }
                        case 68:
                            if (!zza(t, i13, i10)) {
                                break;
                            } else {
                                i12 += zzee.zzc(i13, (zzgi) unsafe2.getObject(t, j2), zzbx(i10));
                                break;
                            }
                    }
                    i9 = i12;
                    i10 += 3;
                } else {
                    int zza = i12 + zza(this.zzakx, t);
                    if (!this.zzako) {
                        return zza;
                    }
                    zzeo zzh = this.zzaky.zzh(t);
                    int i17 = 0;
                    int i18 = 0;
                    while (true) {
                        int i19 = i18;
                        if (i19 < zzh.zzaex.zzwh()) {
                            Entry zzcf = zzh.zzaex.zzcf(i19);
                            i17 += zzeo.zzb((zzeq) zzcf.getKey(), zzcf.getValue());
                            i18 = i19 + 1;
                        } else {
                            for (Entry entry : zzh.zzaex.zzwi()) {
                                i17 += zzeo.zzb((zzeq) entry.getKey(), entry.getValue());
                            }
                            return zza + i17;
                        }
                    }
                }
            }
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:64:0x0042 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:71:0x004d A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean zzv(T r14) {
        /*
            r13 = this;
            r12 = 1048575(0xfffff, float:1.469367E-39)
            r6 = 1
            r2 = 0
            r0 = -1
            r1 = r2
            r3 = r0
            r4 = r2
        L_0x0009:
            int r0 = r13.zzakt
            if (r1 >= r0) goto L_0x00f5
            int[] r0 = r13.zzaks
            r8 = r0[r1]
            int[] r0 = r13.zzakj
            r9 = r0[r8]
            int r10 = r13.zzca(r8)
            boolean r0 = r13.zzakq
            if (r0 != 0) goto L_0x010b
            int[] r0 = r13.zzakj
            int r5 = r8 + 2
            r0 = r0[r5]
            r7 = r0 & r12
            int r0 = r0 >>> 20
            int r0 = r6 << r0
            if (r7 == r3) goto L_0x0108
            sun.misc.Unsafe r3 = zzaki
            long r4 = (long) r7
            int r4 = r3.getInt(r14, r4)
            r5 = r0
            r3 = r7
        L_0x0034:
            r0 = 268435456(0x10000000, float:2.5243549E-29)
            r0 = r0 & r10
            if (r0 == 0) goto L_0x0043
            r0 = r6
        L_0x003a:
            if (r0 == 0) goto L_0x0045
            boolean r0 = r13.zza((T) r14, r8, r4, r5)
            if (r0 != 0) goto L_0x0045
        L_0x0042:
            return r2
        L_0x0043:
            r0 = r2
            goto L_0x003a
        L_0x0045:
            r0 = 267386880(0xff00000, float:2.3665827E-29)
            r0 = r0 & r10
            int r0 = r0 >>> 20
            switch(r0) {
                case 9: goto L_0x0051;
                case 17: goto L_0x0051;
                case 27: goto L_0x0062;
                case 49: goto L_0x0062;
                case 50: goto L_0x00a0;
                case 60: goto L_0x008f;
                case 68: goto L_0x008f;
                default: goto L_0x004d;
            }
        L_0x004d:
            int r0 = r1 + 1
            r1 = r0
            goto L_0x0009
        L_0x0051:
            boolean r0 = r13.zza((T) r14, r8, r4, r5)
            if (r0 == 0) goto L_0x004d
            com.google.android.gms.internal.measurement.zzgx r0 = r13.zzbx(r8)
            boolean r0 = zza(r14, r10, r0)
            if (r0 != 0) goto L_0x004d
            goto L_0x0042
        L_0x0062:
            r0 = r10 & r12
            long r10 = (long) r0
            java.lang.Object r0 = com.google.android.gms.internal.measurement.zzhv.zzp(r14, r10)
            java.util.List r0 = (java.util.List) r0
            boolean r5 = r0.isEmpty()
            if (r5 != 0) goto L_0x008d
            com.google.android.gms.internal.measurement.zzgx r7 = r13.zzbx(r8)
            r5 = r2
        L_0x0076:
            int r8 = r0.size()
            if (r5 >= r8) goto L_0x008d
            java.lang.Object r8 = r0.get(r5)
            boolean r8 = r7.zzv(r8)
            if (r8 != 0) goto L_0x008a
            r0 = r2
        L_0x0087:
            if (r0 != 0) goto L_0x004d
            goto L_0x0042
        L_0x008a:
            int r5 = r5 + 1
            goto L_0x0076
        L_0x008d:
            r0 = r6
            goto L_0x0087
        L_0x008f:
            boolean r0 = r13.zza((T) r14, r9, r8)
            if (r0 == 0) goto L_0x004d
            com.google.android.gms.internal.measurement.zzgx r0 = r13.zzbx(r8)
            boolean r0 = zza(r14, r10, r0)
            if (r0 != 0) goto L_0x004d
            goto L_0x0042
        L_0x00a0:
            com.google.android.gms.internal.measurement.zzgb r0 = r13.zzakz
            r5 = r10 & r12
            long r10 = (long) r5
            java.lang.Object r5 = com.google.android.gms.internal.measurement.zzhv.zzp(r14, r10)
            java.util.Map r5 = r0.zzn(r5)
            boolean r0 = r5.isEmpty()
            if (r0 != 0) goto L_0x00f3
            java.lang.Object r0 = r13.zzby(r8)
            com.google.android.gms.internal.measurement.zzgb r7 = r13.zzakz
            com.google.android.gms.internal.measurement.zzfz r0 = r7.zzr(r0)
            com.google.android.gms.internal.measurement.zzig r0 = r0.zzakd
            com.google.android.gms.internal.measurement.zzij r0 = r0.zzwz()
            com.google.android.gms.internal.measurement.zzij r7 = com.google.android.gms.internal.measurement.zzij.MESSAGE
            if (r0 != r7) goto L_0x00f3
            r0 = 0
            java.util.Collection r5 = r5.values()
            java.util.Iterator r5 = r5.iterator()
        L_0x00d0:
            boolean r7 = r5.hasNext()
            if (r7 == 0) goto L_0x00f3
            java.lang.Object r7 = r5.next()
            if (r0 != 0) goto L_0x00e8
            com.google.android.gms.internal.measurement.zzgt r0 = com.google.android.gms.internal.measurement.zzgt.zzvy()
            java.lang.Class r8 = r7.getClass()
            com.google.android.gms.internal.measurement.zzgx r0 = r0.zzf(r8)
        L_0x00e8:
            boolean r7 = r0.zzv(r7)
            if (r7 != 0) goto L_0x00d0
            r0 = r2
        L_0x00ef:
            if (r0 != 0) goto L_0x004d
            goto L_0x0042
        L_0x00f3:
            r0 = r6
            goto L_0x00ef
        L_0x00f5:
            boolean r0 = r13.zzako
            if (r0 == 0) goto L_0x0105
            com.google.android.gms.internal.measurement.zzen<?> r0 = r13.zzaky
            com.google.android.gms.internal.measurement.zzeo r0 = r0.zzh(r14)
            boolean r0 = r0.isInitialized()
            if (r0 == 0) goto L_0x0042
        L_0x0105:
            r2 = r6
            goto L_0x0042
        L_0x0108:
            r5 = r0
            goto L_0x0034
        L_0x010b:
            r5 = r2
            goto L_0x0034
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgm.zzv(java.lang.Object):boolean");
    }
}
