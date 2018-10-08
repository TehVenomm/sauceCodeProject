package im.getsocial.sdk.invites.p092a.p098e;

import android.content.Intent;
import android.net.Uri;
import android.os.Parcelable;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq.upgqDBbsrL;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;
import jp.colopl.util.MailTo;

/* renamed from: im.getsocial.sdk.invites.a.e.jjbQypPegg */
public class jjbQypPegg extends InviteChannelPlugin {
    /* renamed from: a */
    private void m2337a(Intent intent, InviteChannel inviteChannel, InviteCallback inviteCallback) {
        try {
            Intent createChooser = Intent.createChooser(intent, inviteChannel.getChannelName());
            createChooser.setFlags(DriveFile.MODE_READ_ONLY);
            getContext().startActivity(createChooser);
            inviteCallback.onComplete();
        } catch (Throwable e) {
            inviteCallback.onError(e);
        }
    }

    public boolean isAvailableForDevice(InviteChannel inviteChannel) {
        return true;
    }

    public void presentChannelInterface(final InviteChannel inviteChannel, InvitePackage invitePackage, final InviteCallback inviteCallback) {
        Object obj = null;
        final Intent intent = new Intent("android.intent.action.SENDTO", Uri.parse(MailTo.MAILTO_SCHEME));
        intent.putExtra("android.intent.extra.SUBJECT", invitePackage.getSubject());
        intent.putExtra("android.intent.extra.TEXT", invitePackage.getText());
        upgqDBbsrL c10461 = new upgqDBbsrL(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2388d;

            /* renamed from: a */
            public final void mo4569a(String str) {
                Parcelable b = EmkjBpiUfq.m2103b(str);
                if (b != null) {
                    intent.putExtra("android.intent.extra.STREAM", b);
                }
                this.f2388d.m2337a(intent, inviteChannel, inviteCallback);
            }
        };
        Object obj2 = invitePackage.getGifUrl() != null ? 1 : null;
        Object obj3 = invitePackage.getVideoUrl() != null ? 1 : null;
        if (invitePackage.getImage() != null) {
            obj = 1;
        }
        if (obj2 != null) {
            EmkjBpiUfq.m2099a(getContext(), invitePackage.getGifUrl(), c10461);
        } else if (obj3 != null) {
            EmkjBpiUfq.m2099a(getContext(), invitePackage.getVideoUrl(), c10461);
        } else if (obj != null) {
            c10461.mo4569a(EmkjBpiUfq.m2097a(getContext(), invitePackage.getImage()));
        } else {
            m2337a(intent, inviteChannel, inviteCallback);
        }
    }
}
