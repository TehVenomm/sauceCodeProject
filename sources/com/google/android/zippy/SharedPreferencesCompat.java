package com.google.android.zippy;

import android.content.SharedPreferences.Editor;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

public class SharedPreferencesCompat {
    private static final Method sApplyMethod = findApplyMethod();

    public static void apply(Editor editor) {
        if (sApplyMethod != null) {
            try {
                sApplyMethod.invoke(editor, new Object[0]);
                return;
            } catch (IllegalAccessException | InvocationTargetException e) {
            }
        }
        editor.commit();
    }

    private static Method findApplyMethod() {
        try {
            return Editor.class.getMethod("apply", new Class[0]);
        } catch (NoSuchMethodException e) {
            return null;
        }
    }
}
