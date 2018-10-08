package com.google.firebase;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzbz;
import com.google.android.gms.common.util.zzs;
import java.util.Arrays;

public final class FirebaseOptions {
    private final String zzehw;
    private final String zzleo;
    private final String zzlep;
    private final String zzleq;
    private final String zzler;
    private final String zzles;
    private final String zzlet;

    public static final class Builder {
        private String zzehw;
        private String zzleo;
        private String zzlep;
        private String zzleq;
        private String zzler;
        private String zzles;
        private String zzlet;

        public Builder(FirebaseOptions firebaseOptions) {
            this.zzehw = firebaseOptions.zzehw;
            this.zzleo = firebaseOptions.zzleo;
            this.zzlep = firebaseOptions.zzlep;
            this.zzleq = firebaseOptions.zzleq;
            this.zzler = firebaseOptions.zzler;
            this.zzles = firebaseOptions.zzles;
            this.zzlet = firebaseOptions.zzlet;
        }

        public final FirebaseOptions build() {
            return new FirebaseOptions(this.zzehw, this.zzleo, this.zzlep, this.zzleq, this.zzler, this.zzles, this.zzlet);
        }

        public final Builder setApiKey(@NonNull String str) {
            this.zzleo = zzbp.zzh(str, "ApiKey must be set.");
            return this;
        }

        public final Builder setApplicationId(@NonNull String str) {
            this.zzehw = zzbp.zzh(str, "ApplicationId must be set.");
            return this;
        }

        public final Builder setDatabaseUrl(@Nullable String str) {
            this.zzlep = str;
            return this;
        }

        public final Builder setGcmSenderId(@Nullable String str) {
            this.zzler = str;
            return this;
        }

        public final Builder setProjectId(@Nullable String str) {
            this.zzlet = str;
            return this;
        }

        public final Builder setStorageBucket(@Nullable String str) {
            this.zzles = str;
            return this;
        }
    }

    private FirebaseOptions(@NonNull String str, @NonNull String str2, @Nullable String str3, @Nullable String str4, @Nullable String str5, @Nullable String str6, @Nullable String str7) {
        zzbp.zza(!zzs.zzgl(str), (Object) "ApplicationId must be set.");
        this.zzehw = str;
        this.zzleo = str2;
        this.zzlep = str3;
        this.zzleq = str4;
        this.zzler = str5;
        this.zzles = str6;
        this.zzlet = str7;
    }

    public static FirebaseOptions fromResource(Context context) {
        zzbz zzbz = new zzbz(context);
        Object string = zzbz.getString("google_app_id");
        return TextUtils.isEmpty(string) ? null : new FirebaseOptions(string, zzbz.getString("google_api_key"), zzbz.getString("firebase_database_url"), zzbz.getString("ga_trackingId"), zzbz.getString("gcm_defaultSenderId"), zzbz.getString("google_storage_bucket"), zzbz.getString("project_id"));
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof FirebaseOptions)) {
            return false;
        }
        FirebaseOptions firebaseOptions = (FirebaseOptions) obj;
        return zzbf.equal(this.zzehw, firebaseOptions.zzehw) && zzbf.equal(this.zzleo, firebaseOptions.zzleo) && zzbf.equal(this.zzlep, firebaseOptions.zzlep) && zzbf.equal(this.zzleq, firebaseOptions.zzleq) && zzbf.equal(this.zzler, firebaseOptions.zzler) && zzbf.equal(this.zzles, firebaseOptions.zzles) && zzbf.equal(this.zzlet, firebaseOptions.zzlet);
    }

    public final String getApiKey() {
        return this.zzleo;
    }

    public final String getApplicationId() {
        return this.zzehw;
    }

    public final String getDatabaseUrl() {
        return this.zzlep;
    }

    public final String getGcmSenderId() {
        return this.zzler;
    }

    public final String getProjectId() {
        return this.zzlet;
    }

    public final String getStorageBucket() {
        return this.zzles;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzehw, this.zzleo, this.zzlep, this.zzleq, this.zzler, this.zzles, this.zzlet});
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("applicationId", this.zzehw).zzg("apiKey", this.zzleo).zzg("databaseUrl", this.zzlep).zzg("gcmSenderId", this.zzler).zzg("storageBucket", this.zzles).zzg("projectId", this.zzlet).toString();
    }
}
