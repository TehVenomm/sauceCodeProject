package im.getsocial.sdk.ui.activities.p116a.p119a;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.activities.AbstractActivitiesViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.ui.activities.a.a.XdbacJlTDQ */
public final class XdbacJlTDQ extends AbstractActivitiesViewBuilder<XdbacJlTDQ> {
    /* renamed from: f */
    private final ActivityPost f2587f;
    /* renamed from: g */
    private final jjbQypPegg<String, ActivityPost> f2588g;
    /* renamed from: h */
    private final String f2589h;
    /* renamed from: i */
    private String f2590i;

    private XdbacJlTDQ(String str, ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        this.f2589h = str;
        this.f2587f = activityPost;
        this.f2588g = jjbqyppegg;
    }

    /* renamed from: a */
    public static XdbacJlTDQ m2612a(String str, ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        return new XdbacJlTDQ(str, activityPost, jjbqyppegg);
    }

    /* renamed from: a */
    public final XdbacJlTDQ m2613a(String str) {
        this.f2590i = str;
        return this;
    }

    /* renamed from: c */
    protected final upgqDBbsrL mo4594c() {
        return new cjrhisSQCL(new pdwpUtZXDT(this.d), new jjbQypPegg(this.f2589h, this.f2587f, this.f2588g), this.f2590i);
    }
}
