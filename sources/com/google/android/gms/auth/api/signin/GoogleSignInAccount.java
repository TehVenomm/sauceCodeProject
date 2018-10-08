package com.google.android.gms.auth.api.signin;

import android.accounts.Account;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.common.util.zzh;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class GoogleSignInAccount extends zza implements ReflectedParcelable {
    public static final Creator<GoogleSignInAccount> CREATOR = new zzb();
    private static zzd zzebw = zzh.zzalc();
    private static Comparator<Scope> zzecd = new zza();
    private int versionCode;
    private String zzbsx;
    private List<Scope> zzdxy;
    private String zzeag;
    private String zzeah;
    private String zzeax;
    private String zzebx;
    private String zzeby;
    private Uri zzebz;
    private String zzeca;
    private long zzecb;
    private String zzecc;

    GoogleSignInAccount(int i, String str, String str2, String str3, String str4, Uri uri, String str5, long j, String str6, List<Scope> list, String str7, String str8) {
        this.versionCode = i;
        this.zzbsx = str;
        this.zzeax = str2;
        this.zzebx = str3;
        this.zzeby = str4;
        this.zzebz = uri;
        this.zzeca = str5;
        this.zzecb = j;
        this.zzecc = str6;
        this.zzdxy = list;
        this.zzeag = str7;
        this.zzeah = str8;
    }

    private final JSONObject toJsonObject() {
        JSONObject jSONObject = new JSONObject();
        try {
            if (getId() != null) {
                jSONObject.put("id", getId());
            }
            if (getIdToken() != null) {
                jSONObject.put("tokenId", getIdToken());
            }
            if (getEmail() != null) {
                jSONObject.put("email", getEmail());
            }
            if (getDisplayName() != null) {
                jSONObject.put("displayName", getDisplayName());
            }
            if (getGivenName() != null) {
                jSONObject.put("givenName", getGivenName());
            }
            if (getFamilyName() != null) {
                jSONObject.put("familyName", getFamilyName());
            }
            if (getPhotoUrl() != null) {
                jSONObject.put("photoUrl", getPhotoUrl().toString());
            }
            if (getServerAuthCode() != null) {
                jSONObject.put("serverAuthCode", getServerAuthCode());
            }
            jSONObject.put("expirationTime", this.zzecb);
            jSONObject.put("obfuscatedIdentifier", this.zzecc);
            JSONArray jSONArray = new JSONArray();
            Collections.sort(this.zzdxy, zzecd);
            for (Scope zzafs : this.zzdxy) {
                jSONArray.put(zzafs.zzafs());
            }
            jSONObject.put("grantedScopes", jSONArray);
            return jSONObject;
        } catch (Throwable e) {
            throw new RuntimeException(e);
        }
    }

    @Nullable
    public static GoogleSignInAccount zzen(@Nullable String str) throws JSONException {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        JSONObject jSONObject = new JSONObject(str);
        Uri uri = null;
        Object optString = jSONObject.optString("photoUrl", null);
        if (!TextUtils.isEmpty(optString)) {
            uri = Uri.parse(optString);
        }
        long parseLong = Long.parseLong(jSONObject.getString("expirationTime"));
        Set hashSet = new HashSet();
        JSONArray jSONArray = jSONObject.getJSONArray("grantedScopes");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            hashSet.add(new Scope(jSONArray.getString(i)));
        }
        String optString2 = jSONObject.optString("id");
        String optString3 = jSONObject.optString("tokenId", null);
        String optString4 = jSONObject.optString("email", null);
        String optString5 = jSONObject.optString("displayName", null);
        String optString6 = jSONObject.optString("givenName", null);
        String optString7 = jSONObject.optString("familyName", null);
        Long valueOf = Long.valueOf(parseLong);
        GoogleSignInAccount googleSignInAccount = new GoogleSignInAccount(3, optString2, optString3, optString4, optString5, uri, null, (valueOf == null ? Long.valueOf(zzebw.currentTimeMillis() / 1000) : valueOf).longValue(), zzbp.zzgf(jSONObject.getString("obfuscatedIdentifier")), new ArrayList((Collection) zzbp.zzu(hashSet)), optString6, optString7);
        googleSignInAccount.zzeca = jSONObject.optString("serverAuthCode", null);
        return googleSignInAccount;
    }

    public boolean equals(Object obj) {
        return !(obj instanceof GoogleSignInAccount) ? false : ((GoogleSignInAccount) obj).toJsonObject().toString().equals(toJsonObject().toString());
    }

    @Nullable
    public Account getAccount() {
        return this.zzebx == null ? null : new Account(this.zzebx, "com.google");
    }

    @Nullable
    public String getDisplayName() {
        return this.zzeby;
    }

    @Nullable
    public String getEmail() {
        return this.zzebx;
    }

    @Nullable
    public String getFamilyName() {
        return this.zzeah;
    }

    @Nullable
    public String getGivenName() {
        return this.zzeag;
    }

    @NonNull
    public Set<Scope> getGrantedScopes() {
        return new HashSet(this.zzdxy);
    }

    @Nullable
    public String getId() {
        return this.zzbsx;
    }

    @Nullable
    public String getIdToken() {
        return this.zzeax;
    }

    @Nullable
    public Uri getPhotoUrl() {
        return this.zzebz;
    }

    @Nullable
    public String getServerAuthCode() {
        return this.zzeca;
    }

    public int hashCode() {
        return toJsonObject().toString().hashCode();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, getId(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, getIdToken(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, getEmail(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, getDisplayName(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, getPhotoUrl(), i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, getServerAuthCode(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzecb);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzecc, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 10, this.zzdxy, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 11, getGivenName(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 12, getFamilyName(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzai(parcel, zze);
    }

    public final boolean zzaab() {
        return zzebw.currentTimeMillis() / 1000 >= this.zzecb - 300;
    }

    @NonNull
    public final String zzaac() {
        return this.zzecc;
    }

    public final String zzaad() {
        JSONObject toJsonObject = toJsonObject();
        toJsonObject.remove("serverAuthCode");
        return toJsonObject.toString();
    }
}
