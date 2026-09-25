.class public Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;
.super Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.source "CustomZopimSupport.java"

# interfaces
.implements Lnet/gogame/gowrap/integrations/CanChat;


# static fields
.field public static final CONFIG_ACCOUNT_KEY:Ljava/lang/String; = "accountKey"


# instance fields
.field private integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "zopim"

    .line 23
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method protected doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 0

    .line 33
    iput-object p3, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    const-string p3, "accountKey"

    .line 35
    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 37
    invoke-static {p1, p2}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->initChat(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method public isIntegrated()Z
    .locals 1

    .line 28
    invoke-static {}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->isIntegrated()Z

    move-result v0

    return v0
.end method

.method public startChat()V
    .locals 6

    .line 42
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    .line 44
    iget-object v1, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->isChatBotEnabled()Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    .line 45
    new-instance v1, Lnet/gogame/chat/chatbot/ChatBotConfig;

    invoke-direct {v1}, Lnet/gogame/chat/chatbot/ChatBotConfig;-><init>()V

    .line 46
    iget-object v3, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v3}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getGuid()Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_0

    .line 48
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "_"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v4

    invoke-virtual {v3, v4, v5}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    .line 50
    :cond_0
    iget-object v4, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v4}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getAppId()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v4}, Lnet/gogame/chat/chatbot/ChatBotConfig;->setAppId(Ljava/lang/String;)V

    .line 51
    invoke-virtual {v1, v3}, Lnet/gogame/chat/chatbot/ChatBotConfig;->setGuid(Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    move-object v1, v2

    .line 54
    :goto_0
    iget-object v3, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v3}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->isVip()Z

    move-result v3

    if-nez v3, :cond_2

    iget-object v3, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v3}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->isForceEnableChat()Z

    move-result v3

    if-eqz v3, :cond_3

    .line 55
    :cond_2
    iget-object v2, p0, Lnet/gogame/gowrap/integrations/zopim/CustomZopimSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    .line 56
    invoke-interface {v2}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getGuid()Ljava/lang/String;

    move-result-object v2

    .line 55
    invoke-static {v0, v2}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->getSessionConfig(Landroid/content/Context;Ljava/lang/String;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    move-result-object v2

    :cond_3
    if-nez v1, :cond_4

    if-nez v2, :cond_4

    return-void

    .line 61
    :cond_4
    invoke-static {v0, v1, v2}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->startActivity(Landroid/content/Context;Lnet/gogame/chat/chatbot/ChatBotConfig;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)V

    return-void
.end method
