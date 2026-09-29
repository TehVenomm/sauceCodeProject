.class public abstract Lcom/helpshift/widget/HSBaseObservable;
.super Ljava/lang/Object;
.source "HSBaseObservable.java"


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field private viewObserver:Lcom/helpshift/widget/HSObserver;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/widget/HSBaseObservable;)Lcom/helpshift/widget/HSObserver;
    .locals 0

    .line 11
    iget-object p0, p0, Lcom/helpshift/widget/HSBaseObservable;->viewObserver:Lcom/helpshift/widget/HSObserver;

    return-object p0
.end method


# virtual methods
.method public notifyChange(Ljava/lang/Object;)V
    .locals 2

    .line 45
    iget-object v0, p0, Lcom/helpshift/widget/HSBaseObservable;->viewObserver:Lcom/helpshift/widget/HSObserver;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/widget/HSBaseObservable;->domain:Lcom/helpshift/common/domain/Domain;

    if-eqz v0, :cond_0

    .line 46
    iget-object v0, p0, Lcom/helpshift/widget/HSBaseObservable;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/widget/HSBaseObservable$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/widget/HSBaseObservable$1;-><init>(Lcom/helpshift/widget/HSBaseObservable;Ljava/lang/Object;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method protected abstract notifyInitialState()V
.end method

.method public subscribe(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/widget/HSObserver;)V
    .locals 0

    .line 22
    iput-object p1, p0, Lcom/helpshift/widget/HSBaseObservable;->domain:Lcom/helpshift/common/domain/Domain;

    .line 23
    iput-object p2, p0, Lcom/helpshift/widget/HSBaseObservable;->viewObserver:Lcom/helpshift/widget/HSObserver;

    .line 24
    invoke-virtual {p0}, Lcom/helpshift/widget/HSBaseObservable;->notifyInitialState()V

    return-void
.end method

.method public unsubscribe()V
    .locals 1

    const/4 v0, 0x0

    .line 31
    iput-object v0, p0, Lcom/helpshift/widget/HSBaseObservable;->viewObserver:Lcom/helpshift/widget/HSObserver;

    return-void
.end method
