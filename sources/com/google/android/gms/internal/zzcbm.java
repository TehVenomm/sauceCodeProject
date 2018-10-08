package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import bolts.MeasurementEvent;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.Event;
import com.google.android.gms.measurement.AppMeasurement.Param;
import com.google.android.gms.measurement.AppMeasurement.UserProperty;
import io.fabric.sdk.android.services.common.IdManager;
import org.apache.commons.lang3.StringUtils;

public final class zzcbm extends zzcdm {
    private static String[] zzipf = new String[Event.zzikc.length];
    private static String[] zzipg = new String[Param.zzike.length];
    private static String[] zziph = new String[UserProperty.zzikj.length];

    zzcbm(zzcco zzcco) {
        super(zzcco);
    }

    @Nullable
    private static String zza(String str, String[] strArr, String[] strArr2, String[] strArr3) {
        boolean z = true;
        int i = 0;
        zzbp.zzu(strArr);
        zzbp.zzu(strArr2);
        zzbp.zzu(strArr3);
        zzbp.zzbh(strArr.length == strArr2.length);
        if (strArr.length != strArr3.length) {
            z = false;
        }
        zzbp.zzbh(z);
        while (i < strArr.length) {
            if (zzcfo.zzau(str, strArr[i])) {
                synchronized (strArr3) {
                    if (strArr3[i] == null) {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.append(strArr2[i]);
                        stringBuilder.append("(");
                        stringBuilder.append(strArr[i]);
                        stringBuilder.append(")");
                        strArr3[i] = stringBuilder.toString();
                    }
                    str = strArr3[i];
                }
                return str;
            }
            i++;
        }
        return str;
    }

    private static void zza(StringBuilder stringBuilder, int i) {
        for (int i2 = 0; i2 < i; i2++) {
            stringBuilder.append("  ");
        }
    }

    private final void zza(StringBuilder stringBuilder, int i, zzcfr zzcfr) {
        if (zzcfr != null) {
            zza(stringBuilder, i);
            stringBuilder.append("filter {\n");
            zza(stringBuilder, i, "complement", zzcfr.zzixq);
            zza(stringBuilder, i, "param_name", zzjd(zzcfr.zzixr));
            int i2 = i + 1;
            zzcfu zzcfu = zzcfr.zzixo;
            if (zzcfu != null) {
                zza(stringBuilder, i2);
                stringBuilder.append("string_filter");
                stringBuilder.append(" {\n");
                if (zzcfu.zziya != null) {
                    Object obj = "UNKNOWN_MATCH_TYPE";
                    switch (zzcfu.zziya.intValue()) {
                        case 1:
                            obj = "REGEXP";
                            break;
                        case 2:
                            obj = "BEGINS_WITH";
                            break;
                        case 3:
                            obj = "ENDS_WITH";
                            break;
                        case 4:
                            obj = "PARTIAL";
                            break;
                        case 5:
                            obj = "EXACT";
                            break;
                        case 6:
                            obj = "IN_LIST";
                            break;
                    }
                    zza(stringBuilder, i2, "match_type", obj);
                }
                zza(stringBuilder, i2, "expression", zzcfu.zziyb);
                zza(stringBuilder, i2, "case_sensitive", zzcfu.zziyc);
                if (zzcfu.zziyd.length > 0) {
                    zza(stringBuilder, i2 + 1);
                    stringBuilder.append("expression_list {\n");
                    for (String str : zzcfu.zziyd) {
                        zza(stringBuilder, i2 + 2);
                        stringBuilder.append(str);
                        stringBuilder.append(StringUtils.LF);
                    }
                    stringBuilder.append("}\n");
                }
                zza(stringBuilder, i2);
                stringBuilder.append("}\n");
            }
            zza(stringBuilder, i + 1, "number_filter", zzcfr.zzixp);
            zza(stringBuilder, i);
            stringBuilder.append("}\n");
        }
    }

    private final void zza(StringBuilder stringBuilder, int i, String str, zzcfs zzcfs) {
        if (zzcfs != null) {
            zza(stringBuilder, i);
            stringBuilder.append(str);
            stringBuilder.append(" {\n");
            if (zzcfs.zzixs != null) {
                Object obj = "UNKNOWN_COMPARISON_TYPE";
                switch (zzcfs.zzixs.intValue()) {
                    case 1:
                        obj = "LESS_THAN";
                        break;
                    case 2:
                        obj = "GREATER_THAN";
                        break;
                    case 3:
                        obj = "EQUAL";
                        break;
                    case 4:
                        obj = "BETWEEN";
                        break;
                }
                zza(stringBuilder, i, "comparison_type", obj);
            }
            zza(stringBuilder, i, "match_as_float", zzcfs.zzixt);
            zza(stringBuilder, i, "comparison_value", zzcfs.zzixu);
            zza(stringBuilder, i, "min_comparison_value", zzcfs.zzixv);
            zza(stringBuilder, i, "max_comparison_value", zzcfs.zzixw);
            zza(stringBuilder, i);
            stringBuilder.append("}\n");
        }
    }

    private static void zza(StringBuilder stringBuilder, int i, String str, zzcgd zzcgd) {
        int i2 = 0;
        if (zzcgd != null) {
            int i3;
            long j;
            int i4 = i + 1;
            zza(stringBuilder, i4);
            stringBuilder.append(str);
            stringBuilder.append(" {\n");
            if (zzcgd.zzjab != null) {
                zza(stringBuilder, i4 + 1);
                stringBuilder.append("results: ");
                long[] jArr = zzcgd.zzjab;
                int length = jArr.length;
                i3 = 0;
                int i5 = 0;
                while (i3 < length) {
                    j = jArr[i3];
                    if (i5 != 0) {
                        stringBuilder.append(", ");
                    }
                    stringBuilder.append(Long.valueOf(j));
                    i3++;
                    i5++;
                }
                stringBuilder.append('\n');
            }
            if (zzcgd.zzjaa != null) {
                zza(stringBuilder, i4 + 1);
                stringBuilder.append("status: ");
                long[] jArr2 = zzcgd.zzjaa;
                int length2 = jArr2.length;
                i3 = 0;
                while (i2 < length2) {
                    j = jArr2[i2];
                    if (i3 != 0) {
                        stringBuilder.append(", ");
                    }
                    stringBuilder.append(Long.valueOf(j));
                    i2++;
                    i3++;
                }
                stringBuilder.append('\n');
            }
            zza(stringBuilder, i4);
            stringBuilder.append("}\n");
        }
    }

    private static void zza(StringBuilder stringBuilder, int i, String str, Object obj) {
        if (obj != null) {
            zza(stringBuilder, i + 1);
            stringBuilder.append(str);
            stringBuilder.append(": ");
            stringBuilder.append(obj);
            stringBuilder.append('\n');
        }
    }

    private final void zza(StringBuilder stringBuilder, int i, zzcfy[] zzcfyArr) {
        if (zzcfyArr != null) {
            for (zzcfy zzcfy : zzcfyArr) {
                if (zzcfy != null) {
                    zza(stringBuilder, 2);
                    stringBuilder.append("audience_membership {\n");
                    zza(stringBuilder, 2, "audience_id", zzcfy.zzixe);
                    zza(stringBuilder, 2, "new_audience", zzcfy.zziyq);
                    zza(stringBuilder, 2, "current_data", zzcfy.zziyo);
                    zza(stringBuilder, 2, "previous_data", zzcfy.zziyp);
                    zza(stringBuilder, 2);
                    stringBuilder.append("}\n");
                }
            }
        }
    }

    private final void zza(StringBuilder stringBuilder, int i, zzcfz[] zzcfzArr) {
        if (zzcfzArr != null) {
            for (zzcfz zzcfz : zzcfzArr) {
                if (zzcfz != null) {
                    zza(stringBuilder, 2);
                    stringBuilder.append("event {\n");
                    zza(stringBuilder, 2, "name", zzjc(zzcfz.name));
                    zza(stringBuilder, 2, "timestamp_millis", zzcfz.zziyt);
                    zza(stringBuilder, 2, "previous_timestamp_millis", zzcfz.zziyu);
                    zza(stringBuilder, 2, "count", zzcfz.count);
                    zzcga[] zzcgaArr = zzcfz.zziys;
                    if (zzcgaArr != null) {
                        for (zzcga zzcga : zzcgaArr) {
                            if (zzcga != null) {
                                zza(stringBuilder, 3);
                                stringBuilder.append("param {\n");
                                zza(stringBuilder, 3, "name", zzjd(zzcga.name));
                                zza(stringBuilder, 3, "string_value", zzcga.zzfwi);
                                zza(stringBuilder, 3, "int_value", zzcga.zziyw);
                                zza(stringBuilder, 3, "double_value", zzcga.zziwx);
                                zza(stringBuilder, 3);
                                stringBuilder.append("}\n");
                            }
                        }
                    }
                    zza(stringBuilder, 2);
                    stringBuilder.append("}\n");
                }
            }
        }
    }

    private final void zza(StringBuilder stringBuilder, int i, zzcge[] zzcgeArr) {
        if (zzcgeArr != null) {
            for (zzcge zzcge : zzcgeArr) {
                if (zzcge != null) {
                    zza(stringBuilder, 2);
                    stringBuilder.append("user_property {\n");
                    zza(stringBuilder, 2, "set_timestamp_millis", zzcge.zzjad);
                    zza(stringBuilder, 2, "name", zzje(zzcge.name));
                    zza(stringBuilder, 2, "string_value", zzcge.zzfwi);
                    zza(stringBuilder, 2, "int_value", zzcge.zziyw);
                    zza(stringBuilder, 2, "double_value", zzcge.zziwx);
                    zza(stringBuilder, 2);
                    stringBuilder.append("}\n");
                }
            }
        }
    }

    private final boolean zzayb() {
        return this.zzikb.zzauk().zzad(3);
    }

    @Nullable
    private final String zzb(zzcaz zzcaz) {
        return zzcaz == null ? null : !zzayb() ? zzcaz.toString() : zzw(zzcaz.zzaxy());
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @Nullable
    protected final String zza(zzcax zzcax) {
        if (zzcax == null) {
            return null;
        }
        if (!zzayb()) {
            return zzcax.toString();
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("Event{appId='");
        stringBuilder.append(zzcax.mAppId);
        stringBuilder.append("', name='");
        stringBuilder.append(zzjc(zzcax.mName));
        stringBuilder.append("', params=");
        stringBuilder.append(zzb(zzcax.zzinc));
        stringBuilder.append("}");
        return stringBuilder.toString();
    }

    protected final String zza(zzcfq zzcfq) {
        int i = 0;
        if (zzcfq == null) {
            return "null";
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("\nevent_filter {\n");
        zza(stringBuilder, 0, "filter_id", zzcfq.zzixi);
        zza(stringBuilder, 0, MeasurementEvent.MEASUREMENT_EVENT_NAME_KEY, zzjc(zzcfq.zzixj));
        zza(stringBuilder, 1, "event_count_filter", zzcfq.zzixm);
        stringBuilder.append("  filters {\n");
        zzcfr[] zzcfrArr = zzcfq.zzixk;
        int length = zzcfrArr.length;
        while (i < length) {
            zza(stringBuilder, 2, zzcfrArr[i]);
            i++;
        }
        zza(stringBuilder, 1);
        stringBuilder.append("}\n}\n");
        return stringBuilder.toString();
    }

    protected final String zza(zzcft zzcft) {
        if (zzcft == null) {
            return "null";
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("\nproperty_filter {\n");
        zza(stringBuilder, 0, "filter_id", zzcft.zzixi);
        zza(stringBuilder, 0, "property_name", zzje(zzcft.zzixy));
        zza(stringBuilder, 1, zzcft.zzixz);
        stringBuilder.append("}\n");
        return stringBuilder.toString();
    }

    protected final String zza(zzcgb zzcgb) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("\nbatch {\n");
        if (zzcgb.zziyx != null) {
            for (zzcgc zzcgc : zzcgb.zziyx) {
                if (!(zzcgc == null || zzcgc == null)) {
                    zza(stringBuilder, 1);
                    stringBuilder.append("bundle {\n");
                    zza(stringBuilder, 1, "protocol_version", zzcgc.zziyz);
                    zza(stringBuilder, 1, "platform", zzcgc.zzizh);
                    zza(stringBuilder, 1, "gmp_version", zzcgc.zzizl);
                    zza(stringBuilder, 1, "uploading_gmp_version", zzcgc.zzizm);
                    zza(stringBuilder, 1, "config_version", zzcgc.zzizx);
                    zza(stringBuilder, 1, "gmp_app_id", zzcgc.zziln);
                    zza(stringBuilder, 1, "app_id", zzcgc.zzch);
                    zza(stringBuilder, 1, "app_version", zzcgc.zzhtl);
                    zza(stringBuilder, 1, "app_version_major", zzcgc.zzizu);
                    zza(stringBuilder, 1, "firebase_instance_id", zzcgc.zzilv);
                    zza(stringBuilder, 1, "dev_cert_hash", zzcgc.zzizq);
                    zza(stringBuilder, 1, "app_store", zzcgc.zzilo);
                    zza(stringBuilder, 1, "upload_timestamp_millis", zzcgc.zzizc);
                    zza(stringBuilder, 1, "start_timestamp_millis", zzcgc.zzizd);
                    zza(stringBuilder, 1, "end_timestamp_millis", zzcgc.zzize);
                    zza(stringBuilder, 1, "previous_bundle_start_timestamp_millis", zzcgc.zzizf);
                    zza(stringBuilder, 1, "previous_bundle_end_timestamp_millis", zzcgc.zzizg);
                    zza(stringBuilder, 1, "app_instance_id", zzcgc.zzizp);
                    zza(stringBuilder, 1, "resettable_device_id", zzcgc.zzizn);
                    zza(stringBuilder, 1, "limited_ad_tracking", zzcgc.zzizo);
                    zza(stringBuilder, 1, IdManager.OS_VERSION_FIELD, zzcgc.zzcy);
                    zza(stringBuilder, 1, "device_model", zzcgc.zzizi);
                    zza(stringBuilder, 1, "user_default_language", zzcgc.zzizj);
                    zza(stringBuilder, 1, "time_zone_offset_minutes", zzcgc.zzizk);
                    zza(stringBuilder, 1, "bundle_sequential_index", zzcgc.zzizr);
                    zza(stringBuilder, 1, "service_upload", zzcgc.zzizs);
                    zza(stringBuilder, 1, "health_monitor", zzcgc.zzilr);
                    if (zzcgc.zzizy.longValue() != 0) {
                        zza(stringBuilder, 1, "android_id", zzcgc.zzizy);
                    }
                    zza(stringBuilder, 1, zzcgc.zzizb);
                    zza(stringBuilder, 1, zzcgc.zzizt);
                    zza(stringBuilder, 1, zzcgc.zziza);
                    zza(stringBuilder, 1);
                    stringBuilder.append("}\n");
                }
            }
        }
        stringBuilder.append("}\n");
        return stringBuilder.toString();
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    @Nullable
    protected final String zzb(zzcbc zzcbc) {
        if (zzcbc == null) {
            return null;
        }
        if (!zzayb()) {
            return zzcbc.toString();
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("origin=");
        stringBuilder.append(zzcbc.zzilz);
        stringBuilder.append(",name=");
        stringBuilder.append(zzjc(zzcbc.name));
        stringBuilder.append(",params=");
        stringBuilder.append(zzb(zzcbc.zzinj));
        return stringBuilder.toString();
    }

    @Nullable
    protected final String zzjc(String str) {
        return str == null ? null : zzayb() ? zza(str, Event.zzikd, Event.zzikc, zzipf) : str;
    }

    @Nullable
    protected final String zzjd(String str) {
        return str == null ? null : zzayb() ? zza(str, Param.zzikf, Param.zzike, zzipg) : str;
    }

    @Nullable
    protected final String zzje(String str) {
        if (str == null) {
            return null;
        }
        if (!zzayb()) {
            return str;
        }
        if (!str.startsWith("_exp_")) {
            return zza(str, UserProperty.zzikk, UserProperty.zzikj, zziph);
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("experiment_id");
        stringBuilder.append("(");
        stringBuilder.append(str);
        stringBuilder.append(")");
        return stringBuilder.toString();
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }

    @Nullable
    protected final String zzw(Bundle bundle) {
        if (bundle == null) {
            return null;
        }
        if (!zzayb()) {
            return bundle.toString();
        }
        StringBuilder stringBuilder = new StringBuilder();
        for (String str : bundle.keySet()) {
            if (stringBuilder.length() != 0) {
                stringBuilder.append(", ");
            } else {
                stringBuilder.append("Bundle[{");
            }
            stringBuilder.append(zzjd(str));
            stringBuilder.append("=");
            stringBuilder.append(bundle.get(str));
        }
        stringBuilder.append("}]");
        return stringBuilder.toString();
    }
}
