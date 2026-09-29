.class public Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;
.super Ljava/lang/Object;
.source "RootApiConfig.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/configuration/dto/RootApiConfig;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "RootApiConfigBuilder"
.end annotation


# instance fields
.field private conversationPrefillText:Ljava/lang/String;

.field private enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field private enableDefaultConversationalFiling:Ljava/lang/Boolean;

.field private enableFullPrivacy:Ljava/lang/Boolean;

.field private enableTypingIndicator:Ljava/lang/Boolean;

.field private gotoConversationAfterContactUs:Ljava/lang/Boolean;

.field private hideNameAndEmail:Ljava/lang/Boolean;

.field private initialUserMessageToAutoSend:Ljava/lang/String;

.field private requireEmail:Ljava/lang/Boolean;

.field private showConversationInfoScreen:Ljava/lang/Boolean;

.field private showConversationResolutionQuestion:Ljava/lang/Boolean;

.field private showSearchOnNewConversation:Ljava/lang/Boolean;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 75
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, ""

    .line 83
    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    const-string v0, ""

    .line 87
    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public applyMap(Ljava/util/Map;)Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;"
        }
    .end annotation

    const-string v0, "enableContactUs"

    .line 92
    const-class v1, Ljava/lang/Integer;

    const/4 v2, 0x0

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Integer;

    if-eqz v0, :cond_0

    .line 94
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    invoke-static {v0}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->fromInt(I)Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    :cond_0
    const-string v0, ""

    const-string v1, "gotoConversationAfterContactUs"

    .line 98
    invoke-interface {p1, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    const-string v0, "gotoConversationAfterContactUs"

    goto :goto_0

    :cond_1
    const-string v1, "gotoCoversationAfterContactUs"

    .line 101
    invoke-interface {p1, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    const-string v0, "gotoCoversationAfterContactUs"

    .line 105
    :cond_2
    :goto_0
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->gotoConversationAfterContactUs:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->gotoConversationAfterContactUs:Ljava/lang/Boolean;

    const-string v0, "requireEmail"

    .line 109
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->requireEmail:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->requireEmail:Ljava/lang/Boolean;

    const-string v0, "hideNameAndEmail"

    .line 111
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->hideNameAndEmail:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->hideNameAndEmail:Ljava/lang/Boolean;

    const-string v0, "enableFullPrivacy"

    .line 113
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableFullPrivacy:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableFullPrivacy:Ljava/lang/Boolean;

    const-string v0, "showSearchOnNewConversation"

    .line 115
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showSearchOnNewConversation:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showSearchOnNewConversation:Ljava/lang/Boolean;

    const-string v0, "showConversationResolutionQuestion"

    .line 118
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationResolutionQuestion:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationResolutionQuestion:Ljava/lang/Boolean;

    const-string v0, "conversationPrefillText"

    .line 121
    const-class v1, Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    .line 124
    iget-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    const-string v0, ""

    .line 125
    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    :cond_3
    const-string v0, "showConversationInfoScreen"

    .line 128
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationInfoScreen:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationInfoScreen:Ljava/lang/Boolean;

    const-string v0, "enableTypingIndicator"

    .line 131
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableTypingIndicator:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableTypingIndicator:Ljava/lang/Boolean;

    const-string v0, "enableDefaultConversationalFiling"

    .line 134
    const-class v1, Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableDefaultConversationalFiling:Ljava/lang/Boolean;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableDefaultConversationalFiling:Ljava/lang/Boolean;

    const-string v0, "initialUserMessage"

    .line 138
    const-class v1, Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    invoke-static {p1, v0, v1, v2}, Lcom/helpshift/common/util/MapUtil;->getValue(Ljava/util/Map;Ljava/lang/String;Ljava/lang/Class;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    .line 141
    iget-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    .line 142
    iget-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_4

    const-string p1, ""

    .line 143
    iput-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    :cond_4
    return-object p0
.end method

.method public build()Lcom/helpshift/configuration/dto/RootApiConfig;
    .locals 14

    .line 150
    new-instance v13, Lcom/helpshift/configuration/dto/RootApiConfig;

    iget-object v1, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->gotoConversationAfterContactUs:Ljava/lang/Boolean;

    iget-object v2, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->requireEmail:Ljava/lang/Boolean;

    iget-object v3, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->hideNameAndEmail:Ljava/lang/Boolean;

    iget-object v4, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableFullPrivacy:Ljava/lang/Boolean;

    iget-object v5, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showSearchOnNewConversation:Ljava/lang/Boolean;

    iget-object v6, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationResolutionQuestion:Ljava/lang/Boolean;

    iget-object v7, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    iget-object v8, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->conversationPrefillText:Ljava/lang/String;

    iget-object v9, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->showConversationInfoScreen:Ljava/lang/Boolean;

    iget-object v10, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableTypingIndicator:Ljava/lang/Boolean;

    iget-object v11, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->enableDefaultConversationalFiling:Ljava/lang/Boolean;

    iget-object v12, p0, Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;->initialUserMessageToAutoSend:Ljava/lang/String;

    move-object v0, v13

    invoke-direct/range {v0 .. v12}, Lcom/helpshift/configuration/dto/RootApiConfig;-><init>(Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;Ljava/lang/String;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/String;)V

    return-object v13
.end method
