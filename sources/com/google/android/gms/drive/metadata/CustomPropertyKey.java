package com.google.android.gms.drive.metadata;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.regex.Pattern;
import org.json.JSONException;
import org.json.JSONObject;

public class CustomPropertyKey extends zza {
    public static final Creator<CustomPropertyKey> CREATOR = new zzc();
    public static final int PRIVATE = 1;
    public static final int PUBLIC = 0;
    private static final Pattern zzgki = Pattern.compile("[\\w.!@$%^&*()/-]+");
    private int mVisibility;
    private String zzbfl;

    public CustomPropertyKey(String str, int i) {
        boolean z = true;
        zzbp.zzb((Object) str, (Object) "key");
        zzbp.zzb(zzgki.matcher(str).matches(), (Object) "key name characters must be alphanumeric or one of .!@$%^&*()-_/");
        if (!(i == 0 || i == 1)) {
            z = false;
        }
        zzbp.zzb(z, (Object) "visibility must be either PUBLIC or PRIVATE");
        this.zzbfl = str;
        this.mVisibility = i;
    }

    public static CustomPropertyKey fromJson(JSONObject jSONObject) throws JSONException {
        return new CustomPropertyKey(jSONObject.getString("key"), jSONObject.getInt("visibility"));
    }

    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof CustomPropertyKey)) {
            return false;
        }
        CustomPropertyKey customPropertyKey = (CustomPropertyKey) obj;
        return customPropertyKey.getKey().equals(this.zzbfl) && customPropertyKey.getVisibility() == this.mVisibility;
    }

    public String getKey() {
        return this.zzbfl;
    }

    public int getVisibility() {
        return this.mVisibility;
    }

    public int hashCode() {
        String str = this.zzbfl;
        return new StringBuilder(String.valueOf(str).length() + 11).append(str).append(this.mVisibility).toString().hashCode();
    }

    public JSONObject toJson() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("key", getKey());
        jSONObject.put("visibility", getVisibility());
        return jSONObject;
    }

    public String toString() {
        String str = this.zzbfl;
        return new StringBuilder(String.valueOf(str).length() + 31).append("CustomPropertyKey(").append(str).append(",").append(this.mVisibility).append(")").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzbfl, false);
        zzd.zzc(parcel, 3, this.mVisibility);
        zzd.zzai(parcel, zze);
    }
}
