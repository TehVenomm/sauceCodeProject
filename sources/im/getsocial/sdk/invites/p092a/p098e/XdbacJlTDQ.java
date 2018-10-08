package im.getsocial.sdk.invites.p092a.p098e;

import android.content.Intent;
import android.net.Uri;
import android.os.Build.VERSION;
import android.provider.Telephony.Sms;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;

/* renamed from: im.getsocial.sdk.invites.a.e.XdbacJlTDQ */
public class XdbacJlTDQ extends InviteChannelPlugin {
    public boolean isAvailableForDevice(InviteChannel inviteChannel) {
        return VERSION.SDK_INT >= 19 ? getContext().getPackageManager().hasSystemFeature("android.hardware.telephony") && Sms.getDefaultSmsPackage(getContext()) != null : getContext().getPackageManager().hasSystemFeature("android.hardware.telephony");
    }

    public void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, InviteCallback inviteCallback) {
        Intent intent = new Intent("android.intent.action.VIEW");
        if (VERSION.SDK_INT >= 19) {
            intent.setData(Uri.parse("sms:"));
        } else {
            intent.setType("vnd.android-dir/mms-sms");
        }
        intent.putExtra("sms_body", invitePackage.getText());
        try {
            intent = Intent.createChooser(intent, inviteChannel.getChannelName());
            intent.setFlags(DriveFile.MODE_READ_ONLY);
            getContext().startActivity(intent);
            inviteCallback.onComplete();
        } catch (Throwable e) {
            inviteCallback.onError(e);
        }
    }
}
