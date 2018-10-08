package im.getsocial.sdk.invites.p092a.p098e;

import android.annotation.TargetApi;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Parcelable;
import android.text.TextUtils;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import im.getsocial.sdk.internal.p089m.fOrCGNYyfk;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelIds;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;
import im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg;
import im.getsocial.sdk.invites.p092a.p094b.zoToeBNOjF;
import im.getsocial.sdk.invites.p092a.p097j.XdbacJlTDQ;
import im.getsocial.sdk.invites.p092a.upgqDBbsrL;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.e.cjrhisSQCL */
public class cjrhisSQCL extends InviteChannelPlugin {

    /* renamed from: im.getsocial.sdk.invites.a.e.cjrhisSQCL$upgqDBbsrL */
    private interface upgqDBbsrL {
        /* renamed from: a */
        void mo4568a(Intent intent);
    }

    @TargetApi(22)
    /* renamed from: im.getsocial.sdk.invites.a.e.cjrhisSQCL$jjbQypPegg */
    private static class jjbQypPegg extends BroadcastReceiver {
        /* renamed from: a */
        upgqDBbsrL f2384a;

        jjbQypPegg(upgqDBbsrL upgqdbbsrl) {
            this.f2384a = upgqdbbsrl;
        }

        public void onReceive(Context context, Intent intent) {
            ComponentName componentName = (ComponentName) intent.getExtras().getParcelable("android.intent.extra.CHOSEN_COMPONENT");
            Map hashMap = new HashMap();
            hashMap.put("sub_provider", componentName.flattenToShortString());
            this.f2384a.mo4562a(hashMap);
            context.unregisterReceiver(this);
        }
    }

    /* renamed from: a */
    private void m2335a(Context context, zoToeBNOjF zotoebnojf, InvitePackage invitePackage, final upgqDBbsrL upgqdbbsrl) {
        boolean z = false;
        String a = zotoebnojf.m2289a();
        String d = zotoebnojf.m2292d();
        Object e = zotoebnojf.m2293e();
        Object c = zotoebnojf.m2291c();
        Object f = zotoebnojf.m2294f();
        final Intent intent = a == null ? new Intent() : new Intent(a);
        intent.addFlags(335544320);
        if (!(TextUtils.isEmpty(d) || d.equals(InviteChannelIds.NATIVE_SHARE))) {
            intent.setPackage(d);
        }
        if (!(TextUtils.isEmpty(d) || TextUtils.isEmpty(e))) {
            intent.setClassName(d, e);
        }
        if (!TextUtils.isEmpty(c)) {
            intent.setType(c);
        }
        if (zotoebnojf.m2290b().contains(jjbQypPegg.EXTRA_SUBJECT)) {
            intent.putExtra("android.intent.extra.SUBJECT", invitePackage.getSubject());
        }
        if (zotoebnojf.m2290b().contains(jjbQypPegg.EXTRA_TEXT)) {
            intent.putExtra("android.intent.extra.TEXT", invitePackage.getText());
        }
        if (!TextUtils.isEmpty(f)) {
            intent.setData(Uri.parse(XdbacJlTDQ.m2382a(f.replace("[APP_INVITE_SUBJECT]", EmkjBpiUfq.m2098a(invitePackage.getSubject())).replace("[APP_INVITE_TEXT]", EmkjBpiUfq.m2098a(invitePackage.getText())), d, invitePackage.getUserName(), invitePackage.getReferralUrl(), true)));
        }
        if (zotoebnojf.m2290b().contains(jjbQypPegg.EXTRA_STREAM)) {
            boolean contains = zotoebnojf.m2290b().contains(jjbQypPegg.EXTRA_VIDEO);
            boolean contains2 = zotoebnojf.m2290b().contains(jjbQypPegg.EXTRA_GIF);
            boolean z2 = invitePackage.getVideoUrl() != null;
            boolean z3 = invitePackage.getGifUrl() != null;
            if (invitePackage.getImage() != null) {
                z = true;
            }
            EmkjBpiUfq.upgqDBbsrL c10453 = new EmkjBpiUfq.upgqDBbsrL(this) {
                /* renamed from: c */
                final /* synthetic */ cjrhisSQCL f2383c;

                /* renamed from: a */
                public final void mo4569a(String str) {
                    Parcelable b = EmkjBpiUfq.m2103b(str);
                    if (b != null) {
                        intent.putExtra("android.intent.extra.STREAM", b);
                    }
                    upgqdbbsrl.mo4568a(intent);
                }
            };
            if (z2 && contains) {
                intent.setType("video/mp4");
                EmkjBpiUfq.m2099a(context, invitePackage.getVideoUrl(), c10453);
                return;
            } else if (z3 && contains2) {
                intent.setType("image/gif");
                EmkjBpiUfq.m2099a(context, invitePackage.getGifUrl(), c10453);
                return;
            } else if (z) {
                intent.setType("image/jpg");
                c10453.mo4569a(EmkjBpiUfq.m2097a(context, invitePackage.getImage()));
                return;
            } else {
                upgqdbbsrl.mo4568a(intent);
                return;
            }
        }
        upgqdbbsrl.mo4568a(intent);
    }

    public boolean isAvailableForDevice(InviteChannel inviteChannel) {
        String d = im.getsocial.sdk.invites.upgqDBbsrL.m2409a(inviteChannel).m2292d();
        return TextUtils.isEmpty(d) ? false : d.equals(InviteChannelIds.NATIVE_SHARE) ? true : fOrCGNYyfk.m2114a(getContext(), d) && fOrCGNYyfk.m2115b(getContext(), d);
    }

    public void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, final InviteCallback inviteCallback) {
        if ((inviteCallback instanceof upgqDBbsrL) && inviteChannel.getChannelId().equalsIgnoreCase(InviteChannelIds.NATIVE_SHARE)) {
            final upgqDBbsrL upgqdbbsrl = (upgqDBbsrL) inviteCallback;
            try {
                m2335a(getContext(), im.getsocial.sdk.invites.upgqDBbsrL.m2409a(inviteChannel), invitePackage, new upgqDBbsrL(this) {
                    /* renamed from: b */
                    final /* synthetic */ cjrhisSQCL f2380b;

                    /* renamed from: a */
                    public final void mo4568a(Intent intent) {
                        try {
                            if (VERSION.SDK_INT >= 22) {
                                Intent createChooser = Intent.createChooser(intent, null, PendingIntent.getBroadcast(this.f2380b.getContext(), 0, intent, 134217728).getIntentSender());
                                IntentFilter intentFilter = new IntentFilter(intent.getAction(), intent.getType());
                                createChooser.addFlags(335544320);
                                this.f2380b.getContext().registerReceiver(new jjbQypPegg(upgqdbbsrl), intentFilter);
                                this.f2380b.getContext().startActivity(createChooser);
                                return;
                            }
                            this.f2380b.getContext().startActivity(intent);
                            upgqdbbsrl.onComplete();
                        } catch (Throwable e) {
                            upgqdbbsrl.onError(e);
                        }
                    }
                });
                return;
            } catch (Throwable e) {
                upgqdbbsrl.onError(e);
                return;
            }
        }
        try {
            m2335a(getContext(), im.getsocial.sdk.invites.upgqDBbsrL.m2409a(inviteChannel), invitePackage, new upgqDBbsrL(this) {
                /* renamed from: b */
                final /* synthetic */ cjrhisSQCL f2378b;

                /* renamed from: a */
                public final void mo4568a(Intent intent) {
                    try {
                        this.f2378b.getContext().startActivity(intent);
                        inviteCallback.onComplete();
                    } catch (Throwable e) {
                        inviteCallback.onError(e);
                    }
                }
            });
        } catch (Throwable e2) {
            inviteCallback.onError(e2);
        }
    }
}
