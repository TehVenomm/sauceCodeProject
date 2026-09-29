.class Lcom/helpshift/widget/HSBaseObservable$1;
.super Lcom/helpshift/common/domain/F;
.source "HSBaseObservable.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/widget/HSBaseObservable;->notifyChange(Ljava/lang/Object;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/widget/HSBaseObservable;

.field final synthetic val$viewState:Ljava/lang/Object;


# direct methods
.method constructor <init>(Lcom/helpshift/widget/HSBaseObservable;Ljava/lang/Object;)V
    .locals 0

    .line 46
    iput-object p1, p0, Lcom/helpshift/widget/HSBaseObservable$1;->this$0:Lcom/helpshift/widget/HSBaseObservable;

    iput-object p2, p0, Lcom/helpshift/widget/HSBaseObservable$1;->val$viewState:Ljava/lang/Object;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 49
    iget-object v0, p0, Lcom/helpshift/widget/HSBaseObservable$1;->this$0:Lcom/helpshift/widget/HSBaseObservable;

    invoke-static {v0}, Lcom/helpshift/widget/HSBaseObservable;->access$000(Lcom/helpshift/widget/HSBaseObservable;)Lcom/helpshift/widget/HSObserver;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 50
    iget-object v0, p0, Lcom/helpshift/widget/HSBaseObservable$1;->this$0:Lcom/helpshift/widget/HSBaseObservable;

    invoke-static {v0}, Lcom/helpshift/widget/HSBaseObservable;->access$000(Lcom/helpshift/widget/HSBaseObservable;)Lcom/helpshift/widget/HSObserver;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/widget/HSBaseObservable$1;->val$viewState:Ljava/lang/Object;

    invoke-interface {v0, v1}, Lcom/helpshift/widget/HSObserver;->onChanged(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
