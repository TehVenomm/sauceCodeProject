.class final Lcom/helpshift/support/Support$12;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->setDelegate(Lcom/helpshift/support/Support$Delegate;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$delegate:Lcom/helpshift/support/Support$Delegate;


# direct methods
.method constructor <init>(Lcom/helpshift/support/Support$Delegate;)V
    .locals 0

    .line 1003
    iput-object p1, p0, Lcom/helpshift/support/Support$12;->val$delegate:Lcom/helpshift/support/Support$Delegate;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 1006
    iget-object v0, p0, Lcom/helpshift/support/Support$12;->val$delegate:Lcom/helpshift/support/Support$Delegate;

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->setDelegate(Lcom/helpshift/delegate/RootDelegate;)V

    return-void
.end method
