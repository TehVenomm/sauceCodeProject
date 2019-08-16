package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.util.List;

final class zzec implements zzgy {
    private int tag;
    private final zzeb zzadu;
    private int zzadv;
    private int zzadw = 0;

    private zzec(zzeb zzeb) {
        this.zzadu = (zzeb) zzez.zza(zzeb, "input");
        this.zzadu.zzads = this;
    }

    public static zzec zza(zzeb zzeb) {
        return zzeb.zzads != null ? zzeb.zzads : new zzec(zzeb);
    }

    private final Object zza(zzig zzig, Class<?> cls, zzel zzel) throws IOException {
        switch (zzef.zzaee[zzig.ordinal()]) {
            case 1:
                return Boolean.valueOf(zzsm());
            case 2:
                return zzso();
            case 3:
                return Double.valueOf(readDouble());
            case 4:
                return Integer.valueOf(zzsq());
            case 5:
                return Integer.valueOf(zzsl());
            case 6:
                return Long.valueOf(zzsk());
            case 7:
                return Float.valueOf(readFloat());
            case 8:
                return Integer.valueOf(zzsj());
            case 9:
                return Long.valueOf(zzsi());
            case 10:
                zzba(2);
                return zzc(zzgt.zzvy().zzf(cls), zzel);
            case 11:
                return Integer.valueOf(zzsr());
            case 12:
                return Long.valueOf(zzss());
            case 13:
                return Integer.valueOf(zzst());
            case 14:
                return Long.valueOf(zzsu());
            case 15:
                return zzsn();
            case 16:
                return Integer.valueOf(zzsp());
            case 17:
                return Long.valueOf(zzsh());
            default:
                throw new RuntimeException("unsupported field type.");
        }
    }

    private final void zza(List<String> list, boolean z) throws IOException {
        int zzsg;
        int zzsg2;
        if ((this.tag & 7) != 2) {
            throw zzfi.zzuy();
        } else if (!(list instanceof zzfp) || z) {
            do {
                list.add(z ? zzsn() : readString());
                if (!this.zzadu.zzsw()) {
                    zzsg = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg == this.tag);
            this.zzadw = zzsg;
        } else {
            zzfp zzfp = (zzfp) list;
            do {
                zzfp.zzc(zzso());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
        }
    }

    private final void zzba(int i) throws IOException {
        if ((this.tag & 7) != i) {
            throw zzfi.zzuy();
        }
    }

    private static void zzbb(int i) throws IOException {
        if ((i & 7) != 0) {
            throw zzfi.zzva();
        }
    }

    private static void zzbc(int i) throws IOException {
        if ((i & 3) != 0) {
            throw zzfi.zzva();
        }
    }

    private final void zzbd(int i) throws IOException {
        if (this.zzadu.zzsx() != i) {
            throw zzfi.zzut();
        }
    }

    private final <T> T zzc(zzgx<T> zzgx, zzel zzel) throws IOException {
        int zzsp = this.zzadu.zzsp();
        if (this.zzadu.zzadp >= this.zzadu.zzadq) {
            throw zzfi.zzuz();
        }
        int zzaw = this.zzadu.zzaw(zzsp);
        T newInstance = zzgx.newInstance();
        this.zzadu.zzadp++;
        zzgx.zza(newInstance, this, zzel);
        zzgx.zzj(newInstance);
        this.zzadu.zzat(0);
        this.zzadu.zzadp--;
        this.zzadu.zzax(zzaw);
        return newInstance;
    }

    private final <T> T zzd(zzgx<T> zzgx, zzel zzel) throws IOException {
        int i = this.zzadv;
        this.zzadv = ((this.tag >>> 3) << 3) | 4;
        try {
            T newInstance = zzgx.newInstance();
            zzgx.zza(newInstance, this, zzel);
            zzgx.zzj(newInstance);
            if (this.tag == this.zzadv) {
                return newInstance;
            }
            throw zzfi.zzva();
        } finally {
            this.zzadv = i;
        }
    }

    public final int getTag() {
        return this.tag;
    }

    public final double readDouble() throws IOException {
        zzba(1);
        return this.zzadu.readDouble();
    }

    public final float readFloat() throws IOException {
        zzba(5);
        return this.zzadu.readFloat();
    }

    public final String readString() throws IOException {
        zzba(2);
        return this.zzadu.readString();
    }

    public final void readStringList(List<String> list) throws IOException {
        zza(list, false);
    }

    public final <T> T zza(zzgx<T> zzgx, zzel zzel) throws IOException {
        zzba(2);
        return zzc(zzgx, zzel);
    }

    public final <T> void zza(List<T> list, zzgx<T> zzgx, zzel zzel) throws IOException {
        int zzsg;
        if ((this.tag & 7) != 2) {
            throw zzfi.zzuy();
        }
        int i = this.tag;
        do {
            list.add(zzc(zzgx, zzel));
            if (!this.zzadu.zzsw() && this.zzadw == 0) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == i);
        this.zzadw = zzsg;
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final <K, V> void zza(java.util.Map<K, V> r7, com.google.android.gms.internal.measurement.zzfz<K, V> r8, com.google.android.gms.internal.measurement.zzel r9) throws java.io.IOException {
        /*
            r6 = this;
            r0 = 2
            r6.zzba(r0)
            com.google.android.gms.internal.measurement.zzeb r0 = r6.zzadu
            int r0 = r0.zzsp()
            com.google.android.gms.internal.measurement.zzeb r1 = r6.zzadu
            int r2 = r1.zzaw(r0)
            K r1 = r8.zzakc
            V r0 = r8.zzaba
        L_0x0014:
            int r3 = r6.zzsy()     // Catch:{ all -> 0x0045 }
            r4 = 2147483647(0x7fffffff, float:NaN)
            if (r3 == r4) goto L_0x0062
            com.google.android.gms.internal.measurement.zzeb r4 = r6.zzadu     // Catch:{ all -> 0x0045 }
            boolean r4 = r4.zzsw()     // Catch:{ all -> 0x0045 }
            if (r4 != 0) goto L_0x0062
            switch(r3) {
                case 1: goto L_0x004c;
                case 2: goto L_0x0055;
                default: goto L_0x0028;
            }
        L_0x0028:
            boolean r3 = r6.zzsz()     // Catch:{ zzfh -> 0x0036 }
            if (r3 != 0) goto L_0x0014
            com.google.android.gms.internal.measurement.zzfi r3 = new com.google.android.gms.internal.measurement.zzfi     // Catch:{ zzfh -> 0x0036 }
            java.lang.String r4 = "Unable to parse map entry."
            r3.<init>(r4)     // Catch:{ zzfh -> 0x0036 }
            throw r3     // Catch:{ zzfh -> 0x0036 }
        L_0x0036:
            r3 = move-exception
            boolean r3 = r6.zzsz()     // Catch:{ all -> 0x0045 }
            if (r3 != 0) goto L_0x0014
            com.google.android.gms.internal.measurement.zzfi r0 = new com.google.android.gms.internal.measurement.zzfi     // Catch:{ all -> 0x0045 }
            java.lang.String r1 = "Unable to parse map entry."
            r0.<init>(r1)     // Catch:{ all -> 0x0045 }
            throw r0     // Catch:{ all -> 0x0045 }
        L_0x0045:
            r0 = move-exception
            com.google.android.gms.internal.measurement.zzeb r1 = r6.zzadu
            r1.zzax(r2)
            throw r0
        L_0x004c:
            com.google.android.gms.internal.measurement.zzig r3 = r8.zzakb     // Catch:{ zzfh -> 0x0036 }
            r4 = 0
            r5 = 0
            java.lang.Object r1 = r6.zza(r3, r4, r5)     // Catch:{ zzfh -> 0x0036 }
            goto L_0x0014
        L_0x0055:
            com.google.android.gms.internal.measurement.zzig r3 = r8.zzakd     // Catch:{ zzfh -> 0x0036 }
            V r4 = r8.zzaba     // Catch:{ zzfh -> 0x0036 }
            java.lang.Class r4 = r4.getClass()     // Catch:{ zzfh -> 0x0036 }
            java.lang.Object r0 = r6.zza(r3, r4, r9)     // Catch:{ zzfh -> 0x0036 }
            goto L_0x0014
        L_0x0062:
            r7.put(r1, r0)     // Catch:{ all -> 0x0045 }
            com.google.android.gms.internal.measurement.zzeb r0 = r6.zzadu
            r0.zzax(r2)
            return
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzec.zza(java.util.Map, com.google.android.gms.internal.measurement.zzfz, com.google.android.gms.internal.measurement.zzel):void");
    }

    public final <T> T zzb(zzgx<T> zzgx, zzel zzel) throws IOException {
        zzba(3);
        return zzd(zzgx, zzel);
    }

    public final <T> void zzb(List<T> list, zzgx<T> zzgx, zzel zzel) throws IOException {
        int zzsg;
        if ((this.tag & 7) != 3) {
            throw zzfi.zzuy();
        }
        int i = this.tag;
        do {
            list.add(zzd(zzgx, zzel));
            if (!this.zzadu.zzsw() && this.zzadw == 0) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == i);
        this.zzadw = zzsg;
    }

    public final void zze(List<Double> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzeh) {
            zzeh zzeh = (zzeh) list;
            switch (this.tag & 7) {
                case 1:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbb(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzeh.zzf(this.zzadu.readDouble());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzeh.zzf(this.zzadu.readDouble());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 1:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbb(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Double.valueOf(this.zzadu.readDouble()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Double.valueOf(this.zzadu.readDouble()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzf(List<Float> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzeu) {
            zzeu zzeu = (zzeu) list;
            switch (this.tag & 7) {
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbc(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzeu.zzc(this.zzadu.readFloat());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                case 5:
                    break;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzeu.zzc(this.zzadu.readFloat());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbc(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Float.valueOf(this.zzadu.readFloat()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            case 5:
                break;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Float.valueOf(this.zzadu.readFloat()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzg(List<Long> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfw.zzby(this.zzadu.zzsh());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfw.zzby(this.zzadu.zzsh());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Long.valueOf(this.zzadu.zzsh()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Long.valueOf(this.zzadu.zzsh()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzh(List<Long> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfw.zzby(this.zzadu.zzsi());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfw.zzby(this.zzadu.zzsi());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Long.valueOf(this.zzadu.zzsi()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Long.valueOf(this.zzadu.zzsi()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzi(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzsj());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzsj());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzsj()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzsj()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzj(List<Long> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            switch (this.tag & 7) {
                case 1:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbb(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzfw.zzby(this.zzadu.zzsk());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfw.zzby(this.zzadu.zzsk());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 1:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbb(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Long.valueOf(this.zzadu.zzsk()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Long.valueOf(this.zzadu.zzsk()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzk(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbc(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzsl());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                case 5:
                    break;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzsl());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbc(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzsl()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            case 5:
                break;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzsl()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzl(List<Boolean> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzdn) {
            zzdn zzdn = (zzdn) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzdn.addBoolean(this.zzadu.zzsm());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzdn.addBoolean(this.zzadu.zzsm());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Boolean.valueOf(this.zzadu.zzsm()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Boolean.valueOf(this.zzadu.zzsm()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzm(List<String> list) throws IOException {
        zza(list, true);
    }

    public final void zzn(List<zzdp> list) throws IOException {
        int zzsg;
        if ((this.tag & 7) != 2) {
            throw zzfi.zzuy();
        }
        do {
            list.add(zzso());
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzo(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzsp());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzsp());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzsp()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzsp()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzp(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzsq());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzsq());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzsq()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzsq()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzq(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbc(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzsr());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                case 5:
                    break;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzsr());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbc(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzsr()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            case 5:
                break;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzsr()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzr(List<Long> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            switch (this.tag & 7) {
                case 1:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp();
                    zzbb(zzsp);
                    int zzsx = this.zzadu.zzsx();
                    do {
                        zzfw.zzby(this.zzadu.zzss());
                    } while (this.zzadu.zzsx() < zzsp + zzsx);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfw.zzby(this.zzadu.zzss());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 1:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp();
                zzbb(zzsp2);
                int zzsx2 = this.zzadu.zzsx();
                do {
                    list.add(Long.valueOf(this.zzadu.zzss()));
                } while (this.zzadu.zzsx() < zzsp2 + zzsx2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Long.valueOf(this.zzadu.zzss()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final void zzs(List<Integer> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfa) {
            zzfa zzfa = (zzfa) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfa.zzbu(this.zzadu.zzst());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfa.zzbu(this.zzadu.zzst());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Integer.valueOf(this.zzadu.zzst()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Integer.valueOf(this.zzadu.zzst()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }

    public final long zzsh() throws IOException {
        zzba(0);
        return this.zzadu.zzsh();
    }

    public final long zzsi() throws IOException {
        zzba(0);
        return this.zzadu.zzsi();
    }

    public final int zzsj() throws IOException {
        zzba(0);
        return this.zzadu.zzsj();
    }

    public final long zzsk() throws IOException {
        zzba(1);
        return this.zzadu.zzsk();
    }

    public final int zzsl() throws IOException {
        zzba(5);
        return this.zzadu.zzsl();
    }

    public final boolean zzsm() throws IOException {
        zzba(0);
        return this.zzadu.zzsm();
    }

    public final String zzsn() throws IOException {
        zzba(2);
        return this.zzadu.zzsn();
    }

    public final zzdp zzso() throws IOException {
        zzba(2);
        return this.zzadu.zzso();
    }

    public final int zzsp() throws IOException {
        zzba(0);
        return this.zzadu.zzsp();
    }

    public final int zzsq() throws IOException {
        zzba(0);
        return this.zzadu.zzsq();
    }

    public final int zzsr() throws IOException {
        zzba(5);
        return this.zzadu.zzsr();
    }

    public final long zzss() throws IOException {
        zzba(1);
        return this.zzadu.zzss();
    }

    public final int zzst() throws IOException {
        zzba(0);
        return this.zzadu.zzst();
    }

    public final long zzsu() throws IOException {
        zzba(0);
        return this.zzadu.zzsu();
    }

    public final int zzsy() throws IOException {
        if (this.zzadw != 0) {
            this.tag = this.zzadw;
            this.zzadw = 0;
        } else {
            this.tag = this.zzadu.zzsg();
        }
        if (this.tag == 0 || this.tag == this.zzadv) {
            return Integer.MAX_VALUE;
        }
        return this.tag >>> 3;
    }

    public final boolean zzsz() throws IOException {
        if (this.zzadu.zzsw() || this.tag == this.zzadv) {
            return false;
        }
        return this.zzadu.zzau(this.tag);
    }

    public final void zzt(List<Long> list) throws IOException {
        int zzsg;
        int zzsg2;
        if (list instanceof zzfw) {
            zzfw zzfw = (zzfw) list;
            switch (this.tag & 7) {
                case 0:
                    break;
                case 2:
                    int zzsp = this.zzadu.zzsp() + this.zzadu.zzsx();
                    do {
                        zzfw.zzby(this.zzadu.zzsu());
                    } while (this.zzadu.zzsx() < zzsp);
                    zzbd(zzsp);
                    return;
                default:
                    throw zzfi.zzuy();
            }
            do {
                zzfw.zzby(this.zzadu.zzsu());
                if (!this.zzadu.zzsw()) {
                    zzsg2 = this.zzadu.zzsg();
                } else {
                    return;
                }
            } while (zzsg2 == this.tag);
            this.zzadw = zzsg2;
            return;
        }
        switch (this.tag & 7) {
            case 0:
                break;
            case 2:
                int zzsp2 = this.zzadu.zzsp() + this.zzadu.zzsx();
                do {
                    list.add(Long.valueOf(this.zzadu.zzsu()));
                } while (this.zzadu.zzsx() < zzsp2);
                zzbd(zzsp2);
                return;
            default:
                throw zzfi.zzuy();
        }
        do {
            list.add(Long.valueOf(this.zzadu.zzsu()));
            if (!this.zzadu.zzsw()) {
                zzsg = this.zzadu.zzsg();
            } else {
                return;
            }
        } while (zzsg == this.tag);
        this.zzadw = zzsg;
    }
}
