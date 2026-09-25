.class public Ljp/colopl/gcm/RegistrarHelper;
.super Ljava/lang/Object;
.source "RegistrarHelper.java"


# static fields
.field public static final GCM_SENDER_ID:Ljava/lang/String; = "463095322801"

.field private static final PLAY_SERVICES_RESOLUTION_REQUEST:I = 0x2328

.field private static final PROPERTY_APP_VERSION:Ljava/lang/String; = "appVersion"

.field public static final PROPERTY_REG_ID:Ljava/lang/String; = "registration_id"

.field public static activity:Landroid/app/Activity;

.field static context:Landroid/content/Context;

.field static gcm:Lcom/google/android/gms/gcm/GoogleCloudMessaging;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static CreateRegistrationId()V
    .locals 4

    .line 39
    sget-object v0, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    sput-object v0, Ljp/colopl/gcm/RegistrarHelper;->context:Landroid/content/Context;

    .line 42
    invoke-static {}, Ljp/colopl/gcm/RegistrarHelper;->checkPlayServices()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 44
    sget-object v0, Ljp/colopl/gcm/RegistrarHelper;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/gcm/RegistrarHelper;->getRegistrationId(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    const-string v1, ""

    .line 45
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 46
    sget-object v0, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    invoke-static {v0}, Lcom/google/android/gms/gcm/GoogleCloudMessaging;->getInstance(Landroid/content/Context;)Lcom/google/android/gms/gcm/GoogleCloudMessaging;

    move-result-object v0

    sput-object v0, Ljp/colopl/gcm/RegistrarHelper;->gcm:Lcom/google/android/gms/gcm/GoogleCloudMessaging;

    .line 47
    new-instance v0, Ljp/colopl/gcm/RegistrarHelper$1;

    invoke-direct {v0}, Ljp/colopl/gcm/RegistrarHelper$1;-><init>()V

    const/4 v1, 0x3

    new-array v1, v1, [Ljava/lang/Void;

    const/4 v2, 0x0

    const/4 v3, 0x0

    aput-object v3, v1, v2

    const/4 v2, 0x1

    aput-object v3, v1, v2

    const/4 v2, 0x2

    aput-object v3, v1, v2

    .line 69
    invoke-virtual {v0, v1}, Ljp/colopl/gcm/RegistrarHelper$1;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    goto :goto_0

    :cond_0
    const-string v1, "NativeReceiver"

    const-string v2, "GCMRegistered"

    .line 71
    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    const-string v0, ""

    const-string v1, "Google Play Services \u306f\u7121\u52b9"

    .line 74
    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "NativeReceiver"

    const-string v1, "GCMRegistered"

    const-string v2, "-1"

    .line 75
    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method private static checkPlayServices()Z
    .locals 3

    .line 81
    sget-object v0, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    invoke-static {v0}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->isGooglePlayServicesAvailable(Landroid/content/Context;)I

    move-result v0

    if-eqz v0, :cond_2

    .line 83
    invoke-static {v0}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->isUserRecoverableError(I)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 84
    sget-object v1, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    const/16 v2, 0x2328

    invoke-static {v0, v1, v2}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->getErrorDialog(ILandroid/app/Activity;I)Landroid/app/Dialog;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 86
    invoke-virtual {v0}, Landroid/app/Dialog;->show()V

    goto :goto_0

    .line 88
    :cond_0
    sget-object v0, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    const-string v1, "Something went wrong. Please make sure that you have the Play Store installed and that you are connected to the internet. Contact developer with details if this persists."

    invoke-static {v0, v1}, Ljp/colopl/gcm/RegistrarHelper;->showOkDialogWithText(Landroid/content/Context;Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    const-string v0, ""

    const-string v1, "Play Service not support"

    .line 91
    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    const/4 v0, 0x0

    return v0

    :cond_2
    const/4 v0, 0x1

    return v0
.end method

.method private static getRegistrationId(Landroid/content/Context;)Ljava/lang/String;
    .locals 4

    .line 100
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "registration_id"

    const-string v2, "false"

    .line 101
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, ""

    .line 102
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string p0, ""

    return-object p0

    :cond_0
    const-string v2, "appVersion"

    const/high16 v3, -0x80000000

    .line 107
    invoke-interface {v0, v2, v3}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v0

    .line 108
    invoke-static {p0}, Ljp/colopl/config/Config;->getVersionCode(Landroid/content/Context;)I

    move-result p0

    if-eq v0, p0, :cond_1

    const-string p0, ""

    return-object p0

    :cond_1
    const-string p0, "RegistrarHelper"

    .line 113
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "registration_version:"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    const-string p0, "RegistrarHelper"

    .line 114
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "registration_id:"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-object v1
.end method

.method public static init(Landroid/app/Activity;)V
    .locals 0

    .line 34
    sput-object p0, Ljp/colopl/gcm/RegistrarHelper;->activity:Landroid/app/Activity;

    return-void
.end method

.method public static showOkDialogWithText(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    .line 120
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 121
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    const/4 p0, 0x1

    .line 122
    invoke-virtual {v0, p0}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    const-string p0, "OK"

    const/4 p1, 0x0

    .line 123
    invoke-virtual {v0, p0, p1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 124
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    .line 125
    invoke-virtual {p0}, Landroid/app/AlertDialog;->show()V

    return-void
.end method
