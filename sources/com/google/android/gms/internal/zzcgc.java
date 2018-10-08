package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import com.github.droidfu.support.DisplaySupport;
import im.getsocial.sdk.ErrorCode;
import java.io.IOException;
import jp.colopl.util.ImageUtil;

public final class zzcgc extends zzegi<zzcgc> {
    private static volatile zzcgc[] zziyy;
    public String zzch;
    public String zzcy;
    public String zzhtl;
    public String zziln;
    public String zzilo;
    public String zzilr;
    public String zzilv;
    public Integer zziyz;
    public zzcfz[] zziza;
    public zzcge[] zzizb;
    public Long zzizc;
    public Long zzizd;
    public Long zzize;
    public Long zzizf;
    public Long zzizg;
    public String zzizh;
    public String zzizi;
    public String zzizj;
    public Integer zzizk;
    public Long zzizl;
    public Long zzizm;
    public String zzizn;
    public Boolean zzizo;
    public String zzizp;
    public Long zzizq;
    public Integer zzizr;
    public Boolean zzizs;
    public zzcfy[] zzizt;
    public Integer zzizu;
    private Integer zzizv;
    private Integer zzizw;
    public Long zzizx;
    public Long zzizy;
    public String zzizz;

    public zzcgc() {
        this.zziyz = null;
        this.zziza = zzcfz.zzbaf();
        this.zzizb = zzcge.zzbai();
        this.zzizc = null;
        this.zzizd = null;
        this.zzize = null;
        this.zzizf = null;
        this.zzizg = null;
        this.zzizh = null;
        this.zzcy = null;
        this.zzizi = null;
        this.zzizj = null;
        this.zzizk = null;
        this.zzilo = null;
        this.zzch = null;
        this.zzhtl = null;
        this.zzizl = null;
        this.zzizm = null;
        this.zzizn = null;
        this.zzizo = null;
        this.zzizp = null;
        this.zzizq = null;
        this.zzizr = null;
        this.zzilr = null;
        this.zziln = null;
        this.zzizs = null;
        this.zzizt = zzcfy.zzbae();
        this.zzilv = null;
        this.zzizu = null;
        this.zzizv = null;
        this.zzizw = null;
        this.zzizx = null;
        this.zzizy = null;
        this.zzizz = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcgc[] zzbah() {
        if (zziyy == null) {
            synchronized (zzegm.zzndc) {
                if (zziyy == null) {
                    zziyy = new zzcgc[0];
                }
            }
        }
        return zziyy;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcgc)) {
            return false;
        }
        zzcgc zzcgc = (zzcgc) obj;
        if (this.zziyz == null) {
            if (zzcgc.zziyz != null) {
                return false;
            }
        } else if (!this.zziyz.equals(zzcgc.zziyz)) {
            return false;
        }
        if (!zzegm.equals(this.zziza, zzcgc.zziza)) {
            return false;
        }
        if (!zzegm.equals(this.zzizb, zzcgc.zzizb)) {
            return false;
        }
        if (this.zzizc == null) {
            if (zzcgc.zzizc != null) {
                return false;
            }
        } else if (!this.zzizc.equals(zzcgc.zzizc)) {
            return false;
        }
        if (this.zzizd == null) {
            if (zzcgc.zzizd != null) {
                return false;
            }
        } else if (!this.zzizd.equals(zzcgc.zzizd)) {
            return false;
        }
        if (this.zzize == null) {
            if (zzcgc.zzize != null) {
                return false;
            }
        } else if (!this.zzize.equals(zzcgc.zzize)) {
            return false;
        }
        if (this.zzizf == null) {
            if (zzcgc.zzizf != null) {
                return false;
            }
        } else if (!this.zzizf.equals(zzcgc.zzizf)) {
            return false;
        }
        if (this.zzizg == null) {
            if (zzcgc.zzizg != null) {
                return false;
            }
        } else if (!this.zzizg.equals(zzcgc.zzizg)) {
            return false;
        }
        if (this.zzizh == null) {
            if (zzcgc.zzizh != null) {
                return false;
            }
        } else if (!this.zzizh.equals(zzcgc.zzizh)) {
            return false;
        }
        if (this.zzcy == null) {
            if (zzcgc.zzcy != null) {
                return false;
            }
        } else if (!this.zzcy.equals(zzcgc.zzcy)) {
            return false;
        }
        if (this.zzizi == null) {
            if (zzcgc.zzizi != null) {
                return false;
            }
        } else if (!this.zzizi.equals(zzcgc.zzizi)) {
            return false;
        }
        if (this.zzizj == null) {
            if (zzcgc.zzizj != null) {
                return false;
            }
        } else if (!this.zzizj.equals(zzcgc.zzizj)) {
            return false;
        }
        if (this.zzizk == null) {
            if (zzcgc.zzizk != null) {
                return false;
            }
        } else if (!this.zzizk.equals(zzcgc.zzizk)) {
            return false;
        }
        if (this.zzilo == null) {
            if (zzcgc.zzilo != null) {
                return false;
            }
        } else if (!this.zzilo.equals(zzcgc.zzilo)) {
            return false;
        }
        if (this.zzch == null) {
            if (zzcgc.zzch != null) {
                return false;
            }
        } else if (!this.zzch.equals(zzcgc.zzch)) {
            return false;
        }
        if (this.zzhtl == null) {
            if (zzcgc.zzhtl != null) {
                return false;
            }
        } else if (!this.zzhtl.equals(zzcgc.zzhtl)) {
            return false;
        }
        if (this.zzizl == null) {
            if (zzcgc.zzizl != null) {
                return false;
            }
        } else if (!this.zzizl.equals(zzcgc.zzizl)) {
            return false;
        }
        if (this.zzizm == null) {
            if (zzcgc.zzizm != null) {
                return false;
            }
        } else if (!this.zzizm.equals(zzcgc.zzizm)) {
            return false;
        }
        if (this.zzizn == null) {
            if (zzcgc.zzizn != null) {
                return false;
            }
        } else if (!this.zzizn.equals(zzcgc.zzizn)) {
            return false;
        }
        if (this.zzizo == null) {
            if (zzcgc.zzizo != null) {
                return false;
            }
        } else if (!this.zzizo.equals(zzcgc.zzizo)) {
            return false;
        }
        if (this.zzizp == null) {
            if (zzcgc.zzizp != null) {
                return false;
            }
        } else if (!this.zzizp.equals(zzcgc.zzizp)) {
            return false;
        }
        if (this.zzizq == null) {
            if (zzcgc.zzizq != null) {
                return false;
            }
        } else if (!this.zzizq.equals(zzcgc.zzizq)) {
            return false;
        }
        if (this.zzizr == null) {
            if (zzcgc.zzizr != null) {
                return false;
            }
        } else if (!this.zzizr.equals(zzcgc.zzizr)) {
            return false;
        }
        if (this.zzilr == null) {
            if (zzcgc.zzilr != null) {
                return false;
            }
        } else if (!this.zzilr.equals(zzcgc.zzilr)) {
            return false;
        }
        if (this.zziln == null) {
            if (zzcgc.zziln != null) {
                return false;
            }
        } else if (!this.zziln.equals(zzcgc.zziln)) {
            return false;
        }
        if (this.zzizs == null) {
            if (zzcgc.zzizs != null) {
                return false;
            }
        } else if (!this.zzizs.equals(zzcgc.zzizs)) {
            return false;
        }
        if (!zzegm.equals(this.zzizt, zzcgc.zzizt)) {
            return false;
        }
        if (this.zzilv == null) {
            if (zzcgc.zzilv != null) {
                return false;
            }
        } else if (!this.zzilv.equals(zzcgc.zzilv)) {
            return false;
        }
        if (this.zzizu == null) {
            if (zzcgc.zzizu != null) {
                return false;
            }
        } else if (!this.zzizu.equals(zzcgc.zzizu)) {
            return false;
        }
        if (this.zzizv == null) {
            if (zzcgc.zzizv != null) {
                return false;
            }
        } else if (!this.zzizv.equals(zzcgc.zzizv)) {
            return false;
        }
        if (this.zzizw == null) {
            if (zzcgc.zzizw != null) {
                return false;
            }
        } else if (!this.zzizw.equals(zzcgc.zzizw)) {
            return false;
        }
        if (this.zzizx == null) {
            if (zzcgc.zzizx != null) {
                return false;
            }
        } else if (!this.zzizx.equals(zzcgc.zzizx)) {
            return false;
        }
        if (this.zzizy == null) {
            if (zzcgc.zzizy != null) {
                return false;
            }
        } else if (!this.zzizy.equals(zzcgc.zzizy)) {
            return false;
        }
        if (this.zzizz == null) {
            if (zzcgc.zzizz != null) {
                return false;
            }
        } else if (!this.zzizz.equals(zzcgc.zzizz)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcgc.zzncu == null || zzcgc.zzncu.isEmpty() : this.zzncu.equals(zzcgc.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zziyz == null ? 0 : this.zziyz.hashCode();
        int hashCode3 = zzegm.hashCode(this.zziza);
        int hashCode4 = zzegm.hashCode(this.zzizb);
        int hashCode5 = this.zzizc == null ? 0 : this.zzizc.hashCode();
        int hashCode6 = this.zzizd == null ? 0 : this.zzizd.hashCode();
        int hashCode7 = this.zzize == null ? 0 : this.zzize.hashCode();
        int hashCode8 = this.zzizf == null ? 0 : this.zzizf.hashCode();
        int hashCode9 = this.zzizg == null ? 0 : this.zzizg.hashCode();
        int hashCode10 = this.zzizh == null ? 0 : this.zzizh.hashCode();
        int hashCode11 = this.zzcy == null ? 0 : this.zzcy.hashCode();
        int hashCode12 = this.zzizi == null ? 0 : this.zzizi.hashCode();
        int hashCode13 = this.zzizj == null ? 0 : this.zzizj.hashCode();
        int hashCode14 = this.zzizk == null ? 0 : this.zzizk.hashCode();
        int hashCode15 = this.zzilo == null ? 0 : this.zzilo.hashCode();
        int hashCode16 = this.zzch == null ? 0 : this.zzch.hashCode();
        int hashCode17 = this.zzhtl == null ? 0 : this.zzhtl.hashCode();
        int hashCode18 = this.zzizl == null ? 0 : this.zzizl.hashCode();
        int hashCode19 = this.zzizm == null ? 0 : this.zzizm.hashCode();
        int hashCode20 = this.zzizn == null ? 0 : this.zzizn.hashCode();
        int hashCode21 = this.zzizo == null ? 0 : this.zzizo.hashCode();
        int hashCode22 = this.zzizp == null ? 0 : this.zzizp.hashCode();
        int hashCode23 = this.zzizq == null ? 0 : this.zzizq.hashCode();
        int hashCode24 = this.zzizr == null ? 0 : this.zzizr.hashCode();
        int hashCode25 = this.zzilr == null ? 0 : this.zzilr.hashCode();
        int hashCode26 = this.zziln == null ? 0 : this.zziln.hashCode();
        int hashCode27 = this.zzizs == null ? 0 : this.zzizs.hashCode();
        int hashCode28 = zzegm.hashCode((Object[]) this.zzizt);
        int hashCode29 = this.zzilv == null ? 0 : this.zzilv.hashCode();
        int hashCode30 = this.zzizu == null ? 0 : this.zzizu.hashCode();
        int hashCode31 = this.zzizv == null ? 0 : this.zzizv.hashCode();
        int hashCode32 = this.zzizw == null ? 0 : this.zzizw.hashCode();
        int hashCode33 = this.zzizx == null ? 0 : this.zzizx.hashCode();
        int hashCode34 = this.zzizy == null ? 0 : this.zzizy.hashCode();
        int hashCode35 = this.zzizz == null ? 0 : this.zzizz.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + hashCode7) * 31) + hashCode8) * 31) + hashCode9) * 31) + hashCode10) * 31) + hashCode11) * 31) + hashCode12) * 31) + hashCode13) * 31) + hashCode14) * 31) + hashCode15) * 31) + hashCode16) * 31) + hashCode17) * 31) + hashCode18) * 31) + hashCode19) * 31) + hashCode20) * 31) + hashCode21) * 31) + hashCode22) * 31) + hashCode23) * 31) + hashCode24) * 31) + hashCode25) * 31) + hashCode26) * 31) + hashCode27) * 31) + hashCode28) * 31) + hashCode29) * 31) + hashCode30) * 31) + hashCode31) * 31) + hashCode32) * 31) + hashCode33) * 31) + hashCode34) * 31) + hashCode35) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int zzb;
            Object obj;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zziyz = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 18:
                    zzb = zzegr.zzb(zzegf, 18);
                    zzcbr = this.zziza == null ? 0 : this.zziza.length;
                    obj = new zzcfz[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziza, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfz();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfz();
                    zzegf.zza(obj[zzcbr]);
                    this.zziza = obj;
                    continue;
                case 26:
                    zzb = zzegr.zzb(zzegf, 26);
                    zzcbr = this.zzizb == null ? 0 : this.zzizb.length;
                    obj = new zzcge[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzizb, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcge();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcge();
                    zzegf.zza(obj[zzcbr]);
                    this.zzizb = obj;
                    continue;
                case 32:
                    this.zzizc = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 40:
                    this.zzizd = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 48:
                    this.zzize = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 56:
                    this.zzizg = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 66:
                    this.zzizh = zzegf.readString();
                    continue;
                case 74:
                    this.zzcy = zzegf.readString();
                    continue;
                case 82:
                    this.zzizi = zzegf.readString();
                    continue;
                case ImageUtil.OUTPUT_QUALITY /*90*/:
                    this.zzizj = zzegf.readString();
                    continue;
                case 96:
                    this.zzizk = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 106:
                    this.zzilo = zzegf.readString();
                    continue;
                case 114:
                    this.zzch = zzegf.readString();
                    continue;
                case TransportMediator.KEYCODE_MEDIA_RECORD /*130*/:
                    this.zzhtl = zzegf.readString();
                    continue;
                case 136:
                    this.zzizl = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 144:
                    this.zzizm = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 154:
                    this.zzizn = zzegf.readString();
                    continue;
                case DisplaySupport.SCREEN_DENSITY_MEDIUM /*160*/:
                    this.zzizo = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case 170:
                    this.zzizp = zzegf.readString();
                    continue;
                case 176:
                    this.zzizq = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 184:
                    this.zzizr = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 194:
                    this.zzilr = zzegf.readString();
                    continue;
                case ErrorCode.SDK_NOT_INITIALIZED /*202*/:
                    this.zziln = zzegf.readString();
                    continue;
                case ErrorCode.USER_IS_BANNED /*208*/:
                    this.zzizf = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 224:
                    this.zzizs = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case 234:
                    zzb = zzegr.zzb(zzegf, 234);
                    zzcbr = this.zzizt == null ? 0 : this.zzizt.length;
                    obj = new zzcfy[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzizt, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfy();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfy();
                    zzegf.zza(obj[zzcbr]);
                    this.zzizt = obj;
                    continue;
                case 242:
                    this.zzilv = zzegf.readString();
                    continue;
                case 248:
                    this.zzizu = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 256:
                    this.zzizv = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 264:
                    this.zzizw = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 280:
                    this.zzizx = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 288:
                    this.zzizy = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 298:
                    this.zzizz = zzegf.readString();
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
        if (this.zziyz != null) {
            zzegg.zzu(1, this.zziyz.intValue());
        }
        if (this.zziza != null && this.zziza.length > 0) {
            for (zzego zzego : this.zziza) {
                if (zzego != null) {
                    zzegg.zza(2, zzego);
                }
            }
        }
        if (this.zzizb != null && this.zzizb.length > 0) {
            for (zzego zzego2 : this.zzizb) {
                if (zzego2 != null) {
                    zzegg.zza(3, zzego2);
                }
            }
        }
        if (this.zzizc != null) {
            zzegg.zzb(4, this.zzizc.longValue());
        }
        if (this.zzizd != null) {
            zzegg.zzb(5, this.zzizd.longValue());
        }
        if (this.zzize != null) {
            zzegg.zzb(6, this.zzize.longValue());
        }
        if (this.zzizg != null) {
            zzegg.zzb(7, this.zzizg.longValue());
        }
        if (this.zzizh != null) {
            zzegg.zzl(8, this.zzizh);
        }
        if (this.zzcy != null) {
            zzegg.zzl(9, this.zzcy);
        }
        if (this.zzizi != null) {
            zzegg.zzl(10, this.zzizi);
        }
        if (this.zzizj != null) {
            zzegg.zzl(11, this.zzizj);
        }
        if (this.zzizk != null) {
            zzegg.zzu(12, this.zzizk.intValue());
        }
        if (this.zzilo != null) {
            zzegg.zzl(13, this.zzilo);
        }
        if (this.zzch != null) {
            zzegg.zzl(14, this.zzch);
        }
        if (this.zzhtl != null) {
            zzegg.zzl(16, this.zzhtl);
        }
        if (this.zzizl != null) {
            zzegg.zzb(17, this.zzizl.longValue());
        }
        if (this.zzizm != null) {
            zzegg.zzb(18, this.zzizm.longValue());
        }
        if (this.zzizn != null) {
            zzegg.zzl(19, this.zzizn);
        }
        if (this.zzizo != null) {
            zzegg.zzl(20, this.zzizo.booleanValue());
        }
        if (this.zzizp != null) {
            zzegg.zzl(21, this.zzizp);
        }
        if (this.zzizq != null) {
            zzegg.zzb(22, this.zzizq.longValue());
        }
        if (this.zzizr != null) {
            zzegg.zzu(23, this.zzizr.intValue());
        }
        if (this.zzilr != null) {
            zzegg.zzl(24, this.zzilr);
        }
        if (this.zziln != null) {
            zzegg.zzl(25, this.zziln);
        }
        if (this.zzizf != null) {
            zzegg.zzb(26, this.zzizf.longValue());
        }
        if (this.zzizs != null) {
            zzegg.zzl(28, this.zzizs.booleanValue());
        }
        if (this.zzizt != null && this.zzizt.length > 0) {
            while (i < this.zzizt.length) {
                zzego zzego3 = this.zzizt[i];
                if (zzego3 != null) {
                    zzegg.zza(29, zzego3);
                }
                i++;
            }
        }
        if (this.zzilv != null) {
            zzegg.zzl(30, this.zzilv);
        }
        if (this.zzizu != null) {
            zzegg.zzu(31, this.zzizu.intValue());
        }
        if (this.zzizv != null) {
            zzegg.zzu(32, this.zzizv.intValue());
        }
        if (this.zzizw != null) {
            zzegg.zzu(33, this.zzizw.intValue());
        }
        if (this.zzizx != null) {
            zzegg.zzb(35, this.zzizx.longValue());
        }
        if (this.zzizy != null) {
            zzegg.zzb(36, this.zzizy.longValue());
        }
        if (this.zzizz != null) {
            zzegg.zzl(37, this.zzizz);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int i;
        int i2 = 0;
        int zzn = super.zzn();
        if (this.zziyz != null) {
            zzn += zzegg.zzv(1, this.zziyz.intValue());
        }
        if (this.zziza != null && this.zziza.length > 0) {
            i = zzn;
            for (zzego zzego : this.zziza) {
                if (zzego != null) {
                    i += zzegg.zzb(2, zzego);
                }
            }
            zzn = i;
        }
        if (this.zzizb != null && this.zzizb.length > 0) {
            i = zzn;
            for (zzego zzego2 : this.zzizb) {
                if (zzego2 != null) {
                    i += zzegg.zzb(3, zzego2);
                }
            }
            zzn = i;
        }
        if (this.zzizc != null) {
            zzn += zzegg.zze(4, this.zzizc.longValue());
        }
        if (this.zzizd != null) {
            zzn += zzegg.zze(5, this.zzizd.longValue());
        }
        if (this.zzize != null) {
            zzn += zzegg.zze(6, this.zzize.longValue());
        }
        if (this.zzizg != null) {
            zzn += zzegg.zze(7, this.zzizg.longValue());
        }
        if (this.zzizh != null) {
            zzn += zzegg.zzm(8, this.zzizh);
        }
        if (this.zzcy != null) {
            zzn += zzegg.zzm(9, this.zzcy);
        }
        if (this.zzizi != null) {
            zzn += zzegg.zzm(10, this.zzizi);
        }
        if (this.zzizj != null) {
            zzn += zzegg.zzm(11, this.zzizj);
        }
        if (this.zzizk != null) {
            zzn += zzegg.zzv(12, this.zzizk.intValue());
        }
        if (this.zzilo != null) {
            zzn += zzegg.zzm(13, this.zzilo);
        }
        if (this.zzch != null) {
            zzn += zzegg.zzm(14, this.zzch);
        }
        if (this.zzhtl != null) {
            zzn += zzegg.zzm(16, this.zzhtl);
        }
        if (this.zzizl != null) {
            zzn += zzegg.zze(17, this.zzizl.longValue());
        }
        if (this.zzizm != null) {
            zzn += zzegg.zze(18, this.zzizm.longValue());
        }
        if (this.zzizn != null) {
            zzn += zzegg.zzm(19, this.zzizn);
        }
        if (this.zzizo != null) {
            this.zzizo.booleanValue();
            zzn += zzegg.zzgr(20) + 1;
        }
        if (this.zzizp != null) {
            zzn += zzegg.zzm(21, this.zzizp);
        }
        if (this.zzizq != null) {
            zzn += zzegg.zze(22, this.zzizq.longValue());
        }
        if (this.zzizr != null) {
            zzn += zzegg.zzv(23, this.zzizr.intValue());
        }
        if (this.zzilr != null) {
            zzn += zzegg.zzm(24, this.zzilr);
        }
        if (this.zziln != null) {
            zzn += zzegg.zzm(25, this.zziln);
        }
        if (this.zzizf != null) {
            zzn += zzegg.zze(26, this.zzizf.longValue());
        }
        if (this.zzizs != null) {
            this.zzizs.booleanValue();
            zzn += zzegg.zzgr(28) + 1;
        }
        if (this.zzizt != null && this.zzizt.length > 0) {
            while (i2 < this.zzizt.length) {
                zzego zzego3 = this.zzizt[i2];
                if (zzego3 != null) {
                    zzn += zzegg.zzb(29, zzego3);
                }
                i2++;
            }
        }
        if (this.zzilv != null) {
            zzn += zzegg.zzm(30, this.zzilv);
        }
        if (this.zzizu != null) {
            zzn += zzegg.zzv(31, this.zzizu.intValue());
        }
        if (this.zzizv != null) {
            zzn += zzegg.zzv(32, this.zzizv.intValue());
        }
        if (this.zzizw != null) {
            zzn += zzegg.zzv(33, this.zzizw.intValue());
        }
        if (this.zzizx != null) {
            zzn += zzegg.zze(35, this.zzizx.longValue());
        }
        if (this.zzizy != null) {
            zzn += zzegg.zze(36, this.zzizy.longValue());
        }
        return this.zzizz != null ? zzn + zzegg.zzm(37, this.zzizz) : zzn;
    }
}
