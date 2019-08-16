package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzey.zzb;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.TreeSet;
import org.apache.commons.lang3.StringUtils;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

final class zzgj {
    static String zza(zzgi zzgi, String str) {
        StringBuilder sb = new StringBuilder();
        sb.append("# ").append(str);
        zza(zzgi, sb, 0);
        return sb.toString();
    }

    private static void zza(zzgi zzgi, StringBuilder sb, int i) {
        Method[] declaredMethods;
        boolean booleanValue;
        HashMap hashMap = new HashMap();
        HashMap hashMap2 = new HashMap();
        TreeSet<String> treeSet = new TreeSet<>();
        for (Method method : zzgi.getClass().getDeclaredMethods()) {
            hashMap2.put(method.getName(), method);
            if (method.getParameterTypes().length == 0) {
                hashMap.put(method.getName(), method);
                if (method.getName().startsWith("get")) {
                    treeSet.add(method.getName());
                }
            }
        }
        for (String str : treeSet) {
            String replaceFirst = str.replaceFirst("get", "");
            if (replaceFirst.endsWith("List") && !replaceFirst.endsWith("OrBuilderList") && !replaceFirst.equals("List")) {
                String valueOf = String.valueOf(replaceFirst.substring(0, 1).toLowerCase());
                String valueOf2 = String.valueOf(replaceFirst.substring(1, replaceFirst.length() - 4));
                String str2 = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
                Method method2 = (Method) hashMap.get(str);
                if (method2 != null && method2.getReturnType().equals(List.class)) {
                    zzb(sb, i, zzdv(str2), zzey.zza(method2, (Object) zzgi, new Object[0]));
                }
            }
            if (replaceFirst.endsWith("Map") && !replaceFirst.equals("Map")) {
                String valueOf3 = String.valueOf(replaceFirst.substring(0, 1).toLowerCase());
                String valueOf4 = String.valueOf(replaceFirst.substring(1, replaceFirst.length() - 3));
                String str3 = valueOf4.length() != 0 ? valueOf3.concat(valueOf4) : new String(valueOf3);
                Method method3 = (Method) hashMap.get(str);
                if (method3 != null && method3.getReturnType().equals(Map.class) && !method3.isAnnotationPresent(Deprecated.class) && Modifier.isPublic(method3.getModifiers())) {
                    zzb(sb, i, zzdv(str3), zzey.zza(method3, (Object) zzgi, new Object[0]));
                }
            }
            String valueOf5 = String.valueOf(replaceFirst);
            if (((Method) hashMap2.get(valueOf5.length() != 0 ? "set".concat(valueOf5) : new String("set"))) != null) {
                if (replaceFirst.endsWith("Bytes")) {
                    String valueOf6 = String.valueOf(replaceFirst.substring(0, replaceFirst.length() - 5));
                    if (hashMap.containsKey(valueOf6.length() != 0 ? "get".concat(valueOf6) : new String("get"))) {
                    }
                }
                String valueOf7 = String.valueOf(replaceFirst.substring(0, 1).toLowerCase());
                String valueOf8 = String.valueOf(replaceFirst.substring(1));
                String str4 = valueOf8.length() != 0 ? valueOf7.concat(valueOf8) : new String(valueOf7);
                String valueOf9 = String.valueOf(replaceFirst);
                Method method4 = (Method) hashMap.get(valueOf9.length() != 0 ? "get".concat(valueOf9) : new String("get"));
                String valueOf10 = String.valueOf(replaceFirst);
                Method method5 = (Method) hashMap.get(valueOf10.length() != 0 ? "has".concat(valueOf10) : new String("has"));
                if (method4 != null) {
                    Object zza = zzey.zza(method4, (Object) zzgi, new Object[0]);
                    if (method5 == null) {
                        boolean z = zza instanceof Boolean ? !((Boolean) zza).booleanValue() : zza instanceof Integer ? ((Integer) zza).intValue() == 0 : zza instanceof Float ? ((Float) zza).floatValue() == 0.0f : zza instanceof Double ? ((Double) zza).doubleValue() == 0.0d : zza instanceof String ? zza.equals("") : zza instanceof zzdp ? zza.equals(zzdp.zzadh) : zza instanceof zzgi ? zza == ((zzgi) zza).zzuh() : zza instanceof Enum ? ((Enum) zza).ordinal() == 0 : false;
                        booleanValue = !z;
                    } else {
                        booleanValue = ((Boolean) zzey.zza(method5, (Object) zzgi, new Object[0])).booleanValue();
                    }
                    if (booleanValue) {
                        zzb(sb, i, zzdv(str4), zza);
                    }
                }
            }
        }
        if (zzgi instanceof zzb) {
            Iterator it = ((zzb) zzgi).zzaic.iterator();
            if (it.hasNext()) {
                ((Entry) it.next()).getKey();
                throw new NoSuchMethodError();
            }
        }
        if (((zzey) zzgi).zzahz != null) {
            ((zzey) zzgi).zzahz.zzb(sb, i);
        }
    }

    static final void zzb(StringBuilder sb, int i, String str, Object obj) {
        int i2 = 0;
        if (obj instanceof List) {
            for (Object zzb : (List) obj) {
                zzb(sb, i, str, zzb);
            }
        } else if (obj instanceof Map) {
            for (Entry zzb2 : ((Map) obj).entrySet()) {
                zzb(sb, i, str, zzb2);
            }
        } else {
            sb.append(10);
            for (int i3 = 0; i3 < i; i3++) {
                sb.append(' ');
            }
            sb.append(str);
            if (obj instanceof String) {
                sb.append(": \"").append(zzhl.zzd(zzdp.zzdq((String) obj))).append('\"');
            } else if (obj instanceof zzdp) {
                sb.append(": \"").append(zzhl.zzd((zzdp) obj)).append('\"');
            } else if (obj instanceof zzey) {
                sb.append(" {");
                zza((zzey) obj, sb, i + 2);
                sb.append(StringUtils.f1199LF);
                while (i2 < i) {
                    sb.append(' ');
                    i2++;
                }
                sb.append("}");
            } else if (obj instanceof Entry) {
                sb.append(" {");
                Entry entry = (Entry) obj;
                zzb(sb, i + 2, "key", entry.getKey());
                zzb(sb, i + 2, "value", entry.getValue());
                sb.append(StringUtils.f1199LF);
                while (i2 < i) {
                    sb.append(' ');
                    i2++;
                }
                sb.append("}");
            } else {
                sb.append(": ").append(obj.toString());
            }
        }
    }

    private static final String zzdv(String str) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.length(); i++) {
            char charAt = str.charAt(i);
            if (Character.isUpperCase(charAt)) {
                sb.append(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
            }
            sb.append(Character.toLowerCase(charAt));
        }
        return sb.toString();
    }
}
