package im.getsocial.sdk.internal;

import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Handler;
import android.os.Looper;
import com.facebook.internal.AnalyticsEvents;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GlobalErrorListener;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPostContent;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p047b.upgqDBbsrL;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import im.getsocial.sdk.internal.p089m.HptYHntaqF;
import im.getsocial.sdk.internal.p089m.KluUZYuxme;
import im.getsocial.sdk.internal.upgqDBbsrL.C10291;
import im.getsocial.sdk.invites.FetchReferralDataCallback;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelIds;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InviteContent;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.invites.p092a.p097j.zoToeBNOjF;
import im.getsocial.sdk.invites.p092a.p098e.cjrhisSQCL;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationListener;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.usermanagement.AddAuthIdentityCallback;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.OnUserChangedListener;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UserUpdate;
import im.getsocial.sdk.usermanagement.UsersQuery;
import im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.Callable;

public final class jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg f2194a;
    @XdbacJlTDQ
    /* renamed from: b */
    upgqDBbsrL f2195b;
    @XdbacJlTDQ
    /* renamed from: c */
    bpiSwUyLit f2196c;
    @XdbacJlTDQ
    /* renamed from: d */
    im.getsocial.sdk.invites.p092a.p097j.jjbQypPegg f2197d;
    @XdbacJlTDQ
    /* renamed from: e */
    upgqDBbsrL f2198e;
    @XdbacJlTDQ
    /* renamed from: f */
    zoToeBNOjF f2199f;
    @XdbacJlTDQ
    /* renamed from: g */
    im.getsocial.sdk.internal.p089m.XdbacJlTDQ f2200g;
    @XdbacJlTDQ
    /* renamed from: h */
    im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f2201h;

    /* renamed from: im.getsocial.sdk.internal.jjbQypPegg$5 */
    class C10195 implements Callable<List<InviteChannel>> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2142a;

        C10195(jjbQypPegg jjbqyppegg) {
            this.f2142a = jjbqyppegg;
        }

        public /* synthetic */ Object call() {
            return this.f2142a.f2195b.m2194b();
        }
    }

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
        if (this.f2194a == null) {
            try {
                new im.getsocial.sdk.internal.p069d.jjbQypPegg().m1575a();
            } catch (Exception e) {
                this.f2194a = new im.getsocial.sdk.internal.p033c.p052d.upgqDBbsrL();
            }
        }
        m2049a("email", new im.getsocial.sdk.invites.p092a.p098e.jjbQypPegg());
        m2049a(InviteChannelIds.SMS, new im.getsocial.sdk.invites.p092a.p098e.XdbacJlTDQ());
        m2049a(InviteChannelIds.FACEBOOK_MESSENGER, new im.getsocial.sdk.invites.p092a.p098e.upgqDBbsrL());
        m2049a(InviteChannelIds.GENERIC, new cjrhisSQCL());
    }

    /* renamed from: a */
    static /* synthetic */ String m2012a(Intent intent) {
        return intent == null ? null : intent.getDataString();
    }

    /* renamed from: a */
    static /* synthetic */ boolean m2014a(jjbQypPegg jjbqyppegg, LinkParams linkParams, InviteContent inviteContent) {
        if (linkParams != null) {
            Object obj = linkParams.get(LinkParams.KEY_CUSTOM_IMAGE);
            if (obj instanceof Bitmap) {
                Bitmap bitmap = (Bitmap) obj;
                linkParams.put(LinkParams.KEY_CUSTOM_IMAGE, EmkjBpiUfq.m2102a(bitmap));
                boolean z = inviteContent != null && bitmap.sameAs(inviteContent.getImage());
                linkParams.put("INVITE_AND_LANDINGPAGE_IMAGES_ARE_EQUAL", Boolean.valueOf(z));
                return z;
            }
        }
        return false;
    }

    /* renamed from: a */
    public final void m2015a(final int i, final int i2, final Callback<List<PublicUser>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2137d;

            public void run() {
                this.f2137d.f2195b.m2166a(i, i2, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2016a(Application application) {
        application.registerActivityLifecycleCallbacks(new im.getsocial.sdk.internal.p089m.jjbQypPegg(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2167a;
            /* renamed from: b */
            private final Handler f2168b = new Handler(Looper.getMainLooper());
            /* renamed from: c */
            private final Runnable f2169c = new C10211(this);

            /* renamed from: im.getsocial.sdk.internal.jjbQypPegg$69$1 */
            class C10211 implements Runnable {
                /* renamed from: a */
                final /* synthetic */ AnonymousClass69 f2165a;

                /* renamed from: im.getsocial.sdk.internal.jjbQypPegg$69$1$1 */
                class C10201 implements Runnable {
                    /* renamed from: a */
                    final /* synthetic */ C10211 f2164a;

                    C10201(C10211 c10211) {
                        this.f2164a = c10211;
                    }

                    public void run() {
                        upgqDBbsrL upgqdbbsrl = this.f2164a.f2165a.f2167a.f2195b;
                        upgqdbbsrl.f2247e.mo4359a("application_did_become_inactive_event_timestamp", upgqdbbsrl.f2253k.mo4353b());
                    }
                }

                C10211(AnonymousClass69 anonymousClass69) {
                    this.f2165a = anonymousClass69;
                }

                public void run() {
                    if (this.f2165a.f2167a.f2200g.m2113b() == im.getsocial.sdk.internal.p089m.cjrhisSQCL.PAUSING) {
                        this.f2165a.f2167a.f2200g.m2112a(im.getsocial.sdk.internal.p089m.cjrhisSQCL.PAUSED);
                        if (GetSocial.isInitialized()) {
                            this.f2165a.f2167a.f2194a.m1244a(new C10201(this));
                        }
                    }
                }
            }

            /* renamed from: im.getsocial.sdk.internal.jjbQypPegg$69$2 */
            class C10222 implements Runnable {
                /* renamed from: a */
                final /* synthetic */ AnonymousClass69 f2166a;

                C10222(AnonymousClass69 anonymousClass69) {
                    this.f2166a = anonymousClass69;
                }

                public void run() {
                    this.f2166a.f2167a.f2195b.m2219k();
                }
            }

            {
                this.f2167a = r3;
            }

            public void onActivityPaused(Activity activity) {
                this.f2167a.f2199f.m2397a();
                this.f2167a.f2200g.m2112a(im.getsocial.sdk.internal.p089m.cjrhisSQCL.PAUSING);
                this.f2168b.postDelayed(this.f2169c, 500);
            }

            public void onActivityResumed(Activity activity) {
                im.getsocial.sdk.internal.p089m.cjrhisSQCL b = this.f2167a.f2200g.m2113b();
                Object obj = (b == im.getsocial.sdk.internal.p089m.cjrhisSQCL.PAUSED || b == im.getsocial.sdk.internal.p089m.cjrhisSQCL.NOT_STARTED) ? 1 : null;
                this.f2167a.f2200g.m2112a(im.getsocial.sdk.internal.p089m.cjrhisSQCL.RESUMED);
                this.f2167a.f2194a.m1244a(new Runnable(this.f2167a, activity.getIntent()) {
                    /* renamed from: b */
                    final /* synthetic */ jjbQypPegg f2178b;

                    public void run() {
                        this.f2178b.f2199f.m2399a(jjbQypPegg.m2012a(r3));
                        im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ a = im.getsocial.sdk.pushnotifications.p067a.jjbQypPegg.m2464a(this.f2178b.f2196c);
                        if (a != null) {
                            a.m2418a(true);
                            this.f2178b.f2195b.m2175a(a);
                        }
                    }
                });
                this.f2168b.removeCallbacks(this.f2169c);
                if (obj != null) {
                    this.f2167a.f2194a.m1244a(new C10222(this));
                }
            }
        });
        final UncaughtExceptionHandler defaultUncaughtExceptionHandler = Thread.getDefaultUncaughtExceptionHandler();
        Thread.setDefaultUncaughtExceptionHandler(new UncaughtExceptionHandler(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2176b;

            public void uncaughtException(Thread thread, Throwable th) {
                im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg jjbqyppegg = this.f2176b.f2201h;
                Writer stringWriter = new StringWriter();
                th.printStackTrace(new PrintWriter(stringWriter));
                String stringWriter2 = stringWriter.toString();
                if (im.getsocial.sdk.internal.p089m.ztWNWCuZiM.m2154a(stringWriter2)) {
                    Map hashMap = new HashMap();
                    hashMap.put(AnalyticsEvents.PARAMETER_SHARE_ERROR_MESSAGE, th.getMessage());
                    hashMap.put("error_source", stringWriter2);
                    hashMap.put("error_key", "42");
                    jjbqyppegg.m1053a("sdk_error", hashMap);
                }
                if (defaultUncaughtExceptionHandler != null) {
                    defaultUncaughtExceptionHandler.uncaughtException(thread, th);
                }
            }
        });
    }

    /* renamed from: a */
    public final void m2017a(final Bitmap bitmap, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2087c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(bitmap), "User avatar can not be null");
                this.f2087c.m2028a(UserUpdate.createBuilder().updateAvatar(bitmap).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2018a(final Callback<List<ReferredUser>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2058b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "GetReferredUsers method can not be called without a callback.");
                this.f2058b.f2195b.m2167a(callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2019a(final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2064b;

            public void run() {
                this.f2064b.f2195b.m2168a(completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2020a(final ActivitiesQuery activitiesQuery, final Callback<List<ActivityPost>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2190c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "GetActivities method can not be called without a callback.");
                this.f2190c.f2195b.m2169a(activitiesQuery, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2021a(final TagsQuery tagsQuery, final Callback<List<String>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2025c;

            public void run() {
                this.f2025c.f2195b.m2170a(tagsQuery, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2022a(final FetchReferralDataCallback fetchReferralDataCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2184b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(fetchReferralDataCallback), "GetReferralData method can not be called without a callback.");
                this.f2184b.f2195b.m2172a(fetchReferralDataCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1231a(fetchReferralDataCallback));
    }

    /* renamed from: a */
    public final void m2023a(final NotificationListener notificationListener) {
        this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2147b;

            public void run() {
                upgqDBbsrL upgqdbbsrl = this.f2147b.f2195b;
                NotificationListener notificationListener = (NotificationListener) this.f2147b.f2198e.m1061a(NotificationListener.class, notificationListener, Boolean.valueOf(false));
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) notificationListener), "Can not set null NotificationListener");
                upgqdbbsrl.f2246d.m2436a(notificationListener);
            }
        });
    }

    /* renamed from: a */
    public final void m2024a(final NotificationsCountQuery notificationsCountQuery, final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2153c;

            public void run() {
                this.f2153c.f2195b.m2173a(notificationsCountQuery, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2025a(final NotificationsQuery notificationsQuery, final Callback<List<Notification>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2150c;

            public void run() {
                this.f2150c.f2195b.m2174a(notificationsQuery, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2026a(final AuthIdentity authIdentity, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2067c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "SwitchUser method can not be called without a callback.");
                this.f2067c.f2195b.m2176a(authIdentity, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2027a(final AuthIdentity authIdentity, final AddAuthIdentityCallback addAuthIdentityCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2070c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(addAuthIdentityCallback), "AddAuthIdentity method can not be called without a callback.");
                this.f2070c.f2195b.m2177a(authIdentity, addAuthIdentityCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1233a(addAuthIdentityCallback));
    }

    /* renamed from: a */
    public final void m2028a(final UserUpdate userUpdate, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2033c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "setUserDetails method can not be called without a callback.");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(userUpdate), "setUserDetails method can not be called without UserUpdate.");
                im.getsocial.sdk.usermanagement.XdbacJlTDQ xdbacJlTDQ = new im.getsocial.sdk.usermanagement.XdbacJlTDQ(userUpdate);
                im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg = null;
                if (xdbacJlTDQ.m3642g() != null) {
                    jjbqyppegg = im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(EmkjBpiUfq.m2102a(xdbacJlTDQ.m3642g()));
                }
                this.f2033c.f2195b.m2179a(new pdwpUtZXDT().m3656a(xdbacJlTDQ.m3636a()).m3659b(xdbacJlTDQ.m3637b()).m3657a(xdbacJlTDQ.m3638c()).m3663c(xdbacJlTDQ.m3639d()).m3660b(xdbacJlTDQ.m3640e()).m3664d(xdbacJlTDQ.m3641f()).m3655a(jjbqyppegg), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2029a(final UsersQuery usersQuery, final Callback<List<UserReference>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2104c;

            public void run() {
                this.f2104c.f2195b.m2178a(usersQuery, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2030a(final Runnable runnable) {
        this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2006b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(runnable), "Can not call WhenInitialize with null runnable");
                upgqDBbsrL upgqdbbsrl = this.f2006b.f2195b;
                Runnable runnable = (Runnable) this.f2006b.f2198e.m1060a(Runnable.class, runnable);
                if (upgqdbbsrl.m2192a()) {
                    runnable.run();
                } else {
                    upgqdbbsrl.f2243a.m1310a(runnable);
                }
            }
        });
    }

    /* renamed from: a */
    public final void m2031a(final String str) {
        this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2030b;

            public void run() {
                if (!HptYHntaqF.m2107a()) {
                    im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(this.f2030b.f2197d.mo4574b() != -1, "GetSocial INSTALL_REFERRER receiver is not configured properly. Check the errors above for details.");
                }
                upgqDBbsrL upgqdbbsrl = this.f2030b.f2195b;
                upgqdbbsrl.f2255m.mo4385a(new C10291(upgqdbbsrl, str));
            }
        });
    }

    /* renamed from: a */
    public final void m2032a(String str, int i, int i2, Callback<List<PublicUser>> callback) {
        final Callback<List<PublicUser>> callback2 = callback;
        final String str2 = str;
        final int i3 = i;
        final int i4 = i2;
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: e */
            final /* synthetic */ jjbQypPegg f2015e;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback2), "GetActivityLikers method can not be called without a callback.");
                this.f2015e.f2195b.m2180a(str2, i3, i4, callback2);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2033a(final String str, final Callback<List<ActivityPost>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2187c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "GetStickyActivities method can not be called without a callback.");
                this.f2187c.f2195b.m2211e(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2034a(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2022c;

            public void run() {
                this.f2022c.f2195b.m2198b(str, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2035a(final String str, final ActivityPostContent activityPostContent, final Callback<ActivityPost> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2000d;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "PostActivity method can not be called without a callback.");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Feed name can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(activityPostContent), "Activity content can not be null");
                im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg = null;
                if (activityPostContent.getImage() != null) {
                    jjbqyppegg = im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(EmkjBpiUfq.m2102a(activityPostContent.getImage()));
                }
                if (activityPostContent.getVideo() != null) {
                    jjbqyppegg = im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2008b(activityPostContent.getVideo());
                }
                this.f2000d.f2195b.m2171a(im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg.m952a(str).m956a(activityPostContent.getButtonTitle(), activityPostContent.getButtonAction()).m955a(jjbqyppegg).m958c(activityPostContent.getText()), callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2036a(final String str, final ReportingReason reportingReason, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2019d;

            public void run() {
                this.f2019d.f2195b.m2183a(str, reportingReason, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2037a(String str, InviteContent inviteContent, LinkParams linkParams, InviteCallback inviteCallback) {
        final InviteCallback inviteCallback2 = inviteCallback;
        final LinkParams linkParams2 = linkParams;
        final InviteContent inviteContent2 = inviteContent;
        final String str2 = str;
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: e */
            final /* synthetic */ jjbQypPegg f2174e;

            public void run() {
                im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT pdwputzxdt = null;
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(inviteCallback2), "sendInvite method can not be called without a callback.");
                LinkParams linkParams = linkParams2 != null ? new LinkParams(linkParams2) : null;
                boolean a = jjbQypPegg.m2014a(this.f2174e, linkParams, inviteContent2);
                if (inviteContent2 != null) {
                    byte[] a2;
                    im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg a3 = im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.m2276a().m2271b(inviteContent2.getText()).m2267a(inviteContent2.getSubject());
                    if (!a) {
                        a2 = EmkjBpiUfq.m2102a(inviteContent2.getImage());
                    }
                    pdwputzxdt = a3.m2268a(a2).m2273c(inviteContent2.getImageUrl()).m2275e(inviteContent2.getGifUrl()).m2274d(inviteContent2.getVideoUrl()).m2272b(inviteContent2.getVideo()).m2269a();
                }
                this.f2174e.f2195b.m2184a(str2, pdwputzxdt, linkParams, inviteCallback2);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1232a(inviteCallback));
    }

    /* renamed from: a */
    public final void m2038a(final String str, final String str2, final Callback<PublicUser> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2097d;

            public void run() {
                this.f2097d.f2195b.m2185a(str, str2, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2039a(final String str, final String str2, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2037d;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Public property key can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str2), "Public property value can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "setUserDetails method can not be called without a callback.");
                this.f2037d.m2028a(UserUpdate.createBuilder().setPublicProperty(str, str2).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2040a(final String str, final List<String> list, final Callback<Map<String, PublicUser>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2101d;

            public void run() {
                this.f2101d.f2195b.m2186a(str, list, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2041a(final String str, final List<String> list, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2127d;

            public void run() {
                this.f2127d.f2195b.m2187a(str, list, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2042a(final String str, final boolean z, final Callback<ActivityPost> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2010d;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "LikeActivity method can not be called without a callback.");
                this.f2010d.f2195b.m2188a(str, z, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: a */
    public final void m2043a(final List<String> list, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2123c;

            public void run() {
                this.f2123c.f2195b.m2189a(list, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2044a(final List<String> list, final boolean z, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2157d;

            public void run() {
                this.f2157d.f2195b.m2190a(list, z, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final void m2045a(final boolean z, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2160c;

            public void run() {
                this.f2160c.f2195b.m2191a(z, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: a */
    public final boolean m2046a() {
        return ((Boolean) this.f2194a.m1239a(new Callable<Boolean>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2042a;

            {
                this.f2042a = r1;
            }

            public /* synthetic */ Object call() {
                return Boolean.valueOf(this.f2042a.f2195b.m2192a());
            }
        }, Boolean.valueOf(false))).booleanValue();
    }

    /* renamed from: a */
    public final boolean m2047a(final GlobalErrorListener globalErrorListener) {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2093b;

            public void run() {
                this.f2093b.f2194a.m1242a(globalErrorListener);
            }
        });
    }

    /* renamed from: a */
    public final boolean m2048a(final OnUserChangedListener onUserChangedListener) {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2027b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(onUserChangedListener), "OnUserChangedListener can not be null");
                upgqDBbsrL upgqdbbsrl = this.f2027b.f2195b;
                OnUserChangedListener onUserChangedListener = (OnUserChangedListener) this.f2027b.f2198e.m1060a(OnUserChangedListener.class, onUserChangedListener);
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) onUserChangedListener), "OnUserChangedListener can not be null");
                upgqdbbsrl.f2243a.m1309a(onUserChangedListener);
            }
        });
    }

    /* renamed from: a */
    public final boolean m2049a(final String str, final InviteChannelPlugin inviteChannelPlugin) {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2078c;

            public void run() {
                upgqDBbsrL.m2161a(str, new im.getsocial.sdk.invites.p092a.p098e.pdwpUtZXDT(inviteChannelPlugin));
            }
        });
    }

    /* renamed from: b */
    public final void m2050b(final int i, final int i2, final Callback<List<SuggestedFriend>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2141d;

            public void run() {
                this.f2141d.f2195b.m2195b(i, i2, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: b */
    public final void m2051b(final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2133b;

            public void run() {
                this.f2133b.f2195b.m2196b(callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: b */
    public final void m2052b(final String str, final Callback<ActivityPost> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2193c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "GetActivity method can not be called without a callback.");
                this.f2193c.f2195b.m2214f(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: b */
    public final void m2053b(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2045c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Public property key can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "setUserDetails method can not be called without a callback.");
                this.f2045c.m2028a(UserUpdate.createBuilder().removePublicProperty(str).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: b */
    public final void m2054b(final String str, final ActivityPostContent activityPostContent, final Callback<ActivityPost> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2004d;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(callback), "PostComment method can not be called without a callback.");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Activity ID can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(activityPostContent), "Comment content can not be null");
                im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg = null;
                if (activityPostContent.getImage() != null) {
                    jjbqyppegg = im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(EmkjBpiUfq.m2102a(activityPostContent.getImage()));
                }
                if (activityPostContent.getVideo() != null) {
                    jjbqyppegg = im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2008b(activityPostContent.getVideo());
                }
                this.f2004d.f2195b.m2171a(im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg.m953b(str).m956a(activityPostContent.getButtonTitle(), activityPostContent.getButtonAction()).m955a(jjbqyppegg).m958c(activityPostContent.getText()), callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: b */
    public final void m2055b(final String str, final String str2, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2041d;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Private property key can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str2), "Private property value can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "setUserDetails method can not be called without a callback.");
                this.f2041d.m2028a(UserUpdate.createBuilder().setPrivateProperty(str, str2).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: b */
    public final void m2056b(final String str, final List<String> list, final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2113d;

            public void run() {
                this.f2113d.f2195b.m2199b(str, list, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: b */
    public final boolean m2057b() {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2131a;

            {
                this.f2131a = r1;
            }

            public void run() {
                this.f2131a.f2194a.m1240a();
            }
        });
    }

    /* renamed from: b */
    public final boolean m2058b(final String str) {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2182b;

            public void run() {
                upgqDBbsrL.m2160a(str);
            }
        });
    }

    /* renamed from: c */
    public final String m2059c() {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2161a;

            {
                this.f2161a = r1;
            }

            public /* synthetic */ Object call() {
                String b = this.f2161a.f2195b.f2244b.m1992b();
                return b == null ? "en" : b;
            }
        }, (Object) "en");
    }

    /* renamed from: c */
    public final void m2060c(final Callback<List<UserReference>> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2144b;

            public void run() {
                this.f2144b.f2195b.m2202c(callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: c */
    public final void m2061c(final String str, final Callback<PublicUser> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2091c;

            public void run() {
                this.f2091c.f2195b.m2181a(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: c */
    public final void m2062c(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2048c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "Private property key can not be null");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "setUserDetails method can not be called without a callback.");
                this.f2048c.m2028a(UserUpdate.createBuilder().removePrivateProperty(str).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: c */
    public final void m2063c(final String str, final List<String> list, final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ jjbQypPegg f2120d;

            public void run() {
                this.f2120d.f2195b.m2205c(str, list, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: c */
    public final boolean m2064c(final String str) {
        return ((Boolean) this.f2194a.m1239a(new Callable<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2109b;

            public /* synthetic */ Object call() {
                boolean z;
                upgqDBbsrL upgqdbbsrl = this.f2109b.f2195b;
                String str = str;
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "ChannelId should be one of the constants defined in InviteChannelIds class");
                for (InviteChannel channelId : upgqdbbsrl.m2194b()) {
                    if (str.equals(channelId.getChannelId())) {
                        z = true;
                        break;
                    }
                }
                z = false;
                return Boolean.valueOf(z);
            }
        }, Boolean.valueOf(false))).booleanValue();
    }

    /* renamed from: d */
    public final String m2065d(final String str) {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2050b;

            public /* synthetic */ Object call() {
                return this.f2050b.f2195b.m2193b(str);
            }
        }, null);
    }

    /* renamed from: d */
    public final List<InviteChannel> m2066d() {
        return (List) this.f2194a.m1239a(new C10195(this), Collections.emptyList());
    }

    /* renamed from: d */
    public final void m2067d(final Callback<Boolean> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2163b;

            public void run() {
                this.f2163b.f2195b.m2207d(callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: d */
    public final void m2068d(final String str, final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2107c;

            public void run() {
                this.f2107c.f2195b.m2197b(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: d */
    public final void m2069d(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2073c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(completionCallback), "RemoveAuthIdentity method can not be called without a callback.");
                this.f2073c.f2195b.m2182a(str, completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: e */
    public final String m2070e(final String str) {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2052b;

            public /* synthetic */ Object call() {
                return this.f2052b.f2195b.m2201c(str);
            }
        }, null);
    }

    /* renamed from: e */
    public final void m2071e(final String str, final Callback<Integer> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2116c;

            public void run() {
                this.f2116c.f2195b.m2203c(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: e */
    public final void m2072e(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2081c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "User display name can not be null");
                this.f2081c.m2028a(UserUpdate.createBuilder().updateDisplayName(str).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: e */
    public final boolean m2073e() {
        return this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2028a;

            {
                this.f2028a = r1;
            }

            public void run() {
                this.f2028a.f2195b.f2243a.m1309a(null);
            }
        });
    }

    /* renamed from: f */
    public final Map<String, String> m2074f() {
        return (Map) this.f2194a.m1239a(new Callable<Map<String, String>>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2059a;

            {
                this.f2059a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2059a.f2195b.m2213f();
            }
        }, Collections.emptyMap());
    }

    /* renamed from: f */
    public final void m2075f(final String str, final Callback<Boolean> callback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2130c;

            public void run() {
                this.f2130c.f2195b.m2208d(str, callback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1229a((Callback) callback));
    }

    /* renamed from: f */
    public final void m2076f(final String str, final CompletionCallback completionCallback) {
        this.f2194a.m1243a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2084c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(str), "User avatar url can not be null");
                this.f2084c.m2028a(UserUpdate.createBuilder().updateAvatarUrl(str).build(), completionCallback);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(completionCallback));
    }

    /* renamed from: f */
    public final boolean m2077f(final String str) {
        return ((Boolean) this.f2194a.m1239a(new Callable<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2054b;

            public /* synthetic */ Object call() {
                return Boolean.valueOf(this.f2054b.f2195b.m2209d(str));
            }
        }, Boolean.valueOf(false))).booleanValue();
    }

    /* renamed from: g */
    public final Map<String, String> m2078g() {
        return (Map) this.f2194a.m1239a(new Callable<Map<String, String>>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2060a;

            {
                this.f2060a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2060a.f2195b.m2215g();
            }
        }, Collections.emptyMap());
    }

    /* renamed from: g */
    public final boolean m2079g(final String str) {
        return ((Boolean) this.f2194a.m1239a(new Callable<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2056b;

            public /* synthetic */ Object call() {
                return Boolean.valueOf(this.f2056b.f2195b.m2212e(str));
            }
        }, Boolean.valueOf(false))).booleanValue();
    }

    /* renamed from: h */
    public final String m2080h() {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2061a;

            {
                this.f2061a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2061a.f2195b.m2200c();
            }
        }, null);
    }

    /* renamed from: i */
    public final boolean m2081i() {
        return ((Boolean) this.f2194a.m1239a(new Callable<Boolean>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2062a;

            {
                this.f2062a = r1;
            }

            public /* synthetic */ Object call() {
                return Boolean.valueOf(this.f2062a.f2195b.m2216h());
            }
        }, Boolean.valueOf(true))).booleanValue();
    }

    /* renamed from: j */
    public final String m2082j() {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2074a;

            {
                this.f2074a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2074a.f2195b.m2206d();
            }
        }, null);
    }

    /* renamed from: k */
    public final String m2083k() {
        return (String) this.f2194a.m1239a(new Callable<String>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2075a;

            {
                this.f2075a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2075a.f2195b.m2210e();
            }
        }, null);
    }

    /* renamed from: l */
    public final Map<String, String> m2084l() {
        return (Map) this.f2194a.m1239a(new Callable<Map<String, String>>(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2088a;

            {
                this.f2088a = r1;
            }

            public /* synthetic */ Object call() {
                return this.f2088a.f2195b.m2217i();
            }
        }, new HashMap());
    }

    /* renamed from: m */
    public final void m2085m() {
        this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2145a;

            {
                this.f2145a = r1;
            }

            public void run() {
                this.f2145a.f2195b.m2218j();
            }
        });
    }

    /* renamed from: n */
    public final void m2086n() {
        this.f2194a.m1244a(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2180a;

            /* renamed from: im.getsocial.sdk.internal.jjbQypPegg$72$1 */
            class C10241 implements Runnable {
                /* renamed from: a */
                final /* synthetic */ AnonymousClass72 f2179a;

                C10241(AnonymousClass72 anonymousClass72) {
                    this.f2179a = anonymousClass72;
                }

                public void run() {
                    upgqDBbsrL upgqdbbsrl = this.f2179a.f2180a.f2195b;
                    if (upgqdbbsrl.f2248f instanceof KluUZYuxme) {
                        ((KluUZYuxme) upgqdbbsrl.f2248f).m2109a();
                    }
                }
            }

            {
                this.f2180a = r1;
            }

            public void run() {
                new Handler(Looper.getMainLooper()).postDelayed(new C10241(this), 100);
            }
        });
    }
}
