package com.google.android.gms.internal;

import io.fabric.sdk.android.services.network.HttpRequest;
import java.util.Map;
import org.apache.commons.lang3.CharEncoding;
import org.apache.http.impl.cookie.DateParseException;
import org.apache.http.impl.cookie.DateUtils;

public final class zzam {
    public static String zza(Map<String, String> map) {
        String str = (String) map.get(HttpRequest.HEADER_CONTENT_TYPE);
        if (str != null) {
            String[] split = str.split(";");
            for (int i = 1; i < split.length; i++) {
                String[] split2 = split[i].trim().split("=");
                if (split2.length == 2 && split2[0].equals(HttpRequest.PARAM_CHARSET)) {
                    return split2[1];
                }
            }
        }
        return CharEncoding.ISO_8859_1;
    }

    public static zzc zzb(zzn zzn) {
        long j;
        long j2;
        long j3;
        Object obj;
        Object obj2;
        long currentTimeMillis = System.currentTimeMillis();
        Map map = zzn.zzy;
        long j4 = 0;
        String str = (String) map.get(HttpRequest.HEADER_DATE);
        if (str != null) {
            j4 = zzf(str);
        }
        str = (String) map.get(HttpRequest.HEADER_CACHE_CONTROL);
        if (str != null) {
            String[] split = str.split(",");
            j = 0;
            Object obj3 = null;
            long j5 = 0;
            for (String trim : split) {
                String trim2 = trim2.trim();
                if (trim2.equals("no-cache") || trim2.equals("no-store")) {
                    return null;
                }
                if (trim2.startsWith("max-age=")) {
                    try {
                        j5 = Long.parseLong(trim2.substring(8));
                    } catch (Exception e) {
                    }
                } else if (trim2.startsWith("stale-while-revalidate=")) {
                    try {
                        j = Long.parseLong(trim2.substring(23));
                    } catch (Exception e2) {
                    }
                } else if (trim2.equals("must-revalidate") || trim2.equals("proxy-revalidate")) {
                    obj3 = 1;
                }
            }
            j2 = j5;
            j3 = j;
            obj = 1;
            obj2 = obj3;
        } else {
            j3 = 0;
            obj = null;
            obj2 = null;
            j2 = 0;
        }
        str = (String) map.get(HttpRequest.HEADER_EXPIRES);
        j = str != null ? zzf(str) : 0;
        str = (String) map.get(HttpRequest.HEADER_LAST_MODIFIED);
        long zzf = str != null ? zzf(str) : 0;
        str = (String) map.get(HttpRequest.HEADER_ETAG);
        if (obj != null) {
            j = (1000 * j2) + currentTimeMillis;
            j2 = obj2 != null ? j : (1000 * j3) + j;
        } else if (j4 <= 0 || j < j4) {
            j2 = 0;
            j = 0;
        } else {
            j = (j - j4) + currentTimeMillis;
            j2 = j;
        }
        zzc zzc = new zzc();
        zzc.data = zzn.data;
        zzc.zza = str;
        zzc.zze = j;
        zzc.zzd = j2;
        zzc.zzb = j4;
        zzc.zzc = zzf;
        zzc.zzf = map;
        return zzc;
    }

    private static long zzf(String str) {
        try {
            return DateUtils.parseDate(str).getTime();
        } catch (DateParseException e) {
            return 0;
        }
    }
}
