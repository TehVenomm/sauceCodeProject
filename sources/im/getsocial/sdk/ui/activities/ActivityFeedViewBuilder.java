package im.getsocial.sdk.ui.activities;

import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.ui.activities.p116a.p118b.XdbacJlTDQ;
import im.getsocial.sdk.ui.activities.p116a.p118b.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p118b.pdwpUtZXDT;
import im.getsocial.sdk.ui.activities.p116a.p118b.zoToeBNOjF;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;
import java.util.Arrays;

public class ActivityFeedViewBuilder extends AbstractActivitiesViewBuilder<ActivityFeedViewBuilder> {
    /* renamed from: f */
    private final String f2583f;
    /* renamed from: g */
    private String f2584g;
    /* renamed from: h */
    private boolean f2585h;
    /* renamed from: i */
    private String[] f2586i = new String[0];

    private class jjbQypPegg implements zoToeBNOjF {
        /* renamed from: a */
        final /* synthetic */ ActivityFeedViewBuilder f2582a;

        private jjbQypPegg(ActivityFeedViewBuilder activityFeedViewBuilder) {
            this.f2582a = activityFeedViewBuilder;
        }

        /* renamed from: a */
        public final ActivitiesQuery mo4596a() {
            return ActivitiesQuery.postsForFeed(this.f2582a.f2583f).withLimit(25).filterByUser(this.f2582a.f2584g).withTags(this.f2582a.f2586i).friendsFeed(this.f2582a.f2585h);
        }
    }

    private ActivityFeedViewBuilder(String str) {
        this.f2583f = str;
    }

    public static ActivityFeedViewBuilder create(String str) {
        return new ActivityFeedViewBuilder(str);
    }

    /* renamed from: c */
    protected final upgqDBbsrL mo4594c() {
        byte b = (byte) 1;
        byte b2 = this.f2584g != null ? (byte) 1 : (byte) 0;
        if (this.f2586i.length <= 0) {
            b = (byte) 0;
        }
        zoToeBNOjF jjbqyppegg = new jjbQypPegg();
        im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.jjbQypPegg jjbqyppegg2 = (b2 == (byte) 0 && b == (byte) 0) ? new im.getsocial.sdk.ui.activities.p116a.p118b.jjbQypPegg(this.f2583f, jjbqyppegg) : new cjrhisSQCL(this.f2583f, jjbqyppegg);
        return new pdwpUtZXDT(new XdbacJlTDQ(this.d, Arrays.asList(this.f2586i)), jjbqyppegg2, this.d);
    }

    public ActivityFeedViewBuilder setFilterByTags(String... strArr) {
        this.f2586i = strArr;
        return this;
    }

    public ActivityFeedViewBuilder setFilterByUser(String str) {
        this.f2584g = str;
        return this;
    }

    public ActivityFeedViewBuilder setShowFriendsFeed(boolean z) {
        this.f2585h = z;
        return this;
    }
}
