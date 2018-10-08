package im.getsocial.sdk.ui.activities.p116a.p124c;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.activities.AbstractActivitiesViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.zoToeBNOjF */
public class zoToeBNOjF extends AbstractActivitiesViewBuilder<zoToeBNOjF> {
    /* renamed from: f */
    private final ActivityPost f2719f;
    /* renamed from: g */
    private final jjbQypPegg<String, ActivityPost> f2720g;

    private zoToeBNOjF(ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        this.f2719f = activityPost;
        this.f2720g = jjbqyppegg;
    }

    /* renamed from: a */
    public static zoToeBNOjF m3023a(ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        return new zoToeBNOjF(activityPost, jjbqyppegg);
    }

    /* renamed from: c */
    protected final upgqDBbsrL mo4594c() {
        return new pdwpUtZXDT(new XdbacJlTDQ(), new upgqDBbsrL(this.f2719f, this.f2720g), this.f2719f);
    }
}
