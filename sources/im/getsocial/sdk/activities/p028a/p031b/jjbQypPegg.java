package im.getsocial.sdk.activities.p028a.p031b;

import im.getsocial.sdk.activities.ActivitiesQuery;
import java.util.regex.Pattern;

/* renamed from: im.getsocial.sdk.activities.a.b.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final Pattern f1163a = Pattern.compile("^[a-zA-Z0-9_.-]*$");

    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static String m973a(String str) {
        if (ActivitiesQuery.GLOBAL_FEED.equals(str)) {
            return str;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Feed name can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(str.length() <= 64, "Feed name length can not be more than 64");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(f1163a.matcher(str).matches(), "Feed name should contain only letters, numbers, dots(.), dashes(-) and underscores(_).");
        return "s-" + str;
    }
}
