package net.gogame.gowrap;

public class GoWrapFactory {
    private static GoWrap instance = null;

    public static synchronized GoWrap getInstance() {
        GoWrap goWrap;
        synchronized (GoWrapFactory.class) {
            if (instance == null) {
                try {
                    instance = (GoWrap) Class.forName("net.gogame.gowrap.GoWrapImpl").getField("INSTANCE").get(null);
                } catch (Throwable e) {
                    throw new RuntimeException(e);
                } catch (Throwable e2) {
                    throw new RuntimeException(e2);
                } catch (Throwable e22) {
                    throw new RuntimeException(e22);
                } catch (Throwable e222) {
                    throw new RuntimeException(e222);
                }
            }
            goWrap = instance;
        }
        return goWrap;
    }
}
