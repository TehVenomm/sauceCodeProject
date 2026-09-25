.class public interface abstract Lcom/helpshift/Core$ApiProvider;
.super Ljava/lang/Object;
.source "Core.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/Core;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "ApiProvider"
.end annotation


# virtual methods
.method public abstract _clearAnonymousUser()Z
.end method

.method public abstract _getActionExecutor()Lcom/helpshift/executors/ActionExecutor;
.end method

.method public abstract _handlePush(Landroid/content/Context;Landroid/content/Intent;)V
.end method

.method public abstract _install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract _install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract _login(Lcom/helpshift/HelpshiftUser;)Z
.end method

.method public abstract _logout()Z
.end method

.method public abstract _preInstall(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Application;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract _registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
.end method

.method public abstract _setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract _setSDKLanguage(Ljava/lang/String;)V
.end method

.method public abstract _setTheme(I)V
.end method
