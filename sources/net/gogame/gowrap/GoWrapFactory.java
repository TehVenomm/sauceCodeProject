package net.gogame.gowrap;

public class GoWrapFactory {
    private static GoWrap instance = null;

    public static synchronized GoWrap getInstance() {
        GoWrap goWrap;
        synchronized (GoWrapFactory.class) {
            if (instance == null) {
                try {
                    instance = (GoWrap) Class.forName("net.gogame.gowrap.GoWrapImpl").getField("INSTANCE").get(null);
                } catch (ClassNotFoundException e) {
                    throw new RuntimeException(e);
                } catch (NoSuchFieldException e2) {
                    throw new RuntimeException(e2);
                } catch (SecurityException e3) {
                    throw new RuntimeException(e3);
                } catch (IllegalAccessException e4) {
                    throw new RuntimeException(e4);
                }
            }
            goWrap = instance;
        }
        return goWrap;
    }
}
