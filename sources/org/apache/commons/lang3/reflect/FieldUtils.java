package org.apache.commons.lang3.reflect;

import java.lang.annotation.Annotation;
import java.lang.reflect.AccessibleObject;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.List;
import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.Validate;

public class FieldUtils {
    public static Field getField(Class<?> cls, String str) {
        AccessibleObject field = getField(cls, str, false);
        MemberUtils.setAccessibleWorkaround(field);
        return field;
    }

    public static Field getField(Class<?> cls, String str, boolean z) {
        Field declaredField;
        Validate.isTrue(cls != null, "The class must not be null", new Object[0]);
        Validate.isTrue(StringUtils.isNotBlank(str), "The field name must not be blank/empty", new Object[0]);
        Class cls2 = cls;
        while (cls2 != null) {
            try {
                declaredField = cls2.getDeclaredField(str);
                if (Modifier.isPublic(declaredField.getModifiers())) {
                    break;
                } else if (z) {
                    declaredField.setAccessible(true);
                    break;
                } else {
                    cls2 = cls2.getSuperclass();
                }
            } catch (NoSuchFieldException e) {
            }
        }
        declaredField = null;
        for (Class cls22 : ClassUtils.getAllInterfaces(cls)) {
            Field field;
            try {
                field = cls22.getField(str);
                Validate.isTrue(declaredField == null, "Reference to field %s is ambiguous relative to %s; a matching field exists on two or more implemented interfaces.", str, cls);
            } catch (NoSuchFieldException e2) {
                field = declaredField;
            }
            declaredField = field;
        }
        return declaredField;
    }

    public static Field getDeclaredField(Class<?> cls, String str) {
        return getDeclaredField(cls, str, false);
    }

    public static Field getDeclaredField(Class<?> cls, String str, boolean z) {
        boolean z2 = true;
        if (cls == null) {
            z2 = false;
        }
        Validate.isTrue(z2, "The class must not be null", new Object[0]);
        Validate.isTrue(StringUtils.isNotBlank(str), "The field name must not be blank/empty", new Object[0]);
        try {
            Field declaredField = cls.getDeclaredField(str);
            if (MemberUtils.isAccessible(declaredField)) {
                return declaredField;
            }
            if (!z) {
                return null;
            }
            declaredField.setAccessible(true);
            return declaredField;
        } catch (NoSuchFieldException e) {
            return null;
        }
    }

    public static Field[] getAllFields(Class<?> cls) {
        List allFieldsList = getAllFieldsList(cls);
        return (Field[]) allFieldsList.toArray(new Field[allFieldsList.size()]);
    }

    public static List<Field> getAllFieldsList(Class<?> cls) {
        Validate.isTrue(cls != null, "The class must not be null", new Object[0]);
        List<Field> arrayList = new ArrayList();
        while (cls != null) {
            for (Object add : cls.getDeclaredFields()) {
                arrayList.add(add);
            }
            cls = cls.getSuperclass();
        }
        return arrayList;
    }

    public static Field[] getFieldsWithAnnotation(Class<?> cls, Class<? extends Annotation> cls2) {
        List fieldsListWithAnnotation = getFieldsListWithAnnotation(cls, cls2);
        return (Field[]) fieldsListWithAnnotation.toArray(new Field[fieldsListWithAnnotation.size()]);
    }

    public static List<Field> getFieldsListWithAnnotation(Class<?> cls, Class<? extends Annotation> cls2) {
        Validate.isTrue(cls2 != null, "The annotation class must not be null", new Object[0]);
        List<Field> allFieldsList = getAllFieldsList(cls);
        List<Field> arrayList = new ArrayList();
        for (Field field : allFieldsList) {
            if (field.getAnnotation(cls2) != null) {
                arrayList.add(field);
            }
        }
        return arrayList;
    }

    public static Object readStaticField(Field field) throws IllegalAccessException {
        return readStaticField(field, false);
    }

    public static Object readStaticField(Field field, boolean z) throws IllegalAccessException {
        boolean z2;
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "The field must not be null", new Object[0]);
        Validate.isTrue(Modifier.isStatic(field.getModifiers()), "The field '%s' is not static", field.getName());
        return readField(field, null, z);
    }

    public static Object readStaticField(Class<?> cls, String str) throws IllegalAccessException {
        return readStaticField(cls, str, false);
    }

    public static Object readStaticField(Class<?> cls, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field '%s' on %s", str, cls);
        return readStaticField(field, false);
    }

    public static Object readDeclaredStaticField(Class<?> cls, String str) throws IllegalAccessException {
        return readDeclaredStaticField(cls, str, false);
    }

    public static Object readDeclaredStaticField(Class<?> cls, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        return readStaticField(declaredField, false);
    }

    public static Object readField(Field field, Object obj) throws IllegalAccessException {
        return readField(field, obj, false);
    }

    public static Object readField(Field field, Object obj, boolean z) throws IllegalAccessException {
        Validate.isTrue(field != null, "The field must not be null", new Object[0]);
        if (!z || field.isAccessible()) {
            MemberUtils.setAccessibleWorkaround(field);
        } else {
            field.setAccessible(true);
        }
        return field.get(obj);
    }

    public static Object readField(Object obj, String str) throws IllegalAccessException {
        return readField(obj, str, false);
    }

    public static Object readField(Object obj, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Field field = getField(obj.getClass(), str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field %s on %s", str, r3);
        return readField(field, obj, false);
    }

    public static Object readDeclaredField(Object obj, String str) throws IllegalAccessException {
        return readDeclaredField(obj, str, false);
    }

    public static Object readDeclaredField(Object obj, String str, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Field declaredField = getDeclaredField(obj.getClass(), str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", r3, str);
        return readField(declaredField, obj, false);
    }

    public static void writeStaticField(Field field, Object obj) throws IllegalAccessException {
        writeStaticField(field, obj, false);
    }

    public static void writeStaticField(Field field, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "The field must not be null", new Object[0]);
        Validate.isTrue(Modifier.isStatic(field.getModifiers()), "The field %s.%s is not static", field.getDeclaringClass().getName(), field.getName());
        writeField(field, null, obj, z);
    }

    public static void writeStaticField(Class<?> cls, String str, Object obj) throws IllegalAccessException {
        writeStaticField(cls, str, obj, false);
    }

    public static void writeStaticField(Class<?> cls, String str, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        Field field = getField(cls, str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate field %s on %s", str, cls);
        writeStaticField(field, obj, false);
    }

    public static void writeDeclaredStaticField(Class<?> cls, String str, Object obj) throws IllegalAccessException {
        writeDeclaredStaticField(cls, str, obj, false);
    }

    public static void writeDeclaredStaticField(Class<?> cls, String str, Object obj, boolean z) throws IllegalAccessException {
        boolean z2;
        Field declaredField = getDeclaredField(cls, str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", cls.getName(), str);
        writeField(declaredField, null, obj, false);
    }

    public static void writeField(Field field, Object obj, Object obj2) throws IllegalAccessException {
        writeField(field, obj, obj2, false);
    }

    public static void writeField(Field field, Object obj, Object obj2, boolean z) throws IllegalAccessException {
        Validate.isTrue(field != null, "The field must not be null", new Object[0]);
        if (!z || field.isAccessible()) {
            MemberUtils.setAccessibleWorkaround(field);
        } else {
            field.setAccessible(true);
        }
        field.set(obj, obj2);
    }

    public static void removeFinalModifier(Field field) {
        removeFinalModifier(field, true);
    }

    public static void removeFinalModifier(Field field, boolean z) {
        int i = 1;
        Validate.isTrue(field != null, "The field must not be null", new Object[0]);
        Field declaredField;
        try {
            if (Modifier.isFinal(field.getModifiers())) {
                declaredField = Field.class.getDeclaredField("modifiers");
                if (!z || declaredField.isAccessible()) {
                    i = 0;
                }
                if (i != 0) {
                    declaredField.setAccessible(true);
                }
                declaredField.setInt(field, field.getModifiers() & -17);
                if (i != 0) {
                    declaredField.setAccessible(false);
                }
            }
        } catch (NoSuchFieldException e) {
        } catch (IllegalAccessException e2) {
        } catch (Throwable th) {
            if (i != 0) {
                declaredField.setAccessible(false);
            }
        }
    }

    public static void writeField(Object obj, String str, Object obj2) throws IllegalAccessException {
        writeField(obj, str, obj2, false);
    }

    public static void writeField(Object obj, String str, Object obj2, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Field field = getField(obj.getClass(), str, z);
        if (field != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", r3.getName(), str);
        writeField(field, obj, obj2, false);
    }

    public static void writeDeclaredField(Object obj, String str, Object obj2) throws IllegalAccessException {
        writeDeclaredField(obj, str, obj2, false);
    }

    public static void writeDeclaredField(Object obj, String str, Object obj2, boolean z) throws IllegalAccessException {
        boolean z2;
        Validate.isTrue(obj != null, "target object must not be null", new Object[0]);
        Field declaredField = getDeclaredField(obj.getClass(), str, z);
        if (declaredField != null) {
            z2 = true;
        } else {
            z2 = false;
        }
        Validate.isTrue(z2, "Cannot locate declared field %s.%s", r3.getName(), str);
        writeField(declaredField, obj, obj2, false);
    }
}
