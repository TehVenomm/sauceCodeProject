package im.getsocial.sdk.invites.p092a.p098e;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import com.facebook.messenger.MessengerUtils;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;

/* renamed from: im.getsocial.sdk.invites.a.e.upgqDBbsrL */
public class upgqDBbsrL extends InviteChannelPlugin {
    /* renamed from: a */
    private static boolean m2346a(Context context) {
        try {
            context.getPackageManager().getPackageInfo(MessengerUtils.PACKAGE_NAME, 0);
            return true;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    public boolean isAvailableForDevice(InviteChannel inviteChannel) {
        return upgqDBbsrL.m2346a(getContext());
    }

    public void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, InviteCallback inviteCallback) {
        try {
            Intent intent = new Intent("android.intent.action.SEND");
            intent.setType("text/plain");
            intent.setPackage(MessengerUtils.PACKAGE_NAME);
            intent.putExtra("android.intent.extra.TEXT", invitePackage.getReferralUrl());
            intent = Intent.createChooser(intent, "Share");
            intent.setFlags(DriveFile.MODE_READ_ONLY);
            getContext().startActivity(intent);
            inviteCallback.onComplete();
        } catch (Throwable e) {
            inviteCallback.onError(e);
        }
    }
}
