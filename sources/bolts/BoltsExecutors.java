package bolts;

import io.fabric.sdk.android.services.common.AbstractSpiCall;
import java.util.Locale;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

final class BoltsExecutors {
    private static final BoltsExecutors INSTANCE = new BoltsExecutors();
    private final ExecutorService background;
    private final Executor immediate;
    private final ScheduledExecutorService scheduled;

    private static class ImmediateExecutor implements Executor {
        private static final int MAX_DEPTH = 15;
        private ThreadLocal<Integer> executionDepth;

        private ImmediateExecutor() {
            this.executionDepth = new ThreadLocal();
        }

        private int decrementDepth() {
            Integer num = (Integer) this.executionDepth.get();
            if (num == null) {
                num = Integer.valueOf(0);
            }
            int intValue = num.intValue() - 1;
            if (intValue == 0) {
                this.executionDepth.remove();
            } else {
                this.executionDepth.set(Integer.valueOf(intValue));
            }
            return intValue;
        }

        private int incrementDepth() {
            Integer num = (Integer) this.executionDepth.get();
            if (num == null) {
                num = Integer.valueOf(0);
            }
            int intValue = num.intValue() + 1;
            this.executionDepth.set(Integer.valueOf(intValue));
            return intValue;
        }

        public void execute(Runnable runnable) {
            if (incrementDepth() <= 15) {
                try {
                    runnable.run();
                } catch (Throwable th) {
                    decrementDepth();
                }
            } else {
                BoltsExecutors.background().execute(runnable);
            }
            decrementDepth();
        }
    }

    private BoltsExecutors() {
        this.background = !isAndroidRuntime() ? Executors.newCachedThreadPool() : AndroidExecutors.newCachedThreadPool();
        this.scheduled = Executors.newSingleThreadScheduledExecutor();
        this.immediate = new ImmediateExecutor();
    }

    public static ExecutorService background() {
        return INSTANCE.background;
    }

    static Executor immediate() {
        return INSTANCE.immediate;
    }

    private static boolean isAndroidRuntime() {
        String property = System.getProperty("java.runtime.name");
        return property == null ? false : property.toLowerCase(Locale.US).contains(AbstractSpiCall.ANDROID_CLIENT_TYPE);
    }

    static ScheduledExecutorService scheduled() {
        return INSTANCE.scheduled;
    }
}
