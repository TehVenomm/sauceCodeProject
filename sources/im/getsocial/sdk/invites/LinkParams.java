package im.getsocial.sdk.invites;

import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

public class LinkParams extends HashMap<String, Object> {
    public static final String KEY_CUSTOM_DESCRIPTION = "$description";
    public static final String KEY_CUSTOM_IMAGE = "$image";
    public static final String KEY_CUSTOM_TITLE = "$title";
    public static final String KEY_CUSTOM_YOUTUBE_VIDEO = "$youtube_video";
    /* renamed from: a */
    private static final List<String> f2282a = Arrays.asList(new String[]{KEY_CUSTOM_TITLE, KEY_CUSTOM_DESCRIPTION, KEY_CUSTOM_IMAGE, KEY_CUSTOM_YOUTUBE_VIDEO});

    public LinkParams(Map<String, Object> map) {
        super(map);
    }

    public static String validateLinkParams(LinkParams linkParams) {
        for (String str : linkParams.keySet()) {
            if (str.charAt(0) == '$' && !f2282a.contains(str)) {
                return str;
            }
        }
        return null;
    }

    public Map<String, String> getStringValues() {
        Map<String, String> hashMap = new HashMap();
        for (Entry entry : entrySet()) {
            Object value = entry.getValue();
            if (value instanceof String) {
                hashMap.put(entry.getKey(), (String) value);
            }
        }
        return hashMap;
    }
}
