package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResults;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.GoogleApiManager;
import com.google.android.gms.common.logging.Logger;

public final class zzh {
    private static Logger zzbd = new Logger("GoogleSignInCommon", new String[0]);

    @Nullable
    public static GoogleSignInResult getSignInResultFromIntent(Intent intent) {
        if (intent == null || (!intent.hasExtra("googleSignInStatus") && !intent.hasExtra("googleSignInAccount"))) {
            return null;
        }
        GoogleSignInAccount googleSignInAccount = (GoogleSignInAccount) intent.getParcelableExtra("googleSignInAccount");
        Status status = (Status) intent.getParcelableExtra("googleSignInStatus");
        if (googleSignInAccount != null) {
            status = Status.RESULT_SUCCESS;
        }
        return new GoogleSignInResult(googleSignInAccount, status);
    }

    public static Intent zzc(Context context, GoogleSignInOptions googleSignInOptions) {
        zzbd.mo14040d("getSignInIntent()", new Object[0]);
        SignInConfiguration signInConfiguration = new SignInConfiguration(context.getPackageName(), googleSignInOptions);
        Intent intent = new Intent("com.google.android.gms.auth.GOOGLE_SIGN_IN");
        intent.setPackage(context.getPackageName());
        intent.setClass(context, SignInHubActivity.class);
        Bundle bundle = new Bundle();
        bundle.putParcelable("config", signInConfiguration);
        intent.putExtra("config", bundle);
        return intent;
    }

    /* JADX WARNING: Removed duplicated region for block: B:23:0x0081  */
    /* JADX WARNING: Removed duplicated region for block: B:27:0x0098  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.google.android.gms.common.api.OptionalPendingResult<com.google.android.gms.auth.api.signin.GoogleSignInResult> zzc(com.google.android.gms.common.api.GoogleApiClient r5, android.content.Context r6, com.google.android.gms.auth.api.signin.GoogleSignInOptions r7, boolean r8) {
        /*
            r1 = 0
            r2 = 0
            com.google.android.gms.common.logging.Logger r0 = zzbd
            java.lang.String r3 = "silentSignIn()"
            java.lang.Object[] r4 = new java.lang.Object[r2]
            r0.mo14040d(r3, r4)
            com.google.android.gms.common.logging.Logger r0 = zzbd
            java.lang.String r3 = "getEligibleSavedSignInResult()"
            java.lang.Object[] r4 = new java.lang.Object[r2]
            r0.mo14040d(r3, r4)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r7)
            com.google.android.gms.auth.api.signin.internal.zzp r0 = com.google.android.gms.auth.api.signin.internal.zzp.zzd(r6)
            com.google.android.gms.auth.api.signin.GoogleSignInOptions r3 = r0.zzi()
            if (r3 == 0) goto L_0x0096
            android.accounts.Account r0 = r3.getAccount()
            android.accounts.Account r4 = r7.getAccount()
            if (r0 != 0) goto L_0x0091
            if (r4 != 0) goto L_0x008f
            r0 = 1
        L_0x002e:
            if (r0 == 0) goto L_0x0096
            boolean r0 = r7.isServerAuthCodeRequested()
            if (r0 != 0) goto L_0x0096
            boolean r0 = r7.isIdTokenRequested()
            if (r0 == 0) goto L_0x0050
            boolean r0 = r3.isIdTokenRequested()
            if (r0 == 0) goto L_0x0096
            java.lang.String r0 = r7.getServerClientId()
            java.lang.String r4 = r3.getServerClientId()
            boolean r0 = r0.equals(r4)
            if (r0 == 0) goto L_0x0096
        L_0x0050:
            java.util.HashSet r0 = new java.util.HashSet
            java.util.ArrayList r3 = r3.getScopes()
            r0.<init>(r3)
            java.util.HashSet r3 = new java.util.HashSet
            java.util.ArrayList r4 = r7.getScopes()
            r3.<init>(r4)
            boolean r0 = r0.containsAll(r3)
            if (r0 == 0) goto L_0x0096
            com.google.android.gms.auth.api.signin.internal.zzp r0 = com.google.android.gms.auth.api.signin.internal.zzp.zzd(r6)
            com.google.android.gms.auth.api.signin.GoogleSignInAccount r3 = r0.zzh()
            if (r3 == 0) goto L_0x0096
            boolean r0 = r3.isExpired()
            if (r0 != 0) goto L_0x0096
            com.google.android.gms.auth.api.signin.GoogleSignInResult r0 = new com.google.android.gms.auth.api.signin.GoogleSignInResult
            com.google.android.gms.common.api.Status r4 = com.google.android.gms.common.api.Status.RESULT_SUCCESS
            r0.<init>(r3, r4)
        L_0x007f:
            if (r0 == 0) goto L_0x0098
            com.google.android.gms.common.logging.Logger r1 = zzbd
            java.lang.String r3 = "Eligible saved sign in result found"
            java.lang.Object[] r2 = new java.lang.Object[r2]
            r1.mo14040d(r3, r2)
            com.google.android.gms.common.api.OptionalPendingResult r0 = com.google.android.gms.common.api.PendingResults.immediatePendingResult(r0, r5)
        L_0x008e:
            return r0
        L_0x008f:
            r0 = r2
            goto L_0x002e
        L_0x0091:
            boolean r0 = r0.equals(r4)
            goto L_0x002e
        L_0x0096:
            r0 = r1
            goto L_0x007f
        L_0x0098:
            if (r8 == 0) goto L_0x00aa
            com.google.android.gms.auth.api.signin.GoogleSignInResult r0 = new com.google.android.gms.auth.api.signin.GoogleSignInResult
            com.google.android.gms.common.api.Status r2 = new com.google.android.gms.common.api.Status
            r3 = 4
            r2.<init>(r3)
            r0.<init>(r1, r2)
            com.google.android.gms.common.api.OptionalPendingResult r0 = com.google.android.gms.common.api.PendingResults.immediatePendingResult(r0, r5)
            goto L_0x008e
        L_0x00aa:
            com.google.android.gms.common.logging.Logger r0 = zzbd
            java.lang.String r1 = "trySilentSignIn()"
            java.lang.Object[] r2 = new java.lang.Object[r2]
            r0.mo14040d(r1, r2)
            com.google.android.gms.common.api.internal.OptionalPendingResultImpl r0 = new com.google.android.gms.common.api.internal.OptionalPendingResultImpl
            com.google.android.gms.auth.api.signin.internal.zzi r1 = new com.google.android.gms.auth.api.signin.internal.zzi
            r1.<init>(r5, r6, r7)
            com.google.android.gms.common.api.internal.BaseImplementation$ApiMethodImpl r1 = r5.enqueue(r1)
            r0.<init>(r1)
            goto L_0x008e
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.auth.api.signin.internal.zzh.zzc(com.google.android.gms.common.api.GoogleApiClient, android.content.Context, com.google.android.gms.auth.api.signin.GoogleSignInOptions, boolean):com.google.android.gms.common.api.OptionalPendingResult");
    }

    public static PendingResult<Status> zzc(GoogleApiClient googleApiClient, Context context, boolean z) {
        zzbd.mo14040d("Signing out", new Object[0]);
        zzc(context);
        return z ? PendingResults.immediatePendingResult(Status.RESULT_SUCCESS, googleApiClient) : googleApiClient.execute(new zzk(googleApiClient));
    }

    private static void zzc(Context context) {
        zzp.zzd(context).clear();
        for (GoogleApiClient maybeSignOut : GoogleApiClient.getAllClients()) {
            maybeSignOut.maybeSignOut();
        }
        GoogleApiManager.reportSignOut();
    }

    public static Intent zzd(Context context, GoogleSignInOptions googleSignInOptions) {
        zzbd.mo14040d("getFallbackSignInIntent()", new Object[0]);
        Intent zzc = zzc(context, googleSignInOptions);
        zzc.setAction("com.google.android.gms.auth.APPAUTH_SIGN_IN");
        return zzc;
    }

    public static PendingResult<Status> zzd(GoogleApiClient googleApiClient, Context context, boolean z) {
        zzbd.mo14040d("Revoking access", new Object[0]);
        String savedRefreshToken = Storage.getInstance(context).getSavedRefreshToken();
        zzc(context);
        return z ? zzd.zzc(savedRefreshToken) : googleApiClient.execute(new zzm(googleApiClient));
    }

    public static Intent zze(Context context, GoogleSignInOptions googleSignInOptions) {
        zzbd.mo14040d("getNoImplementationSignInIntent()", new Object[0]);
        Intent zzc = zzc(context, googleSignInOptions);
        zzc.setAction("com.google.android.gms.auth.NO_IMPL");
        return zzc;
    }
}
