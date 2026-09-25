.class Lcom/helpshift/redaction/RedactionManager$2;
.super Lcom/helpshift/common/domain/F;
.source "RedactionManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/redaction/RedactionManager;->updateUserRedactionState(Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/redaction/RedactionManager;

.field final synthetic val$newState:Lcom/helpshift/redaction/RedactionState;

.field final synthetic val$oldState:Lcom/helpshift/redaction/RedactionState;


# direct methods
.method constructor <init>(Lcom/helpshift/redaction/RedactionManager;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V
    .locals 0

    .line 100
    iput-object p1, p0, Lcom/helpshift/redaction/RedactionManager$2;->this$0:Lcom/helpshift/redaction/RedactionManager;

    iput-object p2, p0, Lcom/helpshift/redaction/RedactionManager$2;->val$oldState:Lcom/helpshift/redaction/RedactionState;

    iput-object p3, p0, Lcom/helpshift/redaction/RedactionManager$2;->val$newState:Lcom/helpshift/redaction/RedactionState;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 103
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionManager$2;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v0}, Lcom/helpshift/redaction/RedactionManager;->access$300(Lcom/helpshift/redaction/RedactionManager;)Ljava/lang/ref/WeakReference;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;

    if-eqz v0, :cond_0

    .line 105
    iget-object v1, p0, Lcom/helpshift/redaction/RedactionManager$2;->this$0:Lcom/helpshift/redaction/RedactionManager;

    invoke-static {v1}, Lcom/helpshift/redaction/RedactionManager;->access$100(Lcom/helpshift/redaction/RedactionManager;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/redaction/RedactionManager$2;->val$oldState:Lcom/helpshift/redaction/RedactionState;

    iget-object v3, p0, Lcom/helpshift/redaction/RedactionManager$2;->val$newState:Lcom/helpshift/redaction/RedactionState;

    invoke-interface {v0, v1, v2, v3}, Lcom/helpshift/redaction/RedactionManager$RedactionManagerListener;->redactionStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionState;)V

    :cond_0
    return-void
.end method
