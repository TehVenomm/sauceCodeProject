package im.getsocial.sdk.ui.internal;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.ViewBuilder;
import im.getsocial.sdk.ui.activities.AbstractActivitiesViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p119a.XdbacJlTDQ;
import im.getsocial.sdk.ui.activities.p116a.p120i.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p120i.pdwpUtZXDT;
import im.getsocial.sdk.ui.activities.p116a.p124c.KSZKMmRWhZ;
import im.getsocial.sdk.ui.activities.p116a.p124c.zoToeBNOjF;

public class upgqDBbsrL {
    /* renamed from: a */
    private static final upgqDBbsrL f3074a = new upgqDBbsrL();

    interface jjbQypPegg<T> {
        /* renamed from: a */
        void mo4739a(T t);
    }

    /* renamed from: a */
    private static ViewBuilder m3460a(im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL upgqdbbsrl, final AbstractActivitiesViewBuilder abstractActivitiesViewBuilder) {
        m3464a((Object) upgqdbbsrl, im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL.class, new jjbQypPegg<im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL>() {
            /* renamed from: a */
            public final /* synthetic */ void mo4739a(Object obj) {
                abstractActivitiesViewBuilder.setAvatarClickListener(((im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL) obj).mo4624f());
            }
        });
        m3464a((Object) upgqdbbsrl, im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg.class, new jjbQypPegg<im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg>() {
            /* renamed from: a */
            public final /* synthetic */ void mo4739a(Object obj) {
                abstractActivitiesViewBuilder.setButtonActionListener(((im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg) obj).mo4623e());
            }
        });
        m3464a((Object) upgqdbbsrl, cjrhisSQCL.class, new jjbQypPegg<cjrhisSQCL>() {
            /* renamed from: a */
            public final /* synthetic */ void mo4739a(Object obj) {
                abstractActivitiesViewBuilder.setMentionClickListener(((cjrhisSQCL) obj).mo4625g());
            }
        });
        m3464a((Object) upgqdbbsrl, pdwpUtZXDT.class, new jjbQypPegg<pdwpUtZXDT>() {
            /* renamed from: a */
            public final /* synthetic */ void mo4739a(Object obj) {
                abstractActivitiesViewBuilder.setTagClickListener(((pdwpUtZXDT) obj).mo4626h());
            }
        });
        abstractActivitiesViewBuilder.setUiActionListener(upgqdbbsrl.m2589u());
        return abstractActivitiesViewBuilder;
    }

    /* renamed from: a */
    public static upgqDBbsrL m3461a() {
        return f3074a;
    }

    /* renamed from: a */
    public static void m3462a(im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL upgqdbbsrl, ActivityPost activityPost, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        m3460a(upgqdbbsrl, zoToeBNOjF.m3023a(activityPost, jjbqyppegg)).show();
    }

    /* renamed from: a */
    public static void m3463a(im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL upgqdbbsrl, ActivityPost activityPost, im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.jjbQypPegg jjbqyppegg, boolean z) {
        m3460a(upgqdbbsrl, XdbacJlTDQ.m2612a(jjbqyppegg.mo4650e(), activityPost, jjbqyppegg.mo4649d()).setReadOnly(z)).show();
    }

    /* renamed from: a */
    private static <T> void m3464a(Object obj, Class<T> cls, jjbQypPegg<T> jjbqyppegg) {
        if (cls.isInstance(obj)) {
            jjbqyppegg.mo4739a(cls.cast(obj));
        }
    }

    /* renamed from: b */
    public static void m3465b(im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL upgqdbbsrl, ActivityPost activityPost, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        m3460a(upgqdbbsrl, KSZKMmRWhZ.m2982a(activityPost, jjbqyppegg)).show();
    }
}
