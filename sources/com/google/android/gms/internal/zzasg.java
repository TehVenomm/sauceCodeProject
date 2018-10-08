package com.google.android.gms.internal;

import android.app.PendingIntent;
import android.content.Intent;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.Auth.AuthCredentialsOptions;
import com.google.android.gms.auth.api.credentials.Credential;
import com.google.android.gms.auth.api.credentials.CredentialRequest;
import com.google.android.gms.auth.api.credentials.CredentialRequestResult;
import com.google.android.gms.auth.api.credentials.CredentialsApi;
import com.google.android.gms.auth.api.credentials.HintRequest;
import com.google.android.gms.auth.api.credentials.PasswordSpecification;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import com.google.android.gms.common.internal.safeparcel.zze;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveFile;

public final class zzasg implements CredentialsApi {
    public final PendingResult<Status> delete(GoogleApiClient googleApiClient, Credential credential) {
        return googleApiClient.zze(new zzask(this, googleApiClient, credential));
    }

    public final PendingResult<Status> disableAutoSignIn(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzasl(this, googleApiClient));
    }

    public final PendingIntent getHintPickerIntent(GoogleApiClient googleApiClient, HintRequest hintRequest) {
        zzbp.zzb(googleApiClient.zza(Auth.CREDENTIALS_API), (Object) "Auth.CREDENTIALS_API must be added to GoogleApiClient to use this API");
        AuthCredentialsOptions zzzz = ((zzaso) googleApiClient.zza(Auth.zzdyd)).zzzz();
        Object context = googleApiClient.getContext();
        zzbp.zzb(context, (Object) "context must not be null");
        zzbp.zzb((Object) hintRequest, (Object) "request must not be null");
        if (zzzz == null) {
            SafeParcelable zzzv = (zzzz != null || zzzz.zzzv() == null) ? PasswordSpecification.zzeay : zzzz.zzzv();
            Intent putExtra = new Intent("com.google.android.gms.auth.api.credentials.PICKER").putExtra("com.google.android.gms.credentials.hintRequestVersion", 2).putExtra("com.google.android.gms.credentials.RequestType", "Hints").putExtra("com.google.android.gms.credentials.ClaimedCallingPackage", null);
            zze.zza(zzzv, putExtra, "com.google.android.gms.credentials.PasswordSpecification");
            zze.zza((SafeParcelable) hintRequest, putExtra, "com.google.android.gms.credentials.HintRequest");
            return PendingIntent.getActivity(context, 2000, putExtra, DriveFile.MODE_READ_ONLY);
        }
        if (zzzz != null) {
        }
        Intent putExtra2 = new Intent("com.google.android.gms.auth.api.credentials.PICKER").putExtra("com.google.android.gms.credentials.hintRequestVersion", 2).putExtra("com.google.android.gms.credentials.RequestType", "Hints").putExtra("com.google.android.gms.credentials.ClaimedCallingPackage", null);
        zze.zza(zzzv, putExtra2, "com.google.android.gms.credentials.PasswordSpecification");
        zze.zza((SafeParcelable) hintRequest, putExtra2, "com.google.android.gms.credentials.HintRequest");
        return PendingIntent.getActivity(context, 2000, putExtra2, DriveFile.MODE_READ_ONLY);
    }

    public final PendingResult<CredentialRequestResult> request(GoogleApiClient googleApiClient, CredentialRequest credentialRequest) {
        return googleApiClient.zzd(new zzash(this, googleApiClient, credentialRequest));
    }

    public final PendingResult<Status> save(GoogleApiClient googleApiClient, Credential credential) {
        return googleApiClient.zze(new zzasj(this, googleApiClient, credential));
    }
}
