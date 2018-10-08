package com.google.android.gms.auth.api.signin;

import android.accounts.Account;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.auth.api.signin.internal.zzn;
import com.google.android.gms.auth.api.signin.internal.zzo;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class GoogleSignInOptions extends zza implements Optional, ReflectedParcelable {
    public static final Creator<GoogleSignInOptions> CREATOR = new zzd();
    public static final GoogleSignInOptions DEFAULT_GAMES_SIGN_IN = new Builder().requestScopes(SCOPE_GAMES, new Scope[0]).build();
    public static final GoogleSignInOptions DEFAULT_SIGN_IN = new Builder().requestId().requestProfile().build();
    private static Scope SCOPE_GAMES = new Scope(Scopes.GAMES);
    private static Comparator<Scope> zzecd = new zzc();
    public static final Scope zzece = new Scope(Scopes.PROFILE);
    public static final Scope zzecf = new Scope("email");
    public static final Scope zzecg = new Scope("openid");
    private int versionCode;
    private Account zzdva;
    private boolean zzeaq;
    private String zzear;
    private final ArrayList<Scope> zzech;
    private final boolean zzeci;
    private final boolean zzecj;
    private String zzeck;
    private ArrayList<zzn> zzecl;
    private Map<Integer, zzn> zzecm;

    public static final class Builder {
        private Account zzdva;
        private boolean zzeaq;
        private String zzear;
        private boolean zzeci;
        private boolean zzecj;
        private String zzeck;
        private Set<Scope> zzecn = new HashSet();
        private Map<Integer, zzn> zzeco = new HashMap();

        public Builder(@NonNull GoogleSignInOptions googleSignInOptions) {
            zzbp.zzu(googleSignInOptions);
            this.zzecn = new HashSet(googleSignInOptions.zzech);
            this.zzeci = googleSignInOptions.zzeci;
            this.zzecj = googleSignInOptions.zzecj;
            this.zzeaq = googleSignInOptions.zzeaq;
            this.zzear = googleSignInOptions.zzear;
            this.zzdva = googleSignInOptions.zzdva;
            this.zzeck = googleSignInOptions.zzeck;
            this.zzeco = GoogleSignInOptions.zzu(googleSignInOptions.zzecl);
        }

        private final String zzep(String str) {
            zzbp.zzgf(str);
            boolean z = this.zzear == null || this.zzear.equals(str);
            zzbp.zzb(z, (Object) "two different server client ids provided");
            return str;
        }

        public final Builder addExtension(GoogleSignInOptionsExtension googleSignInOptionsExtension) {
            if (this.zzeco.containsKey(Integer.valueOf(1))) {
                throw new IllegalStateException("Only one extension per type may be added");
            }
            this.zzeco.put(Integer.valueOf(1), new zzn(googleSignInOptionsExtension));
            return this;
        }

        public final GoogleSignInOptions build() {
            if (this.zzeaq && (this.zzdva == null || !this.zzecn.isEmpty())) {
                requestId();
            }
            return new GoogleSignInOptions(new ArrayList(this.zzecn), this.zzdva, this.zzeaq, this.zzeci, this.zzecj, this.zzear, this.zzeck, this.zzeco);
        }

        public final Builder requestEmail() {
            this.zzecn.add(GoogleSignInOptions.zzecf);
            return this;
        }

        public final Builder requestId() {
            this.zzecn.add(GoogleSignInOptions.zzecg);
            return this;
        }

        public final Builder requestIdToken(String str) {
            this.zzeaq = true;
            this.zzear = zzep(str);
            return this;
        }

        public final Builder requestProfile() {
            this.zzecn.add(GoogleSignInOptions.zzece);
            return this;
        }

        public final Builder requestScopes(Scope scope, Scope... scopeArr) {
            this.zzecn.add(scope);
            this.zzecn.addAll(Arrays.asList(scopeArr));
            return this;
        }

        public final Builder requestServerAuthCode(String str) {
            return requestServerAuthCode(str, false);
        }

        public final Builder requestServerAuthCode(String str, boolean z) {
            this.zzeci = true;
            this.zzear = zzep(str);
            this.zzecj = z;
            return this;
        }

        public final Builder setAccountName(String str) {
            this.zzdva = new Account(zzbp.zzgf(str), "com.google");
            return this;
        }

        public final Builder setHostedDomain(String str) {
            this.zzeck = zzbp.zzgf(str);
            return this;
        }
    }

    GoogleSignInOptions(int i, ArrayList<Scope> arrayList, Account account, boolean z, boolean z2, boolean z3, String str, String str2, ArrayList<zzn> arrayList2) {
        this(i, (ArrayList) arrayList, account, z, z2, z3, str, str2, zzu(arrayList2));
    }

    private GoogleSignInOptions(int i, ArrayList<Scope> arrayList, Account account, boolean z, boolean z2, boolean z3, String str, String str2, Map<Integer, zzn> map) {
        this.versionCode = i;
        this.zzech = arrayList;
        this.zzdva = account;
        this.zzeaq = z;
        this.zzeci = z2;
        this.zzecj = z3;
        this.zzear = str;
        this.zzeck = str2;
        this.zzecl = new ArrayList(map.values());
        this.zzecm = map;
    }

    private final JSONObject toJsonObject() {
        JSONObject jSONObject = new JSONObject();
        try {
            JSONArray jSONArray = new JSONArray();
            Collections.sort(this.zzech, zzecd);
            ArrayList arrayList = this.zzech;
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Object obj = arrayList.get(i);
                i++;
                jSONArray.put(((Scope) obj).zzafs());
            }
            jSONObject.put("scopes", jSONArray);
            if (this.zzdva != null) {
                jSONObject.put("accountName", this.zzdva.name);
            }
            jSONObject.put("idTokenRequested", this.zzeaq);
            jSONObject.put("forceCodeForRefreshToken", this.zzecj);
            jSONObject.put("serverAuthRequested", this.zzeci);
            if (!TextUtils.isEmpty(this.zzear)) {
                jSONObject.put("serverClientId", this.zzear);
            }
            if (!TextUtils.isEmpty(this.zzeck)) {
                jSONObject.put("hostedDomain", this.zzeck);
            }
            return jSONObject;
        } catch (Throwable e) {
            throw new RuntimeException(e);
        }
    }

    @Nullable
    public static GoogleSignInOptions zzeo(@Nullable String str) throws JSONException {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        JSONObject jSONObject = new JSONObject(str);
        Collection hashSet = new HashSet();
        JSONArray jSONArray = jSONObject.getJSONArray("scopes");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            hashSet.add(new Scope(jSONArray.getString(i)));
        }
        Object optString = jSONObject.optString("accountName", null);
        return new GoogleSignInOptions(3, new ArrayList(hashSet), !TextUtils.isEmpty(optString) ? new Account(optString, "com.google") : null, jSONObject.getBoolean("idTokenRequested"), jSONObject.getBoolean("serverAuthRequested"), jSONObject.getBoolean("forceCodeForRefreshToken"), jSONObject.optString("serverClientId", null), jSONObject.optString("hostedDomain", null), new HashMap());
    }

    private static Map<Integer, zzn> zzu(@Nullable List<zzn> list) {
        Map hashMap = new HashMap();
        if (list != null) {
            for (zzn zzn : list) {
                hashMap.put(Integer.valueOf(zzn.getType()), zzn);
            }
        }
        return hashMap;
    }

    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        try {
            GoogleSignInOptions googleSignInOptions = (GoogleSignInOptions) obj;
            if (this.zzecl.size() > 0 || googleSignInOptions.zzecl.size() > 0 || this.zzech.size() != googleSignInOptions.zzaae().size() || !this.zzech.containsAll(googleSignInOptions.zzaae())) {
                return false;
            }
            if (this.zzdva == null) {
                if (googleSignInOptions.zzdva != null) {
                    return false;
                }
            } else if (!this.zzdva.equals(googleSignInOptions.zzdva)) {
                return false;
            }
            if (TextUtils.isEmpty(this.zzear)) {
                if (!TextUtils.isEmpty(googleSignInOptions.zzear)) {
                    return false;
                }
            } else if (!this.zzear.equals(googleSignInOptions.zzear)) {
                return false;
            }
            return this.zzecj == googleSignInOptions.zzecj && this.zzeaq == googleSignInOptions.zzeaq && this.zzeci == googleSignInOptions.zzeci;
        } catch (ClassCastException e) {
            return false;
        }
    }

    public final Account getAccount() {
        return this.zzdva;
    }

    public Scope[] getScopeArray() {
        return (Scope[]) this.zzech.toArray(new Scope[this.zzech.size()]);
    }

    public final String getServerClientId() {
        return this.zzear;
    }

    public int hashCode() {
        List arrayList = new ArrayList();
        ArrayList arrayList2 = this.zzech;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            arrayList.add(((Scope) obj).zzafs());
        }
        Collections.sort(arrayList);
        return new zzo().zzo(arrayList).zzo(this.zzdva).zzo(this.zzear).zzaq(this.zzecj).zzaq(this.zzeaq).zzaq(this.zzeci).zzaan();
    }

    public final boolean isIdTokenRequested() {
        return this.zzeaq;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zzc(parcel, 2, zzaae(), false);
        zzd.zza(parcel, 3, this.zzdva, i, false);
        zzd.zza(parcel, 4, this.zzeaq);
        zzd.zza(parcel, 5, this.zzeci);
        zzd.zza(parcel, 6, this.zzecj);
        zzd.zza(parcel, 7, this.zzear, false);
        zzd.zza(parcel, 8, this.zzeck, false);
        zzd.zzc(parcel, 9, this.zzecl, false);
        zzd.zzai(parcel, zze);
    }

    public final ArrayList<Scope> zzaae() {
        return new ArrayList(this.zzech);
    }

    public final boolean zzaaf() {
        return this.zzeci;
    }

    public final String zzaag() {
        return toJsonObject().toString();
    }
}
