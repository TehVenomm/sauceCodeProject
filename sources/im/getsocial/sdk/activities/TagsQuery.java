package im.getsocial.sdk.activities;

public final class TagsQuery {
    /* renamed from: a */
    private String f1149a;
    /* renamed from: b */
    private String f1150b = "";
    /* renamed from: c */
    private int f1151c = 5;

    private TagsQuery(String str) {
        this.f1149a = str;
    }

    public static TagsQuery tagsForFeed(String str) {
        return new TagsQuery(str);
    }

    public static TagsQuery tagsForGlobalFeed() {
        return tagsForFeed(ActivitiesQuery.GLOBAL_FEED);
    }

    /* renamed from: a */
    final void m951a(String str) {
        this.f1149a = str;
    }

    public final String getFeedName() {
        return this.f1149a;
    }

    public final int getLimit() {
        return this.f1151c;
    }

    public final String getQuery() {
        return this.f1150b;
    }

    public final TagsQuery withLimit(int i) {
        this.f1151c = i;
        return this;
    }

    public final TagsQuery withName(String str) {
        this.f1150b = str;
        return this;
    }
}
