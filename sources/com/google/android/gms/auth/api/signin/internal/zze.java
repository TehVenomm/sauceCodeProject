package com.google.android.gms.auth.api.signin.internal;

import android.accounts.Account;
import android.content.Context;
import android.content.Intent;
import android.os.Parcelable;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.OptionalPendingResult;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResults;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzcp;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbcq;
import java.util.HashSet;

public final class zze {
    private static zzbcq zzecu = new zzbcq("GoogleSignInCommon", new String[0]);

    public static GoogleSignInResult getSignInResultFromIntent(Intent intent) {
        if (intent == null || (!intent.hasExtra("googleSignInStatus") && !intent.hasExtra("googleSignInAccount"))) {
            return null;
        }
        GoogleSignInAccount googleSignInAccount = (GoogleSignInAccount) intent.getParcelableExtra("googleSignInAccount");
        Status status = (Status) intent.getParcelableExtra("googleSignInStatus");
        if (googleSignInAccount != null) {
            status = Status.zzfhp;
        }
        return new GoogleSignInResult(googleSignInAccount, status);
    }

    public static Intent zza(Context context, GoogleSignInOptions googleSignInOptions) {
        zzecu.zzb("GoogleSignInCommon", "getSignInIntent()");
        Parcelable signInConfiguration = new SignInConfiguration(context.getPackageName(), googleSignInOptions);
        Intent intent = new Intent("com.google.android.gms.auth.GOOGLE_SIGN_IN");
        intent.setClass(context, SignInHubActivity.class);
        intent.putExtra("config", signInConfiguration);
        return intent;
    }

    public static OptionalPendingResult<GoogleSignInResult> zza(GoogleApiClient googleApiClient, Context context, GoogleSignInOptions googleSignInOptions) {
        Result googleSignInResult;
        zzy zzbm = zzy.zzbm(context);
        zzecu.zzb("GoogleSignInCommon", "getEligibleSavedSignInResult()");
        zzbp.zzu(googleSignInOptions);
        GoogleSignInOptions zzaas = zzbm.zzaas();
        if (zzaas != null) {
            Account account = zzaas.getAccount();
            Account account2 = googleSignInOptions.getAccount();
            boolean equals = account == null ? account2 == null : account.equals(account2);
            if (equals && !googleSignInOptions.zzaaf() && ((!googleSignInOptions.isIdTokenRequested() || (zzaas.isIdTokenRequested() && googleSignInOptions.getServerClientId().equals(zzaas.getServerClientId()))) && new HashSet(zzaas.zzaae()).containsAll(new HashSet(googleSignInOptions.zzaae())))) {
                GoogleSignInAccount zzaar = zzbm.zzaar();
                if (!(zzaar == null || zzaar.zzaab())) {
                    googleSignInResult = new GoogleSignInResult(zzaar, Status.zzfhp);
                    if (googleSignInResult == null) {
                        zzecu.zzb("GoogleSignInCommon", "Eligible saved sign in result found");
                        return PendingResults.zzb(googleSignInResult, googleApiClient);
                    }
                    zzecu.zzb("GoogleSignInCommon", "trySilentSignIn()");
                    return new zzcp(googleApiClient.zzd(new zzf(googleApiClient, zzbm, googleSignInOptions)));
                }
            }
        }
        googleSignInResult = null;
        if (googleSignInResult == null) {
            zzecu.zzb("GoogleSignInCommon", "trySilentSignIn()");
            return new zzcp(googleApiClient.zzd(new zzf(googleApiClient, zzbm, googleSignInOptions)));
        }
        zzecu.zzb("GoogleSignInCommon", "Eligible saved sign in result found");
        return PendingResults.zzb(googleSignInResult, googleApiClient);
    }

    public static PendingResult<Status> zza(GoogleApiClient googleApiClient, Context context) {
        zzecu.zzb("GoogleSignInCommon", "Signing out");
        zzbl(context);
        return googleApiClient.zze(new zzh(googleApiClient));
    }

    public static PendingResult<Status> zzb(GoogleApiClient googleApiClient, Context context) {
        zzecu.zzb("GoogleSignInCommon", "Revoking access");
        zzbl(context);
        return googleApiClient.zze(new zzj(googleApiClient));
    }

    private static void zzbl(Context context) {
        zzy.zzbm(context).zzaat();
        for (GoogleApiClient zzafo : GoogleApiClient.zzafn()) {
            zzafo.zzafo();
        }
        com.google.android.gms.common.api.internal.zzbp.zzaho();
    }
}
