package p017io.fabric.sdk.android;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Future;
import java.util.concurrent.atomic.AtomicBoolean;
import p017io.fabric.sdk.android.ActivityLifecycleManager.Callbacks;
import p017io.fabric.sdk.android.services.common.DataCollectionArbiter;
import p017io.fabric.sdk.android.services.common.IdManager;
import p017io.fabric.sdk.android.services.concurrency.DependsOn;
import p017io.fabric.sdk.android.services.concurrency.PriorityThreadPoolExecutor;
import p017io.fabric.sdk.android.services.concurrency.Task;
import p017io.fabric.sdk.android.services.concurrency.UnmetDependencyException;

/* renamed from: io.fabric.sdk.android.Fabric */
public class Fabric {
    static final String ANSWERS_KIT_IDENTIFIER = "com.crashlytics.sdk.android:answers";
    static final String CRASHLYTICS_KIT_IDENTIFIER = "com.crashlytics.sdk.android:crashlytics";
    static final boolean DEFAULT_DEBUGGABLE = false;
    static final Logger DEFAULT_LOGGER = new DefaultLogger();
    static final String ROOT_DIR = ".Fabric";
    public static final String TAG = "Fabric";
    static volatile Fabric singleton;
    private WeakReference<Activity> activity;
    private ActivityLifecycleManager activityLifecycleManager;
    private final Context context;
    final boolean debuggable;
    private final ExecutorService executorService;
    private final IdManager idManager;
    /* access modifiers changed from: private */
    public final InitializationCallback<Fabric> initializationCallback;
    /* access modifiers changed from: private */
    public AtomicBoolean initialized = new AtomicBoolean(false);
    private final InitializationCallback<?> kitInitializationCallback;
    private final Map<Class<? extends Kit>, Kit> kits;
    final Logger logger;
    private final Handler mainHandler;

    /* renamed from: io.fabric.sdk.android.Fabric$Builder */
    public static class Builder {
        private String appIdentifier;
        private String appInstallIdentifier;
        private final Context context;
        private boolean debuggable;
        private Handler handler;
        private InitializationCallback<Fabric> initializationCallback;
        private Kit[] kits;
        private Logger logger;
        private PriorityThreadPoolExecutor threadPoolExecutor;

        public Builder(Context context2) {
            if (context2 == null) {
                throw new IllegalArgumentException("Context must not be null.");
            }
            this.context = context2;
        }

        public Builder appIdentifier(String str) {
            if (str == null) {
                throw new IllegalArgumentException("appIdentifier must not be null.");
            } else if (this.appIdentifier != null) {
                throw new IllegalStateException("appIdentifier already set.");
            } else {
                this.appIdentifier = str;
                return this;
            }
        }

        public Builder appInstallIdentifier(String str) {
            if (str == null) {
                throw new IllegalArgumentException("appInstallIdentifier must not be null.");
            } else if (this.appInstallIdentifier != null) {
                throw new IllegalStateException("appInstallIdentifier already set.");
            } else {
                this.appInstallIdentifier = str;
                return this;
            }
        }

        public Fabric build() {
            if (this.threadPoolExecutor == null) {
                this.threadPoolExecutor = PriorityThreadPoolExecutor.create();
            }
            if (this.handler == null) {
                this.handler = new Handler(Looper.getMainLooper());
            }
            if (this.logger == null) {
                if (this.debuggable) {
                    this.logger = new DefaultLogger(3);
                } else {
                    this.logger = new DefaultLogger();
                }
            }
            if (this.appIdentifier == null) {
                this.appIdentifier = this.context.getPackageName();
            }
            if (this.initializationCallback == null) {
                this.initializationCallback = InitializationCallback.EMPTY;
            }
            Map access$000 = this.kits == null ? new HashMap() : Fabric.getKitMap(Arrays.asList(this.kits));
            Context applicationContext = this.context.getApplicationContext();
            return new Fabric(applicationContext, access$000, this.threadPoolExecutor, this.handler, this.logger, this.debuggable, this.initializationCallback, new IdManager(applicationContext, this.appIdentifier, this.appInstallIdentifier, access$000.values()), Fabric.extractActivity(this.context));
        }

        public Builder debuggable(boolean z) {
            this.debuggable = z;
            return this;
        }

        @Deprecated
        public Builder executorService(ExecutorService executorService) {
            return this;
        }

        @Deprecated
        public Builder handler(Handler handler2) {
            return this;
        }

        public Builder initializationCallback(InitializationCallback<Fabric> initializationCallback2) {
            if (initializationCallback2 == null) {
                throw new IllegalArgumentException("initializationCallback must not be null.");
            } else if (this.initializationCallback != null) {
                throw new IllegalStateException("initializationCallback already set.");
            } else {
                this.initializationCallback = initializationCallback2;
                return this;
            }
        }

        public Builder kits(Kit... kitArr) {
            Kit[] kitArr2;
            if (this.kits != null) {
                throw new IllegalStateException("Kits already set.");
            }
            if (!DataCollectionArbiter.getInstance(this.context).isDataCollectionEnabled()) {
                ArrayList arrayList = new ArrayList();
                boolean z = false;
                for (Kit kit : kitArr) {
                    String identifier = kit.getIdentifier();
                    char c = 65535;
                    switch (identifier.hashCode()) {
                        case 607220212:
                            if (identifier.equals(Fabric.ANSWERS_KIT_IDENTIFIER)) {
                                c = 1;
                                break;
                            }
                            break;
                        case 1830452504:
                            if (identifier.equals(Fabric.CRASHLYTICS_KIT_IDENTIFIER)) {
                                c = 0;
                                break;
                            }
                            break;
                    }
                    switch (c) {
                        case 0:
                        case 1:
                            arrayList.add(kit);
                            break;
                        default:
                            if (z) {
                                break;
                            } else {
                                Fabric.getLogger().mo20982w(Fabric.TAG, "Fabric will not initialize any kits when Firebase automatic data collection is disabled; to use Third-party kits with automatic data collection disabled, initialize these kits via non-Fabric means.");
                                z = true;
                                break;
                            }
                    }
                }
                kitArr2 = (Kit[]) arrayList.toArray(new Kit[0]);
            } else {
                kitArr2 = kitArr;
            }
            this.kits = kitArr2;
            return this;
        }

        public Builder logger(Logger logger2) {
            if (logger2 == null) {
                throw new IllegalArgumentException("Logger must not be null.");
            } else if (this.logger != null) {
                throw new IllegalStateException("Logger already set.");
            } else {
                this.logger = logger2;
                return this;
            }
        }

        public Builder threadPoolExecutor(PriorityThreadPoolExecutor priorityThreadPoolExecutor) {
            if (priorityThreadPoolExecutor == null) {
                throw new IllegalArgumentException("PriorityThreadPoolExecutor must not be null.");
            } else if (this.threadPoolExecutor != null) {
                throw new IllegalStateException("PriorityThreadPoolExecutor already set.");
            } else {
                this.threadPoolExecutor = priorityThreadPoolExecutor;
                return this;
            }
        }
    }

    Fabric(Context context2, Map<Class<? extends Kit>, Kit> map, PriorityThreadPoolExecutor priorityThreadPoolExecutor, Handler handler, Logger logger2, boolean z, InitializationCallback initializationCallback2, IdManager idManager2, Activity activity2) {
        this.context = context2;
        this.kits = map;
        this.executorService = priorityThreadPoolExecutor;
        this.mainHandler = handler;
        this.logger = logger2;
        this.debuggable = z;
        this.initializationCallback = initializationCallback2;
        this.kitInitializationCallback = createKitInitializationCallback(map.size());
        this.idManager = idManager2;
        setCurrentActivity(activity2);
    }

    private static void addToKitMap(Map<Class<? extends Kit>, Kit> map, Collection<? extends Kit> collection) {
        for (Kit kit : collection) {
            map.put(kit.getClass(), kit);
            if (kit instanceof KitGroup) {
                addToKitMap(map, ((KitGroup) kit).getKits());
            }
        }
    }

    /* access modifiers changed from: private */
    public static Activity extractActivity(Context context2) {
        if (context2 instanceof Activity) {
            return (Activity) context2;
        }
        return null;
    }

    public static <T extends Kit> T getKit(Class<T> cls) {
        return (Kit) singleton().kits.get(cls);
    }

    /* access modifiers changed from: private */
    public static Map<Class<? extends Kit>, Kit> getKitMap(Collection<? extends Kit> collection) {
        HashMap hashMap = new HashMap(collection.size());
        addToKitMap(hashMap, collection);
        return hashMap;
    }

    public static Logger getLogger() {
        return singleton == null ? DEFAULT_LOGGER : singleton.logger;
    }

    private void init() {
        this.activityLifecycleManager = new ActivityLifecycleManager(this.context);
        this.activityLifecycleManager.registerCallbacks(new Callbacks() {
            public void onActivityCreated(Activity activity, Bundle bundle) {
                Fabric.this.setCurrentActivity(activity);
            }

            public void onActivityResumed(Activity activity) {
                Fabric.this.setCurrentActivity(activity);
            }

            public void onActivityStarted(Activity activity) {
                Fabric.this.setCurrentActivity(activity);
            }
        });
        initializeKits(this.context);
    }

    public static boolean isDebuggable() {
        if (singleton == null) {
            return false;
        }
        return singleton.debuggable;
    }

    public static boolean isInitialized() {
        return singleton != null && singleton.initialized.get();
    }

    private static void setFabric(Fabric fabric) {
        singleton = fabric;
        fabric.init();
    }

    static Fabric singleton() {
        if (singleton != null) {
            return singleton;
        }
        throw new IllegalStateException("Must Initialize Fabric before using singleton()");
    }

    public static Fabric with(Context context2, Kit... kitArr) {
        if (singleton == null) {
            synchronized (Fabric.class) {
                try {
                    if (singleton == null) {
                        setFabric(new Builder(context2).kits(kitArr).build());
                    }
                } finally {
                    while (true) {
                        Class<Fabric> cls = Fabric.class;
                    }
                }
            }
        }
        return singleton;
    }

    public static Fabric with(Fabric fabric) {
        if (singleton == null) {
            synchronized (Fabric.class) {
                try {
                    if (singleton == null) {
                        setFabric(fabric);
                    }
                } finally {
                    while (true) {
                        Class<Fabric> cls = Fabric.class;
                    }
                }
            }
        }
        return singleton;
    }

    /* access modifiers changed from: 0000 */
    public void addAnnotatedDependencies(Map<Class<? extends Kit>, Kit> map, Kit kit) {
        Class[] value;
        DependsOn dependsOn = kit.dependsOnAnnotation;
        if (dependsOn != null) {
            for (Class cls : dependsOn.value()) {
                if (cls.isInterface()) {
                    for (Kit kit2 : map.values()) {
                        if (cls.isAssignableFrom(kit2.getClass())) {
                            kit.initializationTask.addDependency((Task) kit2.initializationTask);
                        }
                    }
                } else if (((Kit) map.get(cls)) == null) {
                    throw new UnmetDependencyException("Referenced Kit was null, does the kit exist?");
                } else {
                    kit.initializationTask.addDependency((Task) ((Kit) map.get(cls)).initializationTask);
                }
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public InitializationCallback<?> createKitInitializationCallback(final int i) {
        return new InitializationCallback() {
            final CountDownLatch kitInitializedLatch = new CountDownLatch(i);

            public void failure(Exception exc) {
                Fabric.this.initializationCallback.failure(exc);
            }

            public void success(Object obj) {
                this.kitInitializedLatch.countDown();
                if (this.kitInitializedLatch.getCount() == 0) {
                    Fabric.this.initialized.set(true);
                    Fabric.this.initializationCallback.success(Fabric.this);
                }
            }
        };
    }

    public ActivityLifecycleManager getActivityLifecycleManager() {
        return this.activityLifecycleManager;
    }

    public String getAppIdentifier() {
        return this.idManager.getAppIdentifier();
    }

    public String getAppInstallIdentifier() {
        return this.idManager.getAppInstallIdentifier();
    }

    public Activity getCurrentActivity() {
        if (this.activity != null) {
            return (Activity) this.activity.get();
        }
        return null;
    }

    public ExecutorService getExecutorService() {
        return this.executorService;
    }

    public String getIdentifier() {
        return "io.fabric.sdk.android:fabric";
    }

    public Collection<Kit> getKits() {
        return this.kits.values();
    }

    /* access modifiers changed from: 0000 */
    public Future<Map<String, KitInfo>> getKitsFinderFuture(Context context2) {
        return getExecutorService().submit(new FabricKitsFinder(context2.getPackageCodePath()));
    }

    public Handler getMainHandler() {
        return this.mainHandler;
    }

    public String getVersion() {
        return "1.4.8.32";
    }

    /* access modifiers changed from: 0000 */
    public void initializeKits(Context context2) {
        Future kitsFinderFuture = getKitsFinderFuture(context2);
        Collection kits2 = getKits();
        Onboarding onboarding = new Onboarding(kitsFinderFuture, kits2);
        ArrayList<Kit> arrayList = new ArrayList<>(kits2);
        Collections.sort(arrayList);
        onboarding.injectParameters(context2, this, InitializationCallback.EMPTY, this.idManager);
        for (Kit injectParameters : arrayList) {
            injectParameters.injectParameters(context2, this, this.kitInitializationCallback, this.idManager);
        }
        onboarding.initialize();
        StringBuilder sb = getLogger().isLoggable(TAG, 3) ? new StringBuilder("Initializing ").append(getIdentifier()).append(" [Version: ").append(getVersion()).append("], with the following kits:\n") : null;
        for (Kit kit : arrayList) {
            kit.initializationTask.addDependency((Task) onboarding.initializationTask);
            addAnnotatedDependencies(this.kits, kit);
            kit.initialize();
            if (sb != null) {
                sb.append(kit.getIdentifier()).append(" [Version: ").append(kit.getVersion()).append("]\n");
            }
        }
        if (sb != null) {
            getLogger().mo20969d(TAG, sb.toString());
        }
    }

    public Fabric setCurrentActivity(Activity activity2) {
        this.activity = new WeakReference<>(activity2);
        return this;
    }
}
