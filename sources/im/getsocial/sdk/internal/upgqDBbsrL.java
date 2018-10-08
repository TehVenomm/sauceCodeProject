package im.getsocial.sdk.internal;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.activities.p028a.p035d.HptYHntaqF;
import im.getsocial.sdk.activities.p028a.p035d.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.KCGqEGAizh;
import im.getsocial.sdk.internal.p033c.SKUqohGtGQ;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p059j.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.wWemqSpYTx;
import im.getsocial.sdk.internal.p036a.p044g.cjrhisSQCL;
import im.getsocial.sdk.invites.FetchReferralDataCallback;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.invites.p092a.p097j.zoToeBNOjF;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.socialgraph.p109a.p112c.fOrCGNYyfk;
import im.getsocial.sdk.socialgraph.p109a.p112c.qZypgoeblR;
import im.getsocial.sdk.usermanagement.AddAuthIdentityCallback;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

public class upgqDBbsrL {
    /* renamed from: n */
    private static final long f2242n = TimeUnit.SECONDS.convert(1, TimeUnit.MINUTES);
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2243a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.internal.p082i.p084b.jjbQypPegg f2244b;
    @XdbacJlTDQ
    /* renamed from: c */
    zoToeBNOjF f2245c;
    @XdbacJlTDQ
    /* renamed from: d */
    im.getsocial.sdk.pushnotifications.p067a.p105d.upgqDBbsrL f2246d;
    @XdbacJlTDQ
    /* renamed from: e */
    bpiSwUyLit f2247e;
    @XdbacJlTDQ
    /* renamed from: f */
    SKUqohGtGQ f2248f;
    @XdbacJlTDQ
    /* renamed from: g */
    pdwpUtZXDT f2249g;
    @XdbacJlTDQ
    /* renamed from: h */
    im.getsocial.sdk.internal.p036a.p037a.jjbQypPegg f2250h;
    @XdbacJlTDQ
    /* renamed from: i */
    im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg f2251i;
    @XdbacJlTDQ
    /* renamed from: j */
    wWemqSpYTx f2252j;
    @XdbacJlTDQ
    /* renamed from: k */
    cjrhisSQCL f2253k;
    @XdbacJlTDQ
    /* renamed from: l */
    im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f2254l;
    @XdbacJlTDQ
    /* renamed from: m */
    KCGqEGAizh f2255m;

    /* renamed from: im.getsocial.sdk.internal.upgqDBbsrL$1 */
    class C10291 implements KCGqEGAizh.jjbQypPegg {
        /* renamed from: a */
        final /* synthetic */ String f2234a;
        /* renamed from: b */
        final /* synthetic */ upgqDBbsrL f2235b;

        C10291(upgqDBbsrL upgqdbbsrl, String str) {
            this.f2235b = upgqdbbsrl;
            this.f2234a = str;
        }

        /* renamed from: a */
        public final void mo4561a(CompletionCallback completionCallback) {
            new im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL().m1986a(this.f2234a, new CompletionCallback(this.f2235b, new CompletionCallback(this.f2235b, completionCallback) {
                /* renamed from: b */
                final /* synthetic */ upgqDBbsrL f2237b;

                public void onFailure(GetSocialException getSocialException) {
                    r2.onFailure(getSocialException);
                }

                public void onSuccess() {
                    upgqDBbsrL.m2159a(this.f2237b);
                    r2.onSuccess();
                }
            }) {
                /* renamed from: b */
                final /* synthetic */ upgqDBbsrL f2239b;

                public void onFailure(GetSocialException getSocialException) {
                    r2.onFailure(getSocialException);
                }

                public void onSuccess() {
                    Runnable b = this.f2239b.f2243a.m1311b();
                    if (b != null) {
                        b.run();
                        this.f2239b.f2243a.m1310a(null);
                    }
                    r2.onSuccess();
                }
            });
        }
    }

    public upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private void m2158a(long j, long j2) {
        Map hashMap = new HashMap();
        hashMap.put("duration", String.valueOf(j2 - j));
        this.f2254l.m1052a(im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg.m1015a("app_session_end", hashMap, j, UUID.randomUUID().toString()));
    }

    /* renamed from: a */
    static /* synthetic */ void m2159a(upgqDBbsrL upgqdbbsrl) {
        im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ b = upgqdbbsrl.f2246d.m2439b();
        if (b != null) {
            upgqdbbsrl.m2175a(b);
        }
    }

    /* renamed from: a */
    static void m2160a(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Can not set null or empty language");
        new im.getsocial.sdk.internal.p082i.p085c.jjbQypPegg().m1993a(str);
    }

    /* renamed from: a */
    static void m2161a(String str, im.getsocial.sdk.invites.p092a.pdwpUtZXDT pdwputzxdt) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Can not call registerInviteChannelPlugin with null channelId");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) pdwputzxdt), "Can not call registerInviteChannelPlugin with null sharedInviteProviderPlugin");
        new im.getsocial.sdk.invites.p092a.p102i.cjrhisSQCL().m2369a(str, pdwputzxdt);
    }

    /* renamed from: f */
    private void m2163f(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(m2192a(), "GetSocial SDK should be initialized before calling " + str);
    }

    /* renamed from: g */
    private void m2164g(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.upgqDBbsrL.m1513a(this.f2251i.mo4401a(), "Can not call " + str + " when internet connection is off");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(m2192a(), "GetSocial SDK should be initialized before calling " + str);
    }

    /* renamed from: l */
    private PrivateUser m2165l() {
        return ((im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg) this.f2249g.m1205a(im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg.class)).m3698b();
    }

    /* renamed from: a */
    final void m2166a(int i, int i2, Callback<List<PublicUser>> callback) {
        boolean z = true;
        m2164g("getFriends");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(i2 > 0, "Limit can not be less ot equal zero");
        if (i < 0) {
            z = false;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Offset can not be less than zero");
        new im.getsocial.sdk.socialgraph.p109a.p112c.XdbacJlTDQ().m2489a(i, i2, callback);
    }

    /* renamed from: a */
    final void m2167a(Callback<List<ReferredUser>> callback) {
        m2163f("getReferredUsers");
        new im.getsocial.sdk.invites.p092a.p102i.upgqDBbsrL().m2380a(callback);
    }

    /* renamed from: a */
    final void m2168a(CompletionCallback completionCallback) {
        m2164g("resetUser");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        m2158a(this.f2247e.mo4363c("application_did_become_active_event_timestamp"), this.f2253k.mo4353b());
        new im.getsocial.sdk.internal.p078h.p081c.jjbQypPegg().m1967a(completionCallback);
        this.f2247e.mo4359a("application_did_become_active_event_timestamp", this.f2253k.mo4353b());
        this.f2254l.m1053a("app_session_start", null);
    }

    /* renamed from: a */
    final void m2169a(ActivitiesQuery activitiesQuery, Callback<List<ActivityPost>> callback) {
        m2164g("getActivities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) activitiesQuery), "Query can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.activities.jjbQypPegg.m998a(activitiesQuery).m1008j();
        new im.getsocial.sdk.activities.p028a.p035d.cjrhisSQCL().m992a(activitiesQuery, callback);
    }

    /* renamed from: a */
    final void m2170a(TagsQuery tagsQuery, Callback<List<String>> callback) {
        m2164g("findTags");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) tagsQuery), "Tags query can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.activities.p028a.p035d.upgqDBbsrL().m995a(tagsQuery, callback);
    }

    /* renamed from: a */
    final void m2171a(im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg, Callback<ActivityPost> callback) {
        m2164g("postActivity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new KSZKMmRWhZ().m990a(jjbqyppegg, callback);
    }

    /* renamed from: a */
    final void m2172a(FetchReferralDataCallback fetchReferralDataCallback) {
        m2163f("getReferralData");
        this.f2245c.m2398a(fetchReferralDataCallback);
    }

    /* renamed from: a */
    final void m2173a(NotificationsCountQuery notificationsCountQuery, Callback<Integer> callback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) notificationsCountQuery), "Query can not be null");
        m2164g("getNotificationsCount");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.jjbQypPegg().m2456a(notificationsCountQuery, callback);
    }

    /* renamed from: a */
    final void m2174a(NotificationsQuery notificationsQuery, Callback<List<Notification>> callback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) notificationsQuery), "Query can not be null");
        m2164g("getNotifications");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.upgqDBbsrL().m2459a(notificationsQuery, callback);
    }

    /* renamed from: a */
    final void m2175a(im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ xdbacJlTDQ) {
        if (m2192a()) {
            new im.getsocial.sdk.pushnotifications.p067a.p108g.pdwpUtZXDT().m2458a(xdbacJlTDQ);
        } else {
            this.f2246d.m2437a(xdbacJlTDQ);
        }
    }

    /* renamed from: a */
    final void m2176a(AuthIdentity authIdentity, CompletionCallback completionCallback) {
        m2164g("switchUser");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) authIdentity), "AuthIdentity can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.ztWNWCuZiM().m3727a(authIdentity, completionCallback);
    }

    /* renamed from: a */
    final void m2177a(AuthIdentity authIdentity, AddAuthIdentityCallback addAuthIdentityCallback) {
        m2164g("addAuthIdentity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) authIdentity), "AuthIdentity can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.jjbQypPegg().m3718a(authIdentity, addAuthIdentityCallback);
    }

    /* renamed from: a */
    final void m2178a(UsersQuery usersQuery, Callback<List<UserReference>> callback) {
        m2164g("findUsers");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) usersQuery), "Query can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.pdwpUtZXDT().m3719a(usersQuery, callback);
    }

    /* renamed from: a */
    final void m2179a(im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT pdwputzxdt, CompletionCallback completionCallback) {
        m2164g("setUserDetails");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) pdwputzxdt), "UserUpdate can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.zoToeBNOjF().m3722a(pdwputzxdt, completionCallback);
    }

    /* renamed from: a */
    final void m2180a(String str, int i, int i2, Callback<List<PublicUser>> callback) {
        m2164g("getActivityLikers");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Activity ID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.activities.p028a.p035d.pdwpUtZXDT().m994a(str, i, i2, callback);
    }

    /* renamed from: a */
    final void m2181a(String str, Callback<PublicUser> callback) {
        m2164g("getUserById");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "UserId can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.upgqDBbsrL().m3720a(str, callback);
    }

    /* renamed from: a */
    final void m2182a(String str, CompletionCallback completionCallback) {
        m2164g("removeAuthIdentity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "ProviderId can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.XdbacJlTDQ().m3712a(str, completionCallback);
    }

    /* renamed from: a */
    final void m2183a(String str, ReportingReason reportingReason, CompletionCallback completionCallback) {
        m2164g("reportActivity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Activity ID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) reportingReason), "ReportingReason can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new HptYHntaqF().m989a(str, reportingReason, completionCallback);
    }

    /* renamed from: a */
    final void m2184a(String str, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT pdwputzxdt, LinkParams linkParams, InviteCallback inviteCallback) {
        if (linkParams != null) {
            String validateLinkParams = LinkParams.validateLinkParams(linkParams);
            if (validateLinkParams != null) {
                throw new IllegalArgumentException("Invalid key in LinkParams: '" + validateLinkParams + "'. '$' prefix is reserved for GetSocial SDK.");
            }
        }
        m2164g("sendInvite");
        new im.getsocial.sdk.invites.p092a.p102i.pdwpUtZXDT().m2379a(str, pdwputzxdt, linkParams, inviteCallback);
    }

    /* renamed from: a */
    final void m2185a(String str, String str2, final Callback<PublicUser> callback) {
        m2164g("getUserByAuthIdentity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str2), "User id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.cjrhisSQCL().m3713a(str, Arrays.asList(new String[]{str2}), callback == null ? null : new Callback<Map<String, PublicUser>>(this) {
            /* renamed from: b */
            final /* synthetic */ upgqDBbsrL f2241b;

            public void onFailure(GetSocialException getSocialException) {
                callback.onFailure(getSocialException);
            }

            public /* synthetic */ void onSuccess(Object obj) {
                Map map = (Map) obj;
                int size = map.size();
                if (size == 1) {
                    callback.onSuccess(map.values().iterator().next());
                } else {
                    callback.onFailure(size == 0 ? new GetSocialException(ErrorCode.ILLEGAL_ARGUMENT, "No GetSocial User found for provided arguments") : new GetSocialException(ErrorCode.ILLEGAL_STATE, "API returned unexpected amount of responses"));
                }
            }
        });
    }

    /* renamed from: a */
    final void m2186a(String str, List<String> list, Callback<Map<String, PublicUser>> callback) {
        m2164g("getUsersByAuthIdentities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "List of user ids can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.usermanagement.p138a.p143e.cjrhisSQCL().m3713a(str, list, callback);
    }

    /* renamed from: a */
    final void m2187a(String str, List<String> list, CompletionCallback completionCallback) {
        m2164g("setFriendsByAuthIdentities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "User ids list can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new qZypgoeblR().m2498a(str, list, completionCallback);
    }

    /* renamed from: a */
    final void m2188a(String str, boolean z, Callback<ActivityPost> callback) {
        m2164g("likeActivity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Activity ID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.activities.p028a.p035d.ztWNWCuZiM().m997a(str, z, callback);
    }

    /* renamed from: a */
    final void m2189a(List<String> list, CompletionCallback completionCallback) {
        m2164g("setFriends");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "User ids list can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new fOrCGNYyfk().m2493a(list, completionCallback);
    }

    /* renamed from: a */
    final void m2190a(List<String> list, boolean z, CompletionCallback completionCallback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "Notifications list can not be null");
        m2164g("setNotificationsRead");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.ztWNWCuZiM().m2462a(list, z, completionCallback);
    }

    /* renamed from: a */
    final void m2191a(boolean z, CompletionCallback completionCallback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        m2164g("setPushNotificationsEnabled");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.KSZKMmRWhZ().m2453a(z, completionCallback);
    }

    /* renamed from: a */
    final boolean m2192a() {
        return this.f2252j.mo4553a();
    }

    /* renamed from: b */
    final String m2193b(String str) {
        m2163f("getPrivateProperty");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key can not be null");
        return m2165l().getPrivateProperty(str);
    }

    /* renamed from: b */
    final List<InviteChannel> m2194b() {
        m2163f("getInviteChannels");
        return new im.getsocial.sdk.invites.p092a.p102i.jjbQypPegg().m2370a();
    }

    /* renamed from: b */
    final void m2195b(int i, int i2, Callback<List<SuggestedFriend>> callback) {
        boolean z = true;
        m2164g("getSuggestedFriends");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(i2 > 0, "Limit can not be less ot equal zero");
        if (i < 0) {
            z = false;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Offset can not be less than zero");
        new im.getsocial.sdk.socialgraph.p109a.p112c.zoToeBNOjF().m2500a(i, i2, callback);
    }

    /* renamed from: b */
    final void m2196b(Callback<Integer> callback) {
        m2164g("getFriendsCount");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.cjrhisSQCL().m2490a(callback);
    }

    /* renamed from: b */
    final void m2197b(String str, Callback<Integer> callback) {
        m2164g("addFriend");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "UserID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.jjbQypPegg().m2494a(str, callback);
    }

    /* renamed from: b */
    final void m2198b(String str, CompletionCallback completionCallback) {
        m2164g("deleteActivity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Activity ID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Callback can not be null");
        new im.getsocial.sdk.activities.p028a.p035d.jjbQypPegg().m993a(str, completionCallback);
    }

    /* renamed from: b */
    final void m2199b(String str, List<String> list, Callback<Integer> callback) {
        m2164g("addFriendsByAuthIdentities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "User ids list can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.upgqDBbsrL().m2499a(str, list, callback);
    }

    /* renamed from: c */
    final String m2200c() {
        m2163f("getUserId");
        return m2165l().getId();
    }

    /* renamed from: c */
    final String m2201c(String str) {
        m2163f("getPublicProperty");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key can not be null");
        return m2165l().getPublicProperty(str);
    }

    /* renamed from: c */
    final void m2202c(Callback<List<UserReference>> callback) {
        m2164g("getFriendsReferences");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.pdwpUtZXDT().m2495a(callback);
    }

    /* renamed from: c */
    final void m2203c(String str, Callback<Integer> callback) {
        m2164g("removeFriend");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "UserID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.KSZKMmRWhZ().m2488a(str, callback);
    }

    /* renamed from: c */
    final void m2204c(String str, CompletionCallback completionCallback) {
        m2164g("registerOnPushServer");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.zoToeBNOjF().m2461a(str, completionCallback);
    }

    /* renamed from: c */
    final void m2205c(String str, List<String> list, Callback<Integer> callback) {
        m2164g("removeFriendsByAuthIdentities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "User ids list can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.HptYHntaqF().m2487a(str, list, callback);
    }

    /* renamed from: d */
    final String m2206d() {
        m2163f("getDisplayName");
        return m2165l().getDisplayName();
    }

    /* renamed from: d */
    final void m2207d(Callback<Boolean> callback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        m2164g("isPushNotificationsEnabled");
        new im.getsocial.sdk.pushnotifications.p067a.p108g.cjrhisSQCL().m2455a(callback);
    }

    /* renamed from: d */
    final void m2208d(String str, Callback<Boolean> callback) {
        m2164g("isFriend");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "UserID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.socialgraph.p109a.p112c.ztWNWCuZiM().m2501a(str, callback);
    }

    /* renamed from: d */
    final boolean m2209d(String str) {
        m2163f("hasPrivateProperty");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key can not be null");
        return m2165l().hasPrivateProperty(str);
    }

    /* renamed from: e */
    final String m2210e() {
        m2163f("getAvatarUrl");
        return m2165l().getAvatarUrl();
    }

    /* renamed from: e */
    final void m2211e(String str, Callback<List<ActivityPost>> callback) {
        m2164g("getStickyActivities");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Feed can not be null or empty");
        new im.getsocial.sdk.activities.p028a.p035d.zoToeBNOjF().m996a(str, callback);
    }

    /* renamed from: e */
    final boolean m2212e(String str) {
        m2163f("hasPublicProperty");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key can not be null");
        return m2165l().hasPublicProperty(str);
    }

    /* renamed from: f */
    final Map<String, String> m2213f() {
        m2163f("getAllPublicProperties");
        return m2165l().getAllPublicProperties();
    }

    /* renamed from: f */
    final void m2214f(String str, Callback<ActivityPost> callback) {
        m2164g("getActivity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Activity ID can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Callback can not be null");
        new im.getsocial.sdk.activities.p028a.p035d.XdbacJlTDQ().m991a(str, callback);
    }

    /* renamed from: g */
    final Map<String, String> m2215g() {
        m2163f("getAllPrivateProperties");
        return m2165l().getAllPrivateProperties();
    }

    /* renamed from: h */
    final boolean m2216h() {
        m2163f("isAnonymous");
        return m2165l().getAuthIdentities().isEmpty();
    }

    /* renamed from: i */
    final Map<String, String> m2217i() {
        m2163f("getAuthIdentities");
        return m2165l().getAuthIdentities();
    }

    /* renamed from: j */
    final void m2218j() {
        m2164g("registerForPushNotifications");
        im.getsocial.sdk.pushnotifications.p067a.p108g.XdbacJlTDQ xdbacJlTDQ = new im.getsocial.sdk.pushnotifications.p067a.p108g.XdbacJlTDQ();
        im.getsocial.sdk.pushnotifications.p067a.p108g.XdbacJlTDQ.m2454a();
    }

    /* renamed from: k */
    final void m2219k() {
        Object obj = 1;
        long c = this.f2247e.mo4363c("application_did_become_active_event_timestamp");
        long c2 = this.f2247e.mo4363c("application_did_become_inactive_event_timestamp");
        if (c == 0) {
            c2 = 0;
        }
        if ((c2 == 0 ? 1 : null) != null) {
            this.f2254l.m1053a("app_session_start", null);
        } else {
            if (this.f2253k.mo4353b() - c2 <= f2242n) {
                obj = null;
            }
            if (obj != null) {
                m2158a(c, c2);
                this.f2254l.m1053a("app_session_start", null);
                this.f2247e.mo4365e("application_did_become_inactive_event_timestamp");
            }
        }
        this.f2247e.mo4359a("application_did_become_active_event_timestamp", this.f2253k.mo4353b());
    }
}
