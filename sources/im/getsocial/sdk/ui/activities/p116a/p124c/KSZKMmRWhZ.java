package im.getsocial.sdk.ui.activities.p116a.p124c;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.activities.AbstractActivitiesViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.KSZKMmRWhZ */
public class KSZKMmRWhZ extends AbstractActivitiesViewBuilder {
    /* renamed from: f */
    private final ActivityPost f2700f;
    /* renamed from: g */
    private final jjbQypPegg<String, ActivityPost> f2701g;

    private KSZKMmRWhZ(ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        this.f2700f = activityPost;
        this.f2701g = jjbqyppegg;
    }

    /* renamed from: a */
    public static KSZKMmRWhZ m2982a(ActivityPost activityPost, jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        return new KSZKMmRWhZ(activityPost, jjbqyppegg);
    }

    /* renamed from: c */
    protected final upgqDBbsrL mo4594c() {
        return new ztWNWCuZiM(new XdbacJlTDQ(), new upgqDBbsrL(this.f2700f, this.f2701g), this.f2700f);
    }
}
