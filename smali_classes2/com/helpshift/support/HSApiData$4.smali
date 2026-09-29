.class Lcom/helpshift/support/HSApiData$4;
.super Landroid/os/Handler;
.source "HSApiData.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/HSApiData;->getSection(Ljava/lang/String;Landroid/os/Handler;Landroid/os/Handler;Lcom/helpshift/support/FaqTagFilter;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/HSApiData;

.field final synthetic val$publishId:Ljava/lang/String;

.field final synthetic val$success:Landroid/os/Handler;


# direct methods
.method constructor <init>(Lcom/helpshift/support/HSApiData;Ljava/lang/String;Landroid/os/Handler;)V
    .locals 0

    .line 503
    iput-object p1, p0, Lcom/helpshift/support/HSApiData$4;->this$0:Lcom/helpshift/support/HSApiData;

    iput-object p2, p0, Lcom/helpshift/support/HSApiData$4;->val$publishId:Ljava/lang/String;

    iput-object p3, p0, Lcom/helpshift/support/HSApiData$4;->val$success:Landroid/os/Handler;

    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 1

    .line 506
    invoke-super {p0, p1}, Landroid/os/Handler;->handleMessage(Landroid/os/Message;)V

    .line 507
    iget-object p1, p0, Lcom/helpshift/support/HSApiData$4;->this$0:Lcom/helpshift/support/HSApiData;

    iget-object p1, p1, Lcom/helpshift/support/HSApiData;->sectionDAO:Lcom/helpshift/support/storage/SectionDAO;

    iget-object v0, p0, Lcom/helpshift/support/HSApiData$4;->val$publishId:Ljava/lang/String;

    invoke-interface {p1, v0}, Lcom/helpshift/support/storage/SectionDAO;->getSection(Ljava/lang/String;)Lcom/helpshift/support/Section;

    move-result-object p1

    .line 509
    iget-object v0, p0, Lcom/helpshift/support/HSApiData$4;->val$success:Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/Handler;->obtainMessage()Landroid/os/Message;

    move-result-object v0

    .line 510
    iput-object p1, v0, Landroid/os/Message;->obj:Ljava/lang/Object;

    .line 511
    iget-object p1, p0, Lcom/helpshift/support/HSApiData$4;->val$success:Landroid/os/Handler;

    invoke-virtual {p1, v0}, Landroid/os/Handler;->sendMessage(Landroid/os/Message;)Z

    return-void
.end method
