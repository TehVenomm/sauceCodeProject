.class public Lcom/helpshift/configuration/dto/RootApiConfig;
.super Ljava/lang/Object;
.source "RootApiConfig.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/configuration/dto/RootApiConfig$RootApiConfigBuilder;,
        Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
    }
.end annotation


# instance fields
.field public final conversationPrefillText:Ljava/lang/String;

.field public final enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field public final enableDefaultConversationalFiling:Ljava/lang/Boolean;

.field public final enableFullPrivacy:Ljava/lang/Boolean;

.field public final enableTypingIndicator:Ljava/lang/Boolean;

.field public final gotoConversationAfterContactUs:Ljava/lang/Boolean;

.field public final hideNameAndEmail:Ljava/lang/Boolean;

.field public final initialUserMessageToAutoSend:Ljava/lang/String;

.field public final requireEmail:Ljava/lang/Boolean;

.field public final showConversationInfoScreen:Ljava/lang/Boolean;

.field public final showConversationResolutionQuestion:Ljava/lang/Boolean;

.field public final showSearchOnNewConversation:Ljava/lang/Boolean;


# direct methods
.method public constructor <init>(Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;Ljava/lang/String;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/String;)V
    .locals 0

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 35
    iput-object p7, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->enableContactUs:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    .line 36
    iput-object p1, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->gotoConversationAfterContactUs:Ljava/lang/Boolean;

    .line 37
    iput-object p2, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->requireEmail:Ljava/lang/Boolean;

    .line 38
    iput-object p3, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->hideNameAndEmail:Ljava/lang/Boolean;

    .line 39
    iput-object p8, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->conversationPrefillText:Ljava/lang/String;

    .line 40
    iput-object p4, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->enableFullPrivacy:Ljava/lang/Boolean;

    .line 41
    iput-object p5, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->showSearchOnNewConversation:Ljava/lang/Boolean;

    .line 42
    iput-object p6, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->showConversationResolutionQuestion:Ljava/lang/Boolean;

    .line 43
    iput-object p9, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->showConversationInfoScreen:Ljava/lang/Boolean;

    .line 44
    iput-object p10, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->enableTypingIndicator:Ljava/lang/Boolean;

    .line 45
    iput-object p11, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->enableDefaultConversationalFiling:Ljava/lang/Boolean;

    .line 46
    iput-object p12, p0, Lcom/helpshift/configuration/dto/RootApiConfig;->initialUserMessageToAutoSend:Ljava/lang/String;

    return-void
.end method
