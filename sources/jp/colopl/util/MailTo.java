package p018jp.colopl.util;

import android.net.Uri;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: jp.colopl.util.MailTo */
public class MailTo {
    private static final String BODY = "body";

    /* renamed from: CC */
    private static final String f1193CC = "cc";
    public static final String MAILTO_SCHEME = "mailto:";
    private static final String SUBJECT = "subject";

    /* renamed from: TO */
    private static final String f1194TO = "to";
    private HashMap<String, String> mHeaders = new HashMap<>();

    private MailTo() {
    }

    public static boolean isMailTo(String str) {
        return str != null && str.startsWith(MAILTO_SCHEME);
    }

    public static MailTo parse(String str) throws RuntimeException {
        if (str == null) {
            throw new NullPointerException();
        } else if (!isMailTo(str)) {
            throw new RuntimeException("Not a mailto scheme");
        } else {
            Uri parse = Uri.parse(str.substring(MAILTO_SCHEME.length()));
            MailTo mailTo = new MailTo();
            String encodedQuery = parse.getEncodedQuery();
            if (encodedQuery != null) {
                for (String split : encodedQuery.split("&")) {
                    String[] split2 = split.split("=");
                    if (split2.length != 0) {
                        mailTo.mHeaders.put(Uri.decode(split2[0]).toLowerCase(), split2.length > 1 ? Uri.decode(split2[1]) : null);
                    }
                }
            }
            String path = parse.getPath();
            if (path != null) {
                String to = mailTo.getTo();
                if (to != null) {
                    path = path + ", " + to;
                }
                mailTo.mHeaders.put("to", path);
            }
            return mailTo;
        }
    }

    public String getBody() {
        return (String) this.mHeaders.get("body");
    }

    public String getCc() {
        return (String) this.mHeaders.get(f1193CC);
    }

    public Map<String, String> getHeaders() {
        return this.mHeaders;
    }

    public String getSubject() {
        return (String) this.mHeaders.get(SUBJECT);
    }

    public String getTo() {
        return (String) this.mHeaders.get("to");
    }

    public String toString() {
        StringBuilder sb = new StringBuilder(MAILTO_SCHEME);
        sb.append('?');
        for (Entry entry : this.mHeaders.entrySet()) {
            sb.append(Uri.encode((String) entry.getKey()));
            sb.append('=');
            sb.append(Uri.encode((String) entry.getValue()));
            sb.append('&');
        }
        return sb.toString();
    }
}
