package com.google.android.gms.common.images;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.util.Arrays;
import java.util.Locale;
import org.json.JSONException;
import org.json.JSONObject;

public final class WebImage extends zza {
    public static final Creator<WebImage> CREATOR = new zze();
    private final int zzakv;
    private final int zzakw;
    private int zzdxt;
    private final Uri zzeut;

    WebImage(int i, Uri uri, int i2, int i3) {
        this.zzdxt = i;
        this.zzeut = uri;
        this.zzakv = i2;
        this.zzakw = i3;
    }

    public WebImage(Uri uri) throws IllegalArgumentException {
        this(uri, 0, 0);
    }

    public WebImage(Uri uri, int i, int i2) throws IllegalArgumentException {
        this(1, uri, i, i2);
        if (uri == null) {
            throw new IllegalArgumentException("url cannot be null");
        } else if (i < 0 || i2 < 0) {
            throw new IllegalArgumentException("width and height must not be negative");
        }
    }

    public WebImage(JSONObject jSONObject) throws IllegalArgumentException {
        this(zzp(jSONObject), jSONObject.optInt(SettingsJsonConstants.ICON_WIDTH_KEY, 0), jSONObject.optInt(SettingsJsonConstants.ICON_HEIGHT_KEY, 0));
    }

    private static Uri zzp(JSONObject jSONObject) {
        Uri uri = null;
        if (jSONObject.has("url")) {
            try {
                uri = Uri.parse(jSONObject.getString("url"));
            } catch (JSONException e) {
            }
        }
        return uri;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || !(obj instanceof WebImage)) {
            return false;
        }
        WebImage webImage = (WebImage) obj;
        return zzbf.equal(this.zzeut, webImage.zzeut) && this.zzakv == webImage.zzakv && this.zzakw == webImage.zzakw;
    }

    public final int getHeight() {
        return this.zzakw;
    }

    public final Uri getUrl() {
        return this.zzeut;
    }

    public final int getWidth() {
        return this.zzakv;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzeut, Integer.valueOf(this.zzakv), Integer.valueOf(this.zzakw)});
    }

    public final JSONObject toJson() {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("url", this.zzeut.toString());
            jSONObject.put(SettingsJsonConstants.ICON_WIDTH_KEY, this.zzakv);
            jSONObject.put(SettingsJsonConstants.ICON_HEIGHT_KEY, this.zzakw);
        } catch (JSONException e) {
        }
        return jSONObject;
    }

    public final String toString() {
        return String.format(Locale.US, "Image %dx%d %s", new Object[]{Integer.valueOf(this.zzakv), Integer.valueOf(this.zzakw), this.zzeut.toString()});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, getUrl(), i, false);
        zzd.zzc(parcel, 3, getWidth());
        zzd.zzc(parcel, 4, getHeight());
        zzd.zzai(parcel, zze);
    }
}
