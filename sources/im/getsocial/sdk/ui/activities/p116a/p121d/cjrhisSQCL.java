package im.getsocial.sdk.ui.activities.p116a.p121d;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocial.User;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.ui.activities.p116a.p123g.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.qZypgoeblR;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import im.getsocial.sdk.usermanagement.pdwpUtZXDT;
import java.util.List;
import java.util.Locale;

/* renamed from: im.getsocial.sdk.ui.activities.a.d.cjrhisSQCL */
public class cjrhisSQCL extends upgqDBbsrL<jjbQypPegg> {
    /* renamed from: a */
    private final pdwpUtZXDT f2736a;
    /* renamed from: b */
    private boolean f2737b = false;

    /* renamed from: im.getsocial.sdk.ui.activities.a.d.cjrhisSQCL$jjbQypPegg */
    private class jjbQypPegg extends qZypgoeblR {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2731a;
        /* renamed from: b */
        private final String f2732b;

        jjbQypPegg(cjrhisSQCL cjrhissqcl, String str) {
            this.f2731a = cjrhissqcl;
            this.f2732b = str;
        }

        public void run() {
            if (this.f2731a.f2737b) {
                final String str = this.f2732b;
                GetSocial.findUsers(UsersQuery.usersByDisplayName(str).withLimit(20), new Callback<List<UserReference>>(this) {
                    /* renamed from: b */
                    final /* synthetic */ jjbQypPegg f2725b;

                    public void onFailure(GetSocialException getSocialException) {
                        if (!this.f2725b.m3038b()) {
                            this.f2725b.f2731a.f2736a.mo4644b(getSocialException);
                        }
                    }

                    public /* synthetic */ void onSuccess(Object obj) {
                        List<UserReference> list = (List) obj;
                        for (UserReference a : list) {
                            this.f2725b.f2731a.m3057a(a);
                        }
                        if (!this.f2725b.m3038b()) {
                            if (list.isEmpty()) {
                                this.f2725b.f2731a.m3046b(str);
                            }
                            this.f2725b.f2731a.f2736a.mo4643a(this.f2725b.f2731a.m3048d(str));
                        }
                    }
                });
                return;
            }
            final String str2 = this.f2732b;
            User.getFriendsReferences(new Callback<List<UserReference>>(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f2727b;

                public void onFailure(GetSocialException getSocialException) {
                    if (!this.f2727b.m3038b()) {
                        this.f2727b.f2731a.f2736a.mo4644b(getSocialException);
                    }
                }

                public /* synthetic */ void onSuccess(Object obj) {
                    List<UserReference> list = (List) obj;
                    this.f2727b.f2731a.f2737b = true;
                    for (UserReference a : list) {
                        this.f2727b.f2731a.m3057a(a);
                    }
                    if (!this.f2727b.m3038b()) {
                        this.f2727b.f2731a.m3047c(str2);
                    }
                }
            });
        }
    }

    public cjrhisSQCL(pdwpUtZXDT pdwputzxdt) {
        this.f2736a = pdwputzxdt;
    }

    /* renamed from: a */
    protected final qZypgoeblR mo4711a(String str) {
        return new jjbQypPegg(this, str);
    }

    /* renamed from: a */
    public final void m3055a(jjbQypPegg jjbqyppegg) {
        m3042a((Object) jjbqyppegg);
    }

    /* renamed from: a */
    public final void m3056a(PublicUser publicUser) {
        if (!new pdwpUtZXDT(publicUser).m3730a() && !publicUser.getId().equals(User.getId())) {
            m3042a((Object) new jjbQypPegg(publicUser));
        }
    }

    /* renamed from: a */
    public final void m3057a(UserReference userReference) {
        if (!userReference.getId().equals(User.getId())) {
            m3042a((Object) new jjbQypPegg(userReference));
        }
    }

    /* renamed from: a */
    protected final void mo4712a(List<jjbQypPegg> list) {
        this.f2736a.mo4643a(list);
    }

    /* renamed from: a */
    protected final /* synthetic */ boolean mo4713a(Object obj, String str) {
        String toLowerCase = ((jjbQypPegg) obj).m3063c().toLowerCase(Locale.getDefault());
        if (toLowerCase.startsWith(str)) {
            return true;
        }
        for (String trim : toLowerCase.split(" ")) {
            if (trim.trim().startsWith(str)) {
                return true;
            }
        }
        return false;
    }
}
