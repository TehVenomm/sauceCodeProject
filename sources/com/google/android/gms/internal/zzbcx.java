package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzb;
import com.google.android.gms.common.util.zzn;
import com.google.android.gms.common.util.zzo;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public abstract class zzbcx {
    protected static <O, I> I zza(zzbcy<I, O> zzbcy, Object obj) {
        return zzbcy.zzfwt != null ? zzbcy.convertBack(obj) : obj;
    }

    private static void zza(StringBuilder stringBuilder, zzbcy zzbcy, Object obj) {
        if (zzbcy.zzfwk == 11) {
            stringBuilder.append(((zzbcx) zzbcy.zzfwq.cast(obj)).toString());
        } else if (zzbcy.zzfwk == 7) {
            stringBuilder.append("\"");
            stringBuilder.append(zzn.zzgk((String) obj));
            stringBuilder.append("\"");
        } else {
            stringBuilder.append(obj);
        }
    }

    private static void zza(StringBuilder stringBuilder, zzbcy zzbcy, ArrayList<Object> arrayList) {
        stringBuilder.append("[");
        int size = arrayList.size();
        for (int i = 0; i < size; i++) {
            if (i > 0) {
                stringBuilder.append(",");
            }
            Object obj = arrayList.get(i);
            if (obj != null) {
                zza(stringBuilder, zzbcy, obj);
            }
        }
        stringBuilder.append("]");
    }

    public String toString() {
        Map zzzx = zzzx();
        StringBuilder stringBuilder = new StringBuilder(100);
        for (String str : zzzx.keySet()) {
            zzbcy zzbcy = (zzbcy) zzzx.get(str);
            if (zza(zzbcy)) {
                Object zza = zza(zzbcy, zzb(zzbcy));
                if (stringBuilder.length() == 0) {
                    stringBuilder.append("{");
                } else {
                    stringBuilder.append(",");
                }
                stringBuilder.append("\"").append(str).append("\":");
                if (zza != null) {
                    switch (zzbcy.zzfwm) {
                        case 8:
                            stringBuilder.append("\"").append(zzb.encode((byte[]) zza)).append("\"");
                            break;
                        case 9:
                            stringBuilder.append("\"").append(zzb.zzj((byte[]) zza)).append("\"");
                            break;
                        case 10:
                            zzo.zza(stringBuilder, (HashMap) zza);
                            break;
                        default:
                            if (!zzbcy.zzfwl) {
                                zza(stringBuilder, zzbcy, zza);
                                break;
                            }
                            zza(stringBuilder, zzbcy, (ArrayList) zza);
                            break;
                    }
                }
                stringBuilder.append("null");
            }
        }
        if (stringBuilder.length() > 0) {
            stringBuilder.append("}");
        } else {
            stringBuilder.append("{}");
        }
        return stringBuilder.toString();
    }

    protected boolean zza(zzbcy zzbcy) {
        if (zzbcy.zzfwm != 11) {
            return zzgi(zzbcy.zzfwo);
        }
        if (zzbcy.zzfwn) {
            String str = zzbcy.zzfwo;
            throw new UnsupportedOperationException("Concrete type arrays not supported");
        }
        str = zzbcy.zzfwo;
        throw new UnsupportedOperationException("Concrete types not supported");
    }

    protected Object zzb(zzbcy zzbcy) {
        String str = zzbcy.zzfwo;
        if (zzbcy.zzfwq == null) {
            return zzgh(zzbcy.zzfwo);
        }
        zzgh(zzbcy.zzfwo);
        zzbp.zza(true, "Concrete field shouldn't be value object: %s", zzbcy.zzfwo);
        boolean z = zzbcy.zzfwn;
        try {
            char toUpperCase = Character.toUpperCase(str.charAt(0));
            str = str.substring(1);
            return getClass().getMethod(new StringBuilder(String.valueOf(str).length() + 4).append("get").append(toUpperCase).append(str).toString(), new Class[0]).invoke(this, new Object[0]);
        } catch (Throwable e) {
            throw new RuntimeException(e);
        }
    }

    protected abstract Object zzgh(String str);

    protected abstract boolean zzgi(String str);

    public abstract Map<String, zzbcy<?, ?>> zzzx();
}
