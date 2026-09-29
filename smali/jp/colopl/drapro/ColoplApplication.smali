.class public Ljp/colopl/drapro/ColoplApplication;
.super Lcom/github/droidfu/DroidFuApplication;
.source "ColoplApplication.java"

# interfaces
.implements Landroid/location/LocationListener;
.implements Ljp/colopl/libs/ColoplCellLocationListenerCallback;
.implements Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;


# static fields
.field public static final ACCURACY_FOR_FIRST_CACHED_WIFI_LOCATION:F = 500.0f

.field static final ACTION_CHANGED_LOCATION:Ljava/lang/String; = "jp.colopl.action.CHANGED_LOCATION"

.field public static final ACTION_RECEIVED_NOTIFICATION:Ljava/lang/String; = "jp.colopl.dino.RECEIVED_NOTIFICATION"

.field public static final EXTRA_NOTIFICATON_JSONSTR:Ljava/lang/String; = "extra_jsonstr"

.field private static final HASH_OF_SIG_RELEASE:I = -0x7ec27b0d

.field public static final LOCATION_MEASUREMENT_NG:I = -0x1

.field public static final LOCATION_MEASUREMENT_NG_REQUIRED_BOTH_PROVIDER:I = -0x3

.field public static final LOCATION_MEASUREMENT_NOT_ALLOWED_MOCK:I = -0x2

.field public static final LOCATION_MEASUREMENT_OK:I = 0x1

.field public static final LOCATION_MEASUREMENT_ONLY_CELLLOCATION:I = 0x2

.field public static final LOCATION_MEASUREMENT_ONLY_SPMODE:I = 0x3

.field private static final MAX_COUNT_CDMA_CELL_LOCATION_RECEIVE:I = 0x3

.field private static final MAX_COUNT_SAME_LOCATION:I = 0x3

.field public static final NOTIFICATION_ID:Ljava/lang/String; = "notifyid"

.field public static final NOTIFICATION_TAG:Ljava/lang/String; = "notifytag"

.field public static final REQUEST_CAMERA_CAPTURE:I = 0x3

.field public static final REQUEST_CAMERA_CAPTURE_FOR_SUBMIT:I = 0x7

.field public static final REQUEST_CHOICE_IMAGE:I = 0x2

.field public static final REQUEST_CHOICE_IMAGE_FOR_SUBMIT:I = 0x6

.field public static final REQUEST_EDIT_PHOTO_ATTRIBUTE_FOR_SUBMIT:I = 0xb

.field public static final REQUEST_PHOTO_SUBMIT:I = 0xa

.field public static final REQUEST_SCREEN_CAPTURE:I = 0x4

.field public static final REQUEST_SHOW_LOADING_SCREEN:I = 0x0

.field public static final REQUEST_SHOW_LOCATION_CONFIRM:I = 0x1

.field public static final REQUEST_SHOW_NOTIFICATION:I = 0x5

.field public static final REQUEST_SHOW_PREFERENCE:I = 0x8

.field public static final REQUEST_SORTING_APP:I = 0x9

.field private static final TAG:Ljava/lang/String; = "ColoplApplication"

.field public static final UPDATE_LOCATION_KEY_ADOPT_LATTER_LOCATION:Ljava/lang/String; = "adopt"

.field public static final UPDATE_LOCATION_KEY_DOUBTFUL_LOCATION:Ljava/lang/String; = "doubt"

.field public static final UPDATE_LOCATION_KEY_FORCE_TO_UPDATE:Ljava/lang/String; = "force"

.field public static final UPDATE_LOCATION_KEY_LOCATION:Ljava/lang/String; = "location"

.field private static isRelease:Ljava/lang/Boolean;


# instance fields
.field private cellLocationListener:Ljp/colopl/libs/ColoplCellLocationListener;

.field private config:Ljp/colopl/config/Config;

.field private docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

.field private firstNetworkLocation:Landroid/location/Location;

.field public imageUri:Landroid/net/Uri;

.field private isRequiredVersionDirty:Z

.field private isResultInFailureDoCoMOAPI:Z

.field private latestCellBasedLocation:Landroid/location/Location;

.field private latestLocation:Landroid/location/Location;

.field private locationManager:Landroid/location/LocationManager;

.field private locations:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;"
        }
    .end annotation
.end field

.field private mAnalytics:Ljp/colopl/util/AnalyticsUtil;

.field private maxAccuracy:F

.field private maxAccuracyProvider:Ljava/lang/String;

.field private previousNWLocation:Landroid/location/Location;

.field private receiveCdmaCellLocationCount:I

.field private sameLocationCount:I

.field public screenShot:Landroid/graphics/Picture;

.field private updateRequired:Ljava/lang/Boolean;

.field private urlForAuthorizeDoCoMoAPI:Ljava/lang/String;

.field private urlOfShowSPModeDialog:Ljava/lang/String;

.field private wakelock:Landroid/os/PowerManager$WakeLock;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    .line 47
    invoke-direct {p0}, Lcom/github/droidfu/DroidFuApplication;-><init>()V

    .line 101
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    const v0, 0x7f7fffff    # Float.MAX_VALUE

    .line 102
    iput v0, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    const/4 v0, 0x0

    .line 105
    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->urlOfShowSPModeDialog:Ljava/lang/String;

    const/4 v1, 0x0

    .line 110
    iput-boolean v1, p0, Ljp/colopl/drapro/ColoplApplication;->isRequiredVersionDirty:Z

    .line 607
    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->updateRequired:Ljava/lang/Boolean;

    return-void
.end method

.method private getColoplCellLocationListener()Ljp/colopl/libs/ColoplCellLocationListener;
    .locals 1

    .line 126
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->cellLocationListener:Ljp/colopl/libs/ColoplCellLocationListener;

    if-nez v0, :cond_0

    .line 127
    new-instance v0, Ljp/colopl/libs/ColoplCellLocationListener;

    invoke-direct {v0, p0}, Ljp/colopl/libs/ColoplCellLocationListener;-><init>(Ljp/colopl/libs/ColoplCellLocationListenerCallback;)V

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->cellLocationListener:Ljp/colopl/libs/ColoplCellLocationListener;

    .line 129
    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->cellLocationListener:Ljp/colopl/libs/ColoplCellLocationListener;

    return-object v0
.end method

.method private initAnalytics()V
    .locals 2

    .line 670
    new-instance v0, Ljp/colopl/util/AnalyticsUtil;

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Ljp/colopl/util/AnalyticsUtil;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->mAnalytics:Ljp/colopl/util/AnalyticsUtil;

    return-void
.end method

.method public static isEmulator()Z
    .locals 2

    .line 529
    sget-object v0, Landroid/os/Build;->PRODUCT:Ljava/lang/String;

    const-string v1, "sdk"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Landroid/os/Build;->PRODUCT:Ljava/lang/String;

    const-string v1, "google_sdk"

    .line 530
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method private updateLocation(Landroid/location/Location;ZZ)Z
    .locals 4

    .line 447
    invoke-virtual {p1}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    :cond_0
    const/4 v2, 0x1

    if-nez p2, :cond_4

    .line 453
    iget-object v3, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v3

    if-nez v3, :cond_1

    goto :goto_0

    .line 463
    :cond_1
    invoke-virtual {p1}, Landroid/location/Location;->hasAccuracy()Z

    move-result p2

    if-eqz p2, :cond_3

    .line 464
    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result p1

    .line 465
    iget p2, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    cmpg-float p2, p1, p2

    if-gtz p2, :cond_2

    .line 467
    iput p1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    .line 468
    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracyProvider:Ljava/lang/String;

    goto :goto_1

    :cond_2
    if-eqz p3, :cond_3

    .line 471
    iget-object p2, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracyProvider:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_3

    .line 475
    iput p1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    goto :goto_1

    :cond_3
    const/4 v2, 0x0

    goto :goto_1

    :cond_4
    :goto_0
    if-eqz p2, :cond_5

    .line 456
    iget-object p2, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-virtual {p2}, Ljava/util/HashMap;->clear()V

    .line 458
    :cond_5
    invoke-virtual {p1}, Landroid/location/Location;->hasAccuracy()Z

    move-result p2

    if-eqz p2, :cond_6

    .line 459
    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result p1

    iput p1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    .line 460
    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracyProvider:Ljava/lang/String;

    :cond_6
    :goto_1
    return v2
.end method


# virtual methods
.method public acquireWakeLock()Z
    .locals 2

    .line 166
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    if-eqz v0, :cond_0

    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->isHeld()Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "ColoplApplication"

    const-string v1, "===== acquireWakeLock ======="

    .line 167
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 168
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->acquire()V

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public allowMockLocation()Z
    .locals 3

    .line 663
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    const-string v1, "mock_location"

    const/4 v2, 0x0

    invoke-static {v0, v1, v2}, Landroid/provider/Settings$Secure;->getInt(Landroid/content/ContentResolver;Ljava/lang/String;I)I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method protected attachBaseContext(Landroid/content/Context;)V
    .locals 0

    .line 161
    invoke-super {p0, p1}, Lcom/github/droidfu/DroidFuApplication;->attachBaseContext(Landroid/content/Context;)V

    .line 162
    invoke-static {p0}, Landroidx/multidex/MultiDex;->install(Landroid/content/Context;)V

    return-void
.end method

.method public cleanDoCoMoAsyncTask()V
    .locals 2

    .line 402
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    if-eqz v0, :cond_0

    .line 403
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->cancel(Z)Z

    .line 404
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->setDelegate(Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;)V

    :cond_0
    return-void
.end method

.method public clearDoCoMoLocationInfo()V
    .locals 1

    const/4 v0, 0x0

    .line 429
    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->urlForAuthorizeDoCoMoAPI:Ljava/lang/String;

    return-void
.end method

.method public getAnalyticsUtil()Ljp/colopl/util/AnalyticsUtil;
    .locals 1

    .line 678
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->mAnalytics:Ljp/colopl/util/AnalyticsUtil;

    return-object v0
.end method

.method public getConfig()Ljp/colopl/config/Config;
    .locals 1

    .line 550
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->config:Ljp/colopl/config/Config;

    return-object v0
.end method

.method public getLatestLocation()Landroid/location/Location;
    .locals 1

    .line 682
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->latestLocation:Landroid/location/Location;

    return-object v0
.end method

.method public getLocations()Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;"
        }
    .end annotation

    .line 122
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    return-object v0
.end method

.method public getNowAccuracy()F
    .locals 1

    .line 258
    iget v0, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    return v0
.end method

.method public getUrlForAuthorizeDoCoMoAPI()Ljava/lang/String;
    .locals 1

    .line 433
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->urlForAuthorizeDoCoMoAPI:Ljava/lang/String;

    return-object v0
.end method

.method public getUrlOfShowSPModeDialog()Ljava/lang/String;
    .locals 1

    .line 595
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->urlOfShowSPModeDialog:Ljava/lang/String;

    return-object v0
.end method

.method public hasSuitableLocationForAutoRegister()Z
    .locals 1

    .line 639
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-static {v0}, Ljp/colopl/util/LocationUtil;->getMostAccurateLocation(Ljava/util/HashMap;)Landroid/location/Location;

    move-result-object v0

    .line 640
    invoke-static {v0}, Ljp/colopl/util/LocationUtil;->isSuitableForAutoRegister(Landroid/location/Location;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isAUDevice()Z
    .locals 2

    .line 648
    sget-object v0, Landroid/os/Build;->BRAND:Ljava/lang/String;

    const-string v1, "KDDI"

    .line 649
    invoke-virtual {v0, v1}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method public isAnyLocationProviderEnabled()Z
    .locals 1

    .line 508
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isGPSLocationEnabled()Z

    move-result v0

    if-nez v0, :cond_1

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isNetworkLocationEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public isAvailableDoCoMoAPI()Z
    .locals 1

    .line 654
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isCarrierDoCoMo()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isMobileConnectivity()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isBackgroundDataSetting()Z
    .locals 2

    const-string v0, "connectivity"

    .line 545
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/net/ConnectivityManager;

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 546
    invoke-virtual {v0}, Landroid/net/ConnectivityManager;->getBackgroundDataSetting()Z

    move-result v0

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public isBothLocationProviderEnabled()Z
    .locals 1

    .line 512
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isGPSLocationEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isNetworkLocationEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isCarrierDoCoMo()Z
    .locals 2

    const-string v0, "phone"

    .line 559
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/telephony/TelephonyManager;

    invoke-virtual {v0}, Landroid/telephony/TelephonyManager;->getNetworkOperatorName()Ljava/lang/String;

    move-result-object v0

    .line 560
    invoke-virtual {v0}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v0

    const-string v1, "DOCOMO"

    invoke-virtual {v0, v1}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v0

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isCarrierSoftBank()Z
    .locals 2

    const-string v0, "phone"

    .line 568
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/telephony/TelephonyManager;

    invoke-virtual {v0}, Landroid/telephony/TelephonyManager;->getNetworkOperatorName()Ljava/lang/String;

    move-result-object v0

    .line 569
    invoke-virtual {v0}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v0

    const-string v1, "SOFTBANK"

    invoke-virtual {v0, v1}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v0

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public isFirstLaunch()Z
    .locals 1

    .line 554
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object v0

    invoke-virtual {v0}, Ljp/colopl/config/Config;->hasSettings()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public isGPSLocationEnabled()Z
    .locals 2

    .line 520
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    const-string v1, "gps"

    invoke-virtual {v0, v1}, Landroid/location/LocationManager;->isProviderEnabled(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method public isMobileConnectivity()Z
    .locals 2

    const-string v0, "connectivity"

    .line 577
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/net/ConnectivityManager;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 582
    :cond_0
    invoke-virtual {v0}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object v0

    if-nez v0, :cond_1

    return v1

    .line 587
    :cond_1
    invoke-virtual {v0}, Landroid/net/NetworkInfo;->getType()I

    move-result v0

    if-nez v0, :cond_2

    const/4 v1, 0x1

    :cond_2
    return v1
.end method

.method public isNetworkLocationEnabled()Z
    .locals 2

    .line 516
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    const-string v1, "network"

    invoke-virtual {v0, v1}, Landroid/location/LocationManager;->isProviderEnabled(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method public isReleaseBuild()Z
    .locals 6

    .line 617
    sget-object v0, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    .line 618
    sget-object v0, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0

    :cond_0
    const/4 v0, 0x0

    .line 620
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    sput-object v1, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;

    const/4 v1, 0x1

    .line 622
    :try_start_0
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    .line 623
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getPackageName()Ljava/lang/String;

    move-result-object v3

    const/16 v4, 0x40

    invoke-virtual {v2, v3, v4}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v2

    .line 624
    iget-object v2, v2, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    array-length v3, v2

    :goto_0
    if-ge v0, v3, :cond_2

    aget-object v4, v2, v0

    .line 625
    invoke-virtual {v4}, Landroid/content/pm/Signature;->hashCode()I

    move-result v4

    const v5, -0x7ec27b0d

    if-ne v4, v5, :cond_1

    .line 626
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v4

    sput-object v4, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v2, "Exception thrown when detecting if app is signed by a release keystore."

    .line 631
    invoke-static {v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 633
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    sput-object v0, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;

    .line 635
    :cond_2
    sget-object v0, Ljp/colopl/drapro/ColoplApplication;->isRelease:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public isResultInFailureDoCoMOAPI()Z
    .locals 1

    .line 425
    iget-boolean v0, p0, Ljp/colopl/drapro/ColoplApplication;->isResultInFailureDoCoMOAPI:Z

    return v0
.end method

.method public isUpdateRequired()Z
    .locals 1

    .line 609
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->updateRequired:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    iget-boolean v0, p0, Ljp/colopl/drapro/ColoplApplication;->isRequiredVersionDirty:Z

    if-eqz v0, :cond_1

    .line 610
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object v0

    invoke-virtual {v0}, Ljp/colopl/config/Config;->isUpdateRequired()Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->updateRequired:Ljava/lang/Boolean;

    const/4 v0, 0x0

    .line 611
    iput-boolean v0, p0, Ljp/colopl/drapro/ColoplApplication;->isRequiredVersionDirty:Z

    .line 613
    :cond_1
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->updateRequired:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public networkIsConnected()Z
    .locals 1

    const-string v0, "connectivity"

    .line 539
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/net/ConnectivityManager;

    .line 540
    invoke-virtual {v0}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 541
    invoke-virtual {v0}, Landroid/net/NetworkInfo;->isConnected()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public onCreate()V
    .locals 3

    .line 134
    invoke-super {p0}, Lcom/github/droidfu/DroidFuApplication;->onCreate()V

    .line 136
    invoke-direct {p0}, Ljp/colopl/drapro/ColoplApplication;->initAnalytics()V

    const-string v0, "location"

    .line 137
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/location/LocationManager;

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    const-string v0, "power"

    .line 138
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/os/PowerManager;

    .line 139
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x6

    invoke-virtual {v0, v2, v1}, Landroid/os/PowerManager;->newWakeLock(ILjava/lang/String;)Landroid/os/PowerManager$WakeLock;

    move-result-object v0

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    .line 140
    new-instance v0, Ljp/colopl/config/Config;

    invoke-direct {v0, p0}, Ljp/colopl/config/Config;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->config:Ljp/colopl/config/Config;

    .line 141
    invoke-static {p0}, Ljp/colopl/util/LogUtil;->setup(Landroid/content/Context;)V

    const/4 v0, 0x1

    .line 143
    new-array v0, v0, [Lio/fabric/sdk/android/Kit;

    new-instance v1, Lcom/crashlytics/android/Crashlytics;

    invoke-direct {v1}, Lcom/crashlytics/android/Crashlytics;-><init>()V

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-static {p0, v0}, Lio/fabric/sdk/android/Fabric;->with(Landroid/content/Context;[Lio/fabric/sdk/android/Kit;)Lio/fabric/sdk/android/Fabric;

    :try_start_0
    const-string v0, "android.os.AsyncTask"

    .line 153
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method public onLocationChanged(Landroid/location/Location;)V
    .locals 9

    const-string v0, "ColoplApplication"

    .line 264
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onLocationChanged = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Landroid/location/Location;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    .line 271
    invoke-virtual {p1}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    const-string v2, "gps"

    .line 276
    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    const-string v3, "network"

    .line 277
    invoke-virtual {v0, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    .line 278
    invoke-static {p1}, Ljp/colopl/util/LocationUtil;->isCellbasedLocation(Landroid/location/Location;)Z

    move-result v4

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    .line 281
    :goto_0
    new-instance v5, Ljp/colopl/libs/LocationExtras;

    invoke-direct {v5}, Ljp/colopl/libs/LocationExtras;-><init>()V

    const/4 v6, 0x1

    if-eqz v3, :cond_2

    .line 284
    invoke-virtual {v5, p1}, Ljp/colopl/libs/LocationExtras;->setLocation(Landroid/location/Location;)V

    .line 287
    iget-object v7, p0, Ljp/colopl/drapro/ColoplApplication;->firstNetworkLocation:Landroid/location/Location;

    if-nez v7, :cond_2

    .line 288
    invoke-virtual {v5}, Ljp/colopl/libs/LocationExtras;->getLocationSource()I

    move-result v7

    const/4 v8, 0x2

    if-ne v7, v8, :cond_1

    .line 289
    invoke-virtual {v5}, Ljp/colopl/libs/LocationExtras;->getLocationType()I

    move-result v5

    if-ne v5, v8, :cond_1

    .line 290
    invoke-virtual {p1}, Landroid/location/Location;->hasAccuracy()Z

    move-result v5

    if-eqz v5, :cond_1

    .line 291
    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result v5

    const/high16 v7, 0x43fa0000    # 500.0f

    cmpg-float v5, v5, v7

    if-gez v5, :cond_1

    .line 292
    invoke-virtual {p1, v7}, Landroid/location/Location;->setAccuracy(F)V

    const-string v5, "ColoplApplication"

    const-string v7, "First Cached/Wifi Location. set Accuracyt to 500.0"

    .line 294
    invoke-static {v5, v7}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v5, 0x1

    goto :goto_1

    :cond_1
    const/4 v5, 0x0

    :goto_1
    const-string v7, "ColoplApplication"

    const-string v8, "First Network Location"

    .line 296
    invoke-static {v7, v8}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    .line 298
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->firstNetworkLocation:Landroid/location/Location;

    goto :goto_2

    :cond_2
    const/4 v5, 0x0

    :goto_2
    if-eqz v4, :cond_3

    const-string v2, "ColoplApplication"

    .line 303
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Cell based location (*"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v4, p0, Ljp/colopl/drapro/ColoplApplication;->receiveCdmaCellLocationCount:I

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v4, ")"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    .line 305
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestCellBasedLocation:Landroid/location/Location;

    goto/16 :goto_5

    :cond_3
    if-eqz v2, :cond_4

    const-string v2, "ColoplApplication"

    const-string v3, "Force To Update"

    .line 310
    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "ColoplApplication"

    const-string v3, "Available Location"

    .line 311
    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    :goto_3
    const/4 v1, 0x1

    goto :goto_5

    :cond_4
    if-eqz v3, :cond_8

    .line 315
    iget-object v2, p0, Ljp/colopl/drapro/ColoplApplication;->previousNWLocation:Landroid/location/Location;

    if-eqz v2, :cond_6

    iget-object v2, p0, Ljp/colopl/drapro/ColoplApplication;->previousNWLocation:Landroid/location/Location;

    invoke-static {p1, v2, v1}, Ljp/colopl/util/LocationUtil;->isSameLatLon(Landroid/location/Location;Landroid/location/Location;Z)Z

    move-result v2

    if-eqz v2, :cond_6

    .line 317
    iget p1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    add-int/2addr p1, v6

    iput p1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    .line 318
    iget p1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    const/4 v2, 0x3

    if-lt p1, v2, :cond_5

    iget-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestCellBasedLocation:Landroid/location/Location;

    if-eqz p1, :cond_5

    const-string p1, "ColoplApplication"

    const-string v2, "Same location. Use CellBasedLocation"

    .line 320
    invoke-static {p1, v2}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    .line 323
    iget-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestCellBasedLocation:Landroid/location/Location;

    goto :goto_3

    :cond_5
    const-string p1, "ColoplApplication"

    .line 330
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Same location count up ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ")"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 338
    :cond_6
    iget-object v2, p0, Ljp/colopl/drapro/ColoplApplication;->firstNetworkLocation:Landroid/location/Location;

    if-eq p1, v2, :cond_7

    const-string v2, "ColoplApplication"

    const-string v3, "Through Update Check"

    .line 342
    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_4

    :cond_7
    const/4 v6, 0x0

    .line 345
    :goto_4
    iput v1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    .line 346
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->previousNWLocation:Landroid/location/Location;

    const-string v2, "ColoplApplication"

    const-string v3, "Available Location"

    .line 348
    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_6

    :cond_8
    :goto_5
    const/4 v6, 0x0

    .line 356
    :goto_6
    invoke-direct {p0, p1, v1, v6}, Ljp/colopl/drapro/ColoplApplication;->updateLocation(Landroid/location/Location;ZZ)Z

    move-result v2

    if-eqz v2, :cond_9

    const-string v2, "ColoplApplication"

    const-string v3, "Update Location"

    .line 359
    invoke-static {v2, v3}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    .line 360
    iget-object v2, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-virtual {v2, v0, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 361
    new-instance v0, Landroid/content/Intent;

    const-string v2, "jp.colopl.action.CHANGED_LOCATION"

    invoke-direct {v0, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v2, "location"

    .line 362
    invoke-virtual {v0, v2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    const-string v2, "force"

    .line 363
    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string v1, "doubt"

    .line 364
    invoke-virtual {v0, v1, v5}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string v1, "adopt"

    .line 365
    invoke-virtual {v0, v1, v6}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 366
    invoke-virtual {p0, v0}, Ljp/colopl/drapro/ColoplApplication;->sendBroadcast(Landroid/content/Intent;)V

    .line 369
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestLocation:Landroid/location/Location;

    :cond_9
    return-void
.end method

.method public onProviderDisabled(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public onProviderEnabled(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public onStatusChanged(Ljava/lang/String;ILandroid/os/Bundle;)V
    .locals 0

    const-string p3, "network"

    .line 493
    invoke-virtual {p1, p3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p2, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x1

    :goto_0
    return-void
.end method

.method public receiveErrorDoCoMoLocationInfo(Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V
    .locals 1

    const/4 v0, 0x1

    .line 419
    iput-boolean v0, p0, Ljp/colopl/drapro/ColoplApplication;->isResultInFailureDoCoMOAPI:Z

    .line 420
    invoke-virtual {p1}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getResultInfo()Ljp/colopl/api/docomo/ResultInfo;

    move-result-object p1

    .line 421
    invoke-virtual {p1}, Ljp/colopl/api/docomo/ResultInfo;->getUrlForAuthorize()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->urlForAuthorizeDoCoMoAPI:Ljava/lang/String;

    return-void
.end method

.method public receiveFailedCdmaCellLocation(Ljp/colopl/libs/ColoplCellLocationListener;)V
    .locals 0

    .line 384
    invoke-static {p0, p1}, Ljp/colopl/libs/ColoplCellLocationListener;->stopListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V

    return-void
.end method

.method public receiveSuccessCdmaCellLocation(Ljp/colopl/libs/ColoplCellLocationListener;Landroid/location/Location;)V
    .locals 2

    .line 375
    iget v0, p0, Ljp/colopl/drapro/ColoplApplication;->receiveCdmaCellLocationCount:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Ljp/colopl/drapro/ColoplApplication;->receiveCdmaCellLocationCount:I

    .line 376
    iget v0, p0, Ljp/colopl/drapro/ColoplApplication;->receiveCdmaCellLocationCount:I

    const/4 v1, 0x3

    if-le v0, v1, :cond_0

    .line 377
    invoke-static {p0, p1}, Ljp/colopl/libs/ColoplCellLocationListener;->stopListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V

    .line 379
    :cond_0
    invoke-virtual {p0, p2}, Ljp/colopl/drapro/ColoplApplication;->onLocationChanged(Landroid/location/Location;)V

    return-void
.end method

.method public receiveSuccessDoCoMoLocationInfo(Ljp/colopl/api/docomo/DoCoMoLocationInfo;)V
    .locals 2

    const-string v0, "ColoplApplication"

    const-string v1, "receiveSuccessDoCoMoLocationInfo"

    .line 410
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 411
    invoke-virtual {p1}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getFeature()Ljp/colopl/api/docomo/Feature;

    move-result-object p1

    .line 412
    invoke-virtual {p1}, Ljp/colopl/api/docomo/Feature;->getLocation()Landroid/location/Location;

    move-result-object p1

    const/4 v0, 0x0

    .line 413
    iput-boolean v0, p0, Ljp/colopl/drapro/ColoplApplication;->isResultInFailureDoCoMOAPI:Z

    .line 414
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/ColoplApplication;->onLocationChanged(Landroid/location/Location;)V

    return-void
.end method

.method public releaseWakeLock()Z
    .locals 2

    .line 174
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    if-eqz v0, :cond_0

    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->isHeld()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "ColoplApplication"

    const-string v1, "====== releaseWakeLock ======"

    .line 175
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 176
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->wakelock:Landroid/os/PowerManager$WakeLock;

    invoke-virtual {v0}, Landroid/os/PowerManager$WakeLock;->release()V

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public requestDoCoMoLocationInfo()Z
    .locals 2

    .line 389
    invoke-static {p0}, Ljp/colopl/api/docomo/DoCoMoAPI;->isAvailableDoCoMoAPI(Ljp/colopl/drapro/ColoplApplication;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 392
    :cond_0
    iput-boolean v1, p0, Ljp/colopl/drapro/ColoplApplication;->isResultInFailureDoCoMOAPI:Z

    .line 393
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->clearDoCoMoLocationInfo()V

    .line 394
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->cleanDoCoMoAsyncTask()V

    .line 395
    new-instance v0, Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    invoke-direct {v0, p0}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    .line 396
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    invoke-virtual {v0, p0}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->setDelegate(Ljp/colopl/api/docomo/DoCoMoAsyncTaskDelegate;)V

    .line 397
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->docomoAsyncTask:Ljp/colopl/api/docomo/DoCoMoAsyncTask;

    new-array v1, v1, [Ljava/lang/Void;

    invoke-virtual {v0, v1}, Ljp/colopl/api/docomo/DoCoMoAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    const/4 v0, 0x1

    return v0
.end method

.method public setConfigDirty(Z)V
    .locals 0

    .line 604
    iput-boolean p1, p0, Ljp/colopl/drapro/ColoplApplication;->isRequiredVersionDirty:Z

    return-void
.end method

.method public setImageUri(Landroid/net/Uri;)V
    .locals 0

    .line 600
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->imageUri:Landroid/net/Uri;

    return-void
.end method

.method public setUrlOfShowSPModeDialog(Ljava/lang/String;)V
    .locals 0

    .line 591
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->urlOfShowSPModeDialog:Ljava/lang/String;

    return-void
.end method

.method public startLocationMeasurement()I
    .locals 10

    .line 183
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->allowMockLocation()Z

    move-result v0

    const v1, 0x7f7fffff    # Float.MAX_VALUE

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    .line 184
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v3, "mock_location_not_allowed"

    const-string v4, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-static {p0, v0, v2}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    .line 185
    iput v1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    .line 186
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->clear()V

    const/4 v0, -0x2

    return v0

    .line 189
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object v0

    invoke-virtual {v0}, Ljp/colopl/config/Config;->getPreviousLocation()Landroid/location/Location;

    move-result-object v0

    iput-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->previousNWLocation:Landroid/location/Location;

    .line 191
    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    invoke-virtual {v0, v2}, Landroid/location/LocationManager;->getProviders(Z)Ljava/util/List;

    move-result-object v0

    const/4 v3, -0x1

    .line 193
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isCarrierSoftBank()Z

    move-result v4

    if-eqz v4, :cond_1

    .line 194
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isBothLocationProviderEnabled()Z

    move-result v3

    if-nez v3, :cond_4

    const/4 v0, -0x3

    return v0

    .line 201
    :cond_1
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isAnyLocationProviderEnabled()Z

    move-result v4

    if-nez v4, :cond_4

    .line 202
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isAvailableDoCoMoAPI()Z

    move-result v2

    if-eqz v2, :cond_2

    const/4 v2, 0x3

    goto :goto_0

    .line 205
    :cond_2
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isAUDevice()Z

    move-result v2

    if-eqz v2, :cond_3

    const/4 v2, 0x2

    goto :goto_0

    :cond_3
    return v3

    .line 216
    :cond_4
    :goto_0
    iput v1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracy:F

    const/4 v1, 0x0

    .line 217
    iput-object v1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracyProvider:Ljava/lang/String;

    .line 218
    iget-object v3, p0, Ljp/colopl/drapro/ColoplApplication;->locations:Ljava/util/HashMap;

    invoke-virtual {v3}, Ljava/util/HashMap;->clear()V

    .line 219
    iput-object v1, p0, Ljp/colopl/drapro/ColoplApplication;->latestCellBasedLocation:Landroid/location/Location;

    .line 220
    iput-object v1, p0, Ljp/colopl/drapro/ColoplApplication;->firstNetworkLocation:Landroid/location/Location;

    .line 221
    iput-object v1, p0, Ljp/colopl/drapro/ColoplApplication;->latestLocation:Landroid/location/Location;

    const/4 v1, 0x0

    .line 223
    iput v1, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    .line 226
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->acquireWakeLock()Z

    .line 227
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    move-object v5, v3

    check-cast v5, Ljava/lang/String;

    .line 228
    iget-object v4, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    const-wide/16 v6, 0x0

    const/4 v8, 0x0

    move-object v9, p0

    invoke-virtual/range {v4 .. v9}, Landroid/location/LocationManager;->requestLocationUpdates(Ljava/lang/String;JFLandroid/location/LocationListener;)V

    goto :goto_1

    .line 230
    :cond_5
    iput v1, p0, Ljp/colopl/drapro/ColoplApplication;->receiveCdmaCellLocationCount:I

    .line 231
    invoke-direct {p0}, Ljp/colopl/drapro/ColoplApplication;->getColoplCellLocationListener()Ljp/colopl/libs/ColoplCellLocationListener;

    move-result-object v0

    invoke-static {p0, v0}, Ljp/colopl/libs/ColoplCellLocationListener;->startListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V

    .line 232
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->requestDoCoMoLocationInfo()Z

    const-string v0, "ColoplApplication"

    const-string v1, "----- startLocationMeasurement -----"

    .line 233
    invoke-static {v0, v1}, Ljp/colopl/util/LogUtil;->v(Ljava/lang/String;Ljava/lang/String;)V

    return v2
.end method

.method public stopLocationMeasurement(Z)V
    .locals 1

    if-eqz p1, :cond_0

    .line 239
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->releaseWakeLock()Z

    .line 241
    :cond_0
    iget-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    if-eqz p1, :cond_1

    .line 242
    iget-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->locationManager:Landroid/location/LocationManager;

    invoke-virtual {p1, p0}, Landroid/location/LocationManager;->removeUpdates(Landroid/location/LocationListener;)V

    .line 244
    :cond_1
    invoke-direct {p0}, Ljp/colopl/drapro/ColoplApplication;->getColoplCellLocationListener()Ljp/colopl/libs/ColoplCellLocationListener;

    move-result-object p1

    invoke-static {p0, p1}, Ljp/colopl/libs/ColoplCellLocationListener;->stopListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V

    .line 245
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->cleanDoCoMoAsyncTask()V

    const/4 p1, 0x0

    .line 247
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->maxAccuracyProvider:Ljava/lang/String;

    .line 248
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestCellBasedLocation:Landroid/location/Location;

    .line 249
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->firstNetworkLocation:Landroid/location/Location;

    const/4 v0, 0x0

    .line 250
    iput v0, p0, Ljp/colopl/drapro/ColoplApplication;->sameLocationCount:I

    .line 251
    iput-object p1, p0, Ljp/colopl/drapro/ColoplApplication;->latestLocation:Landroid/location/Location;

    .line 253
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object p1

    iget-object v0, p0, Ljp/colopl/drapro/ColoplApplication;->previousNWLocation:Landroid/location/Location;

    invoke-virtual {p1, v0}, Ljp/colopl/config/Config;->setPreviousNWLocation(Landroid/location/Location;)V

    return-void
.end method
