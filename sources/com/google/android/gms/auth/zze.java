package com.google.android.gms.auth;

import android.accounts.Account;
import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.internal.zzatr;
import com.google.android.gms.internal.zzbcq;
import com.google.android.gms.internal.zzei;
import java.io.IOException;

final class zze implements zzi<TokenData> {
    private /* synthetic */ Bundle val$options;
    private /* synthetic */ Account zzdxn;
    private /* synthetic */ String zzdxo;

    zze(Account account, String str, Bundle bundle) {
        this.zzdxn = account;
        this.zzdxo = str;
        this.val$options = bundle;
    }

    public final /* synthetic */ Object zzaa(IBinder iBinder) throws RemoteException, IOException, GoogleAuthException {
        Bundle bundle = (Bundle) zzd.zzl(zzei.zza(iBinder).zza(this.zzdxn, this.zzdxo, this.val$options));
        TokenData zzd = TokenData.zzd(bundle, "tokenDetails");
        if (zzd != null) {
            return zzd;
        }
        String string = bundle.getString("Error");
        Intent intent = (Intent) bundle.getParcelable("userRecoveryIntent");
        zzatr zzeu = zzatr.zzeu(string);
        int i = (zzatr.BAD_AUTHENTICATION.equals(zzeu) || zzatr.CAPTCHA.equals(zzeu) || zzatr.NEED_PERMISSION.equals(zzeu) || zzatr.NEED_REMOTE_CONSENT.equals(zzeu) || zzatr.NEEDS_BROWSER.equals(zzeu) || zzatr.USER_CANCEL.equals(zzeu) || zzatr.DEVICE_MANAGEMENT_REQUIRED.equals(zzeu) || zzatr.DM_INTERNAL_ERROR.equals(zzeu) || zzatr.DM_SYNC_DISABLED.equals(zzeu) || zzatr.DM_ADMIN_BLOCKED.equals(zzeu) || zzatr.DM_ADMIN_PENDING_APPROVAL.equals(zzeu) || zzatr.DM_STALE_SYNC_REQUIRED.equals(zzeu) || zzatr.DM_DEACTIVATED.equals(zzeu) || zzatr.DM_REQUIRED.equals(zzeu) || zzatr.THIRD_PARTY_DEVICE_MANAGEMENT_REQUIRED.equals(zzeu) || zzatr.DM_SCREENLOCK_REQUIRED.equals(zzeu)) ? 1 : 0;
        if (i != 0) {
            zzbcq zzzt = zzd.zzdxm;
            String valueOf = String.valueOf(zzeu);
            zzzt.zzf("GoogleAuthUtil", new StringBuilder(String.valueOf(valueOf).length() + 31).append("isUserRecoverableError status: ").append(valueOf).toString());
            throw new UserRecoverableAuthException(string, intent);
        }
        int i2 = (zzatr.NETWORK_ERROR.equals(zzeu) || zzatr.SERVICE_UNAVAILABLE.equals(zzeu)) ? 1 : 0;
        if (i2 != 0) {
            throw new IOException(string);
        }
        throw new GoogleAuthException(string);
    }
}
