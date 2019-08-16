package com.google.android.gms.drive.query;

import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.zzb;
import com.google.android.gms.drive.query.internal.zzj;
import com.google.android.gms.drive.query.internal.zzx;
import java.util.Iterator;
import java.util.List;

public final class zzd implements zzj<String> {
    public final /* synthetic */ Object zza(zzb zzb, Object obj) {
        return String.format("contains(%s,%s)", new Object[]{zzb.getName(), obj});
    }

    public final /* synthetic */ Object zza(zzx zzx, MetadataField metadataField, Object obj) {
        return String.format("cmp(%s,%s,%s)", new Object[]{zzx.getTag(), metadataField.getName(), obj});
    }

    public final /* synthetic */ Object zza(zzx zzx, List list) {
        StringBuilder sb = new StringBuilder(String.valueOf(zzx.getTag()).concat("("));
        Iterator it = list.iterator();
        String str = "";
        while (true) {
            String str2 = str;
            if (!it.hasNext()) {
                return sb.append(")").toString();
            }
            String str3 = (String) it.next();
            sb.append(str2);
            sb.append(str3);
            str = ",";
        }
    }

    public final /* synthetic */ Object zza(Object obj) {
        return String.format("not(%s)", new Object[]{(String) obj});
    }

    public final /* synthetic */ Object zzbb() {
        return "ownedByMe()";
    }

    public final /* synthetic */ Object zzbc() {
        return "all()";
    }

    public final /* synthetic */ Object zzc(MetadataField metadataField, Object obj) {
        return String.format("has(%s,%s)", new Object[]{metadataField.getName(), obj});
    }

    public final /* synthetic */ Object zze(MetadataField metadataField) {
        return String.format("fieldOnly(%s)", new Object[]{metadataField.getName()});
    }

    public final /* synthetic */ Object zzg(String str) {
        return String.format("fullTextSearch(%s)", new Object[]{str});
    }
}
