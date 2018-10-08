package im.getsocial.sdk.activities;

import im.getsocial.sdk.activities.ActivityPost.Type;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p066m.upgqDBbsrL;
import im.getsocial.sdk.socialgraph.p109a.p110a.cjrhisSQCL;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public final class ActivitiesQuery {
    public static final int DEFAULT_LIMIT = 10;
    public static final String GLOBAL_FEED = "g-global";
    /* renamed from: a */
    private final Type f1102a;
    /* renamed from: b */
    private final String f1103b;
    /* renamed from: c */
    private final String f1104c;
    /* renamed from: d */
    private int f1105d = 10;
    /* renamed from: e */
    private Filter f1106e = Filter.NO_FILTER;
    /* renamed from: f */
    private String f1107f;
    /* renamed from: g */
    private String f1108g;
    /* renamed from: h */
    private boolean f1109h;
    /* renamed from: i */
    private List<String> f1110i = Collections.emptyList();

    public enum Filter {
        NO_FILTER,
        OLDER,
        NEWER
    }

    private ActivitiesQuery(Type type, String str, String str2) {
        this.f1102a = type;
        this.f1103b = str;
        this.f1104c = str2;
    }

    public static ActivitiesQuery commentsToPost(String str) {
        return new ActivitiesQuery(Type.COMMENT, null, str);
    }

    public static ActivitiesQuery postsForFeed(String str) {
        return new ActivitiesQuery(Type.POST, str, null);
    }

    public static ActivitiesQuery postsForGlobalFeed() {
        return new ActivitiesQuery(Type.POST, GLOBAL_FEED, null);
    }

    /* renamed from: a */
    final Type m913a() {
        return this.f1102a;
    }

    /* renamed from: b */
    final String m914b() {
        return this.f1103b;
    }

    /* renamed from: c */
    final String m915c() {
        return this.f1104c;
    }

    /* renamed from: d */
    final int m916d() {
        return this.f1105d;
    }

    /* renamed from: e */
    final String m917e() {
        return this.f1106e == Filter.OLDER ? this.f1107f : null;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            ActivitiesQuery activitiesQuery = (ActivitiesQuery) obj;
            if (this.f1105d != activitiesQuery.f1105d || this.f1109h != activitiesQuery.f1109h || this.f1102a != activitiesQuery.f1102a) {
                return false;
            }
            if (this.f1103b != null) {
                if (!this.f1103b.equals(activitiesQuery.f1103b)) {
                    return false;
                }
            } else if (activitiesQuery.f1103b != null) {
                return false;
            }
            if (this.f1104c != null) {
                if (!this.f1104c.equals(activitiesQuery.f1104c)) {
                    return false;
                }
            } else if (activitiesQuery.f1104c != null) {
                return false;
            }
            if (this.f1106e != activitiesQuery.f1106e) {
                return false;
            }
            if (this.f1107f != null) {
                if (!this.f1107f.equals(activitiesQuery.f1107f)) {
                    return false;
                }
            } else if (activitiesQuery.f1107f != null) {
                return false;
            }
            if (this.f1108g != null) {
                if (!this.f1108g.equals(activitiesQuery.f1108g)) {
                    return false;
                }
            } else if (activitiesQuery.f1108g != null) {
                return false;
            }
            if (this.f1110i != null) {
                return upgqDBbsrL.m1520a(this.f1110i, activitiesQuery.f1110i);
            }
            if (activitiesQuery.f1110i != null) {
                return false;
            }
        }
        return true;
    }

    /* renamed from: f */
    final String m918f() {
        return this.f1106e == Filter.NEWER ? this.f1107f : null;
    }

    public final ActivitiesQuery filterByUser(String str) {
        this.f1108g = str;
        return this;
    }

    public final ActivitiesQuery friendsFeed(boolean z) {
        this.f1109h = z;
        return this;
    }

    /* renamed from: g */
    final String m919g() {
        return this.f1108g;
    }

    /* renamed from: h */
    final boolean m920h() {
        return this.f1109h;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1102a != null ? this.f1102a.hashCode() : 0;
        int hashCode2 = this.f1103b != null ? this.f1103b.hashCode() : 0;
        int hashCode3 = this.f1104c != null ? this.f1104c.hashCode() : 0;
        int i2 = this.f1105d;
        int hashCode4 = this.f1106e != null ? this.f1106e.hashCode() : 0;
        int hashCode5 = this.f1107f != null ? this.f1107f.hashCode() : 0;
        int hashCode6 = this.f1108g != null ? this.f1108g.hashCode() : 0;
        int i3 = this.f1109h ? 1 : 0;
        if (this.f1110i != null) {
            i = this.f1110i.hashCode();
        }
        return (((((((((((((((hashCode * 31) + hashCode2) * 31) + hashCode3) * 31) + i2) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + i3) * 31) + i;
    }

    /* renamed from: i */
    final List<String> m921i() {
        return this.f1110i;
    }

    /* renamed from: j */
    final void m922j() {
        if (this.f1102a == Type.POST) {
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(this.f1103b), "Can not create query with null feed");
        } else {
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(this.f1104c), "Can not create query with null activityId");
        }
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(this.f1106e), "Filter should not be null, choose one of Enum values.");
        jjbQypPegg.m1512a(this.f1105d > 0, "Limit should be greater that zero");
        if (this.f1108g != null) {
            cjrhisSQCL.m2482a(this.f1108g);
        }
    }

    public final String toString() {
        return "ActivitiesQuery{_type=" + this.f1102a + ", _feed='" + this.f1103b + '\'' + ", _parentActivityId='" + this.f1104c + '\'' + ", _limit=" + this.f1105d + ", _filter=" + this.f1106e + ", _filteringActivityId='" + this.f1107f + '\'' + ", _userId='" + this.f1108g + '\'' + ", _isFriendsFeed=" + this.f1109h + ", _tags=" + this.f1110i + '}';
    }

    public final ActivitiesQuery withFilter(Filter filter, String str) {
        this.f1106e = filter;
        this.f1107f = str;
        return this;
    }

    public final ActivitiesQuery withLimit(int i) {
        this.f1105d = i;
        return this;
    }

    public final ActivitiesQuery withTags(String... strArr) {
        this.f1110i = Arrays.asList(strArr);
        return this;
    }
}
