package im.getsocial.sdk.ui.activities.p116a.p122h;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.ui.activities.p116a.p123g.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.qZypgoeblR;
import java.util.Collection;
import java.util.List;
import java.util.Locale;

/* renamed from: im.getsocial.sdk.ui.activities.a.h.cjrhisSQCL */
public class cjrhisSQCL extends upgqDBbsrL<String> {
    /* renamed from: a */
    private final upgqDBbsrL f2750a;
    /* renamed from: b */
    private final String f2751b;

    /* renamed from: im.getsocial.sdk.ui.activities.a.h.cjrhisSQCL$jjbQypPegg */
    private class jjbQypPegg extends qZypgoeblR {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2748a;
        /* renamed from: b */
        private final String f2749b;

        /* renamed from: im.getsocial.sdk.ui.activities.a.h.cjrhisSQCL$jjbQypPegg$1 */
        class C11201 implements Callback<List<String>> {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2747a;

            C11201(jjbQypPegg jjbqyppegg) {
                this.f2747a = jjbqyppegg;
            }

            public void onFailure(GetSocialException getSocialException) {
                if (!this.f2747a.m3038b()) {
                    this.f2747a.f2748a.f2750a.mo4647c(getSocialException);
                }
            }

            public /* synthetic */ void onSuccess(Object obj) {
                Collection collection = (List) obj;
                this.f2747a.f2748a.m3043a(collection);
                if (!this.f2747a.m3038b()) {
                    if (collection.isEmpty()) {
                        this.f2747a.f2748a.m3046b(this.f2747a.f2749b);
                    }
                    this.f2747a.f2748a.f2750a.mo4646b(collection);
                }
            }
        }

        jjbQypPegg(cjrhisSQCL cjrhissqcl, String str) {
            this.f2748a = cjrhissqcl;
            this.f2749b = str;
        }

        public void run() {
            GetSocial.findTags(TagsQuery.tagsForFeed(this.f2748a.f2751b).withName(this.f2749b).withLimit(20), new C11201(this));
        }
    }

    public cjrhisSQCL(upgqDBbsrL upgqdbbsrl, String str) {
        this.f2750a = upgqdbbsrl;
        this.f2751b = str;
    }

    /* renamed from: a */
    protected final qZypgoeblR mo4711a(String str) {
        return new jjbQypPegg(this, str);
    }

    /* renamed from: a */
    protected final void mo4712a(List<String> list) {
        this.f2750a.mo4646b(list);
    }

    /* renamed from: a */
    protected final /* synthetic */ boolean mo4713a(Object obj, String str) {
        return ((String) obj).toLowerCase(Locale.getDefault()).startsWith(str.toLowerCase(Locale.getDefault()));
    }
}
