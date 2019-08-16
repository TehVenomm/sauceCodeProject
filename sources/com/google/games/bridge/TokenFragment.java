package com.google.games.bridge;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions.Builder;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.games.Games;
import com.google.android.gms.games.Games.GamesOptions;

public class TokenFragment extends Fragment implements ConnectionCallbacks {
    private static final String FRAGMENT_TAG = "gpg.AuthTokenSupport";
    private static final int RC_ACCT = 9002;
    private static final String TAG = "TokenFragment";
    private static TokenFragment helperFragment;
    private static final Object lock = new Object();
    private static TokenRequest pendingTokenRequest;
    /* access modifiers changed from: private */
    public GoogleApiClient mGoogleApiClient;

    private static class TokenRequest {
        /* access modifiers changed from: private */
        public String accountName;
        /* access modifiers changed from: private */
        public boolean doAuthCode;
        /* access modifiers changed from: private */
        public boolean doEmail;
        /* access modifiers changed from: private */
        public boolean doIdToken;
        private boolean forceRefresh;
        /* access modifiers changed from: private */
        public boolean hidePopups;
        private TokenPendingResult pendingResponse = new TokenPendingResult();
        /* access modifiers changed from: private */
        public String[] scopes;
        private String webClientId;

        public TokenRequest(boolean z, boolean z2, boolean z3, String str, boolean z4, String[] strArr, boolean z5, String str2) {
            this.doAuthCode = z;
            this.doEmail = z2;
            this.doIdToken = z3;
            this.webClientId = str;
            this.forceRefresh = z4;
            if (strArr == null || strArr.length <= 0) {
                this.scopes = null;
            } else {
                this.scopes = new String[strArr.length];
                System.arraycopy(strArr, 0, this.scopes, 0, strArr.length);
            }
            this.hidePopups = z5;
            this.accountName = str2;
        }

        public void cancel() {
            this.pendingResponse.cancel();
        }

        public String getAuthCode() {
            return this.pendingResponse.result.getAuthCode();
        }

        public String getEmail() {
            return this.pendingResponse.result.getEmail();
        }

        public boolean getForceRefresh() {
            return this.forceRefresh;
        }

        public String getIdToken() {
            return this.pendingResponse.result.getIdToken();
        }

        public PendingResult<TokenResult> getPendingResponse() {
            return this.pendingResponse;
        }

        public String getWebClientId() {
            return this.webClientId == null ? "" : this.webClientId;
        }

        public void setAuthCode(String str) {
            this.pendingResponse.setAuthCode(str);
        }

        public void setEmail(String str) {
            this.pendingResponse.setEmail(str);
        }

        public void setIdToken(String str) {
            this.pendingResponse.setIdToken(str);
        }

        public void setResult(int i) {
            this.pendingResponse.setStatus(i);
        }

        public String toString() {
            return Integer.toHexString(hashCode()) + " (a:" + this.doAuthCode + " e:" + this.doEmail + " i:" + this.doIdToken + " wc: " + this.webClientId + " f: " + this.forceRefresh + ")";
        }
    }

    private void buildClient(TokenRequest tokenRequest) {
        Log.d(TAG, "Building client for: " + tokenRequest);
        Builder builder = new Builder(GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN);
        if (tokenRequest.doAuthCode) {
            if (!tokenRequest.getWebClientId().isEmpty()) {
                builder.requestServerAuthCode(tokenRequest.getWebClientId(), tokenRequest.getForceRefresh());
            } else {
                Log.e(TAG, "Web client ID is needed for Auth Code");
                tokenRequest.setResult(10);
                synchronized (lock) {
                    pendingTokenRequest = null;
                }
                return;
            }
        }
        if (tokenRequest.doEmail) {
            builder.requestEmail();
        }
        if (tokenRequest.doIdToken) {
            if (!tokenRequest.getWebClientId().isEmpty()) {
                builder.requestIdToken(tokenRequest.getWebClientId());
            } else {
                Log.e(TAG, "Web client ID is needed for ID Token");
                tokenRequest.setResult(10);
                synchronized (lock) {
                    pendingTokenRequest = null;
                }
                return;
            }
        }
        if (tokenRequest.scopes != null) {
            for (String scope : tokenRequest.scopes) {
                builder.requestScopes(new Scope(scope), new Scope[0]);
            }
        }
        if (tokenRequest.hidePopups) {
            Log.d(TAG, "hiding popup views for games API");
            builder.addExtension(GamesOptions.builder().setShowConnectingPopup(false).build());
        }
        if (tokenRequest.accountName != null && !tokenRequest.accountName.isEmpty()) {
            builder.setAccountName(tokenRequest.accountName);
        }
        GoogleApiClient.Builder addApi = new GoogleApiClient.Builder(getActivity()).addApi(Auth.GOOGLE_SIGN_IN_API, builder.build());
        addApi.addApi(Games.API);
        addApi.addConnectionCallbacks(this);
        if (tokenRequest.hidePopups) {
            View view = new View(getActivity());
            view.setVisibility(4);
            view.setClickable(false);
            addApi.setViewForPopups(view);
        }
        this.mGoogleApiClient = addApi.build();
        this.mGoogleApiClient.connect(2);
    }

    /* JADX WARNING: Removed duplicated region for block: B:24:0x00b0  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.google.android.gms.common.api.PendingResult fetchToken(android.app.Activity r9, boolean r10, boolean r11, boolean r12, java.lang.String r13, boolean r14, java.lang.String[] r15, boolean r16, java.lang.String r17) {
        /*
            com.google.games.bridge.TokenFragment$TokenRequest r0 = new com.google.games.bridge.TokenFragment$TokenRequest
            r1 = r10
            r2 = r11
            r3 = r12
            r4 = r13
            r5 = r14
            r6 = r15
            r7 = r16
            r8 = r17
            r0.<init>(r1, r2, r3, r4, r5, r6, r7, r8)
            r1 = 0
            java.lang.Object r2 = lock
            monitor-enter(r2)
            com.google.games.bridge.TokenFragment$TokenRequest r3 = pendingTokenRequest     // Catch:{ all -> 0x0059 }
            if (r3 != 0) goto L_0x001a
            pendingTokenRequest = r0     // Catch:{ all -> 0x0059 }
            r1 = 1
        L_0x001a:
            monitor-exit(r2)     // Catch:{ all -> 0x0059 }
            if (r1 != 0) goto L_0x005c
            java.lang.String r1 = "TokenFragment"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Already a pending token request (requested == ): "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.StringBuilder r2 = r2.append(r0)
            java.lang.String r2 = r2.toString()
            android.util.Log.e(r1, r2)
            java.lang.String r1 = "TokenFragment"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Already a pending token request: "
            java.lang.StringBuilder r2 = r2.append(r3)
            com.google.games.bridge.TokenFragment$TokenRequest r3 = pendingTokenRequest
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            android.util.Log.e(r1, r2)
            r1 = 10
            r0.setResult(r1)
            com.google.android.gms.common.api.PendingResult r0 = r0.getPendingResponse()
        L_0x0058:
            return r0
        L_0x0059:
            r0 = move-exception
            monitor-exit(r2)     // Catch:{ all -> 0x0059 }
            throw r0
        L_0x005c:
            android.app.FragmentManager r1 = r9.getFragmentManager()
            java.lang.String r2 = "gpg.AuthTokenSupport"
            android.app.Fragment r1 = r1.findFragmentByTag(r2)
            com.google.games.bridge.TokenFragment r1 = (com.google.games.bridge.TokenFragment) r1
            if (r1 != 0) goto L_0x00b8
            java.lang.String r1 = "TokenFragment"
            java.lang.String r2 = "Creating fragment"
            android.util.Log.d(r1, r2)     // Catch:{ Throwable -> 0x00c3 }
            com.google.games.bridge.TokenFragment r1 = new com.google.games.bridge.TokenFragment     // Catch:{ Throwable -> 0x00c3 }
            r1.<init>()     // Catch:{ Throwable -> 0x00c3 }
            android.app.FragmentManager r2 = r9.getFragmentManager()     // Catch:{ Throwable -> 0x008b }
            android.app.FragmentTransaction r2 = r2.beginTransaction()     // Catch:{ Throwable -> 0x008b }
            java.lang.String r3 = "gpg.AuthTokenSupport"
            r2.add(r1, r3)     // Catch:{ Throwable -> 0x008b }
            r2.commit()     // Catch:{ Throwable -> 0x008b }
        L_0x0086:
            com.google.android.gms.common.api.PendingResult r0 = r0.getPendingResponse()
            goto L_0x0058
        L_0x008b:
            r1 = move-exception
        L_0x008c:
            java.lang.String r2 = "TokenFragment"
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r3.<init>()
            java.lang.String r4 = "Cannot launch token fragment:"
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.String r4 = r1.getMessage()
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.String r3 = r3.toString()
            android.util.Log.e(r2, r3, r1)
            r1 = 13
            r0.setResult(r1)
            java.lang.Object r1 = lock
            monitor-enter(r1)
            r2 = 0
            pendingTokenRequest = r2     // Catch:{ all -> 0x00b5 }
            monitor-exit(r1)     // Catch:{ all -> 0x00b5 }
            goto L_0x0086
        L_0x00b5:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x00b5 }
            throw r0
        L_0x00b8:
            java.lang.String r2 = "TokenFragment"
            java.lang.String r3 = "Fragment exists.. calling processRequests"
            android.util.Log.d(r2, r3)
            r1.processRequest()
            goto L_0x0086
        L_0x00c3:
            r1 = move-exception
            goto L_0x008c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.games.bridge.TokenFragment.fetchToken(android.app.Activity, boolean, boolean, boolean, java.lang.String, boolean, java.lang.String[], boolean, java.lang.String):com.google.android.gms.common.api.PendingResult");
    }

    /* access modifiers changed from: private */
    public void onSignedIn(int i, GoogleSignInAccount googleSignInAccount) {
        TokenRequest tokenRequest;
        synchronized (lock) {
            tokenRequest = pendingTokenRequest;
            pendingTokenRequest = null;
        }
        if (tokenRequest != null) {
            if (googleSignInAccount != null) {
                tokenRequest.setAuthCode(googleSignInAccount.getServerAuthCode());
                tokenRequest.setEmail(googleSignInAccount.getEmail());
                tokenRequest.setIdToken(googleSignInAccount.getIdToken());
            }
            tokenRequest.setResult(i);
        }
    }

    private void processRequest() {
        TokenRequest tokenRequest;
        TokenRequest tokenRequest2;
        synchronized (lock) {
            tokenRequest = pendingTokenRequest;
        }
        if (tokenRequest != null) {
            buildClient(tokenRequest);
            synchronized (lock) {
                tokenRequest2 = pendingTokenRequest;
            }
            if (tokenRequest2 != null) {
                if (this.mGoogleApiClient.hasConnectedApi(Games.API)) {
                    Auth.GoogleSignInApi.silentSignIn(this.mGoogleApiClient).setResultCallback(new ResultCallback<GoogleSignInResult>() {
                        public void onResult(@NonNull GoogleSignInResult googleSignInResult) {
                            if (googleSignInResult.isSuccess()) {
                                TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), googleSignInResult.getSignInAccount());
                            } else if (googleSignInResult.getStatus().getStatusCode() == 4) {
                                TokenFragment.this.startActivityForResult(Auth.GoogleSignInApi.getSignInIntent(TokenFragment.this.mGoogleApiClient), 9002);
                            } else {
                                Log.e(TokenFragment.TAG, "Error with silentSignIn: " + googleSignInResult.getStatus());
                                TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), null);
                            }
                        }
                    });
                } else {
                    Log.d(TAG, "No connected Games API,!!!!  Hoping for connection!");
                }
            }
            Log.d(TAG, "Done with processRequest!");
        }
    }

    private void reset() {
        if (this.mGoogleApiClient != null) {
            if (this.mGoogleApiClient.hasConnectedApi(Games.API)) {
                try {
                    Games.signOut(this.mGoogleApiClient);
                } catch (RuntimeException e) {
                    Log.w(TAG, "Caught exception when calling Games.signOut: " + e.getMessage(), e);
                }
                try {
                    Auth.GoogleSignInApi.signOut(this.mGoogleApiClient);
                } catch (RuntimeException e2) {
                    Log.w(TAG, "Caught exception when calling GoogleSignInAPI.signOut: " + e2.getMessage(), e2);
                }
            }
            this.mGoogleApiClient.disconnect();
            this.mGoogleApiClient = null;
        }
    }

    public static void signOut(Activity activity) {
        TokenFragment tokenFragment = (TokenFragment) activity.getFragmentManager().findFragmentByTag(FRAGMENT_TAG);
        if (tokenFragment != null) {
            tokenFragment.reset();
        }
        synchronized (lock) {
            pendingTokenRequest = null;
        }
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        if (i == 9002) {
            GoogleSignInResult signInResultFromIntent = Auth.GoogleSignInApi.getSignInResultFromIntent(intent);
            if (signInResultFromIntent != null && signInResultFromIntent.isSuccess()) {
                onSignedIn(signInResultFromIntent.getStatus().getStatusCode(), signInResultFromIntent.getSignInAccount());
            } else if (signInResultFromIntent != null) {
                onSignedIn(signInResultFromIntent.getStatus().getStatusCode(), null);
            } else {
                Log.e(TAG, "Google SignIn Result is null?");
                onSignedIn(13, null);
            }
        } else {
            super.onActivityResult(i, i2, intent);
        }
    }

    public void onConnected(@Nullable Bundle bundle) {
        if (this.mGoogleApiClient != null) {
            if (this.mGoogleApiClient.hasConnectedApi(Games.API)) {
                Auth.GoogleSignInApi.silentSignIn(this.mGoogleApiClient).setResultCallback(new ResultCallback<GoogleSignInResult>() {
                    public void onResult(@NonNull GoogleSignInResult googleSignInResult) {
                        if (googleSignInResult.isSuccess()) {
                            TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), googleSignInResult.getSignInAccount());
                            return;
                        }
                        Log.e(TokenFragment.TAG, "Error with silentSignIn: " + googleSignInResult.getStatus());
                        TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), googleSignInResult.getSignInAccount());
                    }
                });
            } else {
                startActivityForResult(Auth.GoogleSignInApi.getSignInIntent(this.mGoogleApiClient), 9002);
            }
        }
    }

    public void onConnectionSuspended(int i) {
        Log.d(TAG, "onConnectionSuspended() called: " + i);
    }

    public void onResume() {
        Log.d(TAG, "onResume called");
        super.onResume();
        if (helperFragment == null) {
            helperFragment = this;
        }
        processRequest();
    }

    public void onStart() {
        Log.d(TAG, "onStart()");
        super.onStart();
        if (this.mGoogleApiClient != null) {
            this.mGoogleApiClient.connect(2);
        }
    }

    public void onStop() {
        Log.d(TAG, "onStop()");
        super.onStop();
        if (this.mGoogleApiClient != null && this.mGoogleApiClient.isConnected()) {
            this.mGoogleApiClient.disconnect();
        }
    }
}
