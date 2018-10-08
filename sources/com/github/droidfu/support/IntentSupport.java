package com.github.droidfu.support;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import java.io.File;
import java.util.Locale;

public class IntentSupport {
    public static final String MIME_TYPE_EMAIL = "message/rfc822";
    public static final String MIME_TYPE_TEXT = "text/*";

    public static boolean isIntentAvailable(Context context, Intent intent) {
        return !context.getPackageManager().queryIntentActivities(intent, 65536).isEmpty();
    }

    public static boolean isIntentAvailable(Context context, String str, Uri uri, String str2) {
        Intent intent = uri != null ? new Intent(str, uri) : new Intent(str);
        if (str2 != null) {
            intent.setType(str2);
        }
        return !context.getPackageManager().queryIntentActivities(intent, 65536).isEmpty();
    }

    public static boolean isIntentAvailable(Context context, String str, String str2) {
        Intent intent = new Intent(str);
        if (str2 != null) {
            intent.setType(str2);
        }
        return !context.getPackageManager().queryIntentActivities(intent, 65536).isEmpty();
    }

    public static Intent newCallNumberIntent(String str) {
        return new Intent("android.intent.action.CALL", Uri.parse("tel:" + str.replace(" ", "")));
    }

    public static Intent newDialNumberIntent(String str) {
        return new Intent("android.intent.action.DIAL", Uri.parse("tel:" + str.replace(" ", "")));
    }

    public static Intent newEmailIntent(Context context, String str, String str2, String str3) {
        Intent intent = new Intent("android.intent.action.SEND");
        intent.putExtra("android.intent.extra.EMAIL", new String[]{str});
        intent.putExtra("android.intent.extra.TEXT", str3);
        intent.putExtra("android.intent.extra.SUBJECT", str2);
        intent.setType(MIME_TYPE_EMAIL);
        return intent;
    }

    public static Intent newMapsIntent(String str, String str2) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("geo:0,0?q=");
        stringBuilder.append(Uri.encode(str));
        stringBuilder.append(Uri.encode("(" + str2 + ")"));
        stringBuilder.append("&hl=" + Locale.getDefault().getLanguage());
        return new Intent("android.intent.action.VIEW", Uri.parse(stringBuilder.toString()));
    }

    public static Intent newSelectPictureIntent() {
        Intent intent = new Intent("android.intent.action.PICK");
        intent.setType("image/*");
        return intent;
    }

    public static Intent newShareIntent(Context context, String str, String str2, String str3) {
        Intent intent = new Intent("android.intent.action.SEND");
        intent.putExtra("android.intent.extra.TEXT", str2);
        intent.putExtra("android.intent.extra.SUBJECT", str);
        intent.setType(MIME_TYPE_TEXT);
        return Intent.createChooser(intent, str3);
    }

    public static Intent newTakePictureIntent(File file) {
        Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
        intent.putExtra("output", Uri.fromFile(file));
        return intent;
    }
}
