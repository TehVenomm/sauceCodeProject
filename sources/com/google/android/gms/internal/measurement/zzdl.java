package com.google.android.gms.internal.measurement;

import com.google.android.gms.games.Notifications;
import java.io.IOException;

final class zzdl {
    static int zza(int i, byte[] bArr, int i2, int i3, zzdk zzdk) throws zzfi {
        if ((i >>> 3) == 0) {
            throw zzfi.zzuw();
        }
        switch (i & 7) {
            case 0:
                return zzb(bArr, i2, zzdk);
            case 1:
                return i2 + 8;
            case 2:
                return zza(bArr, i2, zzdk) + zzdk.zzada;
            case 3:
                int i4 = (i & -8) | 4;
                int i5 = 0;
                int i6 = i2;
                while (i6 < i3) {
                    i6 = zza(bArr, i6, zzdk);
                    i5 = zzdk.zzada;
                    if (i5 != i4) {
                        i6 = zza(i5, bArr, i6, i3, zzdk);
                    } else if (i6 > i3 && i5 == i4) {
                        return i6;
                    } else {
                        throw zzfi.zzva();
                    }
                }
                if (i6 > i3) {
                }
                throw zzfi.zzva();
            case 5:
                return i2 + 4;
            default:
                throw zzfi.zzuw();
        }
    }

    static int zza(int i, byte[] bArr, int i2, int i3, zzff<?> zzff, zzdk zzdk) {
        zzfa zzfa = (zzfa) zzff;
        int zza = zza(bArr, i2, zzdk);
        zzfa.zzbu(zzdk.zzada);
        while (zza < i3) {
            int zza2 = zza(bArr, zza, zzdk);
            if (i != zzdk.zzada) {
                break;
            }
            zza = zza(bArr, zza2, zzdk);
            zzfa.zzbu(zzdk.zzada);
        }
        return zza;
    }

    static int zza(int i, byte[] bArr, int i2, int i3, zzhs zzhs, zzdk zzdk) throws zzfi {
        if ((i >>> 3) == 0) {
            throw zzfi.zzuw();
        }
        switch (i & 7) {
            case 0:
                int zzb = zzb(bArr, i2, zzdk);
                zzhs.zzb(i, (Object) Long.valueOf(zzdk.zzadb));
                return zzb;
            case 1:
                zzhs.zzb(i, (Object) Long.valueOf(zzb(bArr, i2)));
                return i2 + 8;
            case 2:
                int zza = zza(bArr, i2, zzdk);
                int i4 = zzdk.zzada;
                if (i4 < 0) {
                    throw zzfi.zzuu();
                } else if (i4 > bArr.length - zza) {
                    throw zzfi.zzut();
                } else {
                    if (i4 == 0) {
                        zzhs.zzb(i, (Object) zzdp.zzadh);
                    } else {
                        zzhs.zzb(i, (Object) zzdp.zzb(bArr, zza, i4));
                    }
                    return zza + i4;
                }
            case 3:
                zzhs zzwr = zzhs.zzwr();
                int i5 = (i & -8) | 4;
                int i6 = 0;
                int i7 = i2;
                while (true) {
                    if (i7 < i3) {
                        int zza2 = zza(bArr, i7, zzdk);
                        i6 = zzdk.zzada;
                        if (i6 != i5) {
                            i7 = zza(i6, bArr, zza2, i3, zzwr, zzdk);
                        } else {
                            i7 = zza2;
                        }
                    }
                }
                if (i7 > i3 || i6 != i5) {
                    throw zzfi.zzva();
                }
                zzhs.zzb(i, (Object) zzwr);
                return i7;
            case 5:
                zzhs.zzb(i, (Object) Integer.valueOf(zza(bArr, i2)));
                return i2 + 4;
            default:
                throw zzfi.zzuw();
        }
    }

    static int zza(int i, byte[] bArr, int i2, zzdk zzdk) {
        int i3 = i & Notifications.NOTIFICATION_TYPES_ALL;
        int i4 = i2 + 1;
        byte b = bArr[i2];
        if (b >= 0) {
            zzdk.zzada = i3 | (b << 7);
            return i4;
        }
        int i5 = ((b & Byte.MAX_VALUE) << 7) | i3;
        int i6 = i4 + 1;
        byte b2 = bArr[i4];
        if (b2 >= 0) {
            zzdk.zzada = (b2 << 14) | i5;
            return i6;
        }
        int i7 = i5 | ((b2 & Byte.MAX_VALUE) << 14);
        int i8 = i6 + 1;
        byte b3 = bArr[i6];
        if (b3 >= 0) {
            zzdk.zzada = (b3 << 21) | i7;
            return i8;
        }
        int i9 = i7 | ((b3 & Byte.MAX_VALUE) << 21);
        int i10 = i8 + 1;
        byte b4 = bArr[i8];
        if (b4 >= 0) {
            zzdk.zzada = (b4 << 28) | i9;
            return i10;
        }
        while (true) {
            int i11 = i10 + 1;
            if (bArr[i10] >= 0) {
                zzdk.zzada = ((b4 & Byte.MAX_VALUE) << 28) | i9;
                return i11;
            }
            i10 = i11;
        }
    }

    static int zza(zzgx<?> zzgx, int i, byte[] bArr, int i2, int i3, zzff<?> zzff, zzdk zzdk) throws IOException {
        int zza = zza((zzgx) zzgx, bArr, i2, i3, zzdk);
        zzff.add(zzdk.zzadc);
        while (zza < i3) {
            int zza2 = zza(bArr, zza, zzdk);
            if (i != zzdk.zzada) {
                break;
            }
            zza = zza((zzgx) zzgx, bArr, zza2, i3, zzdk);
            zzff.add(zzdk.zzadc);
        }
        return zza;
    }

    static int zza(zzgx zzgx, byte[] bArr, int i, int i2, int i3, zzdk zzdk) throws IOException {
        zzgm zzgm = (zzgm) zzgx;
        Object newInstance = zzgm.newInstance();
        int zza = zzgm.zza(newInstance, bArr, i, i2, i3, zzdk);
        zzgm.zzj(newInstance);
        zzdk.zzadc = newInstance;
        return zza;
    }

    static int zza(zzgx zzgx, byte[] bArr, int i, int i2, zzdk zzdk) throws IOException {
        int i3;
        int i4 = i + 1;
        byte b = bArr[i];
        if (b < 0) {
            i4 = zza((int) b, bArr, i4, zzdk);
            i3 = zzdk.zzada;
        } else {
            i3 = b;
        }
        if (i3 < 0 || i3 > i2 - i4) {
            throw zzfi.zzut();
        }
        Object newInstance = zzgx.newInstance();
        zzgx.zza(newInstance, bArr, i4, i4 + i3, zzdk);
        zzgx.zzj(newInstance);
        zzdk.zzadc = newInstance;
        return i4 + i3;
    }

    static int zza(byte[] bArr, int i) {
        return (bArr[i] & 255) | ((bArr[i + 1] & 255) << 8) | ((bArr[i + 2] & 255) << 16) | ((bArr[i + 3] & 255) << 24);
    }

    static int zza(byte[] bArr, int i, zzdk zzdk) {
        int i2 = i + 1;
        byte b = bArr[i];
        if (b < 0) {
            return zza((int) b, bArr, i2, zzdk);
        }
        zzdk.zzada = b;
        return i2;
    }

    static int zza(byte[] bArr, int i, zzff<?> zzff, zzdk zzdk) throws IOException {
        zzfa zzfa = (zzfa) zzff;
        int zza = zza(bArr, i, zzdk);
        int i2 = zzdk.zzada + zza;
        while (zza < i2) {
            zza = zza(bArr, zza, zzdk);
            zzfa.zzbu(zzdk.zzada);
        }
        if (zza == i2) {
            return zza;
        }
        throw zzfi.zzut();
    }

    static int zzb(byte[] bArr, int i, zzdk zzdk) {
        int i2 = 7;
        int i3 = i + 1;
        long j = (long) bArr[i];
        if (j >= 0) {
            zzdk.zzadb = j;
        } else {
            byte b = bArr[i3];
            i3++;
            long j2 = (j & 127) | (((long) (b & Byte.MAX_VALUE)) << 7);
            while (b < 0) {
                b = bArr[i3];
                i2 += 7;
                j2 |= ((long) (b & Byte.MAX_VALUE)) << i2;
                i3++;
            }
            zzdk.zzadb = j2;
        }
        return i3;
    }

    static long zzb(byte[] bArr, int i) {
        return (((long) bArr[i]) & 255) | ((((long) bArr[i + 1]) & 255) << 8) | ((((long) bArr[i + 2]) & 255) << 16) | ((((long) bArr[i + 3]) & 255) << 24) | ((((long) bArr[i + 4]) & 255) << 32) | ((((long) bArr[i + 5]) & 255) << 40) | ((((long) bArr[i + 6]) & 255) << 48) | ((((long) bArr[i + 7]) & 255) << 56);
    }

    static double zzc(byte[] bArr, int i) {
        return Double.longBitsToDouble(zzb(bArr, i));
    }

    static int zzc(byte[] bArr, int i, zzdk zzdk) throws zzfi {
        int zza = zza(bArr, i, zzdk);
        int i2 = zzdk.zzada;
        if (i2 < 0) {
            throw zzfi.zzuu();
        } else if (i2 == 0) {
            zzdk.zzadc = "";
            return zza;
        } else {
            zzdk.zzadc = new String(bArr, zza, i2, zzez.UTF_8);
            return zza + i2;
        }
    }

    static float zzd(byte[] bArr, int i) {
        return Float.intBitsToFloat(zza(bArr, i));
    }

    static int zzd(byte[] bArr, int i, zzdk zzdk) throws zzfi {
        int zza = zza(bArr, i, zzdk);
        int i2 = zzdk.zzada;
        if (i2 < 0) {
            throw zzfi.zzuu();
        } else if (i2 == 0) {
            zzdk.zzadc = "";
            return zza;
        } else {
            zzdk.zzadc = zzhy.zzh(bArr, zza, i2);
            return zza + i2;
        }
    }

    static int zze(byte[] bArr, int i, zzdk zzdk) throws zzfi {
        int zza = zza(bArr, i, zzdk);
        int i2 = zzdk.zzada;
        if (i2 < 0) {
            throw zzfi.zzuu();
        } else if (i2 > bArr.length - zza) {
            throw zzfi.zzut();
        } else if (i2 == 0) {
            zzdk.zzadc = zzdp.zzadh;
            return zza;
        } else {
            zzdk.zzadc = zzdp.zzb(bArr, zza, i2);
            return zza + i2;
        }
    }
}
