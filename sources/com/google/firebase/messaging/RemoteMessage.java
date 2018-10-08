package com.google.firebase.messaging;

import android.net.Uri;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.firebase.iid.FirebaseInstanceId;
import java.util.Map;
import java.util.Map.Entry;

public final class RemoteMessage extends zza {
    public static final Creator<RemoteMessage> CREATOR = new zzf();
    Bundle mBundle;
    private Map<String, String> zzdkx;
    private Notification zzmjx;

    public static class Builder {
        private final Bundle mBundle = new Bundle();
        private final Map<String, String> zzdkx = new ArrayMap();

        public Builder(String str) {
            if (TextUtils.isEmpty(str)) {
                String valueOf = String.valueOf(str);
                throw new IllegalArgumentException(valueOf.length() != 0 ? "Invalid to: ".concat(valueOf) : new String("Invalid to: "));
            } else {
                this.mBundle.putString("google.to", str);
            }
        }

        public Builder addData(String str, String str2) {
            this.zzdkx.put(str, str2);
            return this;
        }

        public RemoteMessage build() {
            Bundle bundle = new Bundle();
            for (Entry entry : this.zzdkx.entrySet()) {
                bundle.putString((String) entry.getKey(), (String) entry.getValue());
            }
            bundle.putAll(this.mBundle);
            String token = FirebaseInstanceId.getInstance().getToken();
            if (token != null) {
                this.mBundle.putString("from", token);
            } else {
                this.mBundle.remove("from");
            }
            return new RemoteMessage(bundle);
        }

        public Builder clearData() {
            this.zzdkx.clear();
            return this;
        }

        public Builder setCollapseKey(String str) {
            this.mBundle.putString("collapse_key", str);
            return this;
        }

        public Builder setData(Map<String, String> map) {
            this.zzdkx.clear();
            this.zzdkx.putAll(map);
            return this;
        }

        public Builder setMessageId(String str) {
            this.mBundle.putString("google.message_id", str);
            return this;
        }

        public Builder setMessageType(String str) {
            this.mBundle.putString("message_type", str);
            return this;
        }

        public Builder setTtl(int i) {
            this.mBundle.putString("google.ttl", String.valueOf(i));
            return this;
        }
    }

    public static class Notification {
        private final String mTag;
        private final String zzbrs;
        private final String zzehi;
        private final String zzmjy;
        private final String[] zzmjz;
        private final String zzmka;
        private final String[] zzmkb;
        private final String zzmkc;
        private final String zzmkd;
        private final String zzmke;
        private final String zzmkf;
        private final Uri zzmkg;

        private Notification(Bundle bundle) {
            this.zzehi = zza.zze(bundle, "gcm.n.title");
            this.zzmjy = zza.zzh(bundle, "gcm.n.title");
            this.zzmjz = zzk(bundle, "gcm.n.title");
            this.zzbrs = zza.zze(bundle, "gcm.n.body");
            this.zzmka = zza.zzh(bundle, "gcm.n.body");
            this.zzmkb = zzk(bundle, "gcm.n.body");
            this.zzmkc = zza.zze(bundle, "gcm.n.icon");
            this.zzmkd = zza.zzae(bundle);
            this.mTag = zza.zze(bundle, "gcm.n.tag");
            this.zzmke = zza.zze(bundle, "gcm.n.color");
            this.zzmkf = zza.zze(bundle, "gcm.n.click_action");
            this.zzmkg = zza.zzad(bundle);
        }

        private static String[] zzk(Bundle bundle, String str) {
            Object[] zzi = zza.zzi(bundle, str);
            if (zzi == null) {
                return null;
            }
            String[] strArr = new String[zzi.length];
            for (int i = 0; i < zzi.length; i++) {
                strArr[i] = String.valueOf(zzi[i]);
            }
            return strArr;
        }

        @Nullable
        public String getBody() {
            return this.zzbrs;
        }

        @Nullable
        public String[] getBodyLocalizationArgs() {
            return this.zzmkb;
        }

        @Nullable
        public String getBodyLocalizationKey() {
            return this.zzmka;
        }

        @Nullable
        public String getClickAction() {
            return this.zzmkf;
        }

        @Nullable
        public String getColor() {
            return this.zzmke;
        }

        @Nullable
        public String getIcon() {
            return this.zzmkc;
        }

        @Nullable
        public Uri getLink() {
            return this.zzmkg;
        }

        @Nullable
        public String getSound() {
            return this.zzmkd;
        }

        @Nullable
        public String getTag() {
            return this.mTag;
        }

        @Nullable
        public String getTitle() {
            return this.zzehi;
        }

        @Nullable
        public String[] getTitleLocalizationArgs() {
            return this.zzmjz;
        }

        @Nullable
        public String getTitleLocalizationKey() {
            return this.zzmjy;
        }
    }

    RemoteMessage(Bundle bundle) {
        this.mBundle = bundle;
    }

    public final String getCollapseKey() {
        return this.mBundle.getString("collapse_key");
    }

    public final Map<String, String> getData() {
        if (this.zzdkx == null) {
            this.zzdkx = new ArrayMap();
            for (String str : this.mBundle.keySet()) {
                Object obj = this.mBundle.get(str);
                if (obj instanceof String) {
                    String str2 = (String) obj;
                    if (!(str.startsWith("google.") || str.startsWith("gcm.") || str.equals("from") || str.equals("message_type") || str.equals("collapse_key"))) {
                        this.zzdkx.put(str, str2);
                    }
                }
            }
        }
        return this.zzdkx;
    }

    public final String getFrom() {
        return this.mBundle.getString("from");
    }

    public final String getMessageId() {
        String string = this.mBundle.getString("google.message_id");
        return string == null ? this.mBundle.getString("message_id") : string;
    }

    public final String getMessageType() {
        return this.mBundle.getString("message_type");
    }

    public final Notification getNotification() {
        if (this.zzmjx == null && zza.zzac(this.mBundle)) {
            this.zzmjx = new Notification(this.mBundle);
        }
        return this.zzmjx;
    }

    public final long getSentTime() {
        Object obj = this.mBundle.get("google.sent_time");
        if (obj instanceof Long) {
            return ((Long) obj).longValue();
        }
        if (obj instanceof String) {
            try {
                return Long.parseLong((String) obj);
            } catch (NumberFormatException e) {
                String valueOf = String.valueOf(obj);
                Log.w("FirebaseMessaging", new StringBuilder(String.valueOf(valueOf).length() + 19).append("Invalid sent time: ").append(valueOf).toString());
            }
        }
        return 0;
    }

    public final String getTo() {
        return this.mBundle.getString("google.to");
    }

    public final int getTtl() {
        Object obj = this.mBundle.get("google.ttl");
        if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        }
        if (obj instanceof String) {
            try {
                return Integer.parseInt((String) obj);
            } catch (NumberFormatException e) {
                String valueOf = String.valueOf(obj);
                Log.w("FirebaseMessaging", new StringBuilder(String.valueOf(valueOf).length() + 13).append("Invalid TTL: ").append(valueOf).toString());
            }
        }
        return 0;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.mBundle, false);
        zzd.zzai(parcel, zze);
    }
}
