package net.gogame.gowrap.integrations;

import java.util.HashMap;
import java.util.Map;

public class Config {
    private final Map<String, Object> valueMap = new HashMap();

    public String getString(String str) {
        return getString(str, null);
    }

    public String getString(String str, String str2) {
        try {
            Object obj = this.valueMap.get(str);
            if (obj == null) {
                return str2;
            }
            return (String) obj;
        } catch (Exception e) {
            return str2;
        }
    }

    public void putString(String str, Object obj) {
        this.valueMap.put(str, obj);
    }

    public boolean getBoolean(String str, boolean z) {
        try {
            Object obj = this.valueMap.get(str);
            if (obj == null) {
                return z;
            }
            return ((Boolean) obj).booleanValue();
        } catch (Exception e) {
            return z;
        }
    }

    public void putBoolean(String str, boolean z) {
        this.valueMap.put(str, Boolean.valueOf(z));
    }

    public int getInt(String str, int i) {
        try {
            Object obj = this.valueMap.get(str);
            if (obj == null) {
                return i;
            }
            return ((Integer) obj).intValue();
        } catch (Exception e) {
            return i;
        }
    }

    public void putInt(String str, int i) {
        this.valueMap.put(str, Integer.valueOf(i));
    }
}
