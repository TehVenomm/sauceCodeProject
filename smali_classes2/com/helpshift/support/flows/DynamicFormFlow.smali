.class public Lcom/helpshift/support/flows/DynamicFormFlow;
.super Ljava/lang/Object;
.source "DynamicFormFlow.java"

# interfaces
.implements Lcom/helpshift/support/flows/Flow;


# instance fields
.field private final flowList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private final label:Ljava/lang/String;

.field private final labelResId:I

.field private supportController:Lcom/helpshift/support/controllers/SupportController;


# direct methods
.method public constructor <init>(ILjava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    iput p1, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->labelResId:I

    .line 29
    iput-object p2, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->flowList:Ljava/util/List;

    const/4 p1, 0x0

    .line 30
    iput-object p1, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->label:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    .line 39
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    iput-object p1, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->label:Ljava/lang/String;

    .line 41
    iput-object p2, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->flowList:Ljava/util/List;

    const/4 p1, 0x0

    .line 42
    iput p1, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->labelResId:I

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 62
    iget-object v0, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getLabelResId()I
    .locals 1

    .line 57
    iget v0, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->labelResId:I

    return v0
.end method

.method public performAction()V
    .locals 4

    .line 71
    iget v0, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->labelResId:I

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 72
    iget-object v0, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    iget v2, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->labelResId:I

    iget-object v3, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->flowList:Ljava/util/List;

    invoke-virtual {v0, v2, v3, v1}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(ILjava/util/List;Z)V

    goto :goto_0

    .line 75
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    iget-object v2, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->label:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->flowList:Ljava/util/List;

    invoke-virtual {v0, v2, v3, v1}, Lcom/helpshift/support/controllers/SupportController;->startDynamicForm(Ljava/lang/String;Ljava/util/List;Z)V

    :goto_0
    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 49
    iput-object p1, p0, Lcom/helpshift/support/flows/DynamicFormFlow;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method
