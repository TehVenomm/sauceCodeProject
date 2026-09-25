.class final Lcom/helpshift/support/Support$3;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->leaveBreadCrumb(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$breadCrumb:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 218
    iput-object p1, p0, Lcom/helpshift/support/Support$3;->val$breadCrumb:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 221
    iget-object v0, p0, Lcom/helpshift/support/Support$3;->val$breadCrumb:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->leaveBreadCrumb(Ljava/lang/String;)V

    return-void
.end method
