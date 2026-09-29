.class final Lcom/helpshift/support/Support$1;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$failure:Landroid/os/Handler;

.field final synthetic val$success:Landroid/os/Handler;


# direct methods
.method constructor <init>(Landroid/os/Handler;Landroid/os/Handler;)V
    .locals 0

    .line 171
    iput-object p1, p0, Lcom/helpshift/support/Support$1;->val$success:Landroid/os/Handler;

    iput-object p2, p0, Lcom/helpshift/support/Support$1;->val$failure:Landroid/os/Handler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 174
    iget-object v0, p0, Lcom/helpshift/support/Support$1;->val$success:Landroid/os/Handler;

    iget-object v1, p0, Lcom/helpshift/support/Support$1;->val$failure:Landroid/os/Handler;

    invoke-static {v0, v1}, Lcom/helpshift/support/SupportInternal;->getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V

    return-void
.end method
