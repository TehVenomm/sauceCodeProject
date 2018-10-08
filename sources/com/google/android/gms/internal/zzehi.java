package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import android.support.v4.view.MotionEventCompat;
import com.facebook.GraphRequest;
import com.github.droidfu.support.DisplaySupport;
import java.io.IOException;
import java.util.Arrays;

public final class zzehi extends zzegi<zzehi> implements Cloneable {
    private String tag;
    private int zzaka;
    private boolean zzlua;
    private zzehk zzmnw;
    public long zznfy;
    public long zznfz;
    private long zznga;
    private int zzngb;
    private zzehj[] zzngc;
    private byte[] zzngd;
    private zzehg zznge;
    public byte[] zzngf;
    private String zzngg;
    private String zzngh;
    private zzehf zzngi;
    private String zzngj;
    public long zzngk;
    private zzehh zzngl;
    public byte[] zzngm;
    private String zzngn;
    private int zzngo;
    private int[] zzngp;
    private long zzngq;

    public zzehi() {
        this.zznfy = 0;
        this.zznfz = 0;
        this.zznga = 0;
        this.tag = "";
        this.zzngb = 0;
        this.zzaka = 0;
        this.zzlua = false;
        this.zzngc = zzehj.zzceo();
        this.zzngd = zzegr.zzndo;
        this.zznge = null;
        this.zzngf = zzegr.zzndo;
        this.zzngg = "";
        this.zzngh = "";
        this.zzngi = null;
        this.zzngj = "";
        this.zzngk = 180000;
        this.zzngl = null;
        this.zzngm = zzegr.zzndo;
        this.zzngn = "";
        this.zzngo = 0;
        this.zzngp = zzegr.zzndi;
        this.zzngq = 0;
        this.zzmnw = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    private final zzehi zzcen() {
        try {
            zzehi zzehi = (zzehi) super.zzcdy();
            if (this.zzngc != null && this.zzngc.length > 0) {
                zzehi.zzngc = new zzehj[this.zzngc.length];
                for (int i = 0; i < this.zzngc.length; i++) {
                    if (this.zzngc[i] != null) {
                        zzehi.zzngc[i] = (zzehj) this.zzngc[i].clone();
                    }
                }
            }
            if (this.zznge != null) {
                zzehi.zznge = (zzehg) this.zznge.clone();
            }
            if (this.zzngi != null) {
                zzehi.zzngi = (zzehf) this.zzngi.clone();
            }
            if (this.zzngl != null) {
                zzehi.zzngl = (zzehh) this.zzngl.clone();
            }
            if (this.zzngp != null && this.zzngp.length > 0) {
                zzehi.zzngp = (int[]) this.zzngp.clone();
            }
            if (this.zzmnw != null) {
                zzehi.zzmnw = (zzehk) this.zzmnw.clone();
            }
            return zzehi;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcen();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehi)) {
            return false;
        }
        zzehi zzehi = (zzehi) obj;
        if (this.zznfy != zzehi.zznfy) {
            return false;
        }
        if (this.zznfz != zzehi.zznfz) {
            return false;
        }
        if (this.zznga != zzehi.zznga) {
            return false;
        }
        if (this.tag == null) {
            if (zzehi.tag != null) {
                return false;
            }
        } else if (!this.tag.equals(zzehi.tag)) {
            return false;
        }
        if (this.zzngb != zzehi.zzngb) {
            return false;
        }
        if (this.zzaka != zzehi.zzaka) {
            return false;
        }
        if (this.zzlua != zzehi.zzlua) {
            return false;
        }
        if (!zzegm.equals(this.zzngc, zzehi.zzngc)) {
            return false;
        }
        if (!Arrays.equals(this.zzngd, zzehi.zzngd)) {
            return false;
        }
        if (this.zznge == null) {
            if (zzehi.zznge != null) {
                return false;
            }
        } else if (!this.zznge.equals(zzehi.zznge)) {
            return false;
        }
        if (!Arrays.equals(this.zzngf, zzehi.zzngf)) {
            return false;
        }
        if (this.zzngg == null) {
            if (zzehi.zzngg != null) {
                return false;
            }
        } else if (!this.zzngg.equals(zzehi.zzngg)) {
            return false;
        }
        if (this.zzngh == null) {
            if (zzehi.zzngh != null) {
                return false;
            }
        } else if (!this.zzngh.equals(zzehi.zzngh)) {
            return false;
        }
        if (this.zzngi == null) {
            if (zzehi.zzngi != null) {
                return false;
            }
        } else if (!this.zzngi.equals(zzehi.zzngi)) {
            return false;
        }
        if (this.zzngj == null) {
            if (zzehi.zzngj != null) {
                return false;
            }
        } else if (!this.zzngj.equals(zzehi.zzngj)) {
            return false;
        }
        if (this.zzngk != zzehi.zzngk) {
            return false;
        }
        if (this.zzngl == null) {
            if (zzehi.zzngl != null) {
                return false;
            }
        } else if (!this.zzngl.equals(zzehi.zzngl)) {
            return false;
        }
        if (!Arrays.equals(this.zzngm, zzehi.zzngm)) {
            return false;
        }
        if (this.zzngn == null) {
            if (zzehi.zzngn != null) {
                return false;
            }
        } else if (!this.zzngn.equals(zzehi.zzngn)) {
            return false;
        }
        if (this.zzngo != zzehi.zzngo) {
            return false;
        }
        if (!zzegm.equals(this.zzngp, zzehi.zzngp)) {
            return false;
        }
        if (this.zzngq != zzehi.zzngq) {
            return false;
        }
        if (this.zzmnw == null) {
            if (zzehi.zzmnw != null) {
                return false;
            }
        } else if (!this.zzmnw.equals(zzehi.zzmnw)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzehi.zzncu == null || zzehi.zzncu.isEmpty() : this.zzncu.equals(zzehi.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int i2 = (int) (this.zznfy ^ (this.zznfy >>> 32));
        int i3 = (int) (this.zznfz ^ (this.zznfz >>> 32));
        int i4 = (int) (this.zznga ^ (this.zznga >>> 32));
        int hashCode2 = this.tag == null ? 0 : this.tag.hashCode();
        int i5 = this.zzngb;
        int i6 = this.zzaka;
        int i7 = this.zzlua ? 1231 : 1237;
        int hashCode3 = zzegm.hashCode(this.zzngc);
        int hashCode4 = Arrays.hashCode(this.zzngd);
        int hashCode5 = this.zznge == null ? 0 : this.zznge.hashCode();
        int hashCode6 = Arrays.hashCode(this.zzngf);
        int hashCode7 = this.zzngg == null ? 0 : this.zzngg.hashCode();
        int hashCode8 = this.zzngh == null ? 0 : this.zzngh.hashCode();
        int hashCode9 = this.zzngi == null ? 0 : this.zzngi.hashCode();
        int hashCode10 = this.zzngj == null ? 0 : this.zzngj.hashCode();
        int i8 = (int) (this.zzngk ^ (this.zzngk >>> 32));
        int hashCode11 = this.zzngl == null ? 0 : this.zzngl.hashCode();
        int hashCode12 = Arrays.hashCode(this.zzngm);
        int hashCode13 = this.zzngn == null ? 0 : this.zzngn.hashCode();
        int i9 = this.zzngo;
        int hashCode14 = zzegm.hashCode(this.zzngp);
        int i10 = (int) (this.zzngq ^ (this.zzngq >>> 32));
        int hashCode15 = this.zzmnw == null ? 0 : this.zzmnw.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((((((((((((((((((((((((((((((((hashCode2 + ((((((((hashCode + 527) * 31) + i2) * 31) + i3) * 31) + i4) * 31)) * 31) + i5) * 31) + i6) * 31) + i7) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + hashCode7) * 31) + hashCode8) * 31) + hashCode9) * 31) + hashCode10) * 31) + i8) * 31) + hashCode11) * 31) + hashCode12) * 31) + hashCode13) * 31) + i9) * 31) + hashCode14) * 31) + i10) * 31) + hashCode15) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int zzb;
            Object obj;
            int zzcbs;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zznfy = zzegf.zzcdr();
                    continue;
                case 18:
                    this.tag = zzegf.readString();
                    continue;
                case 26:
                    zzb = zzegr.zzb(zzegf, 26);
                    zzcbr = this.zzngc == null ? 0 : this.zzngc.length;
                    obj = new zzehj[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzngc, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzehj();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzehj();
                    zzegf.zza(obj[zzcbr]);
                    this.zzngc = obj;
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    this.zzngd = zzegf.readBytes();
                    continue;
                case GraphRequest.MAXIMUM_BATCH_SIZE /*50*/:
                    this.zzngf = zzegf.readBytes();
                    continue;
                case 58:
                    if (this.zzngi == null) {
                        this.zzngi = new zzehf();
                    }
                    zzegf.zza(this.zzngi);
                    continue;
                case 66:
                    this.zzngg = zzegf.readString();
                    continue;
                case 74:
                    if (this.zznge == null) {
                        this.zznge = new zzehg();
                    }
                    zzegf.zza(this.zznge);
                    continue;
                case 80:
                    this.zzlua = zzegf.zzcds();
                    continue;
                case 88:
                    this.zzngb = zzegf.zzcbs();
                    continue;
                case 96:
                    this.zzaka = zzegf.zzcbs();
                    continue;
                case 106:
                    this.zzngh = zzegf.readString();
                    continue;
                case 114:
                    this.zzngj = zzegf.readString();
                    continue;
                case DisplaySupport.SCREEN_DENSITY_LOW /*120*/:
                    this.zzngk = zzegf.zzcdt();
                    continue;
                case TransportMediator.KEYCODE_MEDIA_RECORD /*130*/:
                    if (this.zzngl == null) {
                        this.zzngl = new zzehh();
                    }
                    zzegf.zza(this.zzngl);
                    continue;
                case 136:
                    this.zznfz = zzegf.zzcdr();
                    continue;
                case 146:
                    this.zzngm = zzegf.readBytes();
                    continue;
                case 152:
                    zzb = zzegf.getPosition();
                    zzcbs = zzegf.zzcbs();
                    switch (zzcbs) {
                        case 0:
                        case 1:
                        case 2:
                            this.zzngo = zzcbs;
                            break;
                        default:
                            zzegf.zzha(zzb);
                            zza(zzegf, zzcbr);
                            continue;
                    }
                case DisplaySupport.SCREEN_DENSITY_MEDIUM /*160*/:
                    zzb = zzegr.zzb(zzegf, DisplaySupport.SCREEN_DENSITY_MEDIUM);
                    zzcbr = this.zzngp == null ? 0 : this.zzngp.length;
                    obj = new int[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzngp, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcbs();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcbs();
                    this.zzngp = obj;
                    continue;
                case 162:
                    zzcbs = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcbs();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zzngp == null ? 0 : this.zzngp.length;
                    Object obj2 = new int[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zzngp, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcbs();
                        zzb++;
                    }
                    this.zzngp = obj2;
                    zzegf.zzgn(zzcbs);
                    continue;
                case 168:
                    this.zznga = zzegf.zzcdr();
                    continue;
                case 176:
                    this.zzngq = zzegf.zzcdr();
                    continue;
                case 186:
                    if (this.zzmnw == null) {
                        this.zzmnw = new zzehk();
                    }
                    zzegf.zza(this.zzmnw);
                    continue;
                case 194:
                    this.zzngn = zzegf.readString();
                    continue;
                default:
                    if (!super.zza(zzegf, zzcbr)) {
                        break;
                    }
                    continue;
            }
            return this;
        }
    }

    public final void zza(zzegg zzegg) throws IOException {
        int i = 0;
        if (this.zznfy != 0) {
            zzegg.zzb(1, this.zznfy);
        }
        if (!(this.tag == null || this.tag.equals(""))) {
            zzegg.zzl(2, this.tag);
        }
        if (this.zzngc != null && this.zzngc.length > 0) {
            for (zzego zzego : this.zzngc) {
                if (zzego != null) {
                    zzegg.zza(3, zzego);
                }
            }
        }
        if (!Arrays.equals(this.zzngd, zzegr.zzndo)) {
            zzegg.zzc(4, this.zzngd);
        }
        if (!Arrays.equals(this.zzngf, zzegr.zzndo)) {
            zzegg.zzc(6, this.zzngf);
        }
        if (this.zzngi != null) {
            zzegg.zza(7, this.zzngi);
        }
        if (!(this.zzngg == null || this.zzngg.equals(""))) {
            zzegg.zzl(8, this.zzngg);
        }
        if (this.zznge != null) {
            zzegg.zza(9, this.zznge);
        }
        if (this.zzlua) {
            zzegg.zzl(10, this.zzlua);
        }
        if (this.zzngb != 0) {
            zzegg.zzu(11, this.zzngb);
        }
        if (this.zzaka != 0) {
            zzegg.zzu(12, this.zzaka);
        }
        if (!(this.zzngh == null || this.zzngh.equals(""))) {
            zzegg.zzl(13, this.zzngh);
        }
        if (!(this.zzngj == null || this.zzngj.equals(""))) {
            zzegg.zzl(14, this.zzngj);
        }
        if (this.zzngk != 180000) {
            zzegg.zzd(15, this.zzngk);
        }
        if (this.zzngl != null) {
            zzegg.zza(16, this.zzngl);
        }
        if (this.zznfz != 0) {
            zzegg.zzb(17, this.zznfz);
        }
        if (!Arrays.equals(this.zzngm, zzegr.zzndo)) {
            zzegg.zzc(18, this.zzngm);
        }
        if (this.zzngo != 0) {
            zzegg.zzu(19, this.zzngo);
        }
        if (this.zzngp != null && this.zzngp.length > 0) {
            while (i < this.zzngp.length) {
                zzegg.zzu(20, this.zzngp[i]);
                i++;
            }
        }
        if (this.zznga != 0) {
            zzegg.zzb(21, this.zznga);
        }
        if (this.zzngq != 0) {
            zzegg.zzb(22, this.zzngq);
        }
        if (this.zzmnw != null) {
            zzegg.zza(23, this.zzmnw);
        }
        if (!(this.zzngn == null || this.zzngn.equals(""))) {
            zzegg.zzl(24, this.zzngn);
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehi) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehi) clone();
    }

    protected final int zzn() {
        int i;
        int i2 = 0;
        int zzn = super.zzn();
        if (this.zznfy != 0) {
            zzn += zzegg.zze(1, this.zznfy);
        }
        if (!(this.tag == null || this.tag.equals(""))) {
            zzn += zzegg.zzm(2, this.tag);
        }
        if (this.zzngc != null && this.zzngc.length > 0) {
            i = zzn;
            for (zzego zzego : this.zzngc) {
                if (zzego != null) {
                    i += zzegg.zzb(3, zzego);
                }
            }
            zzn = i;
        }
        if (!Arrays.equals(this.zzngd, zzegr.zzndo)) {
            zzn += zzegg.zzd(4, this.zzngd);
        }
        if (!Arrays.equals(this.zzngf, zzegr.zzndo)) {
            zzn += zzegg.zzd(6, this.zzngf);
        }
        if (this.zzngi != null) {
            zzn += zzegg.zzb(7, this.zzngi);
        }
        if (!(this.zzngg == null || this.zzngg.equals(""))) {
            zzn += zzegg.zzm(8, this.zzngg);
        }
        if (this.zznge != null) {
            zzn += zzegg.zzb(9, this.zznge);
        }
        if (this.zzlua) {
            zzn += zzegg.zzgr(10) + 1;
        }
        if (this.zzngb != 0) {
            zzn += zzegg.zzv(11, this.zzngb);
        }
        if (this.zzaka != 0) {
            zzn += zzegg.zzv(12, this.zzaka);
        }
        if (!(this.zzngh == null || this.zzngh.equals(""))) {
            zzn += zzegg.zzm(13, this.zzngh);
        }
        if (!(this.zzngj == null || this.zzngj.equals(""))) {
            zzn += zzegg.zzm(14, this.zzngj);
        }
        if (this.zzngk != 180000) {
            zzn += zzegg.zzf(15, this.zzngk);
        }
        if (this.zzngl != null) {
            zzn += zzegg.zzb(16, this.zzngl);
        }
        if (this.zznfz != 0) {
            zzn += zzegg.zze(17, this.zznfz);
        }
        if (!Arrays.equals(this.zzngm, zzegr.zzndo)) {
            zzn += zzegg.zzd(18, this.zzngm);
        }
        if (this.zzngo != 0) {
            zzn += zzegg.zzv(19, this.zzngo);
        }
        if (this.zzngp != null && this.zzngp.length > 0) {
            i = 0;
            while (i2 < this.zzngp.length) {
                i += zzegg.zzgs(this.zzngp[i2]);
                i2++;
            }
            zzn = (zzn + i) + (this.zzngp.length * 2);
        }
        if (this.zznga != 0) {
            zzn += zzegg.zze(21, this.zznga);
        }
        if (this.zzngq != 0) {
            zzn += zzegg.zze(22, this.zzngq);
        }
        if (this.zzmnw != null) {
            zzn += zzegg.zzb(23, this.zzmnw);
        }
        return (this.zzngn == null || this.zzngn.equals("")) ? zzn : zzn + zzegg.zzm(24, this.zzngn);
    }
}
