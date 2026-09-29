.class public Ljp/colopl/drapro/GooglePlayGamesPlugin;
.super Ljava/lang/Object;
.source "GooglePlayGamesPlugin.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static connect(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 20
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->connect()V

    return-void
.end method

.method public static isConnected(Ljp/colopl/drapro/StartActivity;)Z
    .locals 0

    .line 28
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->isConnected()Z

    move-result p0

    return p0
.end method

.method public static isGooglePlayServicesAvailable(Landroid/app/Activity;)Z
    .locals 4

    .line 41
    move-object v0, p0

    check-cast v0, Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->isGooglePlayServicesAvailable(Landroid/content/Context;)I

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    return v1

    .line 44
    :cond_0
    invoke-static {v0}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->isUserRecoverableError(I)Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_1

    .line 45
    invoke-static {v0, p0, v3}, Lcom/google/android/gms/common/GooglePlayServicesUtil;->getErrorDialog(ILandroid/app/Activity;I)Landroid/app/Dialog;

    move-result-object p0

    if-eqz p0, :cond_2

    .line 47
    invoke-virtual {p0}, Landroid/app/Dialog;->show()V

    goto :goto_0

    :cond_1
    const-string v0, "Google Play Services is not available."

    .line 50
    invoke-static {p0, v0, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p0

    invoke-virtual {p0}, Landroid/widget/Toast;->show()V

    :cond_2
    :goto_0
    return v3
.end method

.method public static showAchievementsList(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 24
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->showAchievementsList()V

    return-void
.end method

.method public static signin(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 16
    invoke-virtual {p0}, Ljp/colopl/drapro/StartActivity;->signin()V

    return-void
.end method

.method public static syncUnlockedAchievements(Ljp/colopl/drapro/StartActivity;[Ljava/lang/String;)V
    .locals 1

    .line 36
    new-instance v0, Ljava/util/ArrayList;

    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->syncUnlockedAchievements(Ljava/util/List;)V

    return-void
.end method

.method public static unlockAchievement(Ljp/colopl/drapro/StartActivity;Ljava/lang/String;)V
    .locals 0

    .line 32
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->unlockAchievement(Ljava/lang/String;)V

    return-void
.end method
