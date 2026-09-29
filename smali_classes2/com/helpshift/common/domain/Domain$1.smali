.class Lcom/helpshift/common/domain/Domain$1;
.super Lcom/helpshift/common/domain/F;
.source "Domain.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/domain/Domain;

.field final synthetic val$f:Lcom/helpshift/common/domain/F;


# direct methods
.method constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/domain/F;)V
    .locals 0

    .line 200
    iput-object p1, p0, Lcom/helpshift/common/domain/Domain$1;->this$0:Lcom/helpshift/common/domain/Domain;

    iput-object p2, p0, Lcom/helpshift/common/domain/Domain$1;->val$f:Lcom/helpshift/common/domain/F;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 203
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain$1;->this$0:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain$1;->val$f:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method
