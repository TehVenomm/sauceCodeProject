.class public Lorg/onepf/oms/OpenIabHelper;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/OpenIabHelper$Options;,
        Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;,
        Lorg/onepf/oms/OpenIabHelper$OnOpenIabHelperInitFinished;,
        Lorg/onepf/oms/OpenIabHelper$OnInitListener;,
        Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;
    }
.end annotation


# static fields
.field public static final BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE:I = 0x3

.field public static final BILLING_RESPONSE_RESULT_ERROR:I = 0x6

.field public static final BILLING_RESPONSE_RESULT_OK:I = 0x0

.field private static final BIND_INTENT:Ljava/lang/String; = "org.onepf.oms.openappstore.BIND"

.field public static final ITEM_TYPE_INAPP:Ljava/lang/String; = "inapp"

.field public static final ITEM_TYPE_SUBS:Ljava/lang/String; = "subs"

.field public static final NAME_AMAZON:Ljava/lang/String; = "com.amazon.apps"

.field public static final NAME_APPLAND:Ljava/lang/String; = "Appland"

.field public static final NAME_APTOIDE:Ljava/lang/String; = "cm.aptoide.pt"

.field public static final NAME_FORTUMO:Ljava/lang/String; = "com.fortumo.billing"

.field public static final NAME_GOOGLE:Ljava/lang/String; = "com.google.play"

.field public static final NAME_NOKIA:Ljava/lang/String; = "com.nokia.nstore"

.field public static final NAME_SAMSUNG:Ljava/lang/String; = "com.samsung.apps"

.field public static final NAME_SKUBIT:Ljava/lang/String; = "com.skubit.android"

.field public static final NAME_SKUBIT_TEST:Ljava/lang/String; = "net.skubit.android"

.field public static final NAME_SLIDEME:Ljava/lang/String; = "SlideME"

.field public static final NAME_YANDEX:Ljava/lang/String; = "com.yandex.store"

.field public static final SETUP_DISPOSED:I = 0x2

.field public static final SETUP_IN_PROGRESS:I = 0x3

.field public static final SETUP_RESULT_FAILED:I = 0x1

.field public static final SETUP_RESULT_NOT_STARTED:I = -0x1

.field public static final SETUP_RESULT_SUCCESSFUL:I


# instance fields
.field private activity:Landroid/app/Activity;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private volatile appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private final appStoreFactoryMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;",
            ">;"
        }
    .end annotation
.end field

.field private volatile appStoreInSetup:Lorg/onepf/oms/Appstore;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private final appStorePackageMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private volatile appstore:Lorg/onepf/oms/Appstore;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private final availableAppstores:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation
.end field

.field private final context:Landroid/content/Context;

.field private final handler:Landroid/os/Handler;

.field private final inventoryExecutor:Ljava/util/concurrent/ExecutorService;
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation
.end field

.field private final options:Lorg/onepf/oms/OpenIabHelper$Options;

.field private final packageManager:Landroid/content/pm/PackageManager;

.field private setupExecutorService:Ljava/util/concurrent/ExecutorService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private volatile setupState:I


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/util/Map;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 383
    new-instance v0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v0}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    invoke-virtual {v0, p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addStoreKeys(Ljava/util/Map;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/util/Map;[Ljava/lang/String;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;[",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 399
    new-instance v0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v0}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    invoke-virtual {v0, p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addStoreKeys(Ljava/util/Map;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2, p3}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addPreferredStoreName([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/util/Map;[Ljava/lang/String;[Lorg/onepf/oms/Appstore;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Map;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;[",
            "Ljava/lang/String;",
            "[",
            "Lorg/onepf/oms/Appstore;",
            ")V"
        }
    .end annotation

    .line 417
    new-instance v0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v0}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    invoke-virtual {v0, p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addStoreKeys(Ljava/util/Map;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2, p3}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addPreferredStoreName([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2, p4}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStores([Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V
    .locals 3
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 433
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, -0x1

    .line 112
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    .line 214
    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->handler:Landroid/os/Handler;

    .line 228
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    .line 233
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->inventoryExecutor:Ljava/util/concurrent/ExecutorService;

    .line 242
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    .line 243
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    .line 247
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.yandex.store"

    const-string v2, "com.yandex.store"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 248
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "cm.aptoide.pt"

    const-string v2, "cm.aptoide.pt"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 251
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.fortumo.billing"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$1;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$1;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 259
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.android.vending"

    const-string v2, "com.google.play"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 260
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.google.play"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$2;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$2;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 271
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.amazon.venezia"

    const-string v2, "com.amazon.apps"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 272
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.amazon.apps"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$3;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$3;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 280
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.sec.android.app.samsungapps"

    const-string v2, "com.samsung.apps"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 281
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.samsung.apps"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$4;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$4;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 289
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.nokia.payment.iapenabler"

    const-string v2, "com.nokia.nstore"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 290
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.nokia.nstore"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$5;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$5;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 298
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "com.skubit.android"

    const-string v2, "com.skubit.android"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 299
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.skubit.android"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$6;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$6;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 307
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    const-string v1, "net.skubit.android"

    const-string v2, "net.skubit.android"

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 308
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "net.skubit.android"

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$7;

    invoke-direct {v2, p0}, Lorg/onepf/oms/OpenIabHelper$7;-><init>(Lorg/onepf/oms/OpenIabHelper;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 434
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    .line 435
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->packageManager:Landroid/content/pm/PackageManager;

    .line 436
    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    .line 437
    instance-of p2, p1, Landroid/app/Activity;

    if-eqz p2, :cond_0

    .line 438
    check-cast p1, Landroid/app/Activity;

    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    .line 441
    :cond_0
    invoke-virtual {p0}, Lorg/onepf/oms/OpenIabHelper;->checkOptions()V

    return-void
.end method

.method static synthetic access$000(Lorg/onepf/oms/OpenIabHelper;)Landroid/content/Context;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    return-object p0
.end method

.method static synthetic access$100(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper$Options;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    return-object p0
.end method

.method static synthetic access$1000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    return-object p0
.end method

.method static synthetic access$1100(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    return-object p0
.end method

.method static synthetic access$1200(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V

    return-void
.end method

.method static synthetic access$1302(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/Appstore;
    .locals 0

    .line 83
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreInSetup:Lorg/onepf/oms/Appstore;

    return-object p1
.end method

.method static synthetic access$1400(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Z
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->versionOk(Lorg/onepf/oms/Appstore;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$1500(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Set;)Lorg/onepf/oms/Appstore;
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->checkInventory(Ljava/util/Set;)Lorg/onepf/oms/Appstore;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$1600(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Collection;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->dispose(Ljava/util/Collection;)V

    return-void
.end method

.method static synthetic access$1700(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    return-void
.end method

.method static synthetic access$1800(Lorg/onepf/oms/OpenIabHelper;)Landroid/os/Handler;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->handler:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic access$1900(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2, p3}, Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V

    return-void
.end method

.method static synthetic access$200(Lorg/onepf/oms/OpenIabHelper;)Landroid/app/Activity;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    return-object p0
.end method

.method static synthetic access$2000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/concurrent/ExecutorService;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->inventoryExecutor:Ljava/util/concurrent/ExecutorService;

    return-object p0
.end method

.method static synthetic access$2100(Lorg/onepf/oms/OpenIabHelper;)I
    .locals 0

    .line 83
    iget p0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    return p0
.end method

.method static synthetic access$300(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Set;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    return-object p0
.end method

.method static synthetic access$400(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/Appstore;
    .locals 0

    .line 83
    iget-object p0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    return-object p0
.end method

.method static synthetic access$500(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setupWithStrategy(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method

.method static synthetic access$600(Lorg/onepf/oms/OpenIabHelper;Landroid/content/ComponentName;Landroid/os/IBinder;Landroid/content/ServiceConnection;)Lorg/onepf/oms/appstore/OpenAppstore;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 83
    invoke-direct {p0, p1, p2, p3}, Lorg/onepf/oms/OpenIabHelper;->getOpenAppstore(Landroid/content/ComponentName;Landroid/os/IBinder;Landroid/content/ServiceConnection;)Lorg/onepf/oms/appstore/OpenAppstore;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$700(Lorg/onepf/oms/OpenIabHelper;Ljava/lang/String;)Lorg/onepf/oms/Appstore;
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$800(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method

.method static synthetic access$900(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    return-void
.end method

.method private checkAmazon()V
    .locals 6

    .line 1151
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    const-string v0, "checkAmazon() Android Lollipop not supported, ignoring amazon wrapper."

    .line 1152
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1153
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.amazon.apps"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    :cond_0
    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 1159
    :try_start_0
    const-class v2, Lorg/onepf/oms/OpenIabHelper;

    invoke-virtual {v2}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v2

    const-string v3, "com.amazon.device.iap.PurchasingService"

    .line 1160
    invoke-virtual {v2, v3}, Ljava/lang/ClassLoader;->loadClass(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v2, 0x1

    goto :goto_0

    :catch_0
    const/4 v2, 0x0

    :goto_0
    const/4 v3, 0x2

    .line 1164
    new-array v4, v3, [Ljava/lang/Object;

    const-string v5, "checkAmazon() amazon sdk available: "

    aput-object v5, v4, v1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v5

    aput-object v5, v4, v0

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v2, :cond_1

    return-void

    .line 1169
    :cond_1
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const-string v4, "com.amazon.apps"

    invoke-virtual {v2, v4}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v2

    if-nez v2, :cond_3

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v2

    const-string v4, "com.amazon.apps"

    invoke-interface {v2, v4}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_3

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getPreferredStoreNames()Ljava/util/Set;

    move-result-object v2

    const-string v4, "com.amazon.apps"

    invoke-interface {v2, v4}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    goto :goto_1

    :cond_2
    const/4 v2, 0x0

    goto :goto_2

    :cond_3
    :goto_1
    const/4 v2, 0x1

    .line 1172
    :goto_2
    new-array v3, v3, [Ljava/lang/Object;

    const-string v4, "checkAmazon() amazon billing required: "

    aput-object v4, v3, v1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    aput-object v1, v3, v0

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-nez v2, :cond_4

    const-string v0, "checkAmazon() ignoring amazon wrapper."

    .line 1176
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1177
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.amazon.apps"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    .line 1174
    :cond_4
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must satisfy amazon sdk dependency."

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Collection;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;",
            "Ljava/util/Collection<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 759
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    const/4 v1, 0x3

    if-ne v0, v1, :cond_2

    .line 763
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 764
    invoke-interface {p2}, Ljava/util/Collection;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 765
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void

    .line 770
    :cond_0
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->isCheckInventory()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 771
    new-instance v1, Lorg/onepf/oms/OpenIabHelper$11;

    invoke-direct {v1, p0, p2, v0, p1}, Lorg/onepf/oms/OpenIabHelper$11;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Collection;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_0

    .line 809
    :cond_1
    new-instance v1, Lorg/onepf/oms/OpenIabHelper$12;

    invoke-direct {v1, p0, p2, v0, p1}, Lorg/onepf/oms/OpenIabHelper$12;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Collection;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    .line 847
    :goto_0
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper;->setupExecutorService:Ljava/util/concurrent/ExecutorService;

    invoke-interface {p1, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    return-void

    .line 760
    :cond_2
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Can\'t check billing. Current state: "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->setupStateToString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/Appstore;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    if-nez p2, :cond_0

    .line 751
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x1

    .line 753
    new-array v0, v0, [Lorg/onepf/oms/Appstore;

    const/4 v1, 0x0

    aput-object p2, v0, v1

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V

    :goto_0
    return-void
.end method

.method private checkFortumo()V
    .locals 6

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 1129
    :try_start_0
    const-class v2, Lorg/onepf/oms/OpenIabHelper;

    invoke-virtual {v2}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v2

    const-string v3, "mp.PaymentRequest"

    .line 1130
    invoke-virtual {v2, v3}, Ljava/lang/ClassLoader;->loadClass(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v2, 0x1

    goto :goto_0

    :catch_0
    const/4 v2, 0x0

    :goto_0
    const/4 v3, 0x2

    .line 1133
    new-array v4, v3, [Ljava/lang/Object;

    const-string v5, "checkFortumo() fortumo sdk available: "

    aput-object v5, v4, v1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v5

    aput-object v5, v4, v0

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v2, :cond_0

    return-void

    .line 1138
    :cond_0
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const-string v4, "com.fortumo.billing"

    invoke-virtual {v2, v4}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v2

    if-nez v2, :cond_2

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v2

    const-string v4, "com.fortumo.billing"

    invoke-interface {v2, v4}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getPreferredStoreNames()Ljava/util/Set;

    move-result-object v2

    const-string v4, "com.fortumo.billing"

    invoke-interface {v2, v4}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    goto :goto_2

    :cond_2
    :goto_1
    const/4 v2, 0x1

    .line 1141
    :goto_2
    new-array v3, v3, [Ljava/lang/Object;

    const-string v4, "checkFortumo() fortumo billing required: "

    aput-object v4, v3, v1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    aput-object v1, v3, v0

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-nez v2, :cond_3

    const-string v0, "checkFortumo() ignoring fortumo wrapper."

    .line 1145
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1146
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.fortumo.billing"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    .line 1143
    :cond_3
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must satisfy fortumo sdk dependency."

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private checkGoogle()V
    .locals 5

    .line 1105
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "checkGoogle() verify mode = "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getVerifyMode()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1106
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getVerifyMode()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    .line 1110
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getStoreKeys()Ljava/util/Map;

    move-result-object v0

    const-string v2, "com.google.play"

    invoke-interface {v0, v2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    const/4 v2, 0x2

    .line 1111
    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "checkGoogle() google key available = "

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    aput-object v3, v2, v1

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v0, :cond_1

    return-void

    .line 1116
    :cond_1
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const-string v2, "com.google.play"

    invoke-virtual {v0, v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v0

    if-nez v0, :cond_3

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v2, "com.google.play"

    invoke-interface {v0, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_3

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getPreferredStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v2, "com.google.play"

    invoke-interface {v0, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    goto :goto_0

    :cond_2
    const/4 v1, 0x0

    :cond_3
    :goto_0
    if-eqz v1, :cond_5

    .line 1119
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getVerifyMode()I

    move-result v0

    if-eqz v0, :cond_4

    goto :goto_1

    .line 1120
    :cond_4
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must supply Google verification key"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_5
    :goto_1
    const-string v0, "checkGoogle() ignoring GooglePlay wrapper."

    .line 1122
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1123
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.google.play"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method private checkInventory(Ljava/util/Set;)Lorg/onepf/oms/Appstore;
    .locals 12
    .param p1    # Ljava/util/Set;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Lorg/onepf/oms/Appstore;",
            ">;)",
            "Lorg/onepf/oms/Appstore;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 1190
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-nez v0, :cond_2

    .line 1194
    new-instance v0, Ljava/util/concurrent/Semaphore;

    const/4 v7, 0x0

    invoke-direct {v0, v7}, Ljava/util/concurrent/Semaphore;-><init>(I)V

    const/4 v1, 0x1

    .line 1196
    new-array v8, v1, [Lorg/onepf/oms/Appstore;

    .line 1198
    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    const/4 v9, 0x0

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    move-object v6, v1

    check-cast v6, Lorg/onepf/oms/Appstore;

    .line 1199
    invoke-interface {v6}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v10

    .line 1200
    new-instance v11, Lorg/onepf/oms/OpenIabHelper$15;

    move-object v1, v11

    move-object v2, p0

    move-object v3, v0

    move-object v4, v10

    move-object v5, v8

    invoke-direct/range {v1 .. v6}, Lorg/onepf/oms/OpenIabHelper$15;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/concurrent/Semaphore;Lorg/onepf/oms/AppstoreInAppBillingService;[Lorg/onepf/oms/Appstore;Lorg/onepf/oms/Appstore;)V

    .line 1229
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->handler:Landroid/os/Handler;

    new-instance v2, Lorg/onepf/oms/OpenIabHelper$16;

    invoke-direct {v2, p0, v10, v11}, Lorg/onepf/oms/OpenIabHelper$16;-><init>(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/AppstoreInAppBillingService;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    invoke-virtual {v1, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    .line 1237
    :try_start_0
    invoke-virtual {v0}, Ljava/util/concurrent/Semaphore;->acquire()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1242
    aget-object v1, v8, v7

    if-eqz v1, :cond_0

    .line 1243
    aget-object p1, v8, v7

    return-object p1

    :catch_0
    move-exception p1

    const-string v0, "checkInventory() Error during inventory check: "

    .line 1239
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-object v9

    :cond_1
    return-object v9

    .line 1191
    :cond_2
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Must not be called from UI thread"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private checkNokia()V
    .locals 4

    .line 1069
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    const-string v1, "com.nokia.payment.BILLING"

    invoke-static {v0, v1}, Lorg/onepf/oms/util/Utils;->hasRequestedPermission(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x2

    .line 1070
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "checkNokia() has permission = "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    const/4 v3, 0x1

    aput-object v2, v1, v3

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v0, :cond_0

    return-void

    .line 1074
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const-string v1, "com.nokia.nstore"

    invoke-virtual {v0, v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v1, "com.nokia.nstore"

    invoke-interface {v0, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getPreferredStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v1, "com.nokia.nstore"

    invoke-interface {v0, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "checkNokia() ignoring Nokia wrapper"

    .line 1080
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1081
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.nokia.nstore"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    .line 1077
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Nokia permission \"com.nokia.payment.BILLING\" NOT REQUESTED"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private checkSamsung()V
    .locals 3

    const/4 v0, 0x2

    .line 1085
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "checkSamsung() activity = "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 1086
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    if-eqz v0, :cond_0

    return-void

    .line 1089
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const-string v1, "com.samsung.apps"

    invoke-virtual {v0, v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v1, "com.samsung.apps"

    invoke-interface {v0, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getPreferredStoreNames()Ljava/util/Set;

    move-result-object v0

    const-string v1, "com.samsung.apps"

    invoke-interface {v0, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string v0, "checkSamsung() ignoring Samsung wrapper"

    .line 1100
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1101
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    const-string v1, "com.samsung.apps"

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    .line 1098
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "You must supply Activity object as context in order to use com.samsung.apps store"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static discoverOpenStores(Landroid/content/Context;Ljava/util/List;Lorg/onepf/oms/OpenIabHelper$Options;)Ljava/util/List;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;",
            "Lorg/onepf/oms/OpenIabHelper$Options;",
            ")",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1028
    new-instance p0, Ljava/lang/UnsupportedOperationException;

    const-string p1, "This action is no longer supported."

    invoke-direct {p0, p1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method private discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V
    .locals 4
    .param p1    # Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/util/Queue;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;",
            "Ljava/util/Queue<",
            "Landroid/content/Intent;",
            ">;",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 987
    :goto_0
    invoke-interface {p2}, Ljava/util/Queue;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_1

    .line 988
    invoke-interface {p2}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Intent;

    .line 989
    new-instance v1, Lorg/onepf/oms/OpenIabHelper$14;

    invoke-direct {v1, p0, p3, p1, p2}, Lorg/onepf/oms/OpenIabHelper$14;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;)V

    .line 1009
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    const/4 v3, 0x1

    invoke-virtual {v2, v0, v1, v3}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v2

    if-eqz v2, :cond_0

    return-void

    .line 1014
    :cond_0
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {v2, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    .line 1015
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "discoverOpenStores() Couldn\'t connect to open store: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    goto :goto_0

    .line 1019
    :cond_1
    invoke-static {p3}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p2

    invoke-interface {p1, p2}, Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;->openStoresDiscovered(Ljava/util/List;)V

    return-void
.end method

.method private dispose(Ljava/util/Collection;)V
    .locals 4
    .param p1    # Ljava/util/Collection;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 851
    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/Appstore;

    .line 852
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v1

    .line 853
    invoke-interface {v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    const/4 v1, 0x2

    .line 854
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "dispose() was called for "

    aput-object v3, v1, v2

    const/4 v2, 0x1

    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public static enableDebugLogging(Z)V
    .locals 1

    const/4 v0, 0x0

    .line 1562
    invoke-static {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->enableDebuglLogging(ZLjava/lang/String;)V

    return-void
.end method

.method public static enableDebuglLogging(ZLjava/lang/String;)V
    .locals 0

    .line 1571
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->setLogTag(Ljava/lang/String;)V

    .line 1572
    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->setLoggable(Z)V

    return-void
.end method

.method private finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 881
    invoke-direct {p0, p1, v0}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    return-void
.end method

.method private finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/Appstore;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    if-nez p2, :cond_0

    .line 886
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x3

    const-string v2, "No suitable appstore was found"

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_0

    :cond_0
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x0

    const-string v2, "Setup ok"

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 889
    :goto_0
    invoke-direct {p0, p1, v0, p2}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/Appstore;)V

    return-void
.end method

.method private finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/Appstore;)V
    .locals 6
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/Appstore;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 895
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v0, 0x0

    .line 898
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    .line 899
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreInSetup:Lorg/onepf/oms/Appstore;

    .line 900
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->setupExecutorService:Ljava/util/concurrent/ExecutorService;

    invoke-interface {v1}, Ljava/util/concurrent/ExecutorService;->shutdownNow()Ljava/util/List;

    .line 901
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupExecutorService:Ljava/util/concurrent/ExecutorService;

    .line 902
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    const/4 v1, 0x0

    const/4 v2, 0x2

    const/4 v3, 0x1

    if-ne v0, v2, :cond_1

    if-eqz p3, :cond_0

    .line 904
    new-array p1, v3, [Lorg/onepf/oms/Appstore;

    aput-object p3, p1, v1

    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->dispose(Ljava/util/Collection;)V

    :cond_0
    return-void

    .line 907
    :cond_1
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    const/4 v4, 0x3

    if-ne v0, v4, :cond_4

    .line 910
    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isSuccess()Z

    move-result v0

    xor-int/lit8 v5, v0, 0x1

    .line 911
    iput v5, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-eqz v0, :cond_3

    if-eqz p3, :cond_2

    .line 916
    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    .line 917
    invoke-interface {p3}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    goto :goto_0

    .line 914
    :cond_2
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Appstore can\'t be null if setup is successful"

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_3
    :goto_0
    const/4 v0, 0x4

    .line 919
    new-array v0, v0, [Ljava/lang/Object;

    const-string v5, "finishSetup() === SETUP DONE === result: "

    aput-object v5, v0, v1

    aput-object p2, v0, v3

    const-string v1, " Appstore: "

    aput-object v1, v0, v2

    aput-object p3, v0, v4

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->dWithTimeFromUp([Ljava/lang/Object;)V

    .line 920
    invoke-interface {p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    return-void

    .line 908
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Setup is not started or already finished."

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 896
    :cond_5
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Must be called from UI thread."

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method private finishSetupWithError(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 871
    invoke-direct {p0, p1, v0}, Lorg/onepf/oms/OpenIabHelper;->finishSetupWithError(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/Exception;)V

    return-void
.end method

.method private finishSetupWithError(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/Exception;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/Exception;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x2

    .line 876
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "finishSetupWithError() error occurred during setup"

    const/4 v2, 0x0

    aput-object v1, v0, v2

    if-nez p2, :cond_0

    const-string p2, ""

    goto :goto_0

    :cond_0
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, " : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    :goto_0
    const/4 v1, 0x1

    aput-object p2, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 877
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v0, 0x6

    const-string v1, "Error occured, setup failed"

    invoke-direct {p2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/Appstore;)V

    return-void
.end method

.method public static getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;
    .locals 1
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 369
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {v0, p0}, Lorg/onepf/oms/SkuManager;->getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object p0

    if-nez p0, :cond_0

    .line 371
    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object p0

    goto :goto_0

    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0, p0}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    move-object p0, v0

    :goto_0
    return-object p0
.end method

.method private getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 926
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/Appstore;

    .line 927
    invoke-interface {v1}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    return-object v1

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method private getBindServiceIntent(Landroid/content/pm/ServiceInfo;)Landroid/content/Intent;
    .locals 2
    .param p1    # Landroid/content/pm/ServiceInfo;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 743
    new-instance v0, Landroid/content/Intent;

    const-string v1, "org.onepf.oms.openappstore.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 744
    iget-object v1, p1, Landroid/content/pm/ServiceInfo;->packageName:Ljava/lang/String;

    iget-object p1, p1, Landroid/content/pm/ServiceInfo;->name:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->setClassName(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method

.method private getOpenAppstore(Landroid/content/ComponentName;Landroid/os/IBinder;Landroid/content/ServiceConnection;)Lorg/onepf/oms/appstore/OpenAppstore;
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 715
    invoke-static {p2}, Lorg/onepf/oms/IOpenAppstore$Stub;->asInterface(Landroid/os/IBinder;)Lorg/onepf/oms/IOpenAppstore;

    move-result-object v3

    .line 716
    invoke-interface {v3}, Lorg/onepf/oms/IOpenAppstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    .line 717
    invoke-interface {v3}, Lorg/onepf/oms/IOpenAppstore;->getBillingServiceIntent()Landroid/content/Intent;

    move-result-object v4

    .line 718
    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {p2}, Lorg/onepf/oms/OpenIabHelper$Options;->getVerifyMode()I

    move-result p2

    const/4 v0, 0x0

    const/4 v7, 0x1

    if-ne p2, v7, :cond_0

    move-object v5, v0

    goto :goto_0

    .line 719
    :cond_0
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getStoreKeys()Ljava/util/Map;

    move-result-object v1

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    move-object v5, v1

    .line 723
    :goto_0
    invoke-static {v2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    const/4 v8, 0x0

    const/4 v9, 0x2

    if-eqz v1, :cond_1

    .line 724
    new-array p2, v9, [Ljava/lang/Object;

    const-string p3, "getOpenAppstore() Appstore doesn\'t have name. Skipped. ComponentName: "

    aput-object p3, p2, v8

    aput-object p1, p2, v7

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    goto :goto_1

    :cond_1
    if-nez v4, :cond_2

    .line 726
    new-array p2, v9, [Ljava/lang/Object;

    const-string p3, "getOpenAppstore() billing is not supported by store: "

    aput-object p3, p2, v8

    aput-object p1, p2, v7

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    goto :goto_1

    :cond_2
    if-nez p2, :cond_3

    .line 727
    invoke-static {v5}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p2

    if-eqz p2, :cond_3

    .line 729
    new-array p2, v9, [Ljava/lang/Object;

    const-string p3, "getOpenAppstore() verification is required but publicKey is not provided: "

    aput-object p3, p2, v8

    aput-object p1, p2, v7

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    :goto_1
    return-object v0

    .line 731
    :cond_3
    new-instance p2, Lorg/onepf/oms/appstore/OpenAppstore;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    move-object v0, p2

    move-object v6, p3

    invoke-direct/range {v0 .. v6}, Lorg/onepf/oms/appstore/OpenAppstore;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/IOpenAppstore;Landroid/content/Intent;Ljava/lang/String;Landroid/content/ServiceConnection;)V

    .line 733
    iput-object p1, p2, Lorg/onepf/oms/appstore/OpenAppstore;->componentName:Landroid/content/ComponentName;

    .line 734
    new-array p1, v9, [Ljava/lang/Object;

    const-string p3, "getOpenAppstore() returns "

    aput-object p3, p1, v8

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/OpenAppstore;->getAppstoreName()Ljava/lang/String;

    move-result-object p3

    aput-object p3, p1, v7

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-object p2
.end method

.method public static getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 359
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {v0, p0, p1}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 348
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {v0, p0, p1}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static isDebugLog()Z
    .locals 1

    .line 1553
    invoke-static {}, Lorg/onepf/oms/util/Logger;->isLoggable()Z

    move-result v0

    return v0
.end method

.method public static mapSku(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 333
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {v0, p0, p1, p2}, Lorg/onepf/oms/SkuManager;->mapSku(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lorg/onepf/oms/SkuManager;

    return-void
.end method

.method private queryOpenStoreServices()Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Landroid/content/pm/ServiceInfo;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 1034
    new-instance v0, Landroid/content/Intent;

    const-string v1, "org.onepf.oms.openappstore.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 1035
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const/4 v2, 0x0

    .line 1036
    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v0

    .line 1037
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1038
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/pm/ResolveInfo;

    .line 1039
    iget-object v2, v2, Landroid/content/pm/ResolveInfo;->serviceInfo:Landroid/content/pm/ServiceInfo;

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1041
    :cond_0
    invoke-static {v1}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method private setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 659
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    .line 661
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v1

    .line 662
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v2}, Ljava/util/Set;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Set;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    goto :goto_0

    .line 674
    :cond_0
    new-instance v2, Lorg/onepf/oms/OpenIabHelper$10;

    invoke-direct {v2, p0, v1, v0, p1}, Lorg/onepf/oms/OpenIabHelper$10;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Set;Ljava/util/Set;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    invoke-virtual {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;)V

    goto :goto_2

    .line 664
    :cond_1
    :goto_0
    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_2
    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 666
    invoke-direct {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v2

    if-eqz v2, :cond_2

    .line 668
    invoke-interface {v0, v2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 671
    :cond_3
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v0, v1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 672
    invoke-direct {p0, p1, v0}, Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V

    :goto_2
    return-void
.end method

.method private setupForPackage(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/String;Z)V
    .locals 4
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 558
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-static {v0, p2}, Lorg/onepf/oms/util/Utils;->packageInstalled(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    if-eqz p3, :cond_0

    .line 562
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_0

    .line 564
    :cond_0
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :goto_0
    return-void

    .line 570
    :cond_1
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    invoke-interface {v0, p2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_4

    .line 572
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStorePackageMap:Ljava/util/Map;

    invoke-interface {v0, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 573
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v2}, Ljava/util/Set;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_2

    .line 574
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    invoke-interface {v2, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_4

    .line 575
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    invoke-interface {v2, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;

    invoke-interface {v0}, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;->get()Lorg/onepf/oms/Appstore;

    move-result-object v0

    goto :goto_2

    .line 579
    :cond_2
    invoke-direct {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->getAvailableStoreByName(Ljava/lang/String;)Lorg/onepf/oms/Appstore;

    move-result-object v0

    if-nez v0, :cond_5

    if-eqz p3, :cond_3

    .line 583
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_1

    .line 585
    :cond_3
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :goto_1
    return-void

    :cond_4
    move-object v0, v1

    :cond_5
    :goto_2
    if-eqz v0, :cond_6

    .line 594
    invoke-direct {p0, p1, v0}, Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    return-void

    .line 600
    :cond_6
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->queryOpenStoreServices()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_7
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_8

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/pm/ServiceInfo;

    .line 601
    iget-object v3, v2, Landroid/content/pm/ServiceInfo;->packageName:Ljava/lang/String;

    invoke-static {v3, p2}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v3

    if-eqz v3, :cond_7

    .line 602
    invoke-direct {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->getBindServiceIntent(Landroid/content/pm/ServiceInfo;)Landroid/content/Intent;

    move-result-object v1

    :cond_8
    if-nez v1, :cond_a

    if-eqz p3, :cond_9

    .line 610
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_3

    .line 612
    :cond_9
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :goto_3
    return-void

    .line 617
    :cond_a
    iget-object p2, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    new-instance v0, Lorg/onepf/oms/OpenIabHelper$9;

    invoke-direct {v0, p0, p3, p1}, Lorg/onepf/oms/OpenIabHelper$9;-><init>(Lorg/onepf/oms/OpenIabHelper;ZLorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    const/4 v2, 0x1

    invoke-virtual {p2, v1, v0, v2}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result p2

    if-nez p2, :cond_c

    const-string p2, "setupForPackage() Error binding to open store service"

    .line 648
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    if-eqz p3, :cond_b

    .line 650
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_4

    .line 652
    :cond_b
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetupWithError(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :cond_c
    :goto_4
    return-void
.end method

.method private static setupStateToString(I)Ljava/lang/String;
    .locals 3

    const/4 v0, -0x1

    if-ne p0, v0, :cond_0

    const-string p0, " IAB helper is not set up."

    goto :goto_0

    :cond_0
    const/4 v0, 0x2

    if-ne p0, v0, :cond_1

    const-string p0, "IAB helper was disposed of."

    goto :goto_0

    :cond_1
    if-nez p0, :cond_2

    const-string p0, "IAB helper is set up."

    goto :goto_0

    :cond_2
    const/4 v0, 0x1

    if-ne p0, v0, :cond_3

    const-string p0, "IAB helper setup failed."

    goto :goto_0

    :cond_3
    const/4 v0, 0x3

    if-ne p0, v0, :cond_4

    const-string p0, "IAB helper setup is in progress."

    :goto_0
    return-object p0

    .line 1534
    :cond_4
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Wrong setup state: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private setupWithStrategy(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 7
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 524
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getStoreSearchStrategy()I

    move-result v0

    const/4 v1, 0x2

    .line 525
    new-array v2, v1, [Ljava/lang/Object;

    const-string v3, "setupWithStrategy() store search strategy = "

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v5, 0x1

    aput-object v3, v2, v5

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 526
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    .line 527
    new-array v3, v1, [Ljava/lang/Object;

    const-string v6, "setupWithStrategy() package name = "

    aput-object v6, v3, v4

    aput-object v2, v3, v5

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 528
    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper;->packageManager:Landroid/content/pm/PackageManager;

    invoke-virtual {v3, v2}, Landroid/content/pm/PackageManager;->getInstallerPackageName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 529
    new-array v3, v1, [Ljava/lang/Object;

    const-string v6, "setupWithStrategy() package installer = "

    aput-object v6, v3, v4

    aput-object v2, v3, v5

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 530
    invoke-static {v2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    xor-int/2addr v3, v5

    if-nez v0, :cond_1

    if-eqz v3, :cond_0

    .line 536
    invoke-direct {p0, p1, v2, v4}, Lorg/onepf/oms/OpenIabHelper;->setupForPackage(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/String;Z)V

    goto :goto_0

    .line 539
    :cond_0
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->finishSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_0

    :cond_1
    if-ne v0, v1, :cond_3

    if-eqz v3, :cond_2

    .line 545
    invoke-direct {p0, p1, v2, v5}, Lorg/onepf/oms/OpenIabHelper;->setupForPackage(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/lang/String;Z)V

    goto :goto_0

    .line 548
    :cond_2
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    goto :goto_0

    .line 551
    :cond_3
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :goto_0
    return-void
.end method

.method private versionOk(Lorg/onepf/oms/Appstore;)Z
    .locals 2
    .param p1    # Lorg/onepf/oms/Appstore;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 859
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    .line 862
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p1

    iget p1, p1, Landroid/content/pm/PackageInfo;->versionCode:I
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    const/4 p1, 0x1

    return p1
.end method


# virtual methods
.method public checkOptions()V
    .locals 3

    const/4 v0, 0x2

    .line 1060
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "checkOptions() "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 1061
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->checkGoogle()V

    .line 1062
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->checkSamsung()V

    .line 1063
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->checkNokia()V

    .line 1064
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->checkFortumo()V

    .line 1065
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->checkAmazon()V

    return-void
.end method

.method checkSetupDone(Ljava/lang/String;)V
    .locals 4

    .line 1514
    invoke-virtual {p0}, Lorg/onepf/oms/OpenIabHelper;->setupSuccessful()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 1515
    :cond_0
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->setupStateToString(I)Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x4

    .line 1516
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "Illegal state for operation ("

    aput-object v3, v1, v2

    const/4 v2, 0x1

    aput-object p1, v1, v2

    const/4 v2, 0x2

    const-string v3, "): "

    aput-object v3, v1, v2

    const/4 v2, 0x3

    aput-object v0, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 1517
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " Can\'t perform operation: "

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 4
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .line 1444
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    .line 1445
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 1446
    iget v2, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-nez v2, :cond_1

    if-eqz v0, :cond_1

    if-nez v1, :cond_0

    goto :goto_0

    .line 1451
    :cond_0
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->clone()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    .line 1452
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v3

    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v3, v0, p1}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 1453
    invoke-interface {v1, v2}, Lorg/onepf/oms/AppstoreInAppBillingService;->consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    return-void

    :cond_1
    :goto_0
    return-void
.end method

.method public consumeAsync(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V
    .locals 1
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    if-eqz p2, :cond_0

    const/4 v0, 0x0

    .line 1467
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/OpenIabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    return-void

    .line 1465
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Consume listener must be not null!"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public consumeAsync(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    .line 1458
    new-array v0, v0, [Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, v0}, Lorg/onepf/oms/OpenIabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    return-void
.end method

.method consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V
    .locals 2
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    const-string v0, "consume"

    .line 1473
    invoke-virtual {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 1474
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 1477
    new-instance v0, Ljava/lang/Thread;

    new-instance v1, Lorg/onepf/oms/OpenIabHelper$18;

    invoke-direct {v1, p0, p1, p2, p3}, Lorg/onepf/oms/OpenIabHelper$18;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    invoke-direct {v0, v1}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    return-void

    .line 1475
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Nothing to consume."

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public discoverOpenStores()Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;"
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 948
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-nez v0, :cond_0

    .line 952
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 953
    new-instance v1, Ljava/util/concurrent/CountDownLatch;

    const/4 v2, 0x1

    invoke-direct {v1, v2}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 954
    new-instance v2, Lorg/onepf/oms/OpenIabHelper$13;

    invoke-direct {v2, p0, v0, v1}, Lorg/onepf/oms/OpenIabHelper$13;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Ljava/util/concurrent/CountDownLatch;)V

    invoke-virtual {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;)V

    .line 962
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    const/4 v0, 0x0

    return-object v0

    .line 949
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Must not be called from UI thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 975
    invoke-direct {p0}, Lorg/onepf/oms/OpenIabHelper;->queryOpenStoreServices()Ljava/util/List;

    move-result-object v0

    .line 976
    new-instance v1, Ljava/util/LinkedList;

    invoke-direct {v1}, Ljava/util/LinkedList;-><init>()V

    .line 977
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/pm/ServiceInfo;

    .line 978
    invoke-direct {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->getBindServiceIntent(Landroid/content/pm/ServiceInfo;)Landroid/content/Intent;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/Queue;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 981
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {p0, p1, v1, v0}, Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;Ljava/util/Queue;Ljava/util/List;)V

    return-void
.end method

.method public dispose()V
    .locals 1

    const-string v0, "Disposing."

    .line 1251
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 1252
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    if-eqz v0, :cond_0

    .line 1253
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    invoke-interface {v0}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    :cond_0
    const/4 v0, 0x0

    .line 1255
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    .line 1256
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 1257
    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->activity:Landroid/app/Activity;

    const/4 v0, 0x2

    .line 1258
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    return-void
.end method

.method public getConnectedAppstoreName()Ljava/lang/String;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 1052
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 1053
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getSetupState()I
    .locals 1

    .line 937
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    return v0
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 8

    const/4 v0, 0x6

    .line 1300
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "handleActivityResult() requestCode: "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v4, 0x1

    aput-object v2, v1, v4

    const-string v2, " resultCode: "

    const/4 v5, 0x2

    aput-object v2, v1, v5

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    const/4 v6, 0x3

    aput-object v2, v1, v6

    const-string v2, " data: "

    const/4 v7, 0x4

    aput-object v2, v1, v7

    const/4 v2, 0x5

    aput-object p3, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->dWithTimeFromUp([Ljava/lang/Object;)V

    .line 1301
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    iget v1, v1, Lorg/onepf/oms/OpenIabHelper$Options;->samsungCertificationRequestCode:I

    if-ne p1, v1, :cond_0

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreInSetup:Lorg/onepf/oms/Appstore;

    if-eqz v1, :cond_0

    .line 1302
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreInSetup:Lorg/onepf/oms/Appstore;

    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    invoke-interface {v0, p1, p2, p3}, Lorg/onepf/oms/AppstoreInAppBillingService;->handleActivityResult(IILandroid/content/Intent;)Z

    move-result p1

    return p1

    .line 1304
    :cond_0
    iget v1, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-eqz v1, :cond_1

    .line 1305
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "handleActivityResult() setup is not done. requestCode: "

    aput-object v1, v0, v3

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v4

    const-string p1, " resultCode: "

    aput-object p1, v0, v5

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v6

    const-string p1, " data: "

    aput-object p1, v0, v7

    aput-object p3, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return v3

    .line 1308
    :cond_1
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    invoke-interface {v0, p1, p2, p3}, Lorg/onepf/oms/AppstoreInAppBillingService;->handleActivityResult(IILandroid/content/Intent;)Z

    move-result p1

    return p1
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;)V
    .locals 6
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v5, ""

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v3, p3

    move-object v4, p4

    .line 1270
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/OpenIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 7
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v3, "inapp"

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v4, p3

    move-object v5, p4

    move-object v6, p5

    .line 1275
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/OpenIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 8
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "launchPurchaseFlow"

    .line 1290
    invoke-virtual {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 1291
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v2}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2, p2}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    move-object v2, p1

    move-object v4, p3

    move v5, p4

    move-object v6, p5

    move-object v7, p6

    invoke-interface/range {v1 .. v7}, Lorg/onepf/oms/AppstoreInAppBillingService;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;)V
    .locals 6
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v5, ""

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v3, p3

    move-object v4, p4

    .line 1280
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/OpenIabHelper;->launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 7
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v3, "subs"

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v4, p3

    move-object v5, p4

    move-object v6, p5

    .line 1285
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/OpenIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public queryInventory(ZLjava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 1
    .param p2    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    const/4 v0, 0x0

    .line 1319
    invoke-virtual {p0, p1, p2, v0}, Lorg/onepf/oms/OpenIabHelper;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object p1

    return-object p1
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 7
    .param p2    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 1340
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-nez v0, :cond_5

    .line 1343
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appstore:Lorg/onepf/oms/Appstore;

    .line 1344
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 1345
    iget v2, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    const/4 v3, 0x0

    if-nez v2, :cond_4

    if-eqz v0, :cond_4

    if-nez v1, :cond_0

    goto :goto_2

    .line 1352
    :cond_0
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    if-eqz p2, :cond_1

    .line 1354
    new-instance v4, Ljava/util/ArrayList;

    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v5

    invoke-direct {v4, v5}, Ljava/util/ArrayList;-><init>(I)V

    .line 1355
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_2

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    .line 1356
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v2, v6, v5}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-interface {v4, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    move-object v4, v3

    :cond_2
    if-eqz p3, :cond_3

    .line 1364
    new-instance v3, Ljava/util/ArrayList;

    invoke-interface {p3}, Ljava/util/List;->size()I

    move-result p2

    invoke-direct {v3, p2}, Ljava/util/ArrayList;-><init>(I)V

    .line 1365
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result p3

    if-eqz p3, :cond_3

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Ljava/lang/String;

    .line 1366
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v5, p3}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    invoke-interface {v3, p3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 1371
    :cond_3
    invoke-interface {v1, p1, v4, v3}, Lorg/onepf/oms/AppstoreInAppBillingService;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object p1

    return-object p1

    :cond_4
    :goto_2
    return-object v3

    .line 1341
    :cond_5
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Must not be called from the UI thread"

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public queryInventoryAsync(Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    .line 1378
    invoke-virtual {p0, v0, p1}, Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(ZLorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public queryInventoryAsync(ZLjava/util/List;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 8
    .param p2    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p4    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;",
            ")V"
        }
    .end annotation

    const-string v0, "queryInventory"

    .line 1413
    invoke-virtual {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->checkSetupDone(Ljava/lang/String;)V

    if-eqz p4, :cond_0

    .line 1418
    new-instance v0, Ljava/lang/Thread;

    new-instance v7, Lorg/onepf/oms/OpenIabHelper$17;

    move-object v1, v7

    move-object v2, p0

    move v3, p1

    move-object v4, p2

    move-object v5, p3

    move-object v6, p4

    invoke-direct/range {v1 .. v6}, Lorg/onepf/oms/OpenIabHelper$17;-><init>(Lorg/onepf/oms/OpenIabHelper;ZLjava/util/List;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    invoke-direct {v0, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    return-void

    .line 1416
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "Inventory listener must be not null"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 1
    .param p2    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;",
            ")V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 1395
    invoke-virtual {p0, p1, p2, v0, p3}, Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(ZLjava/util/List;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public queryInventoryAsync(ZLorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 1
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 1386
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public setupSuccessful()Z
    .locals 1

    .line 1544
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 6
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 452
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    const/4 v0, 0x2

    .line 453
    new-array v0, v0, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "startSetup() options = "

    aput-object v3, v0, v2

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    aput-object v2, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    :cond_0
    if-eqz p1, :cond_7

    .line 459
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    const/4 v2, -0x1

    if-eq v0, v2, :cond_2

    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-ne v0, v1, :cond_1

    goto :goto_0

    .line 460
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Couldn\'t be set up. Current state: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->setupStateToString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_2
    :goto_0
    const/4 v0, 0x3

    .line 462
    iput v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    .line 463
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupExecutorService:Ljava/util/concurrent/ExecutorService;

    .line 466
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->clear()V

    .line 468
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStores()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 469
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 471
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/Appstore;

    .line 472
    invoke-interface {v2}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    goto :goto_1

    .line 475
    :cond_3
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 476
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v2}, Lorg/onepf/oms/OpenIabHelper$Options;->getAvailableStoreNames()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_4
    :goto_2
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 477
    iget-object v4, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_4

    .line 478
    iget-object v4, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreFactoryMap:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;

    invoke-interface {v4}, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;->get()Lorg/onepf/oms/Appstore;

    move-result-object v4

    .line 479
    invoke-interface {v1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 480
    iget-object v5, p0, Lorg/onepf/oms/OpenIabHelper;->availableAppstores:Ljava/util/Set;

    invoke-interface {v5, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 481
    invoke-interface {v0, v3}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    goto :goto_2

    .line 485
    :cond_5
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_6

    .line 486
    new-instance v2, Lorg/onepf/oms/OpenIabHelper$8;

    invoke-direct {v2, p0, v0, p1, v1}, Lorg/onepf/oms/OpenIabHelper$8;-><init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/List;)V

    invoke-virtual {p0, v2}, Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores(Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;)V

    goto :goto_3

    .line 519
    :cond_6
    invoke-direct {p0, p1}, Lorg/onepf/oms/OpenIabHelper;->setupWithStrategy(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    :goto_3
    return-void

    .line 457
    :cond_7
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Setup listener must be not null!"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public subscriptionsSupported()Z
    .locals 2

    const-string v0, "subscriptionsSupported"

    .line 1262
    invoke-virtual {p0, v0}, Lorg/onepf/oms/OpenIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 1263
    iget v0, p0, Lorg/onepf/oms/OpenIabHelper;->setupState:I

    if-nez v0, :cond_0

    .line 1266
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper;->appStoreBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    invoke-interface {v0}, Lorg/onepf/oms/AppstoreInAppBillingService;->subscriptionsSupported()Z

    move-result v0

    return v0

    .line 1264
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "OpenIabHelper is not set up."

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
