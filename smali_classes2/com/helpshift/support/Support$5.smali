.class final Lcom/helpshift/support/Support$5;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->showConversation(Landroid/app/Activity;Ljava/util/Map;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$config:Ljava/util/Map;


# direct methods
.method constructor <init>(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 0

    .line 363
    iput-object p1, p0, Lcom/helpshift/support/Support$5;->val$activity:Landroid/app/Activity;

    iput-object p2, p0, Lcom/helpshift/support/Support$5;->val$config:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 366
    iget-object v0, p0, Lcom/helpshift/support/Support$5;->val$activity:Landroid/app/Activity;

    iget-object v1, p0, Lcom/helpshift/support/Support$5;->val$config:Ljava/util/Map;

    invoke-static {v0, v1}, Lcom/helpshift/support/SupportInternal;->showConversation(Landroid/app/Activity;Ljava/util/Map;)V

    return-void
.end method
