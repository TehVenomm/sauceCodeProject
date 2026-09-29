.class public Lcom/helpshift/Core;
.super Ljava/lang/Object;
.source "Core.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/Core$ApiProvider;
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static clearAnonymousUser()V
    .locals 0

    .line 295
    invoke-static {}, Lcom/helpshift/CoreInternal;->clearAnonymousUser()V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 242
    invoke-static {p0, p1}, Lcom/helpshift/CoreInternal;->handlePush(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Landroid/os/Bundle;)V
    .locals 0

    .line 246
    invoke-static {p0, p1}, Lcom/helpshift/CoreInternal;->handlePush(Landroid/content/Context;Landroid/os/Bundle;)V

    return-void
.end method

.method public static handlePush(Landroid/content/Context;Ljava/util/Map;)V
    .locals 0
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

    .line 250
    invoke-static {p0, p1}, Lcom/helpshift/CoreInternal;->handlePush(Landroid/content/Context;Ljava/util/Map;)V

    return-void
.end method

.method public static init(Lcom/helpshift/Core$ApiProvider;)V
    .locals 0

    .line 33
    invoke-static {p0}, Lcom/helpshift/CoreInternal;->init(Lcom/helpshift/Core$ApiProvider;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 71
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-static {p0, p1, p2, p3, v0}, Lcom/helpshift/Core;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/InstallConfig;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .line 155
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    if-eqz p4, :cond_0

    .line 157
    invoke-virtual {p4}, Lcom/helpshift/InstallConfig;->toMap()Ljava/util/Map;

    move-result-object p4

    invoke-interface {v0, p4}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 159
    :cond_0
    invoke-static {p0, p1, p2, p3, v0}, Lcom/helpshift/Core;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0
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

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/exceptions/InstallException;
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 126
    invoke-static {p0, p1, p2, p3, p4}, Lcom/helpshift/CoreInternal;->install(Landroid/app/Application;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public static login(Lcom/helpshift/HelpshiftUser;)V
    .locals 0

    .line 278
    invoke-static {p0}, Lcom/helpshift/CoreInternal;->login(Lcom/helpshift/HelpshiftUser;)V

    return-void
.end method

.method public static login(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 265
    new-instance v0, Lcom/helpshift/HelpshiftUser$Builder;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/HelpshiftUser$Builder;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 266
    invoke-virtual {v0, p1}, Lcom/helpshift/HelpshiftUser$Builder;->setName(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;

    move-result-object p0

    invoke-virtual {p0}, Lcom/helpshift/HelpshiftUser$Builder;->build()Lcom/helpshift/HelpshiftUser;

    move-result-object p0

    .line 267
    invoke-static {p0}, Lcom/helpshift/CoreInternal;->login(Lcom/helpshift/HelpshiftUser;)V

    return-void
.end method

.method public static logout()V
    .locals 0

    .line 287
    invoke-static {}, Lcom/helpshift/CoreInternal;->logout()V

    return-void
.end method

.method public static registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 193
    invoke-static {p0, p1}, Lcom/helpshift/CoreInternal;->registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method public static setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 52
    invoke-static {p0, p1}, Lcom/helpshift/CoreInternal;->setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static setSDKLanguage(Ljava/lang/String;)V
    .locals 0

    .line 322
    invoke-static {p0}, Lcom/helpshift/CoreInternal;->setSDKLanguage(Ljava/lang/String;)V

    return-void
.end method

.method public static setTheme(I)V
    .locals 0
    .param p0    # I
        .annotation build Landroidx/annotation/StyleRes;
        .end annotation
    .end param

    .line 345
    invoke-static {p0}, Lcom/helpshift/CoreInternal;->setTheme(I)V

    return-void
.end method
