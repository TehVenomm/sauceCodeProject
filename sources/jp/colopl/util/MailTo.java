package jp.colopl.util;

import android.net.Uri;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

public class MailTo {
    private static final String BODY = "body";
    private static final String CC = "cc";
    public static final String MAILTO_SCHEME = "mailto:";
    private static final String SUBJECT = "subject";
    private static final String TO = "to";
    private HashMap<String, String> mHeaders = new HashMap();

    private MailTo() {
    }

    public static boolean isMailTo(String str) {
        return str != null && str.startsWith(MAILTO_SCHEME);
    }

    public static MailTo parse(String str) throws RuntimeException {
        if (str == null) {
            throw new NullPointerException();
        } else if (isMailTo(str)) {
            Uri parse = Uri.parse(str.substring(MAILTO_SCHEME.length()));
            MailTo mailTo = new MailTo();
            String encodedQuery = parse.getEncodedQuery();
            if (encodedQuery != null) {
                for (String encodedQuery2 : encodedQuery2.split("&")) {
                    String[] split = encodedQuery2.split("=");
                    if (split.length != 0) {
                        mailTo.mHeaders.put(Uri.decode(split[0]).toLowerCase(), split.length > 1 ? Uri.decode(split[1]) : null);
                    }
                }
            }
            Object path = parse.getPath();
            if (path != null) {
                String to = mailTo.getTo();
                if (to != null) {
                    path = path + ", " + to;
                }
                mailTo.mHeaders.put("to", path);
            }
            return mailTo;
        } else {
            throw new RuntimeException("Not a mailto scheme");
        }
    }

    public String getBody() {
        return (String) this.mHeaders.get("body");
    }

    public String getCc() {
        return (String) this.mHeaders.get(CC);
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
        StringBuilder stringBuilder = new StringBuilder(MAILTO_SCHEME);
        stringBuilder.append('?');
        for (Entry entry : this.mHeaders.entrySet()) {
            stringBuilder.append(Uri.encode((String) entry.getKey()));
            stringBuilder.append('=');
            stringBuilder.append(Uri.encode((String) entry.getValue()));
            stringBuilder.append('&');
        }
        return stringBuilder.toString();
    }
}
