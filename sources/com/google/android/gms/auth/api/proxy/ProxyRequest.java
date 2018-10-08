package com.google.android.gms.auth.api.proxy;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Patterns;
import com.google.android.gms.common.annotation.KeepForSdkWithMembers;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.Collections;
import java.util.LinkedHashMap;
import java.util.Map;

@KeepForSdkWithMembers
public class ProxyRequest extends zza {
    public static final Creator<ProxyRequest> CREATOR = new zza();
    public static final int HTTP_METHOD_DELETE = 3;
    public static final int HTTP_METHOD_GET = 0;
    public static final int HTTP_METHOD_HEAD = 4;
    public static final int HTTP_METHOD_OPTIONS = 5;
    public static final int HTTP_METHOD_PATCH = 7;
    public static final int HTTP_METHOD_POST = 1;
    public static final int HTTP_METHOD_PUT = 2;
    public static final int HTTP_METHOD_TRACE = 6;
    public static final int LAST_CODE = 7;
    public static final int VERSION_CODE = 2;
    public final byte[] body;
    public final int httpMethod;
    public final long timeoutMillis;
    public final String url;
    private int versionCode;
    private Bundle zzebo;

    @KeepForSdkWithMembers
    public static class Builder {
        private long zzcwc = 3000;
        private String zzebp;
        private int zzebq = ProxyRequest.HTTP_METHOD_GET;
        private byte[] zzebr = null;
        private Bundle zzebs = new Bundle();

        public Builder(String str) {
            zzbp.zzgf(str);
            if (Patterns.WEB_URL.matcher(str).matches()) {
                this.zzebp = str;
                return;
            }
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 51).append("The supplied url [ ").append(str).append("] is not match Patterns.WEB_URL!").toString());
        }

        public ProxyRequest build() {
            if (this.zzebr == null) {
                this.zzebr = new byte[0];
            }
            return new ProxyRequest(2, this.zzebp, this.zzebq, this.zzcwc, this.zzebr, this.zzebs);
        }

        public Builder putHeader(String str, String str2) {
            zzbp.zzh(str, "Header name cannot be null or empty!");
            Bundle bundle = this.zzebs;
            if (str2 == null) {
                str2 = "";
            }
            bundle.putString(str, str2);
            return this;
        }

        public Builder setBody(byte[] bArr) {
            this.zzebr = bArr;
            return this;
        }

        public Builder setHttpMethod(int i) {
            boolean z = i >= 0 && i <= ProxyRequest.LAST_CODE;
            zzbp.zzb(z, (Object) "Unrecognized http method code.");
            this.zzebq = i;
            return this;
        }

        public Builder setTimeoutMillis(long j) {
            zzbp.zzb(j >= 0, (Object) "The specified timeout must be non-negative.");
            this.zzcwc = j;
            return this;
        }
    }

    ProxyRequest(int i, String str, int i2, long j, byte[] bArr, Bundle bundle) {
        this.versionCode = i;
        this.url = str;
        this.httpMethod = i2;
        this.timeoutMillis = j;
        this.body = bArr;
        this.zzebo = bundle;
    }

    public Map<String, String> getHeaderMap() {
        Map linkedHashMap = new LinkedHashMap(this.zzebo.size());
        for (String str : this.zzebo.keySet()) {
            linkedHashMap.put(str, this.zzebo.getString(str));
        }
        return Collections.unmodifiableMap(linkedHashMap);
    }

    public String toString() {
        String str = this.url;
        return new StringBuilder(String.valueOf(str).length() + 42).append("ProxyRequest[ url: ").append(str).append(", method: ").append(this.httpMethod).append(" ]").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.url, false);
        zzd.zzc(parcel, 2, this.httpMethod);
        zzd.zza(parcel, 3, this.timeoutMillis);
        zzd.zza(parcel, 4, this.body, false);
        zzd.zza(parcel, 5, this.zzebo, false);
        zzd.zzc(parcel, 1000, this.versionCode);
        zzd.zzai(parcel, zze);
    }
}
