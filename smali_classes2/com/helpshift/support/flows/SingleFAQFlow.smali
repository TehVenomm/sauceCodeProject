.class public Lcom/helpshift/support/flows/SingleFAQFlow;
.super Ljava/lang/Object;
.source "SingleFAQFlow.java"

# interfaces
.implements Lcom/helpshift/support/flows/Flow;


# instance fields
.field private final config:Ljava/util/HashMap;

.field private final label:Ljava/lang/String;

.field private final labelResId:I

.field private final questionPublishId:Ljava/lang/String;

.field private supportController:Lcom/helpshift/support/controllers/SupportController;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 1

    .line 42
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILjava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 67
    invoke-static {p3}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p3

    invoke-direct {p0, p1, p2, p3}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILjava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 53
    iput p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->labelResId:I

    .line 54
    iput-object p2, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->questionPublishId:Ljava/lang/String;

    .line 55
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p3}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 56
    iput-object p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->label:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 77
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p3    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 102
    invoke-static {p3}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p3

    invoke-direct {p0, p1, p2, p3}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 87
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 88
    iput-object p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->label:Ljava/lang/String;

    .line 89
    iput-object p2, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->questionPublishId:Ljava/lang/String;

    .line 90
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p3}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 91
    iput p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->labelResId:I

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 122
    iget-object v0, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getLabelResId()I
    .locals 1

    .line 117
    iget v0, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->labelResId:I

    return v0
.end method

.method public performAction()V
    .locals 4

    .line 130
    iget-object v0, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->config:Ljava/util/HashMap;

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "questionPublishId"

    .line 131
    iget-object v2, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->questionPublishId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "support_mode"

    const/4 v2, 0x3

    .line 132
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v1, "decomp"

    const/4 v2, 0x1

    .line 133
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 134
    iget-object v1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->config:Ljava/util/HashMap;

    const-string v3, "customContactUsFlows"

    invoke-virtual {v1, v3}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/List;

    .line 135
    iget-object v3, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v3, v0, v2, v1}, Lcom/helpshift/support/controllers/SupportController;->startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V

    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 109
    iput-object p1, p0, Lcom/helpshift/support/flows/SingleFAQFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method
