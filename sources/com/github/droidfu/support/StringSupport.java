package com.github.droidfu.support;

import android.text.TextUtils;
import java.util.ArrayList;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class StringSupport {
    private static String[] splitByCharacterType(String str, boolean z) {
        int i = 0;
        if (str == null) {
            return null;
        }
        if (str.length() == 0) {
            return new String[0];
        }
        char[] charArray = str.toCharArray();
        ArrayList arrayList = new ArrayList();
        int type = Character.getType(charArray[0]);
        for (int i2 = 1; i2 < charArray.length; i2++) {
            int type2 = Character.getType(charArray[i2]);
            if (type2 != type) {
                if (z && type2 == 2 && type == 1) {
                    int i3 = i2 - 1;
                    if (i3 != i) {
                        arrayList.add(new String(charArray, i, i3 - i));
                        i = i3;
                    }
                } else {
                    arrayList.add(new String(charArray, i, i2 - i));
                    i = i2;
                }
                type = type2;
            }
        }
        arrayList.add(new String(charArray, i, charArray.length - i));
        return (String[]) arrayList.toArray(new String[arrayList.size()]);
    }

    public static String[] splitByCharacterTypeCamelCase(String str) {
        return splitByCharacterType(str, true);
    }

    public static String underscore(String str) {
        return TextUtils.join(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR, splitByCharacterTypeCamelCase(str)).toLowerCase();
    }
}
