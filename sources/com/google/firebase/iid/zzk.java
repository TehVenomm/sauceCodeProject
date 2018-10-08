package com.google.firebase.iid;

import android.support.annotation.Nullable;
import android.text.TextUtils;

public final class zzk {
    private static final Object zzaqm = new Object();
    private final zzr zzmji;

    zzk(zzr zzr) {
        this.zzmji = zzr;
    }

    @Nullable
    final String zzbyn() {
        String str = null;
        synchronized (zzaqm) {
            String string = this.zzmji.zzhud.getString("topic_operaion_queue", null);
            if (string != null) {
                String[] split = string.split(",");
                if (split.length > 1 && !TextUtils.isEmpty(split[1])) {
                    str = split[1];
                }
            }
        }
        return str;
    }

    final void zzpq(String str) {
        synchronized (zzaqm) {
            String string = this.zzmji.zzhud.getString("topic_operaion_queue", "");
            this.zzmji.zzhud.edit().putString("topic_operaion_queue", new StringBuilder((String.valueOf(string).length() + String.valueOf(",").length()) + String.valueOf(str).length()).append(string).append(",").append(str).toString()).apply();
        }
    }

    final boolean zzpu(String str) {
        boolean z;
        synchronized (zzaqm) {
            String string = this.zzmji.zzhud.getString("topic_operaion_queue", "");
            String valueOf = String.valueOf(",");
            String valueOf2 = String.valueOf(str);
            if (string.startsWith(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf))) {
                valueOf = String.valueOf(",");
                valueOf2 = String.valueOf(str);
                this.zzmji.zzhud.edit().putString("topic_operaion_queue", string.substring((valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf)).length())).apply();
                z = true;
            } else {
                z = false;
            }
        }
        return z;
    }
}
