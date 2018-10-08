package net.gogame.gowrap.support;

public final class ClassUtils {
    private ClassUtils() {
    }

    public static boolean hasClass(String str) {
        try {
            Class.forName(str);
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }
}
