package com.facebook.appevents.codeless.internal;

import android.text.method.PasswordTransformationMethod;
import android.util.Patterns;
import android.view.View;
import android.widget.TextView;

public class SensitiveUserDataUtils {
    private static boolean isCreditCard(TextView textView) {
        String replaceAll = ViewHierarchy.getTextOfView(textView).replaceAll("\\s", "");
        int length = replaceAll.length();
        if (length < 12 || length > 19) {
            return false;
        }
        int i = 0;
        boolean z = false;
        int i2 = length - 1;
        while (i2 >= 0) {
            char charAt = replaceAll.charAt(i2);
            if (charAt < '0' || charAt > '9') {
                return false;
            }
            int i3 = charAt - '0';
            if (z) {
                i3 *= 2;
                if (i3 > 9) {
                    i3 = (i3 % 10) + 1;
                }
            }
            i += i3;
            i2--;
            z = !z;
        }
        return i % 10 == 0;
    }

    private static boolean isEmail(TextView textView) {
        if (textView.getInputType() == 32) {
            return true;
        }
        String textOfView = ViewHierarchy.getTextOfView(textView);
        if (textOfView == null || textOfView.length() == 0) {
            return false;
        }
        return Patterns.EMAIL_ADDRESS.matcher(textOfView).matches();
    }

    private static boolean isPassword(TextView textView) {
        if (textView.getInputType() == 128) {
            return true;
        }
        return textView.getTransformationMethod() instanceof PasswordTransformationMethod;
    }

    private static boolean isPersonName(TextView textView) {
        return textView.getInputType() == 96;
    }

    private static boolean isPhoneNumber(TextView textView) {
        return textView.getInputType() == 3;
    }

    private static boolean isPostalAddress(TextView textView) {
        return textView.getInputType() == 112;
    }

    public static boolean isSensitiveUserData(View view) {
        if (!(view instanceof TextView)) {
            return false;
        }
        TextView textView = (TextView) view;
        return isPassword(textView) || isCreditCard(textView) || isPersonName(textView) || isPostalAddress(textView) || isPhoneNumber(textView) || isEmail(textView);
    }
}
