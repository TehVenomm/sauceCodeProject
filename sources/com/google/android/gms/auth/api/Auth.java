package com.google.android.gms.auth.api;

import android.os.Bundle;
import android.support.annotation.NonNull;
import com.google.android.gms.auth.api.credentials.CredentialsApi;
import com.google.android.gms.auth.api.credentials.PasswordSpecification;
import com.google.android.gms.auth.api.proxy.ProxyApi;
import com.google.android.gms.auth.api.signin.GoogleSignInApi;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.internal.zzc;
import com.google.android.gms.auth.api.signin.internal.zzd;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.internal.zzari;
import com.google.android.gms.internal.zzarj;
import com.google.android.gms.internal.zzark;
import com.google.android.gms.internal.zzasg;
import com.google.android.gms.internal.zzaso;
import com.google.android.gms.internal.zzatn;

public final class Auth {
    public static final Api<AuthCredentialsOptions> CREDENTIALS_API = new Api("Auth.CREDENTIALS_API", zzdyg, zzdyd);
    public static final CredentialsApi CredentialsApi = new zzasg();
    public static final Api<GoogleSignInOptions> GOOGLE_SIGN_IN_API = new Api("Auth.GOOGLE_SIGN_IN_API", zzdyi, zzdyf);
    public static final GoogleSignInApi GoogleSignInApi = new zzc();
    @KeepForSdk
    public static final Api<zzf> PROXY_API = zzd.API;
    @KeepForSdk
    public static final ProxyApi ProxyApi = new zzatn();
    public static final zzf<zzaso> zzdyd = new zzf();
    private static zzf<zzark> zzdye = new zzf();
    public static final zzf<zzd> zzdyf = new zzf();
    private static final zza<zzaso, AuthCredentialsOptions> zzdyg = new zza();
    private static final zza<zzark, NoOptions> zzdyh = new zzb();
    private static final zza<zzd, GoogleSignInOptions> zzdyi = new zzc();
    private static Api<NoOptions> zzdyj = new Api("Auth.ACCOUNT_STATUS_API", zzdyh, zzdye);
    private static zzari zzdyk = new zzarj();

    public static final class AuthCredentialsOptions implements Optional {
        private static AuthCredentialsOptions zzdyl = new AuthCredentialsOptions(new Builder());
        private final String zzdym = null;
        private final PasswordSpecification zzdyn;

        public static class Builder {
            @NonNull
            private PasswordSpecification zzdyn = PasswordSpecification.zzeay;
        }

        private AuthCredentialsOptions(Builder builder) {
            this.zzdyn = builder.zzdyn;
        }

        public final Bundle zzzs() {
            Bundle bundle = new Bundle();
            bundle.putString("consumer_package", null);
            bundle.putParcelable("password_specification", this.zzdyn);
            return bundle;
        }

        public final PasswordSpecification zzzv() {
            return this.zzdyn;
        }
    }

    private Auth() {
    }
}
