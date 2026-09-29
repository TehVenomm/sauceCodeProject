.class Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;
.super Ljava/lang/Object;
.source "AndroidFAQSearchDM.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidFAQSearchDM;->startFAQSearchIndexing()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/platform/AndroidFAQSearchDM;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidFAQSearchDM;)V
    .locals 0

    .line 33
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;->this$0:Lcom/helpshift/common/platform/AndroidFAQSearchDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 36
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;->this$0:Lcom/helpshift/common/platform/AndroidFAQSearchDM;

    iget-object v0, v0, Lcom/helpshift/common/platform/AndroidFAQSearchDM;->data:Lcom/helpshift/support/HSApiData;

    new-instance v1, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1$1;

    invoke-direct {v1, p0}, Lcom/helpshift/common/platform/AndroidFAQSearchDM$1$1;-><init>(Lcom/helpshift/common/platform/AndroidFAQSearchDM$1;)V

    new-instance v2, Landroid/os/Handler;

    invoke-direct {v2}, Landroid/os/Handler;-><init>()V

    const/4 v3, 0x0

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/support/HSApiData;->getSections(Landroid/os/Handler;Landroid/os/Handler;Lcom/helpshift/support/FaqTagFilter;)V

    return-void
.end method
