package com.google.games.bridge;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentTransaction;
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
    private GoogleApiClient mGoogleApiClient;

    /* renamed from: com.google.games.bridge.TokenFragment$1 */
    class C06551 implements ResultCallback<GoogleSignInResult> {
        C06551() {
        }

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
    }

    /* renamed from: com.google.games.bridge.TokenFragment$2 */
    class C06562 implements ResultCallback<GoogleSignInResult> {
        C06562() {
        }

        public void onResult(@NonNull GoogleSignInResult googleSignInResult) {
            if (googleSignInResult.isSuccess()) {
                TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), googleSignInResult.getSignInAccount());
                return;
            }
            Log.e(TokenFragment.TAG, "Error with silentSignIn: " + googleSignInResult.getStatus());
            TokenFragment.this.onSignedIn(googleSignInResult.getStatus().getStatusCode(), googleSignInResult.getSignInAccount());
        }
    }

    private static class TokenRequest {
        private String accountName;
        private boolean doAuthCode;
        private boolean doEmail;
        private boolean doIdToken;
        private boolean forceRefresh;
        private boolean hidePopups;
        private TokenPendingResult pendingResponse = new TokenPendingResult();
        private String[] scopes;
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
            if (tokenRequest.getWebClientId().isEmpty()) {
                Log.e(TAG, "Web client ID is needed for Auth Code");
                tokenRequest.setResult(10);
                synchronized (lock) {
                    pendingTokenRequest = null;
                }
                return;
            }
            builder.requestServerAuthCode(tokenRequest.getWebClientId(), tokenRequest.getForceRefresh());
        }
        if (tokenRequest.doEmail) {
            builder.requestEmail();
        }
        if (tokenRequest.doIdToken) {
            if (tokenRequest.getWebClientId().isEmpty()) {
                Log.e(TAG, "Web client ID is needed for ID Token");
                tokenRequest.setResult(10);
                synchronized (lock) {
                    pendingTokenRequest = null;
                }
                return;
            }
            builder.requestIdToken(tokenRequest.getWebClientId());
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
        if (!(tokenRequest.accountName == null || tokenRequest.accountName.isEmpty())) {
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

    public static PendingResult fetchToken(Activity activity, boolean z, boolean z2, boolean z3, String str, boolean z4, String[] strArr, boolean z5, String str2) {
        Throwable th;
        TokenRequest tokenRequest = new TokenRequest(z, z2, z3, str, z4, strArr, z5, str2);
        Object obj = null;
        synchronized (lock) {
            if (pendingTokenRequest == null) {
                pendingTokenRequest = tokenRequest;
                obj = 1;
            }
        }
        if (obj == null) {
            Log.e(TAG, "Already a pending token request (requested == ): " + tokenRequest);
            Log.e(TAG, "Already a pending token request: " + pendingTokenRequest);
            tokenRequest.setResult(10);
            return tokenRequest.getPendingResponse();
        }
        TokenFragment tokenFragment = (TokenFragment) activity.getFragmentManager().findFragmentByTag(FRAGMENT_TAG);
        if (tokenFragment == null) {
            try {
                Log.d(TAG, "Creating fragment");
                Fragment tokenFragment2 = new TokenFragment();
                try {
                    FragmentTransaction beginTransaction = activity.getFragmentManager().beginTransaction();
                    beginTransaction.add(tokenFragment2, FRAGMENT_TAG);
                    beginTransaction.commit();
                } catch (Throwable th2) {
                    th = th2;
                    Log.e(TAG, "Cannot launch token fragment:" + th.getMessage(), th);
                    tokenRequest.setResult(13);
                    synchronized (lock) {
                        pendingTokenRequest = null;
                    }
                    return tokenRequest.getPendingResponse();
                }
            } catch (Throwable th3) {
                th = th3;
                Log.e(TAG, "Cannot launch token fragment:" + th.getMessage(), th);
                tokenRequest.setResult(13);
                synchronized (lock) {
                    pendingTokenRequest = null;
                }
                return tokenRequest.getPendingResponse();
            }
        }
        Log.d(TAG, "Fragment exists.. calling processRequests");
        tokenFragment.processRequest();
        return tokenRequest.getPendingResponse();
    }

    private void onSignedIn(int i, GoogleSignInAccount googleSignInAccount) {
        synchronized (lock) {
            TokenRequest tokenRequest = pendingTokenRequest;
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
        synchronized (lock) {
            TokenRequest tokenRequest = pendingTokenRequest;
        }
        if (tokenRequest != null) {
            buildClient(tokenRequest);
            synchronized (lock) {
                tokenRequest = pendingTokenRequest;
            }
            if (tokenRequest != null) {
                if (this.mGoogleApiClient.hasConnectedApi(Games.API)) {
                    Auth.GoogleSignInApi.silentSignIn(this.mGoogleApiClient).setResultCallback(new C06551());
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
                } catch (Throwable e) {
                    Log.w(TAG, "Caught exception when calling Games.signOut: " + e.getMessage(), e);
                }
                try {
                    Auth.GoogleSignInApi.signOut(this.mGoogleApiClient);
                } catch (Throwable e2) {
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
                return;
            } else if (signInResultFromIntent != null) {
                onSignedIn(signInResultFromIntent.getStatus().getStatusCode(), null);
                return;
            } else {
                Log.e(TAG, "Google SignIn Result is null?");
                onSignedIn(13, null);
                return;
            }
        }
        super.onActivityResult(i, i2, intent);
    }

    public void onConnected(@Nullable Bundle bundle) {
        if (this.mGoogleApiClient != null) {
            if (this.mGoogleApiClient.hasConnectedApi(Games.API)) {
                Auth.GoogleSignInApi.silentSignIn(this.mGoogleApiClient).setResultCallback(new C06562());
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
