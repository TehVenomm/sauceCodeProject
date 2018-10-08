package im.getsocial.sdk.socialgraph.p109a.p111b;

import im.getsocial.sdk.internal.p070f.p071a.BpPZzHFMaU;
import im.getsocial.sdk.internal.p070f.p071a.YgeTlQwUNa;
import im.getsocial.sdk.internal.p089m.qdyNCsqjKt;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.socialgraph.SuggestedFriend.Builder;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.pdwpUtZXDT;

/* renamed from: im.getsocial.sdk.socialgraph.a.b.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static SuggestedFriend m2486a(BpPZzHFMaU bpPZzHFMaU) {
        YgeTlQwUNa ygeTlQwUNa = bpPZzHFMaU.f1566a == null ? new YgeTlQwUNa() : bpPZzHFMaU.f1566a;
        PublicUser build = new Builder(ygeTlQwUNa.f1657a).setAvatarUrl(ygeTlQwUNa.f1659c).setDisplayName(ygeTlQwUNa.f1658b).setIdentities(im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3709a(ygeTlQwUNa.f1660d)).setPublicProperties(ygeTlQwUNa.f1661e).setMutualFriendsCount(qdyNCsqjKt.m2121a(bpPZzHFMaU.f1567b)).build();
        pdwpUtZXDT.m3728a(build, ygeTlQwUNa.f1662f);
        pdwpUtZXDT.m3729a(build, false);
        return build;
    }
}
