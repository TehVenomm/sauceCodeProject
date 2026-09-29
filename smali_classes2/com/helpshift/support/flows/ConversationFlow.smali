.class public Lcom/helpshift/support/flows/ConversationFlow;
.super Ljava/lang/Object;
.source "ConversationFlow.java"

# interfaces
.implements Lcom/helpshift/support/flows/Flow;


# instance fields
.field private final config:Ljava/util/HashMap;

.field private final label:Ljava/lang/String;

.field private final labelResId:I

.field private supportController:Lcom/helpshift/support/controllers/SupportController;


# direct methods
.method public constructor <init>(I)V
    .locals 1

    .line 36
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(ILjava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 58
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(ILjava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILjava/util/Map;)V
    .locals 0

    .line 45
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    iput p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->labelResId:I

    .line 47
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 48
    iput-object p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->label:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 67
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 89
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 76
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 77
    iput-object p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->label:Ljava/lang/String;

    .line 78
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 79
    iput p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->labelResId:I

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 109
    iget-object v0, p0, Lcom/helpshift/support/flows/ConversationFlow;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getLabelResId()I
    .locals 1

    .line 104
    iget v0, p0, Lcom/helpshift/support/flows/ConversationFlow;->labelResId:I

    return v0
.end method

.method public performAction()V
    .locals 3

    .line 117
    iget-object v0, p0, Lcom/helpshift/support/flows/ConversationFlow;->config:Ljava/util/HashMap;

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeShowConversationUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    .line 118
    iget-object v1, p0, Lcom/helpshift/support/flows/ConversationFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    const/4 v2, 0x1

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/support/controllers/SupportController;->startConversationFlow(Landroid/os/Bundle;Z)V

    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 96
    iput-object p1, p0, Lcom/helpshift/support/flows/ConversationFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method
