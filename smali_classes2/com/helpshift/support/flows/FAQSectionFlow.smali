.class public Lcom/helpshift/support/flows/FAQSectionFlow;
.super Ljava/lang/Object;
.source "FAQSectionFlow.java"

# interfaces
.implements Lcom/helpshift/support/flows/Flow;


# instance fields
.field private final config:Ljava/util/HashMap;

.field private final label:Ljava/lang/String;

.field private final labelResId:I

.field private final sectionPublishId:Ljava/lang/String;

.field private supportController:Lcom/helpshift/support/controllers/SupportController;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 1

    .line 40
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

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

    .line 65
    invoke-static {p3}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p3

    invoke-direct {p0, p1, p2, p3}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILjava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 50
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 51
    iput p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->labelResId:I

    .line 52
    iput-object p2, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->sectionPublishId:Ljava/lang/String;

    .line 53
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p3}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 54
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->label:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 75
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

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

    .line 100
    invoke-static {p3}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p3

    invoke-direct {p0, p1, p2, p3}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 85
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 86
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->label:Ljava/lang/String;

    .line 87
    iput-object p2, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->sectionPublishId:Ljava/lang/String;

    .line 88
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p3}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 89
    iput p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->labelResId:I

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 120
    iget-object v0, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getLabelResId()I
    .locals 1

    .line 115
    iget v0, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->labelResId:I

    return v0
.end method

.method public performAction()V
    .locals 4

    .line 128
    iget-object v0, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->config:Ljava/util/HashMap;

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->removeFAQFlowUnsupportedConfigs(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "sectionPublishId"

    .line 129
    iget-object v2, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->sectionPublishId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "support_mode"

    const/4 v2, 0x2

    .line 130
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    .line 131
    iget-object v1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->config:Ljava/util/HashMap;

    const-string v2, "customContactUsFlows"

    invoke-virtual {v1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/List;

    .line 132
    iget-object v2, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    const/4 v3, 0x1

    invoke-virtual {v2, v0, v3, v1}, Lcom/helpshift/support/controllers/SupportController;->startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V

    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 107
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQSectionFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method
