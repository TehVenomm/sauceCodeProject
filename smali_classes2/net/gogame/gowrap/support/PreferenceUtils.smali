.class public final Lnet/gogame/gowrap/support/PreferenceUtils;
.super Ljava/lang/Object;
.source "PreferenceUtils.java"


# static fields
.field private static final SHARED_PREFERENCES_NAME:Ljava/lang/String; = "net.gogame.gowrap.preferences"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static clearPreferences(Landroid/content/Context;)V
    .locals 2

    const-string v0, "net.gogame.gowrap.preferences"

    const/4 v1, 0x0

    .line 30
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 32
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 34
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->clear()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 35
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public static getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "net.gogame.gowrap.preferences"

    const/4 v1, 0x0

    .line 16
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    const/4 v0, 0x0

    .line 18
    invoke-interface {p0, p1, v0}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    const-string v0, "net.gogame.gowrap.preferences"

    const/4 v1, 0x0

    .line 22
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 24
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 25
    invoke-interface {p0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 26
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method
