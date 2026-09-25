.class public Ljp/colopl/drapro/StartActivity;
.super Lcom/unity3d/player/UnityPlayerActivity;
.source "StartActivity.java"


# static fields
.field public static final AFFILIATE_BROWSER_REQUEST_CODE:I = 0x87e85

.field private static final HOST:Ljava/lang/String; = "gogame.net"

.field private static IABV3_API_RETRY_LIMIT:I = 0x0

.field private static final LONG_PRESS_TIME:J = 0xc8L

.field private static final SCHEME:Ljava/lang/String; = "gogamedrapro"

.field private static final SCHEME_ALLOWED_PREFS_KEY_LIST:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static final SCHEME_PLAYER_IS_OPEN_URL:Ljava/lang/String; = "o"

.field private static final SCHEME_PLAYER_PREFS_KEY_FOR_KEY:Ljava/lang/String; = "k"

.field private static final SCHEME_PLAYER_PREFS_KEY_FOR_VALUE:Ljava/lang/String; = "v"

.field private static final SHOW_ACHIEVEMENTLIST_CODE:I = 0x3e8

.field private static final TAG:Ljava/lang/String; = "StartActivity"

.field private static timer:J


# instance fields
.field private final ALREADY_SIGNIN_KEY:Ljava/lang/String;

.field private config:Ljp/colopl/drapro/Config;

.field private consumptionRetry:I

.field private depositRetry:I

.field private handler:Landroid/os/Handler;

.field private mBillingHelper:Ljp/colopl/iab/IabHelper;

.field private mBillingRunning:Z

.field mBroadcastReceiver:Ljp/colopl/iab/IabBroadcastReceiver;

.field private mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

.field mConsumeFinishedListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;

.field mDepositPostListener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

.field mDepositPrepareListener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

.field mGotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

.field mProgressDialog:Landroid/app/ProgressDialog;

.field mPurchaseFinishedListener:Ljp/colopl/iab/IabHelper$OnIabPurchaseFinishedListener;

.field mPurchaseList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljp/colopl/iab/Purchase;",
            ">;"
        }
    .end annotation
.end field

.field mUndepositedPurcahseList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljp/colopl/iab/Purchase;",
            ">;"
        }
    .end annotation
.end field

.field private mWakeLock:Landroid/os/PowerManager$WakeLock;

.field private purchaseStatusText:Landroid/widget/TextView;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 88
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "im"

    .line 89
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "fc"

    .line 90
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "il"

    .line 91
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "lt"

    .line 92
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "gc"

    .line 93
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "ic"

    .line 94
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 95
    invoke-static {v0}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    sput-object v0, Ljp/colopl/drapro/StartActivity;->SCHEME_ALLOWED_PREFS_KEY_LIST:Ljava/util/List;

    const/4 v0, 0x3

    .line 506
    sput v0, Ljp/colopl/drapro/StartActivity;->IABV3_API_RETRY_LIMIT:I

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    .line 72
    invoke-direct {p0}, Lcom/unity3d/player/UnityPlayerActivity;-><init>()V

    const-string v0, "have_ever_signin"

    .line 99
    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->ALREADY_SIGNIN_KEY:Ljava/lang/String;

    const/4 v0, 0x0

    .line 114
    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    .line 115
    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

    .line 116
    new-instance v1, Landroid/os/Handler;

    invoke-direct {v1}, Landroid/os/Handler;-><init>()V

    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->handler:Landroid/os/Handler;

    const/4 v1, 0x0

    .line 507
    iput v1, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    .line 508
    iput v1, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    .line 509
    iput-boolean v1, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    .line 510
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    .line 511
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    .line 512
    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    .line 691
    new-instance v0, Ljp/colopl/drapro/StartActivity$6;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/StartActivity$6;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mGotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    .line 824
    new-instance v0, Ljp/colopl/drapro/StartActivity$7;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/StartActivity$7;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mConsumeFinishedListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;

    .line 871
    new-instance v0, Ljp/colopl/drapro/StartActivity$8;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/StartActivity$8;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mPurchaseFinishedListener:Ljp/colopl/iab/IabHelper$OnIabPurchaseFinishedListener;

    .line 912
    new-instance v0, Ljp/colopl/drapro/StartActivity$9;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/StartActivity$9;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mDepositPrepareListener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    .line 930
    new-instance v0, Ljp/colopl/drapro/StartActivity$10;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/StartActivity$10;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mDepositPostListener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    return-void
.end method

.method static synthetic access$000(Ljp/colopl/drapro/StartActivity;III)V
    .locals 0

    .line 72
    invoke-direct {p0, p1, p2, p3}, Ljp/colopl/drapro/StartActivity;->showSimpleDialog(III)V

    return-void
.end method

.method static synthetic access$100(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 72
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->finishBillingRunning()V

    return-void
.end method

.method static synthetic access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;
    .locals 0

    .line 72
    iget-object p0, p0, Ljp/colopl/drapro/StartActivity;->mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

    return-object p0
.end method

.method static synthetic access$300(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/iab/IabHelper;
    .locals 0

    .line 72
    iget-object p0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    return-object p0
.end method

.method static synthetic access$400(Ljp/colopl/drapro/StartActivity;)I
    .locals 0

    .line 72
    iget p0, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    return p0
.end method

.method static synthetic access$402(Ljp/colopl/drapro/StartActivity;I)I
    .locals 0

    .line 72
    iput p1, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    return p1
.end method

.method static synthetic access$408(Ljp/colopl/drapro/StartActivity;)I
    .locals 2

    .line 72
    iget v0, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    return v0
.end method

.method static synthetic access$500()I
    .locals 1

    .line 72
    sget v0, Ljp/colopl/drapro/StartActivity;->IABV3_API_RETRY_LIMIT:I

    return v0
.end method

.method static synthetic access$600(Ljp/colopl/drapro/StartActivity;)I
    .locals 0

    .line 72
    iget p0, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    return p0
.end method

.method static synthetic access$602(Ljp/colopl/drapro/StartActivity;I)I
    .locals 0

    .line 72
    iput p1, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    return p1
.end method

.method static synthetic access$608(Ljp/colopl/drapro/StartActivity;)I
    .locals 2

    .line 72
    iget v0, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    return v0
.end method

.method private disposeBilling()V
    .locals 2

    .line 546
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 547
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v0}, Ljp/colopl/iab/IabHelper;->dispose()V

    .line 548
    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    .line 550
    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

    if-eqz v0, :cond_1

    .line 551
    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

    :cond_1
    return-void
.end method

.method private finishBillingRunning()V
    .locals 3

    .line 576
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->dismissProgressDialog2()Z

    const-string v0, "ShopReceiver"

    const-string v1, "buyItem"

    const-string v2, ""

    .line 578
    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 579
    iput-boolean v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    return-void
.end method

.method private initBilling()V
    .locals 4

    const-string v0, "StartActivity"

    const-string v1, "[IABV3] initBilling start"

    .line 516
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "StartActivity"

    .line 517
    invoke-static {}, Ljp/colopl/drapro/NetworkHelper;->getHost()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 518
    new-instance v0, Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {v0, p0}, Ljp/colopl/drapro/ColoplDepositHelper;-><init>(Ljp/colopl/drapro/StartActivity;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mColoDepositHelper:Ljp/colopl/drapro/ColoplDepositHelper;

    .line 520
    invoke-static {}, Ljp/colopl/drapro/AppConsts;->getAppLicenseKey()Ljava/lang/String;

    move-result-object v0

    const-string v1, "StartActivity"

    .line 521
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "[IABV3] appKey from AppConsts: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 523
    new-instance v1, Ljp/colopl/iab/IabHelper;

    invoke-direct {v1, p0, v0}, Ljp/colopl/iab/IabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    iput-object v1, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    .line 526
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Ljp/colopl/iab/IabHelper;->enableDebugLogging(Z)V

    .line 527
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    invoke-static {v0, p0}, Ljp/colopl/drapro/InAppBillingHelper;->init(Ljp/colopl/iab/IabHelper;Ljp/colopl/drapro/StartActivity;)V

    const/4 v0, 0x0

    .line 529
    iput v0, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    .line 530
    iput v0, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    .line 531
    iput-boolean v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    .line 533
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    new-instance v1, Ljp/colopl/drapro/StartActivity$2;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$2;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, v1}, Ljp/colopl/iab/IabHelper;->startSetup(Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method

.method private initPromoCodeReceiver()V
    .locals 2

    const-string v0, "PromoCode"

    const-string v1, "Called initPromoCodeReceiver"

    .line 219
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 220
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.android.vending.billing.PURCHASES_UPDATED"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 221
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 222
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->sendBroadcast(Landroid/content/Intent;)V

    .line 224
    new-instance v0, Ljp/colopl/iab/IabBroadcastReceiver;

    new-instance v1, Ljp/colopl/drapro/StartActivity$1;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$1;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-direct {v0, v1}, Ljp/colopl/iab/IabBroadcastReceiver;-><init>(Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBroadcastReceiver:Ljp/colopl/iab/IabBroadcastReceiver;

    .line 231
    new-instance v0, Landroid/content/IntentFilter;

    const-string v1, "com.android.vending.billing.PURCHASES_UPDATED"

    invoke-direct {v0, v1}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    .line 232
    iget-object v1, p0, Ljp/colopl/drapro/StartActivity;->mBroadcastReceiver:Ljp/colopl/iab/IabBroadcastReceiver;

    invoke-virtual {p0, v1, v0}, Ljp/colopl/drapro/StartActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method private loadPlayerPrefs(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    .line 411
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 412
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    if-nez v0, :cond_0

    const-string p1, ""

    return-object p1

    :cond_0
    const-string v1, "false"

    .line 417
    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private savePlayerPrefs(Ljava/lang/String;Ljava/lang/String;)V
    .locals 4

    const-string v0, "StartActivity"

    .line 395
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "savePlayerPrefs Key:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " Value:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 397
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 398
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v1

    .line 399
    invoke-interface {v1}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 400
    invoke-interface {v1, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 401
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 404
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, ".v2.playerprefs"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 405
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 406
    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 407
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method private setSchemeParameterToPlayerPrefs()V
    .locals 5

    const-string v0, "StartActivity"

    .line 324
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "intent action name: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "android.intent.action.VIEW"

    .line 325
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "StartActivity"

    const-string v1, "intent action is null"

    .line 326
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 330
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    if-nez v0, :cond_1

    const-string v0, "StartActivity"

    const-string v1, "intent data is null"

    .line 332
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_1
    const-string v1, "StartActivity"

    .line 336
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "url="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 338
    invoke-virtual {v0}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    .line 339
    invoke-virtual {v0}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v2

    const-string v3, "gogamedrapro"

    .line 342
    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_4

    const-string v1, "gogame.net"

    invoke-virtual {v2, v1}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_4

    const-string v1, "StartActivity"

    const-string v2, "Start SetSchemeParameter"

    .line 343
    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "k"

    .line 344
    invoke-virtual {v0, v1}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "v"

    .line 345
    invoke-virtual {v0, v2}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 349
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v2

    const/4 v3, 0x1

    if-eqz v2, :cond_2

    const/4 v4, 0x0

    .line 351
    invoke-virtual {v2, v0, v4}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v4

    if-ne v4, v3, :cond_3

    const-string v0, "StartActivity"

    const-string v1, "Stop SetSchemeParameter : Used Key"

    .line 354
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 358
    :cond_2
    new-instance v2, Landroid/os/Bundle;

    invoke-direct {v2}, Landroid/os/Bundle;-><init>()V

    .line 360
    :cond_3
    invoke-virtual {v2, v0, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    .line 361
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getIntent()Landroid/content/Intent;

    move-result-object v3

    invoke-virtual {v3, v2}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    if-eqz v1, :cond_4

    .line 364
    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_4

    if-eqz v0, :cond_4

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_4

    sget-object v2, Ljp/colopl/drapro/StartActivity;->SCHEME_ALLOWED_PREFS_KEY_LIST:Ljava/util/List;

    .line 365
    invoke-interface {v2, v1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_4

    .line 366
    invoke-direct {p0, v1, v0}, Ljp/colopl/drapro/StartActivity;->savePlayerPrefs(Ljava/lang/String;Ljava/lang/String;)V

    :cond_4
    return-void
.end method

.method private showSimpleDialog(III)V
    .locals 1

    .line 1016
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    const/4 p2, 0x0

    .line 1017
    invoke-virtual {p1, p3, p2}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/AlertDialog;->show()V

    return-void
.end method

.method public static verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z
    .locals 2

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return v0

    .line 1011
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/iab/Purchase;->getDeveloperPayload()Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_1

    const-string v1, ""

    if-eq p0, v1, :cond_1

    const/4 v0, 0x1

    :cond_1
    return v0
.end method


# virtual methods
.method public AcquireWakeLock()V
    .locals 1

    .line 374
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->config:Ljp/colopl/drapro/Config;

    invoke-virtual {v0}, Ljp/colopl/drapro/Config;->getScreenLockMode()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 378
    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    if-eqz v0, :cond_1

    .line 379
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->acquire()V

    :cond_1
    return-void
.end method

.method public ReleaseWakeLock()Z
    .locals 1

    .line 384
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    if-eqz v0, :cond_0

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->isHeld()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 385
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->release()V

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public checkInventory()V
    .locals 2

    const-string v0, "StartActivity"

    const-string v1, "[IABV3] checkInventory start querying inventory"

    .line 686
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 687
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity;->mGotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    invoke-virtual {v0, v1}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public connect()V
    .locals 0

    return-void
.end method

.method public dismissProgressDialog2()Z
    .locals 1

    .line 1038
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 1041
    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {v0}, Landroid/app/ProgressDialog;->dismiss()V

    const/4 v0, 0x0

    .line 1042
    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    const/4 v0, 0x1

    return v0
.end method

.method public dispatchKeyEvent(Landroid/view/KeyEvent;)Z
    .locals 2

    .line 442
    invoke-virtual {p1}, Landroid/view/KeyEvent;->getAction()I

    move-result v0

    const/4 v1, 0x2

    if-ne v0, v1, :cond_0

    .line 443
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1}, Lcom/unity3d/player/UnityPlayer;->injectEvent(Landroid/view/InputEvent;)Z

    move-result p1

    return p1

    .line 444
    :cond_0
    invoke-super {p0, p1}, Lcom/unity3d/player/UnityPlayerActivity;->dispatchKeyEvent(Landroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method protected getConfig()Ljp/colopl/config/Config;
    .locals 1

    .line 421
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getApplication()Landroid/app/Application;

    move-result-object v0

    check-cast v0, Ljp/colopl/drapro/ColoplApplication;

    invoke-virtual {v0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object v0

    return-object v0
.end method

.method public inappbillingStart(Ljava/lang/String;)V
    .locals 0

    .line 558
    iget-boolean p1, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    if-eqz p1, :cond_0

    return-void

    :cond_0
    const/4 p1, 0x1

    .line 562
    iput-boolean p1, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    const/4 p1, 0x0

    .line 564
    iput p1, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    .line 565
    iput p1, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    .line 567
    new-instance p1, Ljp/colopl/drapro/StartActivity$3;

    invoke-direct {p1, p0}, Ljp/colopl/drapro/StartActivity$3;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public isConnected()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method protected onActivityResult(IILandroid/content/Intent;)V
    .locals 1

    .line 999
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v0, p1, p2, p3}, Ljp/colopl/iab/IabHelper;->handleActivityResult(IILandroid/content/Intent;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 1000
    invoke-super {p0, p1, p2, p3}, Lcom/unity3d/player/UnityPlayerActivity;->onActivityResult(IILandroid/content/Intent;)V

    goto :goto_0

    :cond_0
    const-string p1, "StartActivity"

    const-string p2, "onActivityResult handled by IABUtil."

    .line 1002
    invoke-static {p1, p2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public onConfigurationChanged(Landroid/content/res/Configuration;)V
    .locals 1

    .line 427
    invoke-super {p0, p1}, Lcom/unity3d/player/UnityPlayerActivity;->onConfigurationChanged(Landroid/content/res/Configuration;)V

    .line 428
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1}, Lcom/unity3d/player/UnityPlayer;->configurationChanged(Landroid/content/res/Configuration;)V

    return-void
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 4

    const/4 v0, 0x1

    .line 124
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->requestWindowFeature(I)Z

    .line 125
    iget-object v1, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    .line 127
    iget-object v1, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v1}, Lcom/unity3d/player/UnityPlayer;->quit()V

    .line 128
    iput-object v2, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    .line 130
    :cond_0
    invoke-super {p0, p1}, Lcom/unity3d/player/UnityPlayerActivity;->onCreate(Landroid/os/Bundle;)V

    .line 132
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getWindow()Landroid/view/Window;

    move-result-object p1

    invoke-virtual {p1, v2}, Landroid/view/Window;->takeSurface(Landroid/view/SurfaceHolder$Callback2;)V

    .line 133
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getWindow()Landroid/view/Window;

    move-result-object p1

    const/4 v1, 0x2

    invoke-virtual {p1, v1}, Landroid/view/Window;->setFormat(I)V

    .line 136
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "main"

    const-string v2, "layout"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->setContentView(I)V

    .line 139
    new-instance p1, Ljp/colopl/drapro/Config;

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {p1, v1}, Ljp/colopl/drapro/Config;-><init>(Landroid/content/Context;)V

    iput-object p1, p0, Ljp/colopl/drapro/StartActivity;->config:Ljp/colopl/drapro/Config;

    const-string p1, "power"

    .line 142
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/os/PowerManager;

    const/16 v1, 0xa

    .line 143
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v1, v2}, Landroid/os/PowerManager;->newWakeLock(ILjava/lang/String;)Landroid/os/PowerManager$WakeLock;

    move-result-object p1

    iput-object p1, p0, Ljp/colopl/drapro/StartActivity;->mWakeLock:Landroid/os/PowerManager$WakeLock;

    .line 146
    invoke-static {p0}, Ljp/colopl/drapro/AppHelper;->init(Landroid/app/Activity;)V

    .line 147
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->config:Ljp/colopl/drapro/Config;

    invoke-static {p1}, Ljp/colopl/drapro/AppHelper;->setConfig(Ljp/colopl/drapro/Config;)V

    .line 148
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    sput-object p1, Ljp/colopl/drapro/AppConsts;->appContext:Landroid/content/Context;

    .line 149
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result p1

    invoke-static {p1}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p1

    sput-object p1, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    .line 154
    :try_start_0
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {p1, v1, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p1

    iget-object p1, p1, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    sput-object p1, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    const-string p1, "StartActivity"

    .line 155
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "VersionName:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v2, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 157
    invoke-virtual {p1}, Landroid/content/pm/PackageManager$NameNotFoundException;->printStackTrace()V

    .line 161
    :goto_0
    invoke-static {p0}, Ljp/colopl/drapro/NetworkHelper;->init(Landroid/app/Activity;)V

    .line 164
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->setSchemeParameterToPlayerPrefs()V

    .line 168
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "UnityLayout"

    const-string v2, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 167
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/FrameLayout;

    .line 169
    invoke-static {v0, v0, v0}, Landroid/graphics/Color;->rgb(III)I

    move-result v1

    invoke-virtual {p1, v1}, Landroid/widget/FrameLayout;->setBackgroundColor(I)V

    .line 172
    iget-object v1, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v1}, Lcom/unity3d/player/UnityPlayer;->getSettings()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "hide_status_bar"

    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 173
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    const/16 v1, 0x400

    invoke-virtual {v0, v1, v1}, Landroid/view/Window;->setFlags(II)V

    .line 178
    :cond_1
    :try_start_1
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0}, Lcom/unity3d/player/UnityPlayer;->getView()Landroid/view/View;

    move-result-object v0

    new-instance v1, Landroid/view/ViewGroup$LayoutParams;

    const/4 v2, -0x1

    invoke-direct {v1, v2, v2}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {p1, v0, v1}, Landroid/widget/FrameLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_1
    move-exception p1

    const-string v0, "StartActivity"

    const-string v1, "onCreate"

    .line 181
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 182
    invoke-virtual {p1}, Ljava/lang/Exception;->printStackTrace()V

    .line 185
    :goto_1
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->initPromoCodeReceiver()V

    .line 188
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->initBilling()V

    .line 192
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v0, "text_purchase_status"

    const-string v1, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 191
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Ljp/colopl/drapro/StartActivity;->purchaseStatusText:Landroid/widget/TextView;

    .line 193
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->purchaseStatusText:Landroid/widget/TextView;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setVisibility(I)V

    .line 196
    invoke-static {p0}, Ljp/colopl/drapro/AnalyticsHelper;->init(Landroid/app/Activity;)V

    .line 203
    invoke-static {p0}, Ljp/colopl/gcm/RegistrarHelper;->init(Landroid/app/Activity;)V

    return-void
.end method

.method protected onDestroy()V
    .locals 1

    .line 238
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mBroadcastReceiver:Ljp/colopl/iab/IabBroadcastReceiver;

    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    .line 240
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->ReleaseWakeLock()Z

    .line 243
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->disposeBilling()V

    .line 245
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0}, Lcom/unity3d/player/UnityPlayer;->quit()V

    .line 246
    invoke-super {p0}, Lcom/unity3d/player/UnityPlayerActivity;->onDestroy()V

    return-void
.end method

.method public onGenericMotionEvent(Landroid/view/MotionEvent;)Z
    .locals 1

    .line 487
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1}, Lcom/unity3d/player/UnityPlayer;->injectEvent(Landroid/view/InputEvent;)Z

    move-result p1

    return p1
.end method

.method public onKeyDown(ILandroid/view/KeyEvent;)Z
    .locals 4

    const/4 v0, 0x4

    if-ne p1, v0, :cond_0

    .line 463
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1, p2}, Lcom/unity3d/player/UnityPlayer;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1

    :cond_0
    const/16 v0, 0x52

    if-ne p1, v0, :cond_3

    .line 465
    sget-wide p1, Ljp/colopl/drapro/StartActivity;->timer:J

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    const/4 p1, 0x0

    if-nez v2, :cond_1

    .line 466
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    sput-wide v0, Ljp/colopl/drapro/StartActivity;->timer:J

    return p1

    .line 470
    :cond_1
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    sget-wide v2, Ljp/colopl/drapro/StartActivity;->timer:J

    sub-long/2addr v0, v2

    const-wide/16 v2, 0xc8

    cmp-long p2, v0, v2

    if-lez p2, :cond_2

    const/4 p1, 0x1

    return p1

    .line 473
    :cond_2
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    sput-wide v0, Ljp/colopl/drapro/StartActivity;->timer:J

    return p1

    .line 477
    :cond_3
    invoke-super {p0, p1, p2}, Lcom/unity3d/player/UnityPlayerActivity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method public onKeyUp(ILandroid/view/KeyEvent;)Z
    .locals 2

    const/16 v0, 0x52

    if-ne p1, v0, :cond_0

    const-wide/16 v0, 0x0

    .line 451
    sput-wide v0, Ljp/colopl/drapro/StartActivity;->timer:J

    :cond_0
    const/4 v0, 0x4

    if-ne p1, v0, :cond_1

    .line 454
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1, p2}, Lcom/unity3d/player/UnityPlayer;->onKeyUp(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1

    .line 456
    :cond_1
    invoke-super {p0, p1, p2}, Lcom/unity3d/player/UnityPlayerActivity;->onKeyUp(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 2

    .line 279
    invoke-super {p0, p1}, Lcom/unity3d/player/UnityPlayerActivity;->onNewIntent(Landroid/content/Intent;)V

    .line 288
    new-instance v0, Landroid/content/Intent;

    const-class v1, Lcom/google/firebase/messaging/MessageForwardingService;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "com.google.android.c2dm.intent.RECEIVE"

    .line 289
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 290
    invoke-virtual {v0, p1}, Landroid/content/Intent;->putExtras(Landroid/content/Intent;)Landroid/content/Intent;

    .line 291
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    .line 293
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->setIntent(Landroid/content/Intent;)V

    return-void
.end method

.method protected onPause()V
    .locals 1

    .line 252
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->ReleaseWakeLock()Z

    .line 253
    invoke-super {p0}, Lcom/unity3d/player/UnityPlayerActivity;->onPause()V

    .line 254
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0}, Lcom/unity3d/player/UnityPlayer;->pause()V

    .line 256
    invoke-static {}, Ljp/colopl/drapro/AnalyticsHelper;->dispatch()V

    return-void
.end method

.method protected onResume()V
    .locals 3

    .line 262
    invoke-super {p0}, Lcom/unity3d/player/UnityPlayerActivity;->onResume()V

    .line 265
    invoke-direct {p0}, Ljp/colopl/drapro/StartActivity;->setSchemeParameterToPlayerPrefs()V

    .line 268
    :try_start_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0}, Lcom/unity3d/player/UnityPlayer;->resume()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "StartActivity"

    const-string v2, "onStart"

    .line 270
    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 271
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    .line 274
    :goto_0
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->AcquireWakeLock()V

    return-void
.end method

.method protected onStart()V
    .locals 3

    .line 298
    invoke-super {p0}, Lcom/unity3d/player/UnityPlayerActivity;->onStart()V

    .line 300
    :try_start_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0}, Lcom/unity3d/player/UnityPlayer;->resume()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "StartActivity"

    const-string v2, "onStart"

    .line 302
    invoke-static {v1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 303
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    .line 306
    :goto_0
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->AcquireWakeLock()V

    return-void
.end method

.method protected onStop()V
    .locals 0

    .line 313
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->ReleaseWakeLock()Z

    .line 314
    invoke-super {p0}, Lcom/unity3d/player/UnityPlayerActivity;->onStop()V

    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 1

    .line 483
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1}, Lcom/unity3d/player/UnityPlayer;->injectEvent(Landroid/view/InputEvent;)Z

    move-result p1

    return p1
.end method

.method public onWindowFocusChanged(Z)V
    .locals 1

    .line 434
    invoke-super {p0, p1}, Lcom/unity3d/player/UnityPlayerActivity;->onWindowFocusChanged(Z)V

    .line 435
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mUnityPlayer:Lcom/unity3d/player/UnityPlayer;

    invoke-virtual {v0, p1}, Lcom/unity3d/player/UnityPlayer;->windowFocusChanged(Z)V

    return-void
.end method

.method public restoreInventory(Z)V
    .locals 2

    .line 585
    iget-boolean v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    if-eqz v0, :cond_0

    const-string p1, "StartActivity"

    const-string v0, "[IABV3] Billing Process is already running"

    .line 586
    invoke-static {p1, v0}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 589
    iput-boolean v0, p0, Ljp/colopl/drapro/StartActivity;->mBillingRunning:Z

    const/4 v0, 0x0

    .line 591
    iput v0, p0, Ljp/colopl/drapro/StartActivity;->consumptionRetry:I

    .line 592
    iput v0, p0, Ljp/colopl/drapro/StartActivity;->depositRetry:I

    .line 595
    new-instance v0, Ljp/colopl/drapro/StartActivity$4;

    invoke-direct {v0, p0, p1}, Ljp/colopl/drapro/StartActivity$4;-><init>(Ljp/colopl/drapro/StartActivity;Z)V

    if-eqz p1, :cond_1

    .line 666
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->handler:Landroid/os/Handler;

    new-instance v1, Ljp/colopl/drapro/StartActivity$5;

    invoke-direct {v1, p0, v0}, Ljp/colopl/drapro/StartActivity$5;-><init>(Ljp/colopl/drapro/StartActivity;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    invoke-virtual {p1, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto :goto_0

    :cond_1
    const-string p1, "StartActivity"

    const-string v1, "[IABV3] restoreInventory start querying inventory"

    .line 678
    invoke-static {p1, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 679
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mBillingHelper:Ljp/colopl/iab/IabHelper;

    invoke-virtual {p1, v0}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    :goto_0
    return-void
.end method

.method public showAchievementsList()V
    .locals 0

    return-void
.end method

.method public showConsumeDialog(Ljava/lang/String;)V
    .locals 5

    .line 1112
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "dialog_message_terms_recovery"

    const-string v2, "string"

    .line 1113
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 1112
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 1114
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_title_terms_recovery"

    const-string v3, "string"

    .line 1115
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    .line 1114
    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 1116
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 1117
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 1118
    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    .line 1119
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 1120
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_close"

    const-string v2, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$15;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$15;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1127
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_terms_conditions_check_contract"

    const-string v2, "string"

    .line 1128
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 1127
    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$16;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$16;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setNeutralButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1142
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_terms_conditions_exec"

    const-string v2, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$17;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$17;-><init>(Ljp/colopl/drapro/StartActivity;)V

    .line 1141
    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1160
    new-instance p1, Ljp/colopl/drapro/StartActivity$18;

    invoke-direct {p1, p0}, Ljp/colopl/drapro/StartActivity$18;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)Landroid/app/AlertDialog$Builder;

    .line 1167
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p1

    .line 1168
    invoke-virtual {p1}, Landroid/app/AlertDialog;->show()V

    return-void
.end method

.method public showDepositDialog(Ljava/lang/String;)V
    .locals 5

    .line 1050
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "dialog_message_terms_recovery"

    const-string v2, "string"

    .line 1051
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 1050
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 1052
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_title_terms_recovery"

    const-string v3, "string"

    .line 1053
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    .line 1052
    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 1054
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 1055
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 1056
    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    .line 1057
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 1058
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_close"

    const-string v2, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$11;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$11;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1065
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_terms_conditions_check_contract"

    const-string v2, "string"

    .line 1066
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 1065
    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$12;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$12;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setNeutralButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1080
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    const-string v1, "dialog_button_terms_conditions_exec"

    const-string v2, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    new-instance v1, Ljp/colopl/drapro/StartActivity$13;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/StartActivity$13;-><init>(Ljp/colopl/drapro/StartActivity;)V

    .line 1079
    invoke-virtual {v0, p1, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 1097
    new-instance p1, Ljp/colopl/drapro/StartActivity$14;

    invoke-direct {p1, p0}, Ljp/colopl/drapro/StartActivity$14;-><init>(Ljp/colopl/drapro/StartActivity;)V

    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)Landroid/app/AlertDialog$Builder;

    .line 1104
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p1

    .line 1105
    invoke-virtual {p1}, Landroid/app/AlertDialog;->show()V

    return-void
.end method

.method public showProgressDialog2(II)V
    .locals 1

    .line 1021
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    if-eqz v0, :cond_0

    .line 1022
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {v0}, Landroid/app/ProgressDialog;->dismiss()V

    .line 1024
    :cond_0
    new-instance v0, Landroid/app/ProgressDialog;

    invoke-direct {v0, p0}, Landroid/app/ProgressDialog;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    if-eqz p1, :cond_1

    .line 1026
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {v0, p1}, Landroid/app/ProgressDialog;->setTitle(I)V

    :cond_1
    if-eqz p2, :cond_2

    .line 1029
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {p0, p2}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/app/ProgressDialog;->setMessage(Ljava/lang/CharSequence;)V

    .line 1031
    :cond_2
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    const/4 p2, 0x1

    invoke-virtual {p1, p2}, Landroid/app/ProgressDialog;->setIndeterminate(Z)V

    .line 1032
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/app/ProgressDialog;->setProgressStyle(I)V

    .line 1033
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {p1, p2}, Landroid/app/ProgressDialog;->setCancelable(Z)V

    .line 1034
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity;->mProgressDialog:Landroid/app/ProgressDialog;

    invoke-virtual {p1}, Landroid/app/ProgressDialog;->show()V

    return-void
.end method

.method public signin()V
    .locals 0

    return-void
.end method

.method public syncUnlockedAchievements(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    return-void
.end method

.method public unlockAchievement(Ljava/lang/String;)V
    .locals 0

    return-void
.end method
