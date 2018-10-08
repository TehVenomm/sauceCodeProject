package com.google.android.gms.auth.api.signin.internal;

import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.LoaderManager.LoaderCallbacks;
import android.support.v4.content.Loader;
import android.util.Log;
import android.view.accessibility.AccessibilityEvent;
import com.google.android.gms.auth.api.signin.GoogleSignInApi;
import com.google.android.gms.auth.api.signin.SignInAccount;
import com.google.android.gms.common.annotation.KeepName;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Status;

@KeepName
public class SignInHubActivity extends FragmentActivity {
    private zzy zzedf;
    private boolean zzedg = false;
    private SignInConfiguration zzedh;
    private boolean zzedi;
    private int zzedj;
    private Intent zzedk;

    final class zza implements LoaderCallbacks<Void> {
        private /* synthetic */ SignInHubActivity zzedl;

        private zza(SignInHubActivity signInHubActivity) {
            this.zzedl = signInHubActivity;
        }

        public final Loader<Void> onCreateLoader(int i, Bundle bundle) {
            return new zzb(this.zzedl, GoogleApiClient.zzafn());
        }

        public final /* synthetic */ void onLoadFinished(Loader loader, Object obj) {
            this.zzedl.setResult(this.zzedl.zzedj, this.zzedl.zzedk);
            this.zzedl.finish();
        }

        public final void onLoaderReset(Loader<Void> loader) {
        }
    }

    private final void zzaaq() {
        getSupportLoaderManager().initLoader(0, null, new zza());
    }

    private final void zzay(int i) {
        Parcelable status = new Status(i);
        Intent intent = new Intent();
        intent.putExtra("googleSignInStatus", status);
        setResult(0, intent);
        finish();
    }

    public boolean dispatchPopulateAccessibilityEvent(AccessibilityEvent accessibilityEvent) {
        return true;
    }

    protected void onActivityResult(int i, int i2, Intent intent) {
        if (!this.zzedg) {
            setResult(0);
            switch (i) {
                case 40962:
                    if (intent != null) {
                        SignInAccount signInAccount = (SignInAccount) intent.getParcelableExtra(GoogleSignInApi.EXTRA_SIGN_IN_ACCOUNT);
                        if (signInAccount != null && signInAccount.zzaah() != null) {
                            Parcelable zzaah = signInAccount.zzaah();
                            this.zzedf.zza(zzaah, this.zzedh.zzaap());
                            intent.removeExtra(GoogleSignInApi.EXTRA_SIGN_IN_ACCOUNT);
                            intent.putExtra("googleSignInAccount", zzaah);
                            this.zzedi = true;
                            this.zzedj = i2;
                            this.zzedk = intent;
                            zzaaq();
                            return;
                        } else if (intent.hasExtra("errorCode")) {
                            zzay(intent.getIntExtra("errorCode", 8));
                            return;
                        }
                    }
                    zzay(8);
                    return;
                default:
                    return;
            }
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.zzedf = zzy.zzbm(this);
        Intent intent = getIntent();
        if (!"com.google.android.gms.auth.GOOGLE_SIGN_IN".equals(intent.getAction())) {
            String valueOf = String.valueOf(intent.getAction());
            Log.e("AuthSignInClient", valueOf.length() != 0 ? "Unknown action: ".concat(valueOf) : new String("Unknown action: "));
            finish();
        }
        this.zzedh = (SignInConfiguration) intent.getParcelableExtra("config");
        if (this.zzedh == null) {
            Log.e("AuthSignInClient", "Activity started with invalid configuration.");
            setResult(0);
            finish();
        } else if (bundle == null) {
            Intent intent2 = new Intent("com.google.android.gms.auth.GOOGLE_SIGN_IN");
            intent2.setPackage("com.google.android.gms");
            intent2.putExtra("config", this.zzedh);
            try {
                startActivityForResult(intent2, 40962);
            } catch (ActivityNotFoundException e) {
                this.zzedg = true;
                Log.w("AuthSignInClient", "Could not launch sign in Intent. Google Play Service is probably being updated...");
                zzay(17);
            }
        } else {
            this.zzedi = bundle.getBoolean("signingInGoogleApiClients");
            if (this.zzedi) {
                this.zzedj = bundle.getInt("signInResultCode");
                this.zzedk = (Intent) bundle.getParcelable("signInResultData");
                zzaaq();
            }
        }
    }

    protected void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        bundle.putBoolean("signingInGoogleApiClients", this.zzedi);
        if (this.zzedi) {
            bundle.putInt("signInResultCode", this.zzedj);
            bundle.putParcelable("signInResultData", this.zzedk);
        }
    }
}
