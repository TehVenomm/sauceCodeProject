package net.gogame.gowrap.support;

import android.content.Context;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.util.DisplayMetrics;
import android.util.Log;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.core.Wrapper;

public class LocaleManager {
    private final Context context;
    private final List<LocaleDescriptor> supportedLocaleDescriptors;

    public LocaleManager(Context context) {
        this.context = context;
        String[] stringArray = context.getResources().getStringArray(C1426R.array.language_values);
        String[] stringArray2 = context.getResources().getStringArray(C1426R.array.languages);
        Map linkedHashMap = new LinkedHashMap();
        for (int i = 0; i < stringArray.length; i++) {
            String str = stringArray[i];
            String str2 = null;
            if (i < stringArray2.length) {
                str2 = stringArray2[i];
            }
            linkedHashMap.put(str, new LocaleDescriptor(str, str2));
        }
        List supportedLocales = Wrapper.INSTANCE.getSupportedLocales();
        if (supportedLocales == null) {
            supportedLocales = Arrays.asList(stringArray);
        }
        this.supportedLocaleDescriptors = new ArrayList();
        if (r0 != null) {
            for (String str3 : r0) {
                LocaleDescriptor localeDescriptor = (LocaleDescriptor) linkedHashMap.get(str3);
                if (localeDescriptor != null) {
                    this.supportedLocaleDescriptors.add(localeDescriptor);
                }
            }
        }
    }

    public List<LocaleDescriptor> getSupportedLocaleDescriptors() {
        return this.supportedLocaleDescriptors;
    }

    public LocaleDescriptor getSupportedLocaleDescriptorById(String str) {
        if (!(this.supportedLocaleDescriptors == null || str == null)) {
            for (LocaleDescriptor localeDescriptor : this.supportedLocaleDescriptors) {
                if (localeDescriptor != null && StringUtils.isEquals(localeDescriptor.getId(), str)) {
                    return localeDescriptor;
                }
            }
        }
        return null;
    }

    public LocaleDescriptor getSupportedLocaleDescriptorByIndex(int i) {
        if (this.supportedLocaleDescriptors == null || i < 0 || i >= this.supportedLocaleDescriptors.size()) {
            return null;
        }
        return (LocaleDescriptor) this.supportedLocaleDescriptors.get(i);
    }

    public int getSupportedLocaleDescriptorIndex(String str) {
        if (!(str == null || this.supportedLocaleDescriptors == null)) {
            for (int i = 0; i < this.supportedLocaleDescriptors.size(); i++) {
                LocaleDescriptor localeDescriptor = (LocaleDescriptor) this.supportedLocaleDescriptors.get(i);
                if (localeDescriptor != null && StringUtils.isEquals(localeDescriptor.getId(), str)) {
                    return i;
                }
            }
        }
        return -1;
    }

    public void setLocale(String str) {
        Locale locale;
        Wrapper.INSTANCE.setCurrentLocale(this.context, str);
        Wrapper.INSTANCE.readConfiguration(this.context);
        if (str.equals("default")) {
            locale = Locale.ENGLISH;
        } else {
            List arrayList = new ArrayList();
            int i = 0;
            while (true) {
                int indexOf = str.indexOf(95, i);
                if (indexOf == -1) {
                    break;
                }
                arrayList.add(str.substring(i, indexOf));
                i = indexOf + 1;
            }
            arrayList.add(str.substring(i));
            if (arrayList.size() >= 3) {
                locale = new Locale((String) arrayList.get(0), (String) arrayList.get(1), (String) arrayList.get(2));
            } else if (arrayList.size() == 2) {
                locale = new Locale((String) arrayList.get(0), (String) arrayList.get(1));
            } else if (arrayList.size() == 1) {
                locale = new Locale((String) arrayList.get(0));
            } else {
                locale = Locale.ENGLISH;
            }
        }
        Log.v(Constants.TAG, String.format("Locale set to %s / %s", new Object[]{str, locale}));
        Locale.setDefault(locale);
        Resources resources = this.context.getResources();
        DisplayMetrics displayMetrics = resources.getDisplayMetrics();
        Configuration configuration = resources.getConfiguration();
        configuration.locale = locale;
        resources.updateConfiguration(configuration, displayMetrics);
    }
}
