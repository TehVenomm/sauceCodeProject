package com.github.droidfu.testsupport;

import java.util.Calendar;
import java.util.Collection;
import java.util.Date;
import org.junit.Assert;

public class DroidFuAssertions {
    private static boolean junit4;

    static {
        junit4 = true;
        try {
            Class.forName("org.junit.Assert");
        } catch (ClassNotFoundException e) {
            System.out.println("JUnit4 not found on class path, will use JUnit3 assertions!");
            junit4 = false;
        }
    }

    public static void assertDateEquals(Date date, Date date2) {
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        Calendar instance2 = Calendar.getInstance();
        instance2.setTime(date2);
        int i = instance.get(1);
        int i2 = instance2.get(1);
        assertEquals("expected year to be " + i + ", but was " + i2, Integer.valueOf(i), Integer.valueOf(i2));
        i = instance.get(2);
        i2 = instance2.get(2);
        assertEquals("expected month to be " + i + ", but was " + i2, Integer.valueOf(i), Integer.valueOf(i2));
        int i3 = instance.get(5);
        int i4 = instance2.get(5);
        assertEquals("expected day to be " + i3 + ", but was " + i4, Integer.valueOf(i3), Integer.valueOf(i4));
    }

    public static <E> void assertEqualElements(Collection<E> collection, Collection<E> collection2) {
        collection.removeAll(collection2);
        assertTrue("collections expected to contain the same elements, but didn't", collection.isEmpty());
    }

    private static void assertEquals(String str, Object obj, Object obj2) {
        if (junit4) {
            Assert.assertEquals(str, obj, obj2);
        } else {
            junit.framework.Assert.assertEquals(str, obj, obj2);
        }
    }

    public static void assertTimeEquals(Date date, Date date2) {
        assertDateEquals(date, date2);
        Calendar instance = Calendar.getInstance();
        instance.setTime(date);
        Calendar instance2 = Calendar.getInstance();
        instance2.setTime(date2);
        int i = instance.get(11);
        int i2 = instance2.get(11);
        assertEquals("expected hour to be " + i + ", but was " + i2, Integer.valueOf(i), Integer.valueOf(i2));
        i = instance.get(12);
        i2 = instance2.get(12);
        assertEquals("expected minute to be " + i + ", but was " + i2, Integer.valueOf(i), Integer.valueOf(i2));
        int i3 = instance.get(13);
        int i4 = instance2.get(13);
        assertEquals("expected second to be " + i3 + ", but was " + i4, Integer.valueOf(i3), Integer.valueOf(i4));
    }

    private static void assertTrue(String str, boolean z) {
        if (junit4) {
            Assert.assertTrue(str, z);
        } else {
            junit.framework.Assert.assertTrue(str, z);
        }
    }

    public static void useJUnit3() {
        junit4 = false;
    }
}
