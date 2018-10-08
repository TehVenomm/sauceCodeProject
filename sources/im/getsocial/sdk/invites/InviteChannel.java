package im.getsocial.sdk.invites;

import im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT;
import im.getsocial.sdk.invites.p092a.p094b.zoToeBNOjF;
import java.util.Comparator;

public class InviteChannel {
    public static final Comparator<InviteChannel> INVITE_CHANNELS_COMPARATOR_BASED_ON_DISPLAY_ORDER = new C10331();
    /* renamed from: a */
    private final String f2258a;
    /* renamed from: b */
    private final LocalizableText f2259b;
    /* renamed from: c */
    private final String f2260c;
    /* renamed from: d */
    private final boolean f2261d;
    /* renamed from: e */
    private final int f2262e;
    /* renamed from: f */
    private final zoToeBNOjF f2263f;
    /* renamed from: g */
    private final pdwpUtZXDT f2264g;

    /* renamed from: im.getsocial.sdk.invites.InviteChannel$1 */
    static final class C10331 implements Comparator<InviteChannel> {
        C10331() {
        }

        public final /* synthetic */ int compare(Object obj, Object obj2) {
            InviteChannel inviteChannel = (InviteChannel) obj;
            InviteChannel inviteChannel2 = (InviteChannel) obj2;
            return inviteChannel.getDisplayOrder() == inviteChannel2.getDisplayOrder() ? inviteChannel.getChannelName().compareToIgnoreCase(inviteChannel2.getChannelName()) : inviteChannel.getDisplayOrder() - inviteChannel2.getDisplayOrder();
        }
    }

    InviteChannel(String str, LocalizableText localizableText, String str2, boolean z, int i, zoToeBNOjF zotoebnojf, pdwpUtZXDT pdwputzxdt) {
        this.f2258a = str;
        this.f2259b = localizableText;
        this.f2260c = str2;
        this.f2261d = z;
        this.f2262e = i;
        this.f2263f = zotoebnojf;
        this.f2264g = pdwputzxdt;
    }

    /* renamed from: a */
    final zoToeBNOjF m2221a() {
        return this.f2263f;
    }

    /* renamed from: b */
    final pdwpUtZXDT m2222b() {
        return this.f2264g;
    }

    public String getChannelId() {
        return this.f2258a;
    }

    public String getChannelName() {
        return this.f2259b.getLocalisedString();
    }

    public int getDisplayOrder() {
        return this.f2262e;
    }

    public String getIconImageUrl() {
        return this.f2260c;
    }

    @Deprecated
    public LocalizableText getName() {
        return this.f2259b;
    }

    public boolean isEnabled() {
        return this.f2261d;
    }
}
