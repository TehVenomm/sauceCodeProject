.class public Lcom/helpshift/support/flows/FAQsFlow;
.super Ljava/lang/Object;
.source "FAQsFlow.java"

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

    .line 33
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(ILjava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 55
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(ILjava/util/Map;)V

    return-void
.end method

.method public constructor <init>(ILjava/util/Map;)V
    .locals 0

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 43
    iput p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->labelResId:I

    .line 44
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 45
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->label:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 64
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Lcom/helpshift/support/ApiConfig;)V
    .locals 0
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Lcom/helpshift/support/ApiConfig;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 86
    invoke-static {p2}, Lcom/helpshift/support/util/ConfigUtil;->validateAndConvertToMap(Lcom/helpshift/support/ApiConfig;)Ljava/util/Map;

    move-result-object p2

    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 73
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 74
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->label:Ljava/lang/String;

    .line 75
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->config:Ljava/util/HashMap;

    const/4 p1, 0x0

    .line 76
    iput p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->labelResId:I

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 106
    iget-object v0, p0, Lcom/helpshift/support/flows/FAQsFlow;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getLabelResId()I
    .locals 1

    .line 101
    iget v0, p0, Lcom/helpshift/support/flows/FAQsFlow;->labelResId:I

    return v0
.end method

.method public performAction()V
    .locals 4

    .line 114
    iget-object v0, p0, Lcom/helpshift/support/flows/FAQsFlow;->config:Ljava/util/HashMap;

    const-string v1, "customContactUsFlows"

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/List;

    .line 115
    iget-object v1, p0, Lcom/helpshift/support/flows/FAQsFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    iget-object v2, p0, Lcom/helpshift/support/flows/FAQsFlow;->config:Ljava/util/HashMap;

    invoke-static {v2}, Lcom/helpshift/support/SupportInternal;->cleanConfig(Ljava/util/HashMap;)Landroid/os/Bundle;

    move-result-object v2

    const/4 v3, 0x1

    invoke-virtual {v1, v2, v3, v0}, Lcom/helpshift/support/controllers/SupportController;->startFaqFlow(Landroid/os/Bundle;ZLjava/util/List;)V

    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 93
    iput-object p1, p0, Lcom/helpshift/support/flows/FAQsFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method
