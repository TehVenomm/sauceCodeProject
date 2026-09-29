.class final Lcom/helpshift/support/Support$7;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$config:Ljava/util/Map;

.field final synthetic val$questionPublishId:Ljava/lang/String;


# direct methods
.method constructor <init>(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 675
    iput-object p1, p0, Lcom/helpshift/support/Support$7;->val$activity:Landroid/app/Activity;

    iput-object p2, p0, Lcom/helpshift/support/Support$7;->val$questionPublishId:Ljava/lang/String;

    iput-object p3, p0, Lcom/helpshift/support/Support$7;->val$config:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 678
    iget-object v0, p0, Lcom/helpshift/support/Support$7;->val$activity:Landroid/app/Activity;

    iget-object v1, p0, Lcom/helpshift/support/Support$7;->val$questionPublishId:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/support/Support$7;->val$config:Ljava/util/Map;

    invoke-static {v0, v1, v2}, Lcom/helpshift/support/SupportInternal;->showSingleFAQ(Landroid/app/Activity;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method
